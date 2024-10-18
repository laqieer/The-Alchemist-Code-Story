// Decompiled with JetBrains decompiler
// Type: SRPG.RaidResultWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "ドロップアイテムの獲得演出開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "プレイヤー経験値の獲得演出開始", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "ファントム経験値の獲得演出開始", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "アイテムスキップHoldOn", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "アイテムスキップHoldOff", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(20, "経験値演出スキップ", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(100, "ドロップアイテムの獲得演出終了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "プレイヤー経験値の獲得演出終了", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(300, "ファントム経験値の獲得演出終了", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(1000, "終了", FlowNode.PinTypes.Output, 1000)]
  public class RaidResultWindow : SRPG_ListBase, IFlowInterface
  {
    public ScrollRect ResultLayout;
    public Transform ResultParent;
    public GameObject ResultTemplate;
    public Button BtnUp;
    public Button BtnDown;
    public Button BtnOutSide;
    public Text TextOutSide;
    public Button BtnGainExpOutSide;
    [Description("入手アイテムのリストになる親ゲームオブジェクト")]
    public GameObject TreasureList;
    [Description("入手アイテムのリストになる親ゲームオブジェクト Contentnode版")]
    public ContentController CcTreasureList;
    [Description("入手アイテムのゲームオブジェクト")]
    public GameObject TreasureListItem;
    [Description("入手真理念装のゲームオブジェクト")]
    public GameObject TreasureListConceptCard;
    [Description("入新規取得アイテムのバッジ")]
    public GameObject NewItemBadge;
    public GameObject DropItemWindow;
    public GameObject GainExpWindow;
    public GameObject PlayerResult;
    public Slider PlayerGauge;
    public Text TxtPlayerLvVal;
    public Text TxtPlayerExpVal;
    public Text TxtGainGoldVal;
    [Description("レベルアップ時に使用するトリガー。ゲームオブジェクトにアタッチされたAnimatorへ送られます。")]
    public string LevelUpTrigger = "levelup";
    [Description("一秒あたりの経験値の増加量")]
    public float ExpGainRate = 100f;
    [Description("経験値増加アニメーションの最長時間。経験値がExpGainRateの速度で増加する時、これで設定した時間を超える時に加算速度を上げる。")]
    public float ExpGainTimeMax = 2f;
    public float ResultScrollInterval = 1f;
    [Description("経験値増加アニメーションスキップの倍速設定")]
    public float ExpSkipSpeedMul = 10f;
    public GameObject UnitList;
    public GameObject UnitListItem;
    public Button SkipButton;
    public Button ExpSkipButton;
    [Range(0.1f, 10f)]
    public float SkipTimeScale = 2f;
    private RaidResult mRaidResult;
    private List<GameObject> mResults = new List<GameObject>();
    private List<GameObject> mUnitListItems = new List<GameObject>();
    private RaidResultElement mCurrentElement;
    private bool mItemSkipElement;
    private bool mExpSkipElement;
    private bool mIsSkipDropItemResult;
    public int[] AcquiredUnitExp;
    private List<QuestResultTreasureParam> mCcTreasureList = new List<QuestResultTreasureParam>();

    protected override ScrollRect GetScrollRect() => this.ResultLayout;

    protected override RectTransform GetRectTransform() => this.ResultParent as RectTransform;

    protected override void Start()
    {
      base.Start();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultTemplate, (UnityEngine.Object) null))
        this.ResultTemplate.SetActive(false);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.UnitListItem))
        this.UnitListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnUp, (UnityEngine.Object) null))
        ((Selectable) this.BtnUp).interactable = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnDown, (UnityEngine.Object) null))
        ((Selectable) this.BtnUp).interactable = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnOutSide, (UnityEngine.Object) null))
        ((Selectable) this.BtnOutSide).interactable = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnGainExpOutSide, (UnityEngine.Object) null))
        ((Selectable) this.BtnGainExpOutSide).interactable = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultLayout, (UnityEngine.Object) null))
        ((Behaviour) this.ResultLayout).enabled = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GainExpWindow, (UnityEngine.Object) null))
        this.GainExpWindow.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListItem, (UnityEngine.Object) null))
        this.TreasureListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListConceptCard, (UnityEngine.Object) null))
        this.TreasureListConceptCard.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NewItemBadge, (UnityEngine.Object) null))
        this.NewItemBadge.SetActive(false);
      this.mRaidResult = GlobalVars.RaidResult;
      if (this.mRaidResult != null)
      {
        this.ApplyQuestCampaignParams(this.mRaidResult.campaignIds);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultTemplate, (UnityEngine.Object) null))
        {
          for (int index = 0; index < this.mRaidResult.results.Count; ++index)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ResultTemplate);
            gameObject.transform.SetParent(this.ResultParent, false);
            DataSource.Bind<RaidQuestResult>(gameObject, this.mRaidResult.results[index]);
            ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              this.AddItem(component);
            this.mResults.Add(gameObject);
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitListItem, (UnityEngine.Object) null))
        {
          Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitList, (UnityEngine.Object) null) ? this.UnitListItem.transform.parent : this.UnitList.transform;
          for (int index = 0; index < this.mRaidResult.members.Count; ++index)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitListItem);
            gameObject.transform.SetParent(transform, false);
            this.mUnitListItems.Add(gameObject);
            DataSource.Bind<UnitData>(gameObject, this.mRaidResult.members[index]);
            gameObject.SetActive(true);
          }
        }
        QuestResult.DropItemData[] items = this.MergeDropItems(this.mRaidResult);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CcTreasureList, (UnityEngine.Object) null))
          this.CreateTreasureObject(items);
        else
          this.CreateDropItemObjects(items);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtGainGoldVal, (UnityEngine.Object) null))
          this.TxtGainGoldVal.text = this.mRaidResult.gold.ToString();
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextOutSide) && this.mRaidResult.quest != null && this.mRaidResult.quest.IsGenAdvBoss)
          this.TextOutSide.text = LocalizedText.Get("sys.CMD_OK");
      }
      GlobalVars.RaidResult = (RaidResult) null;
      GlobalVars.RaidNum = 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkipButton, (UnityEngine.Object) null))
        ((Component) this.SkipButton).gameObject.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpSkipButton, (UnityEngine.Object) null))
        return;
      ((Component) this.ExpSkipButton).gameObject.SetActive(false);
    }

    private QuestResult.DropItemData[] MergeDropItems(RaidResult raidResult)
    {
      if (raidResult == null)
        return new QuestResult.DropItemData[0];
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      List<QuestResult.DropItemData> dropItemDataList = new List<QuestResult.DropItemData>();
      foreach (RaidQuestResult result in raidResult.results)
      {
        if (result != null)
        {
          foreach (QuestResult.DropItemData drop in result.drops)
          {
            if (drop != null && (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CcTreasureList, (UnityEngine.Object) null) || !drop.IsItem || drop.Param.type != EItemType.Rune))
            {
              bool flag = false;
              for (int index = 0; index < dropItemDataList.Count; ++index)
              {
                if (dropItemDataList[index].IsItem && drop.IsItem && dropItemDataList[index].itemParam == drop.itemParam)
                {
                  dropItemDataList[index].Gain(drop.Num);
                  flag = true;
                  break;
                }
                if (dropItemDataList[index].IsConceptCard && drop.IsConceptCard && dropItemDataList[index].conceptCardParam == drop.conceptCardParam)
                {
                  dropItemDataList[index].Gain(drop.Num);
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
                if (drop.IsItem)
                {
                  dropItemData.SetupDropItemData(EBattleRewardType.Item, 0L, drop.itemParam.iname, drop.Num);
                  switch (drop.itemParam.type)
                  {
                    case EItemType.Unit:
                      string iid = drop.itemParam.iname;
                      if (player.Units.Find((Predicate<UnitData>) (p => p.UnitParam.iname == iid)) == null)
                      {
                        dropItemData.IsNew = true;
                        break;
                      }
                      break;
                    case EItemType.Rune:
                      break;
                    default:
                      ItemData itemDataByItemParam = player.FindItemDataByItemParam(drop.itemParam);
                      dropItemData.IsNew = !player.ItemEntryExists(drop.itemParam.iname) || itemDataByItemParam == null || itemDataByItemParam.IsNew;
                      break;
                  }
                }
                else if (drop.IsConceptCard)
                  dropItemData.SetupDropItemData(EBattleRewardType.ConceptCard, 0L, drop.conceptCardParam.iname, drop.Num);
                dropItemDataList.Add(dropItemData);
              }
            }
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CcTreasureList, (UnityEngine.Object) null) && raidResult.runes_detail.Count > 0)
      {
        raidResult.runes_detail.Sort((Comparison<RuneData>) ((x, y) => x.Rarity != y.Rarity ? x.Rarity.CompareTo(y.Rarity) : 0));
        raidResult.runes_detail.Reverse();
        foreach (RuneData data in raidResult.runes_detail)
        {
          RuneParam runeParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRuneParam(data.iname);
          if (runeParam != null)
          {
            QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
            dropItemData.SetupDropItemData(EBattleRewardType.Item, 0L, runeParam.item_iname, 1);
            dropItemData.SetupDropRuneData(data);
            dropItemDataList.Add(dropItemData);
          }
        }
      }
      return dropItemDataList.ToArray();
    }

    private void CreateDropItemObjects(QuestResult.DropItemData[] items)
    {
      Transform transform1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureList, (UnityEngine.Object) null) ? this.TreasureListItem.transform.parent : this.TreasureList.transform;
      foreach (QuestResult.DropItemData data in items)
      {
        GameObject root = (GameObject) null;
        if (data.IsConceptCard)
        {
          root = UnityEngine.Object.Instantiate<GameObject>(this.TreasureListConceptCard);
          root.transform.SetParent(transform1, false);
          DataSource.Bind<QuestResult.DropItemData>(root, data);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
        else if (data.IsItem)
        {
          root = UnityEngine.Object.Instantiate<GameObject>(this.TreasureListItem);
          root.transform.SetParent(transform1, false);
          DataSource.Bind<ItemData>(root, (ItemData) data);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
        else
          DebugUtility.LogError(string.Format("[コードの追加が必要] DropItemData.mBattleRewardType(={0})は不明な列挙です", (object) data.BattleRewardType));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NewItemBadge, (UnityEngine.Object) null) && data.IsNew)
        {
          RectTransform transform2 = UnityEngine.Object.Instantiate<GameObject>(this.NewItemBadge).transform as RectTransform;
          ((Component) transform2).gameObject.SetActive(true);
          transform2.anchoredPosition = Vector2.zero;
          ((Transform) transform2).SetParent(root.transform, false);
        }
      }
    }

    private void CreateTreasureObject(QuestResult.DropItemData[] items)
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
      source.SetTable((ContentSource.Param[]) this.mCcTreasureList.ToArray());
      this.CcTreasureList.Initialize(source, Vector2.zero);
    }

    private void ApplyQuestCampaignParams(string[] campaignIds)
    {
      this.AcquiredUnitExp = new int[this.mRaidResult.members.Count];
      if (campaignIds != null)
      {
        QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCampaigns(campaignIds);
        List<UnitData> members = this.mRaidResult.members;
        float[] numArray = new float[members.Count];
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
              int index = members.FindIndex((Predicate<UnitData>) (value => value.UnitParam.iname == data.unit));
              if (index >= 0)
                numArray[index] = data.GetRate();
            }
          }
          else if (data.type == QuestCampaignValueTypes.ExpPlayer)
            this.mRaidResult.pexp = Mathf.RoundToInt((float) this.mRaidResult.pexp * data.GetRate());
        }
        int uexp = this.mRaidResult.uexp;
        for (int index = 0; index < numArray.Length; ++index)
        {
          float num2 = 1f;
          if ((double) num1 != 1.0 && (double) numArray[index] != 1.0)
            num2 = num1 + numArray[index];
          else if ((double) num1 != 1.0)
            num2 = num1;
          else if ((double) numArray[index] != 1.0)
            num2 = numArray[index];
          this.AcquiredUnitExp[index] = Mathf.RoundToInt((float) uexp * num2);
        }
      }
      else
      {
        for (int index = 0; index < this.AcquiredUnitExp.Length; ++index)
          this.AcquiredUnitExp[index] = this.mRaidResult.uexp;
      }
    }

    public void SkipDropItemResult()
    {
      this.DropItemWindow.SetActive(false);
      this.mIsSkipDropItemResult = true;
    }

    public void Activated(int pinID)
    {
      if (pinID == 1 && this.mIsSkipDropItemResult)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
        pinID = 2;
      }
      if (pinID == 1)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkipButton, (UnityEngine.Object) null))
          ((Component) this.SkipButton).gameObject.SetActive(true);
        this.StartCoroutine(this.QuestResultAnimation());
      }
      if (pinID == 2)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpSkipButton, (UnityEngine.Object) null))
          ((Component) this.ExpSkipButton).gameObject.SetActive(true);
        this.StartCoroutine(this.GainPlayerExpAnimation());
      }
      if (pinID == 3)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpSkipButton, (UnityEngine.Object) null))
          ((Component) this.ExpSkipButton).gameObject.SetActive(true);
        this.StartCoroutine(this.GainUnitExpAnimation());
      }
      if (pinID == 10)
        this.mItemSkipElement = true;
      if (pinID == 11)
        this.mItemSkipElement = false;
      if (pinID != 20)
        return;
      this.mExpSkipElement = true;
    }

    [DebuggerHidden]
    private IEnumerator QuestResultAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultWindow.\u003CQuestResultAnimation\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator GainPlayerExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultWindow.\u003CGainPlayerExpAnimation\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator GainUnitExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultWindow.\u003CGainUnitExpAnimation\u003Ec__Iterator2()
      {
        \u0024this = this
      };
    }
  }
}
