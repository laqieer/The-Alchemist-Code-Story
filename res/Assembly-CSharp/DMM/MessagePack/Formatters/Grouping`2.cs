// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Grouping`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MessagePack.Formatters
{
  internal class Grouping<TKey, TElement> : 
    IGrouping<TKey, TElement>,
    IEnumerable,
    IEnumerable<TElement>
  {
    private readonly TKey key;
    private readonly IEnumerable<TElement> elements;

    public Grouping(TKey key, IEnumerable<TElement> elements)
    {
      this.key = key;
      this.elements = elements;
    }

    public TKey Key => this.key;

    public IEnumerator<TElement> GetEnumerator() => this.elements.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.elements.GetEnumerator();
  }
}
