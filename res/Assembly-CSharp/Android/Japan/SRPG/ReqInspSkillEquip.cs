// Decompiled with JetBrains decompiler
// Type: SRPG.ReqInspSkillEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqInspSkillEquip : WebAPI
  {
    public ReqInspSkillEquip(long artifact_iid, long inspiration_skil_iid, Network.ResponseCallback response)
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
