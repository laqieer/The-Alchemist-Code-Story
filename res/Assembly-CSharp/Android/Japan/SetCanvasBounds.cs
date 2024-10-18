// Decompiled with JetBrains decompiler
// Type: SetCanvasBounds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SetCanvasBounds : MonoBehaviour
{
  private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  [SerializeField]
  private bool IgnoreApplySafeAreaFlag;
  public RectTransform panel;

  public static Rect GetSafeArea()
  {
    return new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height);
  }

  public static float CalcCanvasBoundsScale()
  {
    return SetCanvasBounds.GetSafeArea().width / (float) Screen.width;
  }

  private void Start()
  {
    if (this.IgnoreApplySafeAreaFlag)
      return;
    this.ApplySafeAreaScale(SetCanvasBounds.GetSafeArea());
  }

  private void ApplySafeAreaScale(Rect area)
  {
    if ((Object) this.panel != (Object) null)
    {
      float num = area.width / (float) Screen.width;
      Vector3 vector3 = new Vector3(num, num, num);
      this.panel.sizeDelta = new Vector2(0.0f, 750f);
      this.panel.anchorMin = new Vector2(0.0f, 1f);
      this.panel.anchorMax = new Vector2(1f, 1f);
      this.panel.pivot = new Vector2(0.5f, 1f);
      this.panel.localScale = vector3;
    }
    this.lastSafeArea = area;
  }
}
