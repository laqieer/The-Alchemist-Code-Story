// Decompiled with JetBrains decompiler
// Type: SRPG.SkillTargetWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class SkillTargetWindow : MonoBehaviour
  {
    public SkillTargetWindow.TargetSelectEvent OnTargetSelect;
    public SkillTargetWindow.CancelEvent OnCancel;
    private WindowController mWC;

    private void Start()
    {
      this.mWC = this.GetComponent<WindowController>();
    }

    public void Show()
    {
      if (!((UnityEngine.Object) this.mWC != (UnityEngine.Object) null))
        return;
      this.mWC.Open();
    }

    public void Hide()
    {
      if (!((UnityEngine.Object) this.mWC != (UnityEngine.Object) null))
        return;
      this.mWC.Close();
    }

    public void ForceHide()
    {
      if (!((UnityEngine.Object) this.mWC != (UnityEngine.Object) null))
        return;
      this.mWC.ForceClose();
    }

    public void UnitSelected()
    {
      if (this.OnTargetSelect == null)
        return;
      this.OnTargetSelect(false);
      this.Hide();
    }

    public void GridSelected()
    {
      if (this.OnTargetSelect == null)
        return;
      this.OnTargetSelect(true);
      this.Hide();
    }

    public void Cancel()
    {
      if (this.OnCancel == null)
        return;
      this.OnCancel();
      this.Hide();
    }

    public delegate void TargetSelectEvent(bool grid);

    public delegate void CancelEvent();
  }
}
