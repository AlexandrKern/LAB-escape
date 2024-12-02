using UnityEngine;
using UnityEngine.Events;

public class EntryPoint : MonoBehaviour
{
    SceneLoader sceneLoader = new SceneLoader();
    public static UnityEvent StartButtonPushed = new UnityEvent();

    private void Awake()
    {
        // временный код длф работы без стартрового меню
        // по необходимости раскомментировать чтобы прогресс загружался
        //DataTerminals.LoadData();
        //Data.LoadData();
        //DataItem.LoadData();
    }

    private void Start()
    {
        MMButtonsBeh.ContButtonPushed.AddListener(GameCont);
        MMButtonsBeh.NGButtonPushed.AddListener(NewGame);
    }

    public void GameCont()
    {
        DataTerminals.LoadData();
        Data.LoadData();
        DataItem.LoadData();
        sceneLoader.LoadSceneAsync("Biom1"); // исправить
    }

    public void NewGame()
    {
        sceneLoader.LoadSceneAsync("Biom1"); // исправить
    }
}