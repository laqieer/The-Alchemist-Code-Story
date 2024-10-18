// Decompiled with JetBrains decompiler
// Type: SupportLogger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SupportLogger : MonoBehaviour
{
  public bool LogTrafficStats = true;

  public void Start()
  {
    if (!((Object) GameObject.Find("PunSupportLogger") == (Object) null))
      return;
    GameObject gameObject = new GameObject("PunSupportLogger");
    Object.DontDestroyOnLoad((Object) gameObject);
    gameObject.AddComponent<SupportLogging>().LogTrafficStats = this.LogTrafficStats;
  }
}
