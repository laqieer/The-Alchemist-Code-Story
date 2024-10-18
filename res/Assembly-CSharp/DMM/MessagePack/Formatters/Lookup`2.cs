// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Lookup`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MessagePack.Formatters
{
  internal class Lookup<TKey, TElement> : 
    ILookup<TKey, TElement>,
    IEnumerable,
    IEnumerable<IGrouping<TKey, TElement>>
  {
    private readonly Dictionary<TKey, IGrouping<TKey, TElement>> groupings;

    public Lookup(
      Dictionary<TKey, IGrouping<TKey, TElement>> groupings)
    {
      this.groupings = groupings;
    }

    public IEnumerable<TElement> this[TKey key] => (IEnumerable<TElement>) this.groupings[key];

    public int Count => this.groupings.Count;

    public bool Contains(TKey key) => this.groupings.ContainsKey(key);

    public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
    {
      return (IEnumerator<IGrouping<TKey, TElement>>) this.groupings.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.groupings.Values.GetEnumerator();
  }
}
