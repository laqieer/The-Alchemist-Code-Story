﻿// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.IMediaSubtitles
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace RenderHeads.Media.AVProVideo
{
  public interface IMediaSubtitles
  {
    bool LoadSubtitlesSRT(string data);

    int GetSubtitleIndex();

    string GetSubtitleText();
  }
}