﻿// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.IMediaSubtitles
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public interface IMediaSubtitles
  {
    bool LoadSubtitlesSRT(string data);

    int GetSubtitleIndex();

    string GetSubtitleText();
  }
}
