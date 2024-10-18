// Decompiled with JetBrains decompiler
// Type: CustomSound2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
[AddComponentMenu("Audio/Custom Sound2")]
public class CustomSound2 : MonoBehaviour
{
  public string CueSheet = "SE";
  public string CueName;
  public bool PlayOnAwake;
  public bool LoopFlag;
  public float StopSec;
  private bool mPlayAutomatic;
  private MySound.PlayHandle mPlayHandle;

  private void OnEnable() => this.mPlayAutomatic = this.PlayOnAwake;

  private void OnDisable()
  {
    this.mPlayAutomatic = false;
    this.Stop();
  }

  private void Update()
  {
    if (!this.mPlayAutomatic)
      return;
    this.Play();
    this.mPlayAutomatic = false;
  }

  public void Play()
  {
    this.Stop();
    if (this.LoopFlag)
    {
      MySound.EType type = MySound.EType.SE;
      if (!string.IsNullOrEmpty(this.CueName))
      {
        if (this.CueName.StartsWith("BGM_"))
          type = MySound.EType.BGM;
        else if (this.CueName.StartsWith("JIN_"))
          type = MySound.EType.JINGLE;
        else if (this.CueName.StartsWith("VO_"))
          type = MySound.EType.VOICE;
      }
      this.mPlayHandle = MonoSingleton<MySound>.Instance.PlayLoop(this.CueSheet, this.CueName, type);
    }
    else
      MonoSingleton<MySound>.Instance.PlayOneShot(this.CueSheet, this.CueName);
  }

  public void Stop()
  {
    if (this.mPlayHandle == null)
      return;
    this.mPlayHandle.Stop(this.StopSec);
    this.mPlayHandle = (MySound.PlayHandle) null;
  }
}
