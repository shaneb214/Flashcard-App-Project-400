using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ExtensionMethods 
{
    public static bool AllCharactersEmptyOrWhiteSpace(this string value) => value.All(char.IsWhiteSpace);

    public static string RemoveSpecialCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();

        for (var i = list.Count; i > 0; i--)
            list.Swap(0, rng.Next(0, i));
    }

    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public static T GetAnyoneElseFromTheList<T>(this List<T> list,int elId)
    {
        if (elId >= list.Count) throw new ArgumentException();
        if (list.Count < 2) return default(T);
        System.Random rndElement = new System.Random();
        int random = rndElement.Next(0, list.Count - 1);
        if (random >= elId) random += 1;
        return list[random];
    }

    public static List<T> GetItemsFromListIgnoringIndex<T>(this List<T> list, int indexToIgnore,int numItemsToReturn)
    {
        if (indexToIgnore >= list.Count) throw new ArgumentException();
        if (numItemsToReturn >= list.Count) throw new ArgumentException();
        if (list.Count < 2) return null;

        List<int> indexesChosen = new List<int>();
        List<T> listToReturn = new List<T>();
        for (int i = 0; i < numItemsToReturn; i++)
        {
            System.Random rng = new System.Random();
            int randomIndex = rng.Next(0, list.Count);

            while (indexesChosen.Contains(randomIndex))
                randomIndex = (randomIndex + 1) % list.Count;

            listToReturn.Add(list[randomIndex]);
            indexesChosen.Add(randomIndex);
        }

        return listToReturn;
    }
}
