// Decompiled with JetBrains decompiler
// Type: MsgPack.ReflectionCacheEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MsgPack
{
  public class ReflectionCacheEntry
  {
    private const BindingFlags FieldBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField;

    public ReflectionCacheEntry(Type t)
    {
      FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField);
      IDictionary<string, FieldInfo> dictionary = (IDictionary<string, FieldInfo>) new Dictionary<string, FieldInfo>(fields.Length);
      for (int index1 = 0; index1 < fields.Length; ++index1)
      {
        FieldInfo fieldInfo = fields[index1];
        string index2 = fieldInfo.Name;
        int num;
        if (index2[0] == '<' && (num = index2.IndexOf('>')) > 1)
          index2 = index2.Substring(1, num - 1);
        dictionary[index2] = fieldInfo;
      }
      this.FieldMap = dictionary;
    }

    public IDictionary<string, FieldInfo> FieldMap { get; private set; }
  }
}
