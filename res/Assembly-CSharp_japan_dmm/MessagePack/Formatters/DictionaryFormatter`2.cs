// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.DictionaryFormatter`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class DictionaryFormatter<TKey, TValue> : 
    DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, Dictionary<TKey, TValue>>
  {
    protected override void Add(
      Dictionary<TKey, TValue> collection,
      int index,
      TKey key,
      TValue value)
    {
      collection.Add(key, value);
    }

    protected override Dictionary<TKey, TValue> Complete(
      Dictionary<TKey, TValue> intermediateCollection)
    {
      return intermediateCollection;
    }

    protected override Dictionary<TKey, TValue> Create(int count)
    {
      return new Dictionary<TKey, TValue>(count);
    }
  }
}
