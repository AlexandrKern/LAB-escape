using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Управляет данными о предметах.
/// </summary>
public static class DataItem 
{
    public static Dictionary<string, Item> items = new Dictionary<string, Item>();

    private static string _filePath = Path.Combine(Application.persistentDataPath, "DataItem.json");

    /// <summary>
    /// Получение предмета по ключу
    /// </summary>
    /// <param name="key">Имя предмета</param>
    /// <returns></returns>
    public static Item GetItem(string key)
    {
        if (items.TryGetValue(key,out Item item))
        {
            return item;
        }
        return null;
    }
    /// <summary>
    /// Добавляет предмет в словарь
    /// </summary>
    /// <param name="item">Предмет</param>
    public static void AddItem(Item item)
    {
        if (!items.ContainsKey(item.name))
        {
            items.Add(item.name, item);
        }
    }
    /// <summary>
    /// Загружает данные о предметах
    /// </summary>
    public static void LoadData()
    {
        if(File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            SerializableDictionary<string,Item> serializableDictionary = JsonUtility.FromJson<SerializableDictionary<string,Item>>(json);
            items = serializableDictionary.ToDictionary();
        }
        else
        {
            SaveData();
        }
    }
    /// <summary>
    /// Сохраняет данные о предметах
    /// </summary>
    public static void SaveData()
    {
        SerializableDictionary<string,Item> serializableDictionary = new SerializableDictionary<string, Item>(items);
        string json = JsonUtility.ToJson(serializableDictionary,true);
        File.WriteAllText(_filePath, json);
    }
}
