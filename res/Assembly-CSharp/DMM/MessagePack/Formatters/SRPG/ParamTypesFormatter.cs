// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ParamTypesFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ParamTypesFormatter : IMessagePackFormatter<ParamTypes>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      ParamTypes value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt16(ref bytes, offset, (short) value);
    }

    public ParamTypes Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return (ParamTypes) MessagePackBinary.ReadInt16(bytes, offset, out readSize);
    }
  }
}
