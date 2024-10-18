// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.SubtitlesUGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Subtitles uGUI", 201)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class SubtitlesUGUI : MonoBehaviour
  {
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    [SerializeField]
    private Text _text;

    private void Start()
    {
      this.ChangeMediaPlayer(this._mediaPlayer);
    }

    private void OnDestroy()
    {
      this.ChangeMediaPlayer((MediaPlayer) null);
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
      if (et != MediaPlayerEvent.EventType.SubtitleChange)
        return;
      this._text.text = this._mediaPlayer.Subtitles.GetSubtitleText().Replace("<font color=", "<color=").Replace("</font>", "</color>").Replace("<u>", string.Empty).Replace("</u>", string.Empty);
    }
  }
}
