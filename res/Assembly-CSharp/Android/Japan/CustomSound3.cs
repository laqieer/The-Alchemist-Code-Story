﻿// Decompiled with JetBrains decompiler
// Type: CustomSound3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Custom Sound3")]
public class CustomSound3 : MonoBehaviour
{
  public CustomSound3.EPlayFunction PlayFunction = CustomSound3.EPlayFunction.VoicePlay;
  public MySound.EType CueSheetHandlePlayCategory = MySound.EType.VOICE;
  public bool PlayOnEnable = true;
  private List<CustomSound3.PlayHandles> mHandles = new List<CustomSound3.PlayHandles>();
  public string sheetName;
  public string cueID;
  public MySound.CueSheetHandle.ELoopFlag CueSheetHandlePlayLoopType;
  private bool mPlayAutomatic;
  public bool StopOnPlay;
  public bool StopOnDisable;
  public float StopSec;
  public float DelayPlaySec;

  private void OnEnable()
  {
    this.mPlayAutomatic = this.PlayOnEnable;
  }

  private void OnDisable()
  {
    this.mPlayAutomatic = false;
    if (!this.StopOnDisable)
      return;
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
    if (this.StopOnPlay)
      this.Stop();
    if (this.mHandles == null)
      return;
    CustomSound3.PlayHandles playHandles = new CustomSound3.PlayHandles();
    if (playHandles == null)
      return;
    if (this.PlayFunction == CustomSound3.EPlayFunction.PlayBGM)
      MonoSingleton<MySound>.Instance.PlayBGM(this.cueID, this.sheetName, false);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.PlayBGMDelay)
      MonoSingleton<MySound>.Instance.PlayBGM(this.cueID, this.DelayPlaySec, this.sheetName);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.PlayJingle)
      MonoSingleton<MySound>.Instance.PlayJingle(this.cueID, this.DelayPlaySec, this.sheetName);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.VoicePlay)
    {
      playHandles.mVoice = new MySound.Voice(this.sheetName, (string) null, (string) null, false);
      if (playHandles.mVoice != null)
        playHandles.mVoice.Play(this.cueID, this.DelayPlaySec, false);
    }
    else if (this.PlayFunction == CustomSound3.EPlayFunction.PlaySEOneShot)
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, this.DelayPlaySec);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.PlaySELoop)
      playHandles.mPlayHandle = MonoSingleton<MySound>.Instance.PlaySELoop(this.cueID, this.DelayPlaySec);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.CueSheetHandlePlayOneShot)
      MonoSingleton<MySound>.Instance.PlayOneShot(this.sheetName, this.cueID, this.CueSheetHandlePlayCategory, this.DelayPlaySec);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.CueSheetHandlePlayLoop)
      playHandles.mPlayHandle = MonoSingleton<MySound>.Instance.PlayLoop(this.sheetName, this.cueID, this.CueSheetHandlePlayCategory, this.DelayPlaySec);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.CueSheetHandlePlay)
    {
      playHandles.mCueSheetHandle = MySound.CueSheetHandle.Create(this.sheetName, this.CueSheetHandlePlayCategory, true, true, false, false);
      if (playHandles.mCueSheetHandle != null)
        playHandles.mPlayHandle = playHandles.mCueSheetHandle.Play(this.cueID, this.CueSheetHandlePlayLoopType, true, this.DelayPlaySec);
    }
    if (!playHandles.IsValid)
      return;
    this.mHandles.Add(playHandles);
  }

  public void Stop()
  {
    if (this.PlayFunction == CustomSound3.EPlayFunction.PlayBGM || this.PlayFunction == CustomSound3.EPlayFunction.PlayBGMDelay)
      MonoSingleton<MySound>.Instance.StopBGM(this.StopSec);
    if (this.mHandles == null)
      return;
    foreach (CustomSound3.PlayHandles mHandle in this.mHandles)
    {
      if (mHandle != null)
      {
        if (mHandle.mPlayHandle != null)
        {
          mHandle.mPlayHandle.Stop(this.StopSec);
          mHandle.mPlayHandle = (MySound.PlayHandle) null;
        }
        if (mHandle.mCueSheetHandle != null)
        {
          mHandle.mCueSheetHandle.StopDefaultAll(this.StopSec);
          mHandle.mCueSheetHandle = (MySound.CueSheetHandle) null;
        }
        if (mHandle.mVoice != null)
        {
          mHandle.mVoice.StopAll(this.StopSec);
          mHandle.mVoice = (MySound.Voice) null;
        }
      }
    }
    this.mHandles.Clear();
  }

  public enum EPlayFunction
  {
    CueSheetHandlePlay,
    CueSheetHandlePlayOneShot,
    CueSheetHandlePlayLoop,
    VoicePlay,
    PlaySEOneShot,
    PlaySELoop,
    PlayJingle,
    PlayBGM,
    PlayBGMDelay,
  }

  private class PlayHandles
  {
    public MySound.PlayHandle mPlayHandle;
    public MySound.CueSheetHandle mCueSheetHandle;
    public MySound.Voice mVoice;

    public bool IsValid
    {
      get
      {
        if (this.mPlayHandle == null && this.mCueSheetHandle == null)
          return this.mVoice != null;
        return true;
      }
    }
  }
}
