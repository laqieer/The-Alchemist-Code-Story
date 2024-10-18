// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.GenericDictionaryFormatter`3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class GenericDictionaryFormatter<TKey, TValue, TDictionary> : 
    DictionaryFormatterBase<TKey, TValue, TDictionary, TDictionary>
    where TDictionary : IDictionary<TKey, TValue>, new()
  {
    protected override void Add(TDictionary collection, int index, TKey key, TValue value)
    {
      collection.Add(key, value);
    }

    protected override TDictionary Complete(TDictionary intermediateCollection)
    {
      return intermediateCollection;
    }

    protected override TDictionary Create(int count) => new TDictionary();
  }
}
