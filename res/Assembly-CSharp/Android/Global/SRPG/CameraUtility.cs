// Decompiled with JetBrains decompiler
// Type: SRPG.CameraUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Camera/Utility")]
  public class CameraUtility : MonoBehaviour
  {
    private float mFixedWidth = 720f;
    private float mFixedHeight = 1280f;

    private void Awake()
    {
      this.Reset();
      UnityEngine.Object.Destroy((UnityEngine.Object) this);
    }

    public void Reset()
    {
      UnityEngine.Camera component = this.GetComponent<UnityEngine.Camera>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.rect = this.CalcScreenAspect();
    }

    public float CalcAspectRatio(float w, float h)
    {
      return 1f * w / h;
    }

    private Rect CalcScreenAspect()
    {
      float num1 = this.CalcAspectRatio((float) Screen.width, (float) Screen.height) / this.CalcAspectRatio(this.mFixedWidth, this.mFixedHeight);
      Rect rect = new Rect(0.0f, 0.0f, 1f, 1f);
      if (1.0 > (double) num1)
      {
        rect.x = 0.0f;
        rect.y = (float) ((1.0 - (double) num1) / 2.0);
        rect.width = 1f;
        rect.height = num1;
      }
      else
      {
        float num2 = 1f / num1;
        rect.x = (float) ((1.0 - (double) num2) / 2.0);
        rect.y = 0.0f;
        rect.width = num2;
        rect.height = 1f;
      }
      return rect;
    }
  }
}
