// Decompiled with JetBrains decompiler
// Type: SRPG.SortMenuButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(101, "Open", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(111, "Close", FlowNode.PinTypes.Output, 111)]
  public class SortMenuButton : SRPG_Button, IFlowInterface
  {
    private const int PIN_OUT_OPEN = 101;
    private const int PIN_OUT_CLOSED = 111;
    public GameObject Target;
    public SortMenu Menu;
    public Text Caption;
    private SortMenu mMenu;
    private GameObject mMenuObject;
    public string MenuID;
    public string FilterActive;
    public bool CreateMenuInstance = true;

    public void OpenSortMenu()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      this.mMenu.Open();
    }

    public void Activated(int PinID)
    {
    }

    private void OnSortChange(SortMenu menu)
    {
      string sortMethod = menu.SortMethod;
      if (!string.IsNullOrEmpty(this.MenuID))
      {
        PlayerPrefsUtility.SetString(this.MenuID, sortMethod);
        PlayerPrefsUtility.SetInt(this.MenuID + "#", !this.mMenu.IsAscending ? 0 : 1);
        string[] filters = this.mMenu.GetFilters(true);
        if (filters == null || filters.Length <= 0)
        {
          this.mMenu.SetAllFiltersOff();
          this.mMenu.SaveState();
          filters = this.mMenu.GetFilters(true);
        }
        PlayerPrefsUtility.SetString(this.MenuID + "&", filters == null ? string.Empty : string.Join("|", filters));
        PlayerPrefsUtility.Save();
      }
      if (Object.op_Inequality((Object) this.Caption, (Object) null))
        this.Caption.text = this.mMenu.CurrentCaption;
      this.UpdateTarget(sortMethod, menu.IsAscending);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }

    protected virtual void Awake()
    {
      ((Selectable) this).Awake();
      if (!Application.isPlaying)
        return;
      if (Object.op_Inequality((Object) this.Menu, (Object) null))
      {
        if (this.CreateMenuInstance)
        {
          this.mMenu = Object.Instantiate<SortMenu>(this.Menu);
          this.mMenu.OnAccept = new SortMenu.SortMenuEvent(this.OnSortChange);
        }
        else
        {
          this.mMenu = this.Menu;
          this.mMenu.OnAccept = new SortMenu.SortMenuEvent(this.OnSortChange);
        }
      }
      // ISSUE: method pointer
      ((UnityEvent) this.onClick).AddListener(new UnityAction((object) this, __methodptr(OpenSortMenu)));
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      if (!Application.isPlaying || !Object.op_Inequality((Object) this.mMenu, (Object) null))
        return;
      string method = (string) null;
      if (!string.IsNullOrEmpty(this.MenuID))
      {
        string str1 = !PlayerPrefsUtility.HasKey(this.MenuID) ? this.mMenu.SortMethod : PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        method = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        this.mMenu.IsAscending = PlayerPrefsUtility.GetInt(this.MenuID + "#") != 0;
        if (this.mMenu.Contains(method))
          this.mMenu.SortMethod = method;
        string key = this.MenuID + "&";
        if (PlayerPrefsUtility.HasKey(key))
        {
          string str2 = PlayerPrefsUtility.GetString(key, string.Empty);
          if (string.IsNullOrEmpty(str2))
            this.mMenu.SetAllFiltersOff();
          else
            this.mMenu.SetFilters(str2.Split('|'), true);
        }
      }
      this.mMenu.SaveState();
      if (Object.op_Inequality((Object) this.Caption, (Object) null))
        this.Caption.text = this.mMenu.CurrentCaption;
      this.UpdateTarget(method, this.mMenu.IsAscending);
    }

    protected virtual void UpdateFilterState(bool active)
    {
      if (string.IsNullOrEmpty(this.FilterActive))
        return;
      Animator component = ((Component) this).GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetBool(this.FilterActive, active);
    }

    protected virtual void OnEnable()
    {
      ((Selectable) this).OnEnable();
      if (!Object.op_Inequality((Object) this.mMenu, (Object) null))
        return;
      this.UpdateFilterState(this.mMenu.GetFilters() != null);
    }

    private void UpdateTarget(string method, bool ascending)
    {
      if (Object.op_Equality((Object) this.mMenu, (Object) null))
        return;
      string[] filters = this.mMenu.GetFilters();
      this.UpdateFilterState(filters != null);
      if (!Object.op_Inequality((Object) this.Target, (Object) null))
        return;
      this.Target.GetComponent<ISortableList>()?.SetSortMethod(method, ascending, filters);
    }

    protected virtual void OnDestroy()
    {
      if (Object.op_Inequality((Object) this.mMenu, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mMenu).gameObject);
        this.mMenu = (SortMenu) null;
      }
      ((UIBehaviour) this).OnDestroy();
    }

    public void ForceReloadFilter()
    {
      if (!Application.isPlaying || !Object.op_Inequality((Object) this.mMenu, (Object) null))
        return;
      string str = (string) null;
      if (!string.IsNullOrEmpty(this.MenuID))
      {
        str = !PlayerPrefsUtility.HasKey(this.MenuID) ? this.mMenu.SortMethod : PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        string method = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        this.mMenu.IsAscending = PlayerPrefsUtility.GetInt(this.MenuID + "#") != 0;
        if (this.mMenu.Contains(method))
          this.mMenu.SortMethod = method;
        string key = this.MenuID + "&";
        if (PlayerPrefsUtility.HasKey(key))
          this.mMenu.SetFilters(PlayerPrefsUtility.GetString(key, string.Empty).Split('|'), true);
      }
      this.mMenu.SaveState();
      if (!Object.op_Inequality((Object) this.Caption, (Object) null))
        return;
      this.Caption.text = this.mMenu.CurrentCaption;
    }
  }
}
