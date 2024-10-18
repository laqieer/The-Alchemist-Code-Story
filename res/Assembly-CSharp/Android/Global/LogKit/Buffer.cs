// Decompiled with JetBrains decompiler
// Type: LogKit.Buffer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

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

    public int AvailableSize
    {
      get
      {
        return this.mSize - this.Count;
      }
    }

    public void Acquire()
    {
      this.IsAcquired = true;
    }

    public void Release()
    {
      this.IsAcquired = false;
    }
  }
}
