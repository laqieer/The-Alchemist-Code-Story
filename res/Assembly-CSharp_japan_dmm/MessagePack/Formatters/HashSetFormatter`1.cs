// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.HashSetFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class HashSetFormatter<T> : 
    CollectionFormatterBase<T, HashSet<T>, HashSet<T>.Enumerator, HashSet<T>>
  {
    protected override int? GetCount(HashSet<T> sequence) => new int?(sequence.Count);

    protected override void Add(HashSet<T> collection, int index, T value) => collection.Add(value);

    protected override HashSet<T> Complete(HashSet<T> intermediateCollection)
    {
      return intermediateCollection;
    }

    protected override HashSet<T> Create(int count) => new HashSet<T>();

    protected override HashSet<T>.Enumerator GetSourceEnumerator(HashSet<T> source)
    {
      return source.GetEnumerator();
    }
  }
}
