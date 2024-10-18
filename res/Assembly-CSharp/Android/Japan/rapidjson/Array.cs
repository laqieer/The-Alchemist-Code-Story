// Decompiled with JetBrains decompiler
// Type: rapidjson.Array
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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

    public int Length
    {
      get
      {
        return (int) this.size;
      }
    }

    [DebuggerHidden]
    public IEnumerator<Value> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<Value>) new Array.\u003CGetEnumerator\u003Ec__Iterator0() { \u0024this = this };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

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
