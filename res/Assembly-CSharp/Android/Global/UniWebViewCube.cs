// Decompiled with JetBrains decompiler
// Type: UniWebViewCube
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class UniWebViewCube : MonoBehaviour
{
  private bool firstHit = true;
  public UniWebDemo webViewDemo;
  private float startTime;

  private void Start()
  {
    this.startTime = Time.time;
  }

  private void OnCollisionEnter(Collision col)
  {
    if (!(col.gameObject.name == "Target"))
      return;
    this.webViewDemo.ShowAlertInWebview(Time.time - this.startTime, this.firstHit);
    this.firstHit = false;
  }
}
