// Decompiled with JetBrains decompiler
// Type: KuroObiCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Camera))]
[DisallowMultipleComponent]
public class KuroObiCamera : MonoBehaviour
{
  private Camera mCamera;

  private void Start()
  {
    this.mCamera = ((Component) this).GetComponent<Camera>();
    this.Update();
  }

  private void Update()
  {
    if (!SRPG_CanvasScaler.UseKuroObi)
    {
      this.mCamera.rect = new Rect(0.0f, 0.0f, 1f, 1f);
    }
    else
    {
      float num1 = (float) Screen.width / (float) Screen.height;
      float num2 = 1.77866662f;
      if ((double) num1 >= (double) num2)
      {
        this.mCamera.rect = new Rect(0.0f, 0.0f, 1f, 1f);
      }
      else
      {
        float num3 = (float) (1.0 - (double) num1 / (double) num2);
        this.mCamera.rect = new Rect(0.0f, num3 * 0.5f, 1f, 1f - num3);
      }
    }
  }
}
