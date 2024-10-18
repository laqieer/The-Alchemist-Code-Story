// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.IMediaInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public interface IMediaInfo
  {
    float GetDurationMs();

    int GetVideoWidth();

    int GetVideoHeight();

    Rect GetCropRect();

    float GetVideoFrameRate();

    float GetVideoDisplayRate();

    bool HasVideo();

    bool HasAudio();

    int GetAudioTrackCount();

    string GetAudioTrackId(int index);

    string GetCurrentAudioTrackId();

    int GetCurrentAudioTrackBitrate();

    int GetVideoTrackCount();

    string GetVideoTrackId(int index);

    string GetCurrentVideoTrackId();

    int GetCurrentVideoTrackBitrate();

    string GetPlayerDescription();

    bool PlayerSupportsLinearColorSpace();

    bool IsPlaybackStalled();

    float[] GetTextureTransform();

    long GetEstimatedTotalBandwidthUsed();
  }
}
