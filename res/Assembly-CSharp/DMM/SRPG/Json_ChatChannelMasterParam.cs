﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ChatChannelMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class Json_ChatChannelMasterParam
  {
    public int pk;
    public Json_ChatChannelMasterParam.Fields fields;

    [MessagePackObject(true)]
    public class Fields
    {
      public int id;
      public byte category_id;
      public string name;
    }
  }
}