// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustMasterReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mItemlist, this.mItemlist.transform.parent);
        gameObject.gameObject.SetActive(true);
        switch (reward.reward_type)
        {
          case eRewardType.Item:
            ItemIcon componentInChildren1 = gameObject.GetComponentInChildren<ItemIcon>();
            if ((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null)
            {
              componentInChildren1.UpdateValue();
              break;
            }
            break;
          case eRewardType.Artifact:
            ArtifactIcon componentInChildren2 = gameObject.GetComponentInChildren<ArtifactIcon>();
            if ((UnityEngine.Object) componentInChildren2 != (UnityEngine.Object) null)
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
      DataSource.Bind<ItemParam>(this.gameObject, itemParam, false);
      if (!((UnityEngine.Object) this.mItemIcon != (UnityEngine.Object) null))
        return;
      this.mItemIcon.gameObject.SetActive(true);
      this.mItemIcon.UpdateValue();
    }

    public void SetArtifact(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return;
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward_item.iname);
      this.mItemName.text = artifactParam.name;
      DataSource.Bind<ArtifactParam>(this.gameObject, artifactParam, false);
      if (!((UnityEngine.Object) this.mArtifactIcon != (UnityEngine.Object) null))
        return;
      this.mArtifactIcon.gameObject.SetActive(true);
      this.mArtifactIcon.UpdateValue();
    }

    public void SetConceptCard(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(reward_item.iname);
      this.mItemName.text = conceptCardParam.name;
      DataSource.Bind<ConceptCardParam>(this.gameObject, conceptCardParam, false);
      if (!((UnityEngine.Object) this.mCardIcon != (UnityEngine.Object) null))
        return;
      this.mCardIcon.gameObject.SetActive(true);
      this.mCardIcon.Setup(ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname));
    }
  }
}
