// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_QuestPartyParam
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
  public class JSON_QuestPartyParam
  {
    public string iname;
    public int type_1;
    public int type_2;
    public int type_3;
    public int type_4;
    public int support_type;
    public int subtype_1;
    public int subtype_2;
    public string unit_1;
    public string unit_2;
    public string unit_3;
    public string unit_4;
    public string subunit_1;
    public string subunit_2;
    public int l_npc_rare;
  }
}
