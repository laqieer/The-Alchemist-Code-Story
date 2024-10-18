// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterModeInfoParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class GenesisChapterModeInfoParam
  {
    private GenesisStarParam mStarParam;
    private int mLiberationQuestNo;
    private string mBossUnitId;
    private UnitParam mBossUnitParam;
    private int mBossHp;
    private string mBossChallengeItemId;
    private ItemParam mBossChallengeItemParam;
    private int mBossChallengeItemNum;
    private GenesisRewardParam mBossRewardParam;
    private int mModeUiIndex;

    public GenesisStarParam StarParam
    {
      get
      {
        return this.mStarParam;
      }
    }

    public int LiberationQuestNo
    {
      get
      {
        return this.mLiberationQuestNo;
      }
    }

    public UnitParam BossUnitParam
    {
      get
      {
        if (this.mBossUnitParam == null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if ((bool) ((UnityEngine.Object) instance))
            this.mBossUnitParam = instance.GetUnitParam(this.mBossUnitId);
        }
        return this.mBossUnitParam;
      }
    }

    public int BossHp
    {
      get
      {
        return this.mBossHp;
      }
    }

    public ItemParam BossChallengeItemParam
    {
      get
      {
        if (this.mBossChallengeItemParam == null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if ((bool) ((UnityEngine.Object) instance))
            this.mBossChallengeItemParam = instance.GetItemParam(this.mBossChallengeItemId);
        }
        return this.mBossChallengeItemParam;
      }
    }

    public int BossChallengeItemNum
    {
      get
      {
        return this.mBossChallengeItemNum;
      }
    }

    public GenesisRewardParam BossRewardParam
    {
      get
      {
        return this.mBossRewardParam;
      }
    }

    public int ModeUiIndex
    {
      get
      {
        return this.mModeUiIndex;
      }
    }

    public void Deserialize(JSON_GenesisChapterModeInfoParam json)
    {
      if (json == null)
        return;
      this.mStarParam = (GenesisStarParam) null;
      if (!string.IsNullOrEmpty(json.star_id))
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if ((bool) ((UnityEngine.Object) instance))
          this.mStarParam = instance.GetGenesisStarParam(json.star_id);
      }
      this.mLiberationQuestNo = json.liberation_qno;
      this.mBossUnitId = json.boss_unit_id;
      this.mBossHp = json.boss_hp;
      this.mBossChallengeItemId = json.boss_ch_item_id;
      this.mBossChallengeItemNum = json.boss_ch_item_num;
      this.mBossRewardParam = (GenesisRewardParam) null;
      if (!string.IsNullOrEmpty(json.boss_reward_id))
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if ((bool) ((UnityEngine.Object) instance))
          this.mBossRewardParam = instance.GetGenesisRewardParam(json.boss_reward_id);
      }
      this.mModeUiIndex = json.mode_ui_index;
    }
  }
}
