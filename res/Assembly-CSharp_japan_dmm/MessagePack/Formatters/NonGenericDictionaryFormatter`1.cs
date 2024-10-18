// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NonGenericDictionaryFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NonGenericDictionaryFormatter<T> : 
    IMessagePackFormatter<T>,
    IMessagePackFormatter
    where T : class, IDictionary, new()
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      T value,
      IFormatterResolver formatterResolver)
    {
      if ((object) value == null)
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

    public T Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (T) null;
      }
      IMessagePackFormatter<object> formatterWithVerify = formatterResolver.GetFormatterWithVerify<object>();
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      T obj1 = new T();
      for (int index = 0; index < num2; ++index)
      {
        object key = formatterWithVerify.Deserialize(bytes, offset, formatterResolver, out readSize);
        offset += readSize;
        object obj2 = formatterWithVerify.Deserialize(bytes, offset, formatterResolver, out readSize);
        offset += readSize;
        obj1.Add(key, obj2);
      }
      readSize = offset - num1;
      return obj1;
    }
  }
}
