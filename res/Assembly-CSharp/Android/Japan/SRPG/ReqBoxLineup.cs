// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBoxLineup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqBoxLineup : WebAPI
  {
    public ReqBoxLineup(string box_iname, Network.ResponseCallback response)
    {
      this.name = "box_lottery/lineup";
      this.body = WebAPI.GetRequestString<ReqBoxLineup.RequestParam>(new ReqBoxLineup.RequestParam()
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
      public int total_step;
      public JSON_BoxGachaSteps[] steps;
    }
  }
}
