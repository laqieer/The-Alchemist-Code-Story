// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.BaseMediaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public abstract class BaseMediaPlayer : 
    IMediaPlayer,
    IMediaControl,
    IMediaInfo,
    IMediaProducer,
    IMediaSubtitles,
    IDisposable
  {
    protected string _playerDescription = string.Empty;
    protected ErrorCode _lastError;
    protected FilterMode _defaultTextureFilterMode = (FilterMode) 1;
    protected TextureWrapMode _defaultTextureWrapMode = (TextureWrapMode) 1;
    protected int _defaultTextureAnisoLevel = 1;
    protected TimeRange[] _seekableTimeRanges = new TimeRange[0];
    private float _stallDetectionTimer;
    private int _stallDetectionFrame;
    protected List<Subtitle> _subtitles;
    protected Subtitle _currentSubtitle;

    public abstract string GetVersion();

    public abstract bool OpenVideoFromFile(
      string path,
      long offset,
      string httpHeaderJson,
      uint sourceSamplerate = 0,
      uint sourceChannels = 0,
      int forceFileFormat = 0);

    public virtual bool OpenVideoFromBuffer(byte[] buffer) => false;

    public virtual bool StartOpenVideoFromBuffer(ulong length) => false;

    public virtual bool AddChunkToVideoBuffer(byte[] chunk, ulong offset, ulong length) => false;

    public virtual bool EndOpenVideoFromBuffer() => false;

    public virtual void CloseVideo()
    {
      this._stallDetectionTimer = 0.0f;
      this._stallDetectionFrame = 0;
      this._lastError = ErrorCode.None;
    }

    public abstract void SetLooping(bool bLooping);

    public abstract bool IsLooping();

    public abstract bool HasMetaData();

    public abstract bool CanPlay();

    public abstract void Play();

    public abstract void Pause();

    public abstract void Stop();

    public virtual void Rewind() => this.SeekFast(0.0f);

    public abstract void Seek(float timeMs);

    public abstract void SeekFast(float timeMs);

    public virtual void SeekWithTolerance(float timeMs, float beforeMs, float afterMs)
    {
      this.Seek(timeMs);
    }

    public abstract float GetCurrentTimeMs();

    public virtual double GetCurrentDateTimeSecondsSince1970() => 0.0;

    public virtual TimeRange[] GetSeekableTimeRanges() => this._seekableTimeRanges;

    public abstract float GetPlaybackRate();

    public abstract void SetPlaybackRate(float rate);

    public abstract float GetDurationMs();

    public abstract int GetVideoWidth();

    public abstract int GetVideoHeight();

    public virtual Rect GetCropRect() => new Rect(0.0f, 0.0f, 0.0f, 0.0f);

    public abstract float GetVideoDisplayRate();

    public abstract bool HasAudio();

    public abstract bool HasVideo();

    public abstract bool IsSeeking();

    public abstract bool IsPlaying();

    public abstract bool IsPaused();

    public abstract bool IsFinished();

    public abstract bool IsBuffering();

    public virtual bool WaitForNextFrame(Camera dummyCamera, int previousFrameCount) => false;

    public virtual void SetPlayWithoutBuffering(bool playWithoutBuffering)
    {
    }

    public virtual void SetKeyServerURL(string url)
    {
    }

    public virtual void SetKeyServerAuthToken(string token)
    {
    }

    public virtual void SetDecryptionKeyBase64(string key)
    {
    }

    public virtual void SetDecryptionKey(byte[] key)
    {
    }

    public virtual int GetTextureCount() => 1;

    public abstract Texture GetTexture(int index = 0);

    public abstract int GetTextureFrameCount();

    public virtual bool SupportsTextureFrameCount() => true;

    public virtual long GetTextureTimeStamp() => long.MinValue;

    public abstract bool RequiresVerticalFlip();

    public virtual float[] GetTextureTransform()
    {
      return new float[6]{ 1f, 0.0f, 0.0f, 1f, 0.0f, 0.0f };
    }

    public virtual Matrix4x4 GetYpCbCrTransform() => Matrix4x4.identity;

    public abstract void MuteAudio(bool bMuted);

    public abstract bool IsMuted();

    public abstract void SetVolume(float volume);

    public virtual void SetBalance(float balance)
    {
    }

    public abstract float GetVolume();

    public virtual float GetBalance() => 0.0f;

    public abstract int GetAudioTrackCount();

    public virtual string GetAudioTrackId(int index) => index.ToString();

    public abstract int GetCurrentAudioTrack();

    public abstract void SetAudioTrack(int index);

    public abstract string GetCurrentAudioTrackId();

    public abstract int GetCurrentAudioTrackBitrate();

    public virtual int GetNumAudioChannels() => -1;

    public virtual void SetAudioHeadRotation(Quaternion q)
    {
    }

    public virtual void ResetAudioHeadRotation()
    {
    }

    public virtual void SetAudioChannelMode(Audio360ChannelMode channelMode)
    {
    }

    public virtual void SetAudioFocusEnabled(bool enabled)
    {
    }

    public virtual void SetAudioFocusProperties(float offFocusLevel, float widthDegrees)
    {
    }

    public virtual void SetAudioFocusRotation(Quaternion q)
    {
    }

    public virtual void ResetAudioFocus()
    {
    }

    public abstract int GetVideoTrackCount();

    public virtual string GetVideoTrackId(int index) => index.ToString();

    public abstract int GetCurrentVideoTrack();

    public abstract void SetVideoTrack(int index);

    public abstract string GetCurrentVideoTrackId();

    public abstract int GetCurrentVideoTrackBitrate();

    public abstract float GetVideoFrameRate();

    public virtual long GetEstimatedTotalBandwidthUsed() => -1;

    public abstract float GetBufferingProgress();

    public abstract void Update();

    public abstract void Render();

    public abstract void Dispose();

    public ErrorCode GetLastError() => this._lastError;

    public virtual long GetLastExtendedErrorCode() => 0;

    public string GetPlayerDescription() => this._playerDescription;

    public virtual bool PlayerSupportsLinearColorSpace() => true;

    public virtual int GetBufferedTimeRangeCount() => 0;

    public virtual bool GetBufferedTimeRange(int index, ref float startTimeMs, ref float endTimeMs)
    {
      return false;
    }

    public void SetTextureProperties(
      FilterMode filterMode = 1,
      TextureWrapMode wrapMode = 1,
      int anisoLevel = 0)
    {
      this._defaultTextureFilterMode = filterMode;
      this._defaultTextureWrapMode = wrapMode;
      this._defaultTextureAnisoLevel = anisoLevel;
      for (int index = 0; index < this.GetTextureCount(); ++index)
        this.ApplyTextureProperties(this.GetTexture(index));
    }

    protected virtual void ApplyTextureProperties(Texture texture)
    {
      if (!Object.op_Inequality((Object) texture, (Object) null))
        return;
      texture.filterMode = this._defaultTextureFilterMode;
      texture.wrapMode = this._defaultTextureWrapMode;
      texture.anisoLevel = this._defaultTextureAnisoLevel;
    }

    public virtual void GrabAudio(float[] buffer, int floatCount, int channelCount)
    {
    }

    protected bool IsExpectingNewVideoFrame()
    {
      return this.HasVideo() && !this.IsFinished() && (!this.IsPaused() || this.IsPlaying());
    }

    public virtual bool IsPlaybackStalled()
    {
      if (this.SupportsTextureFrameCount() && this.IsExpectingNewVideoFrame())
      {
        int textureFrameCount = this.GetTextureFrameCount();
        if (textureFrameCount != this._stallDetectionFrame)
        {
          this._stallDetectionTimer = 0.0f;
          this._stallDetectionFrame = textureFrameCount;
        }
        else
          this._stallDetectionTimer += Time.deltaTime;
        return (double) this._stallDetectionTimer > 0.75;
      }
      this._stallDetectionTimer = 0.0f;
      return false;
    }

    public bool LoadSubtitlesSRT(string data)
    {
      if (string.IsNullOrEmpty(data))
      {
        this._subtitles = (List<Subtitle>) null;
        this._currentSubtitle = (Subtitle) null;
      }
      else
      {
        this._subtitles = Helper.LoadSubtitlesSRT(data);
        this._currentSubtitle = (Subtitle) null;
      }
      return this._subtitles != null;
    }

    public virtual void UpdateSubtitles()
    {
      if (this._subtitles == null)
        return;
      float currentTimeMs = this.GetCurrentTimeMs();
      int num = 0;
      if (this._currentSubtitle != null && !this._currentSubtitle.IsTime(currentTimeMs))
      {
        if ((double) currentTimeMs > (double) this._currentSubtitle.timeEndMs)
          num = this._currentSubtitle.index + 1;
        this._currentSubtitle = (Subtitle) null;
      }
      if (this._currentSubtitle != null)
        return;
      for (int index = num; index < this._subtitles.Count; ++index)
      {
        if (this._subtitles[index].IsTime(currentTimeMs))
        {
          this._currentSubtitle = this._subtitles[index];
          break;
        }
      }
    }

    public virtual int GetSubtitleIndex()
    {
      int subtitleIndex = -1;
      if (this._currentSubtitle != null)
        subtitleIndex = this._currentSubtitle.index;
      return subtitleIndex;
    }

    public virtual string GetSubtitleText()
    {
      string subtitleText = string.Empty;
      if (this._currentSubtitle != null)
        subtitleText = this._currentSubtitle.text;
      return subtitleText;
    }

    public virtual void OnEnable()
    {
    }
  }
}
