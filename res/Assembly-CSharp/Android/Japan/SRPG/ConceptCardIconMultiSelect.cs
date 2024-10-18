// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardIconMultiSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardIconMultiSelect : ConceptCardIcon
  {
    [SerializeField]
    private GameObject mDisable;
    [SerializeField]
    private GameObject mSelect;

    public void RefreshEnableParam(bool enable)
    {
      if (this.ConceptCard == null)
        return;
      if ((UnityEngine.Object) this.mDisable != (UnityEngine.Object) null)
        this.mDisable.SetActive(!enable);
      Button component = this.transform.gameObject.GetComponent<Button>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.enabled = enable;
    }

    public void RefreshSelectParam(bool selected)
    {
      if (this.ConceptCard == null || !((UnityEngine.Object) this.mSelect != (UnityEngine.Object) null))
        return;
      this.mSelect.SetActive(selected);
    }
  }
}
