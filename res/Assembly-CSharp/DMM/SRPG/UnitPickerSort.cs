// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPickerSort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Output, 10)]
  public class UnitPickerSort : MonoBehaviour, IFlowInterface
  {
    public string MenuID = "UNITLIST";
    public bool LocalizeCaption;
    public string DefaultCaption;
    public bool UseFilterCaption;
    public bool mSelectedAscending;
    public Toggle Ascending;
    public Toggle Descending;
    public SortMenu.SortMenuItem[] Items = new SortMenu.SortMenuItem[0];
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
      if (!Object.op_Inequality((Object) this.DecideButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.DecideButton.onClick).AddListener(new UnityAction((object) this, __methodptr(Accept)));
    }

    private void Accept()
    {
      this.SaveState();
      PlayerPrefsUtility.SetString(this.MenuID, this.SortMethod);
      PlayerPrefsUtility.SetInt(this.MenuID + "#", !this.IsAscending ? 0 : 1);
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
        this.IsAscending = PlayerPrefsUtility.GetInt(this.MenuID + "#") != 0;
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
        if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null))
          this.Items[index].LastState = this.Items[index].Toggle.isOn;
      }
    }

    public void RestoreState()
    {
      this.IsAscending = this.mSelectedAscending;
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null))
          GameUtility.SetToggle(this.Items[index].Toggle, this.Items[index].LastState);
      }
    }

    public string CurrentCaption
    {
      get
      {
        for (int index = 0; index < this.Items.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null) && this.Items[index].Toggle.isOn)
            return this.LocalizeCaption ? LocalizedText.Get(this.Items[index].Caption) : this.Items[index].Caption;
        }
        return this.LocalizeCaption ? LocalizedText.Get(this.DefaultCaption) : this.DefaultCaption;
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
            if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null) && this.Items[index].Toggle.isOn)
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
          if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null))
            GameUtility.SetToggle(this.Items[index].Toggle, this.Items[index].Method == value);
        }
      }
    }

    public bool IsAscending
    {
      get => !this.IsDescending;
      set => this.IsDescending = !value;
    }

    public bool IsDescending
    {
      get => Object.op_Inequality((Object) this.Ascending, (Object) null) && !this.Ascending.isOn;
      set
      {
        if (Object.op_Inequality((Object) this.Ascending, (Object) null))
          GameUtility.SetToggle(this.Ascending, !value);
        if (!Object.op_Inequality((Object) this.Descending, (Object) null))
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
      return char.ToUpper(lower[0]).ToString() + lower.Substring(1);
    }

    public delegate void SortEvent(string method, bool ascending);
  }
}
