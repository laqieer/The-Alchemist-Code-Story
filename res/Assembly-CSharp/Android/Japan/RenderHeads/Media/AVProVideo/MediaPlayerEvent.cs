// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.MediaPlayerEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine.Events;

namespace RenderHeads.Media.AVProVideo
{
  [Serializable]
  public class MediaPlayerEvent : UnityEvent<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>
  {
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
    }
  }
}
