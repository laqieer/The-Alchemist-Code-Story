﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SortedDictionaryFormatter`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class SortedDictionaryFormatter<TKey, TValue> : 
    DictionaryFormatterBase<TKey, TValue, SortedDictionary<TKey, TValue>, SortedDictionary<TKey, TValue>>
  {
    protected override void Add(
      SortedDictionary<TKey, TValue> collection,
      int index,
      TKey key,
      TValue value)
    {
      collection.Add(key, value);
    }

    protected override SortedDictionary<TKey, TValue> Complete(
      SortedDictionary<TKey, TValue> intermediateCollection)
    {
      return intermediateCollection;
    }

    protected override SortedDictionary<TKey, TValue> Create(int count)
    {
      return new SortedDictionary<TKey, TValue>();
    }
  }
}
