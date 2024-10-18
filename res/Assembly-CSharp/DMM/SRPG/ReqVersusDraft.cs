// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
