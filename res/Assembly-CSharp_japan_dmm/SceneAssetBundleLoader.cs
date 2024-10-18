// Decompiled with JetBrains decompiler
// Type: SceneAssetBundleLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class SceneAssetBundleLoader : MonoBehaviour
{
  public static Object SceneBundle;

  private void Start()
  {
    if (Object.op_Inequality(SceneAssetBundleLoader.SceneBundle, (Object) null))
    {
      Object.Instantiate(SceneAssetBundleLoader.SceneBundle);
      SceneAssetBundleLoader.SceneBundle = (Object) null;
    }
    Object.Destroy((Object) ((Component) this).gameObject);
  }
}
