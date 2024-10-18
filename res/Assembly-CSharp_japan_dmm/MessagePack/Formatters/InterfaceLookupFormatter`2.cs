// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.InterfaceLookupFormatter`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class InterfaceLookupFormatter<TKey, TElement> : 
    CollectionFormatterBase<IGrouping<TKey, TElement>, Dictionary<TKey, IGrouping<TKey, TElement>>, ILookup<TKey, TElement>>
  {
    protected override void Add(
      Dictionary<TKey, IGrouping<TKey, TElement>> collection,
      int index,
      IGrouping<TKey, TElement> value)
    {
      collection.Add(value.Key, value);
    }

    protected override ILookup<TKey, TElement> Complete(
      Dictionary<TKey, IGrouping<TKey, TElement>> intermediateCollection)
    {
      return (ILookup<TKey, TElement>) new Lookup<TKey, TElement>(intermediateCollection);
    }

    protected override Dictionary<TKey, IGrouping<TKey, TElement>> Create(int count)
    {
      return new Dictionary<TKey, IGrouping<TKey, TElement>>(count);
    }
  }
}
