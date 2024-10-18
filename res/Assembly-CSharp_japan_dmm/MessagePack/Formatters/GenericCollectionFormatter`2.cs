// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.GenericCollectionFormatter`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class GenericCollectionFormatter<TElement, TCollection> : 
    CollectionFormatterBase<TElement, TCollection>
    where TCollection : ICollection<TElement>, new()
  {
    protected override TCollection Create(int count) => new TCollection();

    protected override void Add(TCollection collection, int index, TElement value)
    {
      collection.Add(value);
    }
  }
}
