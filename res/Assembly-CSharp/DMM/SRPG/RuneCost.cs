// Decompiled with JetBrains decompiler
// Type: SRPG.RuneCost
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneCost
  {
    public string iname;
    public string use_item;
    public int use_zeny;
    public int use_num;
    public int use_coin;

    public bool Deserialize(JSON_RuneCost json)
    {
      this.iname = json.iname;
      this.use_item = json.use_item;
      this.use_zeny = json.use_gold;
      this.use_num = json.use_item_num;
      this.use_coin = json.use_coin;
      return true;
    }

    public bool IsPlayerAmountEnough()
    {
      return (string.IsNullOrEmpty(this.use_item) || MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.use_item) >= this.use_num) && MonoSingleton<GameManager>.Instance.Player.Gold >= this.use_zeny && MonoSingleton<GameManager>.Instance.Player.Coin >= this.use_coin;
    }
  }
}
