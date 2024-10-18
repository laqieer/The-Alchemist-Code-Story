// Decompiled with JetBrains decompiler
// Type: CustomSound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
[AddComponentMenu("Audio/Custom Sound")]
public class CustomSound : MonoBehaviour
{
  public CustomSound.EType type;
  public string cueID;
  public bool PlayOnAwake;
  public bool LoopFlag;
  public float StopSec;
  private bool mPlayAutomatic;
  private MySound.PlayHandle mPlayHandle;

  private void Awake()
  {
  }

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
    if (this.type == CustomSound.EType.SE)
    {
      if (this.LoopFlag)
        this.mPlayHandle = MonoSingleton<MySound>.Instance.PlaySELoop(this.cueID);
      else
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID);
    }
    else
    {
      if (this.type != CustomSound.EType.JINGLE)
        return;
      MonoSingleton<MySound>.Instance.PlayJingle(this.cueID);
    }
  }

  public void Stop()
  {
    if (this.mPlayHandle == null)
      return;
    this.mPlayHandle.Stop(this.StopSec);
    this.mPlayHandle = (MySound.PlayHandle) null;
  }

  public void Play(float delaySec)
  {
    this.Stop();
    if (this.type == CustomSound.EType.SE)
    {
      if (this.LoopFlag)
        this.mPlayHandle = MonoSingleton<MySound>.Instance.PlaySELoop(this.cueID, delaySec);
      else
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, delaySec);
    }
    else
    {
      if (this.type != CustomSound.EType.JINGLE)
        return;
      MonoSingleton<MySound>.Instance.PlayJingle(this.cueID, delaySec);
    }
  }

  public enum EType
  {
    SE,
    JINGLE,
  }
}
