﻿// Decompiled with JetBrains decompiler
// Type: DebugUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

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

  public static void Assert(string msg)
  {
    throw new Exception("[Assertion Failed] " + msg);
  }

  public static void Verify(object target, System.Type t)
  {
  }
}
