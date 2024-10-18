﻿// Decompiled with JetBrains decompiler
// Type: DebugUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
public static class DebugUtility
{
  public static void Log(string msg)
  {
    if (!GameUtility.IsDebugBuild)
      return;
    Debug.Log((object) msg);
  }

  public static void LogError(string msg)
  {
    if (!GameUtility.IsDebugBuild)
      return;
    DebugUtility.Log("[Error]" + msg);
    Debug.LogError((object) ("[Error]" + msg));
  }

  public static void LogWarning(string msg)
  {
    if (!GameUtility.IsDebugBuild)
      return;
    DebugUtility.Log("[Warning]" + msg);
    Debug.LogWarning((object) ("[Warning]" + msg));
  }

  public static void LogException(Exception e)
  {
    if (!GameUtility.IsDebugBuild)
      return;
    DebugUtility.Log("[Exception] " + e.Message + "\n-------------------------------\n" + e.StackTrace);
    Debug.LogException(e);
  }

  public static void Assert(bool cond, string msg)
  {
    if (cond)
      return;
    DebugUtility.Assert(msg);
  }

  public static void Assert(string msg) => throw new Exception("[Assertion Failed] " + msg);

  public static void Verify(object target, System.Type t)
  {
  }
}
