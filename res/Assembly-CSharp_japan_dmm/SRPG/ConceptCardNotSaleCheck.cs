// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardNotSaleCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ConceptCardNotSaleCheck : MonoBehaviour
  {
    [SerializeField]
    private GameObject mCardObjectTemplate;
    [SerializeField]
    private RectTransform mCardObjectParent;

    private void Start()
    {
      if (Object.op_Equality((Object) this.mCardObjectTemplate, (Object) null) && Object.op_Equality((Object) this.mCardObjectParent, (Object) null))
      {
        Debug.LogWarning((object) "mCardObject is null");
      }
      else
      {
        this.mCardObjectTemplate.SetActive(false);
        ConceptCardManager instance = ConceptCardManager.Instance;
        if (Object.op_Equality((Object) instance, (Object) null))
          return;
        foreach (ConceptCardData card in instance.SelectedMaterials.GetList())
        {
          if (card.Param.not_sale)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.mCardObjectTemplate);
            gameObject.transform.SetParent((Transform) this.mCardObjectParent, false);
            ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Setup(card);
            gameObject.SetActive(true);
          }
        }
      }
    }
  }
}
