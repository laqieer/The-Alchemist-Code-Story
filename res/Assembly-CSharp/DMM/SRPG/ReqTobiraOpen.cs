// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTobiraOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqTobiraOpen : WebAPI
  {
    public ReqTobiraOpen(
      long unit_iid,
      TobiraParam.Category category,
      Network.ResponseCallback response)
    {
      this.name = "unit/door/open";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"unit_iid\":");
      stringBuilder.Append(unit_iid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"category\":");
      stringBuilder.Append((int) category);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
