// Decompiled with JetBrains decompiler
// Type: SRPG.SetItemObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SetItemObject : MonoBehaviour
  {
    [SerializeField]
    public GameObject UnitIcon;
    [SerializeField]
    public GameObject AwardIcon;
    [SerializeField]
    public GameObject ArtifactIcon;
    [SerializeField]
    public GameObject GoldIcon;
    [SerializeField]
    public GameObject CoinIcon;
    [SerializeField]
    public GameObject ItemIcon;
    [SerializeField]
    public GameObject CardIcon;
    [SerializeField]
    private Text ItemDesc;

    private void Start()
    {
    }

    public void SetupConceptCard(ConceptCardData concept_card_data)
    {
      if (concept_card_data == null)
      {
        DebugUtility.LogError("concept_card_data == null");
      }
      else
      {
        ConceptCardIcon componentInChildren = this.CardIcon.GetComponentInChildren<ConceptCardIcon>();
        if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
          return;
        componentInChildren.Setup(concept_card_data);
      }
    }

    public void SetIconActive(GiftTypes gift_type)
    {
      if (Object.op_Inequality((Object) this.UnitIcon, (Object) null))
        this.UnitIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.AwardIcon, (Object) null))
        this.AwardIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.ArtifactIcon, (Object) null))
        this.ArtifactIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.GoldIcon, (Object) null))
        this.GoldIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.CoinIcon, (Object) null))
        this.CoinIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.ItemIcon, (Object) null))
        this.ItemIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.CardIcon, (Object) null))
        this.CardIcon.SetActive(false);
      if (gift_type >= GiftTypes.Item && gift_type <= GiftTypes.Coin)
      {
        switch (gift_type)
        {
          case GiftTypes.Item:
            if (!Object.op_Inequality((Object) this.ItemIcon, (Object) null))
              return;
            this.ItemIcon.SetActive(true);
            return;
          case GiftTypes.Gold:
            if (!Object.op_Inequality((Object) this.GoldIcon, (Object) null))
              return;
            this.GoldIcon.SetActive(true);
            return;
          case GiftTypes.Coin:
            if (!Object.op_Inequality((Object) this.CoinIcon, (Object) null))
              return;
            this.CoinIcon.SetActive(true);
            return;
        }
      }
      switch (gift_type)
      {
        case GiftTypes.Artifact:
          if (!Object.op_Inequality((Object) this.ArtifactIcon, (Object) null))
            break;
          this.ArtifactIcon.SetActive(true);
          break;
        case GiftTypes.Unit:
          if (!Object.op_Inequality((Object) this.UnitIcon, (Object) null))
            break;
          this.UnitIcon.SetActive(true);
          break;
        case GiftTypes.Award:
          if (!Object.op_Inequality((Object) this.AwardIcon, (Object) null))
            break;
          this.AwardIcon.SetActive(true);
          break;
        default:
          if (gift_type != GiftTypes.ConceptCard || !Object.op_Inequality((Object) this.CardIcon, (Object) null))
            break;
          this.CardIcon.SetActive(true);
          break;
      }
    }

    public void SetItemDesc(GiftTypes gift_type, string name, int num)
    {
      if (Object.op_Inequality((Object) this.UnitIcon, (Object) null))
        this.UnitIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.AwardIcon, (Object) null))
        this.AwardIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.ArtifactIcon, (Object) null))
        this.ArtifactIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.GoldIcon, (Object) null))
        this.GoldIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.CoinIcon, (Object) null))
        this.CoinIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.ItemIcon, (Object) null))
        this.ItemIcon.SetActive(false);
      if (Object.op_Inequality((Object) this.CardIcon, (Object) null))
        this.CardIcon.SetActive(false);
      if (gift_type >= GiftTypes.Item && gift_type <= GiftTypes.Coin)
      {
        switch (gift_type)
        {
          case GiftTypes.Item:
            if (Object.op_Inequality((Object) this.ItemIcon, (Object) null))
              this.ItemIcon.SetActive(true);
            if (!Object.op_Inequality((Object) this.ItemDesc, (Object) null))
              return;
            this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), (object) name, (object) num);
            return;
          case GiftTypes.Gold:
            if (Object.op_Inequality((Object) this.GoldIcon, (Object) null))
              this.GoldIcon.SetActive(true);
            if (!Object.op_Inequality((Object) this.ItemDesc, (Object) null))
              return;
            this.ItemDesc.text = num.ToString() + LocalizedText.Get("sys.GOLD");
            return;
          case GiftTypes.Coin:
            if (Object.op_Inequality((Object) this.CoinIcon, (Object) null))
              this.CoinIcon.SetActive(true);
            if (!Object.op_Inequality((Object) this.ItemDesc, (Object) null))
              return;
            this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
            return;
        }
      }
      switch (gift_type)
      {
        case GiftTypes.Artifact:
          if (Object.op_Inequality((Object) this.ArtifactIcon, (Object) null))
            this.ArtifactIcon.SetActive(true);
          if (!Object.op_Inequality((Object) this.ItemDesc, (Object) null))
            break;
          this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), (object) name, (object) num);
          break;
        case GiftTypes.Unit:
          if (Object.op_Inequality((Object) this.UnitIcon, (Object) null))
            this.UnitIcon.SetActive(true);
          if (!Object.op_Inequality((Object) this.ItemDesc, (Object) null))
            break;
          this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), (object) name, (object) num);
          break;
        case GiftTypes.Award:
          if (Object.op_Inequality((Object) this.AwardIcon, (Object) null))
            this.AwardIcon.SetActive(true);
          if (!Object.op_Inequality((Object) this.ItemDesc, (Object) null))
            break;
          this.ItemDesc.text = name;
          break;
        case GiftTypes.ConceptCard:
          if (Object.op_Inequality((Object) this.CardIcon, (Object) null))
            this.CardIcon.SetActive(true);
          if (!Object.op_Inequality((Object) this.ItemDesc, (Object) null))
            break;
          this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_CONCEPT_CARD"), (object) name, (object) num);
          break;
      }
    }
  }
}
