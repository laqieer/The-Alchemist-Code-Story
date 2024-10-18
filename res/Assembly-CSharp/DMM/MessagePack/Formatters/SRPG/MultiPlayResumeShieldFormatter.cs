// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.MultiPlayResumeShieldFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class MultiPlayResumeShieldFormatter : 
    IMessagePackFormatter<MultiPlayResumeShield>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public MultiPlayResumeShieldFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "inm",
          0
        },
        {
          "nhp",
          1
        },
        {
          "mhp",
          2
        },
        {
          "ntu",
          3
        },
        {
          "mtu",
          4
        },
        {
          "drt",
          5
        },
        {
          "dvl",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("inm"),
        MessagePackBinary.GetEncodedStringBytes("nhp"),
        MessagePackBinary.GetEncodedStringBytes("mhp"),
        MessagePackBinary.GetEncodedStringBytes("ntu"),
        MessagePackBinary.GetEncodedStringBytes("mtu"),
        MessagePackBinary.GetEncodedStringBytes("drt"),
        MessagePackBinary.GetEncodedStringBytes("dvl")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      MultiPlayResumeShield value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.inm, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.nhp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mhp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ntu);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mtu);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.drt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dvl);
      return offset - num;
    }

    public MultiPlayResumeShield Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (MultiPlayResumeShield) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num9;
        if (!this.____keyMapping.TryGetValueSafe(key, out num9))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num9)
          {
            case 0:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new MultiPlayResumeShield()
      {
        inm = str,
        nhp = num3,
        mhp = num4,
        ntu = num5,
        mtu = num6,
        drt = num7,
        dvl = num8
      };
    }
  }
}
