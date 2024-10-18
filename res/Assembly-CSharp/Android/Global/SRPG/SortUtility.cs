// Decompiled with JetBrains decompiler
// Type: SRPG.SortUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public static class SortUtility
  {
    public static void StableSort<T>(List<T> list, Comparison<T> comparison)
    {
      List<KeyValuePair<int, T>> keyValuePairList = new List<KeyValuePair<int, T>>(list.Count);
      for (int key = 0; key < list.Count; ++key)
        keyValuePairList.Add(new KeyValuePair<int, T>(key, list[key]));
      keyValuePairList.Sort((Comparison<KeyValuePair<int, T>>) ((x, y) =>
      {
        int num = comparison(x.Value, y.Value);
        if (num == 0)
          num = x.Key.CompareTo(y.Key);
        return num;
      }));
      for (int index = 0; index < list.Count; ++index)
        list[index] = keyValuePairList[index].Value;
    }
  }
}
