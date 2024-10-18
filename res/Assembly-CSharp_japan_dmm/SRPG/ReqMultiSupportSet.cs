// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiSupportSet
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
  public class ReqMultiSupportSet : WebAPI
  {
    public ReqMultiSupportSet(
      int roomId,
      List<string> selectedUIDs,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "btl/multi/support/set";
      this.body = WebAPI.GetRequestString<ReqMultiSupportSet.RequestParam>(new ReqMultiSupportSet.RequestParam()
      {
        roomid = roomId,
        support_uids = selectedUIDs.ToArray()
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int roomid;
      public string[] support_uids;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
    }
  }
}
