// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.Subtitle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      if ((double) time > (double) this.timeStartMs)
        return (double) time > (double) this.timeEndMs;
      return false;
    }

    public bool IsTime(float time)
    {
      if ((double) time >= (double) this.timeStartMs)
        return (double) time < (double) this.timeEndMs;
      return false;
    }
  }
}
