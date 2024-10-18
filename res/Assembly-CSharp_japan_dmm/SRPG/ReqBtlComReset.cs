// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqBtlComReset : WebAPI
  {
    public ReqBtlComReset(
      string iname,
      eResetCostType cost_type,
      Network.ResponseCallback response)
    {
      this.name = "btl/com/reset";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"cost_type\":");
      stringBuilder.Append((int) cost_type);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
