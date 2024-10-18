// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.NullMediaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public sealed class NullMediaPlayer : BaseMediaPlayer
  {
    private bool _isPlaying;
    private bool _isPaused;
    private float _currentTime;
    private float _volume;
    private float _playbackRate = 1f;
    private bool _bLoop;
    private int _Width = 256;
    private int _height = 256;
    private Texture2D _texture;
    private Texture2D _texture_AVPro;
    private Texture2D _texture_AVPro1;
    private float _fakeFlipTime;
    private int _frameCount;
    private const float FrameRate = 10f;

    public override string GetVersion() => "0.0.0";

    public override bool OpenVideoFromFile(
      string path,
      long offset,
      string httpHeaderJson,
      uint sourceSamplerate = 0,
      uint sourceChannels = 0,
      int forceFileFormat = 0)
    {
      this._texture_AVPro = (Texture2D) Resources.Load("AVPro");
      this._texture_AVPro1 = (Texture2D) Resources.Load("AVPro1");
      if (Object.op_Implicit((Object) this._texture_AVPro))
      {
        this._Width = ((Texture) this._texture_AVPro).width;
        this._height = ((Texture) this._texture_AVPro).height;
      }
      this._texture = this._texture_AVPro;
      this._fakeFlipTime = 0.0f;
      this._frameCount = 0;
      return true;
    }

    public override void CloseVideo()
    {
      this._frameCount = 0;
      Resources.UnloadAsset((Object) this._texture_AVPro);
      Resources.UnloadAsset((Object) this._texture_AVPro1);
      base.CloseVideo();
    }

    public override void SetLooping(bool bLooping) => this._bLoop = bLooping;

    public override bool IsLooping() => this._bLoop;

    public override bool HasMetaData() => true;

    public override bool CanPlay() => true;

    public override bool HasAudio() => false;

    public override bool HasVideo() => false;

    public override void Play()
    {
      this._isPlaying = true;
      this._isPaused = false;
      this._fakeFlipTime = 0.0f;
    }

    public override void Pause()
    {
      this._isPlaying = false;
      this._isPaused = true;
    }

    public override void Stop()
    {
      this._isPlaying = false;
      this._isPaused = false;
    }

    public override bool IsSeeking() => false;

    public override bool IsPlaying() => this._isPlaying;

    public override bool IsPaused() => this._isPaused;

    public override bool IsFinished()
    {
      return this._isPlaying && (double) this._currentTime >= (double) this.GetDurationMs();
    }

    public override bool IsBuffering() => false;

    public override float GetDurationMs() => 10000f;

    public override int GetVideoWidth() => this._Width;

    public override int GetVideoHeight() => this._height;

    public override float GetVideoDisplayRate() => 10f;

    public override Texture GetTexture(int index) => (Texture) this._texture;

    public override int GetTextureFrameCount() => this._frameCount;

    public override bool RequiresVerticalFlip() => false;

    public override void Seek(float timeMs) => this._currentTime = timeMs;

    public override void SeekFast(float timeMs) => this._currentTime = timeMs;

    public override void SeekWithTolerance(float timeMs, float beforeMs, float afterMs)
    {
      this._currentTime = timeMs;
    }

    public override float GetCurrentTimeMs() => this._currentTime;

    public override void SetPlaybackRate(float rate) => this._playbackRate = rate;

    public override float GetPlaybackRate() => this._playbackRate;

    public override float GetBufferingProgress() => 0.0f;

    public override void MuteAudio(bool bMuted)
    {
    }

    public override bool IsMuted() => true;

    public override void SetVolume(float volume) => this._volume = volume;

    public override float GetVolume() => this._volume;

    public override int GetAudioTrackCount() => 0;

    public override int GetCurrentAudioTrack() => 0;

    public override void SetAudioTrack(int index)
    {
    }

    public override int GetVideoTrackCount() => 0;

    public override int GetCurrentVideoTrack() => 0;

    public override string GetCurrentAudioTrackId() => string.Empty;

    public override int GetCurrentAudioTrackBitrate() => 0;

    public override void SetVideoTrack(int index)
    {
    }

    public override string GetCurrentVideoTrackId() => string.Empty;

    public override int GetCurrentVideoTrackBitrate() => 0;

    public override float GetVideoFrameRate() => 0.0f;

    public override void Update()
    {
      this.UpdateSubtitles();
      if (!this._isPlaying)
        return;
      this._currentTime += Time.deltaTime * 1000f;
      if ((double) this._currentTime >= (double) this.GetDurationMs())
      {
        this._currentTime = this.GetDurationMs();
        if (this._bLoop)
          this.Rewind();
      }
      this._fakeFlipTime += Time.deltaTime;
      if ((double) this._fakeFlipTime < 0.1)
        return;
      this._fakeFlipTime = 0.0f;
      this._texture = !Object.op_Equality((Object) this._texture, (Object) this._texture_AVPro) ? this._texture_AVPro : this._texture_AVPro1;
      ++this._frameCount;
    }

    public override void Render()
    {
    }

    public override void Dispose()
    {
    }
  }
}
