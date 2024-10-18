// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_JobFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_JobFormatter : IMessagePackFormatter<Json_Job>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_JobFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iid",
          0
        },
        {
          "iname",
          1
        },
        {
          "rank",
          2
        },
        {
          "cur_skin",
          3
        },
        {
          "equips",
          4
        },
        {
          "abils",
          5
        },
        {
          "artis",
          6
        },
        {
          "select",
          7
        },
        {
          "unit_image",
          8
        }
      };
      this.____stringByteKeys = new byte[9][]
      {
        MessagePackBinary.GetEncodedStringBytes("iid"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("rank"),
        MessagePackBinary.GetEncodedStringBytes("cur_skin"),
        MessagePackBinary.GetEncodedStringBytes("equips"),
        MessagePackBinary.GetEncodedStringBytes("abils"),
        MessagePackBinary.GetEncodedStringBytes("artis"),
        MessagePackBinary.GetEncodedStringBytes("select"),
        MessagePackBinary.GetEncodedStringBytes("unit_image")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_Job value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.iid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rank);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cur_skin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Equip[]>().Serialize(ref bytes, offset, value.equips, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Ability[]>().Serialize(ref bytes, offset, value.abils, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Serialize(ref bytes, offset, value.artis, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<Json_JobSelectable>().Serialize(ref bytes, offset, value.select, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit_image, formatterResolver);
      return offset - num;
    }

    public Json_Job Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_Job) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      long num3 = 0;
      string str1 = (string) null;
      int num4 = 0;
      string str2 = (string) null;
      Json_Equip[] jsonEquipArray = (Json_Equip[]) null;
      Json_Ability[] jsonAbilityArray = (Json_Ability[]) null;
      Json_Artifact[] jsonArtifactArray = (Json_Artifact[]) null;
      Json_JobSelectable jsonJobSelectable = (Json_JobSelectable) null;
      string str3 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num5;
        if (!this.____keyMapping.TryGetValueSafe(key, out num5))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num5)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 1:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonEquipArray = formatterResolver.GetFormatterWithVerify<Json_Equip[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonAbilityArray = formatterResolver.GetFormatterWithVerify<Json_Ability[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonArtifactArray = formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonJobSelectable = formatterResolver.GetFormatterWithVerify<Json_JobSelectable>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_Job()
      {
        iid = num3,
        iname = str1,
        rank = num4,
        cur_skin = str2,
        equips = jsonEquipArray,
        abils = jsonAbilityArray,
        artis = jsonArtifactArray,
        select = jsonJobSelectable,
        unit_image = str3
      };
    }
  }
}
