// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ChallengeMissionItem : MonoBehaviour
  {
    public ChallengeMissionItem.ButtonObject ButtonNormal;
    public ChallengeMissionItem.ButtonObject ButtonHighlight;
    public ChallengeMissionItem.ButtonObject ButtonSecret;
    public Image ClearBadge;
    public UnityAction OnClick;

    private void Start() => this.Refresh();

    public void Refresh()
    {
      ((Component) this).gameObject.SetActive(true);
      ((Component) this.ClearBadge).gameObject.SetActive(false);
      ((Component) this.ButtonHighlight.button).gameObject.SetActive(false);
      ((Component) this.ButtonNormal.button).gameObject.SetActive(false);
      ((Component) this.ButtonSecret.button).gameObject.SetActive(false);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || dataOfClass == null)
      {
        ((Component) this.ButtonSecret.button).gameObject.SetActive(true);
        this.ButtonSecret.LockIcon.SetActive(false);
      }
      else if (!dataOfClass.CheckRequiredTrophies(true))
      {
        this.ButtonSecret.LockIcon.SetActive(true);
        ((UnityEventBase) this.ButtonSecret.button.onClick).RemoveAllListeners();
        ((UnityEvent) this.ButtonSecret.button.onClick).AddListener(this.OnClick);
        ((Selectable) this.ButtonSecret.button).interactable = true;
        ((Component) this.ButtonSecret.button).gameObject.SetActive(true);
      }
      else
      {
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(dataOfClass);
        ChallengeMissionItem.State state = ChallengeMissionItem.State.Challenge;
        if (trophyCounter.IsEnded)
          state = ChallengeMissionItem.State.Ended;
        else if (trophyCounter.IsCompleted)
          state = ChallengeMissionItem.State.Clear;
        ChallengeMissionItem.ButtonObject buttonObject = state != ChallengeMissionItem.State.Clear ? this.ButtonNormal : this.ButtonHighlight;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearBadge, (UnityEngine.Object) null))
          ((Component) this.ClearBadge).gameObject.SetActive(state == ChallengeMissionItem.State.Ended);
        if (buttonObject != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) buttonObject.title, (UnityEngine.Object) null))
          buttonObject.title.text = dataOfClass.Name;
        if (buttonObject != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) buttonObject.button, (UnityEngine.Object) null))
        {
          ((UnityEventBase) buttonObject.button.onClick).RemoveAllListeners();
          ((UnityEvent) buttonObject.button.onClick).AddListener(this.OnClick);
          ((Selectable) buttonObject.button).interactable = state != ChallengeMissionItem.State.Ended;
        }
        if (buttonObject != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) buttonObject.button, (UnityEngine.Object) null))
          ((Component) buttonObject.button).gameObject.SetActive(true);
        if (buttonObject == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) buttonObject.reward, (UnityEngine.Object) null))
          return;
        GameUtility.SetGameObjectActive(buttonObject.itemIcon, false);
        GameUtility.SetGameObjectActive(buttonObject.goldIcon, false);
        GameUtility.SetGameObjectActive(buttonObject.staminaIcon, false);
        GameUtility.SetGameObjectActive(buttonObject.expIcon, false);
        GameUtility.SetGameObjectActive((Component) buttonObject.conceptCardIcon, false);
        GameUtility.SetGameObjectActive(buttonObject.unitIcon, false);
        TrophyRewardPriority trophyRewardPriority = TrophyRewardPriority.Gold;
        if (dataOfClass.RewardPriority == TrophyRewardPriority.None)
        {
          if (dataOfClass.Gold != 0)
            trophyRewardPriority = TrophyRewardPriority.Gold;
          else if (dataOfClass.Exp != 0)
            trophyRewardPriority = TrophyRewardPriority.Exp;
          else if (dataOfClass.Coin != 0)
            trophyRewardPriority = TrophyRewardPriority.Coin;
          else if (dataOfClass.Stamina != 0)
            trophyRewardPriority = TrophyRewardPriority.Stamina;
          else if (dataOfClass.Items != null && dataOfClass.Items.Length > 0)
            trophyRewardPriority = TrophyRewardPriority.Item;
          else if (dataOfClass.ConceptCards != null && dataOfClass.ConceptCards.Length > 0)
            trophyRewardPriority = TrophyRewardPriority.ConceptCard;
        }
        else
          trophyRewardPriority = dataOfClass.RewardPriority;
        bool flag = false;
        switch (trophyRewardPriority)
        {
          case TrophyRewardPriority.Item:
            if (dataOfClass.Items != null && dataOfClass.Items.Length > 0)
            {
              ItemParam itemParam = instanceDirect.GetItemParam(dataOfClass.Items[0].iname);
              if (itemParam != null)
                buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), (object) itemParam.name, (object) dataOfClass.Items[0].Num);
              if (itemParam.type == EItemType.Unit)
              {
                GameUtility.SetGameObjectActive(buttonObject.unitIcon, true);
                DataSource.Bind<ItemParam>(buttonObject.unitIcon.gameObject, itemParam);
                GameParameter.UpdateAll(buttonObject.unitIcon);
              }
              else
              {
                GameUtility.SetGameObjectActive(buttonObject.itemIcon, true);
                DataSource.Bind<ItemParam>(buttonObject.itemIcon.gameObject, itemParam);
                GameParameter.UpdateAll(buttonObject.itemIcon);
              }
              flag = true;
              break;
            }
            break;
          case TrophyRewardPriority.Gold:
            if (dataOfClass.Gold != 0)
            {
              string formatedText = CurrencyBitmapText.CreateFormatedText(dataOfClass.Gold.ToString());
              buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) formatedText);
              GameUtility.SetGameObjectActive(buttonObject.goldIcon, true);
              flag = true;
              break;
            }
            break;
          case TrophyRewardPriority.Coin:
            if (dataOfClass.Coin != 0)
            {
              buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) dataOfClass.Coin);
              GameUtility.SetGameObjectActive(buttonObject.itemIcon, true);
              ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
              DataSource.Bind<ItemParam>(buttonObject.itemIcon.gameObject, itemParam);
              GameParameter.UpdateAll(buttonObject.itemIcon);
              flag = true;
              break;
            }
            break;
          case TrophyRewardPriority.ConceptCard:
            if (dataOfClass.ConceptCards != null && dataOfClass.ConceptCards.Length > 0)
            {
              GameUtility.SetGameObjectActive((Component) buttonObject.conceptCardIcon, true);
              ConceptCardParam conceptCardParam = instanceDirect.MasterParam.GetConceptCardParam(dataOfClass.ConceptCards[0].iname);
              if (conceptCardParam != null)
                buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_CONCEPT_CARD"), (object) conceptCardParam.name, (object) dataOfClass.ConceptCards[0].Num);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) buttonObject.conceptCardIcon, (UnityEngine.Object) null))
              {
                ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname);
                buttonObject.conceptCardIcon.Setup(cardDataForDisplay);
              }
              flag = true;
              break;
            }
            break;
          case TrophyRewardPriority.Stamina:
            if (dataOfClass.Stamina != 0)
            {
              buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) dataOfClass.Stamina);
              GameUtility.SetGameObjectActive(buttonObject.staminaIcon, true);
              flag = true;
              break;
            }
            break;
          case TrophyRewardPriority.Exp:
            if (dataOfClass.Exp != 0)
            {
              buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) dataOfClass.Exp);
              GameUtility.SetGameObjectActive(buttonObject.expIcon, true);
              flag = true;
              break;
            }
            break;
        }
        if (flag)
          return;
        DebugUtility.LogError("チャレンジミッション [" + dataOfClass.iname + "] の表示優先報酬として [" + (object) dataOfClass.RewardPriority + "] が指定されていますが、報酬データが空です。");
      }
    }

    private enum State
    {
      Challenge,
      Clear,
      Ended,
    }

    [Serializable]
    public class ButtonObject
    {
      public Button button;
      public Text title;
      public Text reward;
      public GameObject itemIcon;
      public GameObject goldIcon;
      public GameObject staminaIcon;
      public GameObject expIcon;
      public ConceptCardIcon conceptCardIcon;
      public GameObject unitIcon;
      public GameObject LockIcon;
    }
  }
}
