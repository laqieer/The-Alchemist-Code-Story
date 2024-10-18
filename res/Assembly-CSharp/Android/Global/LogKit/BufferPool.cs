// Decompiled with JetBrains decompiler
// Type: LogKit.BufferPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace LogKit
{
  public class BufferPool
  {
    private readonly int mBufferSize;
    private readonly List<Buffer> mBuffers;

    public BufferPool(int poolSize, int bufferSize)
    {
      this.mBufferSize = bufferSize;
      this.mBuffers = new List<Buffer>(poolSize);
      for (int index = 0; index < poolSize; ++index)
        this.mBuffers.Add(new Buffer(bufferSize));
    }

    public Buffer Get()
    {
      for (int index = 0; index < this.mBuffers.Count; ++index)
      {
        Buffer mBuffer = this.mBuffers[index];
        if (!mBuffer.IsAcquired && mBuffer.Count < this.mBufferSize)
        {
          mBuffer.Acquire();
          return mBuffer;
        }
      }
      return (Buffer) null;
    }
  }
}
