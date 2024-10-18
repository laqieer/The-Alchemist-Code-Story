// Decompiled with JetBrains decompiler
// Type: CustomSound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

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

  private void OnEnable()
  {
    this.mPlayAutomatic = this.PlayOnAwake;
  }

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
        this.mPlayHandle = MonoSingleton<MySound>.Instance.PlaySELoop(this.cueID, 0.0f);
      else
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, 0.0f);
    }
    else
    {
      if (this.type != CustomSound.EType.JINGLE)
        return;
      MonoSingleton<MySound>.Instance.PlayJingle(this.cueID, 0.0f, (string) null);
    }
  }

  public void Stop()
  {
    if (this.mPlayHandle == null)
      return;
    this.mPlayHandle.Stop(this.StopSec);
    this.mPlayHandle = (MySound.PlayHandle) null;
  }

  public enum EType
  {
    SE,
    JINGLE,
  }
}
