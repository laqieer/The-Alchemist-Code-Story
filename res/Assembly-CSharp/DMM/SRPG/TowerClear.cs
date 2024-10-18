﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TowerClear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TowerClear : MonoBehaviour
  {
    private readonly int RANKIN_SCORE = 10;
    public Text TowerName;
    public Text ArrivedNum;
    public Text BigArrivedNum;
    public Text BigArrivedNumEff;
    public GameObject RankInfo;
    public GameObject ResultInfo;
    public Button BackButton;
    public Button NextButton;
    public Text[] CountupText = new Text[0];
    public GameObject[] ScoreObj = new GameObject[0];
    public GameObject[] NewRecord = new GameObject[0];
    public GameObject[] ScoreContents = new GameObject[0];
    public GameObject SpeedRank;
    public GameObject TechRank;
    public GameObject RankInObj;
    public GameObject RankingObj;
    public GameObject ContinueObj;
    public GameObject CongraObj;
    public GameObject TotalScoreObj;
    public GameObject PlusObj;
    public GameObject MinusObj;
    public Image TotalScoreImg;
    public Text SpeedRankText;
    public Text TechRankText;
    private TowerClear.CLEAR_FLOW Nowflow;
    private int NowCount;
    private Text NowText;
    private bool NextState;
    private SpriteSheet Icons;
    private int CountupIndex;
    private int[] NowScore;
    private int[] OldBestScore;
    private int CountValue;
    private TowerParam m_TowerParam;
    private readonly int EFF_TIME = 2;

    private TowerFloorParam TowerFloorParam
    {
      get
      {
        return this.TowerResuponse == null ? (TowerFloorParam) null : this.TowerResuponse.GetCurrentFloor();
      }
    }

    private TowerResuponse TowerResuponse => MonoSingleton<GameManager>.Instance.TowerResuponse;

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      this.m_TowerParam = instance.FindTower(instance.TowerResuponse.TowerID);
      this.NowScore = new int[4];
      this.NowScore[0] = towerResuponse.turn_num;
      this.NowScore[1] = towerResuponse.died_num;
      this.NowScore[2] = towerResuponse.retire_num;
      this.NowScore[3] = towerResuponse.recover_num;
      this.OldBestScore = new int[4];
      this.OldBestScore[0] = towerResuponse.spd_score;
      this.OldBestScore[1] = towerResuponse.tec_score;
      this.OldBestScore[2] = towerResuponse.ret_score;
      this.OldBestScore[3] = towerResuponse.rcv_score;
      this.CountupIndex = 0;
      if (Object.op_Inequality((Object) this.NextButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.NextButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickNext)));
      }
      if (this.NowScore == null || this.ScoreContents == null || this.ScoreObj == null || this.NewRecord == null)
        DebugUtility.LogWarning("Error TowerClear Param Check!!!!");
      this.Icons = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
    }

    private void Update()
    {
      if (this.NowScore == null || this.ScoreContents == null || this.ScoreObj == null || this.NewRecord == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      switch (this.Nowflow)
      {
        case TowerClear.CLEAR_FLOW.SCORE_IN:
          this.RefleshResult();
          break;
        case TowerClear.CLEAR_FLOW.IN:
          this.Nowflow = TowerClear.CLEAR_FLOW.LOOP;
          this.NowCount = 0;
          this.CountValue = (int) ((double) (this.NowScore[this.CountupIndex] / this.EFF_TIME) * (double) Time.deltaTime);
          this.CountValue = Mathf.Max(this.CountValue, 1);
          this.NowText = this.CountupText[this.CountupIndex];
          if (!Object.op_Inequality((Object) this.ScoreContents[this.CountupIndex], (Object) null))
            break;
          this.ScoreContents[this.CountupIndex].gameObject.SetActive(true);
          break;
        case TowerClear.CLEAR_FLOW.LOOP:
          this.NowCount += this.CountValue;
          if (this.NowCount >= this.NowScore[this.CountupIndex] || this.NextState)
          {
            this.NowCount = this.NowScore[this.CountupIndex];
            this.Nowflow = TowerClear.CLEAR_FLOW.SCORE;
            this.NextState = false;
          }
          if (!Object.op_Inequality((Object) this.NowText, (Object) null))
            break;
          this.NowText.text = this.NowCount.ToString();
          break;
        case TowerClear.CLEAR_FLOW.SCORE:
          if (Object.op_Inequality((Object) this.Icons, (Object) null) && Object.op_Inequality((Object) this.ScoreObj[this.CountupIndex], (Object) null))
          {
            this.ScoreObj[this.CountupIndex].gameObject.SetActive(true);
            Image component = this.ScoreObj[this.CountupIndex].GetComponent<Image>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              string rank = instance.ConvertTowerScoreToRank(this.m_TowerParam, this.NowCount, (TOWER_SCORE_TYPE) this.CountupIndex);
              component.sprite = this.Icons.GetSprite(rank);
            }
          }
          if (Object.op_Inequality((Object) this.NewRecord[this.CountupIndex], (Object) null) && this.NowScore[this.CountupIndex] < this.OldBestScore[this.CountupIndex])
            this.NewRecord[this.CountupIndex].gameObject.SetActive(true);
          if (++this.CountupIndex < 4)
          {
            this.Nowflow = TowerClear.CLEAR_FLOW.IN;
            break;
          }
          this.Nowflow = TowerClear.CLEAR_FLOW.TOTAL_SCORE;
          break;
        case TowerClear.CLEAR_FLOW.TOTAL_SCORE:
          if (Object.op_Inequality((Object) this.TotalScoreObj, (Object) null))
          {
            this.TotalScoreObj.gameObject.SetActive(true);
            string str = instance.CalcTowerRank().ToString();
            if (Object.op_Inequality((Object) this.TotalScoreImg, (Object) null))
              this.TotalScoreImg.sprite = this.Icons.GetSprite(str.Replace("_PLUS", string.Empty).Replace("_MINUS", string.Empty));
            if (str.IndexOf("_PLUS") != -1)
            {
              if (Object.op_Inequality((Object) this.PlusObj, (Object) null))
                this.PlusObj.gameObject.SetActive(true);
            }
            else if (str.IndexOf("_MINUS") != -1 && Object.op_Inequality((Object) this.MinusObj, (Object) null))
              this.MinusObj.gameObject.SetActive(true);
            MonoSingleton<GameManager>.Instance.Player.OnTowerScore();
          }
          this.Nowflow = TowerClear.CLEAR_FLOW.SPEED_RANK_IN;
          break;
        case TowerClear.CLEAR_FLOW.SPEED_RANK_IN:
          if (!this.NextState)
            break;
          if (towerResuponse.speedRank <= this.RANKIN_SCORE)
          {
            if (Object.op_Inequality((Object) this.RankingObj, (Object) null))
              this.RankingObj.gameObject.SetActive(true);
            if (Object.op_Inequality((Object) this.RankInObj, (Object) null))
              this.RankInObj.gameObject.SetActive(true);
            if (Object.op_Inequality((Object) this.CongraObj, (Object) null))
              this.CongraObj.gameObject.SetActive(false);
            if (Object.op_Inequality((Object) this.ResultInfo, (Object) null))
              this.ResultInfo.gameObject.SetActive(false);
            if (Object.op_Inequality((Object) this.SpeedRank, (Object) null))
              this.SpeedRank.gameObject.SetActive(true);
            if (Object.op_Inequality((Object) this.SpeedRankText, (Object) null))
              this.SpeedRankText.text = towerResuponse.speedRank.ToString();
          }
          this.Nowflow = TowerClear.CLEAR_FLOW.TECH_RANK_IN;
          this.NextState = false;
          break;
        case TowerClear.CLEAR_FLOW.TECH_RANK_IN:
          if (!this.NextState)
            break;
          if (towerResuponse.techRank <= this.RANKIN_SCORE)
          {
            if (Object.op_Inequality((Object) this.RankingObj, (Object) null))
              this.RankingObj.gameObject.SetActive(true);
            if (Object.op_Inequality((Object) this.RankInObj, (Object) null))
              this.RankInObj.gameObject.SetActive(true);
            if (Object.op_Inequality((Object) this.CongraObj, (Object) null))
              this.CongraObj.gameObject.SetActive(false);
            if (Object.op_Inequality((Object) this.ResultInfo, (Object) null))
              this.ResultInfo.gameObject.SetActive(false);
            if (Object.op_Inequality((Object) this.TechRank, (Object) null))
              this.TechRank.gameObject.SetActive(true);
            if (Object.op_Inequality((Object) this.TechRankText, (Object) null))
              this.TechRankText.text = towerResuponse.techRank.ToString();
          }
          this.Nowflow = TowerClear.CLEAR_FLOW.FINISH;
          this.NextState = false;
          break;
        case TowerClear.CLEAR_FLOW.FINISH:
          if (Object.op_Inequality((Object) this.BackButton, (Object) null))
            ((Component) this.BackButton).gameObject.SetActive(true);
          this.Nowflow = TowerClear.CLEAR_FLOW.NONE;
          break;
      }
    }

    public void Refresh()
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData partyOfType = player.FindPartyOfType(PlayerPartyTypes.Tower);
      UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(partyOfType.GetUnitUniqueID(partyOfType.LeaderIndex));
      if (unitDataByUniqueId != null)
        DataSource.Bind<UnitData>(((Component) this).gameObject, unitDataByUniqueId);
      if (towerResuponse.arrived_num > 0)
      {
        if (this.TowerFloorParam != null)
          this.TowerName.text = LocalizedText.Get("sys.TOWER_CLEAR_DESC", (object) this.TowerFloorParam.title, (object) this.TowerFloorParam.GetFloorNo());
        string str = this.TowerResuponse.arrived_num.ToString();
        if (this.TowerResuponse.arrived_num > 99)
        {
          if (Object.op_Inequality((Object) this.ArrivedNum, (Object) null))
          {
            ((Component) this.ArrivedNum).gameObject.SetActive(true);
            this.ArrivedNum.text = str;
          }
        }
        else if (Object.op_Inequality((Object) this.BigArrivedNum, (Object) null) && Object.op_Inequality((Object) this.BigArrivedNumEff, (Object) null))
        {
          ((Component) this.BigArrivedNum).gameObject.SetActive(true);
          ((Component) this.BigArrivedNumEff).gameObject.SetActive(true);
          this.BigArrivedNum.text = str;
          this.BigArrivedNumEff.text = str;
        }
        if (Object.op_Inequality((Object) this.ResultInfo, (Object) null))
          this.ResultInfo.gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.BackButton, (Object) null))
          ((Component) this.BackButton).gameObject.SetActive(false);
        this.Nowflow = TowerClear.CLEAR_FLOW.RANK_IN;
      }
      else
        this.RefleshResult();
    }

    private void RefleshResult()
    {
      if (MonoSingleton<GameManager>.Instance.TowerResuponse.clear == 2)
      {
        if (Object.op_Inequality((Object) this.RankInfo, (Object) null))
          this.RankInfo.gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.ResultInfo, (Object) null))
          this.ResultInfo.gameObject.SetActive(true);
        this.Nowflow = TowerClear.CLEAR_FLOW.IN;
      }
      else
        this.Nowflow = TowerClear.CLEAR_FLOW.FINISH;
    }

    private void OnClickNext()
    {
      this.NextState = true;
      if (this.Nowflow != TowerClear.CLEAR_FLOW.RANK_IN)
        return;
      this.RefleshResult();
      this.NextState = false;
    }

    public enum TOWER_CLEAR_FLAG
    {
      NOT_CLEAR,
      CLEAR,
      CLEAR_AND_SCORE,
    }

    private enum CLEAR_FLOW
    {
      NONE,
      RANK_IN,
      SCORE_IN,
      IN,
      LOOP,
      SCORE,
      TOTAL_SCORE,
      SPEED_RANK_IN,
      TECH_RANK_IN,
      FINISH,
    }
  }
}
