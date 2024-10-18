// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisRewardIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GenesisRewardIcon : MonoBehaviour
  {
    [SerializeField]
    private GameObject mRewardUnit;
    [SerializeField]
    private GameObject mRewardItem;
    [SerializeField]
    private GameObject mRewardCard;
    [SerializeField]
    private GameObject mRewardArtifact;
    [SerializeField]
    private GameObject mRewardAward;
    [SerializeField]
    private GameObject mRewardGold;
    [SerializeField]
    private GameObject mRewardCoin;

    public void Initialize(GiftData reward)
    {
      if ((UnityEngine.Object) this.mRewardUnit != (UnityEngine.Object) null)
        this.mRewardUnit.SetActive(false);
      if ((UnityEngine.Object) this.mRewardItem != (UnityEngine.Object) null)
        this.mRewardItem.SetActive(false);
      if ((UnityEngine.Object) this.mRewardCard != (UnityEngine.Object) null)
        this.mRewardCard.SetActive(false);
      if ((UnityEngine.Object) this.mRewardArtifact != (UnityEngine.Object) null)
        this.mRewardArtifact.SetActive(false);
      if ((UnityEngine.Object) this.mRewardAward != (UnityEngine.Object) null)
        this.mRewardAward.SetActive(false);
      if ((UnityEngine.Object) this.mRewardGold != (UnityEngine.Object) null)
        this.mRewardGold.SetActive(false);
      if ((UnityEngine.Object) this.mRewardCoin != (UnityEngine.Object) null)
        this.mRewardCoin.SetActive(false);
      GameManager instance = MonoSingleton<GameManager>.Instance;
      bool flag = false;
      int num = 1;
      GameObject gameObject = (GameObject) null;
      if (!string.IsNullOrEmpty(reward.iname))
      {
        if (reward.CheckGiftTypeIncluded(GiftTypes.Item))
        {
          ItemParam itemParam = instance.GetItemParam(reward.iname);
          if (itemParam == null)
            return;
          DataSource.Bind<ItemParam>(this.mRewardItem, itemParam, false);
          gameObject = this.mRewardItem;
          num = reward.num;
          flag = true;
        }
        else if (reward.CheckGiftTypeIncluded(GiftTypes.Award))
        {
          AwardParam awardParam = instance.GetAwardParam(reward.iname);
          if (awardParam == null)
            return;
          DataSource.Bind<AwardParam>(this.mRewardAward, awardParam, false);
          gameObject = this.mRewardAward;
        }
        else if (reward.CheckGiftTypeIncluded(GiftTypes.Unit))
        {
          UnitParam unitParam = instance.GetUnitParam(reward.iname);
          if (unitParam == null)
            return;
          DataSource.Bind<UnitParam>(this.mRewardUnit, unitParam, false);
          gameObject = this.mRewardUnit;
        }
        else if (reward.CheckGiftTypeIncluded(GiftTypes.ConceptCard))
        {
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.iname);
          if (cardDataForDisplay == null)
            return;
          ConceptCardIcon component = this.mRewardCard.GetComponent<ConceptCardIcon>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.Setup(cardDataForDisplay);
          gameObject = this.mRewardCard;
          num = reward.num;
          flag = true;
        }
      }
      else if (reward.gold > 0)
      {
        gameObject = this.mRewardGold;
        num = reward.gold;
        flag = true;
      }
      else if (reward.coin > 0)
      {
        gameObject = this.mRewardCoin;
        num = reward.coin;
        flag = true;
      }
      else if (!string.IsNullOrEmpty(reward.ConceptCardIname))
        ;
      if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
        return;
      gameObject.SetActive(true);
      if (!flag)
        return;
      Transform transform = gameObject.transform.Find("amount/Text_amount");
      if (!((UnityEngine.Object) transform != (UnityEngine.Object) null))
        return;
      Text component1 = transform.GetComponent<Text>();
      if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null))
        return;
      component1.text = num.ToString();
    }

    public void Initialize(GenesisRewardDataParam reward)
    {
      if ((UnityEngine.Object) this.mRewardUnit != (UnityEngine.Object) null)
        this.mRewardUnit.SetActive(false);
      if ((UnityEngine.Object) this.mRewardItem != (UnityEngine.Object) null)
        this.mRewardItem.SetActive(false);
      if ((UnityEngine.Object) this.mRewardCard != (UnityEngine.Object) null)
        this.mRewardCard.SetActive(false);
      if ((UnityEngine.Object) this.mRewardArtifact != (UnityEngine.Object) null)
        this.mRewardArtifact.SetActive(false);
      if ((UnityEngine.Object) this.mRewardAward != (UnityEngine.Object) null)
        this.mRewardAward.SetActive(false);
      if ((UnityEngine.Object) this.mRewardGold != (UnityEngine.Object) null)
        this.mRewardGold.SetActive(false);
      if ((UnityEngine.Object) this.mRewardCoin != (UnityEngine.Object) null)
        this.mRewardCoin.SetActive(false);
      GameManager instance = MonoSingleton<GameManager>.Instance;
      bool flag = false;
      GameObject gameObject = (GameObject) null;
      switch (reward.ItemType)
      {
        case 0:
          ItemParam itemParam = instance.GetItemParam(reward.ItemIname);
          if (itemParam == null)
            return;
          DataSource.Bind<ItemParam>(this.mRewardItem, itemParam, false);
          gameObject = this.mRewardItem;
          flag = true;
          break;
        case 1:
          gameObject = this.mRewardGold;
          flag = true;
          break;
        case 2:
          gameObject = this.mRewardCoin;
          flag = true;
          break;
        case 3:
          AwardParam awardParam = instance.GetAwardParam(reward.ItemIname);
          if (awardParam == null)
            return;
          DataSource.Bind<AwardParam>(this.mRewardAward, awardParam, false);
          gameObject = this.mRewardAward;
          break;
        case 4:
          UnitParam unitParam = instance.GetUnitParam(reward.ItemIname);
          if (unitParam == null)
            return;
          DataSource.Bind<UnitParam>(this.mRewardUnit, unitParam, false);
          gameObject = this.mRewardUnit;
          break;
        case 5:
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.ItemIname);
          if (cardDataForDisplay == null)
            return;
          ConceptCardIcon component1 = this.mRewardCard.GetComponent<ConceptCardIcon>();
          if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
            component1.Setup(cardDataForDisplay);
          gameObject = this.mRewardCard;
          flag = true;
          break;
        case 6:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(reward.ItemIname);
          if (artifactParam == null)
            return;
          DataSource.Bind<ArtifactParam>(this.mRewardArtifact, artifactParam, false);
          gameObject = this.mRewardArtifact;
          break;
      }
      if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
        return;
      gameObject.SetActive(true);
      if (!flag)
        return;
      Transform transform = gameObject.transform.Find("amount/Text_amount");
      if (!((UnityEngine.Object) transform != (UnityEngine.Object) null))
        return;
      Text component2 = transform.GetComponent<Text>();
      if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
        return;
      component2.text = reward.ItemNum.ToString();
    }
  }
}
