// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqVersusDraft : WebAPI
  {
    public ReqVersusDraft(string token, Network.ResponseCallback response)
    {
      this.name = "vs/draft";
      this.body = WebAPI.GetRequestString<ReqVersusDraft.RequestParam>(new ReqVersusDraft.RequestParam()
      {
        token = token
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string token;
    }

    public class ResponseUnit
    {
      public long id;
      public int secret;
    }

    public class Response
    {
      public int turn_own;
      public ReqVersusDraft.ResponseUnit[] draft_units;
    }
  }
}
