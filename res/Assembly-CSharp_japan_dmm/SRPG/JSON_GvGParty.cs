// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GvGParty
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
  public class JSON_GvGParty
  {
    public int id;
    public int win_num;
    public int beat_num;
    public int is_npc;
    public int role;
    public string name;
    public JSON_GvGPartyUnit[] units;
    public JSON_GvGPartyNPC[] npc_units;
  }
}
