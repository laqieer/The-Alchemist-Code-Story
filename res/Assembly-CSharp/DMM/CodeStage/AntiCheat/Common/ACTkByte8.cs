// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Common.ACTkByte8
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace CodeStage.AntiCheat.Common
{
  [Serializable]
  public struct ACTkByte8
  {
    public byte b1;
    public byte b2;
    public byte b3;
    public byte b4;
    public byte b5;
    public byte b6;
    public byte b7;
    public byte b8;

    public void Shuffle()
    {
      byte b1 = this.b1;
      this.b1 = this.b2;
      this.b2 = b1;
      byte b5 = this.b5;
      this.b5 = this.b6;
      byte b8 = this.b8;
      this.b8 = b5;
      this.b6 = b8;
    }

    public void UnShuffle()
    {
      byte b1 = this.b1;
      this.b1 = this.b2;
      this.b2 = b1;
      byte b5 = this.b5;
      this.b5 = this.b8;
      byte b6 = this.b6;
      this.b6 = b5;
      this.b8 = b6;
    }
  }
}
