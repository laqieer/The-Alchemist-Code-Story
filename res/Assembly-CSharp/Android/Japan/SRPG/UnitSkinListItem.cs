// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSkinListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      this.OnSelectAll(this.gameObject);
    }

    public void RemoveAll()
    {
      if (this.OnRemoveAll == null)
        return;
      this.OnRemoveAll(this.gameObject);
    }
  }
}
