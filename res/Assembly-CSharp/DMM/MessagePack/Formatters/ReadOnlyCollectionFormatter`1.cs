// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ReadOnlyCollectionFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ReadOnlyCollectionFormatter<T> : 
    CollectionFormatterBase<T, T[], ReadOnlyCollection<T>>
  {
    protected override void Add(T[] collection, int index, T value) => collection[index] = value;

    protected override ReadOnlyCollection<T> Complete(T[] intermediateCollection)
    {
      return new ReadOnlyCollection<T>((IList<T>) intermediateCollection);
    }

    protected override T[] Create(int count) => new T[count];
  }
}
