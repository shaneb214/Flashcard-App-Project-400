using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JSONHelper 
{
    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [Serializable]
    public class Wrapper<T>
    {
        public List<T> Items;
    }

    public static string ToJson<T>(List<T> list)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = list;
        return JsonUtility.ToJson(wrapper);
    }


    public static string ModifyJSONString(string json)
    {
        return "{\"Items\":" + json + "}";
    }

    public static List<T> ReadListDataFromJSONFile<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return FromJson<T>(json);
    }

    public static void SaveListDataToJSON<T>(List<T> list,string path)
    {
        string json = JSONHelper.ToJson(list);

        if (File.Exists(path) == false)
            File.Create(path).Dispose();

        using (TextWriter writer = new StreamWriter(path, false))
        {
            writer.WriteLine(json);
            writer.Close();
        }
    }
}