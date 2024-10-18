// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.UnitParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class UnitParamFormatter : IMessagePackFormatter<UnitParam>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public UnitParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "IsFlyingPass",
          0
        },
        {
          "SexPrefix",
          1
        },
        {
          "IsNormalSize",
          2
        },
        {
          "flag",
          3
        },
        {
          "iname",
          4
        },
        {
          "name",
          5
        },
        {
          "model",
          6
        },
        {
          "birth",
          7
        },
        {
          "birthID",
          8
        },
        {
          "grow",
          9
        },
        {
          "jobsets",
          10
        },
        {
          "tags",
          11
        },
        {
          "piece",
          12
        },
        {
          "subPiece",
          13
        },
        {
          "ability",
          14
        },
        {
          "ma_quest",
          15
        },
        {
          "sex",
          16
        },
        {
          "rare",
          17
        },
        {
          "raremax",
          18
        },
        {
          "type",
          19
        },
        {
          "element",
          20
        },
        {
          "sw",
          21
        },
        {
          "sh",
          22
        },
        {
          "sd",
          23
        },
        {
          "dbuki",
          24
        },
        {
          "search",
          25
        },
        {
          "height",
          26
        },
        {
          "weight",
          27
        },
        {
          "available_at",
          28
        },
        {
          "image",
          29
        },
        {
          "voice",
          30
        },
        {
          "ini_status",
          31
        },
        {
          "max_status",
          32
        },
        {
          "leader_skills",
          33
        },
        {
          "recipes",
          34
        },
        {
          "no_job_status",
          35
        },
        {
          "skins",
          36
        },
        {
          "unlock_time",
          37
        },
        {
          "unit_piece_shop_group",
          38
        }
      };
      this.____stringByteKeys = new byte[39][]
      {
        MessagePackBinary.GetEncodedStringBytes("IsFlyingPass"),
        MessagePackBinary.GetEncodedStringBytes("SexPrefix"),
        MessagePackBinary.GetEncodedStringBytes("IsNormalSize"),
        MessagePackBinary.GetEncodedStringBytes("flag"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("model"),
        MessagePackBinary.GetEncodedStringBytes("birth"),
        MessagePackBinary.GetEncodedStringBytes("birthID"),
        MessagePackBinary.GetEncodedStringBytes("grow"),
        MessagePackBinary.GetEncodedStringBytes("jobsets"),
        MessagePackBinary.GetEncodedStringBytes("tags"),
        MessagePackBinary.GetEncodedStringBytes("piece"),
        MessagePackBinary.GetEncodedStringBytes("subPiece"),
        MessagePackBinary.GetEncodedStringBytes("ability"),
        MessagePackBinary.GetEncodedStringBytes("ma_quest"),
        MessagePackBinary.GetEncodedStringBytes("sex"),
        MessagePackBinary.GetEncodedStringBytes("rare"),
        MessagePackBinary.GetEncodedStringBytes("raremax"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("element"),
        MessagePackBinary.GetEncodedStringBytes("sw"),
        MessagePackBinary.GetEncodedStringBytes("sh"),
        MessagePackBinary.GetEncodedStringBytes("sd"),
        MessagePackBinary.GetEncodedStringBytes("dbuki"),
        MessagePackBinary.GetEncodedStringBytes("search"),
        MessagePackBinary.GetEncodedStringBytes("height"),
        MessagePackBinary.GetEncodedStringBytes("weight"),
        MessagePackBinary.GetEncodedStringBytes("available_at"),
        MessagePackBinary.GetEncodedStringBytes("image"),
        MessagePackBinary.GetEncodedStringBytes("voice"),
        MessagePackBinary.GetEncodedStringBytes("ini_status"),
        MessagePackBinary.GetEncodedStringBytes("max_status"),
        MessagePackBinary.GetEncodedStringBytes("leader_skills"),
        MessagePackBinary.GetEncodedStringBytes("recipes"),
        MessagePackBinary.GetEncodedStringBytes("no_job_status"),
        MessagePackBinary.GetEncodedStringBytes("skins"),
        MessagePackBinary.GetEncodedStringBytes("unlock_time"),
        MessagePackBinary.GetEncodedStringBytes("unit_piece_shop_group")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      UnitParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 39);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsFlyingPass);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SexPrefix, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNormalSize);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<FlagManager>().Serialize(ref bytes, offset, value.flag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.model, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<OString>().Serialize(ref bytes, offset, value.birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.birthID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<OString>().Serialize(ref bytes, offset, value.grow, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.jobsets, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.tags, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.piece, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.subPiece, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ability, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ma_quest, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<ESex>().Serialize(ref bytes, offset, value.sex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.raremax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<EUnitType>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<EElement>().Serialize(ref bytes, offset, value.element, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.sw);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.sh);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.sd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.dbuki, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.search);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.height);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.weight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<DateTime>().Serialize(ref bytes, offset, value.available_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.image, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.voice, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += formatterResolver.GetFormatterWithVerify<UnitParam.Status>().Serialize(ref bytes, offset, value.ini_status, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<UnitParam.Status>().Serialize(ref bytes, offset, value.max_status, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.leader_skills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.recipes, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += formatterResolver.GetFormatterWithVerify<UnitParam.NoJobStatus>().Serialize(ref bytes, offset, value.no_job_status, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.skins, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unlock_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit_piece_shop_group, formatterResolver);
      return offset - num;
    }

    public UnitParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (UnitParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      bool flag1 = false;
      string str1 = (string) null;
      bool flag2 = false;
      FlagManager flagManager = new FlagManager();
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      OString ostring1 = new OString();
      int num3 = 0;
      OString ostring2 = new OString();
      string[] strArray1 = (string[]) null;
      string[] strArray2 = (string[]) null;
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      ESex esex = ESex.Unknown;
      byte num4 = 0;
      byte num5 = 0;
      EUnitType eunitType = EUnitType.Unit;
      EElement eelement = EElement.None;
      byte num6 = 0;
      byte num7 = 0;
      byte num8 = 0;
      string str9 = (string) null;
      byte num9 = 0;
      short num10 = 0;
      short num11 = 0;
      DateTime dateTime = new DateTime();
      string str10 = (string) null;
      string str11 = (string) null;
      UnitParam.Status status1 = (UnitParam.Status) null;
      UnitParam.Status status2 = (UnitParam.Status) null;
      string[] strArray3 = (string[]) null;
      string[] strArray4 = (string[]) null;
      UnitParam.NoJobStatus noJobStatus = (UnitParam.NoJobStatus) null;
      string[] strArray5 = (string[]) null;
      string str12 = (string) null;
      string str13 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num12;
        if (!this.____keyMapping.TryGetValueSafe(key, out num12))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num12)
          {
            case 0:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 1:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 3:
              flagManager = formatterResolver.GetFormatterWithVerify<FlagManager>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              ostring1 = formatterResolver.GetFormatterWithVerify<OString>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              ostring2 = formatterResolver.GetFormatterWithVerify<OString>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              esex = formatterResolver.GetFormatterWithVerify<ESex>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              num4 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 18:
              num5 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 19:
              eunitType = formatterResolver.GetFormatterWithVerify<EUnitType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              eelement = formatterResolver.GetFormatterWithVerify<EElement>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              num6 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 22:
              num7 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 23:
              num8 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 24:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              num9 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 26:
              num10 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 27:
              num11 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 28:
              dateTime = formatterResolver.GetFormatterWithVerify<DateTime>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 30:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              status1 = formatterResolver.GetFormatterWithVerify<UnitParam.Status>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 32:
              status2 = formatterResolver.GetFormatterWithVerify<UnitParam.Status>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              strArray4 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 35:
              noJobStatus = formatterResolver.GetFormatterWithVerify<UnitParam.NoJobStatus>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 36:
              strArray5 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new UnitParam()
      {
        flag = flagManager,
        iname = str2,
        name = str3,
        model = str4,
        birth = ostring1,
        birthID = num3,
        grow = ostring2,
        jobsets = strArray1,
        tags = strArray2,
        piece = str5,
        subPiece = str6,
        ability = str7,
        ma_quest = str8,
        sex = esex,
        rare = num4,
        raremax = num5,
        type = eunitType,
        element = eelement,
        sw = num6,
        sh = num7,
        sd = num8,
        dbuki = str9,
        search = num9,
        height = num10,
        weight = num11,
        available_at = dateTime,
        image = str10,
        voice = str11,
        ini_status = status1,
        max_status = status2,
        leader_skills = strArray3,
        recipes = strArray4,
        no_job_status = noJobStatus,
        skins = strArray5,
        unlock_time = str12,
        unit_piece_shop_group = str13
      };
    }
  }
}
