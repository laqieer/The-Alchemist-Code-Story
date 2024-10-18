// Decompiled with JetBrains decompiler
// Type: SRPG.RaidResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidResult
  {
    public QuestParam quest;
    public int pexp;
    public int uexp;
    public int gold;
    public List<UnitData> members;
    public List<RaidQuestResult> results = new List<RaidQuestResult>(10);
    public QuestParam[] chquest = new QuestParam[0];
    public string[] campaignIds;
    public List<RuneData> runes_detail = new List<RuneData>();

    public RaidResult(PlayerPartyTypes type)
    {
      this.members = new List<UnitData>(MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(type).MAX_UNIT);
    }
  }
}
