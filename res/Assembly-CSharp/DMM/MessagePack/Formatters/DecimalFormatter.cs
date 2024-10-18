// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.DecimalFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Globalization;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class DecimalFormatter : IMessagePackFormatter<Decimal>, IMessagePackFormatter
  {
    public static readonly DecimalFormatter Instance = new DecimalFormatter();

    private DecimalFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Decimal value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteString(ref bytes, offset, value.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }

    public Decimal Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return Decimal.Parse(MessagePackBinary.ReadString(bytes, offset, out readSize), (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
