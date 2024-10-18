// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSellRarityList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardSellRarityList : MonoBehaviour
  {
    [SerializeField]
    private Image RarityObj;
    [SerializeField]
    private GameObject ConceptCardObj;
    [SerializeField]
    private RectTransform ParentObj;

    public void Init(MultiConceptCard card)
    {
      if (Object.op_Equality((Object) this.RarityObj, (Object) null) || Object.op_Equality((Object) this.ConceptCardObj, (Object) null) || Object.op_Equality((Object) this.ParentObj, (Object) null))
        return;
      this.ConceptCardObj.SetActive(false);
      if (card.GetList().Count > 0)
      {
        this.RarityObj.sprite = (Sprite) null;
        GameSettings instance = GameSettings.Instance;
        if (Object.op_Inequality((Object) instance, (Object) null) && instance.ConceptCardIcon_Rarity.Length > 0)
          this.RarityObj.sprite = instance.ConceptCardIcon_Rarity[(int) card.GetItem(0).Rarity];
      }
      for (int index = 0; index < card.GetList().Count; ++index)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.ConceptCardObj.gameObject);
        gameObject.transform.SetParent((Transform) this.ParentObj, false);
        ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.Setup(card.GetItem(index));
      }
    }
  }
}
