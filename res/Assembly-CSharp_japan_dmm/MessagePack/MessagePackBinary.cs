// Decompiled with JetBrains decompiler
// Type: MessagePack.MessagePackBinary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Decoders;
using System;
using System.IO;

#nullable disable
namespace MessagePack
{
  public static class MessagePackBinary
  {
    private const int MaxSize = 256;
    private const int ArrayMaxSize = 2147483591;
    private static readonly IMapHeaderDecoder[] mapHeaderDecoders = new IMapHeaderDecoder[256];
    private static readonly IArrayHeaderDecoder[] arrayHeaderDecoders = new IArrayHeaderDecoder[256];
    private static readonly IBooleanDecoder[] booleanDecoders = new IBooleanDecoder[256];
    private static readonly IByteDecoder[] byteDecoders = new IByteDecoder[256];
    private static readonly IBytesDecoder[] bytesDecoders = new IBytesDecoder[256];
    private static readonly IBytesSegmentDecoder[] bytesSegmentDecoders = new IBytesSegmentDecoder[256];
    private static readonly ISByteDecoder[] sbyteDecoders = new ISByteDecoder[256];
    private static readonly ISingleDecoder[] singleDecoders = new ISingleDecoder[256];
    private static readonly IDoubleDecoder[] doubleDecoders = new IDoubleDecoder[256];
    private static readonly IInt16Decoder[] int16Decoders = new IInt16Decoder[256];
    private static readonly IInt32Decoder[] int32Decoders = new IInt32Decoder[256];
    private static readonly IInt64Decoder[] int64Decoders = new IInt64Decoder[256];
    private static readonly IUInt16Decoder[] uint16Decoders = new IUInt16Decoder[256];
    private static readonly IUInt32Decoder[] uint32Decoders = new IUInt32Decoder[256];
    private static readonly IUInt64Decoder[] uint64Decoders = new IUInt64Decoder[256];
    private static readonly IStringDecoder[] stringDecoders = new IStringDecoder[256];
    private static readonly IStringSegmentDecoder[] stringSegmentDecoders = new IStringSegmentDecoder[256];
    private static readonly IExtDecoder[] extDecoders = new IExtDecoder[256];
    private static readonly IExtHeaderDecoder[] extHeaderDecoders = new IExtHeaderDecoder[256];
    private static readonly IDateTimeDecoder[] dateTimeDecoders = new IDateTimeDecoder[256];
    private static readonly IReadNextDecoder[] readNextDecoders = new IReadNextDecoder[256];

    static MessagePackBinary()
    {
      for (int index = 0; index < 256; ++index)
      {
        MessagePackBinary.mapHeaderDecoders[index] = InvalidMapHeader.Instance;
        MessagePackBinary.arrayHeaderDecoders[index] = InvalidArrayHeader.Instance;
        MessagePackBinary.booleanDecoders[index] = InvalidBoolean.Instance;
        MessagePackBinary.byteDecoders[index] = InvalidByte.Instance;
        MessagePackBinary.bytesDecoders[index] = InvalidBytes.Instance;
        MessagePackBinary.bytesSegmentDecoders[index] = InvalidBytesSegment.Instance;
        MessagePackBinary.sbyteDecoders[index] = InvalidSByte.Instance;
        MessagePackBinary.singleDecoders[index] = InvalidSingle.Instance;
        MessagePackBinary.doubleDecoders[index] = InvalidDouble.Instance;
        MessagePackBinary.int16Decoders[index] = InvalidInt16.Instance;
        MessagePackBinary.int32Decoders[index] = InvalidInt32.Instance;
        MessagePackBinary.int64Decoders[index] = InvalidInt64.Instance;
        MessagePackBinary.uint16Decoders[index] = InvalidUInt16.Instance;
        MessagePackBinary.uint32Decoders[index] = InvalidUInt32.Instance;
        MessagePackBinary.uint64Decoders[index] = InvalidUInt64.Instance;
        MessagePackBinary.stringDecoders[index] = InvalidString.Instance;
        MessagePackBinary.stringSegmentDecoders[index] = InvalidStringSegment.Instance;
        MessagePackBinary.extDecoders[index] = InvalidExt.Instance;
        MessagePackBinary.extHeaderDecoders[index] = InvalidExtHeader.Instance;
        MessagePackBinary.dateTimeDecoders[index] = InvalidDateTime.Instance;
      }
      for (int index = 224; index <= (int) byte.MaxValue; ++index)
      {
        MessagePackBinary.sbyteDecoders[index] = FixSByte.Instance;
        MessagePackBinary.int16Decoders[index] = FixNegativeInt16.Instance;
        MessagePackBinary.int32Decoders[index] = FixNegativeInt32.Instance;
        MessagePackBinary.int64Decoders[index] = FixNegativeInt64.Instance;
        MessagePackBinary.singleDecoders[index] = FixNegativeFloat.Instance;
        MessagePackBinary.doubleDecoders[index] = FixNegativeDouble.Instance;
        MessagePackBinary.readNextDecoders[index] = ReadNext1.Instance;
      }
      for (int index = 0; index <= (int) sbyte.MaxValue; ++index)
      {
        MessagePackBinary.byteDecoders[index] = FixByte.Instance;
        MessagePackBinary.sbyteDecoders[index] = FixSByte.Instance;
        MessagePackBinary.int16Decoders[index] = FixInt16.Instance;
        MessagePackBinary.int32Decoders[index] = FixInt32.Instance;
        MessagePackBinary.int64Decoders[index] = FixInt64.Instance;
        MessagePackBinary.uint16Decoders[index] = FixUInt16.Instance;
        MessagePackBinary.uint32Decoders[index] = FixUInt32.Instance;
        MessagePackBinary.uint64Decoders[index] = FixUInt64.Instance;
        MessagePackBinary.singleDecoders[index] = FixFloat.Instance;
        MessagePackBinary.doubleDecoders[index] = FixDouble.Instance;
        MessagePackBinary.readNextDecoders[index] = ReadNext1.Instance;
      }
      MessagePackBinary.byteDecoders[204] = UInt8Byte.Instance;
      MessagePackBinary.sbyteDecoders[208] = Int8SByte.Instance;
      MessagePackBinary.int16Decoders[204] = UInt8Int16.Instance;
      MessagePackBinary.int16Decoders[205] = UInt16Int16.Instance;
      MessagePackBinary.int16Decoders[208] = Int8Int16.Instance;
      MessagePackBinary.int16Decoders[209] = Int16Int16.Instance;
      MessagePackBinary.int32Decoders[204] = UInt8Int32.Instance;
      MessagePackBinary.int32Decoders[205] = UInt16Int32.Instance;
      MessagePackBinary.int32Decoders[206] = UInt32Int32.Instance;
      MessagePackBinary.int32Decoders[208] = Int8Int32.Instance;
      MessagePackBinary.int32Decoders[209] = Int16Int32.Instance;
      MessagePackBinary.int32Decoders[210] = Int32Int32.Instance;
      MessagePackBinary.int64Decoders[204] = UInt8Int64.Instance;
      MessagePackBinary.int64Decoders[205] = UInt16Int64.Instance;
      MessagePackBinary.int64Decoders[206] = UInt32Int64.Instance;
      MessagePackBinary.int64Decoders[207] = UInt64Int64.Instance;
      MessagePackBinary.int64Decoders[208] = Int8Int64.Instance;
      MessagePackBinary.int64Decoders[209] = Int16Int64.Instance;
      MessagePackBinary.int64Decoders[210] = Int32Int64.Instance;
      MessagePackBinary.int64Decoders[211] = Int64Int64.Instance;
      MessagePackBinary.uint16Decoders[204] = UInt8UInt16.Instance;
      MessagePackBinary.uint16Decoders[205] = UInt16UInt16.Instance;
      MessagePackBinary.uint32Decoders[204] = UInt8UInt32.Instance;
      MessagePackBinary.uint32Decoders[205] = UInt16UInt32.Instance;
      MessagePackBinary.uint32Decoders[206] = UInt32UInt32.Instance;
      MessagePackBinary.uint64Decoders[204] = UInt8UInt64.Instance;
      MessagePackBinary.uint64Decoders[205] = UInt16UInt64.Instance;
      MessagePackBinary.uint64Decoders[206] = UInt32UInt64.Instance;
      MessagePackBinary.uint64Decoders[207] = UInt64UInt64.Instance;
      MessagePackBinary.singleDecoders[202] = Float32Single.Instance;
      MessagePackBinary.singleDecoders[208] = Int8Single.Instance;
      MessagePackBinary.singleDecoders[209] = Int16Single.Instance;
      MessagePackBinary.singleDecoders[210] = Int32Single.Instance;
      MessagePackBinary.singleDecoders[211] = Int64Single.Instance;
      MessagePackBinary.singleDecoders[204] = UInt8Single.Instance;
      MessagePackBinary.singleDecoders[205] = UInt16Single.Instance;
      MessagePackBinary.singleDecoders[206] = UInt32Single.Instance;
      MessagePackBinary.singleDecoders[207] = UInt64Single.Instance;
      MessagePackBinary.doubleDecoders[202] = Float32Double.Instance;
      MessagePackBinary.doubleDecoders[203] = Float64Double.Instance;
      MessagePackBinary.doubleDecoders[208] = Int8Double.Instance;
      MessagePackBinary.doubleDecoders[209] = Int16Double.Instance;
      MessagePackBinary.doubleDecoders[210] = Int32Double.Instance;
      MessagePackBinary.doubleDecoders[211] = Int64Double.Instance;
      MessagePackBinary.doubleDecoders[204] = UInt8Double.Instance;
      MessagePackBinary.doubleDecoders[205] = UInt16Double.Instance;
      MessagePackBinary.doubleDecoders[206] = UInt32Double.Instance;
      MessagePackBinary.doubleDecoders[207] = UInt64Double.Instance;
      MessagePackBinary.readNextDecoders[208] = ReadNext2.Instance;
      MessagePackBinary.readNextDecoders[209] = ReadNext3.Instance;
      MessagePackBinary.readNextDecoders[210] = ReadNext5.Instance;
      MessagePackBinary.readNextDecoders[211] = ReadNext9.Instance;
      MessagePackBinary.readNextDecoders[204] = ReadNext2.Instance;
      MessagePackBinary.readNextDecoders[205] = ReadNext3.Instance;
      MessagePackBinary.readNextDecoders[206] = ReadNext5.Instance;
      MessagePackBinary.readNextDecoders[207] = ReadNext9.Instance;
      MessagePackBinary.readNextDecoders[202] = ReadNext5.Instance;
      MessagePackBinary.readNextDecoders[203] = ReadNext9.Instance;
      for (int index = 128; index <= 143; ++index)
      {
        MessagePackBinary.mapHeaderDecoders[index] = FixMapHeader.Instance;
        MessagePackBinary.readNextDecoders[index] = ReadNext1.Instance;
      }
      MessagePackBinary.mapHeaderDecoders[222] = Map16Header.Instance;
      MessagePackBinary.mapHeaderDecoders[223] = Map32Header.Instance;
      MessagePackBinary.readNextDecoders[222] = ReadNextMap.Instance;
      MessagePackBinary.readNextDecoders[223] = ReadNextMap.Instance;
      for (int index = 144; index <= 159; ++index)
      {
        MessagePackBinary.arrayHeaderDecoders[index] = FixArrayHeader.Instance;
        MessagePackBinary.readNextDecoders[index] = ReadNext1.Instance;
      }
      MessagePackBinary.arrayHeaderDecoders[220] = Array16Header.Instance;
      MessagePackBinary.arrayHeaderDecoders[221] = Array32Header.Instance;
      MessagePackBinary.readNextDecoders[220] = ReadNextArray.Instance;
      MessagePackBinary.readNextDecoders[221] = ReadNextArray.Instance;
      for (int index = 160; index <= 191; ++index)
      {
        MessagePackBinary.stringDecoders[index] = FixString.Instance;
        MessagePackBinary.stringSegmentDecoders[index] = FixStringSegment.Instance;
        MessagePackBinary.readNextDecoders[index] = ReadNextFixStr.Instance;
      }
      MessagePackBinary.stringDecoders[217] = Str8String.Instance;
      MessagePackBinary.stringDecoders[218] = Str16String.Instance;
      MessagePackBinary.stringDecoders[219] = Str32String.Instance;
      MessagePackBinary.stringSegmentDecoders[217] = Str8StringSegment.Instance;
      MessagePackBinary.stringSegmentDecoders[218] = Str16StringSegment.Instance;
      MessagePackBinary.stringSegmentDecoders[219] = Str32StringSegment.Instance;
      MessagePackBinary.readNextDecoders[217] = ReadNextStr8.Instance;
      MessagePackBinary.readNextDecoders[218] = ReadNextStr16.Instance;
      MessagePackBinary.readNextDecoders[219] = ReadNextStr32.Instance;
      MessagePackBinary.stringDecoders[192] = NilString.Instance;
      MessagePackBinary.stringSegmentDecoders[192] = NilStringSegment.Instance;
      MessagePackBinary.bytesDecoders[192] = NilBytes.Instance;
      MessagePackBinary.bytesSegmentDecoders[192] = NilBytesSegment.Instance;
      MessagePackBinary.readNextDecoders[192] = ReadNext1.Instance;
      MessagePackBinary.booleanDecoders[194] = False.Instance;
      MessagePackBinary.booleanDecoders[195] = True.Instance;
      MessagePackBinary.readNextDecoders[194] = ReadNext1.Instance;
      MessagePackBinary.readNextDecoders[195] = ReadNext1.Instance;
      MessagePackBinary.bytesDecoders[196] = Bin8Bytes.Instance;
      MessagePackBinary.bytesDecoders[197] = Bin16Bytes.Instance;
      MessagePackBinary.bytesDecoders[198] = Bin32Bytes.Instance;
      MessagePackBinary.bytesSegmentDecoders[196] = Bin8BytesSegment.Instance;
      MessagePackBinary.bytesSegmentDecoders[197] = Bin16BytesSegment.Instance;
      MessagePackBinary.bytesSegmentDecoders[198] = Bin32BytesSegment.Instance;
      MessagePackBinary.readNextDecoders[196] = ReadNextBin8.Instance;
      MessagePackBinary.readNextDecoders[197] = ReadNextBin16.Instance;
      MessagePackBinary.readNextDecoders[198] = ReadNextBin32.Instance;
      MessagePackBinary.extDecoders[212] = FixExt1.Instance;
      MessagePackBinary.extDecoders[213] = FixExt2.Instance;
      MessagePackBinary.extDecoders[214] = FixExt4.Instance;
      MessagePackBinary.extDecoders[215] = FixExt8.Instance;
      MessagePackBinary.extDecoders[216] = FixExt16.Instance;
      MessagePackBinary.extDecoders[199] = Ext8.Instance;
      MessagePackBinary.extDecoders[200] = Ext16.Instance;
      MessagePackBinary.extDecoders[201] = Ext32.Instance;
      MessagePackBinary.extHeaderDecoders[212] = FixExt1Header.Instance;
      MessagePackBinary.extHeaderDecoders[213] = FixExt2Header.Instance;
      MessagePackBinary.extHeaderDecoders[214] = FixExt4Header.Instance;
      MessagePackBinary.extHeaderDecoders[215] = FixExt8Header.Instance;
      MessagePackBinary.extHeaderDecoders[216] = FixExt16Header.Instance;
      MessagePackBinary.extHeaderDecoders[199] = Ext8Header.Instance;
      MessagePackBinary.extHeaderDecoders[200] = Ext16Header.Instance;
      MessagePackBinary.extHeaderDecoders[201] = Ext32Header.Instance;
      MessagePackBinary.readNextDecoders[212] = ReadNext3.Instance;
      MessagePackBinary.readNextDecoders[213] = ReadNext4.Instance;
      MessagePackBinary.readNextDecoders[214] = ReadNext6.Instance;
      MessagePackBinary.readNextDecoders[215] = ReadNext10.Instance;
      MessagePackBinary.readNextDecoders[216] = ReadNext18.Instance;
      MessagePackBinary.readNextDecoders[199] = ReadNextExt8.Instance;
      MessagePackBinary.readNextDecoders[200] = ReadNextExt16.Instance;
      MessagePackBinary.readNextDecoders[201] = ReadNextExt32.Instance;
      MessagePackBinary.dateTimeDecoders[214] = FixExt4DateTime.Instance;
      MessagePackBinary.dateTimeDecoders[215] = FixExt8DateTime.Instance;
      MessagePackBinary.dateTimeDecoders[199] = Ext8DateTime.Instance;
    }

    public static void EnsureCapacity(ref byte[] bytes, int offset, int appendLength)
    {
      int length1 = offset + appendLength;
      if (bytes == null)
      {
        bytes = new byte[length1];
      }
      else
      {
        int length2 = bytes.Length;
        if (length1 <= length2)
          return;
        int newSize1 = length1;
        if (newSize1 < 256)
        {
          int newSize2 = 256;
          MessagePackBinary.FastResize(ref bytes, newSize2);
        }
        else
        {
          if (length2 == 2147483591)
            throw new InvalidOperationException("byte[] size reached maximum size of array(0x7FFFFFC7), can not write to single byte[]. Details: https://msdn.microsoft.com/en-us/library/system.array");
          int num = length2 * 2;
          if (num < 0)
            newSize1 = 2147483591;
          else if (newSize1 < num)
            newSize1 = num;
          MessagePackBinary.FastResize(ref bytes, newSize1);
        }
      }
    }

    public static void FastResize(ref byte[] array, int newSize)
    {
      if (newSize < 0)
        throw new ArgumentOutOfRangeException(nameof (newSize));
      byte[] src = array;
      if (src == null)
      {
        array = new byte[newSize];
      }
      else
      {
        if (src.Length == newSize)
          return;
        byte[] dst = new byte[newSize];
        Buffer.BlockCopy((Array) src, 0, (Array) dst, 0, src.Length <= newSize ? src.Length : newSize);
        array = dst;
      }
    }

    public static byte[] FastCloneWithResize(byte[] array, int newSize)
    {
      if (newSize < 0)
        throw new ArgumentOutOfRangeException(nameof (newSize));
      byte[] src = array;
      if (src == null)
      {
        array = new byte[newSize];
        return array;
      }
      byte[] dst = new byte[newSize];
      Buffer.BlockCopy((Array) src, 0, (Array) dst, 0, src.Length <= newSize ? src.Length : newSize);
      return dst;
    }

    public static MessagePackType GetMessagePackType(byte[] bytes, int offset)
    {
      return MessagePackCode.ToMessagePackType(bytes[offset]);
    }

    public static int ReadNext(byte[] bytes, int offset)
    {
      return MessagePackBinary.readNextDecoders[(int) bytes[offset]].Read(bytes, offset);
    }

    public static int ReadNextBlock(byte[] bytes, int offset)
    {
      switch (MessagePackBinary.GetMessagePackType(bytes, offset))
      {
        case MessagePackType.Array:
          int num1 = offset;
          int readSize1;
          int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize1);
          offset += readSize1;
          for (int index = 0; index < num2; ++index)
            offset += MessagePackBinary.ReadNextBlock(bytes, offset);
          return offset - num1;
        case MessagePackType.Map:
          int num3 = offset;
          int readSize2;
          int num4 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize2);
          offset += readSize2;
          for (int index = 0; index < num4; ++index)
          {
            offset += MessagePackBinary.ReadNextBlock(bytes, offset);
            offset += MessagePackBinary.ReadNextBlock(bytes, offset);
          }
          return offset - num3;
        default:
          return MessagePackBinary.ReadNext(bytes, offset);
      }
    }

    public static int WriteNil(ref byte[] bytes, int offset)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
      bytes[offset] = (byte) 192;
      return 1;
    }

    public static Nil ReadNil(byte[] bytes, int offset, out int readSize)
    {
      if (bytes[offset] != (byte) 192)
        throw new InvalidOperationException(string.Format("code is invalid. code:{0} format:{1}", (object) bytes[offset], (object) MessagePackCode.ToFormatName(bytes[offset])));
      readSize = 1;
      return Nil.Default;
    }

    public static bool IsNil(byte[] bytes, int offset) => bytes[offset] == (byte) 192;

    public static int WriteRaw(ref byte[] bytes, int offset, byte[] rawMessagePackBlock)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, rawMessagePackBlock.Length);
      Buffer.BlockCopy((Array) rawMessagePackBlock, 0, (Array) bytes, offset, rawMessagePackBlock.Length);
      return rawMessagePackBlock.Length;
    }

    public static int WriteFixedMapHeaderUnsafe(ref byte[] bytes, int offset, int count)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
      bytes[offset] = (byte) (128 | count);
      return 1;
    }

    public static int WriteMapHeader(ref byte[] bytes, int offset, int count)
    {
      return MessagePackBinary.WriteMapHeader(ref bytes, offset, checked ((uint) count));
    }

    public static int WriteMapHeader(ref byte[] bytes, int offset, uint count)
    {
      if (count <= 15U)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
        bytes[offset] = (byte) (128U | count);
        return 1;
      }
      if (count <= (uint) ushort.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
        bytes[offset] = (byte) 222;
        bytes[offset + 1] = (byte) (count >> 8);
        bytes[offset + 2] = (byte) count;
        return 3;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
      bytes[offset] = (byte) 223;
      bytes[offset + 1] = (byte) (count >> 24);
      bytes[offset + 2] = (byte) (count >> 16);
      bytes[offset + 3] = (byte) (count >> 8);
      bytes[offset + 4] = (byte) count;
      return 5;
    }

    public static int WriteMapHeaderForceMap32Block(ref byte[] bytes, int offset, uint count)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
      bytes[offset] = (byte) 223;
      bytes[offset + 1] = (byte) (count >> 24);
      bytes[offset + 2] = (byte) (count >> 16);
      bytes[offset + 3] = (byte) (count >> 8);
      bytes[offset + 4] = (byte) count;
      return 5;
    }

    public static int ReadMapHeader(byte[] bytes, int offset, out int readSize)
    {
      return checked ((int) MessagePackBinary.mapHeaderDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize));
    }

    public static uint ReadMapHeaderRaw(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.mapHeaderDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int GetArrayHeaderLength(int count)
    {
      if (count <= 15)
        return 1;
      return count <= (int) ushort.MaxValue ? 3 : 5;
    }

    public static int WriteFixedArrayHeaderUnsafe(ref byte[] bytes, int offset, int count)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
      bytes[offset] = (byte) (144 | count);
      return 1;
    }

    public static int WriteArrayHeader(ref byte[] bytes, int offset, int count)
    {
      return MessagePackBinary.WriteArrayHeader(ref bytes, offset, checked ((uint) count));
    }

    public static int WriteArrayHeader(ref byte[] bytes, int offset, uint count)
    {
      if (count <= 15U)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
        bytes[offset] = (byte) (144U | count);
        return 1;
      }
      if (count <= (uint) ushort.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
        bytes[offset] = (byte) 220;
        bytes[offset + 1] = (byte) (count >> 8);
        bytes[offset + 2] = (byte) count;
        return 3;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
      bytes[offset] = (byte) 221;
      bytes[offset + 1] = (byte) (count >> 24);
      bytes[offset + 2] = (byte) (count >> 16);
      bytes[offset + 3] = (byte) (count >> 8);
      bytes[offset + 4] = (byte) count;
      return 5;
    }

    public static int WriteArrayHeaderForceArray32Block(ref byte[] bytes, int offset, uint count)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
      bytes[offset] = (byte) 221;
      bytes[offset + 1] = (byte) (count >> 24);
      bytes[offset + 2] = (byte) (count >> 16);
      bytes[offset + 3] = (byte) (count >> 8);
      bytes[offset + 4] = (byte) count;
      return 5;
    }

    public static int ReadArrayHeader(byte[] bytes, int offset, out int readSize)
    {
      return checked ((int) MessagePackBinary.arrayHeaderDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize));
    }

    public static uint ReadArrayHeaderRaw(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.arrayHeaderDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteBoolean(ref byte[] bytes, int offset, bool value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
      bytes[offset] = !value ? (byte) 194 : (byte) 195;
      return 1;
    }

    public static bool ReadBoolean(byte[] bytes, int offset, out int readSize)
    {
      readSize = 1;
      return MessagePackBinary.booleanDecoders[(int) bytes[offset]].Read();
    }

    public static int WriteByte(ref byte[] bytes, int offset, byte value)
    {
      if (value <= (byte) 127)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
        bytes[offset] = value;
        return 1;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
      bytes[offset] = (byte) 204;
      bytes[offset + 1] = value;
      return 2;
    }

    public static int WriteByteForceByteBlock(ref byte[] bytes, int offset, byte value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
      bytes[offset] = (byte) 204;
      bytes[offset + 1] = value;
      return 2;
    }

    public static byte ReadByte(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.byteDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteBytes(ref byte[] bytes, int offset, byte[] value)
    {
      return value == null ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteBytes(ref bytes, offset, value, 0, value.Length);
    }

    public static int WriteBytes(
      ref byte[] dest,
      int dstOffset,
      byte[] src,
      int srcOffset,
      int count)
    {
      if (src == null)
        return MessagePackBinary.WriteNil(ref dest, dstOffset);
      if (count <= (int) byte.MaxValue)
      {
        int appendLength = count + 2;
        MessagePackBinary.EnsureCapacity(ref dest, dstOffset, appendLength);
        dest[dstOffset] = (byte) 196;
        dest[dstOffset + 1] = (byte) count;
        Buffer.BlockCopy((Array) src, srcOffset, (Array) dest, dstOffset + 2, count);
        return appendLength;
      }
      if (count <= (int) ushort.MaxValue)
      {
        int appendLength = count + 3;
        MessagePackBinary.EnsureCapacity(ref dest, dstOffset, appendLength);
        dest[dstOffset] = (byte) 197;
        dest[dstOffset + 1] = (byte) (count >> 8);
        dest[dstOffset + 2] = (byte) count;
        Buffer.BlockCopy((Array) src, srcOffset, (Array) dest, dstOffset + 3, count);
        return appendLength;
      }
      int appendLength1 = count + 5;
      MessagePackBinary.EnsureCapacity(ref dest, dstOffset, appendLength1);
      dest[dstOffset] = (byte) 198;
      dest[dstOffset + 1] = (byte) (count >> 24);
      dest[dstOffset + 2] = (byte) (count >> 16);
      dest[dstOffset + 3] = (byte) (count >> 8);
      dest[dstOffset + 4] = (byte) count;
      Buffer.BlockCopy((Array) src, srcOffset, (Array) dest, dstOffset + 5, count);
      return appendLength1;
    }

    public static byte[] ReadBytes(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.bytesDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static ArraySegment<byte> ReadBytesSegment(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.bytesSegmentDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteSByte(ref byte[] bytes, int offset, sbyte value)
    {
      if (value < (sbyte) -32)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
        bytes[offset] = (byte) 208;
        bytes[offset + 1] = (byte) value;
        return 2;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
      bytes[offset] = (byte) value;
      return 1;
    }

    public static int WriteSByteForceSByteBlock(ref byte[] bytes, int offset, sbyte value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
      bytes[offset] = (byte) 208;
      bytes[offset + 1] = (byte) value;
      return 2;
    }

    public static sbyte ReadSByte(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.sbyteDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteSingle(ref byte[] bytes, int offset, float value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
      bytes[offset] = (byte) 202;
      Float32Bits float32Bits = new Float32Bits(value);
      if (BitConverter.IsLittleEndian)
      {
        bytes[offset + 1] = float32Bits.Byte3;
        bytes[offset + 2] = float32Bits.Byte2;
        bytes[offset + 3] = float32Bits.Byte1;
        bytes[offset + 4] = float32Bits.Byte0;
      }
      else
      {
        bytes[offset + 1] = float32Bits.Byte0;
        bytes[offset + 2] = float32Bits.Byte1;
        bytes[offset + 3] = float32Bits.Byte2;
        bytes[offset + 4] = float32Bits.Byte3;
      }
      return 5;
    }

    public static float ReadSingle(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.singleDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteDouble(ref byte[] bytes, int offset, double value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 9);
      bytes[offset] = (byte) 203;
      Float64Bits float64Bits = new Float64Bits(value);
      if (BitConverter.IsLittleEndian)
      {
        bytes[offset + 1] = float64Bits.Byte7;
        bytes[offset + 2] = float64Bits.Byte6;
        bytes[offset + 3] = float64Bits.Byte5;
        bytes[offset + 4] = float64Bits.Byte4;
        bytes[offset + 5] = float64Bits.Byte3;
        bytes[offset + 6] = float64Bits.Byte2;
        bytes[offset + 7] = float64Bits.Byte1;
        bytes[offset + 8] = float64Bits.Byte0;
      }
      else
      {
        bytes[offset + 1] = float64Bits.Byte0;
        bytes[offset + 2] = float64Bits.Byte1;
        bytes[offset + 3] = float64Bits.Byte2;
        bytes[offset + 4] = float64Bits.Byte3;
        bytes[offset + 5] = float64Bits.Byte4;
        bytes[offset + 6] = float64Bits.Byte5;
        bytes[offset + 7] = float64Bits.Byte6;
        bytes[offset + 8] = float64Bits.Byte7;
      }
      return 9;
    }

    public static double ReadDouble(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.doubleDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteInt16(ref byte[] bytes, int offset, short value)
    {
      if (value >= (short) 0)
      {
        if (value <= (short) sbyte.MaxValue)
        {
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
          bytes[offset] = (byte) value;
          return 1;
        }
        if (value <= (short) byte.MaxValue)
        {
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
          bytes[offset] = (byte) 204;
          bytes[offset + 1] = (byte) value;
          return 2;
        }
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
        bytes[offset] = (byte) 205;
        bytes[offset + 1] = (byte) ((uint) value >> 8);
        bytes[offset + 2] = (byte) value;
        return 3;
      }
      if ((short) -32 <= value)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
        bytes[offset] = (byte) value;
        return 1;
      }
      if ((short) sbyte.MinValue <= value)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
        bytes[offset] = (byte) 208;
        bytes[offset + 1] = (byte) value;
        return 2;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
      bytes[offset] = (byte) 209;
      bytes[offset + 1] = (byte) ((uint) value >> 8);
      bytes[offset + 2] = (byte) value;
      return 3;
    }

    public static int WriteInt16ForceInt16Block(ref byte[] bytes, int offset, short value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
      bytes[offset] = (byte) 209;
      bytes[offset + 1] = (byte) ((uint) value >> 8);
      bytes[offset + 2] = (byte) value;
      return 3;
    }

    public static short ReadInt16(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.int16Decoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WritePositiveFixedIntUnsafe(ref byte[] bytes, int offset, int value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
      bytes[offset] = (byte) value;
      return 1;
    }

    public static int WriteInt32(ref byte[] bytes, int offset, int value)
    {
      if (value >= 0)
      {
        if (value <= (int) sbyte.MaxValue)
        {
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
          bytes[offset] = (byte) value;
          return 1;
        }
        if (value <= (int) byte.MaxValue)
        {
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
          bytes[offset] = (byte) 204;
          bytes[offset + 1] = (byte) value;
          return 2;
        }
        if (value <= (int) ushort.MaxValue)
        {
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
          bytes[offset] = (byte) 205;
          bytes[offset + 1] = (byte) (value >> 8);
          bytes[offset + 2] = (byte) value;
          return 3;
        }
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
        bytes[offset] = (byte) 206;
        bytes[offset + 1] = (byte) (value >> 24);
        bytes[offset + 2] = (byte) (value >> 16);
        bytes[offset + 3] = (byte) (value >> 8);
        bytes[offset + 4] = (byte) value;
        return 5;
      }
      if (-32 <= value)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
        bytes[offset] = (byte) value;
        return 1;
      }
      if ((int) sbyte.MinValue <= value)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
        bytes[offset] = (byte) 208;
        bytes[offset + 1] = (byte) value;
        return 2;
      }
      if ((int) short.MinValue <= value)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
        bytes[offset] = (byte) 209;
        bytes[offset + 1] = (byte) (value >> 8);
        bytes[offset + 2] = (byte) value;
        return 3;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
      bytes[offset] = (byte) 210;
      bytes[offset + 1] = (byte) (value >> 24);
      bytes[offset + 2] = (byte) (value >> 16);
      bytes[offset + 3] = (byte) (value >> 8);
      bytes[offset + 4] = (byte) value;
      return 5;
    }

    public static int WriteInt32ForceInt32Block(ref byte[] bytes, int offset, int value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
      bytes[offset] = (byte) 210;
      bytes[offset + 1] = (byte) (value >> 24);
      bytes[offset + 2] = (byte) (value >> 16);
      bytes[offset + 3] = (byte) (value >> 8);
      bytes[offset + 4] = (byte) value;
      return 5;
    }

    public static int ReadInt32(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.int32Decoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteInt64(ref byte[] bytes, int offset, long value)
    {
      if (value >= 0L)
      {
        if (value <= (long) sbyte.MaxValue)
        {
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
          bytes[offset] = (byte) value;
          return 1;
        }
        if (value <= (long) byte.MaxValue)
        {
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
          bytes[offset] = (byte) 204;
          bytes[offset + 1] = (byte) value;
          return 2;
        }
        if (value <= (long) ushort.MaxValue)
        {
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
          bytes[offset] = (byte) 205;
          bytes[offset + 1] = (byte) (value >> 8);
          bytes[offset + 2] = (byte) value;
          return 3;
        }
        if (value <= (long) uint.MaxValue)
        {
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
          bytes[offset] = (byte) 206;
          bytes[offset + 1] = (byte) (value >> 24);
          bytes[offset + 2] = (byte) (value >> 16);
          bytes[offset + 3] = (byte) (value >> 8);
          bytes[offset + 4] = (byte) value;
          return 5;
        }
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 9);
        bytes[offset] = (byte) 207;
        bytes[offset + 1] = (byte) (value >> 56);
        bytes[offset + 2] = (byte) (value >> 48);
        bytes[offset + 3] = (byte) (value >> 40);
        bytes[offset + 4] = (byte) (value >> 32);
        bytes[offset + 5] = (byte) (value >> 24);
        bytes[offset + 6] = (byte) (value >> 16);
        bytes[offset + 7] = (byte) (value >> 8);
        bytes[offset + 8] = (byte) value;
        return 9;
      }
      if (-32L <= value)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
        bytes[offset] = (byte) value;
        return 1;
      }
      if ((long) sbyte.MinValue <= value)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
        bytes[offset] = (byte) 208;
        bytes[offset + 1] = (byte) value;
        return 2;
      }
      if ((long) short.MinValue <= value)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
        bytes[offset] = (byte) 209;
        bytes[offset + 1] = (byte) (value >> 8);
        bytes[offset + 2] = (byte) value;
        return 3;
      }
      if ((long) int.MinValue <= value)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
        bytes[offset] = (byte) 210;
        bytes[offset + 1] = (byte) (value >> 24);
        bytes[offset + 2] = (byte) (value >> 16);
        bytes[offset + 3] = (byte) (value >> 8);
        bytes[offset + 4] = (byte) value;
        return 5;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 9);
      bytes[offset] = (byte) 211;
      bytes[offset + 1] = (byte) (value >> 56);
      bytes[offset + 2] = (byte) (value >> 48);
      bytes[offset + 3] = (byte) (value >> 40);
      bytes[offset + 4] = (byte) (value >> 32);
      bytes[offset + 5] = (byte) (value >> 24);
      bytes[offset + 6] = (byte) (value >> 16);
      bytes[offset + 7] = (byte) (value >> 8);
      bytes[offset + 8] = (byte) value;
      return 9;
    }

    public static int WriteInt64ForceInt64Block(ref byte[] bytes, int offset, long value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 9);
      bytes[offset] = (byte) 211;
      bytes[offset + 1] = (byte) (value >> 56);
      bytes[offset + 2] = (byte) (value >> 48);
      bytes[offset + 3] = (byte) (value >> 40);
      bytes[offset + 4] = (byte) (value >> 32);
      bytes[offset + 5] = (byte) (value >> 24);
      bytes[offset + 6] = (byte) (value >> 16);
      bytes[offset + 7] = (byte) (value >> 8);
      bytes[offset + 8] = (byte) value;
      return 9;
    }

    public static long ReadInt64(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.int64Decoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteUInt16(ref byte[] bytes, int offset, ushort value)
    {
      if (value <= (ushort) sbyte.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
        bytes[offset] = (byte) value;
        return 1;
      }
      if (value <= (ushort) byte.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
        bytes[offset] = (byte) 204;
        bytes[offset + 1] = (byte) value;
        return 2;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
      bytes[offset] = (byte) 205;
      bytes[offset + 1] = (byte) ((uint) value >> 8);
      bytes[offset + 2] = (byte) value;
      return 3;
    }

    public static int WriteUInt16ForceUInt16Block(ref byte[] bytes, int offset, ushort value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
      bytes[offset] = (byte) 205;
      bytes[offset + 1] = (byte) ((uint) value >> 8);
      bytes[offset + 2] = (byte) value;
      return 3;
    }

    public static ushort ReadUInt16(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.uint16Decoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteUInt32(ref byte[] bytes, int offset, uint value)
    {
      if (value <= (uint) sbyte.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
        bytes[offset] = (byte) value;
        return 1;
      }
      if (value <= (uint) byte.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
        bytes[offset] = (byte) 204;
        bytes[offset + 1] = (byte) value;
        return 2;
      }
      if (value <= (uint) ushort.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
        bytes[offset] = (byte) 205;
        bytes[offset + 1] = (byte) (value >> 8);
        bytes[offset + 2] = (byte) value;
        return 3;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
      bytes[offset] = (byte) 206;
      bytes[offset + 1] = (byte) (value >> 24);
      bytes[offset + 2] = (byte) (value >> 16);
      bytes[offset + 3] = (byte) (value >> 8);
      bytes[offset + 4] = (byte) value;
      return 5;
    }

    public static int WriteUInt32ForceUInt32Block(ref byte[] bytes, int offset, uint value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
      bytes[offset] = (byte) 206;
      bytes[offset + 1] = (byte) (value >> 24);
      bytes[offset + 2] = (byte) (value >> 16);
      bytes[offset + 3] = (byte) (value >> 8);
      bytes[offset + 4] = (byte) value;
      return 5;
    }

    public static uint ReadUInt32(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.uint32Decoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteUInt64(ref byte[] bytes, int offset, ulong value)
    {
      if (value <= (ulong) sbyte.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
        bytes[offset] = (byte) value;
        return 1;
      }
      if (value <= (ulong) byte.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
        bytes[offset] = (byte) 204;
        bytes[offset + 1] = (byte) value;
        return 2;
      }
      if (value <= (ulong) ushort.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
        bytes[offset] = (byte) 205;
        bytes[offset + 1] = (byte) (value >> 8);
        bytes[offset + 2] = (byte) value;
        return 3;
      }
      if (value <= (ulong) uint.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
        bytes[offset] = (byte) 206;
        bytes[offset + 1] = (byte) (value >> 24);
        bytes[offset + 2] = (byte) (value >> 16);
        bytes[offset + 3] = (byte) (value >> 8);
        bytes[offset + 4] = (byte) value;
        return 5;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 9);
      bytes[offset] = (byte) 207;
      bytes[offset + 1] = (byte) (value >> 56);
      bytes[offset + 2] = (byte) (value >> 48);
      bytes[offset + 3] = (byte) (value >> 40);
      bytes[offset + 4] = (byte) (value >> 32);
      bytes[offset + 5] = (byte) (value >> 24);
      bytes[offset + 6] = (byte) (value >> 16);
      bytes[offset + 7] = (byte) (value >> 8);
      bytes[offset + 8] = (byte) value;
      return 9;
    }

    public static int WriteUInt64ForceUInt64Block(ref byte[] bytes, int offset, ulong value)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 9);
      bytes[offset] = (byte) 207;
      bytes[offset + 1] = (byte) (value >> 56);
      bytes[offset + 2] = (byte) (value >> 48);
      bytes[offset + 3] = (byte) (value >> 40);
      bytes[offset + 4] = (byte) (value >> 32);
      bytes[offset + 5] = (byte) (value >> 24);
      bytes[offset + 6] = (byte) (value >> 16);
      bytes[offset + 7] = (byte) (value >> 8);
      bytes[offset + 8] = (byte) value;
      return 9;
    }

    public static ulong ReadUInt64(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.uint64Decoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteChar(ref byte[] bytes, int offset, char value)
    {
      return MessagePackBinary.WriteUInt16(ref bytes, offset, (ushort) value);
    }

    public static char ReadChar(byte[] bytes, int offset, out int readSize)
    {
      return (char) MessagePackBinary.ReadUInt16(bytes, offset, out readSize);
    }

    public static int WriteFixedStringUnsafe(
      ref byte[] bytes,
      int offset,
      string value,
      int byteCount)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, byteCount + 1);
      bytes[offset] = (byte) (160 | byteCount);
      StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, offset + 1);
      return byteCount + 1;
    }

    public static int WriteStringUnsafe(ref byte[] bytes, int offset, string value, int byteCount)
    {
      if (byteCount <= 31)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, byteCount + 1);
        bytes[offset] = (byte) (160 | byteCount);
        StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, offset + 1);
        return byteCount + 1;
      }
      if (byteCount <= (int) byte.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, byteCount + 2);
        bytes[offset] = (byte) 217;
        bytes[offset + 1] = (byte) byteCount;
        StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, offset + 2);
        return byteCount + 2;
      }
      if (byteCount <= (int) ushort.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, byteCount + 3);
        bytes[offset] = (byte) 218;
        bytes[offset + 1] = (byte) (byteCount >> 8);
        bytes[offset + 2] = (byte) byteCount;
        StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, offset + 3);
        return byteCount + 3;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, byteCount + 5);
      bytes[offset] = (byte) 219;
      bytes[offset + 1] = (byte) (byteCount >> 24);
      bytes[offset + 2] = (byte) (byteCount >> 16);
      bytes[offset + 3] = (byte) (byteCount >> 8);
      bytes[offset + 4] = (byte) byteCount;
      StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, offset + 5);
      return byteCount + 5;
    }

    public static int WriteStringBytes(ref byte[] bytes, int offset, byte[] utf8stringBytes)
    {
      int length = utf8stringBytes.Length;
      if (length <= 31)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 1);
        bytes[offset] = (byte) (160 | length);
        Buffer.BlockCopy((Array) utf8stringBytes, 0, (Array) bytes, offset + 1, length);
        return length + 1;
      }
      if (length <= (int) byte.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 2);
        bytes[offset] = (byte) 217;
        bytes[offset + 1] = (byte) length;
        Buffer.BlockCopy((Array) utf8stringBytes, 0, (Array) bytes, offset + 2, length);
        return length + 2;
      }
      if (length <= (int) ushort.MaxValue)
      {
        MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 3);
        bytes[offset] = (byte) 218;
        bytes[offset + 1] = (byte) (length >> 8);
        bytes[offset + 2] = (byte) length;
        Buffer.BlockCopy((Array) utf8stringBytes, 0, (Array) bytes, offset + 3, length);
        return length + 3;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 5);
      bytes[offset] = (byte) 219;
      bytes[offset + 1] = (byte) (length >> 24);
      bytes[offset + 2] = (byte) (length >> 16);
      bytes[offset + 3] = (byte) (length >> 8);
      bytes[offset + 4] = (byte) length;
      Buffer.BlockCopy((Array) utf8stringBytes, 0, (Array) bytes, offset + 5, length);
      return length + 5;
    }

    public static byte[] GetEncodedStringBytes(string value)
    {
      int byteCount = StringEncoding.UTF8.GetByteCount(value);
      if (byteCount <= 31)
      {
        byte[] bytes = new byte[byteCount + 1];
        bytes[0] = (byte) (160 | byteCount);
        StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, 1);
        return bytes;
      }
      if (byteCount <= (int) byte.MaxValue)
      {
        byte[] bytes = new byte[byteCount + 2];
        bytes[0] = (byte) 217;
        bytes[1] = (byte) byteCount;
        StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, 2);
        return bytes;
      }
      if (byteCount <= (int) ushort.MaxValue)
      {
        byte[] bytes = new byte[byteCount + 3];
        bytes[0] = (byte) 218;
        bytes[1] = (byte) (byteCount >> 8);
        bytes[2] = (byte) byteCount;
        StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, 3);
        return bytes;
      }
      byte[] bytes1 = new byte[byteCount + 5];
      bytes1[0] = (byte) 219;
      bytes1[1] = (byte) (byteCount >> 24);
      bytes1[2] = (byte) (byteCount >> 16);
      bytes1[3] = (byte) (byteCount >> 8);
      bytes1[4] = (byte) byteCount;
      StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes1, 5);
      return bytes1;
    }

    public static int WriteString(ref byte[] bytes, int offset, string value)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      MessagePackBinary.EnsureCapacity(ref bytes, offset, StringEncoding.UTF8.GetMaxByteCount(value.Length) + 5);
      int num1 = value.Length > 31 ? (value.Length > (int) byte.MaxValue ? (value.Length > (int) ushort.MaxValue ? 5 : 3) : 2) : 1;
      int num2 = offset + num1;
      int bytes1 = StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, num2);
      if (bytes1 <= 31)
      {
        if (num1 != 1)
          Buffer.BlockCopy((Array) bytes, num2, (Array) bytes, offset + 1, bytes1);
        bytes[offset] = (byte) (160 | bytes1);
        return bytes1 + 1;
      }
      if (bytes1 <= (int) byte.MaxValue)
      {
        if (num1 != 2)
          Buffer.BlockCopy((Array) bytes, num2, (Array) bytes, offset + 2, bytes1);
        bytes[offset] = (byte) 217;
        bytes[offset + 1] = (byte) bytes1;
        return bytes1 + 2;
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

    public static int WriteStringForceStr32Block(ref byte[] bytes, int offset, string value)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      MessagePackBinary.EnsureCapacity(ref bytes, offset, StringEncoding.UTF8.GetMaxByteCount(value.Length) + 5);
      int bytes1 = StringEncoding.UTF8.GetBytes(value, 0, value.Length, bytes, offset + 5);
      bytes[offset] = (byte) 219;
      bytes[offset + 1] = (byte) (bytes1 >> 24);
      bytes[offset + 2] = (byte) (bytes1 >> 16);
      bytes[offset + 3] = (byte) (bytes1 >> 8);
      bytes[offset + 4] = (byte) bytes1;
      return bytes1 + 5;
    }

    public static string ReadString(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.stringDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static ArraySegment<byte> ReadStringSegment(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.stringSegmentDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int WriteExtensionFormatHeader(
      ref byte[] bytes,
      int offset,
      sbyte typeCode,
      int dataLength)
    {
      switch (dataLength)
      {
        case 1:
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
          bytes[offset] = (byte) 212;
          bytes[offset + 1] = (byte) typeCode;
          return 2;
        case 2:
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 4);
          bytes[offset] = (byte) 213;
          bytes[offset + 1] = (byte) typeCode;
          return 2;
        case 4:
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 6);
          bytes[offset] = (byte) 214;
          bytes[offset + 1] = (byte) typeCode;
          return 2;
        case 8:
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 10);
          bytes[offset] = (byte) 215;
          bytes[offset + 1] = (byte) typeCode;
          return 2;
        default:
          if (dataLength == 16)
          {
            MessagePackBinary.EnsureCapacity(ref bytes, offset, 18);
            bytes[offset] = (byte) 216;
            bytes[offset + 1] = (byte) typeCode;
            return 2;
          }
          if (dataLength <= (int) byte.MaxValue)
          {
            MessagePackBinary.EnsureCapacity(ref bytes, offset, dataLength + 3);
            bytes[offset] = (byte) 199;
            bytes[offset + 1] = (byte) dataLength;
            bytes[offset + 2] = (byte) typeCode;
            return 3;
          }
          if (dataLength <= (int) ushort.MaxValue)
          {
            MessagePackBinary.EnsureCapacity(ref bytes, offset, dataLength + 4);
            bytes[offset] = (byte) 200;
            bytes[offset + 1] = (byte) (dataLength >> 8);
            bytes[offset + 2] = (byte) dataLength;
            bytes[offset + 3] = (byte) typeCode;
            return 4;
          }
          MessagePackBinary.EnsureCapacity(ref bytes, offset, dataLength + 6);
          bytes[offset] = (byte) 201;
          bytes[offset + 1] = (byte) (dataLength >> 24);
          bytes[offset + 2] = (byte) (dataLength >> 16);
          bytes[offset + 3] = (byte) (dataLength >> 8);
          bytes[offset + 4] = (byte) dataLength;
          bytes[offset + 5] = (byte) typeCode;
          return 6;
      }
    }

    public static int WriteExtensionFormatHeaderForceExt32Block(
      ref byte[] bytes,
      int offset,
      sbyte typeCode,
      int dataLength)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, dataLength + 6);
      bytes[offset] = (byte) 201;
      bytes[offset + 1] = (byte) (dataLength >> 24);
      bytes[offset + 2] = (byte) (dataLength >> 16);
      bytes[offset + 3] = (byte) (dataLength >> 8);
      bytes[offset + 4] = (byte) dataLength;
      bytes[offset + 5] = (byte) typeCode;
      return 6;
    }

    public static int WriteExtensionFormat(
      ref byte[] bytes,
      int offset,
      sbyte typeCode,
      byte[] data)
    {
      int length = data.Length;
      switch (length)
      {
        case 1:
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
          bytes[offset] = (byte) 212;
          bytes[offset + 1] = (byte) typeCode;
          bytes[offset + 2] = data[0];
          return 3;
        case 2:
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 4);
          bytes[offset] = (byte) 213;
          bytes[offset + 1] = (byte) typeCode;
          bytes[offset + 2] = data[0];
          bytes[offset + 3] = data[1];
          return 4;
        case 4:
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 6);
          bytes[offset] = (byte) 214;
          bytes[offset + 1] = (byte) typeCode;
          bytes[offset + 2] = data[0];
          bytes[offset + 3] = data[1];
          bytes[offset + 4] = data[2];
          bytes[offset + 5] = data[3];
          return 6;
        case 8:
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 10);
          bytes[offset] = (byte) 215;
          bytes[offset + 1] = (byte) typeCode;
          bytes[offset + 2] = data[0];
          bytes[offset + 3] = data[1];
          bytes[offset + 4] = data[2];
          bytes[offset + 5] = data[3];
          bytes[offset + 6] = data[4];
          bytes[offset + 7] = data[5];
          bytes[offset + 8] = data[6];
          bytes[offset + 9] = data[7];
          return 10;
        default:
          if (length == 16)
          {
            MessagePackBinary.EnsureCapacity(ref bytes, offset, 18);
            bytes[offset] = (byte) 216;
            bytes[offset + 1] = (byte) typeCode;
            bytes[offset + 2] = data[0];
            bytes[offset + 3] = data[1];
            bytes[offset + 4] = data[2];
            bytes[offset + 5] = data[3];
            bytes[offset + 6] = data[4];
            bytes[offset + 7] = data[5];
            bytes[offset + 8] = data[6];
            bytes[offset + 9] = data[7];
            bytes[offset + 10] = data[8];
            bytes[offset + 11] = data[9];
            bytes[offset + 12] = data[10];
            bytes[offset + 13] = data[11];
            bytes[offset + 14] = data[12];
            bytes[offset + 15] = data[13];
            bytes[offset + 16] = data[14];
            bytes[offset + 17] = data[15];
            return 18;
          }
          if (data.Length <= (int) byte.MaxValue)
          {
            MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 3);
            bytes[offset] = (byte) 199;
            bytes[offset + 1] = (byte) length;
            bytes[offset + 2] = (byte) typeCode;
            Buffer.BlockCopy((Array) data, 0, (Array) bytes, offset + 3, length);
            return length + 3;
          }
          if (data.Length <= (int) ushort.MaxValue)
          {
            MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 4);
            bytes[offset] = (byte) 200;
            bytes[offset + 1] = (byte) (length >> 8);
            bytes[offset + 2] = (byte) length;
            bytes[offset + 3] = (byte) typeCode;
            Buffer.BlockCopy((Array) data, 0, (Array) bytes, offset + 4, length);
            return length + 4;
          }
          MessagePackBinary.EnsureCapacity(ref bytes, offset, length + 6);
          bytes[offset] = (byte) 201;
          bytes[offset + 1] = (byte) (length >> 24);
          bytes[offset + 2] = (byte) (length >> 16);
          bytes[offset + 3] = (byte) (length >> 8);
          bytes[offset + 4] = (byte) length;
          bytes[offset + 5] = (byte) typeCode;
          Buffer.BlockCopy((Array) data, 0, (Array) bytes, offset + 6, length);
          return length + 6;
      }
    }

    public static ExtensionResult ReadExtensionFormat(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.extDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static ExtensionHeader ReadExtensionFormatHeader(
      byte[] bytes,
      int offset,
      out int readSize)
    {
      return MessagePackBinary.extHeaderDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    public static int GetExtensionFormatHeaderLength(int dataLength)
    {
      switch (dataLength)
      {
        case 1:
        case 2:
        case 4:
        case 8:
          return 2;
        default:
          if (dataLength != 16)
          {
            if (dataLength <= (int) byte.MaxValue)
              return 3;
            return dataLength <= (int) ushort.MaxValue ? 4 : 6;
          }
          goto case 1;
      }
    }

    public static int WriteDateTime(ref byte[] bytes, int offset, DateTime dateTime)
    {
      dateTime = dateTime.ToUniversalTime();
      long num1 = dateTime.Ticks / 10000000L - 62135596800L;
      long num2 = dateTime.Ticks % 10000000L * 100L;
      if (num1 >> 34 == 0L)
      {
        ulong num3 = (ulong) (num2 << 34 | num1);
        if (((long) num3 & -4294967296L) == 0L)
        {
          uint num4 = (uint) num3;
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 6);
          bytes[offset] = (byte) 214;
          bytes[offset + 1] = byte.MaxValue;
          bytes[offset + 2] = (byte) (num4 >> 24);
          bytes[offset + 3] = (byte) (num4 >> 16);
          bytes[offset + 4] = (byte) (num4 >> 8);
          bytes[offset + 5] = (byte) num4;
          return 6;
        }
        MessagePackBinary.EnsureCapacity(ref bytes, offset, 10);
        bytes[offset] = (byte) 215;
        bytes[offset + 1] = byte.MaxValue;
        bytes[offset + 2] = (byte) (num3 >> 56);
        bytes[offset + 3] = (byte) (num3 >> 48);
        bytes[offset + 4] = (byte) (num3 >> 40);
        bytes[offset + 5] = (byte) (num3 >> 32);
        bytes[offset + 6] = (byte) (num3 >> 24);
        bytes[offset + 7] = (byte) (num3 >> 16);
        bytes[offset + 8] = (byte) (num3 >> 8);
        bytes[offset + 9] = (byte) num3;
        return 10;
      }
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 15);
      bytes[offset] = (byte) 199;
      bytes[offset + 1] = (byte) 12;
      bytes[offset + 2] = byte.MaxValue;
      bytes[offset + 3] = (byte) (num2 >> 24);
      bytes[offset + 4] = (byte) (num2 >> 16);
      bytes[offset + 5] = (byte) (num2 >> 8);
      bytes[offset + 6] = (byte) num2;
      bytes[offset + 7] = (byte) (num1 >> 56);
      bytes[offset + 8] = (byte) (num1 >> 48);
      bytes[offset + 9] = (byte) (num1 >> 40);
      bytes[offset + 10] = (byte) (num1 >> 32);
      bytes[offset + 11] = (byte) (num1 >> 24);
      bytes[offset + 12] = (byte) (num1 >> 16);
      bytes[offset + 13] = (byte) (num1 >> 8);
      bytes[offset + 14] = (byte) num1;
      return 15;
    }

    public static DateTime ReadDateTime(byte[] bytes, int offset, out int readSize)
    {
      return MessagePackBinary.dateTimeDecoders[(int) bytes[offset]].Read(bytes, offset, out readSize);
    }

    private static byte[] ReadMessageBlockFromStreamUnsafe(Stream stream)
    {
      return MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream, false, out int _);
    }

    public static byte[] ReadMessageBlockFromStreamUnsafe(
      Stream stream,
      bool readOnlySingleMessage,
      out int readSize)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      readSize = MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref buffer, 0, readOnlySingleMessage);
      return buffer;
    }

    private static int ReadMessageBlockFromStreamCore(
      Stream stream,
      ref byte[] bytes,
      int offset,
      bool readOnlySingleMessage)
    {
      int num1 = stream.ReadByte();
      byte code = num1 >= 0 && (int) byte.MaxValue >= num1 ? (byte) num1 : throw new InvalidOperationException("Invalid MessagePack code was detected, code:" + (object) num1);
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
      bytes[offset] = code;
      switch (MessagePackCode.ToMessagePackType(code))
      {
        case MessagePackType.Unknown:
        case MessagePackType.Nil:
        case MessagePackType.Boolean:
          return 1;
        case MessagePackType.Integer:
          if ((byte) 224 <= code && code <= byte.MaxValue || (byte) 0 <= code && code <= (byte) 127)
            return 1;
          int readSize1;
          switch (code)
          {
            case 204:
              readSize1 = 1;
              break;
            case 205:
              readSize1 = 2;
              break;
            case 206:
              readSize1 = 4;
              break;
            case 207:
              readSize1 = 8;
              break;
            case 208:
              readSize1 = 1;
              break;
            case 209:
              readSize1 = 2;
              break;
            case 210:
              readSize1 = 4;
              break;
            case 211:
              readSize1 = 8;
              break;
            default:
              throw new InvalidOperationException("Invalid Code");
          }
          MessagePackBinary.EnsureCapacity(ref bytes, offset, readSize1 + 1);
          MessagePackBinary.ReadFully(stream, bytes, offset + 1, readSize1);
          return readSize1 + 1;
        case MessagePackType.Float:
          if (code == (byte) 202)
          {
            MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
            MessagePackBinary.ReadFully(stream, bytes, offset + 1, 4);
            return 5;
          }
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 9);
          MessagePackBinary.ReadFully(stream, bytes, offset + 1, 8);
          return 9;
        case MessagePackType.String:
          if ((byte) 160 <= code && code <= (byte) 191)
          {
            int readSize2 = (int) bytes[offset] & 31;
            MessagePackBinary.EnsureCapacity(ref bytes, offset, 1 + readSize2);
            MessagePackBinary.ReadFully(stream, bytes, offset + 1, readSize2);
            return readSize2 + 1;
          }
          switch (code)
          {
            case 217:
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
              MessagePackBinary.ReadFully(stream, bytes, offset + 1, 1);
              byte readSize3 = bytes[offset + 1];
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 2 + (int) readSize3);
              MessagePackBinary.ReadFully(stream, bytes, offset + 2, (int) readSize3);
              return (int) readSize3 + 2;
            case 218:
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
              MessagePackBinary.ReadFully(stream, bytes, offset + 1, 2);
              int readSize4 = ((int) bytes[offset + 1] << 8) + (int) bytes[offset + 2];
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 3 + readSize4);
              MessagePackBinary.ReadFully(stream, bytes, offset + 3, readSize4);
              return readSize4 + 3;
            case 219:
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
              MessagePackBinary.ReadFully(stream, bytes, offset + 1, 4);
              int readSize5 = (int) bytes[offset + 1] << 24 | (int) bytes[offset + 2] << 16 | (int) bytes[offset + 3] << 8 | (int) bytes[offset + 4];
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 5 + readSize5);
              MessagePackBinary.ReadFully(stream, bytes, offset + 5, readSize5);
              return readSize5 + 5;
            default:
              throw new InvalidOperationException("Invalid Code");
          }
        case MessagePackType.Binary:
          switch (code)
          {
            case 196:
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
              MessagePackBinary.ReadFully(stream, bytes, offset + 1, 1);
              byte readSize6 = bytes[offset + 1];
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 2 + (int) readSize6);
              MessagePackBinary.ReadFully(stream, bytes, offset + 2, (int) readSize6);
              return (int) readSize6 + 2;
            case 197:
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 3);
              MessagePackBinary.ReadFully(stream, bytes, offset + 1, 2);
              int readSize7 = ((int) bytes[offset + 1] << 8) + (int) bytes[offset + 2];
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 3 + readSize7);
              MessagePackBinary.ReadFully(stream, bytes, offset + 3, readSize7);
              return readSize7 + 3;
            case 198:
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 5);
              MessagePackBinary.ReadFully(stream, bytes, offset + 1, 4);
              int readSize8 = (int) bytes[offset + 1] << 24 | (int) bytes[offset + 2] << 16 | (int) bytes[offset + 3] << 8 | (int) bytes[offset + 4];
              MessagePackBinary.EnsureCapacity(ref bytes, offset, 5 + readSize8);
              MessagePackBinary.ReadFully(stream, bytes, offset + 5, readSize8);
              return readSize8 + 5;
            default:
              throw new InvalidOperationException("Invalid Code");
          }
        case MessagePackType.Array:
          int readSize9 = 0;
          if ((byte) 144 <= code && code <= (byte) 159)
          {
            readSize9 = 0;
          }
          else
          {
            switch (code)
            {
              case 220:
                readSize9 = 2;
                break;
              case 221:
                readSize9 = 4;
                break;
            }
          }
          if (readSize9 != 0)
          {
            MessagePackBinary.EnsureCapacity(ref bytes, offset, readSize9 + 1);
            MessagePackBinary.ReadFully(stream, bytes, offset + 1, readSize9);
          }
          int offset1 = offset;
          offset += readSize9 + 1;
          uint num2 = MessagePackBinary.ReadArrayHeaderRaw(bytes, offset1, out int _);
          if (!readOnlySingleMessage)
          {
            for (int index = 0; (long) index < (long) num2; ++index)
              offset += MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref bytes, offset, readOnlySingleMessage);
          }
          return offset - offset1;
        case MessagePackType.Map:
          int readSize10 = 0;
          if ((byte) 128 <= code && code <= (byte) 143)
          {
            readSize10 = 0;
          }
          else
          {
            switch (code)
            {
              case 222:
                readSize10 = 2;
                break;
              case 223:
                readSize10 = 4;
                break;
            }
          }
          if (readSize10 != 0)
          {
            MessagePackBinary.EnsureCapacity(ref bytes, offset, readSize10 + 1);
            MessagePackBinary.ReadFully(stream, bytes, offset + 1, readSize10);
          }
          int offset2 = offset;
          offset += readSize10 + 1;
          uint num3 = MessagePackBinary.ReadMapHeaderRaw(bytes, offset2, out int _);
          if (!readOnlySingleMessage)
          {
            for (int index = 0; (long) index < (long) num3; ++index)
            {
              offset += MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref bytes, offset, readOnlySingleMessage);
              offset += MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref bytes, offset, readOnlySingleMessage);
            }
          }
          return offset - offset2;
        case MessagePackType.Extension:
          int readSize11;
          switch (code)
          {
            case 199:
              readSize11 = 2;
              break;
            case 200:
              readSize11 = 3;
              break;
            case 201:
              readSize11 = 5;
              break;
            case 212:
              readSize11 = 1;
              break;
            case 213:
              readSize11 = 1;
              break;
            case 214:
              readSize11 = 1;
              break;
            case 215:
              readSize11 = 1;
              break;
            case 216:
              readSize11 = 1;
              break;
            default:
              throw new InvalidOperationException("Invalid Code");
          }
          MessagePackBinary.EnsureCapacity(ref bytes, offset, readSize11 + 1);
          MessagePackBinary.ReadFully(stream, bytes, offset + 1, readSize11);
          if (readOnlySingleMessage)
            return readSize11 + 1;
          ExtensionHeader extensionHeader = MessagePackBinary.ReadExtensionFormatHeader(bytes, offset, out int _);
          MessagePackBinary.EnsureCapacity(ref bytes, offset, 1 + readSize11 + (int) extensionHeader.Length);
          MessagePackBinary.ReadFully(stream, bytes, offset + 1 + readSize11, (int) extensionHeader.Length);
          return 1 + readSize11 + (int) extensionHeader.Length;
        default:
          throw new InvalidOperationException("Invalid Code");
      }
    }

    private static void ReadFully(Stream stream, byte[] bytes, int offset, int readSize)
    {
      int num;
      for (int count = readSize; count != 0; count -= num)
      {
        num = stream.Read(bytes, offset, count);
        if (num == -1)
          break;
        offset += num;
      }
    }

    public static int ReadNext(Stream stream)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      return MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref buffer, 0, true);
    }

    public static int ReadNextBlock(Stream stream)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int offset = 0;
      return MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref buffer, offset, false);
    }

    public static int WriteNil(Stream stream)
    {
      stream.WriteByte((byte) 192);
      return 1;
    }

    public static Nil ReadNil(Stream stream)
    {
      return MessagePackBinary.ReadNil(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static bool IsNil(Stream stream)
    {
      return MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream)[0] == (byte) 192;
    }

    public static int WriteFixedMapHeaderUnsafe(Stream stream, int count)
    {
      stream.WriteByte((byte) (128 | count));
      return 1;
    }

    public static int WriteMapHeader(Stream stream, int count)
    {
      return MessagePackBinary.WriteMapHeader(stream, checked ((uint) count));
    }

    public static int WriteMapHeader(Stream stream, uint count)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count1 = MessagePackBinary.WriteMapHeader(ref buffer, 0, count);
      stream.Write(buffer, 0, count1);
      return count1;
    }

    public static int WriteMapHeaderForceMap32Block(Stream stream, uint count)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count1 = MessagePackBinary.WriteMapHeaderForceMap32Block(ref buffer, 0, count);
      stream.Write(buffer, 0, count1);
      return count1;
    }

    public static int ReadMapHeader(Stream stream)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref buffer, 0, true);
      return MessagePackBinary.ReadMapHeader(buffer, 0, out int _);
    }

    public static uint ReadMapHeaderRaw(Stream stream)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref buffer, 0, true);
      return MessagePackBinary.ReadMapHeaderRaw(buffer, 0, out int _);
    }

    public static int WriteFixedArrayHeaderUnsafe(Stream stream, int count)
    {
      stream.WriteByte((byte) (144 | count));
      return 1;
    }

    public static int WriteArrayHeader(Stream stream, int count)
    {
      return MessagePackBinary.WriteArrayHeader(stream, checked ((uint) count));
    }

    public static int WriteArrayHeader(Stream stream, uint count)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count1 = MessagePackBinary.WriteArrayHeader(ref buffer, 0, count);
      stream.Write(buffer, 0, count1);
      return count1;
    }

    public static int WriteArrayHeaderForceArray32Block(Stream stream, uint count)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count1 = MessagePackBinary.WriteArrayHeaderForceArray32Block(ref buffer, 0, count);
      stream.Write(buffer, 0, count1);
      return count1;
    }

    public static int ReadArrayHeader(Stream stream)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref buffer, 0, true);
      return MessagePackBinary.ReadArrayHeader(buffer, 0, out int _);
    }

    public static uint ReadArrayHeaderRaw(Stream stream)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      MessagePackBinary.ReadMessageBlockFromStreamCore(stream, ref buffer, 0, true);
      return MessagePackBinary.ReadArrayHeaderRaw(buffer, 0, out int _);
    }

    public static int WriteBoolean(Stream stream, bool value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteBoolean(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static bool ReadBoolean(Stream stream)
    {
      return MessagePackBinary.ReadBoolean(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteByte(Stream stream, byte value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteByte(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteByteForceByteBlock(Stream stream, byte value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteByteForceByteBlock(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static byte ReadByte(Stream stream)
    {
      return MessagePackBinary.ReadByte(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteBytes(Stream stream, byte[] value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteBytes(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteBytes(Stream stream, byte[] src, int srcOffset, int count)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count1 = MessagePackBinary.WriteBytes(ref buffer, 0, src, srcOffset, count);
      stream.Write(buffer, 0, count1);
      return count1;
    }

    public static byte[] ReadBytes(Stream stream)
    {
      return MessagePackBinary.ReadBytes(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteSByte(Stream stream, sbyte value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteSByte(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteSByteForceSByteBlock(Stream stream, sbyte value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteSByteForceSByteBlock(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static sbyte ReadSByte(Stream stream)
    {
      return MessagePackBinary.ReadSByte(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteSingle(Stream stream, float value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteSingle(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static float ReadSingle(Stream stream)
    {
      return MessagePackBinary.ReadSingle(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteDouble(Stream stream, double value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteDouble(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static double ReadDouble(Stream stream)
    {
      return MessagePackBinary.ReadDouble(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteInt16(Stream stream, short value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteInt16(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteInt16ForceInt16Block(Stream stream, short value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteInt16ForceInt16Block(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static short ReadInt16(Stream stream)
    {
      return MessagePackBinary.ReadInt16(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WritePositiveFixedIntUnsafe(Stream stream, int value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WritePositiveFixedIntUnsafe(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteInt32(Stream stream, int value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteInt32(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteInt32ForceInt32Block(Stream stream, int value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteInt32ForceInt32Block(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int ReadInt32(Stream stream)
    {
      return MessagePackBinary.ReadInt32(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteInt64(Stream stream, long value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteInt64(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteInt64ForceInt64Block(Stream stream, long value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteInt64ForceInt64Block(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static long ReadInt64(Stream stream)
    {
      return MessagePackBinary.ReadInt64(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteUInt16(Stream stream, ushort value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteUInt16(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteUInt16ForceUInt16Block(Stream stream, ushort value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteUInt16ForceUInt16Block(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static ushort ReadUInt16(Stream stream)
    {
      return MessagePackBinary.ReadUInt16(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteUInt32(Stream stream, uint value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteUInt32(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteUInt32ForceUInt32Block(Stream stream, uint value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteUInt32ForceUInt32Block(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static uint ReadUInt32(Stream stream)
    {
      return MessagePackBinary.ReadUInt32(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteUInt64(Stream stream, ulong value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteUInt64(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteUInt64ForceUInt64Block(Stream stream, ulong value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteUInt64ForceUInt64Block(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static ulong ReadUInt64(Stream stream)
    {
      return MessagePackBinary.ReadUInt64(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteChar(Stream stream, char value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteChar(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static char ReadChar(Stream stream)
    {
      return MessagePackBinary.ReadChar(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteFixedStringUnsafe(Stream stream, string value, int byteCount)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteFixedStringUnsafe(ref buffer, 0, value, byteCount);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteStringUnsafe(Stream stream, string value, int byteCount)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteStringUnsafe(ref buffer, 0, value, byteCount);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteStringBytes(Stream stream, byte[] utf8stringBytes)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteStringBytes(ref buffer, 0, utf8stringBytes);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteString(Stream stream, string value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteString(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteStringForceStr32Block(Stream stream, string value)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteStringForceStr32Block(ref buffer, 0, value);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static string ReadString(Stream stream)
    {
      return MessagePackBinary.ReadString(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static int WriteExtensionFormatHeader(Stream stream, sbyte typeCode, int dataLength)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteExtensionFormatHeader(ref buffer, 0, typeCode, dataLength);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteExtensionFormatHeaderForceExt32Block(
      Stream stream,
      sbyte typeCode,
      int dataLength)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteExtensionFormatHeaderForceExt32Block(ref buffer, 0, typeCode, dataLength);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static int WriteExtensionFormat(Stream stream, sbyte typeCode, byte[] data)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteExtensionFormat(ref buffer, 0, typeCode, data);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static ExtensionResult ReadExtensionFormat(Stream stream)
    {
      return MessagePackBinary.ReadExtensionFormat(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    public static ExtensionHeader ReadExtensionFormatHeader(Stream stream)
    {
      int readSize;
      return MessagePackBinary.ReadExtensionFormatHeader(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream, true, out readSize), 0, out readSize);
    }

    public static int WriteDateTime(Stream stream, DateTime dateTime)
    {
      byte[] buffer = MessagePackBinary.StreamDecodeMemoryPool.GetBuffer();
      int count = MessagePackBinary.WriteDateTime(ref buffer, 0, dateTime);
      stream.Write(buffer, 0, count);
      return count;
    }

    public static DateTime ReadDateTime(Stream stream)
    {
      return MessagePackBinary.ReadDateTime(MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream), 0, out int _);
    }

    private static class StreamDecodeMemoryPool
    {
      [ThreadStatic]
      private static byte[] buffer;

      public static byte[] GetBuffer()
      {
        if (MessagePackBinary.StreamDecodeMemoryPool.buffer == null)
          MessagePackBinary.StreamDecodeMemoryPool.buffer = new byte[65536];
        return MessagePackBinary.StreamDecodeMemoryPool.buffer;
      }
    }
  }
}
