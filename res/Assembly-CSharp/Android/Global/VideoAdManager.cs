// Decompiled with JetBrains decompiler
// Type: VideoAdManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Advertisements;

public class VideoAdManager : MonoBehaviour
{
  public static void Init()
  {
    VideoAdManager.InitialiseAds();
  }

  private static void InitialiseAds()
  {
    string str = "1476714";
    bool flag = false;
    if (Advertisement.get_isInitialized())
      return;
    Advertisement.Initialize(str, flag);
  }
}
