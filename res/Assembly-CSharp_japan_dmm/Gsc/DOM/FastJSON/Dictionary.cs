// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.FastJSON.Dictionary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Gsc.DOM.FastJSON
{
  public class Dictionary : 
    IDictionary,
    IDictionary<string, object>,
    IEnumerable,
    ICollection,
    ICollection<KeyValuePair<string, object>>,
    IEnumerable<KeyValuePair<string, object>>
  {
    private readonly Dictionary<string, object> values;

    public Dictionary(IObject value)
    {
      this.values = new Dictionary<string, object>(value.MemberCount);
      foreach (IMember member in (IEnumerable<IMember>) value)
        this.values.Add(member.Name, Json.Deserialize(member.Value));
    }

    public int Count => this.values.Count;

    public ICollection<string> Keys => (ICollection<string>) this.values.Keys;

    ICollection IDictionary.Keys => (ICollection) this.values.Keys;

    public ICollection<object> Values => (ICollection<object>) this.values.Values;

    ICollection IDictionary.Values => (ICollection) this.values.Values;

    public object this[string key]
    {
      get => this.values[key];
      set => throw new NotSupportedException();
    }

    object IDictionary.this[object key]
    {
      get => this.values[(string) key];
      set => throw new NotSupportedException();
    }

    public bool ContainsKey(string key) => this.values.ContainsKey(key);

    bool IDictionary.Contains(object key) => this.ContainsKey((string) key);

    public bool TryGetValue(string key, out object value)
    {
      return this.values.TryGetValue(key, out value);
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<string, object>>) this.values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) this.values.GetEnumerator();
    }

    public bool IsReadOnly => true;

    bool IDictionary.IsFixedSize => true;

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => (object) this;

    [Obsolete("not supported.", true)]
    public bool Contains(KeyValuePair<string, object> member) => false;

    [Obsolete("not supported.", true)]
    public void CopyTo(KeyValuePair<string, object>[] dst, int index)
    {
    }

    [Obsolete("not supported.", true)]
    void ICollection.CopyTo(Array array, int index)
    {
    }

    [Obsolete("not supported.", true)]
    public void Add(string key, object value)
    {
    }

    [Obsolete("not supported.", true)]
    public void Add(KeyValuePair<string, object> member)
    {
    }

    [Obsolete("not supported.", true)]
    public void Clear()
    {
    }

    [Obsolete("not supported.", true)]
    public bool Remove(string key) => false;

    [Obsolete("not supported.", true)]
    public bool Remove(KeyValuePair<string, object> member) => false;

    [Obsolete("not supported.", true)]
    void IDictionary.Add(object key, object value)
    {
    }

    [Obsolete("not supported.", true)]
    void IDictionary.Remove(object key)
    {
    }
  }
}
