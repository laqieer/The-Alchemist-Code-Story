// Decompiled with JetBrains decompiler
// Type: SRPG.SetItemObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
        if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null))
          return;
        componentInChildren.Setup(concept_card_data);
      }
    }

    public void SetIconActive(GiftTypes gift_type)
    {
      if ((UnityEngine.Object) this.UnitIcon != (UnityEngine.Object) null)
        this.UnitIcon.SetActive(false);
      if ((UnityEngine.Object) this.AwardIcon != (UnityEngine.Object) null)
        this.AwardIcon.SetActive(false);
      if ((UnityEngine.Object) this.ArtifactIcon != (UnityEngine.Object) null)
        this.ArtifactIcon.SetActive(false);
      if ((UnityEngine.Object) this.GoldIcon != (UnityEngine.Object) null)
        this.GoldIcon.SetActive(false);
      if ((UnityEngine.Object) this.CoinIcon != (UnityEngine.Object) null)
        this.CoinIcon.SetActive(false);
      if ((UnityEngine.Object) this.ItemIcon != (UnityEngine.Object) null)
        this.ItemIcon.SetActive(false);
      if ((UnityEngine.Object) this.CardIcon != (UnityEngine.Object) null)
        this.CardIcon.SetActive(false);
      if (gift_type >= GiftTypes.Item && gift_type <= GiftTypes.Coin)
      {
        switch (gift_type)
        {
          case GiftTypes.Item:
            if (!((UnityEngine.Object) this.ItemIcon != (UnityEngine.Object) null))
              return;
            this.ItemIcon.SetActive(true);
            return;
          case GiftTypes.Gold:
            if (!((UnityEngine.Object) this.GoldIcon != (UnityEngine.Object) null))
              return;
            this.GoldIcon.SetActive(true);
            return;
          case GiftTypes.Coin:
            if (!((UnityEngine.Object) this.CoinIcon != (UnityEngine.Object) null))
              return;
            this.CoinIcon.SetActive(true);
            return;
        }
      }
      switch (gift_type)
      {
        case GiftTypes.Artifact:
          if (!((UnityEngine.Object) this.ArtifactIcon != (UnityEngine.Object) null))
            break;
          this.ArtifactIcon.SetActive(true);
          break;
        case GiftTypes.Unit:
          if (!((UnityEngine.Object) this.UnitIcon != (UnityEngine.Object) null))
            break;
          this.UnitIcon.SetActive(true);
          break;
        case GiftTypes.Award:
          if (!((UnityEngine.Object) this.AwardIcon != (UnityEngine.Object) null))
            break;
          this.AwardIcon.SetActive(true);
          break;
        default:
          if (gift_type != GiftTypes.ConceptCard || !((UnityEngine.Object) this.CardIcon != (UnityEngine.Object) null))
            break;
          this.CardIcon.SetActive(true);
          break;
      }
    }

    public void SetItemDesc(GiftTypes gift_type, string name, int num)
    {
      if ((UnityEngine.Object) this.UnitIcon != (UnityEngine.Object) null)
        this.UnitIcon.SetActive(false);
      if ((UnityEngine.Object) this.AwardIcon != (UnityEngine.Object) null)
        this.AwardIcon.SetActive(false);
      if ((UnityEngine.Object) this.ArtifactIcon != (UnityEngine.Object) null)
        this.ArtifactIcon.SetActive(false);
      if ((UnityEngine.Object) this.GoldIcon != (UnityEngine.Object) null)
        this.GoldIcon.SetActive(false);
      if ((UnityEngine.Object) this.CoinIcon != (UnityEngine.Object) null)
        this.CoinIcon.SetActive(false);
      if ((UnityEngine.Object) this.ItemIcon != (UnityEngine.Object) null)
        this.ItemIcon.SetActive(false);
      if ((UnityEngine.Object) this.CardIcon != (UnityEngine.Object) null)
        this.CardIcon.SetActive(false);
      if (gift_type >= GiftTypes.Item && gift_type <= GiftTypes.Coin)
      {
        switch (gift_type)
        {
          case GiftTypes.Item:
            if ((UnityEngine.Object) this.ItemIcon != (UnityEngine.Object) null)
              this.ItemIcon.SetActive(true);
            if (!((UnityEngine.Object) this.ItemDesc != (UnityEngine.Object) null))
              return;
            this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), (object) name, (object) num);
            return;
          case GiftTypes.Gold:
            if ((UnityEngine.Object) this.GoldIcon != (UnityEngine.Object) null)
              this.GoldIcon.SetActive(true);
            if (!((UnityEngine.Object) this.ItemDesc != (UnityEngine.Object) null))
              return;
            this.ItemDesc.text = num.ToString() + LocalizedText.Get("sys.GOLD");
            return;
          case GiftTypes.Coin:
            if ((UnityEngine.Object) this.CoinIcon != (UnityEngine.Object) null)
              this.CoinIcon.SetActive(true);
            if (!((UnityEngine.Object) this.ItemDesc != (UnityEngine.Object) null))
              return;
            this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
            return;
        }
      }
      switch (gift_type)
      {
        case GiftTypes.Artifact:
          if ((UnityEngine.Object) this.ArtifactIcon != (UnityEngine.Object) null)
            this.ArtifactIcon.SetActive(true);
          if (!((UnityEngine.Object) this.ItemDesc != (UnityEngine.Object) null))
            break;
          this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), (object) name, (object) num);
          break;
        case GiftTypes.Unit:
          if ((UnityEngine.Object) this.UnitIcon != (UnityEngine.Object) null)
            this.UnitIcon.SetActive(true);
          if (!((UnityEngine.Object) this.ItemDesc != (UnityEngine.Object) null))
            break;
          this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), (object) name, (object) num);
          break;
        case GiftTypes.Award:
          if ((UnityEngine.Object) this.AwardIcon != (UnityEngine.Object) null)
            this.AwardIcon.SetActive(true);
          if (!((UnityEngine.Object) this.ItemDesc != (UnityEngine.Object) null))
            break;
          this.ItemDesc.text = name;
          break;
        case GiftTypes.ConceptCard:
          if ((UnityEngine.Object) this.CardIcon != (UnityEngine.Object) null)
            this.CardIcon.SetActive(true);
          if (!((UnityEngine.Object) this.ItemDesc != (UnityEngine.Object) null))
            break;
          this.ItemDesc.text = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_CONCEPT_CARD"), (object) name, (object) num);
          break;
      }
    }
  }
}
