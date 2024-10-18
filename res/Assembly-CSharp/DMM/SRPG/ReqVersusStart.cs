// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
      public string btl_ver;
    }
  }
}
