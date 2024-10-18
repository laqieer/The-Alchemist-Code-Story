// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_QuestCampaignTrust
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
  public class JSON_QuestCampaignTrust
  {
    public string children_iname;
    public string concept_card;
    public int card_trust_lottery_rate;
    public int card_trust_qe_bonus;
  }
}
