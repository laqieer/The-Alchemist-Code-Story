// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceEventModeInfoParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AdvanceEventModeInfoParam
  {
    private AdvanceStarParam mStarParam;
    private int mLiberationQuestNo;
    private string mBossUnitId;
    private UnitParam mBossUnitParam;
    private int mBossHp;
    private string mBossChallengeItemId;
    private ItemParam mBossChallengeItemParam;
    private int mBossChallengeItemNum;
    private AdvanceRewardParam mBossRewardParam;
    private int mModeUiIndex;
    private string mLapBossId;
    private AdvanceLapBossParam mLapBossParam;

    public AdvanceStarParam StarParam => this.mStarParam;

    public int LiberationQuestNo => this.mLiberationQuestNo;

    public UnitParam BossUnitParam
    {
      get
      {
        if (this.mBossUnitParam == null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (Object.op_Implicit((Object) instance))
            this.mBossUnitParam = instance.GetUnitParam(this.mBossUnitId);
        }
        return this.mBossUnitParam;
      }
    }

    public int BossHp => this.mBossHp;

    public ItemParam BossChallengeItemParam
    {
      get
      {
        if (this.mBossChallengeItemParam == null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (Object.op_Implicit((Object) instance))
            this.mBossChallengeItemParam = instance.GetItemParam(this.mBossChallengeItemId);
        }
        return this.mBossChallengeItemParam;
      }
    }

    public int BossChallengeItemNum => this.mBossChallengeItemNum;

    public AdvanceRewardParam BossRewardParam => this.mBossRewardParam;

    public int ModeUiIndex => this.mModeUiIndex;

    public bool IsLapBoss => !string.IsNullOrEmpty(this.mLapBossId);

    public AdvanceLapBossParam LapBossParam
    {
      get
      {
        if (!this.IsLapBoss)
          return (AdvanceLapBossParam) null;
        if (this.mLapBossParam == null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (Object.op_Implicit((Object) instance))
            this.mLapBossParam = instance.GetAdvanceLapBossParam(this.mLapBossId);
        }
        return this.mLapBossParam;
      }
    }

    public void Deserialize(JSON_AdvanceEventModeInfoParam json)
    {
      if (json == null)
        return;
      this.mStarParam = (AdvanceStarParam) null;
      if (!string.IsNullOrEmpty(json.star_id))
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (Object.op_Implicit((Object) instance))
          this.mStarParam = instance.GetAdvanceStarParam(json.star_id);
      }
      this.mLiberationQuestNo = json.liberation_qno;
      this.mBossUnitId = json.boss_unit_id;
      this.mBossHp = json.boss_hp;
      this.mBossChallengeItemId = json.boss_ch_item_id;
      this.mBossChallengeItemNum = json.boss_ch_item_num;
      this.mBossRewardParam = (AdvanceRewardParam) null;
      if (!string.IsNullOrEmpty(json.boss_reward_id))
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (Object.op_Implicit((Object) instance))
          this.mBossRewardParam = instance.GetAdvanceRewardParam(json.boss_reward_id);
      }
      this.mModeUiIndex = json.mode_ui_index;
      this.mLapBossId = json.lap_boss_id;
    }
  }
}
