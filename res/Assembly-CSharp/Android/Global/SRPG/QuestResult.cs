﻿// Decompiled with JetBrains decompiler
// Type: SRPG.QuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "演出開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(31, "演出終了", FlowNode.PinTypes.Output, 31)]
  [FlowNode.Pin(40, "ランクアップ演出表示", FlowNode.PinTypes.Output, 40)]
  [FlowNode.Pin(41, "ランクアップ演出終了", FlowNode.PinTypes.Input, 41)]
  [FlowNode.Pin(100, "演出スキップ", FlowNode.PinTypes.Input, 100)]
  public class QuestResult : MonoBehaviour, IFlowInterface
  {
    [Description("確認用に使用するユニットのID。ユニットID@ジョブIDで指定する。")]
    public string[] DebugUnitIDs = new string[0];
    public bool[] DebugObjectiveFlags = new bool[3];
    [Description("クリア条件の星を白星に切り替えるトリガーの名前")]
    public string Star_TurnOnTrigger = "on";
    [Description("クリア条件の星が白星にならなかった場合のトリガーの名前")]
    public string Star_KeepOffTrigger = "off";
    [Description("クリア条件の星が白星に既になってる場合のトリガーの名前")]
    public string Star_ClearTrigger = "clear";
    [Description("クリア条件の星にトリガーを送る間隔 (秒数)")]
    public float Star_TriggerInterval = 1f;
    [Description("入手アイテムを可視状態に切り替えるトリガー")]
    public string Treasure_TurnOnTrigger = "on";
    [Description("入手アイテムを可視状態に切り替える間隔 (秒数)")]
    public float Treasure_TriggerInterval = 1f;
    public Color TextConsumeApColor = Color.white;
    [Description("ユニットのレベルアップ時に使用するトリガー。ユニットのゲームオブジェクトにアタッチされたAnimatorへ送られます。")]
    public string Unit_LevelUpTrigger = "levelup";
    [Description("一秒あたりの経験値の増加量")]
    public float ExpGainRate = 100f;
    [Description("経験値増加アニメーションの最長時間。経験値がExpGainRateの速度で増加する時、これで設定した時間を超える時に加算速度を上げる。")]
    public float ExpGainTimeMax = 2f;
    protected List<GameObject> mUnitListItems = new List<GameObject>();
    private List<GameObject> mTreasureListItems = new List<GameObject>();
    protected List<UnitData> mUnits = new List<UnitData>();
    [Description("経験値増加アニメーションスキップの倍速設定")]
    public float ResultSkipSpeedMul = 10f;
    private bool mContinueStarAnimation = true;
    private List<GameObject> mObjectiveStars = new List<GameObject>();
    protected List<int> mMultiTowerUnitsId = new List<int>();
    public string DebugMasterAbilityID;
    [Description("ユニットアイコンを梱包する親ゲームオブジェクト")]
    public GameObject UnitList;
    [Description("ユニットアイコンのゲームオブジェクト")]
    public GameObject UnitListItem;
    [Description("ユニット獲得経験値のゲームオブジェクト")]
    public GameObject UnitExpText;
    [Description("入手アイテムのリストになる親ゲームオブジェクト")]
    public GameObject TreasureList;
    [Description("入手アイテムのゲームオブジェクト")]
    public GameObject TreasureListItem;
    [Description("入手ユニットのゲームオブジェクト")]
    public GameObject TreasureListUnit;
    [Description("入手武具のゲームオブジェクト")]
    public GameObject TreasureListArtifact;
    [Description("クリア条件の星で黒星を無視する")]
    public bool Star_SkipDarkStar;
    public GameObject Prefab_NewItemBadge;
    public GameObject Prefab_MasterAbilityPopup;
    public GameObject Prefab_UnitDataUnlockPopup;
    public Text TextConsumeAp;
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
    private QuestResultData mResultData;
    protected string mQuestName;
    public GameObject RetryButton;
    public Button StarKakuninButton;
    public SRPG_Button TeamUploadButton;
    [Description("スキップボタン")]
    public Button ResultSkipButton;
    private bool mResultSkipElement;
    private bool mExpAnimationEnd;
    public bool UseUnitGetEffect;
    public bool NewEffectUse;
    public int[] AcquiredUnitExp;
    [Description("アリーナ：勝ち表示するゲームオブジェクト")]
    public GameObject GoArenaResultWin;
    [Description("アリーナ：負けを表示するゲームオブジェクト")]
    public GameObject GoArenaResultLose;
    public BattleResultMissionDetail MissionDetailSmall;
    public BattleResultMissionDetail MissionDetailLarge;
    private BattleResultMissionDetail mMissionDetail;
    public GameObject[] MultiTowerPlayerObj;
    public RectTransform[] MultiTowerPlayerTransform;
    public Animator MainAnimator;

    public void OnStarKakuninButtonClick()
    {
      this.mContinueStarAnimation = true;
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject(this.mMasterAbilityPopup);
      this.mMasterAbilityPopup = (GameObject) null;
    }

    private void Start()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      GlobalVars.PartyUploadFinished = false;
      if ((UnityEngine.Object) this.UnitListItem != (UnityEngine.Object) null)
        this.UnitListItem.SetActive(false);
      if ((UnityEngine.Object) this.TreasureListItem != (UnityEngine.Object) null)
        this.TreasureListItem.SetActive(false);
      if ((UnityEngine.Object) this.TreasureListUnit != (UnityEngine.Object) null)
        this.TreasureListUnit.SetActive(false);
      if ((UnityEngine.Object) this.TreasureListArtifact != (UnityEngine.Object) null)
        this.TreasureListArtifact.SetActive(false);
      if ((UnityEngine.Object) this.Prefab_NewItemBadge != (UnityEngine.Object) null && this.Prefab_NewItemBadge.gameObject.activeInHierarchy)
        this.Prefab_NewItemBadge.SetActive(false);
      SceneBattle instance = SceneBattle.Instance;
      GameUtility.DestroyGameObjects(this.mUnitListItems);
      GameUtility.DestroyGameObjects(this.mTreasureListItems);
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null && instance.ResultData != null)
      {
        this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(instance.Battle.QuestID);
        DataSource.Bind<QuestParam>(this.gameObject, this.mCurrentQuest);
        if ((UnityEngine.Object) this.RetryButton != (UnityEngine.Object) null)
        {
          this.RetryButton.SetActive((long) new TimeSpan(TimeManager.ServerTime.Ticks).Days <= (long) new TimeSpan(player.LoginDate.Ticks).Days && this.mCurrentQuest.type != QuestTypes.Tutorial && !this.mCurrentQuest.IsCharacterQuest());
          if (this.mCurrentQuest.GetChallangeCount() >= this.mCurrentQuest.GetChallangeLimit() && this.mCurrentQuest.GetChallangeLimit() > 0)
            this.RetryButton.GetComponent<Button>().interactable = false;
        }
        if (this.mCurrentQuest.type == QuestTypes.Tutorial && (UnityEngine.Object) this.TeamUploadButton != (UnityEngine.Object) null)
          this.TeamUploadButton.interactable = false;
        this.mResultData = instance.ResultData;
        this.mQuestName = this.mCurrentQuest.iname;
        if (instance.IsPlayingArenaQuest)
        {
          this.mResultData.Record.playerexp = (OInt) GlobalVars.ResultArenaBattleResponse.got_pexp;
          this.mResultData.Record.gold = (OInt) GlobalVars.ResultArenaBattleResponse.got_gold;
          this.mResultData.Record.unitexp = (OInt) GlobalVars.ResultArenaBattleResponse.got_uexp;
          if ((bool) ((UnityEngine.Object) this.GoArenaResultWin))
            this.GoArenaResultWin.SetActive(this.mResultData.Record.result == BattleCore.QuestResult.Win);
          if ((bool) ((UnityEngine.Object) this.GoArenaResultLose))
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
          if ((isMultiTower || !isMultiPlay || unit.OwnerPlayerIndex == instance.Battle.MyPlayerIndex) && (player.FindUnitDataByUniqueID(unit.UnitData.UniqueID) != null || isMultiTower && unit.Side == EUnitSide.Player))
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
      DataSource.Bind<BattleCore.Record>(this.gameObject, this.mResultData.Record);
      if (this.mResultData != null)
      {
        if ((UnityEngine.Object) this.TreasureListItem != (UnityEngine.Object) null)
        {
          Transform parent = !((UnityEngine.Object) this.TreasureList != (UnityEngine.Object) null) ? this.TreasureListItem.transform.parent : this.TreasureList.transform;
          List<QuestResult.DropItemData> items = new List<QuestResult.DropItemData>();
          for (int index1 = 0; index1 < this.mResultData.Record.items.Count; ++index1)
          {
            bool flag = false;
            for (int index2 = 0; index2 < items.Count; ++index2)
            {
              if (items[index2].Param == this.mResultData.Record.items[index1].mItemParam && items[index2].mIsSecret == this.mResultData.Record.items[index1].mIsSecret)
              {
                items[index2].Gain(1);
                flag = true;
                break;
              }
            }
            if (!flag)
            {
              ItemData itemDataByItemParam = player.FindItemDataByItemParam(this.mResultData.Record.items[index1].mItemParam);
              QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
              dropItemData.Setup(0L, this.mResultData.Record.items[index1].mItemParam.iname, 1);
              dropItemData.mIsSecret = this.mResultData.Record.items[index1].mIsSecret;
              if (this.mResultData.Record.items[index1].mItemParam.type != EItemType.Unit)
              {
                dropItemData.IsNew = !player.ItemEntryExists(this.mResultData.Record.items[index1].mItemParam.iname) || (itemDataByItemParam == null || itemDataByItemParam.IsNew);
              }
              else
              {
                string iid = this.mResultData.Record.items[index1].mItemParam.iname;
                dropItemData.IsNew = this.mResultData.GetUnits.Params.FindIndex((Predicate<UnitGetParam.Set>) (p =>
                {
                  if (!p.IsConvert)
                    return p.ItemId == iid;
                  return false;
                })) != -1;
              }
              items.Add(dropItemData);
            }
          }
          this.CreateItemObject(items, parent);
          this.CreateArtifactObjects(parent);
        }
        this.ApplyQuestCampaignParams(instance.Battle.QuestCampaignIds);
        if ((UnityEngine.Object) this.UnitListItem != (UnityEngine.Object) null)
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
        if ((int) this.mResultData.Record.gold > 0)
          AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.Zeni, (long) (int) this.mResultData.Record.gold, "Quests", (string) null);
      }
      if ((UnityEngine.Object) this.StarKakuninButton != (UnityEngine.Object) null)
      {
        this.StarKakuninButton.onClick.AddListener(new UnityAction(this.OnStarKakuninButtonClick));
        this.mContinueStarAnimation = false;
      }
      GlobalVars.CreateAutoMultiTower = false;
      GlobalVars.InvtationSameUser = false;
      if (!((UnityEngine.Object) this.ResultSkipButton != (UnityEngine.Object) null))
        return;
      this.ResultSkipButton.gameObject.SetActive(false);
    }

    public virtual void CreateItemObject(List<QuestResult.DropItemData> items, Transform parent)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        GameObject root = UnityEngine.Object.Instantiate<GameObject>(items[index].ItemType != EItemType.Unit ? this.TreasureListItem : this.TreasureListUnit);
        root.transform.SetParent(parent, false);
        this.mTreasureListItems.Add(root);
        DataSource.Bind<ItemData>(root, (ItemData) items[index]);
        if (items[index].mIsSecret)
        {
          ItemIcon component = root.GetComponent<ItemIcon>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.IsSecret = true;
        }
        root.SetActive(true);
        GameParameter.UpdateAll(root);
        if ((UnityEngine.Object) this.Prefab_NewItemBadge != (UnityEngine.Object) null && items[index].IsNew)
        {
          RectTransform transform = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_NewItemBadge).transform as RectTransform;
          transform.gameObject.SetActive(true);
          transform.anchoredPosition = Vector2.zero;
          transform.SetParent(root.transform, false);
        }
        ItemData itemData = (ItemData) items[index];
        if (itemData.ItemType == EItemType.Ticket)
          AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.SummonTicket, (long) itemData.Num, "Quests", itemData.ItemID);
        else
          AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.Item, (long) itemData.Num, "Quests", itemData.ItemID);
      }
    }

    private void CreateArtifactObjects(Transform parent)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts;
      OrderedDictionary orderedDictionary = new OrderedDictionary();
      using (List<ArtifactParam>.Enumerator enumerator = this.mResultData.Record.artifacts.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ArtifactParam current = enumerator.Current;
          if (orderedDictionary.Contains((object) current.iname))
          {
            int num = (int) orderedDictionary[(object) current.iname];
            orderedDictionary[(object) current.iname] = (object) (num + 1);
          }
          else
            orderedDictionary.Add((object) current.iname, (object) 1);
        }
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
        if ((UnityEngine.Object) this.Prefab_NewItemBadge != (UnityEngine.Object) null && MonoSingleton<GameManager>.Instance.Player.GetArtifactNumByRarity(data.iname, data.rareini) <= 0)
        {
          RectTransform transform = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_NewItemBadge).transform as RectTransform;
          transform.gameObject.SetActive(true);
          transform.anchoredPosition = Vector2.zero;
          transform.SetParent(root.transform, false);
        }
      }
    }

    public virtual void AddExpPlayer()
    {
      if ((UnityEngine.Object) this.UnitExpText != (UnityEngine.Object) null)
        this.UnitExpText.AddComponent<QuestResult.CampaignPartyExp>();
      Transform parent = !((UnityEngine.Object) this.UnitList != (UnityEngine.Object) null) ? this.UnitListItem.transform.parent : this.UnitList.transform;
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitListItem);
        gameObject.transform.SetParent(parent, false);
        QuestResult.CampaignPartyExp componentInChildren = gameObject.GetComponentInChildren<QuestResult.CampaignPartyExp>();
        if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
          componentInChildren.Exp = this.AcquiredUnitExp[index];
        this.mUnitListItems.Add(gameObject);
        DataSource.Bind<UnitData>(gameObject, this.mUnits[index]);
        gameObject.SetActive(true);
      }
    }

    public void AddExpPlayerMultiTower()
    {
      if (this.MultiTowerPlayerObj == null || this.MultiTowerPlayerTransform == null)
        return;
      if ((UnityEngine.Object) this.UnitExpText != (UnityEngine.Object) null)
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
        Transform parent = (Transform) this.MultiTowerPlayerTransform[index1];
        for (int index2 = 0; index2 < unitDataListArray[index1].Count; ++index2)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitListItem);
          gameObject.transform.SetParent(parent, false);
          QuestResult.CampaignPartyExp componentInChildren = gameObject.GetComponentInChildren<QuestResult.CampaignPartyExp>();
          if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
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
          if (!((UnityEngine.Object) this.ResultSkipButton != (UnityEngine.Object) null))
            break;
          this.ResultSkipButton.gameObject.SetActive(false);
          break;
      }
    }

    public void TriggerAnimation(string trigger)
    {
      if (string.IsNullOrEmpty(trigger) || !((UnityEngine.Object) this.MainAnimator != (UnityEngine.Object) null))
        return;
      this.MainAnimator.SetTrigger(trigger);
    }

    [DebuggerHidden]
    public virtual IEnumerator PlayAnimationAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CPlayAnimationAsync\u003Ec__Iterator117() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    public virtual IEnumerator AddExp()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CAddExp\u003Ec__Iterator118() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    protected IEnumerator RecvExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CRecvExpAnimation\u003Ec__Iterator119() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    public virtual IEnumerator StartTreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CStartTreasureAnimation\u003Ec__Iterator11A() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    protected virtual IEnumerator TreasureAnimation(List<GameObject> ListItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CTreasureAnimation\u003Ec__Iterator11B() { ListItems = ListItems, \u003C\u0024\u003EListItems = ListItems, \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator ObjectiveAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CObjectiveAnimation\u003Ec__Iterator11C() { \u003C\u003Ef__this = this };
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
          else if (data.type == QuestCampaignValueTypes.Ap && (UnityEngine.Object) this.TextConsumeAp != (UnityEngine.Object) null)
            this.TextConsumeAp.color = this.TextConsumeApColor;
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
      if ((UnityEngine.Object) this.MissionDetailLarge != (UnityEngine.Object) null)
        this.MissionDetailLarge.gameObject.SetActive(this.mUseLarge);
      if ((UnityEngine.Object) this.MissionDetailSmall != (UnityEngine.Object) null)
        this.MissionDetailSmall.gameObject.SetActive(!this.mUseLarge);
      BattleResultMissionDetail resultMissionDetail = !this.mUseLarge ? this.MissionDetailSmall : this.MissionDetailLarge;
      if (!((UnityEngine.Object) resultMissionDetail != (UnityEngine.Object) null))
        return;
      this.mMissionDetail = resultMissionDetail.GetComponent<BattleResultMissionDetail>();
      if (!((UnityEngine.Object) this.mMissionDetail != (UnityEngine.Object) null))
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
      }), (GameObject) null, false, -1, (string) null, (string) null);
    }

    public void OnClickMultiTowerNextRetry()
    {
      ++GlobalVars.SelectedMultiTowerFloor;
      this.OnClickMultiTowerRetry();
    }

    public void OnPartyUploadFinished()
    {
      if (!GlobalVars.PartyUploadFinished || !((UnityEngine.Object) this.TeamUploadButton != (UnityEngine.Object) null))
        return;
      this.TeamUploadButton.interactable = false;
    }

    public class DropItemData : ItemData
    {
      public bool mIsSecret;
    }

    private class CampaignPartyExp : MonoBehaviour
    {
      public int Exp;

      private void Start()
      {
        Text component = this.gameObject.GetComponent<Text>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.text = this.Exp.ToString();
      }
    }
  }
}
