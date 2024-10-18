// Decompiled with JetBrains decompiler
// Type: SRPG.QuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "演出開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(31, "演出終了", FlowNode.PinTypes.Output, 31)]
  [FlowNode.Pin(40, "ランクアップ演出表示", FlowNode.PinTypes.Output, 40)]
  [FlowNode.Pin(41, "ランクアップ演出終了", FlowNode.PinTypes.Input, 41)]
  [FlowNode.Pin(100, "演出スキップ", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(200, "GvGリプレイ", FlowNode.PinTypes.Output, 200)]
  public class QuestResult : MonoBehaviour, IFlowInterface
  {
    [Description("確認用に使用するユニットのID。ユニットID@ジョブIDで指定する。")]
    public string[] DebugUnitIDs = new string[0];
    public bool[] DebugObjectiveFlags = new bool[3];
    public string DebugMasterAbilityID;
    [Description("ユニットアイコンを梱包する親ゲームオブジェクト")]
    public GameObject UnitList;
    [Description("ユニットアイコンのゲームオブジェクト")]
    public GameObject UnitListItem;
    [Description("ユニット獲得経験値のゲームオブジェクト")]
    public GameObject UnitExpText;
    [Description("入手アイテムのリストになる親ゲームオブジェクト")]
    public GameObject TreasureList;
    [Description("入手アイテムのリストになる親ゲームオブジェクト Contentnode版")]
    public ContentController CcTreasureList;
    [Description("入手アイテムのゲームオブジェクト")]
    public GameObject TreasureListItem;
    [Description("入手ユニットのゲームオブジェクト")]
    public GameObject TreasureListUnit;
    [Description("入手武具のゲームオブジェクト")]
    public GameObject TreasureListArtifact;
    [Description("バトルコインのゲームオブジェクト")]
    public GameObject TreasureListBattleCoin;
    [Description("入手真理念装のゲームオブジェクト")]
    public GameObject TreasureListConceptCard;
    [Description("クリア条件の星を白星に切り替えるトリガーの名前")]
    public string Star_TurnOnTrigger = "on";
    [Description("クリア条件の星が白星にならなかった場合のトリガーの名前")]
    public string Star_KeepOffTrigger = "off";
    [Description("クリア条件の星が白星に既になってる場合のトリガーの名前")]
    public string Star_ClearTrigger = "clear";
    [Description("クリア条件の星にトリガーを送る間隔 (秒数)")]
    public float Star_TriggerInterval = 1f;
    [Description("クリア条件の星で黒星を無視する")]
    public bool Star_SkipDarkStar;
    [Description("入手アイテムを可視状態に切り替えるトリガー")]
    public string Treasure_TurnOnTrigger = "on";
    [Description("入手アイテムを可視状態に切り替える間隔 (秒数)")]
    public float Treasure_TriggerInterval = 1f;
    public GameObject Prefab_NewItemBadge;
    public GameObject Prefab_MasterAbilityPopup;
    public GameObject Prefab_UnitDataUnlockPopup;
    public UnityEngine.UI.Text TextConsumeAp;
    public Color TextConsumeApColor = Color.white;
    [Description("ユニットのレベルアップ時に使用するトリガー。ユニットのゲームオブジェクトにアタッチされたAnimatorへ送られます。")]
    public string Unit_LevelUpTrigger = "levelup";
    [Description("一秒あたりの経験値の増加量")]
    public float ExpGainRate = 100f;
    [Description("経験値増加アニメーションの最長時間。経験値がExpGainRateの速度で増加する時、これで設定した時間を超える時に加算速度を上げる。")]
    public float ExpGainTimeMax = 2f;
    protected List<GameObject> mUnitListItems = new List<GameObject>();
    private List<GameObject> mTreasureListItems = new List<GameObject>();
    [SerializeField]
    private int mRaidOffsetLeft;
    [SerializeField]
    private float mRaidOffsetSpacing;
    [SerializeField]
    private float mRaidOffsetScale;
    private bool mUseLarge;
    public string PreStarAnimationTrigger;
    public string PostStarAnimationTrigger;
    public float PreStarAnimationDelay;
    public float PostStarAnimationDelay;
    public string PreExpAnimationTrigger;
    public string PostExpAnimationTrigger;
    public float PreExpAnimationDelay;
    public float PostExpAnimationDelay;
    public string PreItemAnimationTrigger;
    public string PostItemAnimationTrigger;
    public float PreItemAnimationDelay;
    public float PostItemAnimationDelay;
    protected QuestParam mCurrentQuest;
    private GameObject mMasterAbilityPopup;
    protected QuestResultData mResultData;
    protected string mQuestName;
    public GameObject RetryButton;
    public GameObject OwaruButton;
    public GameObject RaidOwaruButton;
    public Button StarKakuninButton;
    public SRPG_Button TeamUploadButton;
    protected List<UnitData> mUnits = new List<UnitData>();
    [Description("スキップボタン")]
    public Button ResultSkipButton;
    [Description("経験値増加アニメーションスキップの倍速設定")]
    public float ResultSkipSpeedMul = 10f;
    private bool mResultSkipElement;
    private bool mExpAnimationEnd;
    protected bool mContinueStarAnimation = true;
    public bool UseUnitGetEffect;
    public bool NewEffectUse;
    public int[] AcquiredUnitExp;
    [Description("アリーナ：勝ち表示するゲームオブジェクト")]
    public GameObject GoArenaResultWin;
    [Description("アリーナ：負けを表示するゲームオブジェクト")]
    public GameObject GoArenaResultLose;
    public BattleResultMissionDetail MissionDetailSmall;
    public BattleResultMissionDetail MissionDetailLarge;
    private List<GameObject> mObjectiveStars = new List<GameObject>();
    protected BattleResultMissionDetail mMissionDetail;
    protected List<int> mMultiTowerUnitsId = new List<int>();
    public GameObject[] MultiTowerPlayerObj;
    public RectTransform[] MultiTowerPlayerTransform;
    public Texture2D GoldTex;
    public Sprite GoldFrame;
    private List<QuestResultTreasureParam> mCcTreasureList = new List<QuestResultTreasureParam>();
    public Animator MainAnimator;
    [SerializeField]
    [Description("クエストの自動周回開始ボタン")]
    private SRPG_Button AutoRepeatQuestStartBtn;
    [SerializeField]
    [Description("クエストの自動周回開始ボタンマスク")]
    private SRPG_Button AutoRepeatQuestStartBtnMask;
    [SerializeField]
    private SRPG_Button AutoRepeatQuestBestTimeBtn;
    [SerializeField]
    private SRPG_Button AutoRepeatQuestBestTimeBtnMask;
    [SerializeField]
    private UnityEngine.UI.Text AutoRepeatQuestBestTimeText;

    protected void SetExpAnimationEnd() => this.mExpAnimationEnd = true;

    public void OnStarKakuninButtonClick() => this.mContinueStarAnimation = true;

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject(this.mMasterAbilityPopup);
      this.mMasterAbilityPopup = (GameObject) null;
    }

    private void Start()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      GlobalVars.PartyUploadFinished = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitListItem, (UnityEngine.Object) null))
        this.UnitListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListItem, (UnityEngine.Object) null))
        this.TreasureListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListUnit, (UnityEngine.Object) null))
        this.TreasureListUnit.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListArtifact, (UnityEngine.Object) null))
        this.TreasureListArtifact.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListConceptCard, (UnityEngine.Object) null))
        this.TreasureListConceptCard.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_NewItemBadge, (UnityEngine.Object) null) && this.Prefab_NewItemBadge.gameObject.activeInHierarchy)
        this.Prefab_NewItemBadge.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoRepeatQuestStartBtnMask, (UnityEngine.Object) null))
        this.AutoRepeatQuestStartBtnMask.AddListener(new SRPG_Button.ButtonClickEvent(this.OnAutoRepeatQuestMask));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoRepeatQuestBestTimeBtnMask, (UnityEngine.Object) null))
        this.AutoRepeatQuestBestTimeBtnMask.AddListener(new SRPG_Button.ButtonClickEvent(this.OnAutoRepeatQuestMask));
      SceneBattle instance = SceneBattle.Instance;
      GameUtility.DestroyGameObjects(this.mUnitListItems);
      GameUtility.DestroyGameObjects(this.mTreasureListItems);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.ResultData != null)
      {
        this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(instance.Battle.QuestID);
        DataSource.Bind<QuestParam>(((Component) this).gameObject, this.mCurrentQuest);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RetryButton, (UnityEngine.Object) null))
          this.RetryButton.SetActive((long) new TimeSpan(TimeManager.ServerTime.Ticks).Days <= (long) new TimeSpan(player.LoginDate.Ticks).Days && this.mCurrentQuest.type != QuestTypes.Tutorial && !this.mCurrentQuest.IsCharacterQuest() && this.mCurrentQuest.type != QuestTypes.UnitRental);
        if (this.mCurrentQuest.type == QuestTypes.Tutorial && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamUploadButton, (UnityEngine.Object) null))
          ((Selectable) this.TeamUploadButton).interactable = false;
        if (this.mCurrentQuest.type == QuestTypes.Raid || this.mCurrentQuest.type == QuestTypes.GuildRaid)
        {
          this.RetryButton.SetActive(false);
          ((Component) this.TeamUploadButton).gameObject.SetActive(false);
          this.OwaruButton.SetActive(false);
          this.RaidOwaruButton.SetActive(true);
        }
        this.mResultData = instance.ResultData;
        this.mQuestName = this.mCurrentQuest.iname;
        if (instance.IsPlayingArenaQuest)
        {
          this.mResultData.Record.playerexp = (OInt) GlobalVars.ResultArenaBattleResponse.got_pexp;
          this.mResultData.Record.gold = (OInt) GlobalVars.ResultArenaBattleResponse.got_gold;
          this.mResultData.Record.unitexp = (OInt) GlobalVars.ResultArenaBattleResponse.got_uexp;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoArenaResultWin))
            this.GoArenaResultWin.SetActive(this.mResultData.Record.result == BattleCore.QuestResult.Win);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoArenaResultLose))
            this.GoArenaResultLose.SetActive(this.mResultData.Record.result != BattleCore.QuestResult.Win);
          if (instance.IsArenaRankupInfo())
            MonoSingleton<GameManager>.Instance.Player.UpdateArenaRankTrophyStates(GlobalVars.ResultArenaBattleResponse.new_rank, GlobalVars.ResultArenaBattleResponse.new_rank);
          else
            MonoSingleton<GameManager>.Instance.Player.UpdateArenaRankTrophyStates(GlobalVars.ResultArenaBattleResponse.new_rank, MonoSingleton<GameManager>.Instance.Player.ArenaRankBest);
        }
        bool isMultiTower = instance.Battle.IsMultiTower;
        bool isMultiPlay = instance.Battle.IsMultiPlay;
        for (int index = 0; index < instance.Battle.Units.Count; ++index)
        {
          Unit unit = instance.Battle.Units[index];
          if (!unit.IsUnitFlag(EUnitFlag.CreatedBreakObj) && !unit.IsUnitFlag(EUnitFlag.IsDynamicTransform) && (isMultiTower || !isMultiPlay || unit.OwnerPlayerIndex == instance.Battle.MyPlayerIndex) && (player.FindUnitDataByUniqueID(unit.UnitData.UniqueID) != null || isMultiTower && unit.Side == EUnitSide.Player))
          {
            UnitData unitData = new UnitData();
            unitData.Setup(unit.UnitData);
            this.mUnits.Add(unitData);
            this.mMultiTowerUnitsId.Add(unit.OwnerPlayerIndex);
          }
        }
        if (instance.IsArenaRankupInfo())
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 40);
      }
      DataSource.Bind<BattleCore.Record>(((Component) this).gameObject, this.mResultData.Record);
      if (this.mResultData != null)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListItem, (UnityEngine.Object) null))
        {
          Transform parent = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureList, (UnityEngine.Object) null) ? this.TreasureListItem.transform.parent : this.TreasureList.transform;
          List<QuestResult.DropItemData> items = new List<QuestResult.DropItemData>();
          for (int index1 = 0; index1 < this.mResultData.Record.items.Count; ++index1)
          {
            BattleCore.DropItemParam dropItemParam = this.mResultData.Record.items[index1];
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CcTreasureList, (UnityEngine.Object) null) || !dropItemParam.IsItem || dropItemParam.itemParam.type != EItemType.Rune)
            {
              bool flag = false;
              for (int index2 = 0; index2 < items.Count; ++index2)
              {
                if (items[index2].mIsSecret == dropItemParam.mIsSecret)
                {
                  if (items[index2].IsItem)
                  {
                    if (items[index2].itemParam == dropItemParam.itemParam)
                    {
                      items[index2].Gain(1);
                      flag = true;
                      break;
                    }
                  }
                  else if (items[index2].IsConceptCard && items[index2].conceptCardParam == dropItemParam.conceptCardParam)
                  {
                    items[index2].Gain(1);
                    flag = true;
                    break;
                  }
                }
              }
              if (!flag)
              {
                QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
                if (dropItemParam.IsItem)
                {
                  dropItemData.SetupDropItemData(EBattleRewardType.Item, 0L, dropItemParam.itemParam.iname, 1);
                  switch (dropItemParam.itemParam.type)
                  {
                    case EItemType.Unit:
                      string iid = dropItemParam.itemParam.iname;
                      if (player.Units.Find((Predicate<UnitData>) (p => p.UnitParam.iname == iid)) == null)
                      {
                        dropItemData.IsNew = true;
                        break;
                      }
                      break;
                    case EItemType.Rune:
                      break;
                    default:
                      ItemData itemDataByItemParam = player.FindItemDataByItemParam(dropItemParam.itemParam);
                      dropItemData.IsNew = !player.ItemEntryExists(dropItemParam.itemParam.iname) || itemDataByItemParam == null || itemDataByItemParam.IsNew;
                      break;
                  }
                }
                else if (this.mResultData.Record.items[index1].IsConceptCard)
                  dropItemData.SetupDropItemData(EBattleRewardType.ConceptCard, 0L, this.mResultData.Record.items[index1].conceptCardParam.iname, 1);
                dropItemData.mIsSecret = this.mResultData.Record.items[index1].mIsSecret;
                items.Add(dropItemData);
              }
            }
          }
          if (this.mCurrentQuest != null && this.mCurrentQuest.IsVersus)
          {
            VersusCoinParam coinParam = MonoSingleton<GameManager>.Instance.GetVersusCoinParam(this.mCurrentQuest.iname);
            if (coinParam != null)
            {
              QuestResult.DropItemData dropItemData1 = items.Find((Predicate<QuestResult.DropItemData>) (x => x.Param.iname == coinParam.coin_iname));
              if (dropItemData1 != null)
                dropItemData1.Gain((int) this.mResultData.Record.pvpcoin);
              else if ((int) this.mResultData.Record.pvpcoin > 0)
              {
                QuestResult.DropItemData dropItemData2 = new QuestResult.DropItemData();
                dropItemData2.Setup(0L, coinParam.coin_iname, (int) this.mResultData.Record.pvpcoin);
                dropItemData2.mIsSecret = false;
                dropItemData2.IsNew = !player.ItemEntryExists(coinParam.coin_iname);
                items.Add(dropItemData2);
              }
            }
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CcTreasureList, (UnityEngine.Object) null) && this.mResultData.Record.runes_detail.Count > 0)
          {
            this.mResultData.Record.runes_detail.Sort((Comparison<RuneData>) ((x, y) => x.Rarity != y.Rarity ? x.Rarity.CompareTo(y.Rarity) : 0));
            this.mResultData.Record.runes_detail.Reverse();
            foreach (RuneData data in this.mResultData.Record.runes_detail)
            {
              RuneParam runeParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRuneParam(data.iname);
              if (runeParam != null)
              {
                QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
                dropItemData.SetupDropItemData(EBattleRewardType.Item, 0L, runeParam.item_iname, 1);
                dropItemData.SetupDropRuneData(data);
                items.Add(dropItemData);
              }
            }
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CcTreasureList, (UnityEngine.Object) null))
          {
            this.CreateTreasureObject(items);
          }
          else
          {
            this.CreateItemObject(items, parent);
            this.CreateArtifactObjects(parent);
            this.CreateGoldObjects(parent);
          }
        }
        this.ApplyQuestCampaignParams(instance.Battle.QuestCampaignIds);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitListItem, (UnityEngine.Object) null))
        {
          if (instance.Battle.IsMultiTower)
            this.AddExpPlayerMultiTower();
          else
            this.AddExpPlayer();
        }
        GlobalVars.PlayerExpOld.Set(this.mResultData.StartExp);
        GlobalVars.PlayerExpNew.Set(this.mResultData.StartExp + (int) this.mResultData.Record.playerexp);
        GlobalVars.PlayerLevelChanged.Set(player.Lv != PlayerData.CalcLevelFromExp(this.mResultData.StartExp));
        this.RefreshQuestMissionReward();
        if (!string.IsNullOrEmpty(this.Star_ClearTrigger))
        {
          for (int index = 0; index < this.mObjectiveStars.Count; ++index)
          {
            if ((this.mCurrentQuest.clear_missions & 1 << index) != 0)
              GameUtility.SetAnimatorTrigger(this.mObjectiveStars[index], this.Star_ClearTrigger);
          }
        }
        player.OnGoldChange((int) this.mResultData.Record.gold);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StarKakuninButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.StarKakuninButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnStarKakuninButtonClick)));
        this.mContinueStarAnimation = false;
      }
      GlobalVars.CreateAutoMultiTower = false;
      GlobalVars.InvtationSameUser = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultSkipButton, (UnityEngine.Object) null))
        ((Component) this.ResultSkipButton).gameObject.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoRepeatQuestStartBtn, (UnityEngine.Object) null))
        return;
      this.RefreshAutoRepeatQuestBtn();
    }

    public virtual void CreateItemObject(List<QuestResult.DropItemData> items, Transform parent)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        GameObject root = (GameObject) null;
        if (items[index].IsConceptCard)
        {
          root = UnityEngine.Object.Instantiate<GameObject>(this.TreasureListConceptCard);
          root.transform.SetParent(parent, false);
          this.mTreasureListItems.Add(root);
          DataSource.Bind<QuestResult.DropItemData>(root, items[index]);
          if (items[index].mIsSecret)
          {
            ItemIcon component = (ItemIcon) root.GetComponent<DropItemIcon>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.IsSecret = true;
          }
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
        else if (items[index].IsItem)
        {
          root = UnityEngine.Object.Instantiate<GameObject>(items[index].ItemType != EItemType.Unit ? this.TreasureListItem : this.TreasureListUnit);
          root.transform.SetParent(parent, false);
          this.mTreasureListItems.Add(root);
          DataSource.Bind<ItemData>(root, (ItemData) items[index]);
          if (items[index].mIsSecret)
          {
            ItemIcon component = root.GetComponent<ItemIcon>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.IsSecret = true;
          }
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
        else
          DebugUtility.LogError(string.Format("[コードの追加が必要] DropItemData.mBattleRewardType(={0})は不明な列挙です", (object) items[index].BattleRewardType));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_NewItemBadge, (UnityEngine.Object) null) && items[index].IsNew)
        {
          RectTransform transform = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_NewItemBadge).transform as RectTransform;
          ((Component) transform).gameObject.SetActive(true);
          transform.anchoredPosition = Vector2.zero;
          ((Transform) transform).SetParent(root.transform, false);
        }
      }
    }

    private void CreateArtifactObjects(Transform parent)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts;
      OrderedDictionary orderedDictionary = new OrderedDictionary();
      foreach (ArtifactParam artifact in this.mResultData.Record.artifacts)
      {
        if (orderedDictionary.Contains((object) artifact.iname))
        {
          int num = (int) orderedDictionary[(object) artifact.iname];
          orderedDictionary[(object) artifact.iname] = (object) (num + 1);
        }
        else
          orderedDictionary.Add((object) artifact.iname, (object) 1);
      }
      foreach (DictionaryEntry dictionaryEntry in orderedDictionary)
      {
        DictionaryEntry artiAndNum = dictionaryEntry;
        GameObject root = UnityEngine.Object.Instantiate<GameObject>(this.TreasureListArtifact);
        root.transform.SetParent(parent, false);
        ArtifactParam data = artifacts.FirstOrDefault<ArtifactParam>((Func<ArtifactParam, bool>) (arti => arti.iname == (string) artiAndNum.Key));
        this.mTreasureListItems.Add(root);
        DataSource.Bind<ArtifactParam>(root, data);
        DataSource.Bind<int>(root, (int) artiAndNum.Value);
        root.SetActive(true);
        GameParameter.UpdateAll(root);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_NewItemBadge, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.Player.GetArtifactNumByRarity(data.iname, data.rareini) <= 0)
        {
          RectTransform transform = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_NewItemBadge).transform as RectTransform;
          ((Component) transform).gameObject.SetActive(true);
          transform.anchoredPosition = Vector2.zero;
          ((Transform) transform).SetParent(root.transform, false);
        }
      }
    }

    private void CreateGoldObjects(Transform parent)
    {
      if (this.mCurrentQuest != null && !this.mCurrentQuest.IsVersus)
        return;
      if ((int) this.mResultData.Record.gold <= 0)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TreasureListItem);
      gameObject.transform.SetParent(parent, false);
      this.mTreasureListItems.Add(gameObject);
      gameObject.SetActive(true);
      Transform transform1 = gameObject.transform.Find("BODY/frame");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform1, (UnityEngine.Object) null))
      {
        Image_Transparent component = ((Component) transform1).GetComponent<Image_Transparent>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GoldFrame, (UnityEngine.Object) null))
          component.sprite = this.GoldFrame;
      }
      Transform transform2 = gameObject.transform.Find("BODY/itemicon");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
      {
        RawImage_Transparent component = ((Component) transform2).GetComponent<RawImage_Transparent>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GoldTex, (UnityEngine.Object) null))
          component.texture = (Texture) this.GoldTex;
      }
      Transform transform3 = gameObject.transform.Find("BODY/amount/Text_amount");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) transform3, (UnityEngine.Object) null))
        return;
      BitmapText component1 = ((Component) transform3).GetComponent<BitmapText>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      ((UnityEngine.UI.Text) component1).text = CurrencyBitmapText.CreateFormatedText(this.mResultData.Record.gold.ToString());
    }

    public void CreateTreasureObject(List<QuestResult.DropItemData> items)
    {
      this.CcTreasureList.Release();
      ContentSource source = new ContentSource();
      this.mCcTreasureList.Clear();
      foreach (QuestResult.DropItemData dropItemData in items)
      {
        QuestResultTreasureParam resultTreasureParam = new QuestResultTreasureParam();
        resultTreasureParam.ItemData = dropItemData;
        resultTreasureParam.Initialize(source);
        this.mCcTreasureList.Add(resultTreasureParam);
      }
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts;
      OrderedDictionary orderedDictionary = new OrderedDictionary();
      foreach (ArtifactParam artifact in this.mResultData.Record.artifacts)
      {
        if (orderedDictionary.Contains((object) artifact.iname))
        {
          int num = (int) orderedDictionary[(object) artifact.iname];
          orderedDictionary[(object) artifact.iname] = (object) (num + 1);
        }
        else
          orderedDictionary.Add((object) artifact.iname, (object) 1);
      }
      foreach (DictionaryEntry dictionaryEntry in orderedDictionary)
      {
        DictionaryEntry artiAndNum = dictionaryEntry;
        ArtifactParam artifactParam = artifacts.FirstOrDefault<ArtifactParam>((Func<ArtifactParam, bool>) (arti => arti.iname == (string) artiAndNum.Key));
        QuestResultTreasureParam resultTreasureParam = new QuestResultTreasureParam();
        resultTreasureParam.ArtfactParam = artifactParam;
        resultTreasureParam.ArtfactNum = (int) artiAndNum.Value;
        resultTreasureParam.Initialize(source);
        this.mCcTreasureList.Add(resultTreasureParam);
      }
      if ((int) this.mResultData.Record.gold > 0 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GoldFrame, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GoldTex, (UnityEngine.Object) null))
      {
        QuestResultTreasureParam resultTreasureParam = new QuestResultTreasureParam();
        resultTreasureParam.GoldNum = (int) this.mResultData.Record.gold;
        resultTreasureParam.GoldFrame = this.GoldFrame;
        resultTreasureParam.GoldTex = this.GoldTex;
        resultTreasureParam.Initialize(source);
        this.mCcTreasureList.Add(resultTreasureParam);
      }
      source.SetTable((ContentSource.Param[]) this.mCcTreasureList.ToArray());
      this.CcTreasureList.Initialize(source, Vector2.zero);
    }

    public virtual void AddExpPlayer()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitExpText, (UnityEngine.Object) null))
        this.UnitExpText.AddComponent<QuestResult.CampaignPartyExp>();
      Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitList, (UnityEngine.Object) null) ? this.UnitListItem.transform.parent : this.UnitList.transform;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitList, (UnityEngine.Object) null) && (this.mCurrentQuest.type == QuestTypes.Raid || this.mCurrentQuest.type == QuestTypes.GuildRaid))
      {
        HorizontalLayoutGroup component = this.UnitList.GetComponent<HorizontalLayoutGroup>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          ((LayoutGroup) component).padding.left = this.mRaidOffsetLeft;
          ((HorizontalOrVerticalLayoutGroup) component).spacing = this.mRaidOffsetSpacing;
          this.UnitList.transform.localScale = new Vector3(this.mRaidOffsetScale, this.mRaidOffsetScale, 1f);
        }
      }
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitListItem);
        gameObject.transform.SetParent(transform, false);
        QuestResult.CampaignPartyExp componentInChildren = gameObject.GetComponentInChildren<QuestResult.CampaignPartyExp>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          componentInChildren.Exp = this.AcquiredUnitExp[index];
        ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Setup(this.mUnits[index].MainConceptCard);
        this.mUnitListItems.Add(gameObject);
        DataSource.Bind<UnitData>(gameObject, this.mUnits[index]);
        gameObject.SetActive(true);
      }
    }

    public void AddExpPlayerMultiTower()
    {
      if (this.MultiTowerPlayerObj == null || this.MultiTowerPlayerTransform == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitExpText, (UnityEngine.Object) null))
        this.UnitExpText.AddComponent<QuestResult.CampaignPartyExp>();
      List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
      int length = SceneBattle.Instance.MultiPlayerCount + 1;
      for (int index = 0; index < this.MultiTowerPlayerObj.Length; ++index)
      {
        if (length > index)
          DataSource.Bind<JSON_MyPhotonPlayerParam>(this.MultiTowerPlayerObj[index], myPlayersStarted[index]);
        else
          this.MultiTowerPlayerObj[index].SetActive(false);
      }
      List<UnitData>[] unitDataListArray = new List<UnitData>[length];
      for (int index = 0; index < unitDataListArray.Length; ++index)
        unitDataListArray[index] = new List<UnitData>();
      for (int index = 0; index < this.mUnits.Count; ++index)
        unitDataListArray[this.mMultiTowerUnitsId[index] - 1].Add(this.mUnits[index]);
      for (int index1 = 0; index1 < length; ++index1)
      {
        Transform transform = (Transform) this.MultiTowerPlayerTransform[index1];
        for (int index2 = 0; index2 < unitDataListArray[index1].Count; ++index2)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitListItem);
          gameObject.transform.SetParent(transform, false);
          QuestResult.CampaignPartyExp componentInChildren = gameObject.GetComponentInChildren<QuestResult.CampaignPartyExp>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
            componentInChildren.Exp = this.AcquiredUnitExp[index2];
          this.mUnitListItems.Add(gameObject);
          DataSource.Bind<UnitData>(gameObject, unitDataListArray[index1][index2]);
          gameObject.SetActive(true);
        }
      }
    }

    public virtual void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.StartCoroutine(this.PlayAnimationAsync());
          break;
        case 100:
          this.mResultSkipElement = true;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultSkipButton, (UnityEngine.Object) null))
            break;
          ((Component) this.ResultSkipButton).gameObject.SetActive(false);
          break;
      }
    }

    public void TriggerAnimation(string trigger)
    {
      if (string.IsNullOrEmpty(trigger) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MainAnimator, (UnityEngine.Object) null))
        return;
      this.MainAnimator.SetTrigger(trigger);
    }

    [DebuggerHidden]
    public virtual IEnumerator PlayAnimationAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CPlayAnimationAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    public virtual IEnumerator AddExp()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CAddExp\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    protected IEnumerator RecvExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CRecvExpAnimation\u003Ec__Iterator2()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    protected IEnumerator RecvTrustAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CRecvTrustAnimation\u003Ec__Iterator3()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    public virtual IEnumerator StartTreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CStartTreasureAnimation\u003Ec__Iterator4()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    protected virtual IEnumerator TreasureAnimation(List<GameObject> ListItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CTreasureAnimation\u003Ec__Iterator5()
      {
        ListItems = ListItems,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    protected IEnumerator ObjectiveAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CObjectiveAnimation\u003Ec__Iterator6()
      {
        \u0024this = this
      };
    }

    private void ApplyQuestCampaignParams(string[] campaignIds)
    {
      this.AcquiredUnitExp = new int[this.mUnits.Count];
      if (campaignIds != null)
      {
        QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCampaigns(campaignIds);
        List<UnitData> mUnits = this.mUnits;
        float[] numArray = new float[mUnits.Count];
        float num1 = 1f;
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = 1f;
        foreach (QuestCampaignData questCampaignData in questCampaigns)
        {
          QuestCampaignData data = questCampaignData;
          if (data.type == QuestCampaignValueTypes.ExpUnit)
          {
            if (string.IsNullOrEmpty(data.unit))
            {
              num1 = data.GetRate();
            }
            else
            {
              int index = mUnits.FindIndex((Predicate<UnitData>) (value => value.UnitParam.iname == data.unit));
              if (index >= 0)
                numArray[index] = data.GetRate();
            }
          }
          else if (data.type == QuestCampaignValueTypes.ExpPlayer)
            this.mResultData.Record.playerexp = (OInt) Mathf.RoundToInt((float) (int) this.mResultData.Record.playerexp * data.GetRate());
          else if (data.type == QuestCampaignValueTypes.Ap && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextConsumeAp, (UnityEngine.Object) null))
            ((Graphic) this.TextConsumeAp).color = this.TextConsumeApColor;
        }
        int unitexp = (int) this.mResultData.Record.unitexp;
        for (int index = 0; index < numArray.Length; ++index)
        {
          float num2 = 1f;
          if ((double) num1 != 1.0 && (double) numArray[index] != 1.0)
            num2 = num1 + numArray[index];
          else if ((double) num1 != 1.0)
            num2 = num1;
          else if ((double) numArray[index] != 1.0)
            num2 = numArray[index];
          this.AcquiredUnitExp[index] = Mathf.RoundToInt((float) unitexp * num2);
        }
      }
      else
      {
        for (int index = 0; index < this.AcquiredUnitExp.Length; ++index)
          this.AcquiredUnitExp[index] = (int) this.mResultData.Record.unitexp;
      }
    }

    private void RefreshQuestMissionReward()
    {
      if (this.mCurrentQuest == null)
        return;
      this.mUseLarge = this.mCurrentQuest.bonusObjective != null && this.mCurrentQuest.bonusObjective.Length > 3;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MissionDetailLarge, (UnityEngine.Object) null))
        ((Component) this.MissionDetailLarge).gameObject.SetActive(this.mUseLarge);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MissionDetailSmall, (UnityEngine.Object) null))
        ((Component) this.MissionDetailSmall).gameObject.SetActive(!this.mUseLarge);
      BattleResultMissionDetail resultMissionDetail = !this.mUseLarge ? this.MissionDetailSmall : this.MissionDetailLarge;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) resultMissionDetail, (UnityEngine.Object) null))
        return;
      this.mMissionDetail = ((Component) resultMissionDetail).GetComponent<BattleResultMissionDetail>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMissionDetail, (UnityEngine.Object) null))
        return;
      this.mObjectiveStars = this.mMissionDetail.GetObjectiveStars();
    }

    public void OnClickMultiTowerRetry()
    {
      GlobalVars.CreateAutoMultiTower = true;
      UIUtility.ConfirmBoxTitle((string) null, LocalizedText.Get("sys.MULTI_TOWER_SAMEUSER"), (UIUtility.DialogResultEvent) (g =>
      {
        FlowNode_Variable.Set("MultiPlayPasscode", "1");
        GlobalVars.InvtationSameUser = true;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FINISH_RESULT");
      }), (UIUtility.DialogResultEvent) (g =>
      {
        FlowNode_Variable.Set("MultiPlayPasscode", "0");
        GlobalVars.InvtationSameUser = false;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FINISH_RESULT");
      }));
    }

    public void OnClickMultiTowerNextRetry()
    {
      ++GlobalVars.SelectedMultiTowerFloor;
      this.OnClickMultiTowerRetry();
    }

    public void OnPartyUploadFinished()
    {
      if (!GlobalVars.PartyUploadFinished || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamUploadButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.TeamUploadButton).interactable = false;
    }

    public void OnGvGReplay()
    {
      GlobalVars.GvGBattleReplay.Set(true);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }

    private bool RefreshAutoRepeatQuestBtn()
    {
      GameUtility.SetGameObjectActive((Component) this.AutoRepeatQuestStartBtn, false);
      GameUtility.SetGameObjectActive((Component) this.AutoRepeatQuestBestTimeBtn, false);
      SRPG_Button srpgButton1 = this.AutoRepeatQuestStartBtn;
      SRPG_Button srpgButton2 = this.AutoRepeatQuestStartBtnMask;
      if (this.mCurrentQuest != null && this.mCurrentQuest.best_clear_time > 0)
      {
        srpgButton1 = this.AutoRepeatQuestBestTimeBtn;
        srpgButton2 = this.AutoRepeatQuestBestTimeBtnMask;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoRepeatQuestBestTimeText, (UnityEngine.Object) null))
          this.AutoRepeatQuestBestTimeText.text = this.mCurrentQuest.GetClearBestTime();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) srpgButton1, (UnityEngine.Object) null))
      {
        if (this.mCurrentQuest == null)
          return false;
        bool autoRepeatQuestFlag = this.mCurrentQuest.IsAutoRepeatQuestFlag;
        bool flag = this.mCurrentQuest != null && this.mCurrentQuest.CheckEnableChallange() && MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.AutoRepeatQuest);
        if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress != null)
          flag = flag && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.State == AutoRepeatQuestData.eState.IDLE;
        ((Component) srpgButton1).gameObject.SetActive(autoRepeatQuestFlag);
        ((Selectable) srpgButton1).interactable = flag;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) srpgButton2, (UnityEngine.Object) null))
          ((Component) srpgButton2).gameObject.SetActive(!flag);
      }
      return true;
    }

    private void OnAutoRepeatQuestMask(SRPG_Button button)
    {
      if (this.mCurrentQuest == null)
      {
        DebugUtility.LogError("クエストが設定されていません.");
      }
      else
      {
        string title = LocalizedText.Get("sys.AUTO_REPEAT_QUEST_TITLE");
        StringBuilder stringBuilder = GameUtility.GetStringBuilder();
        if (!this.mCurrentQuest.CheckEnableChallange())
        {
          if (string.IsNullOrEmpty(stringBuilder.ToString()))
            stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_START"));
          stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_CHALLENGE_LIMIT"));
        }
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(stringBuilder.ToString()))
          return;
        stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_END"));
        UIUtility.SystemMessage(title, stringBuilder.ToString(), (UIUtility.DialogResultEvent) null);
      }
    }

    public class DropItemData : ItemData
    {
      public bool mIsSecret;
      private EBattleRewardType mBattleRewardType = EBattleRewardType.Item;
      private ConceptCardData mConceptCardData;
      private RuneData mRuneData;

      public EBattleRewardType BattleRewardType => this.mBattleRewardType;

      public bool IsItem => this.mBattleRewardType == EBattleRewardType.Item;

      public bool IsConceptCard => this.mBattleRewardType == EBattleRewardType.ConceptCard;

      public ItemParam itemParam => this.Param;

      public ItemData itemData => (ItemData) this;

      public ConceptCardParam conceptCardParam
      {
        get
        {
          return this.mConceptCardData != null ? this.mConceptCardData.Param : (ConceptCardParam) null;
        }
      }

      public ConceptCardData conceptCardData => this.mConceptCardData;

      public RuneData runeData => this.mRuneData;

      public void SetupDropItemData(EBattleRewardType rewardType, long iid, string iname, int num)
      {
        this.mBattleRewardType = rewardType;
        if (rewardType == EBattleRewardType.Item)
        {
          this.Setup(iid, iname, num);
        }
        else
        {
          if (rewardType != EBattleRewardType.ConceptCard)
            return;
          this.SetupConceptCard(iname, num);
        }
      }

      private void SetupConceptCard(string iname, int num)
      {
        this.mBattleRewardType = EBattleRewardType.ConceptCard;
        this.mConceptCardData = ConceptCardData.CreateConceptCardDataForDisplay(iname);
        this.mNum = num;
      }

      public void SetupDropRuneData(RuneData data) => this.mRuneData = data;
    }

    public class TrustAnimWork
    {
      public UnitData beforeUnit;
      public UnitData afterUnit;
      public ConceptCardIconBattleResult cardIcon;

      public TrustAnimWork(UnitData before, UnitData after, ConceptCardIconBattleResult card)
      {
        this.beforeUnit = before;
        this.afterUnit = after;
        this.cardIcon = card;
      }
    }

    private class CampaignPartyExp : MonoBehaviour
    {
      public int Exp;

      private void Start()
      {
        UnityEngine.UI.Text component = ((Component) this).gameObject.GetComponent<UnityEngine.UI.Text>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.text = this.Exp.ToString();
      }
    }
  }
}
