// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusDraftSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ReqVersusDraftSelect : WebAPI
  {
    public ReqVersusDraftSelect(string token, Network.ResponseCallback response)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.name = "vs/draft/select";
      List<ReqVersusDraftSelect.RequestDraftUnit> requestDraftUnitList = new List<ReqVersusDraftSelect.RequestDraftUnit>();
      List<VersusDraftUnitParam> versusDraftUnits = instance.GetVersusDraftUnits(instance.VSDraftId);
      for (int i = 0; i < VersusDraftList.VersusDraftUnitDataListPlayer.Count; ++i)
      {
        VersusDraftUnitParam versusDraftUnitParam = versusDraftUnits.Find((Predicate<VersusDraftUnitParam>) (vdup => vdup.DraftUnitId == VersusDraftList.VersusDraftUnitDataListPlayer[i].UniqueID));
        requestDraftUnitList.Add(new ReqVersusDraftSelect.RequestDraftUnit()
        {
          id = versusDraftUnitParam.DraftUnitId,
          secret = !versusDraftUnitParam.IsSecret ? 0 : 1
        });
      }
      ReqVersusDraftSelect.RequestParam requestParam = new ReqVersusDraftSelect.RequestParam()
      {
        token = token,
        draft_result = new ReqVersusDraftSelect.RequestDraftResult()
      };
      requestParam.draft_result.turn_own = !VersusDraftList.VersusDraftTurnOwn ? 0 : 1;
      requestParam.draft_result.draft_units = requestDraftUnitList.ToArray();
      this.body = WebAPI.GetRequestString<ReqVersusDraftSelect.RequestParam>(requestParam);
      this.callback = response;
    }

    [Serializable]
    public class RequestDraftUnit
    {
      public long id;
      public int secret;
    }

    [Serializable]
    public class RequestDraftResult
    {
      public int turn_own;
      public ReqVersusDraftSelect.RequestDraftUnit[] draft_units;
    }

    [Serializable]
    public class RequestParam
    {
      public string token;
      public ReqVersusDraftSelect.RequestDraftResult draft_result;
    }

    public class Response
    {
      public int draft_id;
    }
  }
}
