// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NonGenericInterfaceListFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NonGenericInterfaceListFormatter : 
    IMessagePackFormatter<IList>,
    IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<IList> Instance = (IMessagePackFormatter<IList>) new NonGenericInterfaceListFormatter();

    private NonGenericInterfaceListFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      IList value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
      {
        MessagePackBinary.WriteNil(ref bytes, offset);
        return 1;
      }
      IMessagePackFormatter<object> formatterWithVerify = formatterResolver.GetFormatterWithVerify<object>();
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Count);
      foreach (object obj in (IEnumerable) value)
        offset += formatterWithVerify.Serialize(ref bytes, offset, obj, formatterResolver);
      return offset - num;
    }

    public IList Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (IList) null;
      }
      IMessagePackFormatter<object> formatterWithVerify = formatterResolver.GetFormatterWithVerify<object>();
      int num = offset;
      int length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      object[] objArray = new object[length];
      for (int index = 0; index < length; ++index)
      {
        objArray[index] = formatterWithVerify.Deserialize(bytes, offset, formatterResolver, out readSize);
        offset += readSize;
      }
      readSize = offset - num;
      return (IList) objArray;
    }
  }
}
