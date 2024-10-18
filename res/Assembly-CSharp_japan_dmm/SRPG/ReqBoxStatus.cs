// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBoxStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqBoxStatus : WebAPI
  {
    public ReqBoxStatus(string box_iname, Network.ResponseCallback response)
    {
      this.name = "box_lottery";
      this.body = WebAPI.GetRequestString<ReqBoxStatus.RequestParam>(new ReqBoxStatus.RequestParam()
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
      public int is_reset_enable;
      public JSON_BoxGachaCost cost;
    }
  }
}
