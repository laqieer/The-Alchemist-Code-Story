// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusStart : WebAPI
  {
    public ReqVersusStart(Network.ResponseCallback response)
    {
      this.name = "vs/start";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    public class JSON_VersusMap
    {
      public string free;
      public string tower;
      public string friend;
    }

    public class Response
    {
      public string app_id;
      public ReqVersusStart.JSON_VersusMap maps;
    }
  }
}
