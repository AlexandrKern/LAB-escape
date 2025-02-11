using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// ��������� ������� � ���������.
/// </summary>
public static class DataItem
{
    public static Dictionary<string, Item> items = new Dictionary<string, Item>();

    private static string _filePath = Path.Combine(Application.persistentDataPath, "DataItem.json");

    /// <summary>
    /// ��������� �������� �� �����.
    /// </summary>
    public static Item GetItem(string key)
    {
        if (items.TryGetValue(key, out Item item))
        {
            return item;
        }
        return null;
    }

    /// <summary>
    /// ��������� ������� � �������.
    /// </summary>
    public static void AddItem(Item item)
    {
        if (!items.ContainsKey(item.itemName))
        {
            items.Add(item.itemName, item);
        }
    }

    /// <summary>
    /// ��������� ������ � ���������.
    /// </summary>
    public static void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            SerializableDictionary<string, Item> serializableDictionary = JsonConvert.DeserializeObject<SerializableDictionary<string, Item>>(json);
            items = serializableDictionary?.ToDictionary() ?? new Dictionary<string, Item>();
        }
        else
        {
            SaveData();
        }
    }

    /// <summary>
    /// ��������� ������ � ���������.
    /// </summary>
    public static void SaveData()
    {
        SerializableDictionary<string, Item> serializableDictionary = new SerializableDictionary<string, Item>(items);
        string json = JsonConvert.SerializeObject(serializableDictionary, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }
}
