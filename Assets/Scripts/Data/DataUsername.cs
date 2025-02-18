using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class DataUsernameContainer
{
    public string LastUsername;
    public string[] UserNames;
}

public class DataUsername
{
    private static readonly string _filePath = Path.Combine(Application.persistentDataPath, "DataUsername.json");

    public static string UserName { get; set; }
    public static string[] AllUserNames { get; set; }

    public static void SaveUsername()
    {
        DataUsernameContainer container;

        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            container = JsonUtility.FromJson<DataUsernameContainer>(json);
            if (container == null)
            {
                container = new DataUsernameContainer();
            }
        }
        else
        {
            container = new DataUsernameContainer();
        }

        container.LastUsername = UserName;

        List<string> usernamesList = new List<string>();
        if (container.UserNames != null)
        {
            usernamesList.AddRange(container.UserNames);
        }

        if (!usernamesList.Contains(UserName))
        {
            usernamesList.Add(UserName);
        }

        container.UserNames = usernamesList.ToArray();

        Debug.Log("Сохранённое имя: " + UserName);
        string jsonOut = JsonUtility.ToJson(container, true);
        File.WriteAllText(_filePath, jsonOut);
    }


    public static void LoadUsername()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            DataUsernameContainer container = JsonUtility.FromJson<DataUsernameContainer>(json);
            UserName = container.LastUsername;

            AllUserNames = container.UserNames != null ? container.UserNames : new string[0];

            Debug.Log("Текущее имя: " + UserName);
            foreach (var name in AllUserNames)
            {
                Debug.Log("Имя из массива: " + name);
            }
        }
        else
        {
            UserName = "";
            AllUserNames = new string[0];
        }
    }
}
