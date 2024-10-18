// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.IMediaProducer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public interface IMediaProducer
  {
    int GetTextureCount();

    Texture GetTexture(int index = 0);

    int GetTextureFrameCount();

    bool SupportsTextureFrameCount();

    long GetTextureTimeStamp();

    bool RequiresVerticalFlip();

    Matrix4x4 GetYpCbCrTransform();
  }
}
