// Decompiled with JetBrains decompiler
// Type: SRPG.Json_RuneData
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
  public class Json_RuneData
  {
    public long iid;
    public string iname;
    public int unit_id;
    public int slot;
    public int enforce;
    public int evo;
    public int favorite;
    public Json_RuneStateData state;

    [IgnoreMember]
    public bool IsEmptyDummyData => this.iid == 0L;

    [IgnoreMember]
    public bool IsFavorite => this.favorite != 0;
  }
}
