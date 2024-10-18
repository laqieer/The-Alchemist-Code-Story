// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "報酬表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "シェア画面表示へ", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "報酬表示した", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "シェア画面表示した", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "強制終了", FlowNode.PinTypes.Output, 102)]
  public class AdvanceResult : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Window;
    [SerializeField]
    private GameObject GoReward;
    [SerializeField]
    private GameObject GoShare;
    [Space(10f)]
    [SerializeField]
    private GameObject GoSingle;
    [SerializeField]
    private GameObject GoMultiple;
    [Space(5f)]
    [SerializeField]
    private Text TextRewardName;
    [SerializeField]
    private Text TextRewardNum;
    [SerializeField]
    private GameObject TextRewardConn;
    [SerializeField]
    private AdvanceRewardIcon RewardIconTemplate;
    [SerializeField]
    private Transform TrRewardIconParent;
    [Space(5f)]
    [SerializeField]
    private AdvanceResultItem RewardItemListTemplate;
    [SerializeField]
    private Transform TrRewardItemListParent;
    [SerializeField]
    private AdvanceResultItem RewardIconListTemplate;
    [SerializeField]
    private Transform TrRewardIconListParent;
    [Space(10f)]
    [SerializeField]
    private GameObject GoShareParent;
    private const int PIN_IN_REWARD = 1;
    private const int PIN_IN_SHARE = 11;
    private const int PIN_OUT_REWARD = 100;
    private const int PIN_OUT_SHARE = 101;
    private const int PIN_OUT_EXIT = 102;

    private void Awake()
    {
      if (!Object.op_Implicit((Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void RewardStart()
    {
      GiftData giftData = (GiftData) null;
      BattleCore battleCore = (BattleCore) null;
      if (Object.op_Implicit((Object) SceneBattle.Instance))
        battleCore = SceneBattle.Instance.Battle;
      if (battleCore != null)
      {
        BattleCore.Record questRecord = battleCore.GetQuestRecord();
        if (questRecord != null && questRecord.mAdvanceBossResultRewards != null && questRecord.mAdvanceBossResultRewards.Length != 0)
        {
          int length = questRecord.mAdvanceBossResultRewards.Length;
          if (Object.op_Implicit((Object) this.GoSingle))
            this.GoSingle.SetActive(length == 1);
          if (Object.op_Implicit((Object) this.GoMultiple))
            this.GoMultiple.SetActive(length != 1);
          if (Object.op_Implicit((Object) this.GoSingle))
            this.GoSingle.SetActive(length == 1);
          if (Object.op_Implicit((Object) this.GoMultiple))
            this.GoMultiple.SetActive(length != 1);
          if (length == 1)
          {
            giftData = new GiftData();
            giftData.Deserialize(questRecord.mAdvanceBossResultRewards[0]);
            string name;
            int amount;
            giftData.GetRewardNameAndAmount(out name, out amount);
            if (Object.op_Implicit((Object) this.TextRewardName))
              this.TextRewardName.text = name;
            if (Object.op_Implicit((Object) this.TextRewardNum))
              this.TextRewardNum.text = !giftData.CheckGiftTypeIncluded(GiftTypes.Gold) ? amount.ToString() : string.Format("{0:#,0}", (object) amount);
            if (Object.op_Implicit((Object) this.TextRewardConn))
              this.TextRewardConn.SetActive(true);
            if (Object.op_Implicit((Object) this.RewardIconTemplate) && Object.op_Implicit((Object) this.TrRewardIconParent))
              Object.Instantiate<AdvanceRewardIcon>(this.RewardIconTemplate, this.TrRewardIconParent).Initialize(giftData);
          }
          else
          {
            if (Object.op_Implicit((Object) this.RewardItemListTemplate) && Object.op_Implicit((Object) this.TrRewardItemListParent))
            {
              ((Component) this.RewardItemListTemplate).gameObject.SetActive(false);
              GameUtility.DestroyChildGameObjects(((Component) this.TrRewardItemListParent).gameObject, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
              {
                ((Component) this.RewardItemListTemplate).gameObject
              }));
            }
            if (Object.op_Implicit((Object) this.RewardIconListTemplate) && Object.op_Implicit((Object) this.TrRewardIconListParent))
            {
              ((Component) this.RewardIconListTemplate).gameObject.SetActive(false);
              GameUtility.DestroyChildGameObjects(((Component) this.TrRewardIconListParent).gameObject, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
              {
                ((Component) this.RewardIconListTemplate).gameObject
              }));
            }
            for (int index = 0; index < length; ++index)
            {
              giftData = new GiftData();
              giftData.Deserialize(questRecord.mAdvanceBossResultRewards[index]);
              if (Object.op_Implicit((Object) this.RewardItemListTemplate) && Object.op_Implicit((Object) this.TrRewardItemListParent))
              {
                AdvanceResultItem advanceResultItem = Object.Instantiate<AdvanceResultItem>(this.RewardItemListTemplate, this.TrRewardItemListParent, false);
                if (Object.op_Implicit((Object) advanceResultItem))
                {
                  advanceResultItem.SetItem(index, giftData);
                  ((Component) advanceResultItem).gameObject.SetActive(true);
                }
              }
              if (Object.op_Implicit((Object) this.RewardIconListTemplate) && Object.op_Implicit((Object) this.TrRewardIconListParent))
              {
                AdvanceResultItem advanceResultItem = Object.Instantiate<AdvanceResultItem>(this.RewardIconListTemplate, this.TrRewardIconListParent, false);
                if (Object.op_Implicit((Object) advanceResultItem))
                {
                  advanceResultItem.SetItem(index, giftData);
                  ((Component) advanceResultItem).gameObject.SetActive(true);
                }
              }
            }
          }
        }
      }
      if (giftData == null)
      {
        this.Exit();
      }
      else
      {
        GameUtility.SetGameObjectActive(this.GoReward, true);
        GameUtility.SetGameObjectActive(this.GoShare, false);
        ((Component) this).gameObject.SetActive(true);
        if (!Object.op_Implicit((Object) this.Window))
          return;
        this.Window.SetActive(true);
        GameParameter.UpdateAll(this.Window);
      }
    }

    private void ShareStart()
    {
      if (Object.op_Implicit((Object) this.GoReward) && Object.op_Implicit((Object) this.GoShare) && Object.op_Implicit((Object) this.GoShareParent))
      {
        GameManager instance1 = MonoSingleton<GameManager>.Instance;
        SceneBattle instance2 = SceneBattle.Instance;
        if (Object.op_Implicit((Object) instance1) && Object.op_Implicit((Object) instance2) && instance2.CurrentQuest != null)
        {
          QuestParam currentQuest = instance2.CurrentQuest;
          AdvanceEventParam eventParamFromAreaId = instance1.GetAdvanceEventParamFromAreaId(currentQuest.ChapterID);
          if (eventParamFromAreaId != null)
          {
            AdvanceEventModeInfoParam modeInfo = eventParamFromAreaId.GetModeInfo(currentQuest.difficulty);
            if (modeInfo != null && modeInfo.IsLapBoss)
            {
              DataSource.Bind<AdvanceBossInfo.AdvanceBossData>(this.GoShareParent, new AdvanceBossInfo.AdvanceBossData()
              {
                unit = modeInfo.BossUnitParam
              });
              GameParameter.UpdateAll(this.GoShareParent);
              this.GoReward.SetActive(false);
              this.GoShare.SetActive(true);
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
              return;
            }
          }
        }
      }
      this.Exit();
    }

    private void PrepareAsset()
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      SceneBattle instance2 = SceneBattle.Instance;
      if (Object.op_Implicit((Object) instance1) && Object.op_Implicit((Object) instance2) && instance2.CurrentQuest != null)
      {
        QuestParam currentQuest = instance2.CurrentQuest;
        AdvanceEventParam eventParamFromAreaId = instance1.GetAdvanceEventParamFromAreaId(currentQuest.ChapterID);
        if (eventParamFromAreaId != null)
        {
          AdvanceEventModeInfoParam modeInfo = eventParamFromAreaId.GetModeInfo(currentQuest.difficulty);
          if (modeInfo != null && modeInfo.IsLapBoss)
            AssetManager.PrepareAssets(AssetPath.UnitImage(modeInfo.BossUnitParam, (string) null));
        }
      }
      this.StartCoroutine(this.DownloadUnitImage());
    }

    [DebuggerHidden]
    private IEnumerator DownloadUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AdvanceResult.\u003CDownloadUnitImage\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void Exit() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);

    public void Activated(int pinID)
    {
      if (pinID != 1)
      {
        if (pinID != 11)
          return;
        this.ShareStart();
      }
      else
        this.PrepareAsset();
    }
  }
}
