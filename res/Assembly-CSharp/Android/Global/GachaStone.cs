﻿// Decompiled with JetBrains decompiler
// Type: GachaStone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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