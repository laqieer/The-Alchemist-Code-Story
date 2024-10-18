// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetConceptCardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ReqSetConceptCardList : WebAPI
  {
    public ReqSetConceptCardList(
      Dictionary<long, long> req_set_list,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      List<KeyValuePair<long, long>> req_set_list1 = new List<KeyValuePair<long, long>>();
      foreach (KeyValuePair<long, long> reqSet in req_set_list)
        req_set_list1.Add(reqSet);
      this.Initialize(req_set_list1, response, serializeCompressMethod);
    }

    public ReqSetConceptCardList(
      List<KeyValuePair<long, long>> req_set_list,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.Initialize(req_set_list, response, serializeCompressMethod);
    }

    private void Initialize(
      List<KeyValuePair<long, long>> req_set_list,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/concept/set_list";
      ReqSetConceptCardList.RequestParam requestParam = new ReqSetConceptCardList.RequestParam();
      if (req_set_list != null && req_set_list.Count > 0)
      {
        requestParam.set_list = new ReqSetConceptCardList.RequestSetList[req_set_list.Count];
        int index = 0;
        foreach (KeyValuePair<long, long> reqSet in req_set_list)
        {
          requestParam.set_list[index] = new ReqSetConceptCardList.RequestSetList();
          requestParam.set_list[index].unit_iid = reqSet.Key;
          requestParam.set_list[index].concept_iid = reqSet.Value;
          ++index;
        }
      }
      this.body = WebAPI.GetRequestString<ReqSetConceptCardList.RequestParam>(requestParam);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestSetList
    {
      public long concept_iid;
      public long unit_iid;
    }

    [Serializable]
    public class RequestParam
    {
      public ReqSetConceptCardList.RequestSetList[] set_list;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_PlayerData player;
      public Json_Unit[] units;
    }
  }
}
