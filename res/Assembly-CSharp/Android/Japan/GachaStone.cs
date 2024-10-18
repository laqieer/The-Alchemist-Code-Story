// Decompiled with JetBrains decompiler
// Type: GachaStone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GachaStone : MonoBehaviour
{
  public Camera TargetCamera;

  public string DROP_ID { get; set; }

  private void Start()
  {
    if (!((Object) this.TargetCamera == (Object) null))
      return;
    this.TargetCamera = Camera.main;
  }

  private void Update()
  {
    this.transform.LookAt(this.TargetCamera.transform);
  }
}
