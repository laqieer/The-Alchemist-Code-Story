// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.VersionFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class VersionFormatter : IMessagePackFormatter<Version>, IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<Version> Instance = (IMessagePackFormatter<Version>) new VersionFormatter();

    private VersionFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Version value,
      IFormatterResolver formatterResolver)
    {
      return value == (Version) null ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteString(ref bytes, offset, value.ToString());
    }

    public Version Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new Version(MessagePackBinary.ReadString(bytes, offset, out readSize));
      readSize = 1;
      return (Version) null;
    }
  }
}
