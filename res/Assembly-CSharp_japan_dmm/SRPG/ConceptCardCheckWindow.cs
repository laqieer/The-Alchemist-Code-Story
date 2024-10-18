// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardCheckWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardCheckWindow : MonoBehaviour
  {
    [SerializeField]
    private RectTransform ListParent;
    [SerializeField]
    private GameObject ListItemTemplate;
    [SerializeField]
    private Text GetExp;
    [SerializeField]
    private Text UsedZenny;
    [SerializeField]
    private Text GetTrust;
    private Dictionary<string, int> mSelectedCard = new Dictionary<string, int>();
    private List<ConceptCardIcon> mConceptCardIcon = new List<ConceptCardIcon>();

    private void Start()
    {
      this.CreateMakeCardIcon();
      this.SetupText();
    }

    private void CreateMakeCardIcon()
    {
      if (Object.op_Equality((Object) this.ListParent, (Object) null) || Object.op_Equality((Object) this.ListItemTemplate, (Object) null))
        return;
      this.ListItemTemplate.SetActive(false);
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.BulkSelectedMaterialList.Count == 0)
        return;
      for (int index = 0; index < instance.BulkSelectedMaterialList.Count; ++index)
      {
        ConceptCardData mSelectedData = instance.BulkSelectedMaterialList[index].mSelectedData;
        if (mSelectedData != null)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ListItemTemplate);
          gameObject.SetActive(true);
          gameObject.transform.SetParent((Transform) this.ListParent, false);
          ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
          if (Object.op_Equality((Object) component, (Object) null))
            return;
          component.Setup(mSelectedData);
          this.mConceptCardIcon.Add(component);
          this.mSelectedCard.Add(mSelectedData.Param.iname, instance.BulkSelectedMaterialList[index].mSelectNum);
        }
      }
      foreach (ConceptCardIcon conceptCardIcon in this.mConceptCardIcon)
      {
        int num = this.mSelectedCard[conceptCardIcon.ConceptCard.Param.iname];
        conceptCardIcon.SetCardNum(num);
      }
    }

    private void SetupText()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.BulkSelectedMaterialList.Count == 0)
        return;
      int mixTotalExp;
      int mixTrustExp;
      ConceptCardManager.CalcTotalExpTrustMaterialData(out mixTotalExp, out mixTrustExp);
      if (Object.op_Inequality((Object) this.GetExp, (Object) null))
        this.GetExp.text = mixTotalExp.ToString();
      if (Object.op_Inequality((Object) this.GetTrust, (Object) null))
        ConceptCardManager.SubstituteTrustFormat(instance.SelectedConceptCardData, this.GetTrust, mixTrustExp);
      int totalMixZeny = 0;
      ConceptCardManager.GalcTotalMixZenyMaterialData(out totalMixZeny);
      if (!Object.op_Inequality((Object) this.UsedZenny, (Object) null))
        return;
      this.UsedZenny.text = totalMixZeny.ToString();
    }
  }
}
