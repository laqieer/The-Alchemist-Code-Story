// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.BuffAttachment_ResistStatusBuffFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class BuffAttachment_ResistStatusBuffFormatter : 
    IMessagePackFormatter<BuffAttachment.ResistStatusBuff>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      BuffAttachment.ResistStatusBuff value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
      offset += formatterResolver.GetFormatterWithVerify<StatusTypes>().Serialize(ref bytes, offset, value.mType, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.mVal, formatterResolver);
      return offset - num;
    }

    public BuffAttachment.ResistStatusBuff Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (BuffAttachment.ResistStatusBuff) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      StatusTypes st = StatusTypes.Hp;
      OInt val = new OInt();
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            st = formatterResolver.GetFormatterWithVerify<StatusTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 1:
            val = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new BuffAttachment.ResistStatusBuff(st, (int) val)
      {
        mType = st,
        mVal = val
      };
    }
  }
}
