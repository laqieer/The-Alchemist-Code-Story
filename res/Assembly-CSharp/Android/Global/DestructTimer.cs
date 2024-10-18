// Decompiled with JetBrains decompiler
// Type: DestructTimer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class DestructTimer : MonoBehaviour
{
  public float Timer = 1f;

  private void Update()
  {
    this.Timer -= Time.deltaTime;
    if ((double) this.Timer > 0.0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
