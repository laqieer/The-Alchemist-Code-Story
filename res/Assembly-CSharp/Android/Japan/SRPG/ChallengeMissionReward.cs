﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(4, "継続", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "完了", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "ミッション報酬", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(3, "コンプリート報酬", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(5, "全ミッションコンプリート", FlowNode.PinTypes.Output, 5)]
  public class ChallengeMissionReward : MonoBehaviour, IFlowInterface
  {
    public GameObject PanelNormal;
    public GameObject PanelComplete;
    public RawImage ImageItem;
    public RawImage ImageExp;
    public RawImage ImageGold;
    public RawImage ImageStamina;
    public ConceptCardIcon ConceptCardObject;
    public RawImage ImageReward;
    public UnityEngine.UI.Text TextMessage;
    private bool isAllMissionCompleteMessageShown;
    private TrophyParam mTrophy;

    public void Activated(int pinID)
    {
      if (pinID != 4)
        return;
      if (this.isAllMissionCompleteMessageShown && this.mTrophy.iname == "CHALLENGE_06")
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      else
        this.StartCoroutine(this.showRewardMessage());
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.PanelNormal != (UnityEngine.Object) null)
        this.PanelNormal.SetActive(false);
      if (!((UnityEngine.Object) this.PanelComplete != (UnityEngine.Object) null))
        return;
      this.PanelComplete.SetActive(false);
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.TextMessage == (UnityEngine.Object) null)
        this.enabled = false;
      else if (string.IsNullOrEmpty(GlobalVars.SelectedChallengeMissionTrophy))
      {
        this.enabled = false;
      }
      else
      {
        this.mTrophy = ChallengeMission.GetTrophy(GlobalVars.SelectedChallengeMissionTrophy);
        if (this.mTrophy == null)
        {
          this.enabled = false;
        }
        else
        {
          if (this.mTrophy.IsChallengeMissionRoot)
          {
            this.PanelNormal.SetActive(false);
            this.PanelComplete.SetActive(true);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
          }
          else
          {
            this.PanelNormal.SetActive(true);
            this.PanelComplete.SetActive(false);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
          }
          this.UpdateReward(this.mTrophy);
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator showRewardMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChallengeMissionReward.\u003CshowRewardMessage\u003Ec__Iterator0() { \u0024this = this };
    }

    private void UpdateReward(TrophyParam trophy)
    {
      if (trophy == null)
        return;
      string format1 = LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_ITEM");
      string format2 = LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_OTHER");
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      string str1 = string.Empty;
      ItemParam data = (ItemParam) null;
      if (trophy.Gold != 0)
      {
        flag3 = true;
        string str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) trophy.Gold);
        str1 = string.Format(format2, (object) str2);
      }
      else if (trophy.Exp != 0)
      {
        flag2 = true;
        string str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) trophy.Exp);
        str1 = string.Format(format2, (object) str2);
      }
      else if (trophy.Coin != 0)
      {
        flag1 = true;
        string str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) trophy.Coin);
        str1 = string.Format(format2, (object) str2);
        data = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
      }
      else if (trophy.Stamina != 0)
      {
        flag4 = true;
        string str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) trophy.Stamina);
        str1 = string.Format(format2, (object) str2);
      }
      else if (trophy.Items != null && trophy.Items.Length > 0)
      {
        flag1 = true;
        data = MonoSingleton<GameManager>.Instance.GetItemParam(trophy.Items[0].iname);
        if (data != null)
        {
          if (data.type == EItemType.Unit)
          {
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(data.iname);
            if (unitParam != null)
            {
              string str2 = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT_BR", (object) ((int) unitParam.rare + 1), (object) unitParam.name);
              str1 = string.Format(format2, (object) str2);
            }
          }
          else
            str1 = string.Format(format1, (object) data.name, (object) trophy.Items[0].Num);
        }
      }
      else if (trophy.ConceptCards != null && trophy.ConceptCards.Length > 0)
      {
        flag5 = true;
        ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(trophy.ConceptCards[0].iname);
        if (conceptCardParam != null)
        {
          str1 = string.Format(LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_CONCEPT_CARD"), (object) conceptCardParam.name, (object) trophy.ConceptCards[0].Num);
          if ((UnityEngine.Object) this.ConceptCardObject != (UnityEngine.Object) null)
            this.ConceptCardObject.Setup(ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname));
        }
      }
      if ((UnityEngine.Object) this.ImageItem != (UnityEngine.Object) null)
        this.ImageItem.gameObject.SetActive(flag1);
      if ((UnityEngine.Object) this.ImageExp != (UnityEngine.Object) null)
        this.ImageExp.gameObject.SetActive(flag2);
      if ((UnityEngine.Object) this.ImageGold != (UnityEngine.Object) null)
        this.ImageGold.gameObject.SetActive(flag3);
      if ((UnityEngine.Object) this.ImageStamina != (UnityEngine.Object) null)
        this.ImageStamina.gameObject.SetActive(flag4);
      if ((UnityEngine.Object) this.ConceptCardObject != (UnityEngine.Object) null)
        this.ConceptCardObject.gameObject.SetActive(flag5);
      if (data != null)
        DataSource.Bind<ItemParam>(this.gameObject, data, false);
      if (!((UnityEngine.Object) this.TextMessage != (UnityEngine.Object) null))
        return;
      this.TextMessage.text = str1;
    }

    private string GetAllRewardText(TrophyParam trophy)
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      if (trophy.Items != null && trophy.Items.Length > 0)
      {
        foreach (TrophyParam.RewardItem rewardItem in this.mTrophy.Items)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(rewardItem.iname);
          if (itemParam != null)
          {
            if (itemParam.type == EItemType.UnitPiece)
            {
              stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_PIECE"), (object) itemParam.name, (object) rewardItem.Num));
              flag = true;
            }
            else if (itemParam.type == EItemType.Unit)
            {
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(itemParam.iname);
              if (unitParam != null)
              {
                string str = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT", (object) ((int) unitParam.rare + 1), (object) unitParam.name);
                stringBuilder.AppendLine(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_GET", new object[1]
                {
                  (object) str
                }));
              }
            }
            else
              stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD"), (object) itemParam.name, (object) rewardItem.Num));
          }
        }
      }
      if (trophy.Gold != 0)
        stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) trophy.Gold));
      else if (trophy.Exp != 0)
        stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) trophy.Exp));
      else if (trophy.Coin != 0)
        stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) trophy.Coin));
      else if (trophy.Stamina != 0)
        stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) trophy.Stamina));
      else if (trophy.ConceptCards != null)
      {
        foreach (TrophyParam.RewardItem conceptCard in trophy.ConceptCards)
        {
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(conceptCard.iname);
          if (conceptCardParam == null)
            Debug.LogError((object) "GetConceptCardParam == null");
          else
            stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_CONCEPT_CARD"), (object) conceptCardParam.name, (object) conceptCard.Num));
        }
      }
      if (flag)
      {
        stringBuilder.AppendLine(string.Empty);
        stringBuilder.AppendLine(LocalizedText.Get("sys.CHALLENGE_REWARD_NOTE"));
      }
      return stringBuilder.ToString();
    }
  }
}
