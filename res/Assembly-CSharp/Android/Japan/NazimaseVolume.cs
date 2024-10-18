// Decompiled with JetBrains decompiler
// Type: NazimaseVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
