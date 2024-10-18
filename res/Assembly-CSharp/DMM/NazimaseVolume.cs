// Decompiled with JetBrains decompiler
// Type: NazimaseVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("SRPG/背景ツール/なじませボリューム")]
[ExecuteInEditMode]
public class NazimaseVolume : MonoBehaviour
{
  private void Awake() => ((Component) this).tag = "EditorOnly";

  public Bounds Bounds
  {
    get
    {
      return new Bounds(((Component) this).transform.position, ((Component) this).transform.localScale);
    }
  }
}
