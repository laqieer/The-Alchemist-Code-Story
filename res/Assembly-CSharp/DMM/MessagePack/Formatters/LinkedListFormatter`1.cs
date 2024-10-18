// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.LinkedListFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class LinkedListFormatter<T> : 
    CollectionFormatterBase<T, LinkedList<T>, LinkedList<T>.Enumerator, LinkedList<T>>
  {
    protected override void Add(LinkedList<T> collection, int index, T value)
    {
      collection.AddLast(value);
    }

    protected override LinkedList<T> Complete(LinkedList<T> intermediateCollection)
    {
      return intermediateCollection;
    }

    protected override LinkedList<T> Create(int count) => new LinkedList<T>();

    protected override LinkedList<T>.Enumerator GetSourceEnumerator(LinkedList<T> source)
    {
      return source.GetEnumerator();
    }
  }
}
