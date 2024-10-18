// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.OldSpecBinaryFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class OldSpecBinaryFormatter : IMessagePackFormatter<byte[]>, IMessagePackFormatter
  {
    public static readonly OldSpecBinaryFormatter Instance = new OldSpecBinaryFormatter();

    public int Serialize(
      ref byte[] bytes,
      int offset,
      byte[] value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int length = value.Length;
      if (length <= 31)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 1);
        bytes[offset] = (byte) (160 | length);
        Buffer.BlockCopy((Array) value, 0, (Array) bytes, offset + 1, length);
        return length + 1;
      }
      if (length <= (int) ushort.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 3);
        bytes[offset] = (byte) 218;
        bytes[offset + 1] = (byte) (length >> 8);
        bytes[offset + 2] = (byte) length;
        Buffer.BlockCopy((Array) value, 0, (Array) bytes, offset + 3, length);
        return length + 3;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 5);
      bytes[offset] = (byte) 219;
      bytes[offset + 1] = (byte) (length >> 24);
      bytes[offset + 2] = (byte) (length >> 16);
      bytes[offset + 3] = (byte) (length >> 8);
      bytes[offset + 4] = (byte) length;
      Buffer.BlockCopy((Array) value, 0, (Array) bytes, offset + 5, length);
      return length + 5;
    }

    public byte[] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      switch (MessagePackBinary.GetMessagePackType(bytes, offset))
      {
        case MessagePackType.Nil:
          readSize = 1;
          return (byte[]) null;
        case MessagePackType.String:
          byte num = bytes[offset];
          if ((byte) 160 <= num && num <= (byte) 191)
          {
            int length = (int) bytes[offset] & 31;
            readSize = length + 1;
            byte[] dst = new byte[length];
            Buffer.BlockCopy((Array) bytes, offset + 1, (Array) dst, 0, dst.Length);
            return dst;
          }
          if (num == (byte) 217)
          {
            int length = (int) bytes[offset + 1];
            readSize = length + 2;
            byte[] dst = new byte[length];
            Buffer.BlockCopy((Array) bytes, offset + 2, (Array) dst, 0, dst.Length);
            return dst;
          }
          if (num == (byte) 218)
          {
            int length = ((int) bytes[offset + 1] << 8) + (int) bytes[offset + 2];
            readSize = length + 3;
            byte[] dst = new byte[length];
            Buffer.BlockCopy((Array) bytes, offset + 3, (Array) dst, 0, dst.Length);
            return dst;
          }
          if (num == (byte) 219)
          {
            int length = (int) bytes[offset + 1] << 24 | (int) bytes[offset + 2] << 16 | (int) bytes[offset + 3] << 8 | (int) bytes[offset + 4];
            readSize = length + 5;
            byte[] dst = new byte[length];
            Buffer.BlockCopy((Array) bytes, offset + 5, (Array) dst, 0, dst.Length);
            return dst;
          }
          break;
        case MessagePackType.Binary:
          return MessagePackBinary.ReadBytes(bytes, offset, out readSize);
      }
      throw new InvalidOperationException(string.Format("code is invalid. code:{0} format:{1}", (object) bytes[offset], (object) MessagePackCode.ToFormatName(bytes[offset])));
    }
  }
}
