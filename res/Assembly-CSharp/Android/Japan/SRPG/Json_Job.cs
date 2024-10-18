// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Job
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
    public string unit_image;
  }
}
