// Decompiled with JetBrains decompiler
// Type: SceneAssetBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
