// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.AudioOutput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [RequireComponent(typeof (AudioSource))]
  [AddComponentMenu("AVPro Video/Audio Output", 400)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class AudioOutput : MonoBehaviour
  {
    public AudioOutput.AudioOutputMode _audioOutputMode = AudioOutput.AudioOutputMode.Multiple;
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    private AudioSource _audioSource;
    [HideInInspector]
    public int _channelMask = -1;

    private void Awake() => this._audioSource = ((Component) this).GetComponent<AudioSource>();

    private void Start() => this.ChangeMediaPlayer(this._mediaPlayer);

    private void OnDestroy() => this.ChangeMediaPlayer((MediaPlayer) null);

    private void Update()
    {
      if (!Object.op_Inequality((Object) this._mediaPlayer, (Object) null) || this._mediaPlayer.Control == null || !this._mediaPlayer.Control.IsPlaying())
        return;
      AudioOutput.ApplyAudioSettings(this._mediaPlayer, this._audioSource);
    }

    public void ChangeMediaPlayer(MediaPlayer newPlayer)
    {
      if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
      {
        // ISSUE: method pointer
        this._mediaPlayer.Events.RemoveListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
        this._mediaPlayer = (MediaPlayer) null;
      }
      this._mediaPlayer = newPlayer;
      if (!Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
        return;
      // ISSUE: method pointer
      this._mediaPlayer.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
    }

    private void OnMediaPlayerEvent(
      MediaPlayer mp,
      MediaPlayerEvent.EventType et,
      ErrorCode errorCode)
    {
      if (et != MediaPlayerEvent.EventType.Closing)
      {
        if (et != MediaPlayerEvent.EventType.Started)
          return;
        AudioOutput.ApplyAudioSettings(this._mediaPlayer, this._audioSource);
        this._audioSource.Play();
      }
      else
        this._audioSource.Stop();
    }

    private static void ApplyAudioSettings(MediaPlayer player, AudioSource audioSource)
    {
      if (!Object.op_Inequality((Object) player, (Object) null) || player.Control == null)
        return;
      float volume = player.Control.GetVolume();
      bool flag = player.Control.IsMuted();
      float playbackRate = player.Control.GetPlaybackRate();
      audioSource.volume = volume;
      audioSource.mute = flag;
      audioSource.pitch = playbackRate;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
      AudioOutputManager.Instance.RequestAudio(this, this._mediaPlayer, data, this._channelMask, channels, this._audioOutputMode);
    }

    public enum AudioOutputMode
    {
      Single,
      Multiple,
    }
  }
}
