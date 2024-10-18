// Decompiled with JetBrains decompiler
// Type: SceneAssetBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SceneAssetBundle : MonoBehaviour
{
  public int Hash;

  private void LateUpdate()
  {
    this.CheckChildren();
  }

  private void CheckChildren()
  {
    if (this.transform.childCount > 0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
