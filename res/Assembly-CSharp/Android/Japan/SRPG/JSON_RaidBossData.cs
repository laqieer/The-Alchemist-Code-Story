// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidBossData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_RaidBossData
  {
    public string uid;
    public string name;
    public int area_id;
    public JSON_RaidBossInfo boss_info;
    public int sos_status;
    public JSON_RaidSOSMember[] sos_member;
  }
}
