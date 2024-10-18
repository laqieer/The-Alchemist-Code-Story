// Decompiled with JetBrains decompiler
// Type: SRPG.ReqInspSkillEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqInspSkillEquip : WebAPI
  {
    public ReqInspSkillEquip(
      long artifact_iid,
      long inspiration_skil_iid,
      Network.ResponseCallback response)
    {
      this.name = "unit/job/artifact/inspirationskill/equip";
      this.body = WebAPI.GetRequestString<ReqInspSkillEquip.RequestParam>(new ReqInspSkillEquip.RequestParam()
      {
        artifact_iid = artifact_iid,
        inspiration_skil_iid = inspiration_skil_iid
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public long artifact_iid;
      public long inspiration_skil_iid;
    }

    [Serializable]
    public class Response
    {
      public Json_Artifact artifact;
    }
  }
}
