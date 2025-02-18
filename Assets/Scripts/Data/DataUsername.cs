using System.IO;
using UnityEngine;

[System.Serializable]
public class DataUsernameContainer
{
    public string UserNameFromContainer;
    public string[] UserNames;
}

public class DataUsername
{
    private static readonly string _filePath = Path.Combine(Application.persistentDataPath, "DataUsername.json");

    public static string UserName { get; set; }

    public static void SaveUsername()
    {
        DataUsernameContainer container = new DataUsernameContainer
        {
            UserNameFromContainer = UserName
        };
        Debug.Log(container.UserNameFromContainer);
        string json = JsonUtility.ToJson(container, true);
        File.WriteAllText(_filePath, json);
    }

    public static void LoadUsername()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            DataUsernameContainer container = JsonUtility.FromJson<DataUsernameContainer>(json);
            UserName = container.UserNameFromContainer;
        }
        else
        {
            UserName = "";
        }
    }
}
