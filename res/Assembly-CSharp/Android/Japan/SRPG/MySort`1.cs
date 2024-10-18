// Decompiled with JetBrains decompiler
// Type: SRPG.MySort`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class MySort<T>
  {
    public static void Sort(List<T> l, Comparison<T> c)
    {
      for (int index1 = 0; index1 < l.Count; ++index1)
      {
        for (int index2 = index1 + 1; index2 < l.Count; ++index2)
        {
          if (c(l[index1], l[index2]) > 0)
          {
            T obj = l[index1];
            l[index1] = l[index2];
            l[index2] = obj;
          }
        }
      }
    }
  }
}
