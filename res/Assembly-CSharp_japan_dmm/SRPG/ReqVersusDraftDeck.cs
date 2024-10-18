// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusDraftDeck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqVersusDraftDeck : WebAPI
  {
    public ReqVersusDraftDeck(string token, long deck_id, Network.ResponseCallback response)
    {
      this.name = "vs/draft/deck";
      this.body = WebAPI.GetRequestString<ReqVersusDraftDeck.RequestParam>(new ReqVersusDraftDeck.RequestParam()
      {
        token = token,
        deck_id = deck_id
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string token;
      public long deck_id;
    }
  }
}
