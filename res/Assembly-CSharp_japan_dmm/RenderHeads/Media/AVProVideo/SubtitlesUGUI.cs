// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.SubtitlesUGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
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

    private void Start() => this.ChangeMediaPlayer(this._mediaPlayer);

    private void OnDestroy() => this.ChangeMediaPlayer((MediaPlayer) null);

    public void ChangeMediaPlayer(MediaPlayer newPlayer)
    {
      if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
      {
        // ISSUE: method pointer
        this._mediaPlayer.Events.RemoveListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
        this._mediaPlayer = (MediaPlayer) null;
      }
      if (!Object.op_Inequality((Object) newPlayer, (Object) null))
        return;
      // ISSUE: method pointer
      newPlayer.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
      this._mediaPlayer = newPlayer;
    }

    private void OnMediaPlayerEvent(
      MediaPlayer mp,
      MediaPlayerEvent.EventType et,
      ErrorCode errorCode)
    {
      if (et != MediaPlayerEvent.EventType.SubtitleChange)
        return;
      this._text.text = this._mediaPlayer.Subtitles.GetSubtitleText().Replace("<font color=", "<color=").Replace("</font>", "</color>").Replace("<u>", string.Empty).Replace("</u>", string.Empty);
    }
  }
}
