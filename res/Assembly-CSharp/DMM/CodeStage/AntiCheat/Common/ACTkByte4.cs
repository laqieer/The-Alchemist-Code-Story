// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Common.ACTkByte4
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace CodeStage.AntiCheat.Common
{
  [Serializable]
  public struct ACTkByte4
  {
    public byte b1;
    public byte b2;
    public byte b3;
    public byte b4;

    public void Shuffle()
    {
      byte b2 = this.b2;
      this.b2 = this.b3;
      this.b3 = b2;
    }

    public void UnShuffle()
    {
      byte b3 = this.b3;
      this.b3 = this.b2;
      this.b2 = b3;
    }
  }
}
