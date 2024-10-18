// Decompiled with JetBrains decompiler
// Type: LogKit.Buffer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
