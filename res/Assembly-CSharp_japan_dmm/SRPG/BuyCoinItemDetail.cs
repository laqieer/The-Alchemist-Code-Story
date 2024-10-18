// Decompiled with JetBrains decompiler
// Type: SRPG.BuyCoinItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BuyCoinItemDetail : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject ItemDetailWindow;
    [SerializeField]
    private GameObject ArtifactDetailWindow;
    [SerializeField]
    private GameObject ConceptCardDetail;
    [SerializeField]
    private Text DetailTitle;
    [SerializeField]
    private GameObject ItemIcon;
    [SerializeField]
    private RewardListItem ItemIconList;

    private void Start() => this.Refresh();

    public void Activated(int pinID)
    {
    }

    public void OnButtonItem(GameObject go)
    {
      ItemParam dataOfClass = DataSource.FindDataOfClass<ItemParam>(go, (ItemParam) null);
      if (dataOfClass == null || !Object.op_Inequality((Object) this.ItemDetailWindow, (Object) null))
        return;
      ItemData data = new ItemData();
      int itemAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(dataOfClass.iname);
      data.Setup(0L, dataOfClass, itemAmount);
      DataSource.Bind<ItemData>(Object.Instantiate<GameObject>(this.ItemDetailWindow), data);
    }

    public void OnButtonAtrifact(GameObject go)
    {
      if (Object.op_Equality((Object) go, (Object) null))
        return;
      ArtifactParam dataOfClass = DataSource.FindDataOfClass<ArtifactParam>(go, (ArtifactParam) null);
      if (dataOfClass == null || !Object.op_Inequality((Object) this.ArtifactDetailWindow, (Object) null))
        return;
      ArtifactData artifactDataForDisplay = ArtifactData.CreateArtifactDataForDisplay(dataOfClass, dataOfClass.raremax);
      if (artifactDataForDisplay == null)
        return;
      DataSource.Bind<ArtifactData>(Object.Instantiate<GameObject>(this.ArtifactDetailWindow), artifactDataForDisplay);
    }

    public void OnButtonConceptCard(GameObject go)
    {
      if (Object.op_Equality((Object) go, (Object) null))
        return;
      ConceptCardIcon dataOfClass = DataSource.FindDataOfClass<ConceptCardIcon>(go, (ConceptCardIcon) null);
      if (!Object.op_Inequality((Object) dataOfClass, (Object) null) || !Object.op_Inequality((Object) this.ConceptCardDetail, (Object) null))
        return;
      GlobalVars.SelectedConceptCardData.Set(dataOfClass.ConceptCard);
      Object.Instantiate<GameObject>(this.ConceptCardDetail);
    }

    private void Refresh()
    {
      BuyCoinProductParam dataOfClass1 = DataSource.FindDataOfClass<BuyCoinProductParam>(((Component) this).gameObject, (BuyCoinProductParam) null);
      PaymentManager.Product dataOfClass2 = DataSource.FindDataOfClass<PaymentManager.Product>(((Component) this).gameObject, (PaymentManager.Product) null);
      if (dataOfClass2 == null || dataOfClass1 == null || !Object.op_Inequality((Object) this.ItemIcon, (Object) null) || !Object.op_Inequality((Object) this.ItemIconList, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      BuyCoinRewardParam buyCoinRewardParam = instance.MasterParam.GetBuyCoinRewardParam(dataOfClass1.Reward);
      if (buyCoinRewardParam == null)
        return;
      if (Object.op_Inequality((Object) this.DetailTitle, (Object) null))
        this.DetailTitle.text = LocalizedText.Get("sys.BUYCOIN_DETAILTITLE", (object) dataOfClass2.name);
      for (int index = 0; index < buyCoinRewardParam.Reward.Count; ++index)
      {
        GameObject gameObject1 = Object.Instantiate<GameObject>(this.ItemIcon);
        gameObject1.SetActive(true);
        gameObject1.transform.SetParent(this.ItemIcon.transform.parent, false);
        RewardListItem componentInChildren1 = gameObject1.GetComponentInChildren<RewardListItem>();
        Text componentInChildren2 = gameObject1.GetComponentInChildren<Text>();
        GameObject gameObject2 = (GameObject) null;
        switch (buyCoinRewardParam.Reward[index].Type)
        {
          case BuyCoinManager.PremiumRewadType.Item:
            ItemParam itemParam = instance.GetItemParam(buyCoinRewardParam.Reward[index].Iname);
            if (itemParam != null)
            {
              gameObject2 = componentInChildren1.RewardItem;
              DataSource.Bind<ItemParam>(gameObject2, itemParam);
              componentInChildren2.text = LocalizedText.Get("sys.BUYCOIN_SET", (object) itemParam.name, (object) buyCoinRewardParam.Reward[index].Num.ToString());
              break;
            }
            break;
          case BuyCoinManager.PremiumRewadType.Gold:
            gameObject2 = componentInChildren1.RewardGold;
            componentInChildren2.text = LocalizedText.Get("sys.BUYCOIN_GOLD", (object) buyCoinRewardParam.Reward[index].Num.ToString());
            break;
          case BuyCoinManager.PremiumRewadType.Coin:
            gameObject2 = componentInChildren1.RewardCoin;
            componentInChildren2.text = LocalizedText.Get("sys.BUYCOIN_COIN", (object) buyCoinRewardParam.Reward[index].Num.ToString());
            break;
          case BuyCoinManager.PremiumRewadType.Artifact:
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(buyCoinRewardParam.Reward[index].Iname);
            if (artifactParam != null)
            {
              gameObject2 = componentInChildren1.RewardArtifact;
              DataSource.Bind<ArtifactParam>(gameObject2, artifactParam);
              componentInChildren2.text = LocalizedText.Get("sys.BUYCOIN_SET", (object) artifactParam.name, (object) buyCoinRewardParam.Reward[index].Num.ToString());
              break;
            }
            break;
          case BuyCoinManager.PremiumRewadType.ConceptCard:
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(buyCoinRewardParam.Reward[index].Iname);
            if (cardDataForDisplay != null)
            {
              gameObject2 = componentInChildren1.RewardCard;
              ConceptCardIcon component = gameObject2.GetComponent<ConceptCardIcon>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                DataSource.Bind<ConceptCardIcon>(gameObject2, component);
                component.Setup(cardDataForDisplay);
                componentInChildren2.text = LocalizedText.Get("sys.BUYCOIN_SET", (object) cardDataForDisplay.Param.name, (object) buyCoinRewardParam.Reward[index].Num.ToString());
                break;
              }
              break;
            }
            break;
          case BuyCoinManager.PremiumRewadType.Unit:
            UnitParam unitParam = instance.GetUnitParam(buyCoinRewardParam.Reward[index].Iname);
            if (unitParam != null)
            {
              gameObject2 = componentInChildren1.RewardUnit;
              DataSource.Bind<UnitParam>(gameObject2, unitParam);
              componentInChildren2.text = LocalizedText.Get("sys.BUYCOIN_SET", (object) unitParam.name, (object) buyCoinRewardParam.Reward[index].Num.ToString());
              break;
            }
            break;
        }
        if (Object.op_Inequality((Object) gameObject2, (Object) null))
        {
          gameObject2.transform.SetParent(componentInChildren1.RewardList, false);
          gameObject2.SetActive(true);
        }
      }
    }
  }
}
