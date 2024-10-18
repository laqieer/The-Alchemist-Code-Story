﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SoundTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [DisallowMultipleComponent]
  public class SoundTrigger : MonoBehaviour
  {
    [FlexibleArray]
    public string[] VoiceNames = new string[0];
    private MySound.Voice[] mVoices;

    private void Start()
    {
      this.mVoices = new MySound.Voice[this.VoiceNames.Length];
      for (int index = 0; index < this.VoiceNames.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.VoiceNames[index]))
          this.mVoices[index] = new MySound.Voice(this.VoiceNames[index], (string) null, (string) null);
      }
    }

    private void OnDestroy()
    {
      for (int index = 0; index < this.mVoices.Length; ++index)
      {
        if (this.mVoices[index] != null)
        {
          this.mVoices[index].StopAll();
          this.mVoices[index].Cleanup();
          this.mVoices[index] = (MySound.Voice) null;
        }
      }
    }

    public void PlayVoice(string cueID)
    {
      int length = cueID.IndexOf('.');
      if (length <= 0)
        return;
      string str = cueID.Substring(0, length);
      string cueID1 = cueID.Substring(length + 1);
      if (string.IsNullOrEmpty(cueID1))
        return;
      for (int index = 0; index < this.VoiceNames.Length; ++index)
      {
        if (this.VoiceNames[index] == str && this.mVoices[index] != null)
        {
          this.mVoices[index].Play(cueID1);
          break;
        }
      }
    }

    public void PlaySE(string cueID) => MonoSingleton<MySound>.Instance.PlaySEOneShot(cueID);

    public void PlayJingle(string cueID) => MonoSingleton<MySound>.Instance.PlayJingle(cueID);
  }
}
