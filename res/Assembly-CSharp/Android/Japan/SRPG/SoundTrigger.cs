// Decompiled with JetBrains decompiler
// Type: SRPG.SoundTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
          this.mVoices[index] = new MySound.Voice(this.VoiceNames[index], (string) null, (string) null, false);
      }
    }

    private void OnDestroy()
    {
      for (int index = 0; index < this.mVoices.Length; ++index)
      {
        if (this.mVoices[index] != null)
        {
          this.mVoices[index].StopAll(1f);
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
          this.mVoices[index].Play(cueID1, 0.0f, false);
          break;
        }
      }
    }

    public void PlaySE(string cueID)
    {
      MonoSingleton<MySound>.Instance.PlaySEOneShot(cueID, 0.0f);
    }

    public void PlayJingle(string cueID)
    {
      MonoSingleton<MySound>.Instance.PlayJingle(cueID, 0.0f, (string) null);
    }
  }
}
