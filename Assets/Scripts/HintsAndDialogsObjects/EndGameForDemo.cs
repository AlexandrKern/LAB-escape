using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameForDemo : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas.SetActive(true);
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneLoader sceneLoader = new SceneLoader();
        sceneLoader.LoadSceneAsync("MainMenu");
    }
}
