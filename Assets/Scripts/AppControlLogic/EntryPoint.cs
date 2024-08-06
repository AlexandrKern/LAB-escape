using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    SceneLoader sceneLoader = new SceneLoader();

    private void Start()
    {
        StartButton.StartButtonPushed.AddListener(GameStart);
    }

    public void GameStart()
    {
        sceneLoader.LoadSceneAsync("Game_Scene");
    }
}