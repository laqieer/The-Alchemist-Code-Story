// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustMasterReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardTrustMasterReward : MonoBehaviour
  {
    [SerializeField]
    private Text mItemName;
    [SerializeField]
    private Text mItemAmount;
    [SerializeField]
    private GameObject mItemlist;
    [SerializeField]
    private ItemIcon mItemIcon;
    [SerializeField]
    private ArtifactIcon mArtifactIcon;
    [SerializeField]
    private ConceptCardIcon mCardIcon;
    [SerializeField]
    private Sprite CoinFrame;
    [SerializeField]
    private Sprite GoldFrame;

    public void SetData(ConceptCardData data)
    {
      ConceptCardTrustRewardItemParam reward = data.GetReward();
      if (reward == null)
        return;
      switch (reward.reward_type)
      {
        case eRewardType.Item:
          this.SetItem(reward);
          break;
        case eRewardType.Artifact:
          this.SetArtifact(reward);
          break;
        case eRewardType.ConceptCard:
          this.SetConceptCard(reward);
          break;
      }
      this.mItemAmount.text = reward.reward_num.ToString();
      if (data.TrustBonusCount < 2)
        return;
      for (int index = 0; index < data.TrustBonusCount - 1; ++index)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.mItemlist, this.mItemlist.transform.parent);
        gameObject.gameObject.SetActive(true);
        switch (reward.reward_type)
        {
          case eRewardType.Item:
            ItemIcon componentInChildren1 = gameObject.GetComponentInChildren<ItemIcon>();
            if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
            {
              componentInChildren1.UpdateValue();
              break;
            }
            break;
          case eRewardType.Artifact:
            ArtifactIcon componentInChildren2 = gameObject.GetComponentInChildren<ArtifactIcon>();
            if (Object.op_Inequality((Object) componentInChildren2, (Object) null))
            {
              componentInChildren2.UpdateValue();
              break;
            }
            break;
        }
      }
    }

    public void SetItem(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(reward_item.iname);
      this.mItemName.text = itemParam.name;
      DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
      if (!Object.op_Inequality((Object) this.mItemIcon, (Object) null))
        return;
      ((Component) this.mItemIcon).gameObject.SetActive(true);
      this.mItemIcon.UpdateValue();
    }

    public void SetArtifact(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return;
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward_item.iname);
      this.mItemName.text = artifactParam.name;
      DataSource.Bind<ArtifactParam>(((Component) this).gameObject, artifactParam);
      if (!Object.op_Inequality((Object) this.mArtifactIcon, (Object) null))
        return;
      ((Component) this.mArtifactIcon).gameObject.SetActive(true);
      this.mArtifactIcon.UpdateValue();
    }

    public void SetConceptCard(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(reward_item.iname);
      this.mItemName.text = conceptCardParam.name;
      DataSource.Bind<ConceptCardParam>(((Component) this).gameObject, conceptCardParam);
      if (!Object.op_Inequality((Object) this.mCardIcon, (Object) null))
        return;
      ((Component) this.mCardIcon).gameObject.SetActive(true);
      this.mCardIcon.Setup(ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname));
    }
  }
}
