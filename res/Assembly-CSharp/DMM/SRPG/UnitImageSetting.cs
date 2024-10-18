// Decompiled with JetBrains decompiler
// Type: SRPG.UnitImageSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class UnitImageSetting
  {
    [SerializeField]
    private System.Collections.Generic.List<UnitImageSetting.KeyValue> List;
    private Dictionary<string, UnitImageSetting.Vector2AndFloat> mTable;

    public Dictionary<string, UnitImageSetting.Vector2AndFloat> GetTable()
    {
      if (this.mTable == null)
        this.mTable = this.ConvertListToDictionary(this.List);
      return this.mTable;
    }

    private Dictionary<string, UnitImageSetting.Vector2AndFloat> ConvertListToDictionary(
      System.Collections.Generic.List<UnitImageSetting.KeyValue> list)
    {
      Dictionary<string, UnitImageSetting.Vector2AndFloat> dictionary = new Dictionary<string, UnitImageSetting.Vector2AndFloat>();
      foreach (UnitImageSetting.KeyValue keyValue in list)
        dictionary.Add(keyValue.Key, keyValue.Value);
      return dictionary;
    }

    [Serializable]
    public class KeyValue
    {
      public string Key;
      public UnitImageSetting.Vector2AndFloat Value;
    }

    [Serializable]
    public class Vector2AndFloat
    {
      public Vector2 Offset;
      public float Scale;
    }
  }
}
