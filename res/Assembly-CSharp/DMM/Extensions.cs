﻿// Decompiled with JetBrains decompiler
// Type: Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
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

  public static PhotonView GetPhotonView(this GameObject go) => go.GetComponent<PhotonView>();

  public static bool AlmostEquals(this Vector3 target, Vector3 second, float sqrMagnitudePrecision)
  {
    Vector3 vector3 = Vector3.op_Subtraction(target, second);
    return (double) ((Vector3) ref vector3).sqrMagnitude < (double) sqrMagnitudePrecision;
  }

  public static bool AlmostEquals(this Vector2 target, Vector2 second, float sqrMagnitudePrecision)
  {
    Vector2 vector2 = Vector2.op_Subtraction(target, second);
    return (double) ((Vector2) ref vector2).sqrMagnitude < (double) sqrMagnitudePrecision;
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
    foreach (object key in (IEnumerable) addHash.Keys)
      target[key] = addHash[key];
  }

  public static void MergeStringKeys(this IDictionary target, IDictionary addHash)
  {
    if (addHash == null || target.Equals((object) addHash))
      return;
    foreach (object key in (IEnumerable) addHash.Keys)
    {
      if (key is string)
        target[key] = addHash[key];
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
    Hashtable stringKeys = new Hashtable();
    if (original != null)
    {
      foreach (object key in (IEnumerable) original.Keys)
      {
        if (key is string)
          stringKeys[key] = original[key];
      }
    }
    return stringKeys;
  }

  public static void StripKeysWithNullValues(this IDictionary original)
  {
    object[] objArray = new object[original.Count];
    int num = 0;
    foreach (object key in (IEnumerable) original.Keys)
      objArray[num++] = key;
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
