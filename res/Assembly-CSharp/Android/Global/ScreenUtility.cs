// Decompiled with JetBrains decompiler
// Type: ScreenUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public static class ScreenUtility
{
  private static int mDefaultScreenWidth = Screen.width;
  private static int mDefaultScreenHeight = Screen.height;

  public static void SetResolution(int w, int h)
  {
    Screen.SetResolution(w, h, true);
  }

  public static int DefaultScreenWidth
  {
    get
    {
      return ScreenUtility.mDefaultScreenWidth;
    }
  }

  public static int DefaultScreenHeight
  {
    get
    {
      return ScreenUtility.mDefaultScreenHeight;
    }
  }

  public static float ScreenWidthScale
  {
    get
    {
      return (float) ScreenUtility.mDefaultScreenWidth / (float) Screen.width;
    }
  }

  public static float ScreenHeightScale
  {
    get
    {
      return (float) ScreenUtility.mDefaultScreenHeight / (float) Screen.height;
    }
  }
}
