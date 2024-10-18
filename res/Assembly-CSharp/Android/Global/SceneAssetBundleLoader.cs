// Decompiled with JetBrains decompiler
// Type: SceneAssetBundleLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SceneAssetBundleLoader : MonoBehaviour
{
  public static Object SceneBundle;

  private void Start()
  {
    if (SceneAssetBundleLoader.SceneBundle != (Object) null)
    {
      Object.Instantiate(SceneAssetBundleLoader.SceneBundle);
      SceneAssetBundleLoader.SceneBundle = (Object) null;
    }
    Object.Destroy((Object) this.gameObject);
  }
}
