// Decompiled with JetBrains decompiler
// Type: GachaStone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class GachaStone : MonoBehaviour
{
  public Camera TargetCamera;

  public string DROP_ID { get; set; }

  private void Start()
  {
    if (!Object.op_Equality((Object) this.TargetCamera, (Object) null))
      return;
    this.TargetCamera = Camera.main;
  }

  private void Update()
  {
    ((Component) this).transform.LookAt(((Component) this.TargetCamera).transform);
  }
}
