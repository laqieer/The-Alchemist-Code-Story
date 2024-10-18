// Decompiled with JetBrains decompiler
// Type: MessagePack.MessagePackSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using MessagePack.Internal;
using MessagePack.Resolvers;
using System;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace MessagePack
{
  public static class MessagePackSerializer
  {
    private static IFormatterResolver defaultResolver;

    public static IFormatterResolver DefaultResolver
    {
      get
      {
        if (MessagePackSerializer.defaultResolver == null)
          MessagePackSerializer.defaultResolver = StandardResolver.Instance;
        return MessagePackSerializer.defaultResolver;
      }
    }

    public static bool IsInitialized => MessagePackSerializer.defaultResolver != null;

    public static void SetDefaultResolver(IFormatterResolver resolver)
    {
      MessagePackSerializer.defaultResolver = resolver;
    }

    public static byte[] Serialize<T>(T obj)
    {
      return MessagePackSerializer.Serialize<T>(obj, MessagePackSerializer.defaultResolver);
    }

    public static byte[] Serialize<T>(T obj, IFormatterResolver resolver)
    {
      if (resolver == null)
        resolver = MessagePackSerializer.DefaultResolver;
      IMessagePackFormatter<T> formatterWithVerify = resolver.GetFormatterWithVerify<T>();
      byte[] buffer = InternalMemoryPool.GetBuffer();
      int newSize = formatterWithVerify.Serialize(ref buffer, 0, obj, resolver);
      return MessagePackBinary.FastCloneWithResize(buffer, newSize);
    }

    public static ArraySegment<byte> SerializeUnsafe<T>(T obj)
    {
      return MessagePackSerializer.SerializeUnsafe<T>(obj, MessagePackSerializer.defaultResolver);
    }

    public static ArraySegment<byte> SerializeUnsafe<T>(T obj, IFormatterResolver resolver)
    {
      if (resolver == null)
        resolver = MessagePackSerializer.DefaultResolver;
      IMessagePackFormatter<T> formatterWithVerify = resolver.GetFormatterWithVerify<T>();
      byte[] buffer = InternalMemoryPool.GetBuffer();
      int count = formatterWithVerify.Serialize(ref buffer, 0, obj, resolver);
      return new ArraySegment<byte>(buffer, 0, count);
    }

    public static void Serialize<T>(Stream stream, T obj)
    {
      MessagePackSerializer.Serialize<T>(stream, obj, MessagePackSerializer.defaultResolver);
    }

    public static void Serialize<T>(Stream stream, T obj, IFormatterResolver resolver)
    {
      if (resolver == null)
        resolver = MessagePackSerializer.DefaultResolver;
      IMessagePackFormatter<T> formatterWithVerify = resolver.GetFormatterWithVerify<T>();
      byte[] buffer = InternalMemoryPool.GetBuffer();
      int count = formatterWithVerify.Serialize(ref buffer, 0, obj, resolver);
      stream.Write(buffer, 0, count);
    }

    public static int Serialize<T>(
      ref byte[] bytes,
      int offset,
      T value,
      IFormatterResolver resolver)
    {
      return resolver.GetFormatterWithVerify<T>().Serialize(ref bytes, offset, value, resolver);
    }

    public static T Deserialize<T>(byte[] bytes)
    {
      return MessagePackSerializer.Deserialize<T>(bytes, MessagePackSerializer.defaultResolver);
    }

    public static T Deserialize<T>(byte[] bytes, IFormatterResolver resolver)
    {
      if (resolver == null)
        resolver = MessagePackSerializer.DefaultResolver;
      return resolver.GetFormatterWithVerify<T>().Deserialize(bytes, 0, resolver, out int _);
    }

    public static T Deserialize<T>(ArraySegment<byte> bytes)
    {
      return MessagePackSerializer.Deserialize<T>(bytes, MessagePackSerializer.defaultResolver);
    }

    public static T Deserialize<T>(ArraySegment<byte> bytes, IFormatterResolver resolver)
    {
      if (resolver == null)
        resolver = MessagePackSerializer.DefaultResolver;
      return resolver.GetFormatterWithVerify<T>().Deserialize(bytes.Array, bytes.Offset, resolver, out int _);
    }

    public static T Deserialize<T>(Stream stream)
    {
      return MessagePackSerializer.Deserialize<T>(stream, MessagePackSerializer.defaultResolver);
    }

    public static T Deserialize<T>(Stream stream, IFormatterResolver resolver)
    {
      return MessagePackSerializer.Deserialize<T>(stream, resolver, false);
    }

    public static T Deserialize<T>(Stream stream, bool readStrict)
    {
      return MessagePackSerializer.Deserialize<T>(stream, MessagePackSerializer.defaultResolver, readStrict);
    }

    public static T Deserialize<T>(Stream stream, IFormatterResolver resolver, bool readStrict)
    {
      if (resolver == null)
        resolver = MessagePackSerializer.DefaultResolver;
      IMessagePackFormatter<T> formatterWithVerify = resolver.GetFormatterWithVerify<T>();
      if (!readStrict)
      {
        byte[] buffer = InternalMemoryPool.GetBuffer();
        MessagePackSerializer.FillFromStream(stream, ref buffer);
        return formatterWithVerify.Deserialize(buffer, 0, resolver, out int _);
      }
      byte[] bytes = MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream, false, out int _);
      return formatterWithVerify.Deserialize(bytes, 0, resolver, out int _);
    }

    public static T Deserialize<T>(
      byte[] bytes,
      int offset,
      IFormatterResolver resolver,
      out int readSize)
    {
      return resolver.GetFormatterWithVerify<T>().Deserialize(bytes, offset, resolver, out readSize);
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
      return MessagePackSerializer.ToJson(MessagePackSerializer.Serialize<T>(obj));
    }

    public static string ToJson<T>(T obj, IFormatterResolver resolver)
    {
      return MessagePackSerializer.ToJson(MessagePackSerializer.Serialize<T>(obj, resolver));
    }

    public static string ToJson(byte[] bytes)
    {
      if (bytes == null || bytes.Length == 0)
        return string.Empty;
      StringBuilder builder = new StringBuilder();
      MessagePackSerializer.ToJsonCore(bytes, 0, builder);
      return builder.ToString();
    }

    public static byte[] FromJson(string str)
    {
      using (StringReader reader = new StringReader(str))
        return MessagePackSerializer.FromJson((TextReader) reader);
    }

    public static byte[] FromJson(TextReader reader)
    {
      int offset = 0;
      byte[] numArray = (byte[]) null;
      using (TinyJsonReader jr = new TinyJsonReader(reader, false))
      {
        int num = (int) MessagePackSerializer.FromJsonCore(jr, ref numArray, ref offset);
      }
      MessagePackBinary.FastResize(ref numArray, offset);
      return numArray;
    }

    internal static ArraySegment<byte> FromJsonUnsafe(TextReader reader)
    {
      int offset = 0;
      byte[] buffer = InternalMemoryPool.GetBuffer();
      using (TinyJsonReader jr = new TinyJsonReader(reader, false))
      {
        int num = (int) MessagePackSerializer.FromJsonCore(jr, ref buffer, ref offset);
      }
      return new ArraySegment<byte>(buffer, 0, offset);
    }

    private static uint FromJsonCore(TinyJsonReader jr, ref byte[] binary, ref int offset)
    {
      uint num = 0;
      while (jr.Read())
      {
        switch (jr.TokenType)
        {
          case TinyJsonToken.StartObject:
            int offset1 = offset;
            offset += 5;
            uint count1 = MessagePackSerializer.FromJsonCore(jr, ref binary, ref offset) / 2U;
            MessagePackBinary.WriteMapHeaderForceMap32Block(ref binary, offset1, count1);
            ++num;
            continue;
          case TinyJsonToken.EndObject:
            return num;
          case TinyJsonToken.StartArray:
            int offset2 = offset;
            offset += 5;
            uint count2 = MessagePackSerializer.FromJsonCore(jr, ref binary, ref offset);
            MessagePackBinary.WriteArrayHeaderForceArray32Block(ref binary, offset2, count2);
            ++num;
            continue;
          case TinyJsonToken.EndArray:
            return num;
          case TinyJsonToken.Number:
            switch (jr.ValueType)
            {
              case ValueType.Double:
                offset += MessagePackBinary.WriteDouble(ref binary, offset, jr.DoubleValue);
                break;
              case ValueType.Long:
                offset += MessagePackBinary.WriteInt64(ref binary, offset, jr.LongValue);
                break;
              case ValueType.ULong:
                offset += MessagePackBinary.WriteUInt64(ref binary, offset, jr.ULongValue);
                break;
              case ValueType.Decimal:
                offset += DecimalFormatter.Instance.Serialize(ref binary, offset, jr.DecimalValue, (IFormatterResolver) null);
                break;
            }
            ++num;
            continue;
          case TinyJsonToken.String:
            offset += MessagePackBinary.WriteString(ref binary, offset, jr.StringValue);
            ++num;
            continue;
          case TinyJsonToken.True:
            offset += MessagePackBinary.WriteBoolean(ref binary, offset, true);
            ++num;
            continue;
          case TinyJsonToken.False:
            offset += MessagePackBinary.WriteBoolean(ref binary, offset, false);
            ++num;
            continue;
          case TinyJsonToken.Null:
            offset += MessagePackBinary.WriteNil(ref binary, offset);
            ++num;
            continue;
          default:
            continue;
        }
      }
      return num;
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
            builder.Append(MessagePackBinary.ReadSByte(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
            break;
          }
          if ((byte) 0 <= num1 && num1 <= (byte) 127)
          {
            builder.Append(MessagePackBinary.ReadByte(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
            break;
          }
          switch (num1)
          {
            case 204:
              builder.Append(MessagePackBinary.ReadByte(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case 205:
              builder.Append(MessagePackBinary.ReadUInt16(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case 206:
              builder.Append(MessagePackBinary.ReadUInt32(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case 207:
              builder.Append(MessagePackBinary.ReadUInt64(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case 208:
              builder.Append(MessagePackBinary.ReadSByte(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case 209:
              builder.Append(MessagePackBinary.ReadInt16(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case 210:
              builder.Append(MessagePackBinary.ReadInt32(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case 211:
              builder.Append(MessagePackBinary.ReadInt64(bytes, offset, out readSize).ToString((IFormatProvider) CultureInfo.InvariantCulture));
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
          MessagePackSerializer.WriteJsonString(MessagePackBinary.ReadString(bytes, offset, out readSize), builder);
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
            int jsonCore2 = MessagePackSerializer.ToJsonCore(bytes, offset, builder);
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
                jsonCore4 = MessagePackSerializer.ToJsonCore(bytes, offset, builder);
                break;
              default:
                builder.Append("\"");
                jsonCore4 = MessagePackSerializer.ToJsonCore(bytes, offset, builder);
                builder.Append("\"");
                break;
            }
            offset += jsonCore4;
            int num4 = jsonCore3 + jsonCore4;
            builder.Append(":");
            int jsonCore5 = MessagePackSerializer.ToJsonCore(bytes, offset, builder);
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
