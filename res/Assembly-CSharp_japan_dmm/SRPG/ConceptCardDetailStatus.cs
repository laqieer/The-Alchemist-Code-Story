// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ConceptCardDetailStatus : ConceptCardDetailBase
  {
    [SerializeField]
    private GameObject mParentObject;
    [SerializeField]
    private GameObject mStatusBaseItem;
    private List<GameObject> mInstantiateItems = new List<GameObject>();
    private int mAddExp;
    private int mAddAwakeLv;
    private bool mIsEnhance;

    public override void SetParam(
      ConceptCardData card_data,
      int addExp,
      int addTrust,
      int addAwakeLv)
    {
      this.mConceptCardData = card_data;
      this.mAddExp = addExp;
      this.mAddAwakeLv = addAwakeLv;
      this.mIsEnhance = ConceptCardDescription.IsEnhance;
      if (!Object.op_Inequality((Object) this.mStatusBaseItem, (Object) null))
        return;
      this.mStatusBaseItem.SetActive(false);
    }

    public override void Refresh()
    {
      if (this.mConceptCardData == null || Object.op_Equality((Object) this.mParentObject, (Object) null) || Object.op_Equality((Object) this.mStatusBaseItem, (Object) null))
        return;
      this.InitStatusItems();
      int index = 0;
      int num = index + 1;
      ConceptCardDetailStatusItems componentInChildren1 = this.CreateStatusItem(index).GetComponentInChildren<ConceptCardDetailStatusItems>();
      if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
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
          if (Object.op_Inequality((Object) componentInChildren2, (Object) null))
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
      ConceptCardEffectsParam baseEffectParam = (ConceptCardEffectsParam) null;
      foreach (ConceptCardEffectsParam cardEffectsParam in effects)
      {
        if (cardEffectsParam != null && string.IsNullOrEmpty(cardEffectsParam.cnds_iname) && !string.IsNullOrEmpty(cardEffectsParam.statusup_skill))
        {
          if (baseEffectParam != null)
            DebugUtility.LogError("基準パラメータが２つ以上設定されています");
          baseEffectParam = cardEffectsParam;
        }
      }
      return baseEffectParam;
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
      GameObject statusItem = Object.Instantiate<GameObject>(this.mStatusBaseItem);
      statusItem.transform.SetParent(this.mParentObject.transform);
      this.mInstantiateItems.Add(statusItem);
      statusItem.SetActive(true);
      statusItem.transform.localScale = new Vector3(1f, 1f, 1f);
      return statusItem;
    }
  }
}
