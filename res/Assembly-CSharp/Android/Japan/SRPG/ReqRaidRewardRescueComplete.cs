// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRewardRescueComplete
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
      public Json_Gift[] reward;
      private int round;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
