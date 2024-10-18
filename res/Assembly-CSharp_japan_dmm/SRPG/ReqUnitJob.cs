// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitJob
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqUnitJob : WebAPI
  {
    public ReqUnitJob(long iid_unit, long iid_job, Network.ResponseCallback response)
    {
      this.Setup(iid_unit, iid_job, (string) null, response);
    }

    public ReqUnitJob(
      long iid_unit,
      long iid_job,
      string ptype,
      Network.ResponseCallback response)
    {
      this.Setup(iid_unit, iid_job, ptype, response);
    }

    private void Setup(
      long iid_unit,
      long iid_job,
      string ptype,
      Network.ResponseCallback response)
    {
      this.name = "unit/job/set";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\":");
      stringBuilder.Append(iid_unit);
      stringBuilder.Append(",\"iid_job\":");
      stringBuilder.Append(iid_job);
      if (!string.IsNullOrEmpty(ptype))
      {
        stringBuilder.Append(",\"type\":\"");
        stringBuilder.Append(ptype);
        stringBuilder.Append('"');
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
