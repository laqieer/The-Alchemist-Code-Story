// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ChallengeMissionItem : MonoBehaviour
  {
    public ChallengeMissionItem.ButtonObject ButtonNormal;
    public ChallengeMissionItem.ButtonObject ButtonHighlight;
    public ChallengeMissionItem.ButtonObject ButtonSecret;
    public Image ClearBadge;
    public UnityAction OnClick;

    private void Start()
    {
      this.Refresh();
    }

    public void Refresh()
    {
      this.gameObject.SetActive(true);
      this.ClearBadge.gameObject.SetActive(false);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(this.gameObject, (TrophyParam) null);
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null || dataOfClass == null)
      {
        this.ButtonHighlight.button.gameObject.SetActive(false);
        this.ButtonNormal.button.gameObject.SetActive(false);
        this.ButtonSecret.button.gameObject.SetActive(true);
      }
      else
      {
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(dataOfClass);
        ChallengeMissionItem.State state = ChallengeMissionItem.State.Challenge;
        if (trophyCounter.IsEnded)
          state = ChallengeMissionItem.State.Ended;
        else if (trophyCounter.IsCompleted)
          state = ChallengeMissionItem.State.Clear;
        ChallengeMissionItem.ButtonObject buttonObject;
        if (state == ChallengeMissionItem.State.Clear)
        {
          this.ButtonHighlight.button.gameObject.SetActive(true);
          this.ButtonNormal.button.gameObject.SetActive(false);
          this.ButtonSecret.button.gameObject.SetActive(false);
          buttonObject = this.ButtonHighlight;
        }
        else
        {
          this.ButtonHighlight.button.gameObject.SetActive(false);
          this.ButtonNormal.button.gameObject.SetActive(true);
          this.ButtonSecret.button.gameObject.SetActive(false);
          buttonObject = this.ButtonNormal;
        }
        if ((UnityEngine.Object) this.ClearBadge != (UnityEngine.Object) null)
          this.ClearBadge.gameObject.SetActive(state == ChallengeMissionItem.State.Ended);
        if (buttonObject != null && (UnityEngine.Object) buttonObject.title != (UnityEngine.Object) null)
          buttonObject.title.text = dataOfClass.Name;
        if (buttonObject != null && (UnityEngine.Object) buttonObject.button != (UnityEngine.Object) null)
        {
          buttonObject.button.onClick.RemoveAllListeners();
          buttonObject.button.onClick.AddListener(this.OnClick);
          buttonObject.button.interactable = state != ChallengeMissionItem.State.Ended;
        }
        if (buttonObject != null && (UnityEngine.Object) buttonObject.reward != (UnityEngine.Object) null)
        {
          if (dataOfClass.Gold != 0)
          {
            buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) dataOfClass.Gold);
            GameUtility.SetGameObjectActive((Component) buttonObject.icon, true);
            GameUtility.SetGameObjectActive((Component) buttonObject.conceptCardIcon, false);
          }
          else if (dataOfClass.Exp != 0)
          {
            buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) dataOfClass.Exp);
            GameUtility.SetGameObjectActive((Component) buttonObject.icon, true);
            GameUtility.SetGameObjectActive((Component) buttonObject.conceptCardIcon, false);
          }
          else if (dataOfClass.Coin != 0)
          {
            buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) dataOfClass.Coin);
            GameUtility.SetGameObjectActive((Component) buttonObject.icon, true);
            GameUtility.SetGameObjectActive((Component) buttonObject.conceptCardIcon, false);
          }
          else if (dataOfClass.Stamina != 0)
          {
            buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) dataOfClass.Stamina);
            GameUtility.SetGameObjectActive((Component) buttonObject.icon, true);
            GameUtility.SetGameObjectActive((Component) buttonObject.conceptCardIcon, false);
          }
          else if (dataOfClass.Items != null && dataOfClass.Items.Length > 0)
          {
            GameUtility.SetGameObjectActive((Component) buttonObject.icon, true);
            GameUtility.SetGameObjectActive((Component) buttonObject.conceptCardIcon, false);
            ItemParam itemParam = instanceDirect.GetItemParam(dataOfClass.Items[0].iname);
            if (itemParam != null)
              buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), (object) itemParam.name, (object) dataOfClass.Items[0].Num);
          }
          else if (dataOfClass.ConceptCards != null && dataOfClass.ConceptCards.Length > 0)
          {
            GameUtility.SetGameObjectActive((Component) buttonObject.icon, false);
            GameUtility.SetGameObjectActive((Component) buttonObject.conceptCardIcon, true);
            ConceptCardParam conceptCardParam = instanceDirect.MasterParam.GetConceptCardParam(dataOfClass.ConceptCards[0].iname);
            if (conceptCardParam != null)
              buttonObject.reward.text = string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_CONCEPT_CARD"), (object) conceptCardParam.name, (object) dataOfClass.ConceptCards[0].Num);
            if ((UnityEngine.Object) buttonObject.conceptCardIcon != (UnityEngine.Object) null)
            {
              ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname);
              buttonObject.conceptCardIcon.Setup(cardDataForDisplay);
            }
          }
        }
        if (!((UnityEngine.Object) buttonObject.icon != (UnityEngine.Object) null))
          return;
        buttonObject.icon.UpdateValue();
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
      public GameParameter icon;
      public ConceptCardIcon conceptCardIcon;
    }
  }
}
