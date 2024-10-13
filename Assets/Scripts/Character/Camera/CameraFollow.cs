using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CameraFollow : MonoBehaviour
	{
	    private Transform _target;
        private Rigidbody2D _targetRG;
		[SerializeField] private float smoothSpeed = 0.125f;
		[SerializeField] private float smoothChangeDuration = 1;
		public Vector3 offset;
		[Header("Camera bounds")]
		public Vector3 minCamerabounds;
		public Vector3 maxCamerabounds;
        private bool _isFacingRight = true;

    private void OnEnable()
    {
        PlayerSpawnLocations.OnCharacterSpawn.AddListener(FindAnObjectToFollow);
    }

    private void OnDisable()
    {
        PlayerSpawnLocations.OnCharacterSpawn.RemoveListener(FindAnObjectToFollow);
    }

    public void FindAnObjectToFollow()
    {
        GameObject targetGO = GameObject.FindWithTag("Player");
		if (targetGO != null)
        _target = targetGO.GetComponent<Transform>();
        _targetRG = _target.gameObject.GetComponent<Rigidbody2D>();
    }

    public void ChangeOffsetX(float targetX)
	{
        SmoothOffsetChangeXAsync(targetX, smoothChangeDuration).Forget();
    }

	bool ifOffsetChangingOnProcess;

    private async UniTask SmoothOffsetChangeXAsync(float targetX, float duration)
    {
		if (!ifOffsetChangingOnProcess)
		{
            ifOffsetChangingOnProcess = true;
            float startX = offset.x;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newX = Mathf.Lerp(startX, targetX, elapsedTime / duration);
                offset = new Vector3(newX, offset.y, offset.z);

                await UniTask.Yield();
            }

            offset = new Vector3(targetX, offset.y, offset.z);
			ifOffsetChangingOnProcess = false;
        }
    }

    private void LateUpdate()
	{
		Vector3 desiredPosition = _target.localPosition + offset;
		var localPosition = transform.localPosition;
		Vector3 smoothedPosition = Vector3.Lerp(localPosition, desiredPosition, smoothSpeed);
		localPosition = smoothedPosition;

		localPosition = new Vector3(
			Mathf.Clamp(localPosition.x, minCamerabounds.x, maxCamerabounds.x),
			Mathf.Clamp(localPosition.y, minCamerabounds.y, maxCamerabounds.y),
			Mathf.Clamp(localPosition.z, minCamerabounds.z, maxCamerabounds.z)
		);
		transform.localPosition = localPosition;
        if (_targetRG.velocity != null)
        {
            if (_targetRG.velocity.x > 0.5f && !_isFacingRight)
            {
                _isFacingRight = true;
                ChangeOffsetX(10);
            }
            else if (_targetRG.velocity.x < -0.5f && _isFacingRight)
            {
                _isFacingRight = false;
                ChangeOffsetX(-10);
            }
        }
    }
}
