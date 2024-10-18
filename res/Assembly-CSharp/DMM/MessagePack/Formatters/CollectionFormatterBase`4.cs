// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.CollectionFormatterBase`4
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public abstract class CollectionFormatterBase<TElement, TIntermediate, TEnumerator, TCollection> : 
    IMessagePackFormatter<TCollection>,
    IMessagePackFormatter
    where TEnumerator : IEnumerator<TElement>
    where TCollection : IEnumerable<TElement>
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      TCollection value,
      IFormatterResolver formatterResolver)
    {
      if ((object) value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      if (value is TElement[] elementArray)
      {
        int num = offset;
        IMessagePackFormatter<TElement> formatterWithVerify = formatterResolver.GetFormatterWithVerify<TElement>();
        offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, elementArray.Length);
        foreach (TElement element in elementArray)
          offset += formatterWithVerify.Serialize(ref bytes, offset, element, formatterResolver);
        return offset - num;
      }
      int num1 = offset;
      IMessagePackFormatter<TElement> formatterWithVerify1 = formatterResolver.GetFormatterWithVerify<TElement>();
      int? count1 = this.GetCount(value);
      if (count1.HasValue)
      {
        offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, count1.Value);
        using (TEnumerator sourceEnumerator = this.GetSourceEnumerator(value))
        {
          while (sourceEnumerator.MoveNext())
            offset += formatterWithVerify1.Serialize(ref bytes, offset, sourceEnumerator.Current, formatterResolver);
        }
        return offset - num1;
      }
      int offset1 = offset;
      int count2 = 0;
      int count3 = 0;
      offset += 3;
      using (TEnumerator sourceEnumerator = this.GetSourceEnumerator(value))
      {
        while (sourceEnumerator.MoveNext())
        {
          ++count2;
          int num2 = formatterWithVerify1.Serialize(ref bytes, offset, sourceEnumerator.Current, formatterResolver);
          count3 += num2;
          offset += num2;
        }
      }
      int arrayHeaderLength = MessagePackBinary.GetArrayHeaderLength(count2);
      switch (arrayHeaderLength)
      {
        case 1:
          offset -= 2;
          break;
        case 3:
label_23:
          MessagePackBinary.WriteArrayHeader(ref bytes, offset1, count2);
          return offset - num1;
        default:
          offset += 2;
          break;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, arrayHeaderLength);
      Buffer.BlockCopy((Array) bytes, offset1 + 3, (Array) bytes, offset1 + arrayHeaderLength, count3);
      goto label_23;
    }

    public TCollection Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return default (TCollection);
      }
      int num = offset;
      IMessagePackFormatter<TElement> formatterWithVerify = formatterResolver.GetFormatterWithVerify<TElement>();
      int count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      TIntermediate intermediate = this.Create(count);
      for (int index = 0; index < count; ++index)
      {
        this.Add(intermediate, index, formatterWithVerify.Deserialize(bytes, offset, formatterResolver, out readSize));
        offset += readSize;
      }
      readSize = offset - num;
      return this.Complete(intermediate);
    }

    protected virtual int? GetCount(TCollection sequence)
    {
      return sequence is ICollection<TElement> elements ? new int?(elements.Count) : new int?();
    }

    protected abstract TEnumerator GetSourceEnumerator(TCollection source);

    protected abstract TIntermediate Create(int count);

    protected abstract void Add(TIntermediate collection, int index, TElement value);

    protected abstract TCollection Complete(TIntermediate intermediateCollection);
  }
}
