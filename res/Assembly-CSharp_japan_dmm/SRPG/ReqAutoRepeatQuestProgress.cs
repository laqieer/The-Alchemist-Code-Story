// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAutoRepeatQuestProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqAutoRepeatQuestProgress : WebAPI
  {
    public ReqAutoRepeatQuestProgress(
      int current_lap,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "btl/auto_repeat/progress";
      this.body = WebAPI.GetRequestString<ReqAutoRepeatQuestProgress.RequestParam>(new ReqAutoRepeatQuestProgress.RequestParam()
      {
        current_lap_num = current_lap
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int current_lap_num;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_AutoRepeatQuestData auto_repeat;
      public int box_extension_count;
      public int box_expansion_purchase_count;
    }
  }
}
