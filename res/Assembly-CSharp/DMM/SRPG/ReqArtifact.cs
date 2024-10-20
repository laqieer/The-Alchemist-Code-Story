﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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