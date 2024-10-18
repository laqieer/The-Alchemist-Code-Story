// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Job
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
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
  }
}
