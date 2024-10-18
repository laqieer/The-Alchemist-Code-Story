// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPicker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    protected override void Awake()
    {
      base.Awake();
      this.mAnimator = this.GetComponent<Animator>();
    }

    protected override void Start()
    {
      base.Start();
      this.mAnimator.SetTrigger(this.OpenTrigger);
    }

    public void Refresh(List<UnitData> inactive, List<UnitData> active)
    {
    }

    protected override void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
    }
  }
}
