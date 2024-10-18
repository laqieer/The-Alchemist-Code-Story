// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.OldSpecStringFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class OldSpecStringFormatter : IMessagePackFormatter<string>, IMessagePackFormatter
  {
    public static readonly OldSpecStringFormatter Instance = new OldSpecStringFormatter();

    public int Serialize(
      ref byte[] bytes,
      int offset,
      string value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      MessagePackBinary.EnsureCapacity(ref bytes, offset, StringEncoding.UTF8.GetMaxByteCount(value.Length) + 5);
      int num1 = value.Length > 31 ? (value.Length > (int) ushort.MaxValue ? 5 : 3) : 1;
      int num2 = offset + num1;
      int bytes1 = StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, num2);
      if (bytes1 <= 31)
      {
        if (num1 != 1)
          Buffer.BlockCopy((Array) bytes, num2, (Array) bytes, offset + 1, bytes1);
        bytes[offset] = (byte) (160 | bytes1);
        return bytes1 + 1;
      }
      if (bytes1 <= (int) ushort.MaxValue)
      {
        if (num1 != 3)
          Buffer.BlockCopy((Array) bytes, num2, (Array) bytes, offset + 3, bytes1);
        bytes[offset] = (byte) 218;
        bytes[offset + 1] = (byte) (bytes1 >> 8);
        bytes[offset + 2] = (byte) bytes1;
        return bytes1 + 3;
      }
      if (num1 != 5)
        Buffer.BlockCopy((Array) bytes, num2, (Array) bytes, offset + 5, bytes1);
      bytes[offset] = (byte) 219;
      bytes[offset + 1] = (byte) (bytes1 >> 24);
      bytes[offset + 2] = (byte) (bytes1 >> 16);
      bytes[offset + 3] = (byte) (bytes1 >> 8);
      bytes[offset + 4] = (byte) bytes1;
      return bytes1 + 5;
    }

    public string Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadString(bytes, offset, out readSize);
    }
  }
}
