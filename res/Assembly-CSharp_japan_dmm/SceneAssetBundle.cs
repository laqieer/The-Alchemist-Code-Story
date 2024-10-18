// Decompiled with JetBrains decompiler
// Type: SceneAssetBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class SceneAssetBundle : MonoBehaviour
{
  public int Hash;

  private void LateUpdate() => this.CheckChildren();

  private void CheckChildren()
  {
    if (((Component) this).transform.childCount > 0)
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
  }
}
