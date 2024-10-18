// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RaidBossData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
