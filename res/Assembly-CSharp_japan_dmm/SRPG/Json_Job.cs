// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Job
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
  public class Json_Job
  {
    public long iid;
    public string iname;
    public int rank;
    public string cur_skin;
    public Json_Equip[] equips;
    public Json_Ability[] abils;
    public Json_Artifact[] artis;
    public Json_JobSelectable select;
    public string unit_image;
  }
}
