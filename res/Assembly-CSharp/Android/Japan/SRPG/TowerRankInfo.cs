// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRankInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerRankInfo : MonoBehaviour
  {
    private readonly int SHOW_VIP_RANK = 4;
    private bool IsSpeed = true;
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

    private void Start()
    {
      this.mSheet = AssetManager.Load<SpriteSheet>("UI/TowerRankIcon");
      this.UpdateOwnValue();
      this.UpdateRankValue(true);
      if ((UnityEngine.Object) this.Speed != (UnityEngine.Object) null)
        this.Speed.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeSpeed));
      if ((UnityEngine.Object) this.Tech != (UnityEngine.Object) null)
        this.Tech.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeTech));
      if ((UnityEngine.Object) this.OwnStatus != (UnityEngine.Object) null)
        this.OwnStatus.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeOwnStatus));
      if (!((UnityEngine.Object) this.OwnObj != (UnityEngine.Object) null))
        return;
      DataSource.Bind<PlayerData>(this.OwnObj, MonoSingleton<GameManager>.Instance.Player, false);
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
        if ((UnityEngine.Object) this.ClearPage != (UnityEngine.Object) null)
          this.ClearPage.gameObject.SetActive(flag);
        if ((UnityEngine.Object) this.NotClearPage != (UnityEngine.Object) null)
          this.NotClearPage.gameObject.SetActive(!flag);
      }
      int rank = !this.IsSpeed ? towerResuponse.techRank : towerResuponse.speedRank;
      int sameRank = this.GetSameRank(!this.IsSpeed ? towerResuponse.tec_score : towerResuponse.spd_score, rank);
      if ((UnityEngine.Object) this.OwnIcon != (UnityEngine.Object) null)
        this.OwnIcon.sprite = sameRank > this.SHOW_VIP_RANK ? this.mSheet.GetSprite("normal") : this.mSheet.GetSprite((sameRank - 1).ToString());
      if ((UnityEngine.Object) this.Ranking != (UnityEngine.Object) null)
      {
        this.Ranking.gameObject.SetActive(sameRank > this.SHOW_VIP_RANK);
        this.Ranking.text = sameRank.ToString() + LocalizedText.Get("sys.TOWER_RANK_LABEL");
      }
      if ((UnityEngine.Object) this.OwnObj != (UnityEngine.Object) null)
      {
        PlayerData player = instance.Player;
        long unitUniqueId = player.Partys[6].GetUnitUniqueID(0);
        DataSource.Bind<UnitData>(this.OwnObj, player.FindUnitDataByUniqueID(unitUniqueId), false);
      }
      if ((UnityEngine.Object) this.OwnSpeedObj != (UnityEngine.Object) null)
        this.OwnSpeedObj.gameObject.SetActive(this.IsSpeed);
      if ((UnityEngine.Object) this.OwnTechObj != (UnityEngine.Object) null)
        this.OwnTechObj.gameObject.SetActive(!this.IsSpeed);
      if (this.IsSpeed)
      {
        if ((UnityEngine.Object) this.OwnSpeedScore != (UnityEngine.Object) null)
          this.OwnSpeedScore.text = towerResuponse.spd_score.ToString();
      }
      else if ((UnityEngine.Object) this.OwnSpeedScore != (UnityEngine.Object) null)
        this.OwnTechScore.text = towerResuponse.tec_score.ToString();
      if (!((UnityEngine.Object) this.OwnTotalScore != (UnityEngine.Object) null))
        return;
      string empty = string.Empty;
      this.OwnTotalScore.sprite = this.mSheet.GetSprite(!this.IsSpeed ? instance.ConvertTowerScoreToRank(tower, towerResuponse.tec_score, TOWER_SCORE_TYPE.DIED) : instance.ConvertTowerScoreToRank(tower, towerResuponse.spd_score, TOWER_SCORE_TYPE.TURN));
    }

    private void UpdateRankValue(bool isSpeedRank = true)
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      GameObject gameObject = this.NotClearPageParent;
      ListItemEvents original = this.NotClearTemplate;
      if (towerResuponse != null)
      {
        gameObject = towerResuponse.speedRank == 0 || towerResuponse.techRank == 0 ? this.NotClearPageParent : this.ClearPageParent;
        original = towerResuponse.speedRank == 0 || towerResuponse.techRank == 0 ? this.NotClearTemplate : this.ClearTemplate;
        if (towerResuponse.SpdRank == null || towerResuponse.SpdRank.Length == 0 || (towerResuponse.TecRank == null || towerResuponse.TecRank.Length == 0))
        {
          if (!((UnityEngine.Object) this.NotDataObj != (UnityEngine.Object) null))
            return;
          this.NotDataObj.gameObject.SetActive(true);
          return;
        }
      }
      if ((UnityEngine.Object) this.NotClearTemplate != (UnityEngine.Object) null)
        this.NotClearTemplate.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.ClearTemplate != (UnityEngine.Object) null)
        this.ClearTemplate.gameObject.SetActive(false);
      Transform transform = gameObject.transform;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!((UnityEngine.Object) child == (UnityEngine.Object) null) && child.gameObject.activeSelf)
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) child.gameObject);
      }
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
        gameObject.gameObject.SetActive(true);
      TowerResuponse.TowerRankParam[] towerRankParamArray = !isSpeedRank ? towerResuponse.TecRank : towerResuponse.SpdRank;
      for (int index1 = 0; index1 < towerRankParamArray.Length; ++index1)
      {
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
        int num1 = index1;
        if ((UnityEngine.Object) listItemEvents != (UnityEngine.Object) null)
        {
          DataSource.Bind<UnitData>(listItemEvents.gameObject, towerRankParamArray[index1].unit, false);
          DataSource.Bind<ViewGuildData>(listItemEvents.gameObject, towerRankParamArray[index1].ViewGuild, false);
          SerializeValueBehaviour component = listItemEvents.GetComponent<SerializeValueBehaviour>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            long num2 = towerRankParamArray[index1].ViewGuild == null ? 0L : (long) towerRankParamArray[index1].ViewGuild.id;
            component.list.SetField(GuildSVB_Key.GUILD_ID, (int) num2);
          }
          listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
          listItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
          listItemEvents.transform.SetParent(gameObject.transform, false);
          listItemEvents.gameObject.SetActive(true);
          for (int index2 = 0; index2 < index1; ++index2)
          {
            if (towerRankParamArray[index2].score == towerRankParamArray[index1].score)
            {
              num1 = index2;
              break;
            }
          }
          this.UpdateValue(listItemEvents, num1, towerRankParamArray[index1], isSpeedRank);
        }
      }
      if (!((UnityEngine.Object) this.Root != (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(this.Root);
    }

    private void UpdateValue(ListItemEvents obj, int num, TowerResuponse.TowerRankParam param, bool isSpeed)
    {
      if ((UnityEngine.Object) this.mSheet == (UnityEngine.Object) null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      TowerParam tower = instance.FindTower(towerResuponse.TowerID);
      DataSource.Bind<TowerResuponse.TowerRankParam>(obj.gameObject, param, false);
      Transform transform1 = obj.transform.Find("body");
      if (!((UnityEngine.Object) transform1 != (UnityEngine.Object) null))
        return;
      Transform transform2 = transform1.transform.Find("ranking");
      if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
      {
        Image component = transform2.GetComponent<Image>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.sprite = num >= this.SHOW_VIP_RANK ? this.mSheet.GetSprite("normal") : this.mSheet.GetSprite(num.ToString());
      }
      Transform transform3 = transform1.transform.Find("rank");
      if ((UnityEngine.Object) transform3 != (UnityEngine.Object) null)
      {
        Text component = transform3.GetComponent<Text>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.text = num >= this.SHOW_VIP_RANK ? (num + 1).ToString() + LocalizedText.Get("sys.TOWER_RANK_LABEL") : string.Empty;
      }
      Transform transform4 = transform1.transform.Find("Text_player");
      if ((UnityEngine.Object) transform4 != (UnityEngine.Object) null)
      {
        Text component = transform4.GetComponent<Text>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.text = param.name;
      }
      Transform transform5 = transform1.transform.Find("player_level");
      if ((UnityEngine.Object) transform5 != (UnityEngine.Object) null)
      {
        Text component = transform5.GetComponent<Text>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.text = LocalizedText.Get("sys.TOWER_RANK_LBL_LV") + param.lv.ToString();
      }
      if (isSpeed)
      {
        Transform transform6 = transform1.transform.Find("speed");
        if ((UnityEngine.Object) transform6 != (UnityEngine.Object) null)
        {
          Transform transform7 = transform6.transform.Find("speed_cnt");
          if ((UnityEngine.Object) transform7 != (UnityEngine.Object) null)
          {
            Text component = transform7.GetComponent<Text>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.text = param.score.ToString();
          }
          transform6.gameObject.SetActive(true);
        }
        string rank = instance.ConvertTowerScoreToRank(tower, param.score, TOWER_SCORE_TYPE.TURN);
        Transform transform8 = transform1.transform.Find("score_img");
        if (!((UnityEngine.Object) transform8 != (UnityEngine.Object) null))
          return;
        Image component1 = transform8.GetComponent<Image>();
        if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null))
          return;
        component1.sprite = this.mSheet.GetSprite(rank);
      }
      else
      {
        Transform transform6 = transform1.transform.Find("tech");
        if ((UnityEngine.Object) transform6 != (UnityEngine.Object) null)
        {
          Transform transform7 = transform6.transform.Find("tech_cnt");
          if ((UnityEngine.Object) transform7 != (UnityEngine.Object) null)
          {
            Text component = transform7.GetComponent<Text>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.text = param.score.ToString();
          }
          transform6.gameObject.SetActive(true);
        }
        string rank = instance.ConvertTowerScoreToRank(tower, param.score, TOWER_SCORE_TYPE.DIED);
        Transform transform8 = transform1.transform.Find("score_img");
        if (!((UnityEngine.Object) transform8 != (UnityEngine.Object) null))
          return;
        Image component1 = transform8.GetComponent<Image>();
        if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null))
          return;
        component1.sprite = this.mSheet.GetSprite(rank);
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
