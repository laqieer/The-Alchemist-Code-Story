// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardCheckWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardCheckWindow : MonoBehaviour
  {
    private Dictionary<string, int> mSelectedCard = new Dictionary<string, int>();
    private List<ConceptCardIcon> mConceptCardIcon = new List<ConceptCardIcon>();
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

    private void Start()
    {
      this.CreateMakeCardIcon();
      this.SetupText();
    }

    private void CreateMakeCardIcon()
    {
      if ((UnityEngine.Object) this.ListParent == (UnityEngine.Object) null || (UnityEngine.Object) this.ListItemTemplate == (UnityEngine.Object) null)
        return;
      this.ListItemTemplate.SetActive(false);
      ConceptCardManager instance = ConceptCardManager.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || instance.BulkSelectedMaterialList.Count == 0)
        return;
      for (int index = 0; index < instance.BulkSelectedMaterialList.Count; ++index)
      {
        ConceptCardData mSelectedData = instance.BulkSelectedMaterialList[index].mSelectedData;
        if (mSelectedData != null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListItemTemplate);
          gameObject.SetActive(true);
          gameObject.transform.SetParent((Transform) this.ListParent, false);
          ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
          if ((UnityEngine.Object) component == (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || instance.BulkSelectedMaterialList.Count == 0)
        return;
      int mixTotalExp;
      int mixTrustExp;
      ConceptCardManager.CalcTotalExpTrustMaterialData(out mixTotalExp, out mixTrustExp);
      if ((UnityEngine.Object) this.GetExp != (UnityEngine.Object) null)
        this.GetExp.text = mixTotalExp.ToString();
      if ((UnityEngine.Object) this.GetTrust != (UnityEngine.Object) null)
        ConceptCardManager.SubstituteTrustFormat(instance.SelectedConceptCardData, this.GetTrust, mixTrustExp, false);
      int totalMixZeny = 0;
      ConceptCardManager.GalcTotalMixZenyMaterialData(out totalMixZeny);
      if (!((UnityEngine.Object) this.UsedZenny != (UnityEngine.Object) null))
        return;
      this.UsedZenny.text = totalMixZeny.ToString();
    }
  }
}
