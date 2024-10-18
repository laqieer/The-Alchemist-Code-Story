// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollablePulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ScrollablePulldown : ScrollablePulldownBase
  {
    [SerializeField]
    private GameObject PulldownItemTemplate;

    protected override void Start()
    {
      base.Start();
      if (!Object.op_Inequality((Object) this.PulldownItemTemplate, (Object) null))
        return;
      this.PulldownItemTemplate.gameObject.SetActive(false);
    }

    protected override void OnDestroy()
    {
      this.ClearItems();
      base.OnDestroy();
    }

    public PulldownItem AddItem(string label, int value, bool is_lock = false)
    {
      if (Object.op_Equality((Object) this.PulldownItemTemplate, (Object) null))
        return (PulldownItem) null;
      GameObject gameObject = Object.Instantiate<GameObject>(this.PulldownItemTemplate);
      SRPG_Button component1 = gameObject.GetComponent<SRPG_Button>();
      component1.AddListener((SRPG_Button.ButtonClickEvent) (g =>
      {
        this.Selection = value;
        this.ClosePulldown(false);
        this.TriggerItemChange();
      }));
      PulldownItem component2 = gameObject.GetComponent<PulldownItem>();
      if (Object.op_Inequality((Object) component2.Text, (Object) null))
        component2.Text.text = label;
      component2.Value = value;
      this.Items.Add(component2);
      gameObject.transform.SetParent((Transform) this.ItemHolder, false);
      gameObject.SetActive(true);
      this.ScrollRect.verticalNormalizedPosition = 1f;
      this.ScrollRect.horizontalNormalizedPosition = 1f;
      if (Object.op_Inequality((Object) component2, (Object) null) && Object.op_Inequality((Object) component2.LockObject, (Object) null))
      {
        ((Selectable) component1).interactable = !is_lock;
        GameUtility.SetGameObjectActive(component2.LockObject, is_lock);
      }
      return component2;
    }

    public void ClearItems()
    {
      for (int index = 0; index < this.Items.Count; ++index)
        Object.Destroy((Object) ((Component) this.Items[index]).gameObject);
      this.Items.Clear();
      this.ResetAllStatus();
    }
  }
}
