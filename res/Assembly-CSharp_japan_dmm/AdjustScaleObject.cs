// Decompiled with JetBrains decompiler
// Type: AdjustScaleObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AdjustScaleObject : MonoBehaviour
{
  [SerializeField]
  private float SetPower = 1f;
  private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  private Vector3 initScale = Vector3.one;
  private float lastSetPower;

  private void Start()
  {
    this.initScale = ((Component) this).transform.localScale;
    this.ApplySafeAreaScale(SetCanvasBounds.GetSafeArea());
  }

  private void ApplySafeAreaScale(Rect area)
  {
    if ((double) (((Rect) ref area).width / (float) Screen.width) < 1.0)
      ((Component) this).transform.localScale = Vector3.op_Multiply(this.SetPower, this.initScale);
    else
      ((Component) this).transform.localScale = this.initScale;
    this.lastSafeArea = area;
    this.lastSetPower = this.SetPower;
  }
}
