using System.Collections;
using UnityEngine;

public static class Utils
{
    static private MonoBehaviour _mainCameraBehaviour = null;
    static private IEnumerator Shake(Transform target, float totalTime, float speed)
    {
        float timer = totalTime;
        Vector3 offset = new Vector3(0, 0, 0);
        while (timer > 0)
        {
            Vector3 delta = Random.insideUnitSphere * Time.deltaTime * speed;
            Vector3 newOffset = (offset + delta) * timer/totalTime;
            target.position += newOffset - offset;
            offset = newOffset;

            timer -= Time.deltaTime;
            yield return null;
        }
        target.position -= offset;
    }
    static public void ShakeCamera(float time, float speed)
    {
        if(_mainCameraBehaviour == null)
        {
            if(!Camera.main.TryGetComponent<MonoBehaviour>(out _mainCameraBehaviour))
            {
                Debug.LogWarning("Utils.ShakeCamera: " +
                    "There must be at least one monobehaviour on the main camera to shake it.");
                return;
            }
        }

        _mainCameraBehaviour.StartCoroutine(Shake(_mainCameraBehaviour.transform, time, speed));
    }

}
