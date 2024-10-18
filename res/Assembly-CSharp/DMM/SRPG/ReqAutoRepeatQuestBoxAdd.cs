// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAutoRepeatQuestBoxAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqAutoRepeatQuestBoxAdd : WebAPI
  {
    public ReqAutoRepeatQuestBoxAdd(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "btl/auto_repeat/box/add";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_PlayerData player;
      public int box_extension_count;
      public int lap_num;
      public int is_push;
      public int box_expansion_purchase_count;
    }
  }
}
