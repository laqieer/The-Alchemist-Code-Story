// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBoxDrawStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqBoxDrawStatus : WebAPI
  {
    public ReqBoxDrawStatus(string box_iname, Network.ResponseCallback response)
    {
      this.name = "box_lottery/draw_status";
      this.body = WebAPI.GetRequestString<ReqBoxDrawStatus.RequestParam>(new ReqBoxDrawStatus.RequestParam()
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
      public int total_step;
      public int total_num;
      public int remain_num;
      public int is_reset_enable;
      public int total_num_feature;
      public int total_num_normal;
      public int remain_num_feature;
      public int remain_num_normal;
      public JSON_BoxGachaItems[] box_items;
    }
  }
}
