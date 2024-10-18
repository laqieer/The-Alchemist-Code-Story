// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.IMediaInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
