// Decompiled with JetBrains decompiler
// Type: AdjustScaleObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AdjustScaleObject : MonoBehaviour
{
  [SerializeField]
  private float SetPower = 1f;
  private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  private Vector3 initScale = Vector3.one;
  private float lastSetPower;

  private void Start()
  {
    this.initScale = this.transform.localScale;
    this.ApplySafeAreaScale(SetCanvasBounds.GetSafeArea());
  }

  private void ApplySafeAreaScale(Rect area)
  {
    if ((double) (area.width / (float) Screen.width) < 1.0)
      this.transform.localScale = this.SetPower * this.initScale;
    else
      this.transform.localScale = this.initScale;
    this.lastSafeArea = area;
    this.lastSetPower = this.SetPower;
  }
}
