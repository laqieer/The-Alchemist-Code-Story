// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.Stream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public abstract class Stream
  {
    public abstract int Width { get; }

    public abstract int Height { get; }

    public abstract int Bandwidth { get; }

    public abstract string URL { get; }

    public abstract List<Stream.Chunk> GetAllChunks();

    public abstract List<Stream.Chunk> GetChunks();

    public abstract List<Stream> GetAllStreams();

    public abstract List<Stream> GetStreams();

    public struct Chunk
    {
      public string name;
    }
  }
}
