// Decompiled with JetBrains decompiler
// Type: SRPG.ReqDrawCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqDrawCard : WebAPI
  {
    public ReqDrawCard(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "drawcard";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class CardInfo
    {
      public int is_miss;
      public ReqDrawCard.CardInfo.Card draw_card_reward;

      public CardInfo()
      {
      }

      public CardInfo(int type, string iname, int num, int miss)
      {
        this.draw_card_reward = new ReqDrawCard.CardInfo.Card(type, iname, num);
        this.is_miss = miss;
      }

      [MessagePackObject(true)]
      [Serializable]
      public class Card
      {
        public int item_type;
        public string item_iname;
        public int item_num;

        public Card()
        {
        }

        public Card(int type, string iname, int num)
        {
          this.item_type = type;
          this.item_iname = iname;
          this.item_num = num;
        }
      }
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public ReqDrawCard.Response.Status drawcard_current_status;
      public ReqDrawCard.CardInfo.Card[] rewards;

      [MessagePackObject(true)]
      [Serializable]
      public class Status
      {
        public int step;
        public int is_finish;
        public ReqDrawCard.CardInfo[] draw_infos;
      }
    }
  }
}
