// Decompiled with JetBrains decompiler
// Type: SRPG.JukeBoxItemSectionNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  public class JukeBoxItemSectionNode : ContentNode
  {
    [SerializeField]
    private ImageArray ImageArraySection;
    [Space(5f)]
    [SerializeField]
    private GameObject GoActive;
    [SerializeField]
    private GameObject NewBadge;
    [Space(5f)]
    [SerializeField]
    private SRPG_Button BtnSelect;
    private JukeBoxItemSectionParam mParam;

    public JukeBoxItemSectionParam Param => this.mParam;

    public void Setup(
      JukeBoxItemSectionParam param,
      bool is_current,
      bool is_new,
      UnityAction action = null)
    {
      if (param == null || param.SectionParam == null)
        return;
      this.mParam = param;
      if (Object.op_Implicit((Object) this.ImageArraySection))
        this.ImageArraySection.ImageIndex = this.mParam.id;
      if (action != null && Object.op_Implicit((Object) this.BtnSelect))
      {
        ((UnityEventBase) this.BtnSelect.onClick).RemoveAllListeners();
        ((UnityEvent) this.BtnSelect.onClick).AddListener(action);
      }
      if (Object.op_Implicit((Object) this.GoActive))
        this.GoActive.SetActive(false);
      if (Object.op_Implicit((Object) this.NewBadge))
        this.NewBadge.SetActive(false);
      this.SetCurrent(is_current);
      this.SetNewBadge(is_new);
    }

    public void SetCurrent(bool is_active)
    {
      if (!Object.op_Implicit((Object) this.GoActive))
        return;
      this.GoActive.SetActive(is_active);
    }

    public bool IsCurrent()
    {
      if (Object.op_Implicit((Object) this.GoActive))
        this.GoActive.GetActive();
      return false;
    }

    public void SetNewBadge(bool is_new)
    {
      if (!Object.op_Implicit((Object) this.NewBadge))
        return;
      this.NewBadge.SetActive(is_new);
    }
  }
}
