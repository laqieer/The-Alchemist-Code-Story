// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.BuiltinResolverGetFormatterHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace MessagePack.Internal
{
  internal static class BuiltinResolverGetFormatterHelper
  {
    private static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
    {
      {
        typeof (short),
        (object) Int16Formatter.Instance
      },
      {
        typeof (int),
        (object) Int32Formatter.Instance
      },
      {
        typeof (long),
        (object) Int64Formatter.Instance
      },
      {
        typeof (ushort),
        (object) UInt16Formatter.Instance
      },
      {
        typeof (uint),
        (object) UInt32Formatter.Instance
      },
      {
        typeof (ulong),
        (object) UInt64Formatter.Instance
      },
      {
        typeof (float),
        (object) SingleFormatter.Instance
      },
      {
        typeof (double),
        (object) DoubleFormatter.Instance
      },
      {
        typeof (bool),
        (object) BooleanFormatter.Instance
      },
      {
        typeof (byte),
        (object) ByteFormatter.Instance
      },
      {
        typeof (sbyte),
        (object) SByteFormatter.Instance
      },
      {
        typeof (DateTime),
        (object) DateTimeFormatter.Instance
      },
      {
        typeof (char),
        (object) CharFormatter.Instance
      },
      {
        typeof (short?),
        (object) NullableInt16Formatter.Instance
      },
      {
        typeof (int?),
        (object) NullableInt32Formatter.Instance
      },
      {
        typeof (long?),
        (object) NullableInt64Formatter.Instance
      },
      {
        typeof (ushort?),
        (object) NullableUInt16Formatter.Instance
      },
      {
        typeof (uint?),
        (object) NullableUInt32Formatter.Instance
      },
      {
        typeof (ulong?),
        (object) NullableUInt64Formatter.Instance
      },
      {
        typeof (float?),
        (object) NullableSingleFormatter.Instance
      },
      {
        typeof (double?),
        (object) NullableDoubleFormatter.Instance
      },
      {
        typeof (bool?),
        (object) NullableBooleanFormatter.Instance
      },
      {
        typeof (byte?),
        (object) NullableByteFormatter.Instance
      },
      {
        typeof (sbyte?),
        (object) NullableSByteFormatter.Instance
      },
      {
        typeof (DateTime?),
        (object) NullableDateTimeFormatter.Instance
      },
      {
        typeof (char?),
        (object) NullableCharFormatter.Instance
      },
      {
        typeof (string),
        (object) NullableStringFormatter.Instance
      },
      {
        typeof (Decimal),
        (object) DecimalFormatter.Instance
      },
      {
        typeof (Decimal?),
        (object) new StaticNullableFormatter<Decimal>((IMessagePackFormatter<Decimal>) DecimalFormatter.Instance)
      },
      {
        typeof (TimeSpan),
        (object) TimeSpanFormatter.Instance
      },
      {
        typeof (TimeSpan?),
        (object) new StaticNullableFormatter<TimeSpan>(TimeSpanFormatter.Instance)
      },
      {
        typeof (DateTimeOffset),
        (object) DateTimeOffsetFormatter.Instance
      },
      {
        typeof (DateTimeOffset?),
        (object) new StaticNullableFormatter<DateTimeOffset>(DateTimeOffsetFormatter.Instance)
      },
      {
        typeof (Guid),
        (object) GuidFormatter.Instance
      },
      {
        typeof (Guid?),
        (object) new StaticNullableFormatter<Guid>(GuidFormatter.Instance)
      },
      {
        typeof (Uri),
        (object) UriFormatter.Instance
      },
      {
        typeof (Version),
        (object) VersionFormatter.Instance
      },
      {
        typeof (StringBuilder),
        (object) StringBuilderFormatter.Instance
      },
      {
        typeof (BitArray),
        (object) BitArrayFormatter.Instance
      },
      {
        typeof (byte[]),
        (object) ByteArrayFormatter.Instance
      },
      {
        typeof (Nil),
        (object) NilFormatter.Instance
      },
      {
        typeof (Nil?),
        (object) NullableNilFormatter.Instance
      },
      {
        typeof (short[]),
        (object) Int16ArrayFormatter.Instance
      },
      {
        typeof (int[]),
        (object) Int32ArrayFormatter.Instance
      },
      {
        typeof (long[]),
        (object) Int64ArrayFormatter.Instance
      },
      {
        typeof (ushort[]),
        (object) UInt16ArrayFormatter.Instance
      },
      {
        typeof (uint[]),
        (object) UInt32ArrayFormatter.Instance
      },
      {
        typeof (ulong[]),
        (object) UInt64ArrayFormatter.Instance
      },
      {
        typeof (float[]),
        (object) SingleArrayFormatter.Instance
      },
      {
        typeof (double[]),
        (object) DoubleArrayFormatter.Instance
      },
      {
        typeof (bool[]),
        (object) BooleanArrayFormatter.Instance
      },
      {
        typeof (sbyte[]),
        (object) SByteArrayFormatter.Instance
      },
      {
        typeof (DateTime[]),
        (object) DateTimeArrayFormatter.Instance
      },
      {
        typeof (char[]),
        (object) CharArrayFormatter.Instance
      },
      {
        typeof (string[]),
        (object) NullableStringArrayFormatter.Instance
      },
      {
        typeof (List<short>),
        (object) new ListFormatter<short>()
      },
      {
        typeof (List<int>),
        (object) new ListFormatter<int>()
      },
      {
        typeof (List<long>),
        (object) new ListFormatter<long>()
      },
      {
        typeof (List<ushort>),
        (object) new ListFormatter<ushort>()
      },
      {
        typeof (List<uint>),
        (object) new ListFormatter<uint>()
      },
      {
        typeof (List<ulong>),
        (object) new ListFormatter<ulong>()
      },
      {
        typeof (List<float>),
        (object) new ListFormatter<float>()
      },
      {
        typeof (List<double>),
        (object) new ListFormatter<double>()
      },
      {
        typeof (List<bool>),
        (object) new ListFormatter<bool>()
      },
      {
        typeof (List<byte>),
        (object) new ListFormatter<byte>()
      },
      {
        typeof (List<sbyte>),
        (object) new ListFormatter<sbyte>()
      },
      {
        typeof (List<DateTime>),
        (object) new ListFormatter<DateTime>()
      },
      {
        typeof (List<char>),
        (object) new ListFormatter<char>()
      },
      {
        typeof (List<string>),
        (object) new ListFormatter<string>()
      },
      {
        typeof (ArraySegment<byte>),
        (object) ByteArraySegmentFormatter.Instance
      },
      {
        typeof (ArraySegment<byte>?),
        (object) new StaticNullableFormatter<ArraySegment<byte>>((IMessagePackFormatter<ArraySegment<byte>>) ByteArraySegmentFormatter.Instance)
      }
    };

    internal static object GetFormatter(Type t)
    {
      object obj;
      return BuiltinResolverGetFormatterHelper.formatterMap.TryGetValue(t, out obj) ? obj : (object) null;
    }
  }
}
