// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Unit_UnitInspFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Unit_UnitInspFormatter : 
    IMessagePackFormatter<Unit.UnitInsp>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Unit.UnitInsp value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
      offset += formatterResolver.GetFormatterWithVerify<ArtifactData>().Serialize(ref bytes, offset, value.mArtifact, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.mSlotNo, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.mValid, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.mCheckCtr, formatterResolver);
      return offset - num;
    }

    public Unit.UnitInsp Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Unit.UnitInsp) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      ArtifactData artifact = (ArtifactData) null;
      OInt slot_no = new OInt();
      OBool valid = new OBool();
      OInt check_ctr = new OInt();
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            artifact = formatterResolver.GetFormatterWithVerify<ArtifactData>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 1:
            slot_no = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 2:
            valid = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 3:
            check_ctr = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Unit.UnitInsp(artifact, (int) slot_no, (bool) valid, (int) check_ctr)
      {
        mArtifact = artifact,
        mSlotNo = slot_no,
        mValid = valid,
        mCheckCtr = check_ctr
      };
    }
  }
}
