// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAutoRepeatQuestSetApItemPriority
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqAutoRepeatQuestSetApItemPriority : WebAPI
  {
    public ReqAutoRepeatQuestSetApItemPriority(
      string[] ap_items,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "btl/auto_repeat/drip/set";
      this.body = WebAPI.GetRequestString<ReqAutoRepeatQuestSetApItemPriority.RequestParam>(new ReqAutoRepeatQuestSetApItemPriority.RequestParam()
      {
        drip_priority = ap_items
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string[] drip_priority;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public string[] drip_priority;
    }
  }
}
