﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPickerSort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Output, 10)]
  public class UnitPickerSort : MonoBehaviour, IFlowInterface
  {
    public string MenuID = "UNITLIST";
    public SortMenu.SortMenuItem[] Items = new SortMenu.SortMenuItem[0];
    public bool LocalizeCaption;
    public string DefaultCaption;
    public bool UseFilterCaption;
    public bool mSelectedAscending;
    public Toggle Ascending;
    public Toggle Descending;
    public SRPG_Button DecideButton;
    public UnitPickerSort.SortEvent OnAccept;

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
      if (!((UnityEngine.Object) this.DecideButton != (UnityEngine.Object) null))
        return;
      this.DecideButton.onClick.AddListener(new UnityAction(this.Accept));
    }

    private void Accept()
    {
      this.SaveState();
      PlayerPrefsUtility.SetString(this.MenuID, this.SortMethod, false);
      PlayerPrefsUtility.SetInt(this.MenuID + "#", !this.IsAscending ? 0 : 1, false);
      PlayerPrefsUtility.Save();
      if (this.OnAccept == null)
        return;
      this.OnAccept(this.SortMethod, this.IsAscending);
    }

    public void SetUp(string method)
    {
      if (PlayerPrefsUtility.HasKey(this.MenuID))
        this.SortMethod = PlayerPrefsUtility.GetString(this.MenuID, "TIME");
      if (PlayerPrefsUtility.HasKey(this.MenuID + "#"))
        this.IsAscending = PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) != 0;
      this.SaveState();
    }

    public void SetUp(string method, bool ascending = false)
    {
      this.SortMethod = string.IsNullOrEmpty(method) ? "TIME" : method;
      this.IsAscending = ascending;
      this.SaveState();
    }

    public void SaveState()
    {
      this.mSelectedAscending = this.IsAscending;
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if ((UnityEngine.Object) this.Items[index].Toggle != (UnityEngine.Object) null)
          this.Items[index].LastState = this.Items[index].Toggle.isOn;
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
        if (this.LocalizeCaption)
          return LocalizedText.Get(this.DefaultCaption);
        return this.DefaultCaption;
      }
    }

    public string SortMethod
    {
      get
      {
        if (this.Items != null && this.Items.Length > 0)
        {
          for (int index = 0; index < this.Items.Length; ++index)
          {
            if ((UnityEngine.Object) this.Items[index].Toggle != (UnityEngine.Object) null && this.Items[index].Toggle.isOn)
              return this.Items[index].Method;
          }
        }
        return (string) null;
      }
      set
      {
        if (this.Items == null || this.Items.Length <= 0)
          return;
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

    public string GetSortMethod()
    {
      string empty = string.Empty;
      string lower = this.SortMethod.ToLower();
      return ((int) char.ToUpper(lower[0])).ToString() + lower.Substring(1);
    }

    public delegate void SortEvent(string method, bool ascending);
  }
}
