// Decompiled with JetBrains decompiler
// Type: SceneAssetBundleLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
