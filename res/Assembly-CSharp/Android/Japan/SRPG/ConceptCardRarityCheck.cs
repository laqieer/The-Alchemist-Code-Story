﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardRarityCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ConceptCardRarityCheck : MonoBehaviour
  {
    [SerializeField]
    private GameObject mCardObjectTemplate;
    [SerializeField]
    private RectTransform mCardObjectParent;
    [SerializeField]
    private LText mLText;
    [SerializeField]
    private GameObject mButtonEnhance;
    [SerializeField]
    private GameObject mButtonSell;

    private void Start()
    {
      if ((UnityEngine.Object) this.mCardObjectTemplate == (UnityEngine.Object) null && (UnityEngine.Object) this.mCardObjectParent == (UnityEngine.Object) null)
      {
        Debug.LogWarning((object) "mCardObject is null");
      }
      else
      {
        this.mCardObjectTemplate.SetActive(false);
        ConceptCardManager instance = ConceptCardManager.Instance;
        if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
          return;
        this.mLText.text = string.Format(LocalizedText.Get(this.mLText.text), (object) instance.CostConceptCardRare.ToString());
        if (instance.IsDetailActive)
        {
          this.mButtonEnhance.SetActive(true);
          this.mButtonSell.SetActive(false);
        }
        else if (instance.IsSellListActive)
        {
          this.mButtonEnhance.SetActive(false);
          this.mButtonSell.SetActive(true);
        }
        else
        {
          Debug.LogWarning((object) "Must be from Sell or Enhance");
          return;
        }
        foreach (ConceptCardData card in instance.SelectedMaterials.GetList())
        {
          if ((int) card.Rarity + 1 >= instance.CostConceptCardRare)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mCardObjectTemplate);
            gameObject.transform.SetParent((Transform) this.mCardObjectParent, false);
            ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.Setup(card);
            gameObject.SetActive(true);
          }
        }
      }
    }
  }
}
