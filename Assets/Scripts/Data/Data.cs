using System;
using System.IO;
using UnityEngine;

/// <summary>
/// ����� ��� ���������� � �������� ������
/// </summary>
public static class Data
{
    private static int _hp;
    private static int _fullHP;
    private static int _checkpointNumber;
    private static bool _isHumanFormAvailable;
    private static bool _isHammerFormAvailable;
    private static bool _isBreakerFormAvailable;
    private static bool _isMimicFormAvailable;
    private static bool _isMirrorFormAvailable;

    // ���� � ����� ��� ���������� ������
    private static string _filePathData = Path.Combine(Application.persistentDataPath, $"Data{DataUsername.UserName}.json");

    public static int FullHP
    {
        get => _fullHP;
        set => _fullHP = Mathf.Max(0, value);
    }

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

    public static bool IsAnthropomorphicFormAvailable
    {
        get => _isHumanFormAvailable;
        set => _isHumanFormAvailable = value;
    }

    public static bool IsHammerFormAvailable
    {
        get => _isHammerFormAvailable;
        set => _isHammerFormAvailable = value;
    }

    public static bool IsBurglarFormAvailable
    {
        get => _isBreakerFormAvailable;
        set => _isBreakerFormAvailable = value;
    }

    public static bool IsMimicryFormAvailable
    {
        get => _isMimicFormAvailable;
        set => _isMimicFormAvailable = value;
    }

    public static bool IsMirrorFormAvailable
    {
        get => _isMirrorFormAvailable;
        set => _isMirrorFormAvailable = value;
    }


    /// <summary>
    /// ��������� ������ � JSON-����
    /// </summary>
    public static void SaveData()
    {
        string json = JsonUtility.ToJson(new DataContainer
        {
            HP = _hp,
            FullHP = _fullHP,
            CheckpointNumber = _checkpointNumber,
            IsHumanFormAvailable = _isHumanFormAvailable,
            IsHammerFormAvailable = _isHammerFormAvailable,
            IsBreakerFormAvailable = _isBreakerFormAvailable,
            IsMimicFormAvailable = _isMimicFormAvailable,
            IsMirrorFormAvailable = _isMirrorFormAvailable,
        }, true);

        File.WriteAllText(_filePathData, json);
    }

    /// <summary>
    /// ��������� ������ �� JSON-�����
    /// </summary>
    public static void LoadData()
    {
        if (File.Exists(_filePathData))
        {
            string json = File.ReadAllText(_filePathData);
            DataContainer dataContainer = JsonUtility.FromJson<DataContainer>(json);
            HP = dataContainer.HP;
            FullHP = dataContainer.FullHP;
            CheckpointNumber = dataContainer.CheckpointNumber;
            IsAnthropomorphicFormAvailable = dataContainer.IsHumanFormAvailable;
            IsHammerFormAvailable = dataContainer.IsHammerFormAvailable;
            IsBurglarFormAvailable = dataContainer.IsBreakerFormAvailable;
            IsMimicryFormAvailable = dataContainer.IsMimicFormAvailable;
            IsMirrorFormAvailable = dataContainer.IsMirrorFormAvailable;
        }
        else
        {
            SaveData();
        }
    }

    /// <summary>
    /// ��������������� �����-��������� ��� ������
    /// </summary>
    [System.Serializable]
    private class DataContainer
    {
        public int HP;
        public int FullHP;
        public int CheckpointNumber;
        public bool IsHumanFormAvailable;
        public bool IsHammerFormAvailable;
        public bool IsBreakerFormAvailable;
        public bool IsMimicFormAvailable;
        public bool IsMirrorFormAvailable;
    }
}