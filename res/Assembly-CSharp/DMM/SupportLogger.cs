// Decompiled with JetBrains decompiler
// Type: SupportLogger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class SupportLogger : MonoBehaviour
{
  public bool LogTrafficStats = true;

  public void Start()
  {
    if (!Object.op_Equality((Object) GameObject.Find("PunSupportLogger"), (Object) null))
      return;
    GameObject gameObject = new GameObject("PunSupportLogger");
    Object.DontDestroyOnLoad((Object) gameObject);
    gameObject.AddComponent<SupportLogging>().LogTrafficStats = this.LogTrafficStats;
  }
}
