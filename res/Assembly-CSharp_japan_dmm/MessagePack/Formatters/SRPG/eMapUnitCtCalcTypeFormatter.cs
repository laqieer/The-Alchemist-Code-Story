// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.eMapUnitCtCalcTypeFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class eMapUnitCtCalcTypeFormatter : 
    IMessagePackFormatter<eMapUnitCtCalcType>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      eMapUnitCtCalcType value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt32(ref bytes, offset, (int) value);
    }

    public eMapUnitCtCalcType Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return (eMapUnitCtCalcType) MessagePackBinary.ReadInt32(bytes, offset, out readSize);
    }
  }
}
