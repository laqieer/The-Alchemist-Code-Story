// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPickerFilter
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
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Restore State", FlowNode.PinTypes.Input, 2)]
  public class UnitPickerFilter : MonoBehaviour, IFlowInterface
  {
    public string MenuID = "UNITLIST";
    public bool LocalizeCaption;
    public string DefaultCaption;
    public bool UseFilterCaption;
    public Button ToggleFiltersOn;
    public Button ToggleFiltersOff;
    public SortMenu.SortMenuItem[] Filters = new SortMenu.SortMenuItem[0];
    public SRPG_Button DecideButton;
    public UnitPickerFilter.FilterEvent OnAccept;

    public void Activated(int pinID)
    {
    }

    public void Open()
    {
      this.RestoreState();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecideButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DecideButton.onClick).AddListener(new UnityAction((object) this, __methodptr(Accecpt)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleFiltersOff, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ToggleFiltersOff.onClick).AddListener(new UnityAction((object) this, __methodptr(SetAllFiltersOff)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleFiltersOn, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ToggleFiltersOn.onClick).AddListener(new UnityAction((object) this, __methodptr(SetAllFiltersOn)));
    }

    private void Accecpt()
    {
      this.SaveState();
      string[] filters = this.GetFilters(true);
      PlayerPrefsUtility.SetString(this.MenuID + "&", filters == null ? string.Empty : string.Join("&", filters), true);
      if (this.OnAccept == null)
        return;
      this.OnAccept(this.GetFilters());
    }

    public void SaveState()
    {
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          this.Filters[index].LastState = this.Filters[index].Toggle.isOn;
      }
    }

    public void RestoreState()
    {
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.Filters[index].Toggle, this.Filters[index].LastState);
      }
    }

    public string CurrentCaption
    {
      get
      {
        if (this.UseFilterCaption)
        {
          for (int index = 0; index < this.Filters.Length; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null) && this.Filters[index].Toggle.isOn)
              return this.LocalizeCaption ? LocalizedText.Get(this.Filters[index].Caption) : this.Filters[index].Caption;
          }
        }
        return this.LocalizeCaption ? LocalizedText.Get(this.DefaultCaption) : this.DefaultCaption;
      }
    }

    public void SetAllFiltersOn()
    {
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.Filters[index].Toggle, true);
      }
    }

    public void SetAllFiltersOff()
    {
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.Filters[index].Toggle, false);
      }
    }

    public string[] GetFilters(bool invert = false)
    {
      List<string> stringList = new List<string>();
      if (invert)
      {
        for (int index = 0; index < this.Filters.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null) && !this.Filters[index].Toggle.isOn)
            stringList.Add(this.Filters[index].Method);
        }
        if (stringList.Count == 0)
          return (string[]) null;
      }
      else
      {
        for (int index = 0; index < this.Filters.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null) && this.Filters[index].Toggle.isOn)
            stringList.Add(this.Filters[index].Method);
        }
        if (this.Filters.Length == stringList.Count)
          return (string[]) null;
      }
      return stringList.ToArray();
    }

    public string[] GetFiltersAll()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null) && this.Filters[index].Toggle.isOn)
          stringList.Add(this.Filters[index].Method);
      }
      return stringList.ToArray();
    }

    public void SetFilters(string[] filters, bool invert = false)
    {
      if (filters == null || filters.Length == 0)
      {
        if (invert)
          this.SetAllFiltersOn();
        else
          this.SetAllFiltersOff();
      }
      else
      {
        for (int index = 0; index < this.Filters.Length; ++index)
        {
          if (!string.IsNullOrEmpty(this.Filters[index].Method) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          {
            bool flag = Array.IndexOf<string>(filters, this.Filters[index].Method) >= 0;
            if (invert)
              flag = !flag;
            GameUtility.SetToggle(this.Filters[index].Toggle, flag);
          }
        }
      }
    }

    public delegate void FilterEvent(string[] filter);
  }
}
