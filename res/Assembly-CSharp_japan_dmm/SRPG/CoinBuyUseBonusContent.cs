// Decompiled with JetBrains decompiler
// Type: SRPG.CoinBuyUseBonusContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class CoinBuyUseBonusContent : MonoBehaviour
  {
    [SerializeField]
    private GameObject mRewardIconParent;
    [SerializeField]
    private Button mReceiveButton;
    [SerializeField]
    private Text mTitleText;
    [SerializeField]
    private Text mCurrentValueText;
    [SerializeField]
    private Text mTargetValueText;
    [SerializeField]
    private ImageArray mBackgroundImageArray;
    [SerializeField]
    private GameObject mReceivedImage;
    private CoinBuyUseBonusParam mBonusParam;
    private CoinBuyUseBonusContentParam mContentParam;

    public void Refresh(CoinBuyUseBonusParam bonus_param, CoinBuyUseBonusContentParam content_param)
    {
      foreach (Transform transform in this.mRewardIconParent.transform)
      {
        ((Component) transform).gameObject.SetActive(false);
        Object.Destroy((Object) ((Component) transform).gameObject);
      }
      this.mBonusParam = bonus_param;
      this.mContentParam = content_param;
      bool flag = MonoSingleton<GameManager>.Instance.Player.IsReceivableCoinBuyUseBonus(this.mBonusParam.Iname, this.mContentParam.Num);
      ((Selectable) this.mReceiveButton).interactable = flag;
      this.mBackgroundImageArray.ImageIndex = !flag ? 0 : 1;
      this.mReceivedImage.SetActive(MonoSingleton<GameManager>.Instance.Player.IsReceivedCoinBuyUseBonus(this.mBonusParam.Iname, this.mContentParam.Num));
      this.mTitleText.text = content_param.Name;
      this.mCurrentValueText.text = MonoSingleton<GameManager>.Instance.Player.GetCoinBuyUseBonusProgress(this.mBonusParam.Iname).ToString();
      this.mTargetValueText.text = content_param.Num.ToString();
      for (int index = 0; index < content_param.RewardParam.Rewards.Length; ++index)
      {
        CoinBuyUseBonusItemParam reward = content_param.RewardParam.Rewards[index];
        switch (reward.Type)
        {
          case eCoinBuyUseBonusRewardType.Item:
            this.SetParam_Item(reward);
            break;
          case eCoinBuyUseBonusRewardType.Gold:
            this.SetParam_Gold(reward);
            break;
          case eCoinBuyUseBonusRewardType.Coin:
            this.SetParam_Coin(reward);
            break;
          case eCoinBuyUseBonusRewardType.Artifact:
            this.SetParam_Artifact(reward);
            break;
          case eCoinBuyUseBonusRewardType.Unit:
            this.SetParam_Unit(reward);
            break;
          case eCoinBuyUseBonusRewardType.ConceptCard:
            this.SetParam_ConceptCard(reward);
            break;
          case eCoinBuyUseBonusRewardType.Award:
            this.SetParam_Award(reward);
            break;
        }
      }
    }

    public void OnClickReceiveReward()
    {
      if (this.mBonusParam == null || this.mContentParam == null)
        return;
      CoinBuyUseBonusWindow.Instance.ReceiveReward(this.mBonusParam, this.mContentParam);
    }

    private void SetParam_Item(CoinBuyUseBonusItemParam reward)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(CoinBuyUseBonusWindow.Instance.ItemIcon);
      gameObject.transform.SetParent(this.mRewardIconParent.transform, false);
      gameObject.SetActive(true);
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(reward.Item);
      DataSource.Bind<ItemParam>(gameObject, itemParam);
      DataSource.Bind<int>(gameObject, reward.Num);
    }

    private void SetParam_Gold(CoinBuyUseBonusItemParam reward)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(CoinBuyUseBonusWindow.Instance.GoldIcon);
      gameObject.transform.SetParent(this.mRewardIconParent.transform, false);
      gameObject.SetActive(true);
      DataSource.Bind<int>(gameObject, reward.Num);
    }

    private void SetParam_Coin(CoinBuyUseBonusItemParam reward)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(CoinBuyUseBonusWindow.Instance.CoinIcon);
      gameObject.transform.SetParent(this.mRewardIconParent.transform, false);
      gameObject.SetActive(true);
      DataSource.Bind<int>(gameObject, reward.Num);
    }

    private void SetParam_Artifact(CoinBuyUseBonusItemParam reward)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(CoinBuyUseBonusWindow.Instance.ArtifactIcon);
      gameObject.transform.SetParent(this.mRewardIconParent.transform, false);
      gameObject.SetActive(true);
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward.Item);
      DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
      ArtifactIcon component = gameObject.GetComponent<ArtifactIcon>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.UpdateValue();
    }

    private void SetParam_Unit(CoinBuyUseBonusItemParam reward)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(CoinBuyUseBonusWindow.Instance.UnitIcon);
      gameObject.transform.SetParent(this.mRewardIconParent.transform, false);
      gameObject.SetActive(true);
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(reward.Item);
      DataSource.Bind<UnitParam>(gameObject, unitParam);
    }

    private void SetParam_ConceptCard(CoinBuyUseBonusItemParam reward)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(CoinBuyUseBonusWindow.Instance.ConceptCardIcon);
      gameObject.transform.SetParent(this.mRewardIconParent.transform, false);
      gameObject.SetActive(true);
      ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.Item);
      if (cardDataForDisplay == null)
        return;
      ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Setup(cardDataForDisplay);
      DataSource.Bind<int>(gameObject, reward.Num);
    }

    private void SetParam_Award(CoinBuyUseBonusItemParam reward)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(CoinBuyUseBonusWindow.Instance.AwardIcon);
      gameObject.transform.SetParent(this.mRewardIconParent.transform, false);
      gameObject.SetActive(true);
      AwardParam awardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAwardParam(reward.Item);
      DataSource.Bind<AwardParam>(gameObject, awardParam);
    }
  }
}
