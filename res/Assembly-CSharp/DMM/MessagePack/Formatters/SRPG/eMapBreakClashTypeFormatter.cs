// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.eMapBreakClashTypeFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class eMapBreakClashTypeFormatter : 
    IMessagePackFormatter<eMapBreakClashType>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      eMapBreakClashType value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt32(ref bytes, offset, (int) value);
    }

    public eMapBreakClashType Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return (eMapBreakClashType) MessagePackBinary.ReadInt32(bytes, offset, out readSize);
    }
  }
}
