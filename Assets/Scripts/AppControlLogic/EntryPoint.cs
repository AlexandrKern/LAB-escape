using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EntryPoint : MonoBehaviour
{
    SceneLoader sceneLoader = new SceneLoader();
    public static UnityEvent StartButtonPushed = new UnityEvent();
    [SerializeField] VideoPlayer videoPlayer;

    private void Awake()
    {
        // временный код длф работы без стартрового меню
        // по необходимости раскомментировать чтобы прогресс загружался
        //if (SceneManager.GetActiveScene().name == "Biom1")
        //{
        //    DataTerminals.LoadData();
        //    Data.LoadData();
        //    DataItem.LoadData();
        //}
    }

    private void Start()
    {
        MMButtonsBeh.OnContButtonPushed.AddListener(GameCont);
        MMButtonsBeh.OnNGButtonPushed.AddListener(NewGame);
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
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
        sceneLoader.LoadSceneAsync(videoPlayer, "Biom1"); // исправить
    }
}