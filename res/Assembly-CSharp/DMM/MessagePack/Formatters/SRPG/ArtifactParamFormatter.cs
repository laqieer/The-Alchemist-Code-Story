// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ArtifactParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ArtifactParamFormatter : 
    IMessagePackFormatter<ArtifactParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ArtifactParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Rarity",
          0
        },
        {
          "iname",
          1
        },
        {
          "name",
          2
        },
        {
          "spec",
          3
        },
        {
          "asset",
          4
        },
        {
          "voice",
          5
        },
        {
          "icon",
          6
        },
        {
          "type",
          7
        },
        {
          "rareini",
          8
        },
        {
          "raremax",
          9
        },
        {
          "kakera",
          10
        },
        {
          "is_create",
          11
        },
        {
          "maxnum",
          12
        },
        {
          "skill",
          13
        },
        {
          "kcoin",
          14
        },
        {
          "tcoin",
          15
        },
        {
          "acoin",
          16
        },
        {
          "mcoin",
          17
        },
        {
          "pcoin",
          18
        },
        {
          "buy",
          19
        },
        {
          "sell",
          20
        },
        {
          "enhance_cost",
          21
        },
        {
          "skills",
          22
        },
        {
          "equip_effects",
          23
        },
        {
          "attack_effects",
          24
        },
        {
          "abil_inames",
          25
        },
        {
          "abil_conds",
          26
        },
        {
          "abil_levels",
          27
        },
        {
          "abil_rareties",
          28
        },
        {
          "abil_shows",
          29
        },
        {
          "tag",
          30
        },
        {
          "condition_lv",
          31
        },
        {
          "condition_units",
          32
        },
        {
          "condition_jobs",
          33
        },
        {
          "condition_birth",
          34
        },
        {
          "condition_sex",
          35
        },
        {
          "condition_element",
          36
        },
        {
          "condition_raremin",
          37
        },
        {
          "condition_raremax",
          38
        },
        {
          "CachedKakeraCount",
          39
        },
        {
          "CondSkillMotion",
          40
        },
        {
          "skin_hide",
          41
        },
        {
          "insp_skill",
          42
        },
        {
          "insp_lv_bonus",
          43
        },
        {
          "gallery_view",
          44
        }
      };
      this.____stringByteKeys = new byte[45][]
      {
        MessagePackBinary.GetEncodedStringBytes("Rarity"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("spec"),
        MessagePackBinary.GetEncodedStringBytes("asset"),
        MessagePackBinary.GetEncodedStringBytes("voice"),
        MessagePackBinary.GetEncodedStringBytes("icon"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("rareini"),
        MessagePackBinary.GetEncodedStringBytes("raremax"),
        MessagePackBinary.GetEncodedStringBytes("kakera"),
        MessagePackBinary.GetEncodedStringBytes("is_create"),
        MessagePackBinary.GetEncodedStringBytes("maxnum"),
        MessagePackBinary.GetEncodedStringBytes("skill"),
        MessagePackBinary.GetEncodedStringBytes("kcoin"),
        MessagePackBinary.GetEncodedStringBytes("tcoin"),
        MessagePackBinary.GetEncodedStringBytes("acoin"),
        MessagePackBinary.GetEncodedStringBytes("mcoin"),
        MessagePackBinary.GetEncodedStringBytes("pcoin"),
        MessagePackBinary.GetEncodedStringBytes("buy"),
        MessagePackBinary.GetEncodedStringBytes("sell"),
        MessagePackBinary.GetEncodedStringBytes("enhance_cost"),
        MessagePackBinary.GetEncodedStringBytes("skills"),
        MessagePackBinary.GetEncodedStringBytes("equip_effects"),
        MessagePackBinary.GetEncodedStringBytes("attack_effects"),
        MessagePackBinary.GetEncodedStringBytes("abil_inames"),
        MessagePackBinary.GetEncodedStringBytes("abil_conds"),
        MessagePackBinary.GetEncodedStringBytes("abil_levels"),
        MessagePackBinary.GetEncodedStringBytes("abil_rareties"),
        MessagePackBinary.GetEncodedStringBytes("abil_shows"),
        MessagePackBinary.GetEncodedStringBytes("tag"),
        MessagePackBinary.GetEncodedStringBytes("condition_lv"),
        MessagePackBinary.GetEncodedStringBytes("condition_units"),
        MessagePackBinary.GetEncodedStringBytes("condition_jobs"),
        MessagePackBinary.GetEncodedStringBytes("condition_birth"),
        MessagePackBinary.GetEncodedStringBytes("condition_sex"),
        MessagePackBinary.GetEncodedStringBytes("condition_element"),
        MessagePackBinary.GetEncodedStringBytes("condition_raremin"),
        MessagePackBinary.GetEncodedStringBytes("condition_raremax"),
        MessagePackBinary.GetEncodedStringBytes("CachedKakeraCount"),
        MessagePackBinary.GetEncodedStringBytes("CondSkillMotion"),
        MessagePackBinary.GetEncodedStringBytes("skin_hide"),
        MessagePackBinary.GetEncodedStringBytes("insp_skill"),
        MessagePackBinary.GetEncodedStringBytes("insp_lv_bonus"),
        MessagePackBinary.GetEncodedStringBytes("gallery_view")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ArtifactParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 45);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.Rarity, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.spec, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.asset, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.voice, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.icon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<ArtifactTypes>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rareini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.raremax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.kakera, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.is_create);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.maxnum);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kcoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tcoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.acoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mcoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pcoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.buy);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sell);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.enhance_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.skills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.equip_effects, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.attack_effects, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.abil_inames, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.abil_conds, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.abil_levels, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.abil_rareties, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.abil_shows, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.condition_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.condition_units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.condition_jobs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.condition_birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += formatterResolver.GetFormatterWithVerify<ESex>().Serialize(ref bytes, offset, value.condition_sex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<EElement>().Serialize(ref bytes, offset, value.condition_element, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.condition_raremin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.condition_raremax, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CachedKakeraCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CondSkillMotion, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.skin_hide);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.insp_skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.insp_lv_bonus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += formatterResolver.GetFormatterWithVerify<GalleryVisibilityType>().Serialize(ref bytes, offset, value.gallery_view, formatterResolver);
      return offset - num;
    }

    public ArtifactParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ArtifactParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      OInt oint1 = new OInt();
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      ArtifactTypes artifactTypes = ArtifactTypes.None;
      int num3 = 0;
      int num4 = 0;
      string str7 = (string) null;
      bool flag = false;
      int num5 = 0;
      string str8 = (string) null;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      int num12 = 0;
      int num13 = 0;
      string[] strArray1 = (string[]) null;
      string[] strArray2 = (string[]) null;
      string[] strArray3 = (string[]) null;
      string[] strArray4 = (string[]) null;
      string[] strArray5 = (string[]) null;
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
      int[] numArray3 = (int[]) null;
      string str9 = (string) null;
      int num14 = 0;
      string[] strArray6 = (string[]) null;
      string[] strArray7 = (string[]) null;
      string str10 = (string) null;
      ESex esex = ESex.Unknown;
      EElement eelement = EElement.None;
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      int num15 = 0;
      string str11 = (string) null;
      int num16 = 0;
      string str12 = (string) null;
      int num17 = 0;
      GalleryVisibilityType galleryVisibilityType = GalleryVisibilityType.None;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num18;
        if (!this.____keyMapping.TryGetValueSafe(key, out num18))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num18)
          {
            case 0:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              artifactTypes = formatterResolver.GetFormatterWithVerify<ArtifactTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 12:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              strArray4 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              strArray5 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 30:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              strArray6 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              strArray7 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 35:
              esex = formatterResolver.GetFormatterWithVerify<ESex>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 36:
              eelement = formatterResolver.GetFormatterWithVerify<EElement>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 39:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 41:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 42:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              galleryVisibilityType = formatterResolver.GetFormatterWithVerify<GalleryVisibilityType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ArtifactParam()
      {
        iname = str1,
        name = str2,
        spec = str3,
        asset = str4,
        voice = str5,
        icon = str6,
        type = artifactTypes,
        rareini = num3,
        raremax = num4,
        kakera = str7,
        is_create = flag,
        maxnum = num5,
        skill = str8,
        kcoin = num6,
        tcoin = num7,
        acoin = num8,
        mcoin = num9,
        pcoin = num10,
        buy = num11,
        sell = num12,
        enhance_cost = num13,
        skills = strArray1,
        equip_effects = strArray2,
        attack_effects = strArray3,
        abil_inames = strArray4,
        abil_conds = strArray5,
        abil_levels = numArray1,
        abil_rareties = numArray2,
        abil_shows = numArray3,
        tag = str9,
        condition_lv = num14,
        condition_units = strArray6,
        condition_jobs = strArray7,
        condition_birth = str10,
        condition_sex = esex,
        condition_element = eelement,
        condition_raremin = oint2,
        condition_raremax = oint3,
        CachedKakeraCount = num15,
        CondSkillMotion = str11,
        skin_hide = num16,
        insp_skill = str12,
        insp_lv_bonus = num17,
        gallery_view = galleryVisibilityType
      };
    }
  }
}
