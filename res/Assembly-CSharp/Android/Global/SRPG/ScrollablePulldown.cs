// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollablePulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ScrollablePulldown : ScrollablePulldownBase
  {
    [SerializeField]
    private GameObject PulldownItemTemplate;

    protected override void Start()
    {
      base.Start();
      if (!((UnityEngine.Object) this.PulldownItemTemplate != (UnityEngine.Object) null))
        return;
      this.PulldownItemTemplate.gameObject.SetActive(false);
    }

    protected override void OnDestroy()
    {
      this.ClearItems();
      base.OnDestroy();
    }

    public PulldownItem AddItem(string label, int value)
    {
      if ((UnityEngine.Object) this.PulldownItemTemplate == (UnityEngine.Object) null)
        return (PulldownItem) null;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PulldownItemTemplate);
      gameObject.GetComponent<SRPG_Button>().AddListener((SRPG_Button.ButtonClickEvent) (g =>
      {
        this.Selection = value;
        this.ClosePulldown(false);
        this.TriggerItemChange();
      }));
      PulldownItem component = gameObject.GetComponent<PulldownItem>();
      if ((UnityEngine.Object) component.Text != (UnityEngine.Object) null)
        component.Text.text = label;
      component.Value = value;
      this.Items.Add(component);
      gameObject.transform.SetParent((Transform) this.ItemHolder, false);
      gameObject.SetActive(true);
      this.ScrollRect.verticalNormalizedPosition = 1f;
      this.ScrollRect.horizontalNormalizedPosition = 1f;
      return component;
    }

    public void ClearItems()
    {
      for (int index = 0; index < this.Items.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.Items[index].gameObject);
      this.Items.Clear();
      this.ResetAllStatus();
    }
  }
}
