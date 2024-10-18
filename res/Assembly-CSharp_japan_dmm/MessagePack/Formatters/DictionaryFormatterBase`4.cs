// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.DictionaryFormatterBase`4
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public abstract class DictionaryFormatterBase<TKey, TValue, TIntermediate, TDictionary> : 
    IMessagePackFormatter<TDictionary>,
    IMessagePackFormatter
    where TDictionary : IDictionary<TKey, TValue>
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      TDictionary value,
      IFormatterResolver formatterResolver)
    {
      if ((object) value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      IMessagePackFormatter<TKey> formatterWithVerify1 = formatterResolver.GetFormatterWithVerify<TKey>();
      IMessagePackFormatter<TValue> formatterWithVerify2 = formatterResolver.GetFormatterWithVerify<TValue>();
      int count = value.Count;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, count);
      using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = value.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<TKey, TValue> current = enumerator.Current;
          offset += formatterWithVerify1.Serialize(ref bytes, offset, current.Key, formatterResolver);
          offset += formatterWithVerify2.Serialize(ref bytes, offset, current.Value, formatterResolver);
        }
      }
      return offset - num;
    }

    public TDictionary Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return default (TDictionary);
      }
      int num = offset;
      IMessagePackFormatter<TKey> formatterWithVerify1 = formatterResolver.GetFormatterWithVerify<TKey>();
      IMessagePackFormatter<TValue> formatterWithVerify2 = formatterResolver.GetFormatterWithVerify<TValue>();
      int count = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      TIntermediate intermediate = this.Create(count);
      for (int index = 0; index < count; ++index)
      {
        TKey key = formatterWithVerify1.Deserialize(bytes, offset, formatterResolver, out readSize);
        offset += readSize;
        TValue obj = formatterWithVerify2.Deserialize(bytes, offset, formatterResolver, out readSize);
        offset += readSize;
        this.Add(intermediate, index, key, obj);
      }
      readSize = offset - num;
      return this.Complete(intermediate);
    }

    protected abstract TIntermediate Create(int count);

    protected abstract void Add(TIntermediate collection, int index, TKey key, TValue value);

    protected abstract TDictionary Complete(TIntermediate intermediateCollection);
  }
}
