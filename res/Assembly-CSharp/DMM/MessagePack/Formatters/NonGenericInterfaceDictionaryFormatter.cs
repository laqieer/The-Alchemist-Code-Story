// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NonGenericInterfaceDictionaryFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NonGenericInterfaceDictionaryFormatter : 
    IMessagePackFormatter<IDictionary>,
    IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<IDictionary> Instance = (IMessagePackFormatter<IDictionary>) new NonGenericInterfaceDictionaryFormatter();

    private NonGenericInterfaceDictionaryFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      IDictionary value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
      {
        MessagePackBinary.WriteNil(ref bytes, offset);
        return 1;
      }
      IMessagePackFormatter<object> formatterWithVerify = formatterResolver.GetFormatterWithVerify<object>();
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, value.Count);
      foreach (DictionaryEntry dictionaryEntry in value)
      {
        offset += formatterWithVerify.Serialize(ref bytes, offset, dictionaryEntry.Key, formatterResolver);
        offset += formatterWithVerify.Serialize(ref bytes, offset, dictionaryEntry.Value, formatterResolver);
      }
      return offset - num;
    }

    public IDictionary Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (IDictionary) null;
      }
      IMessagePackFormatter<object> formatterWithVerify = formatterResolver.GetFormatterWithVerify<object>();
      int num = offset;
      int capacity = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Dictionary<object, object> dictionary = new Dictionary<object, object>(capacity);
      for (int index = 0; index < capacity; ++index)
      {
        object key = formatterWithVerify.Deserialize(bytes, offset, formatterResolver, out readSize);
        offset += readSize;
        object obj = formatterWithVerify.Deserialize(bytes, offset, formatterResolver, out readSize);
        offset += readSize;
        dictionary.Add(key, obj);
      }
      readSize = offset - num;
      return (IDictionary) dictionary;
    }
  }
}
