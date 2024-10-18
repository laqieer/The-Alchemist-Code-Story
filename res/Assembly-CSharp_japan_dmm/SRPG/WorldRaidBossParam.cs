// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBossParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class WorldRaidBossParam
  {
    private string mIname;
    private string mName;
    private long mHp;
    private string mUnitId;
    private UnitParam mUnitParam;
    private string mQuestId;
    private QuestParam mQuestParam;
    private string mDetailUrl;
    private string mDetailTitle;

    public string Iname => this.mIname;

    public string Name => this.mName;

    public long Hp => this.mHp;

    public string UnitId => this.mUnitId;

    public UnitParam UnitParam
    {
      get
      {
        if (this.mUnitParam == null && !string.IsNullOrEmpty(this.mUnitId))
          this.mUnitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(this.mUnitId);
        return this.mUnitParam;
      }
    }

    public string QuestId => this.mQuestId;

    public QuestParam QuestParam
    {
      get
      {
        if (this.mQuestParam == null && !string.IsNullOrEmpty(this.mQuestId))
          this.mQuestParam = MonoSingleton<GameManager>.Instance.FindQuest(this.mQuestId);
        return this.mQuestParam;
      }
    }

    public string DetailUrl => this.mDetailUrl;

    public string DetailTitle => this.mDetailTitle;

    public void Deserialize(JSON_WorldRaidBossParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mHp = json.hp;
      this.mUnitId = json.unit_id;
      this.mQuestId = json.quest_id;
      this.mDetailUrl = json.detail_url;
      this.mDetailTitle = json.detail_title;
    }

    public static void Deserialize(
      ref List<WorldRaidBossParam> list,
      JSON_WorldRaidBossParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<WorldRaidBossParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        WorldRaidBossParam worldRaidBossParam = new WorldRaidBossParam();
        worldRaidBossParam.Deserialize(json[index]);
        list.Add(worldRaidBossParam);
      }
    }

    public static WorldRaidBossParam GetParam(string iname)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      return !UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.WorldRaidBossParamList == null ? (WorldRaidBossParam) null : instance.WorldRaidBossParamList.Find((Predicate<WorldRaidBossParam>) (p => p.Iname == iname));
    }
  }
}
