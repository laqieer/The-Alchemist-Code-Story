// Decompiled with JetBrains decompiler
// Type: ScreenUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public static class ScreenUtility
{
  private static int mDefaultScreenWidth = 1920;
  private static int mDefaultScreenHeight = 1080;
  public const int MIN_WINDOW_WIDTH = 480;
  public const int MIN_WINDOW_HEIGHT = 270;
  public const int DEFAULT_WINDOW_WIDTH = 1920;
  public const int DEFAULT_WINDOW_HEIGHT = 1080;
  public const float ASPECT_RATIO = 1.77777779f;

  public static void SetResolution(int w, int h) => Screen.SetResolution(w, h, false);

  public static int DefaultScreenWidth => ScreenUtility.mDefaultScreenWidth;

  public static int DefaultScreenHeight => ScreenUtility.mDefaultScreenHeight;

  public static float ScreenWidthScale
  {
    get => (float) ScreenUtility.mDefaultScreenWidth / (float) Screen.width;
  }

  public static float ScreenHeightScale
  {
    get => (float) ScreenUtility.mDefaultScreenHeight / (float) Screen.height;
  }
}
