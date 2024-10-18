// Decompiled with JetBrains decompiler
// Type: SRPG.TowerClear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerClear : MonoBehaviour
  {
    private readonly int RANKIN_SCORE = 10;
    public Text[] CountupText = new Text[0];
    public GameObject[] ScoreObj = new GameObject[0];
    public GameObject[] NewRecord = new GameObject[0];
    public GameObject[] ScoreContents = new GameObject[0];
    private readonly int EFF_TIME = 2;
    public Text TowerName;
    public Text ArrivedNum;
    public Text BigArrivedNum;
    public Text BigArrivedNumEff;
    public GameObject RankInfo;
    public GameObject ResultInfo;
    public Button BackButton;
    public Button NextButton;
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

    private TowerFloorParam TowerFloorParam
    {
      get
      {
        if (this.TowerResuponse == null)
          return (TowerFloorParam) null;
        return this.TowerResuponse.GetCurrentFloor();
      }
    }

    private TowerResuponse TowerResuponse
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.TowerResuponse;
      }
    }

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
      if ((UnityEngine.Object) this.NextButton != (UnityEngine.Object) null)
        this.NextButton.onClick.AddListener(new UnityAction(this.OnClickNext));
      if (this.NowScore == null || this.ScoreContents == null || (this.ScoreObj == null || this.NewRecord == null))
        DebugUtility.LogWarning("Error TowerClear Param Check!!!!");
      this.Icons = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
    }

    private void Update()
    {
      if (this.NowScore == null || this.ScoreContents == null || (this.ScoreObj == null || this.NewRecord == null))
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
          if (!((UnityEngine.Object) this.ScoreContents[this.CountupIndex] != (UnityEngine.Object) null))
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
          if (!((UnityEngine.Object) this.NowText != (UnityEngine.Object) null))
            break;
          this.NowText.text = this.NowCount.ToString();
          break;
        case TowerClear.CLEAR_FLOW.SCORE:
          if ((UnityEngine.Object) this.Icons != (UnityEngine.Object) null && (UnityEngine.Object) this.ScoreObj[this.CountupIndex] != (UnityEngine.Object) null)
          {
            this.ScoreObj[this.CountupIndex].gameObject.SetActive(true);
            Image component = this.ScoreObj[this.CountupIndex].GetComponent<Image>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              string rank = instance.ConvertTowerScoreToRank(this.m_TowerParam, this.NowCount, (TOWER_SCORE_TYPE) this.CountupIndex);
              component.sprite = this.Icons.GetSprite(rank);
            }
          }
          if ((UnityEngine.Object) this.NewRecord[this.CountupIndex] != (UnityEngine.Object) null && this.NowScore[this.CountupIndex] < this.OldBestScore[this.CountupIndex])
            this.NewRecord[this.CountupIndex].gameObject.SetActive(true);
          if (++this.CountupIndex < 4)
          {
            this.Nowflow = TowerClear.CLEAR_FLOW.IN;
            break;
          }
          this.Nowflow = TowerClear.CLEAR_FLOW.TOTAL_SCORE;
          break;
        case TowerClear.CLEAR_FLOW.TOTAL_SCORE:
          if ((UnityEngine.Object) this.TotalScoreObj != (UnityEngine.Object) null)
          {
            this.TotalScoreObj.gameObject.SetActive(true);
            string str = instance.CalcTowerRank(true).ToString();
            if ((UnityEngine.Object) this.TotalScoreImg != (UnityEngine.Object) null)
              this.TotalScoreImg.sprite = this.Icons.GetSprite(str.Replace("_PLUS", string.Empty).Replace("_MINUS", string.Empty));
            if (str.IndexOf("_PLUS") != -1)
            {
              if ((UnityEngine.Object) this.PlusObj != (UnityEngine.Object) null)
                this.PlusObj.gameObject.SetActive(true);
            }
            else if (str.IndexOf("_MINUS") != -1 && (UnityEngine.Object) this.MinusObj != (UnityEngine.Object) null)
              this.MinusObj.gameObject.SetActive(true);
            MonoSingleton<GameManager>.Instance.Player.OnTowerScore(true);
          }
          this.Nowflow = TowerClear.CLEAR_FLOW.SPEED_RANK_IN;
          break;
        case TowerClear.CLEAR_FLOW.SPEED_RANK_IN:
          if (!this.NextState)
            break;
          if (towerResuponse.speedRank <= this.RANKIN_SCORE)
          {
            if ((UnityEngine.Object) this.RankingObj != (UnityEngine.Object) null)
              this.RankingObj.gameObject.SetActive(true);
            if ((UnityEngine.Object) this.RankInObj != (UnityEngine.Object) null)
              this.RankInObj.gameObject.SetActive(true);
            if ((UnityEngine.Object) this.CongraObj != (UnityEngine.Object) null)
              this.CongraObj.gameObject.SetActive(false);
            if ((UnityEngine.Object) this.ResultInfo != (UnityEngine.Object) null)
              this.ResultInfo.gameObject.SetActive(false);
            if ((UnityEngine.Object) this.SpeedRank != (UnityEngine.Object) null)
              this.SpeedRank.gameObject.SetActive(true);
            if ((UnityEngine.Object) this.SpeedRankText != (UnityEngine.Object) null)
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
            if ((UnityEngine.Object) this.RankingObj != (UnityEngine.Object) null)
              this.RankingObj.gameObject.SetActive(true);
            if ((UnityEngine.Object) this.RankInObj != (UnityEngine.Object) null)
              this.RankInObj.gameObject.SetActive(true);
            if ((UnityEngine.Object) this.CongraObj != (UnityEngine.Object) null)
              this.CongraObj.gameObject.SetActive(false);
            if ((UnityEngine.Object) this.ResultInfo != (UnityEngine.Object) null)
              this.ResultInfo.gameObject.SetActive(false);
            if ((UnityEngine.Object) this.TechRank != (UnityEngine.Object) null)
              this.TechRank.gameObject.SetActive(true);
            if ((UnityEngine.Object) this.TechRankText != (UnityEngine.Object) null)
              this.TechRankText.text = towerResuponse.techRank.ToString();
          }
          this.Nowflow = TowerClear.CLEAR_FLOW.FINISH;
          this.NextState = false;
          break;
        case TowerClear.CLEAR_FLOW.FINISH:
          if ((UnityEngine.Object) this.BackButton != (UnityEngine.Object) null)
            this.BackButton.gameObject.SetActive(true);
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
        DataSource.Bind<UnitData>(this.gameObject, unitDataByUniqueId, false);
      if (towerResuponse.arrived_num > 0)
      {
        if (this.TowerFloorParam != null)
          this.TowerName.text = LocalizedText.Get("sys.TOWER_CLEAR_DESC", (object) this.TowerFloorParam.title, (object) this.TowerFloorParam.GetFloorNo());
        string str = this.TowerResuponse.arrived_num.ToString();
        if (this.TowerResuponse.arrived_num > 99)
        {
          if ((UnityEngine.Object) this.ArrivedNum != (UnityEngine.Object) null)
          {
            this.ArrivedNum.gameObject.SetActive(true);
            this.ArrivedNum.text = str;
          }
        }
        else if ((UnityEngine.Object) this.BigArrivedNum != (UnityEngine.Object) null && (UnityEngine.Object) this.BigArrivedNumEff != (UnityEngine.Object) null)
        {
          this.BigArrivedNum.gameObject.SetActive(true);
          this.BigArrivedNumEff.gameObject.SetActive(true);
          this.BigArrivedNum.text = str;
          this.BigArrivedNumEff.text = str;
        }
        if ((UnityEngine.Object) this.ResultInfo != (UnityEngine.Object) null)
          this.ResultInfo.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.BackButton != (UnityEngine.Object) null)
          this.BackButton.gameObject.SetActive(false);
        this.Nowflow = TowerClear.CLEAR_FLOW.RANK_IN;
      }
      else
        this.RefleshResult();
    }

    private void RefleshResult()
    {
      if (MonoSingleton<GameManager>.Instance.TowerResuponse.clear == 2)
      {
        if ((UnityEngine.Object) this.RankInfo != (UnityEngine.Object) null)
          this.RankInfo.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.ResultInfo != (UnityEngine.Object) null)
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
