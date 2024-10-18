// Decompiled with JetBrains decompiler
// Type: NazimaseVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("SRPG/背景ツール/なじませボリューム")]
[ExecuteInEditMode]
public class NazimaseVolume : MonoBehaviour
{
  private void Awake()
  {
    this.tag = "EditorOnly";
  }

  public Bounds Bounds
  {
    get
    {
      return new Bounds(this.transform.position, this.transform.localScale);
    }
  }
}
