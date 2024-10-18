// Decompiled with JetBrains decompiler
// Type: MessagePack.LZ4MessagePackSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using MessagePack.Internal;
using MessagePack.LZ4;
using System;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace MessagePack
{
  public static class LZ4MessagePackSerializer
  {
    public const sbyte ExtensionTypeCode = 99;
    public const int NotCompressionSize = 64;

    public static byte[] Serialize<T>(T obj)
    {
      return LZ4MessagePackSerializer.Serialize<T>(obj, (IFormatterResolver) null);
    }

    public static byte[] Serialize<T>(T obj, IFormatterResolver resolver)
    {
      if (resolver == null)
        resolver = MessagePackSerializer.DefaultResolver;
      ArraySegment<byte> arraySegment = LZ4MessagePackSerializer.SerializeCore<T>(obj, resolver);
      return MessagePackBinary.FastCloneWithResize(arraySegment.Array, arraySegment.Count);
    }

    public static void Serialize<T>(Stream stream, T obj)
    {
      LZ4MessagePackSerializer.Serialize<T>(stream, obj, (IFormatterResolver) null);
    }

    public static void Serialize<T>(Stream stream, T obj, IFormatterResolver resolver)
    {
      if (resolver == null)
        resolver = MessagePackSerializer.DefaultResolver;
      ArraySegment<byte> arraySegment = LZ4MessagePackSerializer.SerializeCore<T>(obj, resolver);
      stream.Write(arraySegment.Array, 0, arraySegment.Count);
    }

    public static int SerializeToBlock<T>(
      ref byte[] bytes,
      int offset,
      T obj,
      IFormatterResolver resolver)
    {
      ArraySegment<byte> arraySegment = MessagePackSerializer.SerializeUnsafe<T>(obj, resolver);
      if (arraySegment.Count < 64)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, arraySegment.Count);
        Buffer.BlockCopy((Array) arraySegment.Array, arraySegment.Offset, (Array) bytes, offset, arraySegment.Count);
        return arraySegment.Count;
      }
      int num1 = LZ4Codec.MaximumOutputLength(arraySegment.Count);
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 11 + num1);
      int offset1 = offset;
      offset += 11;
      int num2 = LZ4Codec.Encode(arraySegment.Array, arraySegment.Offset, arraySegment.Count, bytes, offset, bytes.Length - offset);
      int offset2 = offset1 + MessagePackBinary.WriteExtensionFormatHeaderForceExt32Block(ref bytes, offset1, (sbyte) 99, num2 + 5);
      MessagePackBinary.WriteInt32ForceInt32Block(ref bytes, offset2, arraySegment.Count);
      return 11 + num2;
    }

    public static byte[] ToLZ4Binary(ArraySegment<byte> messagePackBinary)
    {
      ArraySegment<byte> lz4BinaryCore = LZ4MessagePackSerializer.ToLZ4BinaryCore(messagePackBinary);
      return MessagePackBinary.FastCloneWithResize(lz4BinaryCore.Array, lz4BinaryCore.Count);
    }

    private static ArraySegment<byte> SerializeCore<T>(T obj, IFormatterResolver resolver)
    {
      return LZ4MessagePackSerializer.ToLZ4BinaryCore(MessagePackSerializer.SerializeUnsafe<T>(obj, resolver));
    }

    private static ArraySegment<byte> ToLZ4BinaryCore(ArraySegment<byte> serializedData)
    {
      if (serializedData.Count < 64)
        return serializedData;
      int num1 = 0;
      byte[] bytes = LZ4MemoryPool.GetBuffer();
      int num2 = LZ4Codec.MaximumOutputLength(serializedData.Count);
      if (bytes.Length + 6 + 5 < num2)
        bytes = new byte[11 + num2];
      int offset1 = num1;
      int outputOffset = num1 + 11;
      int num3 = LZ4Codec.Encode(serializedData.Array, serializedData.Offset, serializedData.Count, bytes, outputOffset, bytes.Length - outputOffset);
      int offset2 = offset1 + MessagePackBinary.WriteExtensionFormatHeaderForceExt32Block(ref bytes, offset1, (sbyte) 99, num3 + 5);
      MessagePackBinary.WriteInt32ForceInt32Block(ref bytes, offset2, serializedData.Count);
      return new ArraySegment<byte>(bytes, 0, 11 + num3);
    }

    public static T Deserialize<T>(byte[] bytes)
    {
      return LZ4MessagePackSerializer.Deserialize<T>(bytes, (IFormatterResolver) null);
    }

    public static T Deserialize<T>(byte[] bytes, IFormatterResolver resolver)
    {
      return LZ4MessagePackSerializer.DeserializeCore<T>(new ArraySegment<byte>(bytes, 0, bytes.Length), resolver);
    }

    public static T Deserialize<T>(ArraySegment<byte> bytes)
    {
      return LZ4MessagePackSerializer.DeserializeCore<T>(bytes, (IFormatterResolver) null);
    }

    public static T Deserialize<T>(ArraySegment<byte> bytes, IFormatterResolver resolver)
    {
      return LZ4MessagePackSerializer.DeserializeCore<T>(bytes, resolver);
    }

    public static T Deserialize<T>(Stream stream)
    {
      return LZ4MessagePackSerializer.Deserialize<T>(stream, (IFormatterResolver) null);
    }

    public static T Deserialize<T>(Stream stream, IFormatterResolver resolver)
    {
      return LZ4MessagePackSerializer.Deserialize<T>(stream, resolver, false);
    }

    public static T Deserialize<T>(Stream stream, bool readStrict)
    {
      return LZ4MessagePackSerializer.Deserialize<T>(stream, MessagePackSerializer.DefaultResolver, readStrict);
    }

    public static T Deserialize<T>(Stream stream, IFormatterResolver resolver, bool readStrict)
    {
      if (readStrict)
      {
        int readSize;
        return LZ4MessagePackSerializer.DeserializeCore<T>(new ArraySegment<byte>(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream, false, out readSize), 0, readSize), resolver);
      }
      byte[] buffer = InternalMemoryPool.GetBuffer();
      int count = LZ4MessagePackSerializer.FillFromStream(stream, ref buffer);
      return LZ4MessagePackSerializer.DeserializeCore<T>(new ArraySegment<byte>(buffer, 0, count), resolver);
    }

    public static byte[] Decode(Stream stream, bool readStrict = false)
    {
      if (readStrict)
      {
        int readSize;
        return LZ4MessagePackSerializer.Decode(new ArraySegment<byte>(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream, false, out readSize), 0, readSize));
      }
      byte[] buffer = InternalMemoryPool.GetBuffer();
      int count = LZ4MessagePackSerializer.FillFromStream(stream, ref buffer);
      return LZ4MessagePackSerializer.Decode(new ArraySegment<byte>(buffer, 0, count));
    }

    public static byte[] Decode(byte[] bytes)
    {
      return LZ4MessagePackSerializer.Decode(new ArraySegment<byte>(bytes, 0, bytes.Length));
    }

    public static byte[] Decode(ArraySegment<byte> bytes)
    {
      int readSize;
      if (MessagePackBinary.GetMessagePackType(bytes.Array, bytes.Offset) == MessagePackType.Extension && MessagePackBinary.ReadExtensionFormatHeader(bytes.Array, bytes.Offset, out readSize).TypeCode == (sbyte) 99)
      {
        int offset = bytes.Offset + readSize;
        int outputLength = MessagePackBinary.ReadInt32(bytes.Array, offset, out readSize);
        int inputOffset = offset + readSize;
        byte[] output = new byte[outputLength];
        int inputLength = bytes.Count + bytes.Offset - inputOffset;
        LZ4Codec.Decode(bytes.Array, inputOffset, inputLength, output, 0, outputLength);
        return output;
      }
      if (bytes.Offset == 0 && bytes.Array.Length == bytes.Count)
        return bytes.Array;
      byte[] dst = new byte[bytes.Count];
      Buffer.BlockCopy((Array) bytes.Array, bytes.Offset, (Array) dst, 0, dst.Length);
      return dst;
    }

    public static byte[] DecodeUnsafe(byte[] bytes)
    {
      return LZ4MessagePackSerializer.DecodeUnsafe(new ArraySegment<byte>(bytes, 0, bytes.Length));
    }

    public static byte[] DecodeUnsafe(ArraySegment<byte> bytes)
    {
      int readSize;
      if (MessagePackBinary.GetMessagePackType(bytes.Array, bytes.Offset) == MessagePackType.Extension && MessagePackBinary.ReadExtensionFormatHeader(bytes.Array, bytes.Offset, out readSize).TypeCode == (sbyte) 99)
      {
        int offset = bytes.Offset + readSize;
        int outputLength = MessagePackBinary.ReadInt32(bytes.Array, offset, out readSize);
        int inputOffset = offset + readSize;
        byte[] output = LZ4MemoryPool.GetBuffer();
        if (output.Length < outputLength)
          output = new byte[outputLength];
        int inputLength = bytes.Count + bytes.Offset - inputOffset;
        LZ4Codec.Decode(bytes.Array, inputOffset, inputLength, output, 0, outputLength);
        return output;
      }
      if (bytes.Offset == 0 && bytes.Array.Length == bytes.Count)
        return bytes.Array;
      byte[] dst = new byte[bytes.Count];
      Buffer.BlockCopy((Array) bytes.Array, bytes.Offset, (Array) dst, 0, dst.Length);
      return dst;
    }

    private static T DeserializeCore<T>(ArraySegment<byte> bytes, IFormatterResolver resolver)
    {
      if (resolver == null)
        resolver = MessagePackSerializer.DefaultResolver;
      IMessagePackFormatter<T> formatterWithVerify = resolver.GetFormatterWithVerify<T>();
      int readSize;
      if (MessagePackBinary.GetMessagePackType(bytes.Array, bytes.Offset) != MessagePackType.Extension || MessagePackBinary.ReadExtensionFormatHeader(bytes.Array, bytes.Offset, out readSize).TypeCode != (sbyte) 99)
        return formatterWithVerify.Deserialize(bytes.Array, bytes.Offset, resolver, out readSize);
      int offset = bytes.Offset + readSize;
      int outputLength = MessagePackBinary.ReadInt32(bytes.Array, offset, out readSize);
      int inputOffset = offset + readSize;
      byte[] numArray = LZ4MemoryPool.GetBuffer();
      if (numArray.Length < outputLength)
        numArray = new byte[outputLength];
      int inputLength = bytes.Count + bytes.Offset - inputOffset;
      LZ4Codec.Decode(bytes.Array, inputOffset, inputLength, numArray, 0, outputLength);
      return formatterWithVerify.Deserialize(numArray, 0, resolver, out readSize);
    }

    private static int FillFromStream(Stream input, ref byte[] buffer)
    {
      int offset = 0;
      int num;
      while ((num = input.Read(buffer, offset, buffer.Length - offset)) > 0)
      {
        offset += num;
        if (offset == buffer.Length)
          MessagePackBinary.FastResize(ref buffer, offset * 2);
      }
      return offset;
    }

    public static string ToJson<T>(T obj)
    {
      return LZ4MessagePackSerializer.ToJson(LZ4MessagePackSerializer.Serialize<T>(obj));
    }

    public static string ToJson<T>(T obj, IFormatterResolver resolver)
    {
      return LZ4MessagePackSerializer.ToJson(LZ4MessagePackSerializer.Serialize<T>(obj, resolver));
    }

    public static string ToJson(byte[] bytes)
    {
      if (bytes == null || bytes.Length == 0)
        return string.Empty;
      int readSize;
      if (MessagePackBinary.GetMessagePackType(bytes, 0) == MessagePackType.Extension && MessagePackBinary.ReadExtensionFormatHeader(bytes, 0, out readSize).TypeCode == (sbyte) 99)
      {
        int offset = readSize;
        int outputLength = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
        int inputOffset = offset + readSize;
        byte[] output = LZ4MemoryPool.GetBuffer();
        if (output.Length < outputLength)
          output = new byte[outputLength];
        LZ4Codec.Decode(bytes, inputOffset, bytes.Length - inputOffset, output, 0, outputLength);
        bytes = output;
      }
      StringBuilder builder = new StringBuilder();
      LZ4MessagePackSerializer.ToJsonCore(bytes, 0, builder);
      return builder.ToString();
    }

    public static byte[] FromJson(string str)
    {
      using (StringReader reader = new StringReader(str))
        return LZ4MessagePackSerializer.FromJson((TextReader) reader);
    }

    public static byte[] FromJson(TextReader reader)
    {
      return LZ4MessagePackSerializer.ToLZ4Binary(MessagePackSerializer.FromJsonUnsafe(reader));
    }

    private static int ToJsonCore(byte[] bytes, int offset, StringBuilder builder)
    {
      int readSize = 0;
      switch (MessagePackBinary.GetMessagePackType(bytes, offset))
      {
        case MessagePackType.Integer:
          byte num1 = bytes[offset];
          if ((byte) 224 <= num1 && num1 <= byte.MaxValue)
          {
            builder.Append(MessagePackBinary.ReadSByte(bytes, offset, out readSize));
            break;
          }
          if ((byte) 0 <= num1 && num1 <= (byte) 127)
          {
            builder.Append(MessagePackBinary.ReadByte(bytes, offset, out readSize));
            break;
          }
          switch (num1)
          {
            case 204:
              builder.Append(MessagePackBinary.ReadByte(bytes, offset, out readSize));
              break;
            case 205:
              builder.Append(MessagePackBinary.ReadUInt16(bytes, offset, out readSize));
              break;
            case 206:
              builder.Append(MessagePackBinary.ReadUInt32(bytes, offset, out readSize));
              break;
            case 207:
              builder.Append(MessagePackBinary.ReadUInt64(bytes, offset, out readSize));
              break;
            case 208:
              builder.Append(MessagePackBinary.ReadSByte(bytes, offset, out readSize));
              break;
            case 209:
              builder.Append(MessagePackBinary.ReadInt16(bytes, offset, out readSize));
              break;
            case 210:
              builder.Append(MessagePackBinary.ReadInt32(bytes, offset, out readSize));
              break;
            case 211:
              builder.Append(MessagePackBinary.ReadInt64(bytes, offset, out readSize));
              break;
          }
          break;
        case MessagePackType.Boolean:
          builder.Append(!MessagePackBinary.ReadBoolean(bytes, offset, out readSize) ? "false" : "true");
          break;
        case MessagePackType.Float:
          if (bytes[offset] == (byte) 202)
          {
            builder.Append(MessagePackBinary.ReadSingle(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
            break;
          }
          builder.Append(MessagePackBinary.ReadDouble(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case MessagePackType.String:
          LZ4MessagePackSerializer.WriteJsonString(MessagePackBinary.ReadString(bytes, offset, out readSize), builder);
          break;
        case MessagePackType.Binary:
          builder.Append("\"" + Convert.ToBase64String(MessagePackBinary.ReadBytes(bytes, offset, out readSize)) + "\"");
          break;
        case MessagePackType.Array:
          uint num2 = MessagePackBinary.ReadArrayHeaderRaw(bytes, offset, out readSize);
          int jsonCore1 = readSize;
          offset += readSize;
          builder.Append("[");
          for (int index = 0; (long) index < (long) num2; ++index)
          {
            int jsonCore2 = LZ4MessagePackSerializer.ToJsonCore(bytes, offset, builder);
            offset += jsonCore2;
            jsonCore1 += jsonCore2;
            if ((long) index != (long) (num2 - 1U))
              builder.Append(",");
          }
          builder.Append("]");
          return jsonCore1;
        case MessagePackType.Map:
          uint num3 = MessagePackBinary.ReadMapHeaderRaw(bytes, offset, out readSize);
          int jsonCore3 = readSize;
          offset += readSize;
          builder.Append("{");
          for (int index = 0; (long) index < (long) num3; ++index)
          {
            int jsonCore4;
            switch (MessagePackBinary.GetMessagePackType(bytes, offset))
            {
              case MessagePackType.String:
              case MessagePackType.Binary:
                jsonCore4 = LZ4MessagePackSerializer.ToJsonCore(bytes, offset, builder);
                break;
              default:
                builder.Append("\"");
                jsonCore4 = LZ4MessagePackSerializer.ToJsonCore(bytes, offset, builder);
                builder.Append("\"");
                break;
            }
            offset += jsonCore4;
            int num4 = jsonCore3 + jsonCore4;
            builder.Append(":");
            int jsonCore5 = LZ4MessagePackSerializer.ToJsonCore(bytes, offset, builder);
            offset += jsonCore5;
            jsonCore3 = num4 + jsonCore5;
            if ((long) index != (long) (num3 - 1U))
              builder.Append(",");
          }
          builder.Append("}");
          return jsonCore3;
        case MessagePackType.Extension:
          if (MessagePackBinary.ReadExtensionFormatHeader(bytes, offset, out readSize).TypeCode == (sbyte) -1)
          {
            DateTime dateTime = MessagePackBinary.ReadDateTime(bytes, offset, out readSize);
            builder.Append("\"");
            builder.Append(dateTime.ToString("o", (IFormatProvider) CultureInfo.InvariantCulture));
            builder.Append("\"");
            break;
          }
          ExtensionResult extensionResult = MessagePackBinary.ReadExtensionFormat(bytes, offset, out readSize);
          builder.Append("[");
          builder.Append(extensionResult.TypeCode);
          builder.Append(",");
          builder.Append("\"");
          builder.Append(Convert.ToBase64String(extensionResult.Data));
          builder.Append("\"");
          builder.Append("]");
          break;
        default:
          readSize = 1;
          builder.Append("null");
          break;
      }
      return readSize;
    }

    private static void WriteJsonString(string value, StringBuilder builder)
    {
      builder.Append('"');
      int length = value.Length;
      for (int index = 0; index < length; ++index)
      {
        char ch = value[index];
        switch (ch)
        {
          case '\b':
            builder.Append("\\b");
            break;
          case '\t':
            builder.Append("\\t");
            break;
          case '\n':
            builder.Append("\\n");
            break;
          case '\f':
            builder.Append("\\f");
            break;
          case '\r':
            builder.Append("\\r");
            break;
          default:
            switch (ch)
            {
              case '"':
                builder.Append("\\\"");
                continue;
              case '\\':
                builder.Append("\\\\");
                continue;
              default:
                builder.Append(ch);
                continue;
            }
        }
      }
      builder.Append('"');
    }
  }
}
