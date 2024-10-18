// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCheckVersion2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqCheckVersion2 : WebAPI
  {
    public ReqCheckVersion2(string ver, Network.ResponseCallback response)
    {
      this.name = "chkver2";
      this.body = "{";
      ReqCheckVersion2 reqCheckVersion2 = this;
      reqCheckVersion2.body = reqCheckVersion2.body + "\"ver\":\"" + ver + "\"";
      this.body += "}";
      this.callback = response;
    }
  }
}
