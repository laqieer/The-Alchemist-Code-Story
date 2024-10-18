// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusDraftDeck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
