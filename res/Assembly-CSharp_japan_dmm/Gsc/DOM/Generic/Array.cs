// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Array
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Gsc.DOM.Generic
{
  public class Array : IArray, IEnumerable<Value>, IEnumerable<IValue>, IEnumerable
  {
    private readonly List<Value> values;

    public Array() => this.values = new List<Value>();

    public int Length => this.values.Count;

    [DebuggerHidden]
    public IEnumerator<Value> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<Value>) new Array.\u003CGetEnumerator\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    IEnumerator<IValue> IEnumerable<IValue>.GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<IValue>) new Array.\u003CSystem_Collections_Generic_IEnumerable\u003CGsc_DOM_IValue\u003E_GetEnumerator\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public virtual void Add(Value value) => this.values.Add(value);

    public Value this[int index]
    {
      get => this.values[index];
      set => this.values[index] = value;
    }

    IValue IArray.this[int index] => (IValue) this[index];
  }
}
