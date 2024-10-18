// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.UnitParam_NoJobStatusFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class UnitParam_NoJobStatusFormatter : 
    IMessagePackFormatter<UnitParam.NoJobStatus>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public UnitParam_NoJobStatusFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "default_skill",
          0
        },
        {
          "jobtype",
          1
        },
        {
          "role",
          2
        },
        {
          "mov",
          3
        },
        {
          "jmp",
          4
        },
        {
          "inimp",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("default_skill"),
        MessagePackBinary.GetEncodedStringBytes("jobtype"),
        MessagePackBinary.GetEncodedStringBytes("role"),
        MessagePackBinary.GetEncodedStringBytes("mov"),
        MessagePackBinary.GetEncodedStringBytes("jmp"),
        MessagePackBinary.GetEncodedStringBytes("inimp")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      UnitParam.NoJobStatus value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.default_skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JobTypes>().Serialize(ref bytes, offset, value.jobtype, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<RoleTypes>().Serialize(ref bytes, offset, value.role, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.mov);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.jmp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.inimp);
      return offset - num;
    }

    public UnitParam.NoJobStatus Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (UnitParam.NoJobStatus) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
      JobTypes jobTypes = JobTypes.None;
      RoleTypes roleTypes = RoleTypes.None;
      byte num3 = 0;
      byte num4 = 0;
      int num5 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num6;
        if (!this.____keyMapping.TryGetValueSafe(key, out num6))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num6)
          {
            case 0:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jobTypes = formatterResolver.GetFormatterWithVerify<JobTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              roleTypes = formatterResolver.GetFormatterWithVerify<RoleTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              num3 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 4:
              num4 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 5:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new UnitParam.NoJobStatus()
      {
        default_skill = str,
        jobtype = jobTypes,
        role = roleTypes,
        mov = num3,
        jmp = num4,
        inimp = num5
      };
    }
  }
}
