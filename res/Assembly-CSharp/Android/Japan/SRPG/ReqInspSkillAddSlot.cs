// Decompiled with JetBrains decompiler
// Type: SRPG.ReqInspSkillAddSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
