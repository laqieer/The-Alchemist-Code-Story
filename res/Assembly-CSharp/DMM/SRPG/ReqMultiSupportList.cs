// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiSupportList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace SRPG
{
  public class ReqMultiSupportList : WebAPI
  {
    public ReqMultiSupportList(
      int roomId,
      bool isUpdate,
      List<string> selectedFUIDs,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "btl/multi/supportlist";
      this.body = WebAPI.GetRequestString<ReqMultiSupportList.RequestParam>(new ReqMultiSupportList.RequestParam()
      {
        roomid = roomId,
        is_update = !isUpdate ? 0 : 1,
        helps = selectedFUIDs.Select<string, ReqMultiSupportList.RequestParamHelp>((Func<string, ReqMultiSupportList.RequestParamHelp>) (support => new ReqMultiSupportList.RequestParamHelp()
        {
          fuid = support
        })).ToArray<ReqMultiSupportList.RequestParamHelp>()
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int roomid;
      public int is_update;
      public ReqMultiSupportList.RequestParamHelp[] helps;
    }

    [Serializable]
    public class RequestParamHelp
    {
      public string fuid;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_MultiSupport[] supports;
    }
  }
}
