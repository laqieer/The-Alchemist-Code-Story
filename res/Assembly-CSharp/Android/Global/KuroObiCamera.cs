﻿// Decompiled with JetBrains decompiler
// Type: KuroObiCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Camera))]
[DisallowMultipleComponent]
public class KuroObiCamera : MonoBehaviour
{
  private Camera mCamera;

  private void Start()
  {
    this.mCamera = this.GetComponent<Camera>();
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
      float num2 = 1.6f;
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
