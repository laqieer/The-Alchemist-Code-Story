// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.MediaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Media Player", -100)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class MediaPlayer : MonoBehaviour
  {
    public MediaPlayer.FileLocation m_VideoLocation = MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
    public string m_VideoPath;
    public bool m_AutoOpen = true;
    public bool m_AutoStart = true;
    public bool m_Loop;
    [Range(0.0f, 1f)]
    public float m_Volume = 1f;
    [SerializeField]
    [Range(-1f, 1f)]
    private float m_Balance;
    public bool m_Muted;
    [SerializeField]
    [Range(-4f, 4f)]
    public float m_PlaybackRate = 1f;
    public bool m_Resample;
    public Resampler.ResampleMode m_ResampleMode;
    [Range(3f, 10f)]
    public int m_ResampleBufferSize = 5;
    private Resampler m_Resampler;
    [SerializeField]
    private bool m_Persistent;
    [SerializeField]
    private VideoMapping m_videoMapping;
    public StereoPacking m_StereoPacking;
    public AlphaPacking m_AlphaPacking;
    public bool m_DisplayDebugStereoColorTint;
    public FilterMode m_FilterMode = (FilterMode) 1;
    public TextureWrapMode m_WrapMode = (TextureWrapMode) 1;
    [Range(0.0f, 16f)]
    public int m_AnisoLevel;
    [SerializeField]
    private bool m_LoadSubtitles;
    [SerializeField]
    private MediaPlayer.FileLocation m_SubtitleLocation = MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
    private MediaPlayer.FileLocation m_queueSubtitleLocation;
    [SerializeField]
    private string m_SubtitlePath;
    private string m_queueSubtitlePath;
    private Coroutine m_loadSubtitlesRoutine;
    [SerializeField]
    private Transform m_AudioHeadTransform;
    [SerializeField]
    private bool m_AudioFocusEnabled;
    [SerializeField]
    private Transform m_AudioFocusTransform;
    [SerializeField]
    [Range(40f, 120f)]
    private float m_AudioFocusWidthDegrees = 90f;
    [SerializeField]
    [Range(-24f, 0.0f)]
    private float m_AudioFocusOffLevelDB;
    [SerializeField]
    private MediaPlayerEvent m_events;
    [SerializeField]
    private int m_eventMask = -1;
    [SerializeField]
    private FileFormat m_forceFileFormat;
    [SerializeField]
    private bool _pauseMediaOnAppPause = true;
    [SerializeField]
    private bool _playMediaOnAppUnpause = true;
    private IMediaControl m_Control;
    private IMediaProducer m_Texture;
    private IMediaInfo m_Info;
    private IMediaPlayer m_Player;
    private IMediaSubtitles m_Subtitles;
    private IDisposable m_Dispose;
    private bool m_VideoOpened;
    private bool m_AutoStartTriggered;
    private bool m_WasPlayingOnPause;
    private Coroutine _renderingCoroutine;
    private static bool s_GlobalStartup;
    private bool m_EventFired_ReadyToPlay;
    private bool m_EventFired_Started;
    private bool m_EventFired_FirstFrameReady;
    private bool m_EventFired_FinishedPlaying;
    private bool m_EventFired_MetaDataReady;
    private bool m_EventState_PlaybackStalled;
    private bool m_EventState_PlaybackBuffering;
    private bool m_EventState_PlaybackSeeking;
    private int m_EventState_PreviousWidth;
    private int m_EventState_PreviousHeight;
    private int m_previousSubtitleIndex = -1;
    private static Camera m_DummyCamera;
    private bool m_FinishedFrameOpenCheck;
    [SerializeField]
    private uint m_sourceSampleRate;
    [SerializeField]
    private uint m_sourceChannels;
    [SerializeField]
    private bool m_manuallySetAudioSourceProperties;
    [SerializeField]
    private MediaPlayer.OptionsWindows _optionsWindows = new MediaPlayer.OptionsWindows();
    [SerializeField]
    private MediaPlayer.OptionsMacOSX _optionsMacOSX = new MediaPlayer.OptionsMacOSX();
    [SerializeField]
    private MediaPlayer.OptionsIOS _optionsIOS = new MediaPlayer.OptionsIOS();
    [SerializeField]
    private MediaPlayer.OptionsTVOS _optionsTVOS = new MediaPlayer.OptionsTVOS();
    [SerializeField]
    private MediaPlayer.OptionsAndroid _optionsAndroid = new MediaPlayer.OptionsAndroid();
    [SerializeField]
    private MediaPlayer.OptionsWindowsPhone _optionsWindowsPhone = new MediaPlayer.OptionsWindowsPhone();
    [SerializeField]
    private MediaPlayer.OptionsWindowsUWP _optionsWindowsUWP = new MediaPlayer.OptionsWindowsUWP();
    [SerializeField]
    private MediaPlayer.OptionsWebGL _optionsWebGL = new MediaPlayer.OptionsWebGL();
    [SerializeField]
    private MediaPlayer.OptionsPS4 _optionsPS4 = new MediaPlayer.OptionsPS4();

    public Resampler FrameResampler => this.m_Resampler;

    public bool Persistent
    {
      get => this.m_Persistent;
      set => this.m_Persistent = value;
    }

    public VideoMapping VideoLayoutMapping
    {
      get => this.m_videoMapping;
      set => this.m_videoMapping = value;
    }

    public virtual IMediaInfo Info => this.m_Info;

    public virtual IMediaControl Control => this.m_Control;

    public virtual IMediaPlayer Player => this.m_Player;

    public virtual IMediaProducer TextureProducer => this.m_Texture;

    public virtual IMediaSubtitles Subtitles => this.m_Subtitles;

    public MediaPlayerEvent Events
    {
      get
      {
        if (this.m_events == null)
          this.m_events = new MediaPlayerEvent();
        return this.m_events;
      }
    }

    public bool VideoOpened => this.m_VideoOpened;

    public bool PauseMediaOnAppPause
    {
      get => this._pauseMediaOnAppPause;
      set => this._pauseMediaOnAppPause = value;
    }

    public bool PlayMediaOnAppUnpause
    {
      get => this._playMediaOnAppUnpause;
      set => this._playMediaOnAppUnpause = value;
    }

    public FileFormat ForceFileFormat
    {
      get => this.m_forceFileFormat;
      set => this.m_forceFileFormat = value;
    }

    public Transform AudioHeadTransform
    {
      set => this.m_AudioHeadTransform = value;
      get => this.m_AudioHeadTransform;
    }

    public bool AudioFocusEnabled
    {
      get => this.m_AudioFocusEnabled;
      set => this.m_AudioFocusEnabled = value;
    }

    public float AudioFocusOffLevelDB
    {
      get => this.m_AudioFocusOffLevelDB;
      set => this.m_AudioFocusOffLevelDB = value;
    }

    public float AudioFocusWidthDegrees
    {
      get => this.m_AudioFocusWidthDegrees;
      set => this.m_AudioFocusWidthDegrees = value;
    }

    public Transform AudioFocusTransform
    {
      get => this.m_AudioFocusTransform;
      set => this.m_AudioFocusTransform = value;
    }

    public MediaPlayer.OptionsWindows PlatformOptionsWindows => this._optionsWindows;

    public MediaPlayer.OptionsMacOSX PlatformOptionsMacOSX => this._optionsMacOSX;

    public MediaPlayer.OptionsIOS PlatformOptionsIOS => this._optionsIOS;

    public MediaPlayer.OptionsTVOS PlatformOptionsTVOS => this._optionsTVOS;

    public MediaPlayer.OptionsAndroid PlatformOptionsAndroid => this._optionsAndroid;

    public MediaPlayer.OptionsWindowsPhone PlatformOptionsWindowsPhone => this._optionsWindowsPhone;

    public MediaPlayer.OptionsWindowsUWP PlatformOptionsWindowsUWP => this._optionsWindowsUWP;

    public MediaPlayer.OptionsWebGL PlatformOptionsWebGL => this._optionsWebGL;

    public MediaPlayer.OptionsPS4 PlatformOptionsPS4 => this._optionsPS4;

    private void Awake()
    {
      if (!this.m_Persistent)
        return;
      Object.DontDestroyOnLoad((Object) ((Component) this).gameObject);
    }

    protected void Initialise()
    {
      BaseMediaPlayer platformMediaPlayer = this.CreatePlatformMediaPlayer();
      if (platformMediaPlayer == null)
        return;
      this.m_Control = (IMediaControl) platformMediaPlayer;
      this.m_Texture = (IMediaProducer) platformMediaPlayer;
      this.m_Info = (IMediaInfo) platformMediaPlayer;
      this.m_Player = (IMediaPlayer) platformMediaPlayer;
      this.m_Subtitles = (IMediaSubtitles) platformMediaPlayer;
      this.m_Dispose = (IDisposable) platformMediaPlayer;
      if (MediaPlayer.s_GlobalStartup)
        return;
      MediaPlayer.s_GlobalStartup = true;
    }

    private void Start()
    {
      if (this.m_Control == null)
        this.Initialise();
      if (this.m_Control == null)
        return;
      if (this.m_AutoOpen)
      {
        this.OpenVideoFromFile();
        if (this.m_LoadSubtitles && this.m_Subtitles != null && !string.IsNullOrEmpty(this.m_SubtitlePath))
          this.EnableSubtitles(this.m_SubtitleLocation, this.m_SubtitlePath);
      }
      this.StartRenderCoroutine();
    }

    public bool OpenVideoFromFile(MediaPlayer.FileLocation location, string path, bool autoPlay = true)
    {
      this.m_VideoLocation = location;
      this.m_VideoPath = path;
      this.m_AutoStart = autoPlay;
      if (this.m_Control == null)
      {
        this.m_AutoOpen = false;
        this.Initialise();
      }
      return this.OpenVideoFromFile();
    }

    public bool OpenVideoFromBuffer(byte[] buffer, bool autoPlay = true)
    {
      this.m_VideoLocation = MediaPlayer.FileLocation.AbsolutePathOrURL;
      this.m_VideoPath = nameof (buffer);
      this.m_AutoStart = autoPlay;
      if (this.m_Control == null)
        this.Initialise();
      return this.OpenVideoFromBufferInternal(buffer);
    }

    public bool StartOpenChunkedVideoFromBuffer(ulong length, bool autoPlay = true)
    {
      this.m_VideoLocation = MediaPlayer.FileLocation.AbsolutePathOrURL;
      this.m_VideoPath = "buffer";
      this.m_AutoStart = autoPlay;
      if (this.m_Control == null)
        this.Initialise();
      return this.StartOpenVideoFromBufferInternal(length);
    }

    public bool AddChunkToVideoBuffer(byte[] chunk, ulong offset, ulong chunkSize)
    {
      return this.AddChunkToBufferInternal(chunk, offset, chunkSize);
    }

    public bool EndOpenChunkedVideoFromBuffer() => this.EndOpenVideoFromBufferInternal();

    public bool SubtitlesEnabled => this.m_LoadSubtitles;

    public string SubtitlePath => this.m_SubtitlePath;

    public MediaPlayer.FileLocation SubtitleLocation => this.m_SubtitleLocation;

    public bool EnableSubtitles(MediaPlayer.FileLocation fileLocation, string filePath)
    {
      bool flag1 = false;
      if (this.m_Subtitles != null)
      {
        if (!string.IsNullOrEmpty(filePath))
        {
          string platformFilePath = this.GetPlatformFilePath(MediaPlayer.GetPlatform(), ref filePath, ref fileLocation);
          bool flag2 = true;
          if (platformFilePath.Contains("://"))
            flag2 = false;
          if (flag2 && !File.Exists(platformFilePath))
          {
            Debug.LogError((object) ("[AVProVideo] Subtitle file not found: " + platformFilePath), (Object) this);
          }
          else
          {
            this.m_previousSubtitleIndex = -1;
            try
            {
              if (platformFilePath.Contains("://"))
              {
                if (this.m_loadSubtitlesRoutine != null)
                {
                  this.StopCoroutine(this.m_loadSubtitlesRoutine);
                  this.m_loadSubtitlesRoutine = (Coroutine) null;
                }
                this.m_loadSubtitlesRoutine = this.StartCoroutine(this.LoadSubtitlesCoroutine(platformFilePath, fileLocation, filePath));
              }
              else if (this.m_Subtitles.LoadSubtitlesSRT(File.ReadAllText(platformFilePath)))
              {
                this.m_SubtitleLocation = fileLocation;
                this.m_SubtitlePath = filePath;
                this.m_LoadSubtitles = false;
                flag1 = true;
              }
              else
                Debug.LogError((object) ("[AVProVideo] Failed to load subtitles" + platformFilePath), (Object) this);
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ("[AVProVideo] Failed to load subtitles " + platformFilePath), (Object) this);
              Debug.LogException(ex, (Object) this);
            }
          }
        }
        else
          Debug.LogError((object) "[AVProVideo] No subtitle file path specified", (Object) this);
      }
      else
      {
        this.m_queueSubtitleLocation = fileLocation;
        this.m_queueSubtitlePath = filePath;
      }
      return flag1;
    }

    [DebuggerHidden]
    private IEnumerator LoadSubtitlesCoroutine(
      string url,
      MediaPlayer.FileLocation fileLocation,
      string filePath)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MediaPlayer.\u003CLoadSubtitlesCoroutine\u003Ec__Iterator0()
      {
        url = url,
        fileLocation = fileLocation,
        filePath = filePath,
        \u0024this = this
      };
    }

    public void DisableSubtitles()
    {
      if (this.m_loadSubtitlesRoutine != null)
      {
        this.StopCoroutine(this.m_loadSubtitlesRoutine);
        this.m_loadSubtitlesRoutine = (Coroutine) null;
      }
      if (this.m_Subtitles != null)
      {
        this.m_previousSubtitleIndex = -1;
        this.m_LoadSubtitles = false;
        this.m_Subtitles.LoadSubtitlesSRT(string.Empty);
      }
      else
        this.m_queueSubtitlePath = string.Empty;
    }

    private bool OpenVideoFromBufferInternal(byte[] buffer)
    {
      bool flag = false;
      if (this.m_Control != null)
      {
        this.CloseVideo();
        this.m_VideoOpened = true;
        this.m_AutoStartTriggered = !this.m_AutoStart;
        if (!this.m_Control.OpenVideoFromBuffer(buffer))
        {
          Debug.LogError((object) "[AVProVideo] Failed to open buffer", (Object) this);
          if (this.GetCurrentPlatformOptions() != this.PlatformOptionsWindows || this.PlatformOptionsWindows.videoApi != Windows.VideoApi.DirectShow)
            Debug.LogError((object) "[AVProVideo] Loading from buffer is currently only supported in Windows when using the DirectShow API");
        }
        else
        {
          this.SetPlaybackOptions();
          flag = true;
          this.StartRenderCoroutine();
        }
      }
      return flag;
    }

    private bool StartOpenVideoFromBufferInternal(ulong length)
    {
      bool flag = false;
      if (this.m_Control != null)
      {
        this.CloseVideo();
        this.m_VideoOpened = true;
        this.m_AutoStartTriggered = !this.m_AutoStart;
        if (!this.m_Control.StartOpenVideoFromBuffer(length))
        {
          Debug.LogError((object) "[AVProVideo] Failed to start open video from buffer", (Object) this);
          if (this.GetCurrentPlatformOptions() != this.PlatformOptionsWindows || this.PlatformOptionsWindows.videoApi != Windows.VideoApi.DirectShow)
            Debug.LogError((object) "[AVProVideo] Loading from buffer is currently only supported in Windows when using the DirectShow API");
        }
        else
        {
          this.SetPlaybackOptions();
          flag = true;
          this.StartRenderCoroutine();
        }
      }
      return flag;
    }

    private bool AddChunkToBufferInternal(byte[] chunk, ulong offset, ulong chunkSize)
    {
      return this.Control != null && this.Control.AddChunkToVideoBuffer(chunk, offset, chunkSize);
    }

    private bool EndOpenVideoFromBufferInternal()
    {
      return this.Control != null && this.Control.EndOpenVideoFromBuffer();
    }

    private bool OpenVideoFromFile()
    {
      bool flag1 = false;
      if (this.m_Control != null)
      {
        this.CloseVideo();
        this.m_VideoOpened = true;
        this.m_AutoStartTriggered = !this.m_AutoStart;
        this.m_FinishedFrameOpenCheck = true;
        long platformFileOffset = this.GetPlatformFileOffset();
        string platformFilePath = this.GetPlatformFilePath(MediaPlayer.GetPlatform(), ref this.m_VideoPath, ref this.m_VideoLocation);
        if (!string.IsNullOrEmpty(this.m_VideoPath))
        {
          string httpHeaderJson = (string) null;
          bool flag2 = true;
          if (platformFilePath.Contains("://"))
          {
            flag2 = false;
            httpHeaderJson = this.GetPlatformHttpHeaderJson();
          }
          if (flag2 && !File.Exists(platformFilePath))
          {
            Debug.LogError((object) ("[AVProVideo] File not found: " + platformFilePath), (Object) this);
          }
          else
          {
            if (this._optionsWindows.enableAudio360)
              this.m_Control.SetAudioChannelMode(this._optionsWindows.audio360ChannelMode);
            else
              this.m_Control.SetAudioChannelMode(Audio360ChannelMode.INVALID);
            if (!this.m_Control.OpenVideoFromFile(platformFilePath, platformFileOffset, httpHeaderJson, !this.m_manuallySetAudioSourceProperties ? 0U : this.m_sourceSampleRate, !this.m_manuallySetAudioSourceProperties ? 0U : this.m_sourceChannels, (int) this.m_forceFileFormat))
            {
              Debug.LogError((object) ("[AVProVideo] Failed to open " + platformFilePath), (Object) this);
            }
            else
            {
              this.SetPlaybackOptions();
              flag1 = true;
              this.StartRenderCoroutine();
            }
          }
        }
        else
          Debug.LogError((object) "[AVProVideo] No file path specified", (Object) this);
      }
      return flag1;
    }

    private void SetPlaybackOptions()
    {
      if (this.m_Control == null)
        return;
      this.m_Control.SetLooping(this.m_Loop);
      this.m_Control.SetPlaybackRate(this.m_PlaybackRate);
      this.m_Control.SetVolume(this.m_Volume);
      this.m_Control.SetBalance(this.m_Balance);
      this.m_Control.MuteAudio(this.m_Muted);
      this.m_Control.SetTextureProperties(this.m_FilterMode, this.m_WrapMode, this.m_AnisoLevel);
      MediaPlayer.PlatformOptions currentPlatformOptions = this.GetCurrentPlatformOptions();
      if (currentPlatformOptions == null)
        return;
      this.m_Control.SetKeyServerURL(currentPlatformOptions.GetKeyServerURL());
      this.m_Control.SetKeyServerAuthToken(currentPlatformOptions.GetKeyServerAuthToken());
      this.m_Control.SetDecryptionKeyBase64(currentPlatformOptions.GetDecryptionKey());
    }

    public void CloseVideo()
    {
      if (this.m_Control != null)
      {
        if (this.m_events != null && this.m_VideoOpened && this.m_events.HasListeners() && this.IsHandleEvent(MediaPlayerEvent.EventType.Closing))
          this.m_events.Invoke(this, MediaPlayerEvent.EventType.Closing, ErrorCode.None);
        this.m_AutoStartTriggered = false;
        this.m_VideoOpened = false;
        this.m_EventFired_MetaDataReady = false;
        this.m_EventFired_ReadyToPlay = false;
        this.m_EventFired_Started = false;
        this.m_EventFired_FirstFrameReady = false;
        this.m_EventFired_FinishedPlaying = false;
        this.m_EventState_PlaybackBuffering = false;
        this.m_EventState_PlaybackSeeking = false;
        this.m_EventState_PlaybackStalled = false;
        this.m_EventState_PreviousWidth = 0;
        this.m_EventState_PreviousHeight = 0;
        if (this.m_loadSubtitlesRoutine != null)
        {
          this.StopCoroutine(this.m_loadSubtitlesRoutine);
          this.m_loadSubtitlesRoutine = (Coroutine) null;
        }
        this.m_previousSubtitleIndex = -1;
        this.m_Control.CloseVideo();
      }
      if (this.m_Resampler != null)
        this.m_Resampler.Reset();
      this.StopRenderCoroutine();
    }

    public void Play()
    {
      if (this.m_Control != null && this.m_Control.CanPlay())
      {
        this.m_Control.Play();
        this.m_EventFired_ReadyToPlay = true;
      }
      else
      {
        this.m_AutoStart = true;
        this.m_AutoStartTriggered = false;
      }
    }

    public void Pause()
    {
      if (this.m_Control != null && this.m_Control.IsPlaying())
        this.m_Control.Pause();
      this.m_WasPlayingOnPause = false;
    }

    public void Stop()
    {
      if (this.m_Control == null)
        return;
      this.m_Control.Stop();
    }

    public void Rewind(bool pause)
    {
      if (this.m_Control == null)
        return;
      if (pause)
        this.Pause();
      this.m_Control.Rewind();
    }

    protected virtual void Update()
    {
      if (this.m_Control == null)
        return;
      if (this.m_VideoOpened && this.m_AutoStart && !this.m_AutoStartTriggered && this.m_Control.CanPlay())
      {
        this.m_AutoStartTriggered = true;
        this.Play();
      }
      if (this._renderingCoroutine == null && this.m_Control.CanPlay())
        this.StartRenderCoroutine();
      if (this.m_Subtitles != null && !string.IsNullOrEmpty(this.m_queueSubtitlePath))
      {
        this.EnableSubtitles(this.m_queueSubtitleLocation, this.m_queueSubtitlePath);
        this.m_queueSubtitlePath = string.Empty;
      }
      this.UpdateAudioHeadTransform();
      this.UpdateAudioFocus();
      this.m_Player.Update();
      this.UpdateErrors();
      this.UpdateEvents();
    }

    private void LateUpdate() => this.UpdateResampler();

    private void UpdateResampler()
    {
      if (this.m_Resample && this.m_Resampler == null)
        this.m_Resampler = new Resampler(this, ((Object) ((Component) this).gameObject).name, this.m_ResampleBufferSize, this.m_ResampleMode);
      if (this.m_Resampler == null)
        return;
      this.m_Resampler.Update();
      this.m_Resampler.UpdateTimestamp();
    }

    private void OnEnable()
    {
      if (this.m_Control != null && this.m_WasPlayingOnPause)
      {
        this.m_AutoStart = true;
        this.m_AutoStartTriggered = false;
        this.m_WasPlayingOnPause = false;
      }
      if (this.m_Player != null)
        this.m_Player.OnEnable();
      this.StartRenderCoroutine();
    }

    private void OnDisable()
    {
      if (this.m_Control != null && this.m_Control.IsPlaying())
      {
        this.m_WasPlayingOnPause = true;
        this.Pause();
      }
      this.StopRenderCoroutine();
    }

    protected virtual void OnDestroy()
    {
      this.CloseVideo();
      if (this.m_Dispose != null)
      {
        this.m_Dispose.Dispose();
        this.m_Dispose = (IDisposable) null;
      }
      this.m_Control = (IMediaControl) null;
      this.m_Texture = (IMediaProducer) null;
      this.m_Info = (IMediaInfo) null;
      this.m_Player = (IMediaPlayer) null;
      if (this.m_Resampler == null)
        return;
      this.m_Resampler.Release();
      this.m_Resampler = (Resampler) null;
    }

    private void OnApplicationQuit()
    {
      if (!MediaPlayer.s_GlobalStartup)
        return;
      MediaPlayer[] objectsOfTypeAll = Resources.FindObjectsOfTypeAll<MediaPlayer>();
      if (objectsOfTypeAll != null && objectsOfTypeAll.Length > 0)
      {
        for (int index = 0; index < objectsOfTypeAll.Length; ++index)
        {
          objectsOfTypeAll[index].CloseVideo();
          objectsOfTypeAll[index].OnDestroy();
        }
      }
      WindowsMediaPlayer.DeinitPlatform();
      MediaPlayer.s_GlobalStartup = false;
    }

    private void StartRenderCoroutine()
    {
      if (this._renderingCoroutine != null)
        return;
      this._renderingCoroutine = this.StartCoroutine(this.FinalRenderCapture());
    }

    private void StopRenderCoroutine()
    {
      if (this._renderingCoroutine == null)
        return;
      this.StopCoroutine(this._renderingCoroutine);
      this._renderingCoroutine = (Coroutine) null;
    }

    [DebuggerHidden]
    private IEnumerator FinalRenderCapture()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MediaPlayer.\u003CFinalRenderCapture\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    public static Platform GetPlatform() => Platform.Windows;

    public MediaPlayer.PlatformOptions GetCurrentPlatformOptions()
    {
      return (MediaPlayer.PlatformOptions) this._optionsWindows;
    }

    public static string GetPath(MediaPlayer.FileLocation location)
    {
      string path = string.Empty;
      switch (location)
      {
        case MediaPlayer.FileLocation.RelativeToProjectFolder:
          path = Path.GetFullPath(Path.Combine(Application.dataPath, "..")).Replace('\\', '/');
          break;
        case MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder:
          path = Application.streamingAssetsPath;
          break;
        case MediaPlayer.FileLocation.RelativeToDataFolder:
          path = Application.dataPath;
          break;
        case MediaPlayer.FileLocation.RelativeToPeristentDataFolder:
          path = Application.persistentDataPath;
          break;
      }
      return path;
    }

    public static string GetFilePath(string path, MediaPlayer.FileLocation location)
    {
      string filePath = string.Empty;
      if (!string.IsNullOrEmpty(path))
      {
        switch (location)
        {
          case MediaPlayer.FileLocation.AbsolutePathOrURL:
            filePath = path;
            break;
          case MediaPlayer.FileLocation.RelativeToProjectFolder:
          case MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder:
          case MediaPlayer.FileLocation.RelativeToDataFolder:
          case MediaPlayer.FileLocation.RelativeToPeristentDataFolder:
            filePath = Path.Combine(MediaPlayer.GetPath(location), path);
            break;
        }
      }
      return filePath;
    }

    private long GetPlatformFileOffset() => 0;

    private string GetPlatformHttpHeaderJson()
    {
      string platformHttpHeaderJson = (string) null;
      if (!string.IsNullOrEmpty(platformHttpHeaderJson))
        platformHttpHeaderJson = platformHttpHeaderJson.Trim();
      return platformHttpHeaderJson;
    }

    private string GetPlatformFilePath(
      Platform platform,
      ref string filePath,
      ref MediaPlayer.FileLocation fileLocation)
    {
      string empty = string.Empty;
      if (platform != Platform.Unknown)
      {
        MediaPlayer.PlatformOptions currentPlatformOptions = this.GetCurrentPlatformOptions();
        if (currentPlatformOptions != null && currentPlatformOptions.overridePath)
        {
          filePath = currentPlatformOptions.path;
          fileLocation = currentPlatformOptions.pathLocation;
        }
      }
      return MediaPlayer.GetFilePath(filePath, fileLocation);
    }

    public virtual BaseMediaPlayer CreatePlatformMediaPlayer()
    {
      BaseMediaPlayer platformMediaPlayer = (BaseMediaPlayer) null;
      if (WindowsMediaPlayer.InitialisePlatform())
        platformMediaPlayer = (BaseMediaPlayer) new WindowsMediaPlayer(this._optionsWindows.videoApi, this._optionsWindows.useHardwareDecoding, this._optionsWindows.useTextureMips, this._optionsWindows.hintAlphaChannel, this._optionsWindows.useLowLatency, this._optionsWindows.forceAudioOutputDeviceName, this._optionsWindows.useUnityAudio, this._optionsWindows.forceAudioResample, this._optionsWindows.preferredFilters);
      if (platformMediaPlayer == null)
      {
        Debug.LogError((object) string.Format("[AVProVideo] Not supported on this platform {0} {1} {2} {3}.  Using null media player!", (object) Application.platform, (object) SystemInfo.deviceModel, (object) SystemInfo.processorType, (object) SystemInfo.operatingSystem));
        platformMediaPlayer = (BaseMediaPlayer) new NullMediaPlayer();
      }
      return platformMediaPlayer;
    }

    private bool ForceWaitForNewFrame(int lastFrameCount, float timeoutMs)
    {
      bool flag = false;
      DateTime now = DateTime.Now;
      int num = 0;
      while (this.Control != null && (DateTime.Now - now).TotalMilliseconds < (double) timeoutMs)
      {
        this.m_Player.Update();
        if (lastFrameCount != this.TextureProducer.GetTextureFrameCount())
        {
          flag = true;
          break;
        }
        ++num;
      }
      this.m_Player.Render();
      return flag;
    }

    private void UpdateAudioFocus()
    {
      this.m_Control.SetAudioFocusEnabled(this.m_AudioFocusEnabled);
      this.m_Control.SetAudioFocusProperties(this.m_AudioFocusOffLevelDB, this.m_AudioFocusWidthDegrees);
      this.m_Control.SetAudioFocusRotation(!Object.op_Equality((Object) this.m_AudioFocusTransform, (Object) null) ? this.m_AudioFocusTransform.rotation : Quaternion.identity);
    }

    private void UpdateAudioHeadTransform()
    {
      if (Object.op_Inequality((Object) this.m_AudioHeadTransform, (Object) null))
        this.m_Control.SetAudioHeadRotation(this.m_AudioHeadTransform.rotation);
      else
        this.m_Control.ResetAudioHeadRotation();
    }

    private void UpdateErrors()
    {
      ErrorCode lastError = this.m_Control.GetLastError();
      if (lastError == ErrorCode.None)
        return;
      Debug.LogError((object) ("[AVProVideo] Error: " + Helper.GetErrorMessage(lastError)));
      if (this.m_events == null || !this.m_events.HasListeners() || !this.IsHandleEvent(MediaPlayerEvent.EventType.Error))
        return;
      this.m_events.Invoke(this, MediaPlayerEvent.EventType.Error, lastError);
    }

    private void UpdateEvents()
    {
      if (this.m_events == null || this.m_Control == null || !this.m_events.HasListeners())
        return;
      this.m_FinishedFrameOpenCheck = false;
      if (this.IsHandleEvent(MediaPlayerEvent.EventType.FinishedPlaying) && this.FireEventIfPossible(MediaPlayerEvent.EventType.FinishedPlaying, this.m_EventFired_FinishedPlaying))
        this.m_EventFired_FinishedPlaying = !this.m_FinishedFrameOpenCheck;
      if (this.m_EventFired_Started && this.IsHandleEvent(MediaPlayerEvent.EventType.Started) && this.m_Control != null && !this.m_Control.IsPlaying() && !this.m_Control.IsSeeking())
        this.m_EventFired_Started = false;
      if (this.m_EventFired_FinishedPlaying && this.IsHandleEvent(MediaPlayerEvent.EventType.FinishedPlaying) && this.m_Control != null && this.m_Control.IsPlaying() && !this.m_Control.IsFinished())
      {
        bool flag = false;
        if (this.m_Info.HasVideo())
        {
          float num = 1000f / this.m_Info.GetVideoFrameRate();
          if ((double) this.m_Info.GetDurationMs() - (double) this.m_Control.GetCurrentTimeMs() > (double) num)
            flag = true;
        }
        else if ((double) this.m_Control.GetCurrentTimeMs() < (double) this.m_Info.GetDurationMs())
          flag = true;
        if (flag)
          this.m_EventFired_FinishedPlaying = false;
      }
      this.m_EventFired_MetaDataReady = this.FireEventIfPossible(MediaPlayerEvent.EventType.MetaDataReady, this.m_EventFired_MetaDataReady);
      this.m_EventFired_ReadyToPlay = this.FireEventIfPossible(MediaPlayerEvent.EventType.ReadyToPlay, this.m_EventFired_ReadyToPlay);
      this.m_EventFired_Started = this.FireEventIfPossible(MediaPlayerEvent.EventType.Started, this.m_EventFired_Started);
      this.m_EventFired_FirstFrameReady = this.FireEventIfPossible(MediaPlayerEvent.EventType.FirstFrameReady, this.m_EventFired_FirstFrameReady);
      if (this.FireEventIfPossible(MediaPlayerEvent.EventType.SubtitleChange, false))
        this.m_previousSubtitleIndex = this.m_Subtitles.GetSubtitleIndex();
      if (this.FireEventIfPossible(MediaPlayerEvent.EventType.ResolutionChanged, false))
      {
        this.m_EventState_PreviousWidth = this.m_Info.GetVideoWidth();
        this.m_EventState_PreviousHeight = this.m_Info.GetVideoHeight();
      }
      if (this.IsHandleEvent(MediaPlayerEvent.EventType.Stalled))
      {
        bool flag = this.m_Info.IsPlaybackStalled();
        if (flag != this.m_EventState_PlaybackStalled)
        {
          this.m_EventState_PlaybackStalled = flag;
          this.FireEventIfPossible(!this.m_EventState_PlaybackStalled ? MediaPlayerEvent.EventType.Unstalled : MediaPlayerEvent.EventType.Stalled, false);
        }
      }
      if (this.IsHandleEvent(MediaPlayerEvent.EventType.StartedSeeking))
      {
        bool flag = this.m_Control.IsSeeking();
        if (flag != this.m_EventState_PlaybackSeeking)
        {
          this.m_EventState_PlaybackSeeking = flag;
          this.FireEventIfPossible(!this.m_EventState_PlaybackSeeking ? MediaPlayerEvent.EventType.FinishedSeeking : MediaPlayerEvent.EventType.StartedSeeking, false);
        }
      }
      if (!this.IsHandleEvent(MediaPlayerEvent.EventType.StartedBuffering))
        return;
      bool flag1 = this.m_Control.IsBuffering();
      if (flag1 == this.m_EventState_PlaybackBuffering)
        return;
      this.m_EventState_PlaybackBuffering = flag1;
      this.FireEventIfPossible(!this.m_EventState_PlaybackBuffering ? MediaPlayerEvent.EventType.FinishedBuffering : MediaPlayerEvent.EventType.StartedBuffering, false);
    }

    protected bool IsHandleEvent(MediaPlayerEvent.EventType eventType)
    {
      return ((long) (uint) this.m_eventMask & (long) (1 << (int) (eventType & (MediaPlayerEvent.EventType.PropertiesChanged | MediaPlayerEvent.EventType.PlaylistItemChanged)))) != 0L;
    }

    private bool FireEventIfPossible(MediaPlayerEvent.EventType eventType, bool hasFired)
    {
      if (this.CanFireEvent(eventType, hasFired))
      {
        hasFired = true;
        this.m_events.Invoke(this, eventType, ErrorCode.None);
      }
      return hasFired;
    }

    private bool CanFireEvent(MediaPlayerEvent.EventType et, bool hasFired)
    {
      bool flag = false;
      if (this.m_events != null && this.m_Control != null && !hasFired && this.IsHandleEvent(et))
      {
        switch (et)
        {
          case MediaPlayerEvent.EventType.MetaDataReady:
            flag = this.m_Control.HasMetaData();
            break;
          case MediaPlayerEvent.EventType.ReadyToPlay:
            flag = !this.m_Control.IsPlaying() && this.m_Control.CanPlay() && !this.m_AutoStart;
            break;
          case MediaPlayerEvent.EventType.Started:
            flag = this.m_Control.IsPlaying();
            break;
          case MediaPlayerEvent.EventType.FirstFrameReady:
            flag = this.m_Texture != null && this.m_Control.CanPlay() && this.m_Texture.GetTextureFrameCount() > 0;
            break;
          case MediaPlayerEvent.EventType.FinishedPlaying:
            flag = !this.m_Control.IsLooping() && this.m_Control.CanPlay() && this.m_Control.IsFinished() || (double) this.m_Control.GetCurrentTimeMs() > (double) this.m_Info.GetDurationMs() && !this.m_Control.IsLooping();
            break;
          case MediaPlayerEvent.EventType.SubtitleChange:
            flag = this.m_previousSubtitleIndex != this.m_Subtitles.GetSubtitleIndex();
            break;
          case MediaPlayerEvent.EventType.Stalled:
            flag = this.m_Info.IsPlaybackStalled();
            break;
          case MediaPlayerEvent.EventType.Unstalled:
            flag = !this.m_Info.IsPlaybackStalled();
            break;
          case MediaPlayerEvent.EventType.ResolutionChanged:
            flag = this.m_Info != null && (this.m_EventState_PreviousWidth != this.m_Info.GetVideoWidth() || this.m_EventState_PreviousHeight != this.m_Info.GetVideoHeight());
            break;
          case MediaPlayerEvent.EventType.StartedSeeking:
            flag = this.m_Control.IsSeeking();
            break;
          case MediaPlayerEvent.EventType.FinishedSeeking:
            flag = !this.m_Control.IsSeeking();
            break;
          case MediaPlayerEvent.EventType.StartedBuffering:
            flag = this.m_Control.IsBuffering();
            break;
          case MediaPlayerEvent.EventType.FinishedBuffering:
            flag = !this.m_Control.IsBuffering();
            break;
          default:
            Debug.LogWarning((object) "[AVProVideo] Unhandled event type");
            break;
        }
      }
      return flag;
    }

    private void OnApplicationFocus(bool focusStatus)
    {
    }

    private void OnApplicationPause(bool pauseStatus)
    {
    }

    [ContextMenu("Save Frame To PNG")]
    public void SaveFrameToPng()
    {
      Texture2D frame = this.ExtractFrame((Texture2D) null);
      if (!Object.op_Inequality((Object) frame, (Object) null))
        return;
      byte[] png = ImageConversion.EncodeToPNG(frame);
      if (png != null)
        File.WriteAllBytes("frame-" + Mathf.FloorToInt(this.Control.GetCurrentTimeMs()).ToString("D8") + ".png", png);
      Object.Destroy((Object) frame);
    }

    private static Camera GetDummyCamera()
    {
      if (Object.op_Equality((Object) MediaPlayer.m_DummyCamera, (Object) null))
      {
        GameObject gameObject1 = GameObject.Find("AVPro Video Dummy Camera");
        if (Object.op_Equality((Object) gameObject1, (Object) null))
        {
          GameObject gameObject2 = new GameObject("AVPro Video Dummy Camera");
          ((Object) gameObject2).hideFlags = (HideFlags) 53;
          gameObject2.SetActive(false);
          Object.DontDestroyOnLoad((Object) gameObject2);
          MediaPlayer.m_DummyCamera = gameObject2.AddComponent<Camera>();
          ((Object) MediaPlayer.m_DummyCamera).hideFlags = (HideFlags) 54;
          MediaPlayer.m_DummyCamera.cullingMask = 0;
          MediaPlayer.m_DummyCamera.clearFlags = (CameraClearFlags) 4;
          ((Behaviour) MediaPlayer.m_DummyCamera).enabled = false;
        }
        else
          MediaPlayer.m_DummyCamera = gameObject1.GetComponent<Camera>();
      }
      return MediaPlayer.m_DummyCamera;
    }

    [DebuggerHidden]
    private IEnumerator ExtractFrameCoroutine(
      Texture2D target,
      MediaPlayer.ProcessExtractedFrame callback,
      float timeSeconds = -1f,
      bool accurateSeek = true,
      int timeoutMs = 1000)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MediaPlayer.\u003CExtractFrameCoroutine\u003Ec__Iterator2()
      {
        target = target,
        timeSeconds = timeSeconds,
        accurateSeek = accurateSeek,
        callback = callback,
        \u0024this = this
      };
    }

    public void ExtractFrameAsync(
      Texture2D target,
      MediaPlayer.ProcessExtractedFrame callback,
      float timeSeconds = -1f,
      bool accurateSeek = true,
      int timeoutMs = 1000)
    {
      this.StartCoroutine(this.ExtractFrameCoroutine(target, callback, timeSeconds, accurateSeek, timeoutMs));
    }

    public Texture2D ExtractFrame(
      Texture2D target,
      float timeSeconds = -1f,
      bool accurateSeek = true,
      int timeoutMs = 1000)
    {
      Texture2D frame1 = target;
      Texture frame2 = this.ExtractFrame(timeSeconds, accurateSeek, timeoutMs);
      if (Object.op_Inequality((Object) frame2, (Object) null))
        frame1 = Helper.GetReadableTexture(frame2, this.TextureProducer.RequiresVerticalFlip(), Helper.GetOrientation(this.Info.GetTextureTransform()), target);
      return frame1;
    }

    private Texture ExtractFrame(float timeSeconds = -1f, bool accurateSeek = true, int timeoutMs = 1000)
    {
      Texture frame = (Texture) null;
      if (this.m_Control != null)
      {
        if ((double) timeSeconds >= 0.0)
        {
          this.Pause();
          float timeMs = timeSeconds * 1000f;
          if (Object.op_Inequality((Object) this.TextureProducer.GetTexture(), (Object) null) && (double) this.m_Control.GetCurrentTimeMs() == (double) timeMs)
          {
            frame = this.TextureProducer.GetTexture();
          }
          else
          {
            int textureFrameCount = this.TextureProducer.GetTextureFrameCount();
            if (accurateSeek)
              this.m_Control.Seek(timeMs);
            else
              this.m_Control.SeekFast(timeMs);
            this.ForceWaitForNewFrame(textureFrameCount, (float) timeoutMs);
            frame = this.TextureProducer.GetTexture();
          }
        }
        else
          frame = this.TextureProducer.GetTexture();
      }
      return frame;
    }

    [Serializable]
    public class Setup
    {
      public bool persistent;
    }

    public enum FileLocation
    {
      AbsolutePathOrURL,
      RelativeToProjectFolder,
      RelativeToStreamingAssetsFolder,
      RelativeToDataFolder,
      RelativeToPeristentDataFolder,
    }

    [Serializable]
    public class PlatformOptions
    {
      public bool overridePath;
      public MediaPlayer.FileLocation pathLocation = MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
      public string path;

      public virtual bool IsModified() => this.overridePath;

      public virtual string GetKeyServerURL() => (string) null;

      public virtual string GetKeyServerAuthToken() => (string) null;

      public virtual string GetDecryptionKey() => (string) null;
    }

    [Serializable]
    public class OptionsWindows : MediaPlayer.PlatformOptions
    {
      public Windows.VideoApi videoApi;
      public bool useHardwareDecoding = true;
      public bool useUnityAudio;
      public bool forceAudioResample = true;
      public bool useTextureMips;
      public bool hintAlphaChannel;
      public bool useLowLatency;
      public string forceAudioOutputDeviceName = string.Empty;
      public List<string> preferredFilters = new List<string>();
      public bool enableAudio360;
      public Audio360ChannelMode audio360ChannelMode;

      public override bool IsModified()
      {
        return base.IsModified() || !this.useHardwareDecoding || this.useTextureMips || this.hintAlphaChannel || this.useLowLatency || this.useUnityAudio || this.videoApi != Windows.VideoApi.MediaFoundation || !this.forceAudioResample || this.enableAudio360 || this.audio360ChannelMode != Audio360ChannelMode.TBE_8_2 || !string.IsNullOrEmpty(this.forceAudioOutputDeviceName) || this.preferredFilters.Count != 0;
      }
    }

    [Serializable]
    public class OptionsApple : MediaPlayer.PlatformOptions
    {
      [Multiline]
      public string httpHeaderJson;
      public string keyServerURLOverride;
      public string keyServerAuthToken;
      [Multiline]
      public string base64EncodedKeyBlob;

      public override bool IsModified()
      {
        return base.IsModified() || !string.IsNullOrEmpty(this.httpHeaderJson) || !string.IsNullOrEmpty(this.keyServerURLOverride) || !string.IsNullOrEmpty(this.keyServerAuthToken) || !string.IsNullOrEmpty(this.base64EncodedKeyBlob);
      }

      public override string GetKeyServerURL() => this.keyServerURLOverride;

      public override string GetKeyServerAuthToken() => this.keyServerAuthToken;

      public override string GetDecryptionKey() => this.base64EncodedKeyBlob;
    }

    [Serializable]
    public class OptionsMacOSX : MediaPlayer.OptionsApple
    {
    }

    [Serializable]
    public class OptionsIOS : MediaPlayer.OptionsApple
    {
      public bool useYpCbCr420Textures = true;

      public override bool IsModified() => base.IsModified() || !this.useYpCbCr420Textures;
    }

    [Serializable]
    public class OptionsTVOS : MediaPlayer.OptionsIOS
    {
    }

    [Serializable]
    public class OptionsAndroid : MediaPlayer.PlatformOptions
    {
      public Android.VideoApi videoApi = Android.VideoApi.ExoPlayer;
      public bool useFastOesPath;
      public bool showPosterFrame;
      public bool enableAudio360;
      public Audio360ChannelMode audio360ChannelMode;
      public bool preferSoftwareDecoder;
      [Multiline]
      public string httpHeaderJson;
      [SerializeField]
      [Tooltip("Byte offset into the file where the media file is located.  This is useful when hiding or packing media files within another file.")]
      public int fileOffset;

      public override bool IsModified()
      {
        return base.IsModified() || this.fileOffset != 0 || this.useFastOesPath || this.showPosterFrame || this.videoApi != Android.VideoApi.ExoPlayer || !string.IsNullOrEmpty(this.httpHeaderJson) || this.enableAudio360 || this.audio360ChannelMode != Audio360ChannelMode.TBE_8_2 || this.preferSoftwareDecoder;
      }
    }

    [Serializable]
    public class OptionsWindowsPhone : MediaPlayer.PlatformOptions
    {
      public bool useHardwareDecoding = true;
      public bool useUnityAudio;
      public bool forceAudioResample = true;
      public bool useTextureMips;
      public bool useLowLatency;

      public override bool IsModified()
      {
        return base.IsModified() || !this.useHardwareDecoding || this.useTextureMips || this.useLowLatency || this.useUnityAudio || !this.forceAudioResample;
      }
    }

    [Serializable]
    public class OptionsWindowsUWP : MediaPlayer.PlatformOptions
    {
      public bool useHardwareDecoding = true;
      public bool useUnityAudio;
      public bool forceAudioResample = true;
      public bool useTextureMips;
      public bool useLowLatency;

      public override bool IsModified()
      {
        return base.IsModified() || !this.useHardwareDecoding || this.useTextureMips || this.useLowLatency || this.useUnityAudio || !this.forceAudioResample;
      }
    }

    [Serializable]
    public class OptionsWebGL : MediaPlayer.PlatformOptions
    {
      public WebGL.ExternalLibrary externalLibrary;
      public bool useTextureMips;

      public override bool IsModified()
      {
        return base.IsModified() || this.externalLibrary != WebGL.ExternalLibrary.None || this.useTextureMips;
      }
    }

    [Serializable]
    public class OptionsPS4 : MediaPlayer.PlatformOptions
    {
    }

    public delegate void ProcessExtractedFrame(Texture2D extractedFrame);
  }
}
