// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JobParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JobParamFormatter : IMessagePackFormatter<JobParam>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JobParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "DefaultArtifact",
          0
        },
        {
          "IsRiding",
          1
        },
        {
          "IsFlyingPass",
          2
        },
        {
          "iname",
          3
        },
        {
          "name",
          4
        },
        {
          "expr",
          5
        },
        {
          "model",
          6
        },
        {
          "ac2d",
          7
        },
        {
          "modelp",
          8
        },
        {
          "pet",
          9
        },
        {
          "buki",
          10
        },
        {
          "origin",
          11
        },
        {
          "type",
          12
        },
        {
          "role",
          13
        },
        {
          "mov",
          14
        },
        {
          "jmp",
          15
        },
        {
          "wepmdl",
          16
        },
        {
          "atkskill",
          17
        },
        {
          "artifact",
          18
        },
        {
          "ai",
          19
        },
        {
          "master",
          20
        },
        {
          "fixed_ability",
          21
        },
        {
          "MapEffectAbility",
          22
        },
        {
          "IsMapEffectRevReso",
          23
        },
        {
          "DescCharacteristic",
          24
        },
        {
          "DescOther",
          25
        },
        {
          "status",
          26
        },
        {
          "avoid",
          27
        },
        {
          "inimp",
          28
        },
        {
          "ranks",
          29
        },
        {
          "unit_image",
          30
        },
        {
          "MovType",
          31
        },
        {
          "tags",
          32
        },
        {
          "buff",
          33
        }
      };
      this.____stringByteKeys = new byte[34][]
      {
        MessagePackBinary.GetEncodedStringBytes("DefaultArtifact"),
        MessagePackBinary.GetEncodedStringBytes("IsRiding"),
        MessagePackBinary.GetEncodedStringBytes("IsFlyingPass"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("model"),
        MessagePackBinary.GetEncodedStringBytes("ac2d"),
        MessagePackBinary.GetEncodedStringBytes("modelp"),
        MessagePackBinary.GetEncodedStringBytes("pet"),
        MessagePackBinary.GetEncodedStringBytes("buki"),
        MessagePackBinary.GetEncodedStringBytes("origin"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("role"),
        MessagePackBinary.GetEncodedStringBytes("mov"),
        MessagePackBinary.GetEncodedStringBytes("jmp"),
        MessagePackBinary.GetEncodedStringBytes("wepmdl"),
        MessagePackBinary.GetEncodedStringBytes("atkskill"),
        MessagePackBinary.GetEncodedStringBytes("artifact"),
        MessagePackBinary.GetEncodedStringBytes("ai"),
        MessagePackBinary.GetEncodedStringBytes("master"),
        MessagePackBinary.GetEncodedStringBytes("fixed_ability"),
        MessagePackBinary.GetEncodedStringBytes("MapEffectAbility"),
        MessagePackBinary.GetEncodedStringBytes("IsMapEffectRevReso"),
        MessagePackBinary.GetEncodedStringBytes("DescCharacteristic"),
        MessagePackBinary.GetEncodedStringBytes("DescOther"),
        MessagePackBinary.GetEncodedStringBytes("status"),
        MessagePackBinary.GetEncodedStringBytes("avoid"),
        MessagePackBinary.GetEncodedStringBytes("inimp"),
        MessagePackBinary.GetEncodedStringBytes("ranks"),
        MessagePackBinary.GetEncodedStringBytes("unit_image"),
        MessagePackBinary.GetEncodedStringBytes("MovType"),
        MessagePackBinary.GetEncodedStringBytes("tags"),
        MessagePackBinary.GetEncodedStringBytes("buff")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JobParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 34);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ArtifactParam>().Serialize(ref bytes, offset, value.DefaultArtifact, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsRiding);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsFlyingPass);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.model, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ac2d, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.modelp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.pet, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.buki, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.origin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<JobTypes>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<RoleTypes>().Serialize(ref bytes, offset, value.role, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.mov, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.jmp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.wepmdl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.atkskill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.artifact, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ai, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.master, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.fixed_ability, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.MapEffectAbility, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsMapEffectRevReso);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DescCharacteristic, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DescOther, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<StatusParam>().Serialize(ref bytes, offset, value.status, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.avoid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.inimp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<JobRankParam[]>().Serialize(ref bytes, offset, value.ranks, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit_image, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += formatterResolver.GetFormatterWithVerify<eMovType>().Serialize(ref bytes, offset, value.MovType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.tags, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.buff, formatterResolver);
      return offset - num;
    }

    public JobParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JobParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ArtifactParam artifactParam = (ArtifactParam) null;
      bool flag1 = false;
      bool flag2 = false;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      string str9 = (string) null;
      JobTypes jobTypes = JobTypes.None;
      RoleTypes roleTypes = RoleTypes.None;
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      string str10 = (string) null;
      string[] strArray1 = (string[]) null;
      string str11 = (string) null;
      string str12 = (string) null;
      string str13 = (string) null;
      string str14 = (string) null;
      string str15 = (string) null;
      bool flag3 = false;
      string str16 = (string) null;
      string str17 = (string) null;
      StatusParam statusParam = (StatusParam) null;
      OInt oint3 = new OInt();
      OInt oint4 = new OInt();
      JobRankParam[] jobRankParamArray = (JobRankParam[]) null;
      string str18 = (string) null;
      eMovType eMovType = eMovType.Normal;
      string[] strArray2 = (string[]) null;
      string str19 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num3)
          {
            case 0:
              artifactParam = formatterResolver.GetFormatterWithVerify<ArtifactParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 2:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 3:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              jobTypes = formatterResolver.GetFormatterWithVerify<JobTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              roleTypes = formatterResolver.GetFormatterWithVerify<RoleTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 24:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              str17 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              statusParam = formatterResolver.GetFormatterWithVerify<StatusParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              jobRankParamArray = formatterResolver.GetFormatterWithVerify<JobRankParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 30:
              str18 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              eMovType = formatterResolver.GetFormatterWithVerify<eMovType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 32:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              str19 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JobParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        model = str4,
        ac2d = str5,
        modelp = str6,
        pet = str7,
        buki = str8,
        origin = str9,
        type = jobTypes,
        role = roleTypes,
        mov = oint1,
        jmp = oint2,
        wepmdl = str10,
        atkskill = strArray1,
        artifact = str11,
        ai = str12,
        master = str13,
        fixed_ability = str14,
        MapEffectAbility = str15,
        IsMapEffectRevReso = flag3,
        DescCharacteristic = str16,
        DescOther = str17,
        status = statusParam,
        avoid = oint3,
        inimp = oint4,
        ranks = jobRankParamArray,
        unit_image = str18,
        MovType = eMovType,
        tags = strArray2,
        buff = str19
      };
    }
  }
}
