// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.WindowsMediaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  public sealed class WindowsMediaPlayer : BaseMediaPlayer
  {
    private static string _version = "Plug-in not yet initialised";
    private bool _forceAudioResample = true;
    private int _desiredAudioChannels = 2;
    private string _audioDeviceOutputName = string.Empty;
    private float _volume = 1f;
    private bool _isTextureTopDown = true;
    private IntPtr _nativeTexture = IntPtr.Zero;
    private IntPtr _instance = IntPtr.Zero;
    private float _displayRate = 1f;
    private bool _useHardwareDecoding = true;
    private int _queueSetAudioTrackIndex = -1;
    private bool _supportsLinearColorSpace = true;
    private float[] _bufferedTimeRanges = new float[0];
    private int _textureQuality = QualitySettings.masterTextureLimit;
    private bool _useUnityAudio;
    private bool _isPlaying;
    private bool _isPaused;
    private bool _audioMuted;
    private float _balance;
    private bool _bLoop;
    private bool _canPlay;
    private bool _hasMetaData;
    private int _width;
    private int _height;
    private float _frameRate;
    private bool _hasAudio;
    private bool _hasVideo;
    private Texture2D _texture;
    private float _displayRateTimer;
    private int _lastFrameCount;
    private Windows.VideoApi _videoApi;
    private bool _useTextureMips;
    private int _bufferedTimeRangeCount;
    private static bool _isInitialised;
    private static IntPtr _nativeFunction_UpdateAllTextures;
    private static IntPtr _nativeFunction_FreeTextures;

    public WindowsMediaPlayer(Windows.VideoApi videoApi, bool useHardwareDecoding, bool useTextureMips, string audioDeviceOutputName, bool useUnityAudio, bool forceResample, int channels)
    {
      this.SetOptions(videoApi, useHardwareDecoding, useTextureMips, audioDeviceOutputName, useUnityAudio, forceResample, channels);
    }

    public static void InitialisePlatform()
    {
      if (WindowsMediaPlayer._isInitialised)
        return;
      if (!WindowsMediaPlayer.Native.Init(QualitySettings.activeColorSpace == ColorSpace.Linear, true))
      {
        Debug.LogError((object) "[AVProVideo] Failing to initialise platform");
      }
      else
      {
        WindowsMediaPlayer._version = WindowsMediaPlayer.GetPluginVersion();
        WindowsMediaPlayer._nativeFunction_UpdateAllTextures = WindowsMediaPlayer.Native.GetRenderEventFunc_UpdateAllTextures();
        WindowsMediaPlayer._nativeFunction_FreeTextures = WindowsMediaPlayer.Native.GetRenderEventFunc_FreeTextures();
      }
    }

    public static void DeinitPlatform()
    {
      WindowsMediaPlayer.Native.Deinit();
    }

    public override int GetNumAudioChannels()
    {
      return WindowsMediaPlayer.Native.GetAudioChannelCount(this._instance);
    }

    private static int CalculateClosestViableChannelNumber(int channels)
    {
      int max;
      switch (AudioSettings.speakerMode)
      {
        case AudioSpeakerMode.Mono:
          max = 1;
          break;
        case AudioSpeakerMode.Stereo:
          max = 2;
          break;
        case AudioSpeakerMode.Quad:
          max = 4;
          break;
        case AudioSpeakerMode.Surround:
          max = 5;
          break;
        case AudioSpeakerMode.Mode5point1:
          max = 6;
          break;
        case AudioSpeakerMode.Mode7point1:
          max = 8;
          break;
        default:
          max = 2;
          break;
      }
      channels = Mathf.Clamp(channels, 1, max);
      int[] numArray = new int[6]{ 8, 6, 5, 4, 2, 1 };
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (channels >= numArray[index])
          return numArray[index];
      }
      return 2;
    }

    public void SetOptions(Windows.VideoApi videoApi, bool useHardwareDecoding, bool useTextureMips, string audioDeviceOutputName, bool useUnityAudio, bool forceResample, int channels)
    {
      this._videoApi = videoApi;
      this._useHardwareDecoding = useHardwareDecoding;
      this._useTextureMips = useTextureMips;
      this._audioDeviceOutputName = audioDeviceOutputName;
      this._useUnityAudio = useUnityAudio;
      this._forceAudioResample = forceResample;
      this._desiredAudioChannels = channels;
    }

    public override string GetVersion()
    {
      return WindowsMediaPlayer._version;
    }

    public override bool OpenVideoFromFile(string path, long offset, string httpHeaderJson)
    {
      this.CloseVideo();
      int viableChannelNumber = WindowsMediaPlayer.CalculateClosestViableChannelNumber(this._desiredAudioChannels);
      this._instance = WindowsMediaPlayer.Native.OpenSource(this._instance, path, (int) this._videoApi, this._useHardwareDecoding, this._useTextureMips, this._audioDeviceOutputName, this._useUnityAudio, this._forceAudioResample, viableChannelNumber);
      if (this._instance == IntPtr.Zero)
      {
        this.DisplayLoadFailureSuggestion(path);
        return false;
      }
      WindowsMediaPlayer.Native.SetUnityAudioEnabled(this._instance, this._useUnityAudio);
      return true;
    }

    public override bool OpenVideoFromBuffer(byte[] buffer)
    {
      this.CloseVideo();
      this._instance = WindowsMediaPlayer.Native.OpenSourceFromBuffer(this._instance, buffer, (ulong) buffer.Length, (int) this._videoApi, this._useHardwareDecoding, this._useTextureMips, this._audioDeviceOutputName, this._useUnityAudio);
      if (this._instance == IntPtr.Zero)
        return false;
      WindowsMediaPlayer.Native.SetUnityAudioEnabled(this._instance, this._useUnityAudio);
      return true;
    }

    private void DisplayLoadFailureSuggestion(string path)
    {
      if (this._videoApi != Windows.VideoApi.DirectShow && !SystemInfo.operatingSystem.Contains("Windows 7") && !SystemInfo.operatingSystem.Contains("Windows Vista") && !SystemInfo.operatingSystem.Contains("Windows XP") || !path.Contains(".mp4"))
        return;
      Debug.LogWarning((object) "[AVProVideo] The native Windows DirectShow H.264 decoder doesn't support videos with resolution above 1920x1080. You may need to reduce your video resolution, switch to another codec (such as DivX or Hap), or install 3rd party DirectShow codec (eg LAV Filters).  This shouldn't be a problem for Windows 8 and above as it has a native limitation of 3840x2160.");
    }

    public override void CloseVideo()
    {
      this._width = 0;
      this._height = 0;
      this._frameRate = 0.0f;
      this._hasAudio = this._hasVideo = false;
      this._hasMetaData = false;
      this._canPlay = false;
      this._isPaused = false;
      this._isPlaying = false;
      this._bLoop = false;
      this._audioMuted = false;
      this._volume = 1f;
      this._balance = 0.0f;
      this._lastFrameCount = 0;
      this._displayRate = 0.0f;
      this._displayRateTimer = 0.0f;
      this._queueSetAudioTrackIndex = -1;
      this._supportsLinearColorSpace = true;
      this._lastError = ErrorCode.None;
      this._nativeTexture = IntPtr.Zero;
      if ((Object) this._texture != (Object) null)
      {
        Object.Destroy((Object) this._texture);
        this._texture = (Texture2D) null;
      }
      if (this._instance != IntPtr.Zero)
      {
        WindowsMediaPlayer.Native.CloseSource(this._instance);
        this._instance = IntPtr.Zero;
      }
      WindowsMediaPlayer.IssueRenderThreadEvent(WindowsMediaPlayer.Native.RenderThreadEvent.FreeTextures);
    }

    public override void SetLooping(bool looping)
    {
      this._bLoop = looping;
      WindowsMediaPlayer.Native.SetLooping(this._instance, looping);
    }

    public override bool IsLooping()
    {
      return this._bLoop;
    }

    public override bool HasMetaData()
    {
      return this._hasMetaData;
    }

    public override bool HasAudio()
    {
      return this._hasAudio;
    }

    public override bool HasVideo()
    {
      return this._hasVideo;
    }

    public override bool CanPlay()
    {
      return this._canPlay;
    }

    public override void Play()
    {
      this._isPlaying = true;
      this._isPaused = false;
      WindowsMediaPlayer.Native.Play(this._instance);
    }

    public override void Pause()
    {
      this._isPlaying = false;
      this._isPaused = true;
      WindowsMediaPlayer.Native.Pause(this._instance);
    }

    public override void Stop()
    {
      this._isPlaying = false;
      this._isPaused = false;
      WindowsMediaPlayer.Native.Pause(this._instance);
    }

    public override bool IsSeeking()
    {
      return WindowsMediaPlayer.Native.IsSeeking(this._instance);
    }

    public override bool IsPlaying()
    {
      return this._isPlaying;
    }

    public override bool IsPaused()
    {
      return this._isPaused;
    }

    public override bool IsFinished()
    {
      return WindowsMediaPlayer.Native.IsFinished(this._instance);
    }

    public override bool IsBuffering()
    {
      return WindowsMediaPlayer.Native.IsBuffering(this._instance);
    }

    public override float GetDurationMs()
    {
      return WindowsMediaPlayer.Native.GetDuration(this._instance) * 1000f;
    }

    public override int GetVideoWidth()
    {
      return this._width;
    }

    public override int GetVideoHeight()
    {
      return this._height;
    }

    public override float GetVideoFrameRate()
    {
      return this._frameRate;
    }

    public override float GetVideoDisplayRate()
    {
      return this._displayRate;
    }

    public override Texture GetTexture(int index)
    {
      Texture texture = (Texture) null;
      if (WindowsMediaPlayer.Native.GetTextureFrameCount(this._instance) > 0)
        texture = (Texture) this._texture;
      return texture;
    }

    public override int GetTextureFrameCount()
    {
      return WindowsMediaPlayer.Native.GetTextureFrameCount(this._instance);
    }

    public override long GetTextureTimeStamp()
    {
      return WindowsMediaPlayer.Native.GetTextureTimeStamp(this._instance);
    }

    public override bool RequiresVerticalFlip()
    {
      return this._isTextureTopDown;
    }

    public override void Rewind()
    {
      this.Seek(0.0f);
    }

    public override void Seek(float timeMs)
    {
      WindowsMediaPlayer.Native.SetCurrentTime(this._instance, timeMs / 1000f, false);
    }

    public override void SeekFast(float timeMs)
    {
      WindowsMediaPlayer.Native.SetCurrentTime(this._instance, timeMs / 1000f, true);
    }

    public override float GetCurrentTimeMs()
    {
      return WindowsMediaPlayer.Native.GetCurrentTime(this._instance) * 1000f;
    }

    public override void SetPlaybackRate(float rate)
    {
      WindowsMediaPlayer.Native.SetPlaybackRate(this._instance, rate);
    }

    public override float GetPlaybackRate()
    {
      return WindowsMediaPlayer.Native.GetPlaybackRate(this._instance);
    }

    public override float GetBufferingProgress()
    {
      return WindowsMediaPlayer.Native.GetBufferingProgress(this._instance);
    }

    public override int GetBufferedTimeRangeCount()
    {
      return this._bufferedTimeRangeCount;
    }

    public override bool GetBufferedTimeRange(int index, ref float startTimeMs, ref float endTimeMs)
    {
      bool flag = false;
      if (index >= 0 && index < this._bufferedTimeRangeCount)
      {
        flag = true;
        startTimeMs = 1000f * this._bufferedTimeRanges[index * 2];
        endTimeMs = 1000f * this._bufferedTimeRanges[index * 2 + 1];
      }
      return flag;
    }

    public override void MuteAudio(bool bMuted)
    {
      this._audioMuted = bMuted;
      WindowsMediaPlayer.Native.SetMuted(this._instance, this._audioMuted);
    }

    public override bool IsMuted()
    {
      return this._audioMuted;
    }

    public override void SetVolume(float volume)
    {
      this._volume = volume;
      WindowsMediaPlayer.Native.SetVolume(this._instance, volume);
    }

    public override float GetVolume()
    {
      return this._volume;
    }

    public override void SetBalance(float balance)
    {
      this._balance = balance;
      WindowsMediaPlayer.Native.SetBalance(this._instance, balance);
    }

    public override float GetBalance()
    {
      return this._balance;
    }

    public override int GetAudioTrackCount()
    {
      return WindowsMediaPlayer.Native.GetAudioTrackCount(this._instance);
    }

    public override int GetCurrentAudioTrack()
    {
      return WindowsMediaPlayer.Native.GetAudioTrack(this._instance);
    }

    public override void SetAudioTrack(int index)
    {
      this._queueSetAudioTrackIndex = index;
    }

    public override int GetVideoTrackCount()
    {
      int num = 0;
      if (this.HasVideo())
        num = 1;
      return num;
    }

    public override bool IsPlaybackStalled()
    {
      return WindowsMediaPlayer.Native.IsPlaybackStalled(this._instance);
    }

    public override string GetCurrentAudioTrackId()
    {
      return string.Empty;
    }

    public override int GetCurrentAudioTrackBitrate()
    {
      return 0;
    }

    public override int GetCurrentVideoTrack()
    {
      return 0;
    }

    public override void SetVideoTrack(int index)
    {
    }

    public override string GetCurrentVideoTrackId()
    {
      return string.Empty;
    }

    public override int GetCurrentVideoTrackBitrate()
    {
      return 0;
    }

    public override void Update()
    {
      WindowsMediaPlayer.Native.Update(this._instance);
      this._lastError = (ErrorCode) WindowsMediaPlayer.Native.GetLastErrorCode(this._instance);
      if (this._queueSetAudioTrackIndex >= 0 && this._hasAudio)
      {
        WindowsMediaPlayer.Native.SetAudioTrack(this._instance, this._queueSetAudioTrackIndex);
        this._queueSetAudioTrackIndex = -1;
      }
      this._bufferedTimeRangeCount = WindowsMediaPlayer.Native.GetBufferedRanges(this._instance, this._bufferedTimeRanges, this._bufferedTimeRanges.Length / 2);
      if (this._bufferedTimeRangeCount > this._bufferedTimeRanges.Length / 2)
      {
        this._bufferedTimeRanges = new float[this._bufferedTimeRangeCount * 2];
        this._bufferedTimeRangeCount = WindowsMediaPlayer.Native.GetBufferedRanges(this._instance, this._bufferedTimeRanges, this._bufferedTimeRanges.Length / 2);
      }
      this.UpdateSubtitles();
      if (!this._canPlay)
      {
        if (!this._hasMetaData && WindowsMediaPlayer.Native.HasMetaData(this._instance))
        {
          if (WindowsMediaPlayer.Native.HasVideo(this._instance))
          {
            this._width = WindowsMediaPlayer.Native.GetWidth(this._instance);
            this._height = WindowsMediaPlayer.Native.GetHeight(this._instance);
            this._frameRate = WindowsMediaPlayer.Native.GetFrameRate(this._instance);
            if (this._width > 0 && this._height > 0)
            {
              this._hasVideo = true;
              if (Mathf.Max(this._width, this._height) > SystemInfo.maxTextureSize)
              {
                Debug.LogError((object) string.Format("[AVProVideo] Video dimensions ({0}x{1}) larger than maxTextureSize ({2} for current build target)", (object) this._width, (object) this._height, (object) SystemInfo.maxTextureSize));
                this._width = this._height = 0;
                this._hasVideo = false;
              }
            }
            if (this._hasVideo && WindowsMediaPlayer.Native.HasAudio(this._instance))
              this._hasAudio = true;
          }
          else if (WindowsMediaPlayer.Native.HasAudio(this._instance))
            this._hasAudio = true;
          if (this._hasVideo || this._hasAudio)
            this._hasMetaData = true;
          this._playerDescription = Marshal.PtrToStringAnsi(WindowsMediaPlayer.Native.GetPlayerDescription(this._instance));
          this._supportsLinearColorSpace = !this._playerDescription.Contains("MF-MediaEngine-Hardware");
          if (this._hasVideo)
            this.OnTextureSizeChanged();
        }
        if (this._hasMetaData)
          this._canPlay = WindowsMediaPlayer.Native.CanPlay(this._instance);
      }
      if (!this._hasVideo)
        return;
      IntPtr texturePointer = WindowsMediaPlayer.Native.GetTexturePointer(this._instance);
      if ((Object) this._texture != (Object) null && this._nativeTexture != IntPtr.Zero && this._nativeTexture != texturePointer)
      {
        this._width = WindowsMediaPlayer.Native.GetWidth(this._instance);
        this._height = WindowsMediaPlayer.Native.GetHeight(this._instance);
        if (texturePointer == IntPtr.Zero || this._width != this._texture.width || this._height != this._texture.height)
        {
          if (this._width != this._texture.width || this._height != this._texture.height)
            this.OnTextureSizeChanged();
          this._nativeTexture = IntPtr.Zero;
          Object.Destroy((Object) this._texture);
          this._texture = (Texture2D) null;
        }
        else if (this._nativeTexture != texturePointer)
        {
          this._texture.UpdateExternalTexture(texturePointer);
          this._nativeTexture = texturePointer;
        }
      }
      if (this._textureQuality != QualitySettings.masterTextureLimit)
      {
        if ((Object) this._texture != (Object) null && this._nativeTexture != IntPtr.Zero && this._texture.GetNativeTexturePtr() == IntPtr.Zero)
          this._texture.UpdateExternalTexture(this._nativeTexture);
        this._textureQuality = QualitySettings.masterTextureLimit;
      }
      if (!((Object) this._texture == (Object) null) || this._width <= 0 || (this._height <= 0 || !(texturePointer != IntPtr.Zero)))
        return;
      this._isTextureTopDown = WindowsMediaPlayer.Native.IsTextureTopDown(this._instance);
      this._texture = Texture2D.CreateExternalTexture(this._width, this._height, TextureFormat.RGBA32, this._useTextureMips, false, texturePointer);
      if ((Object) this._texture != (Object) null)
      {
        this._nativeTexture = texturePointer;
        this.ApplyTextureProperties((Texture) this._texture);
      }
      else
        Debug.LogError((object) "[AVProVideo] Failed to create texture");
    }

    private void OnTextureSizeChanged()
    {
      if ((this._width == 720 || this._height == 480) && this._playerDescription.Contains("DirectShow"))
        Debug.LogWarning((object) "[AVProVideo] If video fails to play then it may be due to the resolution being higher than 1920x1080 which is the limitation of the Microsoft DirectShow H.264 decoder.\nTo resolve this you can either use Windows 8 or above (and disable 'Force DirectShow' option), resize your video, use a different codec (such as Hap or DivX), or install a 3rd party H.264 decoder such as LAV Filters.");
      else if (this._width <= 1920 && this._height <= 1080 || !this._playerDescription.Contains("MF-MediaEngine-Software"))
        ;
    }

    private void UpdateDisplayFrameRate()
    {
      this._displayRateTimer += Time.deltaTime;
      if ((double) this._displayRateTimer < 0.5)
        return;
      int textureFrameCount = WindowsMediaPlayer.Native.GetTextureFrameCount(this._instance);
      this._displayRate = (float) (textureFrameCount - this._lastFrameCount) / this._displayRateTimer;
      this._displayRateTimer -= 0.5f;
      if ((double) this._displayRateTimer >= 0.5)
        this._displayRateTimer = 0.0f;
      this._lastFrameCount = textureFrameCount;
    }

    public override void Render()
    {
      this.UpdateDisplayFrameRate();
      WindowsMediaPlayer.IssueRenderThreadEvent(WindowsMediaPlayer.Native.RenderThreadEvent.UpdateAllTextures);
    }

    public override void Dispose()
    {
      this.CloseVideo();
    }

    public override void GrabAudio(float[] buffer, int floatCount, int channelCount)
    {
      WindowsMediaPlayer.Native.GrabAudio(this._instance, buffer, floatCount, channelCount);
    }

    public override bool PlayerSupportsLinearColorSpace()
    {
      return this._supportsLinearColorSpace;
    }

    private static void IssueRenderThreadEvent(WindowsMediaPlayer.Native.RenderThreadEvent renderEvent)
    {
      if (renderEvent == WindowsMediaPlayer.Native.RenderThreadEvent.UpdateAllTextures)
      {
        GL.IssuePluginEvent(WindowsMediaPlayer._nativeFunction_UpdateAllTextures, 0);
      }
      else
      {
        if (renderEvent != WindowsMediaPlayer.Native.RenderThreadEvent.FreeTextures)
          return;
        GL.IssuePluginEvent(WindowsMediaPlayer._nativeFunction_FreeTextures, 0);
      }
    }

    private static string GetPluginVersion()
    {
      return Marshal.PtrToStringAnsi(WindowsMediaPlayer.Native.GetPluginVersion());
    }

    public override void OnEnable()
    {
      base.OnEnable();
      if ((Object) this._texture != (Object) null && this._nativeTexture != IntPtr.Zero && this._texture.GetNativeTexturePtr() == IntPtr.Zero)
        this._texture.UpdateExternalTexture(this._nativeTexture);
      this._textureQuality = QualitySettings.masterTextureLimit;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Native
    {
      public const int PluginID = 262537216;

      [DllImport("AVProVideo")]
      public static extern bool Init(bool linearColorSpace, bool isD3D11NoSingleThreaded);

      [DllImport("AVProVideo")]
      public static extern void Deinit();

      [DllImport("AVProVideo")]
      public static extern IntPtr GetPluginVersion();

      [DllImport("AVProVideo")]
      public static extern bool IsTrialVersion();

      [DllImport("AVProVideo")]
      public static extern IntPtr OpenSource(IntPtr instance, [MarshalAs(UnmanagedType.LPWStr)] string path, int videoApiIndex, bool useHardwareDecoding, bool generateTextureMips, [MarshalAs(UnmanagedType.LPWStr)] string forceAudioOutputDeviceName, bool useUnityAudio, bool forceResample, int channels);

      [DllImport("AVProVideo")]
      public static extern IntPtr OpenSourceFromBuffer(IntPtr instance, byte[] buffer, ulong bufferLength, int videoApiIndex, bool useHardwareDecoding, bool generateTextureMips, [MarshalAs(UnmanagedType.LPWStr)] string forceAudioOutputDeviceName, bool useUnityAudio);

      [DllImport("AVProVideo")]
      public static extern void CloseSource(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern IntPtr GetPlayerDescription(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern int GetLastErrorCode(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern void Play(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern void Pause(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern void SetMuted(IntPtr instance, bool muted);

      [DllImport("AVProVideo")]
      public static extern void SetVolume(IntPtr instance, float volume);

      [DllImport("AVProVideo")]
      public static extern void SetBalance(IntPtr instance, float volume);

      [DllImport("AVProVideo")]
      public static extern void SetLooping(IntPtr instance, bool looping);

      [DllImport("AVProVideo")]
      public static extern bool HasVideo(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern bool HasAudio(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern int GetWidth(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern int GetHeight(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern float GetFrameRate(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern float GetDuration(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern int GetAudioTrackCount(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern bool IsPlaybackStalled(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern bool HasMetaData(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern bool CanPlay(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern bool IsSeeking(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern bool IsFinished(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern bool IsBuffering(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern float GetCurrentTime(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern void SetCurrentTime(IntPtr instance, float time, bool fast);

      [DllImport("AVProVideo")]
      public static extern float GetPlaybackRate(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern void SetPlaybackRate(IntPtr instance, float rate);

      [DllImport("AVProVideo")]
      public static extern int GetAudioTrack(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern void SetAudioTrack(IntPtr instance, int index);

      [DllImport("AVProVideo")]
      public static extern float GetBufferingProgress(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern int GetBufferedRanges(IntPtr instance, float[] timeArray, int arrayCount);

      [DllImport("AVProVideo")]
      public static extern void Update(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern IntPtr GetTexturePointer(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern bool IsTextureTopDown(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern int GetTextureFrameCount(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern long GetTextureTimeStamp(IntPtr instance);

      [DllImport("AVProVideo")]
      public static extern IntPtr GetRenderEventFunc_UpdateAllTextures();

      [DllImport("AVProVideo")]
      public static extern IntPtr GetRenderEventFunc_FreeTextures();

      [DllImport("AVProVideo")]
      public static extern void SetUnityAudioEnabled(IntPtr instance, bool enabled);

      [DllImport("AVProVideo")]
      public static extern void GrabAudio(IntPtr instance, float[] buffer, int floatCount, int channelCount);

      [DllImport("AVProVideo")]
      public static extern int GetAudioChannelCount(IntPtr instance);

      public enum RenderThreadEvent
      {
        UpdateAllTextures,
        FreeTextures,
      }
    }
  }
}
