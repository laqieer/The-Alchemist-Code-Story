// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.PlaylistMediaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Playlist Media Player (BETA)", -100)]
  public class PlaylistMediaPlayer : MediaPlayer, IMediaProducer
  {
    [SerializeField]
    private MediaPlayer _playerA;
    [SerializeField]
    private MediaPlayer _playerB;
    [SerializeField]
    private bool _playlistAutoProgress = true;
    [SerializeField]
    private PlaylistMediaPlayer.PlaylistLoopMode _playlistLoopMode;
    [SerializeField]
    private MediaPlaylist _playlist = new MediaPlaylist();
    [SerializeField]
    [Tooltip("Pause the previously playing video. This is useful for systems that will struggle to play 2 videos at once")]
    private bool _pausePreviousOnTransition = true;
    [SerializeField]
    private PlaylistMediaPlayer.Transition _nextTransition;
    [SerializeField]
    private float _transitionDuration = 1f;
    [SerializeField]
    private PlaylistMediaPlayer.Easing _transitionEasing;
    private static int _propFromTex;
    private static int _propT;
    private int _playlistIndex;
    private MediaPlayer _nextPlayer;
    private Shader _shader;
    private Material _material;
    private PlaylistMediaPlayer.Transition _currentTransition;
    private string _currentTransitionName = "LERP_NONE";
    private float _currentTransitionDuration = 1f;
    private PlaylistMediaPlayer.Easing.Preset _currentTransitionEasing;
    private float _textureTimer;
    private float _transitionTimer;
    private Func<float, float> _easeFunc;
    private RenderTexture _rt;
    private MediaPlaylist.MediaItem _currentItem;
    private MediaPlaylist.MediaItem _nextItem;

    public MediaPlayer CurrentPlayer
    {
      get
      {
        return Object.op_Equality((Object) this.NextPlayer, (Object) this._playerA) ? this._playerB : this._playerA;
      }
    }

    public MediaPlayer NextPlayer => this._nextPlayer;

    public MediaPlaylist Playlist => this._playlist;

    public int PlaylistIndex => this._playlistIndex;

    public MediaPlaylist.MediaItem PlaylistItem
    {
      get
      {
        return this._playlist.HasItemAt(this._playlistIndex) ? this._playlist.Items[this._playlistIndex] : (MediaPlaylist.MediaItem) null;
      }
    }

    public PlaylistMediaPlayer.PlaylistLoopMode LoopMode
    {
      get => this._playlistLoopMode;
      set => this._playlistLoopMode = value;
    }

    public bool AutoProgress
    {
      get => this._playlistAutoProgress;
      set => this._playlistAutoProgress = value;
    }

    public override IMediaInfo Info
    {
      get
      {
        return Object.op_Inequality((Object) this.CurrentPlayer, (Object) null) ? this.CurrentPlayer.Info : (IMediaInfo) null;
      }
    }

    public override IMediaControl Control
    {
      get
      {
        return Object.op_Inequality((Object) this.CurrentPlayer, (Object) null) ? this.CurrentPlayer.Control : (IMediaControl) null;
      }
    }

    public override IMediaProducer TextureProducer
    {
      get
      {
        if (!Object.op_Inequality((Object) this.CurrentPlayer, (Object) null))
          return (IMediaProducer) null;
        return this.IsTransitioning() ? (IMediaProducer) this : this.CurrentPlayer.TextureProducer;
      }
    }

    private void SwapPlayers()
    {
      if (this._pausePreviousOnTransition)
        this.CurrentPlayer.Pause();
      this.Events.Invoke((MediaPlayer) this, MediaPlayerEvent.EventType.PlaylistItemChanged, ErrorCode.None);
      if (this._currentTransition != PlaylistMediaPlayer.Transition.None)
      {
        Texture currentTexture = this.GetCurrentTexture();
        Texture nextTexture = this.GetNextTexture();
        if (Object.op_Inequality((Object) currentTexture, (Object) null) && Object.op_Inequality((Object) nextTexture, (Object) null))
        {
          int num1 = Mathf.Max(nextTexture.width, currentTexture.width);
          int num2 = Mathf.Max(nextTexture.height, currentTexture.height);
          if (Object.op_Inequality((Object) this._rt, (Object) null) && (((Texture) this._rt).width != num1 || ((Texture) this._rt).height != num2))
            RenderTexture.ReleaseTemporary(this._rt = (RenderTexture) null);
          if (Object.op_Equality((Object) this._rt, (Object) null))
          {
            this._rt = RenderTexture.GetTemporary(num1, num2, 0, (RenderTextureFormat) 7, (RenderTextureReadWrite) 0, 1);
            Graphics.Blit(currentTexture, this._rt);
          }
          this._material.SetTexture(PlaylistMediaPlayer._propFromTex, currentTexture);
          this._easeFunc = PlaylistMediaPlayer.Easing.GetFunction(this._currentTransitionEasing);
          this._transitionTimer = 0.0f;
        }
        else
          this._transitionTimer = this._currentTransitionDuration;
      }
      this._nextPlayer = !Object.op_Equality((Object) this.NextPlayer, (Object) this._playerA) ? this._playerA : this._playerB;
      this._currentItem = this._nextItem;
      this._nextItem = (MediaPlaylist.MediaItem) null;
    }

    private Texture GetCurrentTexture()
    {
      return Object.op_Inequality((Object) this.CurrentPlayer, (Object) null) && this.CurrentPlayer.TextureProducer != null ? this.CurrentPlayer.TextureProducer.GetTexture() : (Texture) null;
    }

    private Texture GetNextTexture()
    {
      return Object.op_Inequality((Object) this._nextPlayer, (Object) null) && this._nextPlayer.TextureProducer != null ? this._nextPlayer.TextureProducer.GetTexture() : (Texture) null;
    }

    private void Awake()
    {
      this._nextPlayer = this._playerA;
      this._shader = Shader.Find("AVProVideo/Helper/Transition");
      this._material = new Material(this._shader);
      PlaylistMediaPlayer._propFromTex = Shader.PropertyToID("_FromTex");
      PlaylistMediaPlayer._propT = Shader.PropertyToID("_Fade");
      this._easeFunc = PlaylistMediaPlayer.Easing.GetFunction(this._transitionEasing.preset);
    }

    protected override void OnDestroy()
    {
      if (Object.op_Inequality((Object) this._rt, (Object) null))
      {
        RenderTexture.ReleaseTemporary(this._rt);
        this._rt = (RenderTexture) null;
      }
      if (Object.op_Inequality((Object) this._material, (Object) null))
      {
        Object.Destroy((Object) this._material);
        this._material = (Material) null;
      }
      base.OnDestroy();
    }

    private void Start()
    {
      if (Object.op_Implicit((Object) this.CurrentPlayer))
      {
        // ISSUE: method pointer
        this.CurrentPlayer.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnVideoEvent)));
        if (Object.op_Implicit((Object) this.NextPlayer))
        {
          // ISSUE: method pointer
          this.NextPlayer.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnVideoEvent)));
        }
      }
      this.JumpToItem(0);
    }

    public void OnVideoEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
      if (Object.op_Equality((Object) mp, (Object) this.CurrentPlayer))
        this.Events.Invoke(mp, et, errorCode);
      switch (et)
      {
        case MediaPlayerEvent.EventType.FirstFrameReady:
          if (!Object.op_Equality((Object) mp, (Object) this.NextPlayer))
            break;
          this.SwapPlayers();
          this.Events.Invoke(mp, et, errorCode);
          break;
        case MediaPlayerEvent.EventType.FinishedPlaying:
          if (!this._playlistAutoProgress || !Object.op_Equality((Object) mp, (Object) this.CurrentPlayer) || this._currentItem.progressMode != PlaylistMediaPlayer.ProgressMode.OnFinish)
            break;
          this.NextItem();
          break;
      }
    }

    public bool PrevItem() => this.JumpToItem(this._playlistIndex - 1);

    public bool NextItem()
    {
      bool flag = this.JumpToItem(this._playlistIndex + 1);
      if (!flag)
        this.Events.Invoke((MediaPlayer) this, MediaPlayerEvent.EventType.PlaylistFinished, ErrorCode.None);
      return flag;
    }

    public bool CanJumpToItem(int index)
    {
      if (this._playlistLoopMode == PlaylistMediaPlayer.PlaylistLoopMode.Loop && this._playlist.Items.Count > 0)
      {
        index %= this._playlist.Items.Count;
        if (index < 0)
          index += this._playlist.Items.Count;
      }
      return this._playlist.HasItemAt(index);
    }

    public bool JumpToItem(int index)
    {
      if (this._playlistLoopMode == PlaylistMediaPlayer.PlaylistLoopMode.Loop && this._playlist.Items.Count > 0)
      {
        index %= this._playlist.Items.Count;
        if (index < 0)
          index += this._playlist.Items.Count;
      }
      if (!this._playlist.HasItemAt(index))
        return false;
      this._playlistIndex = index;
      this._nextItem = this._playlist.Items[this._playlistIndex];
      this.OpenVideoFile(this._nextItem);
      return true;
    }

    public void OpenVideoFile(MediaPlaylist.MediaItem mediaItem)
    {
      bool flag = false;
      if (this.NextPlayer.m_VideoPath == mediaItem.filePath && this.NextPlayer.m_VideoLocation == mediaItem.fileLocation)
        flag = true;
      if (mediaItem.isOverrideTransition)
        this.SetTransition(mediaItem.overrideTransition, mediaItem.overrideTransitionDuration, mediaItem.overrideTransitionEasing.preset);
      else
        this.SetTransition(this._nextTransition, this._transitionDuration, this._transitionEasing.preset);
      this.m_Loop = this.NextPlayer.m_Loop = mediaItem.loop;
      this.m_AutoStart = this.NextPlayer.m_AutoStart = mediaItem.autoPlay;
      this.m_VideoLocation = this.NextPlayer.m_VideoLocation = mediaItem.fileLocation;
      this.m_VideoPath = this.NextPlayer.m_VideoPath = mediaItem.filePath;
      this.m_StereoPacking = this.NextPlayer.m_StereoPacking = mediaItem.stereoPacking;
      this.m_AlphaPacking = this.NextPlayer.m_AlphaPacking = mediaItem.alphaPacking;
      if (flag)
      {
        this.NextPlayer.Rewind(false);
        if (this._nextItem.startMode == PlaylistMediaPlayer.StartMode.Immediate)
          this.NextPlayer.Play();
        this.SwapPlayers();
      }
      else if (string.IsNullOrEmpty(this.NextPlayer.m_VideoPath))
        this.NextPlayer.CloseVideo();
      else
        this.NextPlayer.OpenVideoFromFile(this.NextPlayer.m_VideoLocation, this.NextPlayer.m_VideoPath, this._nextItem.startMode == PlaylistMediaPlayer.StartMode.Immediate);
    }

    private bool IsTransitioning()
    {
      return Object.op_Inequality((Object) this._rt, (Object) null) && (double) this._transitionTimer < (double) this._currentTransitionDuration && this._currentTransition != PlaylistMediaPlayer.Transition.None;
    }

    private void SetTransition(
      PlaylistMediaPlayer.Transition transition,
      float duration,
      PlaylistMediaPlayer.Easing.Preset easing)
    {
      if (transition == PlaylistMediaPlayer.Transition.Random)
        transition = (PlaylistMediaPlayer.Transition) Random.Range(0, 21);
      if (transition != this._currentTransition)
      {
        if (!string.IsNullOrEmpty(this._currentTransitionName))
          this._material.DisableKeyword(this._currentTransitionName);
        this._currentTransition = transition;
        this._currentTransitionName = PlaylistMediaPlayer.GetTransitionName(transition);
        this._material.EnableKeyword(this._currentTransitionName);
      }
      this._currentTransitionDuration = duration;
      this._currentTransitionEasing = easing;
    }

    protected override void Update()
    {
      if (this.IsTransitioning())
      {
        this._transitionTimer += Time.deltaTime;
        float volume = this._easeFunc(Mathf.Clamp01(this._transitionTimer / this._currentTransitionDuration));
        this.NextPlayer.Control.SetVolume(1f - volume);
        this.CurrentPlayer.Control.SetVolume(volume);
        this._material.SetFloat(PlaylistMediaPlayer._propT, volume);
        this._rt.DiscardContents();
        Graphics.Blit(this.GetCurrentTexture(), this._rt, this._material);
        if (!this._pausePreviousOnTransition && !this.IsTransitioning() && Object.op_Inequality((Object) this.NextPlayer, (Object) null) && this.NextPlayer.Control.IsPlaying())
          this.NextPlayer.Pause();
      }
      else if (this._playlistAutoProgress && this._nextItem == null && this._currentItem != null && this._currentItem.progressMode == PlaylistMediaPlayer.ProgressMode.BeforeFinish && this.Control != null && (double) this.Control.GetCurrentTimeMs() >= (double) this.Info.GetDurationMs() - (double) this._currentItem.progressTimeSeconds * 1000.0)
        this.NextItem();
      base.Update();
    }

    public Texture GetTexture(int index = 0) => (Texture) this._rt;

    public int GetTextureCount() => this.CurrentPlayer.TextureProducer.GetTextureCount();

    public int GetTextureFrameCount() => this.CurrentPlayer.TextureProducer.GetTextureFrameCount();

    public bool SupportsTextureFrameCount()
    {
      return this.CurrentPlayer.TextureProducer.SupportsTextureFrameCount();
    }

    public long GetTextureTimeStamp() => this.CurrentPlayer.TextureProducer.GetTextureTimeStamp();

    public bool RequiresVerticalFlip() => this.CurrentPlayer.TextureProducer.RequiresVerticalFlip();

    public Matrix4x4 GetYpCbCrTransform()
    {
      return this.CurrentPlayer.TextureProducer.GetYpCbCrTransform();
    }

    private static string GetTransitionName(PlaylistMediaPlayer.Transition transition)
    {
      switch (transition)
      {
        case PlaylistMediaPlayer.Transition.None:
          return "LERP_NONE";
        case PlaylistMediaPlayer.Transition.Fade:
          return "LERP_FADE";
        case PlaylistMediaPlayer.Transition.Black:
          return "LERP_BLACK";
        case PlaylistMediaPlayer.Transition.White:
          return "LERP_WHITE";
        case PlaylistMediaPlayer.Transition.Transparent:
          return "LERP_TRANSP";
        case PlaylistMediaPlayer.Transition.Horiz:
          return "LERP_HORIZ";
        case PlaylistMediaPlayer.Transition.Vert:
          return "LERP_VERT";
        case PlaylistMediaPlayer.Transition.Diag:
          return "LERP_DIAG";
        case PlaylistMediaPlayer.Transition.MirrorH:
          return "LERP_HORIZ_MIRROR";
        case PlaylistMediaPlayer.Transition.MirrorV:
          return "LERP_VERT_MIRROR";
        case PlaylistMediaPlayer.Transition.MirrorD:
          return "LERP_DIAG_MIRROR";
        case PlaylistMediaPlayer.Transition.ScrollV:
          return "LERP_SCROLL_VERT";
        case PlaylistMediaPlayer.Transition.ScrollH:
          return "LERP_SCROLL_HORIZ";
        case PlaylistMediaPlayer.Transition.Circle:
          return "LERP_CIRCLE";
        case PlaylistMediaPlayer.Transition.Diamond:
          return "LERP_DIAMOND";
        case PlaylistMediaPlayer.Transition.Blinds:
          return "LERP_BLINDS";
        case PlaylistMediaPlayer.Transition.Arrows:
          return "LERP_ARROW";
        case PlaylistMediaPlayer.Transition.SlideH:
          return "LERP_SLIDE_HORIZ";
        case PlaylistMediaPlayer.Transition.SlideV:
          return "LERP_SLIDE_VERT";
        case PlaylistMediaPlayer.Transition.Zoom:
          return "LERP_ZOOM_FADE";
        case PlaylistMediaPlayer.Transition.RectV:
          return "LERP_RECTS_VERT";
        default:
          return string.Empty;
      }
    }

    public enum Transition
    {
      None,
      Fade,
      Black,
      White,
      Transparent,
      Horiz,
      Vert,
      Diag,
      MirrorH,
      MirrorV,
      MirrorD,
      ScrollV,
      ScrollH,
      Circle,
      Diamond,
      Blinds,
      Arrows,
      SlideH,
      SlideV,
      Zoom,
      RectV,
      Random,
    }

    public enum PlaylistLoopMode
    {
      None,
      Loop,
    }

    public enum StartMode
    {
      Immediate,
      Manual,
    }

    public enum ProgressMode
    {
      OnFinish,
      BeforeFinish,
      Manual,
    }

    [Serializable]
    public class Easing
    {
      public PlaylistMediaPlayer.Easing.Preset preset = PlaylistMediaPlayer.Easing.Preset.Linear;

      public static Func<float, float> GetFunction(PlaylistMediaPlayer.Easing.Preset preset)
      {
        Func<float, float> function = (Func<float, float>) null;
        switch (preset)
        {
          case PlaylistMediaPlayer.Easing.Preset.Step:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache0 = new Func<float, float>(PlaylistMediaPlayer.Easing.Step);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache0;
            break;
          case PlaylistMediaPlayer.Easing.Preset.Linear:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache1 = new Func<float, float>(PlaylistMediaPlayer.Easing.Linear);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache1;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InQuad:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache2 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache2 = new Func<float, float>(PlaylistMediaPlayer.Easing.InQuad);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache2;
            break;
          case PlaylistMediaPlayer.Easing.Preset.OutQuad:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache3 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache3 = new Func<float, float>(PlaylistMediaPlayer.Easing.OutQuad);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache3;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InOutQuad:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache4 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache4 = new Func<float, float>(PlaylistMediaPlayer.Easing.InOutQuad);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache4;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InCubic:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache5 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache5 = new Func<float, float>(PlaylistMediaPlayer.Easing.InCubic);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache5;
            break;
          case PlaylistMediaPlayer.Easing.Preset.OutCubic:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache6 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache6 = new Func<float, float>(PlaylistMediaPlayer.Easing.OutCubic);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache6;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InOutCubic:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache7 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache7 = new Func<float, float>(PlaylistMediaPlayer.Easing.InOutCubic);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache7;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InQuint:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache8 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache8 = new Func<float, float>(PlaylistMediaPlayer.Easing.InQuint);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache8;
            break;
          case PlaylistMediaPlayer.Easing.Preset.OutQuint:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache9 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache9 = new Func<float, float>(PlaylistMediaPlayer.Easing.OutQuint);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache9;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InOutQuint:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheA == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheA = new Func<float, float>(PlaylistMediaPlayer.Easing.InOutQuint);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheA;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InQuart:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheB == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheB = new Func<float, float>(PlaylistMediaPlayer.Easing.InQuart);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheB;
            break;
          case PlaylistMediaPlayer.Easing.Preset.OutQuart:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheC == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheC = new Func<float, float>(PlaylistMediaPlayer.Easing.OutQuart);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheC;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InOutQuart:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheD == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheD = new Func<float, float>(PlaylistMediaPlayer.Easing.InOutQuart);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheD;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InExpo:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheE == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheE = new Func<float, float>(PlaylistMediaPlayer.Easing.InExpo);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheE;
            break;
          case PlaylistMediaPlayer.Easing.Preset.OutExpo:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheF == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheF = new Func<float, float>(PlaylistMediaPlayer.Easing.OutExpo);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cacheF;
            break;
          case PlaylistMediaPlayer.Easing.Preset.InOutExpo:
            // ISSUE: reference to a compiler-generated field
            if (PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache10 == null)
            {
              // ISSUE: reference to a compiler-generated field
              PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache10 = new Func<float, float>(PlaylistMediaPlayer.Easing.InOutExpo);
            }
            // ISSUE: reference to a compiler-generated field
            function = PlaylistMediaPlayer.Easing.\u003C\u003Ef__mg\u0024cache10;
            break;
          case PlaylistMediaPlayer.Easing.Preset.Random:
            function = PlaylistMediaPlayer.Easing.GetFunction((PlaylistMediaPlayer.Easing.Preset) Random.Range(0, 17));
            break;
          case PlaylistMediaPlayer.Easing.Preset.RandomNotStep:
            function = PlaylistMediaPlayer.Easing.GetFunction((PlaylistMediaPlayer.Easing.Preset) Random.Range(1, 17));
            break;
        }
        return function;
      }

      public static float PowerEaseIn(float t, float power) => Mathf.Pow(t, power);

      public static float PowerEaseOut(float t, float power)
      {
        return 1f - Mathf.Abs(Mathf.Pow(t - 1f, power));
      }

      public static float PowerEaseInOut(float t, float power)
      {
        return (double) t >= 0.5 ? (float) ((double) PlaylistMediaPlayer.Easing.PowerEaseOut((float) ((double) t * 2.0 - 1.0), power) / 2.0 + 0.5) : PlaylistMediaPlayer.Easing.PowerEaseIn(t * 2f, power) / 2f;
      }

      public static float Step(float t)
      {
        float num = 0.0f;
        if ((double) t >= 0.5)
          num = 1f;
        return num;
      }

      public static float Linear(float t) => t;

      public static float InQuad(float t) => PlaylistMediaPlayer.Easing.PowerEaseIn(t, 2f);

      public static float OutQuad(float t) => PlaylistMediaPlayer.Easing.PowerEaseOut(t, 2f);

      public static float InOutQuad(float t) => PlaylistMediaPlayer.Easing.PowerEaseInOut(t, 2f);

      public static float InCubic(float t) => PlaylistMediaPlayer.Easing.PowerEaseIn(t, 3f);

      public static float OutCubic(float t) => PlaylistMediaPlayer.Easing.PowerEaseOut(t, 3f);

      public static float InOutCubic(float t) => PlaylistMediaPlayer.Easing.PowerEaseInOut(t, 3f);

      public static float InQuart(float t) => PlaylistMediaPlayer.Easing.PowerEaseIn(t, 4f);

      public static float OutQuart(float t) => PlaylistMediaPlayer.Easing.PowerEaseOut(t, 4f);

      public static float InOutQuart(float t) => PlaylistMediaPlayer.Easing.PowerEaseInOut(t, 4f);

      public static float InQuint(float t) => PlaylistMediaPlayer.Easing.PowerEaseIn(t, 5f);

      public static float OutQuint(float t) => PlaylistMediaPlayer.Easing.PowerEaseOut(t, 5f);

      public static float InOutQuint(float t) => PlaylistMediaPlayer.Easing.PowerEaseInOut(t, 5f);

      public static float InExpo(float t)
      {
        float num = 0.0f;
        if ((double) t != 0.0)
          num = Mathf.Pow(2f, (float) (10.0 * ((double) t - 1.0)));
        return num;
      }

      public static float OutExpo(float t)
      {
        float num = 1f;
        if ((double) t != 1.0)
          num = (float) (-(double) Mathf.Pow(2f, -10f * t) + 1.0);
        return num;
      }

      public static float InOutExpo(float t)
      {
        float num = 0.0f;
        if ((double) t > 0.0)
        {
          num = 1f;
          if ((double) t < 1.0)
          {
            t *= 2f;
            if ((double) t < 1.0)
            {
              num = 0.5f * Mathf.Pow(2f, (float) (10.0 * ((double) t - 1.0)));
            }
            else
            {
              --t;
              num = (float) (0.5 * (-(double) Mathf.Pow(2f, -10f * t) + 2.0));
            }
          }
        }
        return num;
      }

      public enum Preset
      {
        Step,
        Linear,
        InQuad,
        OutQuad,
        InOutQuad,
        InCubic,
        OutCubic,
        InOutCubic,
        InQuint,
        OutQuint,
        InOutQuint,
        InQuart,
        OutQuart,
        InOutQuart,
        InExpo,
        OutExpo,
        InOutExpo,
        Random,
        RandomNotStep,
      }
    }
  }
}
