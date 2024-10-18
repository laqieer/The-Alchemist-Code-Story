// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.ArrayPool`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Internal
{
  internal class ArrayPool<T>
  {
    private readonly int bufferLength;
    private readonly object gate;
    private int index;
    private T[][] buffers;

    public ArrayPool(int bufferLength)
    {
      this.bufferLength = bufferLength;
      this.buffers = new T[4][];
      this.gate = new object();
    }

    public T[] Rent()
    {
      lock (this.gate)
      {
        if (this.index >= this.buffers.Length)
          Array.Resize<T[]>(ref this.buffers, this.buffers.Length * 2);
        if (this.buffers[this.index] == null)
          this.buffers[this.index] = new T[this.bufferLength];
        T[] buffer = this.buffers[this.index];
        this.buffers[this.index] = (T[]) null;
        ++this.index;
        return buffer;
      }
    }

    public void Return(T[] array)
    {
      if (array.Length != this.bufferLength)
        throw new InvalidOperationException("return buffer is not from pool");
      lock (this.gate)
      {
        if (this.index == 0)
          return;
        this.buffers[--this.index] = array;
      }
    }
  }
}
