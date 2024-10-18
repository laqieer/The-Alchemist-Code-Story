// Decompiled with JetBrains decompiler
// Type: SRPG.SortMenuButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class SortMenuButton : SRPG_Button
  {
    public bool CreateMenuInstance = true;
    public GameObject Target;
    public SortMenu Menu;
    public Text Caption;
    private SortMenu mMenu;
    private GameObject mMenuObject;
    public string MenuID;
    public string FilterActive;

    public void OpenSortMenu()
    {
      this.mMenu.Open();
    }

    private void OnSortChange(SortMenu menu)
    {
      string sortMethod = menu.SortMethod;
      if (!string.IsNullOrEmpty(this.MenuID))
      {
        PlayerPrefsUtility.SetString(this.MenuID, sortMethod, false);
        PlayerPrefsUtility.SetInt(this.MenuID + "#", !this.mMenu.IsAscending ? 0 : 1, false);
        string[] filters = this.mMenu.GetFilters(true);
        PlayerPrefsUtility.SetString(this.MenuID + "&", filters == null ? string.Empty : string.Join("|", filters), false);
        PlayerPrefsUtility.Save();
      }
      if ((UnityEngine.Object) this.Caption != (UnityEngine.Object) null)
        this.Caption.text = this.mMenu.CurrentCaption;
      this.UpdateTarget(sortMethod, menu.IsAscending);
    }

    protected override void Awake()
    {
      base.Awake();
      if (!Application.isPlaying)
        return;
      if ((UnityEngine.Object) this.Menu != (UnityEngine.Object) null)
      {
        if (this.CreateMenuInstance)
        {
          this.mMenu = UnityEngine.Object.Instantiate<SortMenu>(this.Menu);
          this.mMenu.OnAccept = new SortMenu.SortMenuEvent(this.OnSortChange);
        }
        else
        {
          this.mMenu = this.Menu;
          this.mMenu.OnAccept = new SortMenu.SortMenuEvent(this.OnSortChange);
        }
      }
      this.onClick.AddListener(new UnityAction(this.OpenSortMenu));
    }

    protected override void Start()
    {
      base.Start();
      if (!Application.isPlaying || !((UnityEngine.Object) this.mMenu != (UnityEngine.Object) null))
        return;
      string method = (string) null;
      if (!string.IsNullOrEmpty(this.MenuID))
      {
        string str = !PlayerPrefsUtility.HasKey(this.MenuID) ? this.mMenu.SortMethod : PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        method = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        this.mMenu.IsAscending = PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) != 0;
        if (this.mMenu.Contains(method))
          this.mMenu.SortMethod = method;
        string key = this.MenuID + "&";
        if (PlayerPrefsUtility.HasKey(key))
          this.mMenu.SetFilters(PlayerPrefsUtility.GetString(key, string.Empty).Split('|'), true);
      }
      this.mMenu.SaveState();
      if ((UnityEngine.Object) this.Caption != (UnityEngine.Object) null)
        this.Caption.text = this.mMenu.CurrentCaption;
      this.UpdateTarget(method, this.mMenu.IsAscending);
    }

    protected virtual void UpdateFilterState(bool active)
    {
      if (string.IsNullOrEmpty(this.FilterActive))
        return;
      Animator component = this.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SetBool(this.FilterActive, active);
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      if (!((UnityEngine.Object) this.mMenu != (UnityEngine.Object) null))
        return;
      this.UpdateFilterState(this.mMenu.GetFilters(false) != null);
    }

    private void UpdateTarget(string method, bool ascending)
    {
      if ((UnityEngine.Object) this.mMenu == (UnityEngine.Object) null)
        return;
      string[] filters = this.mMenu.GetFilters(false);
      this.UpdateFilterState(filters != null);
      if (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null))
        return;
      this.Target.GetComponent<ISortableList>()?.SetSortMethod(method, ascending, filters);
    }

    protected override void OnDestroy()
    {
      if ((UnityEngine.Object) this.mMenu != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mMenu.gameObject);
        this.mMenu = (SortMenu) null;
      }
      base.OnDestroy();
    }

    public void ForceReloadFilter()
    {
      if (!Application.isPlaying || !((UnityEngine.Object) this.mMenu != (UnityEngine.Object) null))
        return;
      string str = (string) null;
      if (!string.IsNullOrEmpty(this.MenuID))
      {
        str = !PlayerPrefsUtility.HasKey(this.MenuID) ? this.mMenu.SortMethod : PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        string method = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        this.mMenu.IsAscending = PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) != 0;
        if (this.mMenu.Contains(method))
          this.mMenu.SortMethod = method;
        string key = this.MenuID + "&";
        if (PlayerPrefsUtility.HasKey(key))
          this.mMenu.SetFilters(PlayerPrefsUtility.GetString(key, string.Empty).Split('|'), true);
      }
      this.mMenu.SaveState();
      if (!((UnityEngine.Object) this.Caption != (UnityEngine.Object) null))
        return;
      this.Caption.text = this.mMenu.CurrentCaption;
    }
  }
}
