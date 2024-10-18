// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFirstChargeBonus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqFirstChargeBonus : WebAPI
  {
    public ReqFirstChargeBonus(Network.ResponseCallback _response)
    {
      this.name = "charge/bonus/exec";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = _response;
    }
  }
}
