// Decompiled with JetBrains decompiler
// Type: SRPG.FilterUnitCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class FilterUnitCategory : MonoBehaviour
  {
    [SerializeField]
    private Text m_HeaderText;
    [SerializeField]
    private GameObject m_ToggleTemplate;

    private void Start() => GameUtility.SetGameObjectActive(this.m_ToggleTemplate, false);

    public void SetHeaderText(string headerText) => this.m_HeaderText.text = headerText;

    public Toggle CreateFilterButton(FilterUnitConditionParam filterConditionParam)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(this.m_ToggleTemplate, ((Component) this).transform, false);
      Toggle componentInChildren = gameObject.GetComponentInChildren<Toggle>();
      FilterUtility.FilterBindData data = new FilterUtility.FilterBindData(filterConditionParam.rarity_ini, filterConditionParam.name);
      DataSource.Bind<FilterUtility.FilterBindData>(gameObject, data);
      DataSource.Bind<FilterUnitConditionParam>(gameObject, filterConditionParam);
      return componentInChildren;
    }
  }
}
