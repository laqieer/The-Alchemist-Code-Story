// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.MediaPlayerEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
