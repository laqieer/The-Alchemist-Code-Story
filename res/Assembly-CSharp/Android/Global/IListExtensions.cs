// Decompiled with JetBrains decompiler
// Type: IListExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

internal static class IListExtensions
{
  private const string DEFAULT_DELIMITER = " | ";
  private const string DEFAULT_START_CAP = " [ ";
  private const string DEFAULT_END_CAP = " ] ";

  public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
  {
    if (list == null)
      throw new Exception("empty list" + (object) list);
    if (firstIndex < 0 || firstIndex >= list.Count)
      throw new Exception("first index OutOfBounds: " + (object) firstIndex + (object) list);
    if (secondIndex < 0 || secondIndex >= list.Count)
      throw new Exception("second index OutOfBounds: " + (object) secondIndex + (object) list);
    if (firstIndex == secondIndex)
      return;
    T obj = list[firstIndex];
    list[firstIndex] = list[secondIndex];
    list[secondIndex] = obj;
  }

  public static string ListToString<T>(this IList<T> list, bool isNewLinePerElement = false, bool isNumbered = false, bool isNumberingFromOne = false, string inAlternateDelimiter = "", string inAlternateStartCap = "", string inAlternateEndCap = "")
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (!isNewLinePerElement)
    {
      if (string.IsNullOrEmpty(inAlternateStartCap))
        stringBuilder.Append(" [ ");
      else
        stringBuilder.Append(inAlternateStartCap);
    }
    if (list == null)
      stringBuilder.Append("--NULL List--");
    else if (list.Count == 0)
    {
      stringBuilder.Append("--EMPTY List--");
    }
    else
    {
      for (int index = 0; index < list.Count; ++index)
      {
        if (isNumbered)
        {
          if (isNumberingFromOne)
            stringBuilder.Append(index + 1);
          else
            stringBuilder.Append(index);
          stringBuilder.Append(": ");
        }
        if ((object) list[index] != null)
          stringBuilder.Append(list[index].ToString());
        else
          stringBuilder.Append("--NULL--");
        if (isNewLinePerElement)
          stringBuilder.Append('\n');
        else if (index < list.Count - 1)
        {
          if (string.IsNullOrEmpty(inAlternateDelimiter))
            stringBuilder.Append(" | ");
          else
            stringBuilder.Append(inAlternateDelimiter);
        }
      }
    }
    if (!isNewLinePerElement)
    {
      if (string.IsNullOrEmpty(inAlternateEndCap))
        stringBuilder.Append(" ] ");
      else
        stringBuilder.Append(inAlternateEndCap);
    }
    return stringBuilder.ToString();
  }

  public static string DictionaryToString<T, K>(this Dictionary<T, K> inDictionary, bool isNewLinePerElement = false, bool isNumbered = false, bool isNumberingFromOne = false, string inAlternateDelimiter = "", string inAlternateStartCap = "", string inAlternateEndCap = "")
  {
    if (inDictionary != null)
      return inDictionary.ToList<KeyValuePair<T, K>>().ListToString<KeyValuePair<T, K>>(isNewLinePerElement, isNumbered, isNumberingFromOne, inAlternateDelimiter, inAlternateStartCap, inAlternateEndCap);
    return "--EMPTY DICTIONARY--";
  }

  public static Dictionary<T, string> ConvertValuesToString<T, K>(this Dictionary<T, K> inDictionary)
  {
    Dictionary<T, string> dictionary = new Dictionary<T, string>();
    using (Dictionary<T, K>.Enumerator enumerator = inDictionary.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<T, K> current = enumerator.Current;
        dictionary.Add(current.Key, current.Value.ToString());
      }
    }
    return dictionary;
  }

  public static string[] ListToArrayOfStrings<T>(this IList<T> list, bool isNewLinePerElement = false, bool isNumbered = false, bool isNumberingFromOne = false)
  {
    List<string> stringList = new List<string>();
    if (list == null)
      stringList.Add("--NULL List--");
    else if (list.Count == 0)
    {
      stringList.Add("--EMPTY List--");
    }
    else
    {
      for (int index = 0; index < list.Count; ++index)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (isNumbered)
        {
          if (isNumberingFromOne)
            stringBuilder.Append(index + 1);
          else
            stringBuilder.Append(index);
          stringBuilder.Append(": ");
        }
        if ((object) list[index] != null)
        {
          stringBuilder.Append(list[index].ToString());
          stringList.Add(stringBuilder.ToString());
        }
        else
        {
          stringBuilder.Append("--NULL--");
          stringList.Add(stringBuilder.ToString());
        }
      }
    }
    return stringList.ToArray();
  }

  public static T GetRandElementInList<T>(this IList<T> list, System.Random inRandom = null)
  {
    if (inRandom == null)
      inRandom = new System.Random();
    return list[inRandom.Next(list.Count)];
  }

  public static T GetRandElementInListAndRemoveRandElement<T>(this IList<T> list, System.Random inRandom = null)
  {
    if (list.Count == 0)
      return default (T);
    if (inRandom == null)
      inRandom = new System.Random();
    int index = inRandom.Next(0, list.Count);
    T obj = list[index];
    list.RemoveAt(index);
    return obj;
  }

  public static void Shuffle<T>(this IList<T> list, System.Random inRandom = null)
  {
    if (inRandom == null)
      inRandom = new System.Random();
    int count = list.Count;
    while (count > 1)
    {
      --count;
      int index = inRandom.Next(count + 1);
      T obj = list[index];
      list[index] = list[count];
      list[count] = obj;
    }
  }

  public static List<Vector3> DeepCopyWorldPositionsFromTransformList(this IList<Transform> list)
  {
    List<Vector3> vector3List = new List<Vector3>();
    foreach (Transform transform in (IEnumerable<Transform>) list)
      vector3List.Add(transform.position);
    return vector3List;
  }

  public static List<Vector3> DeepCopyVector3List(this IList<Vector3> list)
  {
    if (list.Count <= 0)
      return (List<Vector3>) null;
    List<Vector3> vector3List = new List<Vector3>();
    foreach (Vector3 vector3 in (IEnumerable<Vector3>) list)
      vector3List.Add(vector3);
    return vector3List;
  }

  public static List<string> DeepCopyStringList(this IList<string> list)
  {
    if (list.Count <= 0)
      return (List<string>) null;
    List<string> stringList = new List<string>();
    foreach (string str in (IEnumerable<string>) list)
      stringList.Add(string.Copy(str));
    return stringList;
  }

  public static List<int> DeepCopyIntList(this IList<int> list)
  {
    if (list.Count <= 0)
      return (List<int>) null;
    List<int> intList = new List<int>();
    foreach (int num in (IEnumerable<int>) list)
      intList.Add(num);
    return intList;
  }

  public static int NumberOf<T>(this IList<T> list, Func<T, bool> inPredicate)
  {
    int num = 0;
    for (int index = 0; index < list.Count; ++index)
    {
      if (inPredicate(list[index]))
        ++num;
    }
    return num;
  }

  public static int NumberOf<T>(this IList<T> list, T inItemWeAreLookingToBeEqualsTo)
  {
    int num = 0;
    for (int index = 0; index < list.Count; ++index)
    {
      if (inItemWeAreLookingToBeEqualsTo.Equals((object) list[index]))
        ++num;
    }
    return num;
  }

  public static void Clear<T>(this T[] list)
  {
    for (int index = 0; index < list.Length; ++index)
      list[index] = default (T);
  }

  public static List<T> ShallowCopy<T>(this IList<T> list)
  {
    List<T> objList = new List<T>();
    foreach (T obj in (IEnumerable<T>) list)
      objList.Add(obj);
    return objList;
  }

  public static List<T> ShallowCopyIgnoring<T>(this IList<T> list, T inIgnoreThisElement)
  {
    List<T> objList = new List<T>();
    foreach (T obj in (IEnumerable<T>) list)
    {
      if (!obj.Equals((object) inIgnoreThisElement))
        objList.Add(obj);
    }
    return objList;
  }

  public static IList<T> Clone<T>(this IList<T> inList) where T : ICloneable
  {
    return (IList<T>) Enumerable.ToList<T>(inList.Select<T, T>((Func<T, T>) (item => (T) item.Clone())));
  }

  public static float AddedTotal(this IList<float> list)
  {
    float num1 = 0.0f;
    foreach (float num2 in (IEnumerable<float>) list)
      num1 += num2;
    return num1;
  }

  public static int AddedTotal(this IList<int> list)
  {
    int num1 = 0;
    foreach (int num2 in (IEnumerable<int>) list)
      num1 += num2;
    return num1;
  }
}
