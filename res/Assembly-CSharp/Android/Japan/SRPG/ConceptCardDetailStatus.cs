// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardDetailStatus : ConceptCardDetailBase
  {
    private List<GameObject> mInstantiateItems = new List<GameObject>();
    [SerializeField]
    private GameObject mParentObject;
    [SerializeField]
    private GameObject mStatusBaseItem;
    private int mAddExp;
    private int mAddAwakeLv;
    private bool mIsEnhance;

    public override void SetParam(ConceptCardData card_data, int addExp, int addTrust, int addAwakeLv)
    {
      this.mConceptCardData = card_data;
      this.mAddExp = addExp;
      this.mAddAwakeLv = addAwakeLv;
      this.mIsEnhance = ConceptCardDescription.IsEnhance;
      if (!((UnityEngine.Object) this.mStatusBaseItem != (UnityEngine.Object) null))
        return;
      this.mStatusBaseItem.SetActive(false);
    }

    public override void Refresh()
    {
      if (this.mConceptCardData == null || (UnityEngine.Object) this.mParentObject == (UnityEngine.Object) null || (UnityEngine.Object) this.mStatusBaseItem == (UnityEngine.Object) null)
        return;
      this.InitStatusItems();
      int index = 0;
      int num = index + 1;
      ConceptCardDetailStatusItems componentInChildren1 = this.CreateStatusItem(index).GetComponentInChildren<ConceptCardDetailStatusItems>();
      if ((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null)
      {
        componentInChildren1.SetParam(this.mConceptCardData, this.GetBaseEffectParam(), this.mAddExp, this.mAddAwakeLv, this.mIsEnhance, true);
        componentInChildren1.Refresh();
      }
      ConceptCardEffectsParam[] effects = this.mConceptCardData.Param.effects;
      if (effects == null || 0 >= effects.Length)
        return;
      foreach (ConceptCardEffectsParam conceptCardEffectsParam in effects)
      {
        if (conceptCardEffectsParam != null && !string.IsNullOrEmpty(conceptCardEffectsParam.cnds_iname) && !string.IsNullOrEmpty(conceptCardEffectsParam.statusup_skill))
        {
          ConceptCardDetailStatusItems componentInChildren2 = this.CreateStatusItem(num++).GetComponentInChildren<ConceptCardDetailStatusItems>();
          if ((UnityEngine.Object) componentInChildren2 != (UnityEngine.Object) null)
          {
            componentInChildren2.SetParam(this.mConceptCardData, conceptCardEffectsParam, this.mAddExp, this.mAddAwakeLv, this.mIsEnhance, false);
            componentInChildren2.Refresh();
          }
        }
      }
    }

    private ConceptCardEffectsParam GetBaseEffectParam()
    {
      ConceptCardEffectsParam[] effects = this.mConceptCardData.Param.effects;
      if (effects == null || 0 >= effects.Length)
        return (ConceptCardEffectsParam) null;
      ConceptCardEffectsParam cardEffectsParam1 = (ConceptCardEffectsParam) null;
      foreach (ConceptCardEffectsParam cardEffectsParam2 in effects)
      {
        if (cardEffectsParam2 != null && string.IsNullOrEmpty(cardEffectsParam2.cnds_iname) && !string.IsNullOrEmpty(cardEffectsParam2.statusup_skill))
        {
          if (cardEffectsParam1 != null)
            DebugUtility.LogError("基準パラメータが２つ以上設定されています");
          cardEffectsParam1 = cardEffectsParam2;
        }
      }
      return cardEffectsParam1;
    }

    private void InitStatusItems()
    {
      foreach (GameObject mInstantiateItem in this.mInstantiateItems)
        mInstantiateItem.SetActive(false);
    }

    private GameObject CreateStatusItem(int index)
    {
      if (index < this.mInstantiateItems.Count)
      {
        this.mInstantiateItems[index].SetActive(true);
        return this.mInstantiateItems[index];
      }
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mStatusBaseItem);
      gameObject.transform.SetParent(this.mParentObject.transform);
      this.mInstantiateItems.Add(gameObject);
      gameObject.SetActive(true);
      gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
      return gameObject;
    }
  }
}
