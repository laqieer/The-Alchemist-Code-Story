// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBoxReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqBoxReset : WebAPI
  {
    public ReqBoxReset(string box_iname, Network.ResponseCallback response)
    {
      this.name = "box_lottery/next_step";
      this.body = WebAPI.GetRequestString<ReqBoxReset.RequestParam>(new ReqBoxReset.RequestParam()
      {
        box_iname = box_iname
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string box_iname;
    }

    [Serializable]
    public class Response
    {
      public string box_iname;
      public int step;
      public int total_num;
      public int remain_num;
    }
  }
}
