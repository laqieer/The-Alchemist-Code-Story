// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.MediaPlayerEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine.Events;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [Serializable]
  public class MediaPlayerEvent : UnityEvent<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>
  {
    private List<UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>> _listeners = new List<UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>>(4);

    public bool HasListeners()
    {
      return this._listeners.Count > 0 || ((UnityEventBase) this).GetPersistentEventCount() > 0;
    }

    public void AddListener(
      UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode> call)
    {
      if (this._listeners.Contains(call))
        return;
      this._listeners.Add(call);
      base.AddListener(call);
    }

    public void RemoveListener(
      UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode> call)
    {
      int index = this._listeners.IndexOf(call);
      if (index < 0)
        return;
      this._listeners.RemoveAt(index);
      base.RemoveListener(call);
    }

    public enum EventType
    {
      MetaDataReady,
      ReadyToPlay,
      Started,
      FirstFrameReady,
      FinishedPlaying,
      Closing,
      Error,
      SubtitleChange,
      Stalled,
      Unstalled,
      ResolutionChanged,
      StartedSeeking,
      FinishedSeeking,
      StartedBuffering,
      FinishedBuffering,
      PropertiesChanged,
      PlaylistItemChanged,
      PlaylistFinished,
    }
  }
}
