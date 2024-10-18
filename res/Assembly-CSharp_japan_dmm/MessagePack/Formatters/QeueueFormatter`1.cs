// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.QeueueFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class QeueueFormatter<T> : 
    CollectionFormatterBase<T, Queue<T>, Queue<T>.Enumerator, Queue<T>>
  {
    protected override int? GetCount(Queue<T> sequence) => new int?(sequence.Count);

    protected override void Add(Queue<T> collection, int index, T value)
    {
      collection.Enqueue(value);
    }

    protected override Queue<T> Create(int count) => new Queue<T>(count);

    protected override Queue<T>.Enumerator GetSourceEnumerator(Queue<T> source)
    {
      return source.GetEnumerator();
    }

    protected override Queue<T> Complete(Queue<T> intermediateCollection) => intermediateCollection;
  }
}
