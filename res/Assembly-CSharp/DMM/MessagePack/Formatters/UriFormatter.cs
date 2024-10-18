// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.UriFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class UriFormatter : IMessagePackFormatter<Uri>, IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<Uri> Instance = (IMessagePackFormatter<Uri>) new UriFormatter();

    private UriFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Uri value,
      IFormatterResolver formatterResolver)
    {
      return value == (Uri) null ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteString(ref bytes, offset, value.ToString());
    }

    public Uri Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new Uri(MessagePackBinary.ReadString(bytes, offset, out readSize), UriKind.RelativeOrAbsolute);
      readSize = 1;
      return (Uri) null;
    }
  }
}
