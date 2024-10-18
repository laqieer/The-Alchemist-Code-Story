// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.IMediaControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public interface IMediaControl
  {
    bool OpenVideoFromFile(
      string path,
      long offset,
      string httpHeaderJson,
      uint sourceSamplerate = 0,
      uint sourceChannels = 0,
      int forceFileFormat = 0);

    bool OpenVideoFromBuffer(byte[] buffer);

    bool StartOpenVideoFromBuffer(ulong length);

    bool AddChunkToVideoBuffer(byte[] chunk, ulong offset, ulong length);

    bool EndOpenVideoFromBuffer();

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

    void SeekWithTolerance(float timeMs, float beforeMs, float afterMs);

    float GetCurrentTimeMs();

    double GetCurrentDateTimeSecondsSince1970();

    TimeRange[] GetSeekableTimeRanges();

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

    long GetLastExtendedErrorCode();

    void SetTextureProperties(FilterMode filterMode = 1, TextureWrapMode wrapMode = 1, int anisoLevel = 1);

    void GrabAudio(float[] buffer, int floatCount, int channelCount);

    int GetNumAudioChannels();

    void SetAudioHeadRotation(Quaternion q);

    void ResetAudioHeadRotation();

    void SetAudioChannelMode(Audio360ChannelMode channelMode);

    void SetAudioFocusEnabled(bool enabled);

    void SetAudioFocusProperties(float offFocusLevel, float widthDegrees);

    void SetAudioFocusRotation(Quaternion q);

    void ResetAudioFocus();

    bool WaitForNextFrame(Camera dummyCamera, int previousFrameCount);

    void SetPlayWithoutBuffering(bool playWithoutBuffering);

    void SetKeyServerURL(string url);

    void SetKeyServerAuthToken(string token);

    void SetDecryptionKeyBase64(string key);

    void SetDecryptionKey(byte[] key);
  }
}
