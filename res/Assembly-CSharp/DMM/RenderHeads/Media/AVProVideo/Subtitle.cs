// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.Subtitle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public class Subtitle
  {
    public int index;
    public string text;
    public int timeStartMs;
    public int timeEndMs;

    public bool IsBefore(float time)
    {
      return (double) time > (double) this.timeStartMs && (double) time > (double) this.timeEndMs;
    }

    public bool IsTime(float time)
    {
      return (double) time >= (double) this.timeStartMs && (double) time < (double) this.timeEndMs;
    }
  }
}
