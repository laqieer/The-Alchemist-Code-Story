// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.FastJSON.List
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Gsc.DOM.FastJSON
{
  public class List : 
    IList,
    IList<object>,
    IEnumerable,
    ICollection,
    ICollection<object>,
    IEnumerable<object>
  {
    private readonly IArray value;
    private object[] _elements;

    public List(IArray value) => this.value = value;

    public int Count => this.value.Length;

    private object[] GetElements(bool create)
    {
      if (this._elements == null && create)
      {
        this._elements = new object[this.value.Length];
        for (int index = 0; index < this.value.Length; ++index)
          this._elements[index] = Json.Deserialize(this.value[index]);
      }
      return this._elements;
    }

    public bool Contains(object value) => this.IndexOf(value) >= 0;

    public int IndexOf(object value) => Array.IndexOf<object>(this.GetElements(true), value);

    public object this[int index]
    {
      get
      {
        object[] elements = this.GetElements(false);
        return elements != null ? elements[index] : Json.Deserialize(this.value[index]);
      }
      set => throw new NotSupportedException();
    }

    [DebuggerHidden]
    public IEnumerator<object> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<object>) new List.\u003CGetEnumerator\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public bool IsReadOnly => true;

    [Obsolete("not supported.", true)]
    public void CopyTo(object[] dst, int index)
    {
    }

    [Obsolete("not supported.", true)]
    void ICollection.CopyTo(Array array, int index)
    {
    }

    [Obsolete("not supported.", true)]
    public void Add(object value)
    {
    }

    [Obsolete("not supported.", true)]
    public void Clear()
    {
    }

    [Obsolete("not supported.", true)]
    public void Insert(int index, object value)
    {
    }

    [Obsolete("not supported.", true)]
    public bool Remove(object value) => false;

    [Obsolete("not supported.", true)]
    public void RemoveAt(int index)
    {
    }

    [Obsolete("not supported.", true)]
    int IList.Add(object value) => 0;

    [Obsolete("not supported.", true)]
    void IList.Remove(object value)
    {
    }

    bool IList.IsFixedSize => true;

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => (object) this;
  }
}
