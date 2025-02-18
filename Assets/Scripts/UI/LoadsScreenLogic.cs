using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadsScreenLogic : MonoBehaviour
{
    [SerializeField] GameObject loadButton;
    [SerializeField] GameObject loadButtonsGrid;
    [SerializeField] GameObject loadscreen;

    void Start()
    {
        for (int i = 0; i < DataUsername.AllUserNames.Length; i++)
        {
            GameObject newLoadButton = Instantiate(loadButton, loadButtonsGrid.transform);
            newLoadButton.name = "LoadButton_" + DataUsername.AllUserNames[i];

            TextMeshProUGUI textComponent = newLoadButton.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = DataUsername.AllUserNames[i];
            }

            Button buttonComponent = newLoadButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() =>
                {
                    LoadGameWithASelectedUser(newLoadButton);
                });
            }
        }
    }

    void LoadGameWithASelectedUser(GameObject clickedButton)
    {
        TextMeshProUGUI textComponent = clickedButton.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            loadscreen.SetActive(true);
            DataUsername.UserName = textComponent.text;
            Debug.Log("Загружаем игру за " + DataUsername.UserName);
            DataUsername.SaveUsername();
            FindObjectOfType<EntryPoint>().GameCont();
        }
    }
}
