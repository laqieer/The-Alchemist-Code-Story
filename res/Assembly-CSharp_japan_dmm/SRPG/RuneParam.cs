// Decompiled with JetBrains decompiler
// Type: SRPG.RuneParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneParam
  {
    public string iname;
    public string item_iname;
    public RuneSlotIndex slot_index;
    public int seteff_type;

    [IgnoreMember]
    public int SetEffTypeIconIndex => Mathf.Max(0, this.seteff_type - 1);

    public bool Deserialize(JSON_RuneParam json)
    {
      this.iname = json.iname;
      this.item_iname = json.item_iname;
      this.slot_index = (RuneSlotIndex) (byte) (json.slot - 1);
      this.seteff_type = json.seteff_type;
      return true;
    }

    public ItemParam ItemParam => MonoSingleton<GameManager>.Instance.GetItemParam(this.item_iname);

    public RuneSetEff RuneSetEff
    {
      get => MonoSingleton<GameManager>.Instance.MasterParam.GetRuneSetEff(this.seteff_type);
    }
  }
}
