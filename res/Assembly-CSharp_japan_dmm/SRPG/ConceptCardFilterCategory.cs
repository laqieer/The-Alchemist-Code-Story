// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardFilterCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardFilterCategory : MonoBehaviour
  {
    [SerializeField]
    private Text mHeaderText;
    [SerializeField]
    private GameObject mToggleTemplate;
    private List<Toggle> mToggles = new List<Toggle>();
    private FilterConceptCardParam mFilterParam;
    private Dictionary<string, GameObject> mCreatedToggles = new Dictionary<string, GameObject>();

    public List<Toggle> Toggles => this.mToggles;

    public FilterConceptCardParam FilterParam => this.mFilterParam;

    public void Init(FilterConceptCardParam filter_param)
    {
      this.mFilterParam = filter_param;
      GameUtility.SetGameObjectActive(this.mToggleTemplate, false);
      this.mHeaderText.text = filter_param.name;
      for (int index = 0; index < filter_param.conditions.Length; ++index)
      {
        if (!this.mCreatedToggles.ContainsKey(filter_param.conditions[index].cnds_iname))
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.mToggleTemplate, ((Component) this).transform, false);
          Toggle componentInChildren1 = gameObject.GetComponentInChildren<Toggle>();
          // ISSUE: method pointer
          ((UnityEvent<bool>) componentInChildren1.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CInit\u003Em__0)));
          this.mToggles.Add(componentInChildren1);
          gameObject.SetActive(true);
          this.mCreatedToggles.Add(filter_param.conditions[index].cnds_iname, gameObject);
          Text componentInChildren2 = gameObject.GetComponentInChildren<Text>();
          if (Object.op_Inequality((Object) componentInChildren2, (Object) null))
            componentInChildren2.text = filter_param.conditions[index].name;
          FilterUtility.FilterBindData data = new FilterUtility.FilterBindData(filter_param.conditions[index].rarity, filter_param.conditions[index].name);
          DataSource.Bind<FilterUtility.FilterBindData>(((Component) componentInChildren1).gameObject, data);
          DataSource.Bind<FilterConceptCardConditionParam>(((Component) componentInChildren1).gameObject, filter_param.conditions[index]);
          if (Object.op_Inequality((Object) ConceptCardListFilterWindow.Instance, (Object) null))
          {
            bool flag = ConceptCardListFilterWindow.Instance.CurrentFilterPrefs.GetValue(filter_param.conditions[index].parent.iname, filter_param.conditions[index].cnds_iname);
            GameUtility.SetToggle(componentInChildren1, flag);
          }
        }
      }
    }

    private void OnToggleValueChanged()
    {
      if (!Object.op_Inequality((Object) ConceptCardListFilterWindow.Instance, (Object) null))
        return;
      ConceptCardListFilterWindow.Instance.UpdateTabState();
    }

    public bool IsExistActiveToggle()
    {
      for (int index = 0; index < this.mToggles.Count; ++index)
      {
        if (this.mToggles[index].isOn)
          return true;
      }
      return false;
    }
  }
}
