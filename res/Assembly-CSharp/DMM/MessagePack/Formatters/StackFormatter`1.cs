// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.StackFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class StackFormatter<T> : 
    CollectionFormatterBase<T, T[], Stack<T>.Enumerator, Stack<T>>
  {
    protected override int? GetCount(Stack<T> sequence) => new int?(sequence.Count);

    protected override void Add(T[] collection, int index, T value)
    {
      collection[collection.Length - 1 - index] = value;
    }

    protected override T[] Create(int count) => new T[count];

    protected override Stack<T>.Enumerator GetSourceEnumerator(Stack<T> source)
    {
      return source.GetEnumerator();
    }

    protected override Stack<T> Complete(T[] intermediateCollection)
    {
      return new Stack<T>((IEnumerable<T>) intermediateCollection);
    }
  }
}
