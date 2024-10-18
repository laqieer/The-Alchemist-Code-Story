// Decompiled with JetBrains decompiler
// Type: UnityComponentExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

internal static class UnityComponentExtensions
{
  public static List<T> GetComponentsAsANewList<T>(this GameObject obj)
  {
    List<T> objList = new List<T>();
    obj.GetComponents<T>(objList);
    return objList;
  }

  public static void RunActionsOnComponents<T>(this GameObject obj, Action<T> actionToBeRun)
  {
    T[] components = obj.GetComponents<T>();
    int index = 0;
    for (int length = components.Length; index < length; ++index)
    {
      T obj1 = components[index];
      actionToBeRun(obj1);
    }
  }
}
