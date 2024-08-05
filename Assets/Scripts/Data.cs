using System.IO;
using UnityEngine;

/// <summary>
/// Класс для сохранения и закрузки данных
/// </summary>
public static class Data
{
    private static int _hp;
    private static int _checkpointNumber;
    private static bool _isHumanFormAvaible;
    private static bool _isHummerFormAvaible;
    private static bool _isBreakerFormAvaible;
    private static bool _isMimicFormAvaible;
    private static bool _isMirrorFormAvaible;

    // Путь к файлу для сохранения данных
    private static string _filePath = Path.Combine(Application.persistentDataPath, "Data.json");

    public static int HP
    {
        get => _hp;
        set => _hp = Mathf.Max(0, value);
    }

    public static int CheckpointNumber
    {
        get => _checkpointNumber;
        set => _checkpointNumber = Mathf.Max(0, value);
    }

    public static bool IsHumanFormAvaible
    {
        get => _isHumanFormAvaible;
        set => _isHumanFormAvaible = value;
    }

    public static bool IsHummerFormAvaible
    {
        get => _isHummerFormAvaible;
        set => _isHummerFormAvaible = value;
    }

    public static bool IsBreakerFormAvaible
    {
        get => _isBreakerFormAvaible;
        set => _isBreakerFormAvaible = value;
    }

    public static bool IsMimicFormAvaible
    {
        get => _isMimicFormAvaible;
        set => _isMimicFormAvaible = value;
    }

    public static bool IsMirrorFormAvaible
    {
        get => _isMirrorFormAvaible;
        set => _isMirrorFormAvaible = value;
    }

    /// <summary>
    /// Сохранаяет данные в JSON-файл
    /// </summary>
    public static void DataSave()
    {
        string json = JsonUtility.ToJson(new DataContainer
        {
            hp = _hp,
            checkpointNumber = _checkpointNumber,
            isHumanFormAvaible = _isHumanFormAvaible,
            isHummerFormAvaible = _isHummerFormAvaible,
            isBreakerFormAvaible = _isBreakerFormAvaible,
            isMimicFormAvaible = _isMimicFormAvaible,
            isMirrorFormAvaible = _isMirrorFormAvaible
        },true);

        File.WriteAllText(_filePath, json);
    }

    /// <summary>
    /// Загружает данные из JSON-файла
    /// </summary>
    public static void DataLoad()
    {
        if (File.Exists(_filePath))
        {
            string json =  File.ReadAllText(_filePath);
            DataContainer dataContainer = JsonUtility.FromJson<DataContainer>(json);
            HP = dataContainer.hp;
            CheckpointNumber = dataContainer.checkpointNumber;
            IsHumanFormAvaible = dataContainer.isHumanFormAvaible;
            IsHummerFormAvaible = dataContainer.isHummerFormAvaible;
            IsBreakerFormAvaible = dataContainer.isBreakerFormAvaible;
            IsMimicFormAvaible = dataContainer.isMimicFormAvaible;
            IsMirrorFormAvaible = dataContainer.isMirrorFormAvaible;
        }
        else
        {
           DataSave();
        }
    }

    /// <summary>
    /// Вспомогательный класс-контейнер для данных
    /// </summary>
    [System.Serializable]
    private class DataContainer
    {
        public int hp;
        public int checkpointNumber;
        public bool isHumanFormAvaible;
        public bool isHummerFormAvaible;
        public bool isBreakerFormAvaible;
        public bool isMimicFormAvaible;
        public bool isMirrorFormAvaible;
    }
}