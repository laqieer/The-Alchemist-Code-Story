// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobMasterWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  public class UnitJobMasterWindow : SRPG_FixedList, IFlowInterface
  {
    public GameObject StatusItemTemplate;
    public Button NextButton;
    private List<JobMasterValue> mStatusValues = new List<JobMasterValue>();

    protected override void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StatusItemTemplate, (UnityEngine.Object) null) && this.StatusItemTemplate.activeInHierarchy)
        this.StatusItemTemplate.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.NextButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnNextClick)));
    }

    public void Activated(int pinID)
    {
    }

    public void Refresh(BaseStatus old_status, BaseStatus new_status)
    {
      if (old_status == null || new_status == null)
        return;
      this.mStatusValues.Clear();
      string[] names = Enum.GetNames(typeof (ParamTypes));
      Array values = Enum.GetValues(typeof (ParamTypes));
      for (int index = 0; index < values.Length; ++index)
      {
        ParamTypes type = (ParamTypes) values.GetValue(index);
        switch (type)
        {
          case ParamTypes.None:
          case ParamTypes.HpMax:
            continue;
          default:
            int oldStatu = old_status[type];
            int newStatu = new_status[type];
            if (oldStatu != newStatu)
            {
              this.mStatusValues.Add(new JobMasterValue()
              {
                type = names[index],
                old_value = oldStatu,
                new_value = newStatu
              });
              continue;
            }
            continue;
        }
      }
      this.SetData((object[]) this.mStatusValues.ToArray(), typeof (JobMasterValue));
    }

    protected override GameObject CreateItem()
    {
      return UnityEngine.Object.Instantiate<GameObject>(this.StatusItemTemplate);
    }

    protected override void OnUpdateItem(GameObject go, int index)
    {
      StatusListItem component = go.GetComponent<StatusListItem>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      string type = this.mStatusValues[index].type;
      int oldValue = this.mStatusValues[index].old_value;
      int newValue = this.mStatusValues[index].new_value;
      ((Component) component).gameObject.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Label, (UnityEngine.Object) null))
        component.Label.text = LocalizedText.Get("sys." + type);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Value, (UnityEngine.Object) null))
        component.Value.text = oldValue.ToString();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Bonus, (UnityEngine.Object) null))
        return;
      if (newValue != 0)
      {
        component.Bonus.text = newValue.ToString();
        ((Component) component.Bonus).gameObject.SetActive(true);
      }
      else
        ((Component) component.Bonus).gameObject.SetActive(false);
    }

    private void OnNextClick()
    {
      if (this.Page < this.MaxPage - 1)
        this.GotoNextPage();
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
