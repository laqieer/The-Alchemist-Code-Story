// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRankInfo
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
  public class TowerRankInfo : MonoBehaviour
  {
    private readonly int SHOW_VIP_RANK = 4;
    public Text Ranking;
    public Image OwnIcon;
    public GameObject OwnObj;
    public Image OwnTotalScore;
    public Text OwnSpeedScore;
    public Text OwnTechScore;
    public GameObject OwnSpeedObj;
    public GameObject OwnTechObj;
    [HeaderBar("▼「ランキング」表示用（最上階までクリア済み）")]
    public GameObject ClearPage;
    public GameObject ClearPageParent;
    public ListItemEvents ClearTemplate;
    [HeaderBar("▼「ランキング」表示用（最上階まで未クリア）")]
    public GameObject NotClearPage;
    public GameObject NotClearPageParent;
    public ListItemEvents NotClearTemplate;
    [HeaderBar("▼「ランキング」表示用（集計中）")]
    public GameObject NotDataObj;
    [HeaderBar("▼「自分の戦績」表示用オブジェクトの親")]
    [SerializeField]
    private GameObject m_TowerPlayerInfoRoot;
    [HeaderBar("▼タブ")]
    public Toggle Speed;
    public Toggle Tech;
    public Toggle OwnStatus;
    public GameObject Root;
    private SpriteSheet mSheet;
    private bool IsSpeed = true;

    private void Start()
    {
      this.mSheet = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
      this.UpdateOwnValue();
      this.UpdateRankValue();
      if (Object.op_Inequality((Object) this.Speed, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.Speed.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeSpeed)));
      }
      if (Object.op_Inequality((Object) this.Tech, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.Tech.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeTech)));
      }
      if (Object.op_Inequality((Object) this.OwnStatus, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.OwnStatus.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeOwnStatus)));
      }
      if (!Object.op_Inequality((Object) this.OwnObj, (Object) null))
        return;
      DataSource.Bind<PlayerData>(this.OwnObj, MonoSingleton<GameManager>.Instance.Player);
    }

    private int GetSameRank(int score, int rank)
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (towerResuponse.SpdRank != null && towerResuponse.SpdRank.Length > 0 || towerResuponse.TecRank != null && towerResuponse.TecRank.Length > 0)
      {
        TowerResuponse.TowerRankParam[] towerRankParamArray = !this.IsSpeed ? towerResuponse.TecRank : towerResuponse.SpdRank;
        for (int index = 0; index < towerRankParamArray.Length; ++index)
        {
          if (towerRankParamArray[index].score == score)
            return index + 1;
        }
      }
      return rank;
    }

    private void UpdateOwnValue()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = instance.TowerResuponse;
      TowerParam tower = instance.FindTower(towerResuponse.TowerID);
      if (towerResuponse != null)
      {
        bool flag = towerResuponse.speedRank != 0 && towerResuponse.techRank != 0;
        if (Object.op_Inequality((Object) this.ClearPage, (Object) null))
          this.ClearPage.gameObject.SetActive(flag);
        if (Object.op_Inequality((Object) this.NotClearPage, (Object) null))
          this.NotClearPage.gameObject.SetActive(!flag);
      }
      int rank = !this.IsSpeed ? towerResuponse.techRank : towerResuponse.speedRank;
      int sameRank = this.GetSameRank(!this.IsSpeed ? towerResuponse.tec_score : towerResuponse.spd_score, rank);
      if (Object.op_Inequality((Object) this.OwnIcon, (Object) null))
        this.OwnIcon.sprite = sameRank > this.SHOW_VIP_RANK ? this.mSheet.GetSprite("normal") : this.mSheet.GetSprite((sameRank - 1).ToString());
      if (Object.op_Inequality((Object) this.Ranking, (Object) null))
      {
        ((Component) this.Ranking).gameObject.SetActive(sameRank > this.SHOW_VIP_RANK);
        this.Ranking.text = sameRank.ToString() + LocalizedText.Get("sys.TOWER_RANK_LABEL");
      }
      if (Object.op_Inequality((Object) this.OwnObj, (Object) null))
      {
        PlayerData player = instance.Player;
        long unitUniqueId = player.Partys[6].GetUnitUniqueID(0);
        DataSource.Bind<UnitData>(this.OwnObj, player.FindUnitDataByUniqueID(unitUniqueId));
      }
      if (Object.op_Inequality((Object) this.OwnSpeedObj, (Object) null))
        this.OwnSpeedObj.gameObject.SetActive(this.IsSpeed);
      if (Object.op_Inequality((Object) this.OwnTechObj, (Object) null))
        this.OwnTechObj.gameObject.SetActive(!this.IsSpeed);
      if (this.IsSpeed)
      {
        if (Object.op_Inequality((Object) this.OwnSpeedScore, (Object) null))
          this.OwnSpeedScore.text = towerResuponse.spd_score.ToString();
      }
      else if (Object.op_Inequality((Object) this.OwnSpeedScore, (Object) null))
        this.OwnTechScore.text = towerResuponse.tec_score.ToString();
      if (!Object.op_Inequality((Object) this.OwnTotalScore, (Object) null))
        return;
      string empty = string.Empty;
      this.OwnTotalScore.sprite = this.mSheet.GetSprite(!this.IsSpeed ? instance.ConvertTowerScoreToRank(tower, towerResuponse.tec_score, TOWER_SCORE_TYPE.DIED) : instance.ConvertTowerScoreToRank(tower, towerResuponse.spd_score, TOWER_SCORE_TYPE.TURN));
    }

    private void UpdateRankValue(bool isSpeedRank = true)
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      GameObject gameObject = this.NotClearPageParent;
      ListItemEvents listItemEvents1 = this.NotClearTemplate;
      if (towerResuponse != null)
      {
        gameObject = towerResuponse.speedRank == 0 || towerResuponse.techRank == 0 ? this.NotClearPageParent : this.ClearPageParent;
        listItemEvents1 = towerResuponse.speedRank == 0 || towerResuponse.techRank == 0 ? this.NotClearTemplate : this.ClearTemplate;
        if (towerResuponse.SpdRank == null || towerResuponse.SpdRank.Length == 0 || towerResuponse.TecRank == null || towerResuponse.TecRank.Length == 0)
        {
          if (!Object.op_Inequality((Object) this.NotDataObj, (Object) null))
            return;
          this.NotDataObj.gameObject.SetActive(true);
          return;
        }
      }
      if (Object.op_Inequality((Object) this.NotClearTemplate, (Object) null))
        ((Component) this.NotClearTemplate).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.ClearTemplate, (Object) null))
        ((Component) this.ClearTemplate).gameObject.SetActive(false);
      Transform transform = gameObject.transform;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null) && ((Component) child).gameObject.activeSelf)
          Object.DestroyImmediate((Object) ((Component) child).gameObject);
      }
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        gameObject.gameObject.SetActive(true);
      TowerResuponse.TowerRankParam[] towerRankParamArray = !isSpeedRank ? towerResuponse.TecRank : towerResuponse.SpdRank;
      for (int index1 = 0; index1 < towerRankParamArray.Length; ++index1)
      {
        ListItemEvents listItemEvents2 = Object.Instantiate<ListItemEvents>(listItemEvents1);
        int num = index1;
        if (Object.op_Inequality((Object) listItemEvents2, (Object) null))
        {
          DataSource.Bind<UnitData>(((Component) listItemEvents2).gameObject, towerRankParamArray[index1].unit);
          DataSource.Bind<ViewGuildData>(((Component) listItemEvents2).gameObject, towerRankParamArray[index1].ViewGuild);
          SerializeValueBehaviour component = ((Component) listItemEvents2).GetComponent<SerializeValueBehaviour>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            long id = towerRankParamArray[index1].ViewGuild == null ? 0L : (long) towerRankParamArray[index1].ViewGuild.id;
            component.list.SetField(GuildSVB_Key.GUILD_ID, (int) id);
          }
          listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
          listItemEvents2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
          ((Component) listItemEvents2).transform.SetParent(gameObject.transform, false);
          ((Component) listItemEvents2).gameObject.SetActive(true);
          for (int index2 = 0; index2 < index1; ++index2)
          {
            if (towerRankParamArray[index2].score == towerRankParamArray[index1].score)
            {
              num = index2;
              break;
            }
          }
          this.UpdateValue(listItemEvents2, num, towerRankParamArray[index1], isSpeedRank);
        }
      }
      if (!Object.op_Inequality((Object) this.Root, (Object) null))
        return;
      GameParameter.UpdateAll(this.Root);
    }

    private void UpdateValue(
      ListItemEvents obj,
      int num,
      TowerResuponse.TowerRankParam param,
      bool isSpeed)
    {
      if (Object.op_Equality((Object) this.mSheet, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      TowerParam tower = instance.FindTower(towerResuponse.TowerID);
      DataSource.Bind<TowerResuponse.TowerRankParam>(((Component) obj).gameObject, param);
      Transform transform1 = ((Component) obj).transform.Find("body");
      if (!Object.op_Inequality((Object) transform1, (Object) null))
        return;
      Transform transform2 = ((Component) transform1).transform.Find("ranking");
      if (Object.op_Inequality((Object) transform2, (Object) null))
      {
        Image component = ((Component) transform2).GetComponent<Image>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.sprite = num >= this.SHOW_VIP_RANK ? this.mSheet.GetSprite("normal") : this.mSheet.GetSprite(num.ToString());
      }
      Transform transform3 = ((Component) transform1).transform.Find("rank");
      if (Object.op_Inequality((Object) transform3, (Object) null))
      {
        Text component = ((Component) transform3).GetComponent<Text>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.text = num >= this.SHOW_VIP_RANK ? (num + 1).ToString() + LocalizedText.Get("sys.TOWER_RANK_LABEL") : string.Empty;
      }
      Transform transform4 = ((Component) transform1).transform.Find("Text_player");
      if (Object.op_Inequality((Object) transform4, (Object) null))
      {
        Text component = ((Component) transform4).GetComponent<Text>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.text = param.name;
      }
      Transform transform5 = ((Component) transform1).transform.Find("player_level");
      if (Object.op_Inequality((Object) transform5, (Object) null))
      {
        Text component = ((Component) transform5).GetComponent<Text>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.text = LocalizedText.Get("sys.TOWER_RANK_LBL_LV") + param.lv.ToString();
      }
      if (isSpeed)
      {
        Transform transform6 = ((Component) transform1).transform.Find("speed");
        if (Object.op_Inequality((Object) transform6, (Object) null))
        {
          Transform transform7 = ((Component) transform6).transform.Find("speed_cnt");
          if (Object.op_Inequality((Object) transform7, (Object) null))
          {
            Text component = ((Component) transform7).GetComponent<Text>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.text = param.score.ToString();
          }
          ((Component) transform6).gameObject.SetActive(true);
        }
        string rank = instance.ConvertTowerScoreToRank(tower, param.score, TOWER_SCORE_TYPE.TURN);
        Transform transform8 = ((Component) transform1).transform.Find("score_img");
        if (!Object.op_Inequality((Object) transform8, (Object) null))
          return;
        Image component1 = ((Component) transform8).GetComponent<Image>();
        if (!Object.op_Inequality((Object) component1, (Object) null))
          return;
        component1.sprite = this.mSheet.GetSprite(rank);
      }
      else
      {
        Transform transform9 = ((Component) transform1).transform.Find("tech");
        if (Object.op_Inequality((Object) transform9, (Object) null))
        {
          Transform transform10 = ((Component) transform9).transform.Find("tech_cnt");
          if (Object.op_Inequality((Object) transform10, (Object) null))
          {
            Text component = ((Component) transform10).GetComponent<Text>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.text = param.score.ToString();
          }
          ((Component) transform9).gameObject.SetActive(true);
        }
        string rank = instance.ConvertTowerScoreToRank(tower, param.score, TOWER_SCORE_TYPE.DIED);
        Transform transform11 = ((Component) transform1).transform.Find("score_img");
        if (!Object.op_Inequality((Object) transform11, (Object) null))
          return;
        Image component2 = ((Component) transform11).GetComponent<Image>();
        if (!Object.op_Inequality((Object) component2, (Object) null))
          return;
        component2.sprite = this.mSheet.GetSprite(rank);
      }
    }

    private void OnItemSelect(GameObject go)
    {
    }

    private void OnItemDetail(GameObject go)
    {
    }

    public void OnChangeSpeed(bool val)
    {
      if (!val)
        return;
      GameUtility.SetGameObjectActive(this.m_TowerPlayerInfoRoot, false);
      this.IsSpeed = true;
      this.UpdateOwnValue();
      this.UpdateRankValue(this.IsSpeed);
    }

    public void OnChangeTech(bool val)
    {
      if (!val)
        return;
      GameUtility.SetGameObjectActive(this.m_TowerPlayerInfoRoot, false);
      this.IsSpeed = false;
      this.UpdateOwnValue();
      this.UpdateRankValue(this.IsSpeed);
    }

    public void OnChangeOwnStatus(bool val)
    {
      if (!val)
        return;
      GameUtility.SetGameObjectActive(this.m_TowerPlayerInfoRoot, true);
      GameUtility.SetGameObjectActive(this.ClearPage, false);
      GameUtility.SetGameObjectActive(this.NotClearPage, false);
      GameUtility.SetGameObjectActive(this.NotDataObj, false);
    }
  }
}
