// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.AudioOutput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

namespace RenderHeads.Media.AVProVideo
{
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [RequireComponent(typeof (AudioSource))]
  [AddComponentMenu("AVPro Video/Audio Output", 400)]
  public class AudioOutput : MonoBehaviour
  {
    public AudioOutput.AudioOutputMode _audioOutputMode = AudioOutput.AudioOutputMode.Multiple;
    [HideInInspector]
    public int _channelMask = -1;
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    private AudioSource _audioSource;

    private void Awake()
    {
      this._audioSource = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
      this.ChangeMediaPlayer(this._mediaPlayer);
    }

    private void OnDestroy()
    {
      this.ChangeMediaPlayer((MediaPlayer) null);
    }

    private void Update()
    {
      if (!((Object) this._mediaPlayer != (Object) null) || this._mediaPlayer.Control == null || !this._mediaPlayer.Control.IsPlaying())
        return;
      AudioOutput.ApplyAudioSettings(this._mediaPlayer, this._audioSource);
    }

    public void ChangeMediaPlayer(MediaPlayer newPlayer)
    {
      if ((Object) this._mediaPlayer != (Object) null)
      {
        this._mediaPlayer.Events.RemoveListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>(this.OnMediaPlayerEvent));
        this._mediaPlayer = (MediaPlayer) null;
      }
      this._mediaPlayer = newPlayer;
      if (!((Object) this._mediaPlayer != (Object) null))
        return;
      this._mediaPlayer.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>(this.OnMediaPlayerEvent));
    }

    private void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
      switch (et)
      {
        case MediaPlayerEvent.EventType.Started:
          AudioOutput.ApplyAudioSettings(this._mediaPlayer, this._audioSource);
          this._audioSource.Play();
          break;
        case MediaPlayerEvent.EventType.Closing:
          this._audioSource.Stop();
          break;
      }
    }

    private static void ApplyAudioSettings(MediaPlayer player, AudioSource audioSource)
    {
      if (!((Object) player != (Object) null) || player.Control == null)
        return;
      float volume = player.Control.GetVolume();
      bool flag = player.Control.IsMuted();
      float playbackRate = player.Control.GetPlaybackRate();
      audioSource.volume = volume;
      audioSource.mute = flag;
      audioSource.pitch = playbackRate;
    }

    public enum AudioOutputMode
    {
      Single,
      Multiple,
    }
  }
}
