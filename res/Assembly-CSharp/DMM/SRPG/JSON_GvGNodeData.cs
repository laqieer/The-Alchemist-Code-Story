﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GvGNodeData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_GvGNodeData
  {
    public int iid;
    public int id;
    public int state;
    public int gid;
    public int declared_gid;
    public long capture_time;
    public long attack_enable_time;
    public int defense_party_num;
  }
}