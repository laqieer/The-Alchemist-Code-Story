// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_QuestClearUnlockUnitDataParam
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
  public class JSON_QuestClearUnlockUnitDataParam
  {
    public string iname;
    public string uid;
    public int add;
    public int type;
    public string new_id;
    public string old_id;
    public string parent_id;
    public int ulv;
    public string aid;
    public int alv;
    public string[] qids;
    public int qcnd;
  }
}
