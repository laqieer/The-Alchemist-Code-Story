// Decompiled with JetBrains decompiler
// Type: SRPG.ReqDrawCardExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqDrawCardExec : WebAPI
  {
    public ReqDrawCardExec(
      int select_card_index,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "drawcard/exec";
      this.body = WebAPI.GetRequestString<ReqDrawCardExec.RequestParam>(new ReqDrawCardExec.RequestParam()
      {
        select_card_index = select_card_index
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int select_card_index;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public ReqDrawCard.CardInfo draw_info;
      public ReqDrawCard.Response.Status drawcard_current_status;
      public ReqDrawCard.CardInfo.Card[] rewards;
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public JSON_ConceptCard[] cards;
      public Json_Artifact[] artifacts;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
