// Decompiled with JetBrains decompiler
// Type: SRPG.SortMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Open", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Restore State", FlowNode.PinTypes.Input, 2)]
  public class SortMenu : MonoBehaviour, IFlowInterface
  {
    public SortMenu.SortMenuItem[] Items = new SortMenu.SortMenuItem[0];
    public SortMenu.SortMenuItem[] Filters = new SortMenu.SortMenuItem[0];
    public bool LocalizeCaption;
    public string DefaultCaption;
    public Toggle Ascending;
    public Toggle Descending;
    public SortMenu.SortMenuEvent OnAccept;
    public Button ToggleFiltersOn;
    public Button ToggleFiltersOff;
    private bool mSelectedAscending;
    public bool UseFilterCaption;

    public void Activated(int pinID)
    {
      if (pinID != 2)
        return;
      this.RestoreState();
    }

    public void Reset()
    {
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if ((UnityEngine.Object) this.Items[index].Toggle != (UnityEngine.Object) null)
          GameUtility.SetToggle(this.Items[index].Toggle, false);
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.ToggleFiltersOff != (UnityEngine.Object) null)
        this.ToggleFiltersOff.onClick.AddListener(new UnityAction(this.SetAllFiltersOff));
      if (!((UnityEngine.Object) this.ToggleFiltersOn != (UnityEngine.Object) null))
        return;
      this.ToggleFiltersOn.onClick.AddListener(new UnityAction(this.SetAllFiltersOn));
    }

    public void SetAllFiltersOn()
    {
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if ((UnityEngine.Object) this.Filters[index].Toggle != (UnityEngine.Object) null)
          GameUtility.SetToggle(this.Filters[index].Toggle, true);
      }
    }

    public void SetAllFiltersOff()
    {
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if ((UnityEngine.Object) this.Filters[index].Toggle != (UnityEngine.Object) null)
          GameUtility.SetToggle(this.Filters[index].Toggle, false);
      }
    }

    public void Open()
    {
      this.RestoreState();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    public void SaveState()
    {
      this.mSelectedAscending = this.IsAscending;
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if ((UnityEngine.Object) this.Items[index].Toggle != (UnityEngine.Object) null)
          this.Items[index].LastState = this.Items[index].Toggle.isOn;
      }
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if ((UnityEngine.Object) this.Filters[index].Toggle != (UnityEngine.Object) null)
          this.Filters[index].LastState = this.Filters[index].Toggle.isOn;
      }
    }

    public void RestoreState()
    {
      this.IsAscending = this.mSelectedAscending;
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if ((UnityEngine.Object) this.Items[index].Toggle != (UnityEngine.Object) null)
          GameUtility.SetToggle(this.Items[index].Toggle, this.Items[index].LastState);
      }
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if ((UnityEngine.Object) this.Filters[index].Toggle != (UnityEngine.Object) null)
          GameUtility.SetToggle(this.Filters[index].Toggle, this.Filters[index].LastState);
      }
    }

    public void Accept()
    {
      this.SaveState();
      if (this.OnAccept == null)
        return;
      this.OnAccept(this);
    }

    public string CurrentCaption
    {
      get
      {
        for (int index = 0; index < this.Items.Length; ++index)
        {
          if ((UnityEngine.Object) this.Items[index].Toggle != (UnityEngine.Object) null && this.Items[index].Toggle.isOn)
          {
            if (this.LocalizeCaption)
              return LocalizedText.Get(this.Items[index].Caption);
            return this.Items[index].Caption;
          }
        }
        if (this.UseFilterCaption)
        {
          for (int index = 0; index < this.Filters.Length; ++index)
          {
            if ((UnityEngine.Object) this.Filters[index].Toggle != (UnityEngine.Object) null && this.Filters[index].Toggle.isOn)
            {
              if (this.LocalizeCaption)
                return LocalizedText.Get(this.Filters[index].Caption);
              return this.Filters[index].Caption;
            }
          }
        }
        if (this.LocalizeCaption)
          return LocalizedText.Get(this.DefaultCaption);
        return this.DefaultCaption;
      }
    }

    public string SortMethod
    {
      get
      {
        for (int index = 0; index < this.Items.Length; ++index)
        {
          if ((UnityEngine.Object) this.Items[index].Toggle != (UnityEngine.Object) null && this.Items[index].Toggle.isOn)
            return this.Items[index].Method;
        }
        return (string) null;
      }
      set
      {
        for (int index = 0; index < this.Items.Length; ++index)
        {
          if ((UnityEngine.Object) this.Items[index].Toggle != (UnityEngine.Object) null)
            GameUtility.SetToggle(this.Items[index].Toggle, this.Items[index].Method == value);
        }
      }
    }

    public bool IsAscending
    {
      get
      {
        return !this.IsDescending;
      }
      set
      {
        this.IsDescending = !value;
      }
    }

    public bool IsDescending
    {
      get
      {
        if ((UnityEngine.Object) this.Ascending != (UnityEngine.Object) null)
          return !this.Ascending.isOn;
        return false;
      }
      set
      {
        if ((UnityEngine.Object) this.Ascending != (UnityEngine.Object) null)
          GameUtility.SetToggle(this.Ascending, !value);
        if (!((UnityEngine.Object) this.Descending != (UnityEngine.Object) null))
          return;
        GameUtility.SetToggle(this.Descending, value);
      }
    }

    public bool Contains(string method)
    {
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (this.Items[index].Method == method)
          return true;
      }
      return false;
    }

    public string[] GetFilters(bool invert = false)
    {
      List<string> stringList = new List<string>();
      if (invert)
      {
        for (int index = 0; index < this.Filters.Length; ++index)
        {
          if ((UnityEngine.Object) this.Filters[index].Toggle != (UnityEngine.Object) null && !this.Filters[index].Toggle.isOn)
            stringList.Add(this.Filters[index].Method);
        }
        if (stringList.Count == 0)
          return (string[]) null;
      }
      else
      {
        for (int index = 0; index < this.Filters.Length; ++index)
        {
          if ((UnityEngine.Object) this.Filters[index].Toggle != (UnityEngine.Object) null && this.Filters[index].Toggle.isOn)
            stringList.Add(this.Filters[index].Method);
        }
        if (this.Filters.Length == stringList.Count)
          return (string[]) null;
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
          if (!string.IsNullOrEmpty(this.Filters[index].Method) && (UnityEngine.Object) this.Filters[index].Toggle != (UnityEngine.Object) null)
          {
            bool flag = Array.IndexOf<string>(filters, this.Filters[index].Method) >= 0;
            if (invert)
              flag = !flag;
            GameUtility.SetToggle(this.Filters[index].Toggle, flag);
          }
        }
      }
    }

    public delegate void SortMenuEvent(SortMenu menu);

    [Serializable]
    public struct SortMenuItem
    {
      public string Method;
      public Toggle Toggle;
      public string Caption;
      [NonSerialized]
      public bool LastState;
    }
  }
}
