// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSkinListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitSkinListItem : ListItemEvents
  {
    public ListItemEvents.ListItemEvent OnSelectAll;
    public ListItemEvents.ListItemEvent OnRemoveAll;
    public SRPG_Button Button;
    public GameObject Lock;

    public void SelectAll()
    {
      if (this.OnSelectAll == null)
        return;
      this.OnSelectAll(((Component) this).gameObject);
    }

    public void RemoveAll()
    {
      if (this.OnRemoveAll == null)
        return;
      this.OnRemoveAll(((Component) this).gameObject);
    }
  }
}
