// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGenesisRaidboss
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqGenesisRaidboss : WebAPI
  {
    public ReqGenesisRaidboss(string area_id, QuestDifficulties difficulty, Network.ResponseCallback response)
    {
      this.name = "genesis/raidboss";
      this.body = WebAPI.GetRequestString<ReqGenesisRaidboss.RequestParam>(new ReqGenesisRaidboss.RequestParam()
      {
        area_id = area_id,
        difficulty = (int) difficulty
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string area_id;
      public int difficulty;
    }

    [Serializable]
    public class Response
    {
      public int hp;
    }
  }
}
