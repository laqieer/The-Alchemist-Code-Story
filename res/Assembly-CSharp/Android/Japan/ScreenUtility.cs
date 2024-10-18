// Decompiled with JetBrains decompiler
// Type: ScreenUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public static class ScreenUtility
{
  private static int mDefaultScreenWidth = 1920;
  private static int mDefaultScreenHeight = 1080;
  public const int MIN_WINDOW_WIDTH = 480;
  public const int MIN_WINDOW_HEIGHT = 270;
  public const int DEFAULT_WINDOW_WIDTH = 1920;
  public const int DEFAULT_WINDOW_HEIGHT = 1080;
  public const float ASPECT_RATIO = 1.777778f;

  public static void SetResolution(int w, int h)
  {
    Screen.SetResolution(w, h, false);
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
