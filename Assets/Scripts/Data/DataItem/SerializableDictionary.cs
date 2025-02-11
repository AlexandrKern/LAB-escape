using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ������������ �������.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    public List<TKey> keys = new List<TKey>();
    public List<TValue> values = new List<TValue>();

    public SerializableDictionary() { }

    public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
    {
        foreach (var item in dictionary)
        {
            keys.Add(item.Key);
            values.Add(item.Value);
        }
    }

    /// <summary>
    /// ����������� ������������� ������� � �������.
    /// </summary>
    public Dictionary<TKey, TValue> ToDictionary()
    {
        var dict = new Dictionary<TKey, TValue>();
        if (keys.Count != values.Count)
        {
            Debug.LogError("������: ���������� ������ � �������� �� ���������!");
            return dict;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            dict[keys[i]] = values[i];
        }
        return dict;
    }
}
