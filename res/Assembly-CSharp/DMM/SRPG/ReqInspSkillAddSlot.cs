// Decompiled with JetBrains decompiler
// Type: SRPG.ReqInspSkillAddSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqInspSkillAddSlot : WebAPI
  {
    public ReqInspSkillAddSlot(long artifact_iid, Network.ResponseCallback response)
    {
      this.name = "unit/job/artifact/inspirationskill/add_slot";
      this.body = WebAPI.GetRequestString<ReqInspSkillAddSlot.RequestParam>(new ReqInspSkillAddSlot.RequestParam()
      {
        artifact_iid = artifact_iid
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public long artifact_iid;
    }

    [Serializable]
    public class Response
    {
      public Json_Artifact artifact;
      public Json_PlayerData player;
    }
  }
}
