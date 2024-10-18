// Decompiled with JetBrains decompiler
// Type: UniWebViewHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class UniWebViewHelper
{
  public static int screenHeight
  {
    get
    {
      return Screen.height;
    }
  }

  public static int screenWidth
  {
    get
    {
      return Screen.width;
    }
  }

  public static int screenScale
  {
    get
    {
      return 1;
    }
  }

  public static string streamingAssetURLForPath(string path)
  {
    return "file:///android_asset/" + path;
  }
}
