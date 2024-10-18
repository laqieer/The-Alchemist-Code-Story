// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.InterfaceListFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class InterfaceListFormatter<T> : CollectionFormatterBase<T, T[], IList<T>>
  {
    protected override void Add(T[] collection, int index, T value) => collection[index] = value;

    protected override T[] Create(int count) => new T[count];

    protected override IList<T> Complete(T[] intermediateCollection)
    {
      return (IList<T>) intermediateCollection;
    }
  }
}
