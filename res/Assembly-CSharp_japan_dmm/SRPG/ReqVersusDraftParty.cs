// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusDraftParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ReqVersusDraftParty : WebAPI
  {
    public ReqVersusDraftParty(string token, int draft_id, Network.ResponseCallback response)
    {
      this.name = "vs/draft/party";
      ReqVersusDraftParty.RequestParam requestParam = new ReqVersusDraftParty.RequestParam();
      requestParam.token = token;
      requestParam.draft_id = draft_id;
      List<int> intList = new List<int>();
      for (int i = 0; i < VersusDraftList.VersusDraftPartyUnits.Count; ++i)
      {
        int index = VersusDraftList.VersusDraftUnitDataListPlayer.FindIndex((Predicate<UnitData>) (ud => ud.UniqueID == VersusDraftList.VersusDraftPartyUnits[i].UniqueID));
        intList.Add(index);
      }
      requestParam.party_indexes = intList.ToArray();
      this.body = WebAPI.GetRequestString<ReqVersusDraftParty.RequestParam>(requestParam);
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string token;
      public int draft_id;
      public int[] party_indexes;
    }
  }
}
