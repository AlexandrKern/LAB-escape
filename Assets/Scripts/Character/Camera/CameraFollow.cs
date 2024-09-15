using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CameraFollow : MonoBehaviour
	{
	    private Transform target;
		[SerializeField] private float smoothSpeed = 0.125f;
		[SerializeField] private float smoothChangeDuration = 1;
		public Vector3 offset;
		[Header("Camera bounds")]
		public Vector3 minCamerabounds;
		public Vector3 maxCamerabounds;

    private void Start()
    {
		while (target == null) 
		{
            GameObject targetGO = GameObject.FindGameObjectWithTag("Player");
			if (targetGO != null)
            target = targetGO.GetComponent<Transform>();
        }
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
		Vector3 desiredPosition = target.localPosition + offset;
		var localPosition = transform.localPosition;
		Vector3 smoothedPosition = Vector3.Lerp(localPosition, desiredPosition, smoothSpeed);
		localPosition = smoothedPosition;

		localPosition = new Vector3(
			Mathf.Clamp(localPosition.x, minCamerabounds.x, maxCamerabounds.x),
			Mathf.Clamp(localPosition.y, minCamerabounds.y, maxCamerabounds.y),
			Mathf.Clamp(localPosition.z, minCamerabounds.z, maxCamerabounds.z)
		);
		transform.localPosition = localPosition;
	}
}
