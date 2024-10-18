// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.IMediaControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  public interface IMediaControl
  {
    bool OpenVideoFromFile(string path, long offset, string httpHeaderJson);

    bool OpenVideoFromBuffer(byte[] buffer);

    void CloseVideo();

    void SetLooping(bool bLooping);

    bool IsLooping();

    bool HasMetaData();

    bool CanPlay();

    bool IsPlaying();

    bool IsSeeking();

    bool IsPaused();

    bool IsFinished();

    bool IsBuffering();

    void Play();

    void Pause();

    void Stop();

    void Rewind();

    void Seek(float timeMs);

    void SeekFast(float timeMs);

    float GetCurrentTimeMs();

    float GetPlaybackRate();

    void SetPlaybackRate(float rate);

    void MuteAudio(bool bMute);

    bool IsMuted();

    void SetVolume(float volume);

    void SetBalance(float balance);

    float GetVolume();

    float GetBalance();

    int GetCurrentAudioTrack();

    void SetAudioTrack(int index);

    int GetCurrentVideoTrack();

    void SetVideoTrack(int index);

    float GetBufferingProgress();

    int GetBufferedTimeRangeCount();

    bool GetBufferedTimeRange(int index, ref float startTimeMs, ref float endTimeMs);

    ErrorCode GetLastError();

    void SetTextureProperties(FilterMode filterMode = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp, int anisoLevel = 1);

    void GrabAudio(float[] buffer, int floatCount, int channelCount);

    int GetNumAudioChannels();
  }
}
