// Decompiled with JetBrains decompiler
// Type: FPSDisplay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class FPSDisplay : MonoBehaviour
{
  private float mDeltaTime;
  public Text FPS;

  private void Start() => Object.Destroy((Object) ((Component) this).gameObject);

  private void Update()
  {
  }
}
