// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRewardRescueComplete
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaidRewardRescueComplete : WebAPI
  {
    public ReqRaidRewardRescueComplete(Network.ResponseCallback response)
    {
      this.name = "raidboss/reward/rescue_complete";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public JSON_ConceptCard[] cards;
      public Json_Artifact[] artifacts;
      public Json_Gift[] reward;
      private int round;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
