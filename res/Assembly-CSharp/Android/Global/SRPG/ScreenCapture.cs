// Decompiled with JetBrains decompiler
// Type: SRPG.ScreenCapture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  internal class ScreenCapture : MonoBehaviour
  {
    internal byte[] Image;
    internal bool isCapturing;

    internal void SaveScreenshot(bool changeOrientation)
    {
      this.isCapturing = true;
      this.StartCoroutine(this.SaveScreenshot_ReadPixelsAsynch(changeOrientation));
    }

    [DebuggerHidden]
    private IEnumerator SaveScreenshot_ReadPixelsAsynch(bool changeOrientation)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ScreenCapture.\u003CSaveScreenshot_ReadPixelsAsynch\u003Ec__Iterator45() { changeOrientation = changeOrientation, \u003C\u0024\u003EchangeOrientation = changeOrientation, \u003C\u003Ef__this = this };
    }

    private Texture2D LandscapeToPortrait(Texture2D origTex)
    {
      Color[] pixels = origTex.GetPixels();
      Color[] colors = new Color[pixels.Length];
      for (int index1 = 0; index1 < origTex.width; ++index1)
      {
        for (int index2 = 0; index2 < origTex.height; ++index2)
        {
          int num1 = index2;
          int num2 = origTex.width - index1 - 1;
          colors[num2 * origTex.height + num1] = pixels[index2 * origTex.width + index1];
        }
      }
      Texture2D texture2D = new Texture2D(origTex.height, origTex.width);
      texture2D.SetPixels(colors);
      texture2D.Apply();
      return texture2D;
    }
  }
}
