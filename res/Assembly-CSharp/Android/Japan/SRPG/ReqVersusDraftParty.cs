﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusDraftParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

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
