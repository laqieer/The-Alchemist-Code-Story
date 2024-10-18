// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ConceptCardParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ConceptCardParamFormatter : 
    IMessagePackFormatter<ConceptCardParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ConceptCardParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "IsEnableAwake",
          0
        },
        {
          "AwakeCountCap",
          1
        },
        {
          "AwakeLevelCap",
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
          "type",
          6
        },
        {
          "icon",
          7
        },
        {
          "rare",
          8
        },
        {
          "lvcap",
          9
        },
        {
          "sell",
          10
        },
        {
          "coin_item",
          11
        },
        {
          "en_cost",
          12
        },
        {
          "en_exp",
          13
        },
        {
          "en_trust",
          14
        },
        {
          "trust_reward",
          15
        },
        {
          "first_get_unit",
          16
        },
        {
          "is_override_lvcap",
          17
        },
        {
          "effects",
          18
        },
        {
          "not_sale",
          19
        },
        {
          "birth_id",
          20
        },
        {
          "concept_card_groups",
          21
        },
        {
          "leader_skill",
          22
        },
        {
          "gallery_view",
          23
        },
        {
          "is_other",
          24
        },
        {
          "limit_up_items",
          25
        },
        {
          "bg_image",
          26
        },
        {
          "unit_images",
          27
        }
      };
      this.____stringByteKeys = new byte[28][]
      {
        MessagePackBinary.GetEncodedStringBytes("IsEnableAwake"),
        MessagePackBinary.GetEncodedStringBytes("AwakeCountCap"),
        MessagePackBinary.GetEncodedStringBytes("AwakeLevelCap"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("icon"),
        MessagePackBinary.GetEncodedStringBytes("rare"),
        MessagePackBinary.GetEncodedStringBytes("lvcap"),
        MessagePackBinary.GetEncodedStringBytes("sell"),
        MessagePackBinary.GetEncodedStringBytes("coin_item"),
        MessagePackBinary.GetEncodedStringBytes("en_cost"),
        MessagePackBinary.GetEncodedStringBytes("en_exp"),
        MessagePackBinary.GetEncodedStringBytes("en_trust"),
        MessagePackBinary.GetEncodedStringBytes("trust_reward"),
        MessagePackBinary.GetEncodedStringBytes("first_get_unit"),
        MessagePackBinary.GetEncodedStringBytes("is_override_lvcap"),
        MessagePackBinary.GetEncodedStringBytes("effects"),
        MessagePackBinary.GetEncodedStringBytes("not_sale"),
        MessagePackBinary.GetEncodedStringBytes("birth_id"),
        MessagePackBinary.GetEncodedStringBytes("concept_card_groups"),
        MessagePackBinary.GetEncodedStringBytes("leader_skill"),
        MessagePackBinary.GetEncodedStringBytes("gallery_view"),
        MessagePackBinary.GetEncodedStringBytes("is_other"),
        MessagePackBinary.GetEncodedStringBytes("limit_up_items"),
        MessagePackBinary.GetEncodedStringBytes("bg_image"),
        MessagePackBinary.GetEncodedStringBytes("unit_images")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ConceptCardParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 28);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsEnableAwake);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.AwakeCountCap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.AwakeLevelCap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<eCardType>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.icon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lvcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sell);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.coin_item);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.en_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.en_exp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.en_trust);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.trust_reward, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.first_get_unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.is_override_lvcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<ConceptCardEffectsParam[]>().Serialize(ref bytes, offset, value.effects, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.not_sale);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.birth_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.concept_card_groups, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.leader_skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<GalleryVisibilityType>().Serialize(ref bytes, offset, value.gallery_view, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.is_other);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<List<ConceptLimitUpItemParam>>().Serialize(ref bytes, offset, value.limit_up_items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.bg_image, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.unit_images, formatterResolver);
      return offset - num;
    }

    public ConceptCardParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ConceptCardParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      bool flag1 = false;
      int num3 = 0;
      int num4 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      eCardType eCardType = eCardType.None;
      string str4 = (string) null;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      string str5 = (string) null;
      string str6 = (string) null;
      bool flag2 = false;
      ConceptCardEffectsParam[] cardEffectsParamArray = (ConceptCardEffectsParam[]) null;
      bool flag3 = false;
      int num12 = 0;
      string[] strArray1 = (string[]) null;
      string str7 = (string) null;
      GalleryVisibilityType galleryVisibilityType = GalleryVisibilityType.None;
      bool flag4 = false;
      List<ConceptLimitUpItemParam> limitUpItemParamList = (List<ConceptLimitUpItemParam>) null;
      string str8 = (string) null;
      string[] strArray2 = (string[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num13;
        if (!this.____keyMapping.TryGetValueSafe(key, out num13))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num13)
          {
            case 0:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
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
              eCardType = formatterResolver.GetFormatterWithVerify<eCardType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 18:
              cardEffectsParamArray = formatterResolver.GetFormatterWithVerify<ConceptCardEffectsParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 20:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              galleryVisibilityType = formatterResolver.GetFormatterWithVerify<GalleryVisibilityType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 25:
              limitUpItemParamList = formatterResolver.GetFormatterWithVerify<List<ConceptLimitUpItemParam>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ConceptCardParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        type = eCardType,
        icon = str4,
        rare = num5,
        lvcap = num6,
        sell = num7,
        coin_item = num8,
        en_cost = num9,
        en_exp = num10,
        en_trust = num11,
        trust_reward = str5,
        first_get_unit = str6,
        is_override_lvcap = flag2,
        effects = cardEffectsParamArray,
        not_sale = flag3,
        birth_id = num12,
        concept_card_groups = strArray1,
        leader_skill = str7,
        gallery_view = galleryVisibilityType,
        is_other = flag4,
        limit_up_items = limitUpItemParamList,
        bg_image = str8,
        unit_images = strArray2
      };
    }
  }
}
