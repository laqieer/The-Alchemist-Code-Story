// Decompiled with JetBrains decompiler
// Type: DebugUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
    Debug.LogError((object) ("[Error]" + msg));
  }

  public static void LogWarning(string msg)
  {
    if (!GameUtility.IsDebugBuild)
      return;
    Debug.LogWarning((object) ("[Warning]" + msg));
  }

  public static void LogException(Exception e)
  {
    if (!GameUtility.IsDebugBuild)
      return;
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
