// Decompiled with JetBrains decompiler
// Type: LogKit.Buffer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace LogKit
{
  public class Buffer : List<Log>
  {
    private readonly int mSize;

    public Buffer(int size)
      : base(size)
    {
      this.mSize = size;
    }

    public string mDeviceID { get; set; }

    public bool IsAcquired { get; private set; }

    public int AvailableSize => this.mSize - this.Count;

    public void Acquire() => this.IsAcquired = true;

    public void Release() => this.IsAcquired = false;
  }
}
