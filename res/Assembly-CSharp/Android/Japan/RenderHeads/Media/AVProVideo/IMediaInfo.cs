// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.IMediaInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace RenderHeads.Media.AVProVideo
{
  public interface IMediaInfo
  {
    float GetDurationMs();

    int GetVideoWidth();

    int GetVideoHeight();

    float GetVideoFrameRate();

    float GetVideoDisplayRate();

    bool HasVideo();

    bool HasAudio();

    int GetAudioTrackCount();

    string GetCurrentAudioTrackId();

    int GetCurrentAudioTrackBitrate();

    int GetVideoTrackCount();

    string GetCurrentVideoTrackId();

    int GetCurrentVideoTrackBitrate();

    string GetPlayerDescription();

    bool PlayerSupportsLinearColorSpace();

    bool IsPlaybackStalled();

    float[] GetTextureTransform();
  }
}
