// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGachaExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqGachaExec : WebAPI
  {
    public ReqGachaExec(string gachaid, Network.ResponseCallback response)
    {
      this.name = "gacha/exec";
      this.body = WebAPI.GetRequestString("\"gachaid\":\"" + gachaid + "\"");
      this.callback = response;
    }

    public ReqGachaExec(
      string iname,
      Network.ResponseCallback response,
      int free = 0,
      int num = 0,
      int is_decision = 0)
    {
      this.name = "gacha/exec";
      this.body = "\"gachaid\":\"" + iname + "\",";
      ReqGachaExec reqGachaExec1 = this;
      reqGachaExec1.body = reqGachaExec1.body + "\"free\":" + free.ToString();
      if (num > 0)
      {
        ReqGachaExec reqGachaExec2 = this;
        reqGachaExec2.body = reqGachaExec2.body + ",\"ticketnum\":" + num.ToString();
      }
      if (is_decision != 0)
      {
        ReqGachaExec reqGachaExec3 = this;
        reqGachaExec3.body = reqGachaExec3.body + ",\"is_decision\":" + is_decision.ToString();
      }
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
