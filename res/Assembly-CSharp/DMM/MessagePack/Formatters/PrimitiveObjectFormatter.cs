// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.PrimitiveObjectFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class PrimitiveObjectFormatter : IMessagePackFormatter<object>, IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<object> Instance = (IMessagePackFormatter<object>) new PrimitiveObjectFormatter();
    private static readonly Dictionary<Type, int> typeToJumpCode = new Dictionary<Type, int>()
    {
      {
        typeof (bool),
        0
      },
      {
        typeof (char),
        1
      },
      {
        typeof (sbyte),
        2
      },
      {
        typeof (byte),
        3
      },
      {
        typeof (short),
        4
      },
      {
        typeof (ushort),
        5
      },
      {
        typeof (int),
        6
      },
      {
        typeof (uint),
        7
      },
      {
        typeof (long),
        8
      },
      {
        typeof (ulong),
        9
      },
      {
        typeof (float),
        10
      },
      {
        typeof (double),
        11
      },
      {
        typeof (DateTime),
        12
      },
      {
        typeof (string),
        13
      },
      {
        typeof (byte[]),
        14
      }
    };

    private PrimitiveObjectFormatter()
    {
    }

    public static bool IsSupportedType(Type type, TypeInfo typeInfo, object value)
    {
      if (value == null || PrimitiveObjectFormatter.typeToJumpCode.ContainsKey(type) || typeInfo.IsEnum)
        return true;
      switch (value)
      {
        case IDictionary _:
          return true;
        case ICollection _:
          return true;
        default:
          return false;
      }
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      object value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      Type type = value.GetType();
      int num1;
      if (PrimitiveObjectFormatter.typeToJumpCode.TryGetValue(type, out num1))
      {
        switch (num1)
        {
          case 0:
            return MessagePackBinary.WriteBoolean(ref bytes, offset, (bool) value);
          case 1:
            return MessagePackBinary.WriteChar(ref bytes, offset, (char) value);
          case 2:
            return MessagePackBinary.WriteSByteForceSByteBlock(ref bytes, offset, (sbyte) value);
          case 3:
            return MessagePackBinary.WriteByteForceByteBlock(ref bytes, offset, (byte) value);
          case 4:
            return MessagePackBinary.WriteInt16ForceInt16Block(ref bytes, offset, (short) value);
          case 5:
            return MessagePackBinary.WriteUInt16ForceUInt16Block(ref bytes, offset, (ushort) value);
          case 6:
            return MessagePackBinary.WriteInt32ForceInt32Block(ref bytes, offset, (int) value);
          case 7:
            return MessagePackBinary.WriteUInt32ForceUInt32Block(ref bytes, offset, (uint) value);
          case 8:
            return MessagePackBinary.WriteInt64ForceInt64Block(ref bytes, offset, (long) value);
          case 9:
            return MessagePackBinary.WriteUInt64ForceUInt64Block(ref bytes, offset, (ulong) value);
          case 10:
            return MessagePackBinary.WriteSingle(ref bytes, offset, (float) value);
          case 11:
            return MessagePackBinary.WriteDouble(ref bytes, offset, (double) value);
          case 12:
            return MessagePackBinary.WriteDateTime(ref bytes, offset, (DateTime) value);
          case 13:
            return MessagePackBinary.WriteString(ref bytes, offset, (string) value);
          case 14:
            return MessagePackBinary.WriteBytes(ref bytes, offset, (byte[]) value);
          default:
            throw new InvalidOperationException("Not supported primitive object resolver. type:" + type.Name);
        }
      }
      else
      {
        if (ReflectionExtensions.GetTypeInfo(type).IsEnum)
        {
          Type underlyingType = Enum.GetUnderlyingType(type);
          switch (PrimitiveObjectFormatter.typeToJumpCode[underlyingType])
          {
            case 2:
              return MessagePackBinary.WriteSByteForceSByteBlock(ref bytes, offset, (sbyte) value);
            case 3:
              return MessagePackBinary.WriteByteForceByteBlock(ref bytes, offset, (byte) value);
            case 4:
              return MessagePackBinary.WriteInt16ForceInt16Block(ref bytes, offset, (short) value);
            case 5:
              return MessagePackBinary.WriteUInt16ForceUInt16Block(ref bytes, offset, (ushort) value);
            case 6:
              return MessagePackBinary.WriteInt32ForceInt32Block(ref bytes, offset, (int) value);
            case 7:
              return MessagePackBinary.WriteUInt32ForceUInt32Block(ref bytes, offset, (uint) value);
            case 8:
              return MessagePackBinary.WriteInt64ForceInt64Block(ref bytes, offset, (long) value);
            case 9:
              return MessagePackBinary.WriteUInt64ForceUInt64Block(ref bytes, offset, (ulong) value);
          }
        }
        else
        {
          switch (value)
          {
            case IDictionary _:
              IDictionary dictionary = value as IDictionary;
              int num2 = offset;
              offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, dictionary.Count);
              foreach (DictionaryEntry dictionaryEntry in dictionary)
              {
                offset += this.Serialize(ref bytes, offset, dictionaryEntry.Key, formatterResolver);
                offset += this.Serialize(ref bytes, offset, dictionaryEntry.Value, formatterResolver);
              }
              return offset - num2;
            case ICollection _:
              ICollection collection = value as ICollection;
              int num3 = offset;
              offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, collection.Count);
              foreach (object obj in (IEnumerable) collection)
                offset += this.Serialize(ref bytes, offset, obj, formatterResolver);
              return offset - num3;
          }
        }
        throw new InvalidOperationException("Not supported primitive object resolver. type:" + type.Name);
      }
    }

    public object Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      switch (MessagePackBinary.GetMessagePackType(bytes, offset))
      {
        case MessagePackType.Integer:
          byte num1 = bytes[offset];
          if ((byte) 224 <= num1 && num1 <= byte.MaxValue)
            return (object) MessagePackBinary.ReadSByte(bytes, offset, out readSize);
          if ((byte) 0 <= num1 && num1 <= (byte) 127)
            return (object) MessagePackBinary.ReadByte(bytes, offset, out readSize);
          switch (num1)
          {
            case 204:
              return (object) MessagePackBinary.ReadByte(bytes, offset, out readSize);
            case 205:
              return (object) MessagePackBinary.ReadUInt16(bytes, offset, out readSize);
            case 206:
              return (object) MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
            case 207:
              return (object) MessagePackBinary.ReadUInt64(bytes, offset, out readSize);
            case 208:
              return (object) MessagePackBinary.ReadSByte(bytes, offset, out readSize);
            case 209:
              return (object) MessagePackBinary.ReadInt16(bytes, offset, out readSize);
            case 210:
              return (object) MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            case 211:
              return (object) MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            default:
              throw new InvalidOperationException("Invalid primitive bytes.");
          }
        case MessagePackType.Nil:
          readSize = 1;
          return (object) null;
        case MessagePackType.Boolean:
          return (object) MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
        case MessagePackType.Float:
          return bytes[offset] == (byte) 202 ? (object) MessagePackBinary.ReadSingle(bytes, offset, out readSize) : (object) MessagePackBinary.ReadDouble(bytes, offset, out readSize);
        case MessagePackType.String:
          return (object) MessagePackBinary.ReadString(bytes, offset, out readSize);
        case MessagePackType.Binary:
          return (object) MessagePackBinary.ReadBytes(bytes, offset, out readSize);
        case MessagePackType.Array:
          int length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
          int num2 = offset;
          offset += readSize;
          IMessagePackFormatter<object> formatter1 = formatterResolver.GetFormatter<object>();
          object[] objArray = new object[length];
          for (int index = 0; index < length; ++index)
          {
            objArray[index] = formatter1.Deserialize(bytes, offset, formatterResolver, out readSize);
            offset += readSize;
          }
          readSize = offset - num2;
          return (object) objArray;
        case MessagePackType.Map:
          int capacity = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
          int num3 = offset;
          offset += readSize;
          IMessagePackFormatter<object> formatter2 = formatterResolver.GetFormatter<object>();
          Dictionary<object, object> dictionary = new Dictionary<object, object>(capacity);
          for (int index = 0; index < capacity; ++index)
          {
            object key = formatter2.Deserialize(bytes, offset, formatterResolver, out readSize);
            offset += readSize;
            object obj = formatter2.Deserialize(bytes, offset, formatterResolver, out readSize);
            offset += readSize;
            dictionary.Add(key, obj);
          }
          readSize = offset - num3;
          return (object) dictionary;
        case MessagePackType.Extension:
          if (MessagePackBinary.ReadExtensionFormatHeader(bytes, offset, out readSize).TypeCode == (sbyte) -1)
            return (object) MessagePackBinary.ReadDateTime(bytes, offset, out readSize);
          throw new InvalidOperationException("Invalid primitive bytes.");
        default:
          throw new InvalidOperationException("Invalid primitive bytes.");
      }
    }
  }
}
