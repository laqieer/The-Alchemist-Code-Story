// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitAwake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqUnitAwake : WebAPI
  {
    public ReqUnitAwake(
      long unitUniqueId,
      int target_plus,
      int pieceCountUnit,
      int pieceCountElement,
      int pieceCountCommon,
      Network.ResponseCallback response,
      string trophyprog = null,
      string bingoprog = null)
    {
      this.name = "unit/plus/add2";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\":" + (object) unitUniqueId);
      stringBuilder.Append(",\"target_plus\":" + (object) target_plus);
      stringBuilder.Append(",\"unit\":" + (object) pieceCountUnit);
      stringBuilder.Append(",\"element\":" + (object) pieceCountElement);
      stringBuilder.Append(",\"common\":" + (object) pieceCountCommon);
      if (!string.IsNullOrEmpty(trophyprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(trophyprog);
      }
      if (!string.IsNullOrEmpty(bingoprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
