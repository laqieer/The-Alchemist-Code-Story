// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRuneStorageAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqRuneStorageAdd : WebAPI
  {
    public ReqRuneStorageAdd(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "unit/rune/storage/add";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public int rune_storage;
      public Json_PlayerData player;
    }
  }
}
