using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    SceneLoader sceneLoader = new SceneLoader();

    private void Awake()
    {
        // временный код пока нет стартрового меню
        // по необходимости раскомментировать чтобы прогресс загружался
        //DataTerminals.LoadData();
        //Data.LoadData();
    }

    private void Start()
    {
        StartButton.StartButtonPushed.AddListener(GameStart);
    }

    public void GameStart()
    {
        DataTerminals.LoadData();
        Data.LoadData();
        sceneLoader.LoadSceneAsync("Game_Scene");
    }
}