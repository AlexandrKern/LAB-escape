using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataTerminals
{
    // добавляем в словарь новые терминалы (руками прямо тут)
    private static Dictionary<int, bool> _isTerminalFirstTimeVisit = new Dictionary<int, bool>
    {
        { 1, false },
        { 2, false }
    };

    private static string _filePathDataTerminals = Path.Combine(Application.persistentDataPath, $"DataTerminals{DataUsername.UserName}.json");

    private static string FilePathDataTerminals
    {
        get { return Path.Combine(Application.persistentDataPath, $"DataTerminals{DataUsername.UserName}.json"); }
    }

    // проверка наличия сохранений (нужно для доступности кнопок в главном меню)
    public static bool IsFirstTerminalWasFirstTimeVisit()
    {
        if (File.Exists(_filePathDataTerminals))
        { 
            return true;
        }
        else { return false; }
    }

    public static bool IsTerminalFirstTimeVisit(int terminalNumber)
    {
        return _isTerminalFirstTimeVisit.ContainsKey(terminalNumber) && !_isTerminalFirstTimeVisit[terminalNumber];
    }

    public static void SetTerminalAvailability(int terminalNumber, bool isAvailable)
    {
        if (_isTerminalFirstTimeVisit.ContainsKey(terminalNumber))
        {
            _isTerminalFirstTimeVisit[terminalNumber] = isAvailable;
        }
        else
        {
            Debug.LogWarning($"Терминал с номером {terminalNumber} не найден.");
        }
    }

    public static void SaveData()
    {
        DataTerminalsContainer dataContainer = new DataTerminalsContainer
        {
            TerminalKeys = new List<int>(_isTerminalFirstTimeVisit.Keys),
            TerminalValues = new List<bool>(_isTerminalFirstTimeVisit.Values)
        };

        string json = JsonUtility.ToJson(dataContainer, true);
        File.WriteAllText(FilePathDataTerminals, json);
    }

    public static void LoadData()
    {
        if (File.Exists(FilePathDataTerminals))
        {
            string json = File.ReadAllText(FilePathDataTerminals);
            DataTerminalsContainer dataContainer = JsonUtility.FromJson<DataTerminalsContainer>(json);

            _isTerminalFirstTimeVisit = new Dictionary<int, bool>();
            for (int i = 0; i < dataContainer.TerminalKeys.Count; i++)
            {
                _isTerminalFirstTimeVisit[dataContainer.TerminalKeys[i]] = dataContainer.TerminalValues[i];
            }
        }
        else
        {
            SaveData();
        }
    }

    [System.Serializable]
    private class DataTerminalsContainer
    {
        public List<int> TerminalKeys;
        public List<bool> TerminalValues;
    }
}
