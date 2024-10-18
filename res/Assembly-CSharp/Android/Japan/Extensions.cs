// Decompiled with JetBrains decompiler
// Type: Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class Extensions
{
  public static Dictionary<MethodInfo, ParameterInfo[]> ParametersOfMethods = new Dictionary<MethodInfo, ParameterInfo[]>();

  public static ParameterInfo[] GetCachedParemeters(this MethodInfo mo)
  {
    ParameterInfo[] parameters;
    if (!Extensions.ParametersOfMethods.TryGetValue(mo, out parameters))
    {
      parameters = mo.GetParameters();
      Extensions.ParametersOfMethods[mo] = parameters;
    }
    return parameters;
  }

  public static PhotonView[] GetPhotonViewsInChildren(this GameObject go)
  {
    return go.GetComponentsInChildren<PhotonView>(true);
  }

  public static PhotonView GetPhotonView(this GameObject go)
  {
    return go.GetComponent<PhotonView>();
  }

  public static bool AlmostEquals(this Vector3 target, Vector3 second, float sqrMagnitudePrecision)
  {
    return (double) (target - second).sqrMagnitude < (double) sqrMagnitudePrecision;
  }

  public static bool AlmostEquals(this Vector2 target, Vector2 second, float sqrMagnitudePrecision)
  {
    return (double) (target - second).sqrMagnitude < (double) sqrMagnitudePrecision;
  }

  public static bool AlmostEquals(this Quaternion target, Quaternion second, float maxAngle)
  {
    return (double) Quaternion.Angle(target, second) < (double) maxAngle;
  }

  public static bool AlmostEquals(this float target, float second, float floatDiff)
  {
    return (double) Mathf.Abs(target - second) < (double) floatDiff;
  }

  public static void Merge(this IDictionary target, IDictionary addHash)
  {
    if (addHash == null || target.Equals((object) addHash))
      return;
    IEnumerator enumerator = addHash.Keys.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        target[current] = addHash[current];
      }
    }
    finally
    {
      IDisposable disposable;
      if ((disposable = enumerator as IDisposable) != null)
        disposable.Dispose();
    }
  }

  public static void MergeStringKeys(this IDictionary target, IDictionary addHash)
  {
    if (addHash == null || target.Equals((object) addHash))
      return;
    IEnumerator enumerator = addHash.Keys.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        if (current is string)
          target[current] = addHash[current];
      }
    }
    finally
    {
      IDisposable disposable;
      if ((disposable = enumerator as IDisposable) != null)
        disposable.Dispose();
    }
  }

  public static string ToStringFull(this IDictionary origin)
  {
    return SupportClass.DictionaryToString(origin, false);
  }

  public static string ToStringFull(this object[] data)
  {
    if (data == null)
      return "null";
    string[] strArray = new string[data.Length];
    for (int index = 0; index < data.Length; ++index)
    {
      object obj = data[index];
      strArray[index] = obj == null ? "null" : obj.ToString();
    }
    return string.Join(", ", strArray);
  }

  public static Hashtable StripToStringKeys(this IDictionary original)
  {
    Hashtable hashtable = new Hashtable();
    if (original != null)
    {
      IEnumerator enumerator = original.Keys.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          object current = enumerator.Current;
          if (current is string)
            hashtable[current] = original[current];
        }
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
    }
    return hashtable;
  }

  public static void StripKeysWithNullValues(this IDictionary original)
  {
    object[] objArray = new object[original.Count];
    int num = 0;
    IEnumerator enumerator = original.Keys.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        objArray[num++] = current;
      }
    }
    finally
    {
      IDisposable disposable;
      if ((disposable = enumerator as IDisposable) != null)
        disposable.Dispose();
    }
    for (int index = 0; index < objArray.Length; ++index)
    {
      object key = objArray[index];
      if (original[key] == null)
        original.Remove(key);
    }
  }

  public static bool Contains(this int[] target, int nr)
  {
    if (target == null)
      return false;
    for (int index = 0; index < target.Length; ++index)
    {
      if (target[index] == nr)
        return true;
    }
    return false;
  }
}
