﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerPlayerInfo : MonoBehaviour
  {
    private string NotSocreText = "-";
    [HeaderBar("▼現在のスコア")]
    public Text NowTurn;
    public Text NowDied;
    public Text NowRetire;
    public Text NowRecover;
    public Text NowChallenge;
    public Text NowLose;
    public Text NowReset;
    [HeaderBar("▼ベストスコア")]
    public Text BestTurn;
    public Text BestDied;
    public Text BestRetire;
    public Text BestRecover;
    public Text BestChallenge;
    public Text BestLose;
    public Text BestReset;
    [HeaderBar("▼ランキングの順位")]
    public Text SpdRank;
    public Text TecRank;
    [HeaderBar("▼スコアのアートフォント")]
    public GameObject TurnScore;
    public GameObject DiedScore;
    public GameObject RetireScore;
    public GameObject RecoverScore;
    public GameObject ChallengeScore;
    public GameObject LoseScore;
    public GameObject ResetScore;
    public GameObject TotalScore;
    public GameObject PlusObj;
    public GameObject MinusObj;
    [HeaderBar("▼ユニット表示用オブジェクトの親")]
    public GameObject Leader;
    private SpriteSheet Icons;

    private void Start()
    {
      this.Icons = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
      this.RefreshData();
    }

    private void RefreshData()
    {
      this.UpdateNowScore();
      this.UpdateBestScore();
      this.UpdateTotalScore();
      if ((UnityEngine.Object) this.Leader != (UnityEngine.Object) null)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId = player.Partys[6].GetUnitUniqueID(0);
        DataSource.Bind<UnitData>(this.Leader, player.FindUnitDataByUniqueID(unitUniqueId), false);
        GameParameter.UpdateAll(this.Leader);
      }
      DataSource.Bind<PlayerData>(this.gameObject, MonoSingleton<GameManager>.Instance.Player, false);
    }

    private void UpdateNowScore()
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if ((UnityEngine.Object) this.NowTurn != (UnityEngine.Object) null)
        this.NowTurn.text = towerResuponse.turn_num.ToString();
      if ((UnityEngine.Object) this.NowDied != (UnityEngine.Object) null)
        this.NowDied.text = towerResuponse.died_num.ToString();
      if ((UnityEngine.Object) this.NowRetire != (UnityEngine.Object) null)
        this.NowRetire.text = towerResuponse.retire_num.ToString();
      if ((UnityEngine.Object) this.NowRecover != (UnityEngine.Object) null)
        this.NowRecover.text = towerResuponse.recover_num.ToString();
      if ((UnityEngine.Object) this.NowChallenge != (UnityEngine.Object) null)
        this.NowChallenge.text = towerResuponse.challenge_num.ToString();
      if ((UnityEngine.Object) this.NowLose != (UnityEngine.Object) null)
        this.NowLose.text = towerResuponse.lose_num.ToString();
      if (!((UnityEngine.Object) this.NowReset != (UnityEngine.Object) null))
        return;
      this.NowReset.text = towerResuponse.reset_num.ToString();
    }

    private void UpdateBestScore()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      TowerParam tower = instance.FindTower(towerResuponse.TowerID);
      if (towerResuponse.spd_score != 0)
      {
        this.SetText(this.BestTurn, towerResuponse.spd_score.ToString());
        this.SetText(this.BestDied, towerResuponse.tec_score.ToString());
        this.SetText(this.BestRetire, towerResuponse.ret_score.ToString());
        this.SetText(this.BestRecover, towerResuponse.rcv_score.ToString());
        this.SetText(this.BestChallenge, towerResuponse.challenge_score.ToString());
        this.SetText(this.BestLose, towerResuponse.lose_score.ToString());
        this.SetText(this.BestReset, towerResuponse.reset_score.ToString());
        this.SetRankIcon(this.TurnScore, instance.ConvertTowerScoreToRank(tower, towerResuponse.spd_score, TOWER_SCORE_TYPE.TURN));
        this.SetRankIcon(this.DiedScore, instance.ConvertTowerScoreToRank(tower, towerResuponse.tec_score, TOWER_SCORE_TYPE.DIED));
        this.SetRankIcon(this.RetireScore, instance.ConvertTowerScoreToRank(tower, towerResuponse.ret_score, TOWER_SCORE_TYPE.RETIRE));
        this.SetRankIcon(this.RecoverScore, instance.ConvertTowerScoreToRank(tower, towerResuponse.rcv_score, TOWER_SCORE_TYPE.RECOVER));
        this.SetDefault(this.ChallengeScore);
        this.SetDefault(this.LoseScore);
        this.SetDefault(this.ResetScore);
      }
      else
      {
        this.SetText(this.BestTurn, this.NotSocreText);
        this.SetText(this.BestDied, this.NotSocreText);
        this.SetText(this.BestRetire, this.NotSocreText);
        this.SetText(this.BestRecover, this.NotSocreText);
        this.SetText(this.BestChallenge, this.NotSocreText);
        this.SetText(this.BestLose, this.NotSocreText);
        this.SetText(this.BestReset, this.NotSocreText);
        this.SetDefault(this.TurnScore);
        this.SetDefault(this.DiedScore);
        this.SetDefault(this.RetireScore);
        this.SetDefault(this.RecoverScore);
        this.SetDefault(this.ChallengeScore);
        this.SetDefault(this.LoseScore);
        this.SetDefault(this.ResetScore);
      }
    }

    private void UpdateTotalScore()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      if (towerResuponse.speedRank != 0 && towerResuponse.techRank != 0)
      {
        if ((UnityEngine.Object) this.SpdRank != (UnityEngine.Object) null)
          this.SpdRank.text = towerResuponse.speedRank.ToString();
        if ((UnityEngine.Object) this.TecRank != (UnityEngine.Object) null)
          this.TecRank.text = towerResuponse.techRank.ToString();
        string str = instance.CalcTowerRank(true).ToString();
        if ((UnityEngine.Object) this.TotalScore != (UnityEngine.Object) null)
        {
          string name = str.Replace("_PLUS", string.Empty).Replace("_MINUS", string.Empty);
          Image component = this.TotalScore.GetComponent<Image>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.sprite = this.Icons.GetSprite(name);
        }
        if (str.IndexOf("_PLUS") != -1)
        {
          if (!((UnityEngine.Object) this.PlusObj != (UnityEngine.Object) null))
            return;
          this.PlusObj.gameObject.SetActive(true);
        }
        else
        {
          if (str.IndexOf("_MINUS") == -1 || !((UnityEngine.Object) this.MinusObj != (UnityEngine.Object) null))
            return;
          this.MinusObj.gameObject.SetActive(true);
        }
      }
      else
      {
        if ((UnityEngine.Object) this.SpdRank != (UnityEngine.Object) null)
          this.SpdRank.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.TecRank != (UnityEngine.Object) null)
          this.TecRank.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.TotalScore != (UnityEngine.Object) null)
          this.TotalScore.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.PlusObj != (UnityEngine.Object) null)
          this.PlusObj.gameObject.SetActive(false);
        if (!((UnityEngine.Object) this.MinusObj != (UnityEngine.Object) null))
          return;
        this.MinusObj.gameObject.SetActive(false);
      }
    }

    private void SetRankIcon(GameObject obj, string rank)
    {
      if (!((UnityEngine.Object) obj != (UnityEngine.Object) null))
        return;
      Image component = obj.GetComponent<Image>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null && (UnityEngine.Object) this.Icons != (UnityEngine.Object) null)
      {
        component.sprite = this.Icons.GetSprite(rank);
        obj.gameObject.SetActive(true);
      }
      else
        obj.gameObject.SetActive(false);
    }

    private void SetDefault(GameObject obj)
    {
      if (!((UnityEngine.Object) obj != (UnityEngine.Object) null))
        return;
      obj.gameObject.SetActive(false);
    }

    private void SetText(Text text, string val)
    {
      if (!((UnityEngine.Object) text != (UnityEngine.Object) null))
        return;
      text.text = val;
    }
  }
}
