// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_QuestCampaignChildParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_QuestCampaignChildParam
  {
    public string iname;
    public int scope;
    public int quest_type;
    public int quest_mode;
    public string quest_id;
    public string unit;
    public int drop_rate;
    public int drop_num;
    public int exp_player;
    public int exp_unit;
    public int ap_rate;
    public int rental_fav_rate;
  }
}
