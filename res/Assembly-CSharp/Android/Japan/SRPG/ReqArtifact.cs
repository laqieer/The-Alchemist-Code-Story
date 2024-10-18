// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqArtifact : WebAPI
  {
    public ReqArtifact(long last_artifact_iid, Network.ResponseCallback response)
    {
      this.name = "unit/job/artifact";
      this.body = WebAPI.GetRequestString<ReqArtifact.RequestParam>(new ReqArtifact.RequestParam()
      {
        last_iid = last_artifact_iid
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public long last_iid;
    }

    [Serializable]
    public class Response
    {
      public Json_Artifact[] artifacts;
    }
  }
}
