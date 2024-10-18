// Decompiled with JetBrains decompiler
// Type: SRPG.CameraUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      Object.Destroy((Object) this);
    }

    public void Reset()
    {
      Camera component = ((Component) this).GetComponent<Camera>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.rect = this.CalcScreenAspect();
    }

    public float CalcAspectRatio(float w, float h) => 1f * w / h;

    private Rect CalcScreenAspect()
    {
      float num1 = this.CalcAspectRatio(this.mFixedWidth, this.mFixedHeight);
      float num2 = this.CalcAspectRatio((float) Screen.width, (float) Screen.height) / num1;
      Rect rect;
      // ISSUE: explicit constructor call
      ((Rect) ref rect).\u002Ector(0.0f, 0.0f, 1f, 1f);
      if (1.0 > (double) num2)
      {
        ((Rect) ref rect).x = 0.0f;
        ((Rect) ref rect).y = (float) ((1.0 - (double) num2) / 2.0);
        ((Rect) ref rect).width = 1f;
        ((Rect) ref rect).height = num2;
      }
      else
      {
        float num3 = 1f / num2;
        ((Rect) ref rect).x = (float) ((1.0 - (double) num3) / 2.0);
        ((Rect) ref rect).y = 0.0f;
        ((Rect) ref rect).width = num3;
        ((Rect) ref rect).height = 1f;
      }
      return rect;
    }
  }
}
