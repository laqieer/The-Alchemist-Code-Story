// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGachaRate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqGachaRate : WebAPI
  {
    public ReqGachaRate(string gachaid, Network.ResponseCallback response)
    {
      this.name = "gacha/slot_info";
      this.body = WebAPI.GetRequestString("\"gachaid\":\"" + gachaid + "\"");
      this.callback = response;
    }
  }
}
