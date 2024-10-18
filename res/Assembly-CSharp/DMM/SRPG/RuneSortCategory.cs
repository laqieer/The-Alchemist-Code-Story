// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSortCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneSortCategory : MonoBehaviour
  {
    [SerializeField]
    private Text mHeaderText;
    [SerializeField]
    private RuneSortToggleItem mToggleTemplate;
    private SortRuneParam mSortParam;
    private Dictionary<string, RuneSortToggleItem> mCreatedToggles = new Dictionary<string, RuneSortToggleItem>();

    public SortRuneParam SortParam => this.mSortParam;

    public RuneSortToggleItem[] ToggleValues
    {
      get
      {
        return this.mCreatedToggles.Select<KeyValuePair<string, RuneSortToggleItem>, RuneSortToggleItem>((Func<KeyValuePair<string, RuneSortToggleItem>, RuneSortToggleItem>) (pair => pair.Value)).ToArray<RuneSortToggleItem>();
      }
    }

    public void Init(SortRuneParam sort_param)
    {
      this.mSortParam = sort_param;
      GameUtility.SetGameObjectActive((Component) this.mToggleTemplate, false);
      this.mHeaderText.text = sort_param.name;
      for (int index = 0; index < sort_param.conditions.Length; ++index)
      {
        if (!this.mCreatedToggles.ContainsKey(sort_param.conditions[index].cnds_iname))
        {
          RuneSortToggleItem runeSortToggleItem = UnityEngine.Object.Instantiate<RuneSortToggleItem>(this.mToggleTemplate, ((Component) this).transform, false);
          GameObject gameObject = ((Component) runeSortToggleItem).gameObject;
          // ISSUE: method pointer
          runeSortToggleItem.SetToggleValueChangeListner(new UnityAction<bool>((object) this, __methodptr(\u003CInit\u003Em__1)));
          gameObject.SetActive(true);
          this.mCreatedToggles.Add(sort_param.conditions[index].cnds_iname, runeSortToggleItem);
          runeSortToggleItem.SetName(sort_param.conditions[index].name);
          DataSource.Bind<SortRuneConditionParam>(gameObject, sort_param.conditions[index]);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) RuneSortWindow.Instance, (UnityEngine.Object) null))
          {
            bool flag = RuneSortWindow.Instance.CurrentSortPrefs.GetValue(sort_param.conditions[index].parent.iname, sort_param.conditions[index].cnds_iname);
            runeSortToggleItem.SetToggleSilient(flag);
          }
        }
      }
    }

    private void OnToggleValueChanged()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) RuneFilterWindow.Instance, (UnityEngine.Object) null))
        return;
      RuneFilterWindow.Instance.UpdateTabState();
    }

    public bool IsExistActiveToggle()
    {
      foreach (RuneSortToggleItem runeSortToggleItem in this.mCreatedToggles.Values)
      {
        if (runeSortToggleItem.isOn)
          return true;
      }
      return false;
    }
  }
}
