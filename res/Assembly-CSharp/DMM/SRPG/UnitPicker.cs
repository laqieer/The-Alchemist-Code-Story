// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPicker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  public class UnitPicker : UIBehaviour
  {
    private Animator mAnimator;
    public string OpenTrigger;
    public string CloseTrigger;
    public float CloseDelay1;
    public float CloseDelay2;
    public ListItemEvents Item_Remove;
    public ListItemEvents Item_Inactive;
    public ListItemEvents Item_Active;

    protected virtual void Awake()
    {
      base.Awake();
      this.mAnimator = ((Component) this).GetComponent<Animator>();
    }

    protected virtual void Start()
    {
      base.Start();
      this.mAnimator.SetTrigger(this.OpenTrigger);
    }

    public void Refresh(List<UnitData> inactive, List<UnitData> active)
    {
    }

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
    }
  }
}
