using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataTerminals
{
    // добавляем в словарь новые терминалы
    private static readonly Dictionary<int, bool> _isTerminalFirstTimeVisit = new Dictionary<int, bool>
    {
        { 1, false },
        { 2, false }
    };

    private static readonly string _filePath = Path.Combine(Application.persistentDataPath, "DataTerminals.json");

    public static bool IsTerminalFirstTimeVisit(int terminalNumber)
    {
        return _isTerminalFirstTimeVisit.ContainsKey(terminalNumber) && _isTerminalFirstTimeVisit[terminalNumber];
    }

    public static void SetTerminalAvailability(int terminalNumber, bool isAvailable)
    {
        if (_isTerminalFirstTimeVisit.ContainsKey(terminalNumber))
        {
            _isTerminalFirstTimeVisit[terminalNumber] = isAvailable;
        }
    }

    public static void SaveData()
    {
        string json = JsonUtility.ToJson(new DataTerminalsContainer
        {
            IsTerminalFirstTimeVisit = _isTerminalFirstTimeVisit
        }, true);

        File.WriteAllText(_filePath, json);
    }

    public static void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            DataTerminalsContainer dataContainer = JsonUtility.FromJson<DataTerminalsContainer>(json);
            foreach (var terminal in dataContainer.IsTerminalFirstTimeVisit)
            {
                _isTerminalFirstTimeVisit[terminal.Key] = terminal.Value;
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
        public Dictionary<int, bool> IsTerminalFirstTimeVisit = new Dictionary<int, bool>();
    }
}
