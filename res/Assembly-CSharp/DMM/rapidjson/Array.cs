// Decompiled with JetBrains decompiler
// Type: rapidjson.Array
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace rapidjson
{
  public class Array : IEnumerable<Value>, IEnumerable
  {
    private readonly Document doc;
    private readonly ulong begin;
    private readonly uint elementSize;
    private readonly uint size;

    public Array(Document doc, ref IntPtr ptr)
    {
      doc.CheckDisposed();
      IntPtr elementsPointer;
      if (!DLL._rapidjson_get_array_iterator(ptr, out elementsPointer, out this.size, out this.elementSize))
        throw new InvalidOperationException("Not Array Type.");
      this.doc = doc;
      this.begin = (ulong) (long) elementsPointer;
    }

    public int Length => (int) this.size;

    [DebuggerHidden]
    public IEnumerator<Value> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<Value>) new Array.\u003CGetEnumerator\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public Value this[int index]
    {
      get
      {
        if (index < 0 || (uint) index >= this.size)
          throw new IndexOutOfRangeException();
        IntPtr ptr = (IntPtr) ((long) this.begin + (long) (this.elementSize * (uint) index));
        return new Value(this.doc, ref ptr);
      }
    }
  }
}
