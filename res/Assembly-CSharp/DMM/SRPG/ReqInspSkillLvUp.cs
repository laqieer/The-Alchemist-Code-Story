// Decompiled with JetBrains decompiler
// Type: SRPG.ReqInspSkillLvUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqInspSkillLvUp : WebAPI
  {
    public ReqInspSkillLvUp(
      long artifact_iid,
      long inspiration_skil_iid,
      long[] mix_iids,
      Network.ResponseCallback response)
    {
      this.name = "unit/job/artifact/inspirationskill/lvup";
      this.body = WebAPI.GetRequestString<ReqInspSkillLvUp.RequestParam>(new ReqInspSkillLvUp.RequestParam()
      {
        artifact_iid = artifact_iid,
        inspiration_skil_iid = inspiration_skil_iid,
        mix_iids = mix_iids
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public long artifact_iid;
      public long inspiration_skil_iid;
      public long[] mix_iids;
    }

    [Serializable]
    public class Response
    {
      public Json_InspirationSkill inspiration_skill;
      public Json_PlayerData player;
      public long[] mix_iids;
    }
  }
}
