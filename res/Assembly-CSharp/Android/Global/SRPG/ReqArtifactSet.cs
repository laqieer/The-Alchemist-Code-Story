// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqArtifactSet : WebAPI
  {
    public ReqArtifactSet(long iid_job, long iid_artifact, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid_job\":");
      stringBuilder.Append(iid_job);
      stringBuilder.Append(",\"iid_artifact\":");
      stringBuilder.Append(iid_artifact);
      this.name = "unit/job/artifact/set";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public ReqArtifactSet(long iid_job, long[] iid_artifact, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid_job\":");
      stringBuilder.Append(iid_job);
      stringBuilder.Append(",\"iid_artifact\":");
      stringBuilder.Append(iid_artifact[0]);
      stringBuilder.Append(",\"iid_artifacts\":[");
      for (int index = 0; index < iid_artifact.Length; ++index)
      {
        if (index > 0)
          stringBuilder.Append(',');
        stringBuilder.Append(iid_artifact[index]);
      }
      stringBuilder.Append(']');
      this.name = "unit/job/artifact/set";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public ReqArtifactSet(long iid_unit, long iid_job, long[] iid_artifact, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid_unit\":");
      stringBuilder.Append(iid_unit);
      stringBuilder.Append(",\"iid_job\":");
      stringBuilder.Append(iid_job);
      stringBuilder.Append(",\"iid_artifacts\":[");
      for (int index = 0; index < iid_artifact.Length; ++index)
      {
        if (index > 0)
          stringBuilder.Append(',');
        stringBuilder.Append(iid_artifact[index]);
      }
      stringBuilder.Append(']');
      this.name = "unit/job/artifact/set";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
