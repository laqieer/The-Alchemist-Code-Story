// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Array
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gsc.DOM.Json
{
  public struct Array : IArray, IEnumerable<Value>, IEnumerable<IValue>, IEnumerable
  {
    private readonly rapidjson.Array value;

    public Array(rapidjson.Array value)
    {
      this.value = value;
    }

    public int Length
    {
      get
      {
        return this.value.Length;
      }
    }

    [DebuggerHidden]
    public IEnumerator<Value> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<Value>) new Array.\u003CGetEnumerator\u003Ec__Iterator1() { \u0024this = this };
    }

    [DebuggerHidden]
    IEnumerator<IValue> IEnumerable<IValue>.GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<IValue>) new Array.\u003CSystem_Collections_Generic_IEnumerable\u003CGsc_DOM_IValue\u003E_GetEnumerator\u003Ec__Iterator0() { \u0024this = this };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public Value this[int index]
    {
      get
      {
        return new Value(this.value[index]);
      }
    }

    IValue IArray.this[int index]
    {
      get
      {
        return (IValue) this[index];
      }
    }
  }
}
