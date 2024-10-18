// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.EmbeddedTutorialMasterParams_JSON_EmbededMasterParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class EmbeddedTutorialMasterParams_JSON_EmbededMasterParamFormatter : 
    IMessagePackFormatter<EmbeddedTutorialMasterParams.JSON_EmbededMasterParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public EmbeddedTutorialMasterParams_JSON_EmbededMasterParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Unit",
          0
        },
        {
          "JobSet",
          1
        },
        {
          "Job",
          2
        },
        {
          "Artifact",
          3
        },
        {
          "Ability",
          4
        },
        {
          "Skill",
          5
        },
        {
          "Buff",
          6
        },
        {
          "Trick",
          7
        },
        {
          "Cond",
          8
        },
        {
          "Grow",
          9
        },
        {
          "AI",
          10
        },
        {
          "Item",
          11
        },
        {
          "UnitJobOverwrite",
          12
        },
        {
          "Weapon",
          13
        },
        {
          "Tips",
          14
        },
        {
          "Geo",
          15
        },
        {
          "Fix",
          16
        },
        {
          "Unlock",
          17
        },
        {
          "Rarity",
          18
        },
        {
          "Player",
          19
        },
        {
          "UnitLvTbl",
          20
        },
        {
          "PlayerLvTbl",
          21
        }
      };
      this.____stringByteKeys = new byte[22][]
      {
        MessagePackBinary.GetEncodedStringBytes("Unit"),
        MessagePackBinary.GetEncodedStringBytes("JobSet"),
        MessagePackBinary.GetEncodedStringBytes("Job"),
        MessagePackBinary.GetEncodedStringBytes("Artifact"),
        MessagePackBinary.GetEncodedStringBytes("Ability"),
        MessagePackBinary.GetEncodedStringBytes("Skill"),
        MessagePackBinary.GetEncodedStringBytes("Buff"),
        MessagePackBinary.GetEncodedStringBytes("Trick"),
        MessagePackBinary.GetEncodedStringBytes("Cond"),
        MessagePackBinary.GetEncodedStringBytes("Grow"),
        MessagePackBinary.GetEncodedStringBytes("AI"),
        MessagePackBinary.GetEncodedStringBytes("Item"),
        MessagePackBinary.GetEncodedStringBytes("UnitJobOverwrite"),
        MessagePackBinary.GetEncodedStringBytes("Weapon"),
        MessagePackBinary.GetEncodedStringBytes("Tips"),
        MessagePackBinary.GetEncodedStringBytes("Geo"),
        MessagePackBinary.GetEncodedStringBytes("Fix"),
        MessagePackBinary.GetEncodedStringBytes("Unlock"),
        MessagePackBinary.GetEncodedStringBytes("Rarity"),
        MessagePackBinary.GetEncodedStringBytes("Player"),
        MessagePackBinary.GetEncodedStringBytes("UnitLvTbl"),
        MessagePackBinary.GetEncodedStringBytes("PlayerLvTbl")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      EmbeddedTutorialMasterParams.JSON_EmbededMasterParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 22);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitParam[]>().Serialize(ref bytes, offset, value.Unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_JobSetParam[]>().Serialize(ref bytes, offset, value.JobSet, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_JobParam[]>().Serialize(ref bytes, offset, value.Job, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ArtifactParam[]>().Serialize(ref bytes, offset, value.Artifact, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AbilityParam[]>().Serialize(ref bytes, offset, value.Ability, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SkillParam[]>().Serialize(ref bytes, offset, value.Skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_BuffEffectParam[]>().Serialize(ref bytes, offset, value.Buff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrickParam[]>().Serialize(ref bytes, offset, value.Trick, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_CondEffectParam[]>().Serialize(ref bytes, offset, value.Cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GrowParam[]>().Serialize(ref bytes, offset, value.Grow, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_AIParam[]>().Serialize(ref bytes, offset, value.AI, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ItemParam[]>().Serialize(ref bytes, offset, value.Item, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitJobOverwriteParam[]>().Serialize(ref bytes, offset, value.UnitJobOverwrite, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WeaponParam[]>().Serialize(ref bytes, offset, value.Weapon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TipsParam[]>().Serialize(ref bytes, offset, value.Tips, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GeoParam[]>().Serialize(ref bytes, offset, value.Geo, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_FixParam[]>().Serialize(ref bytes, offset, value.Fix, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnlockParam[]>().Serialize(ref bytes, offset, value.Unlock, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RarityParam[]>().Serialize(ref bytes, offset, value.Rarity, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_PlayerParam[]>().Serialize(ref bytes, offset, value.Player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.UnitLvTbl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.PlayerLvTbl, formatterResolver);
      return offset - num;
    }

    public EmbeddedTutorialMasterParams.JSON_EmbededMasterParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (EmbeddedTutorialMasterParams.JSON_EmbededMasterParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_UnitParam[] jsonUnitParamArray = (JSON_UnitParam[]) null;
      JSON_JobSetParam[] jsonJobSetParamArray = (JSON_JobSetParam[]) null;
      JSON_JobParam[] jsonJobParamArray = (JSON_JobParam[]) null;
      JSON_ArtifactParam[] jsonArtifactParamArray = (JSON_ArtifactParam[]) null;
      JSON_AbilityParam[] jsonAbilityParamArray = (JSON_AbilityParam[]) null;
      JSON_SkillParam[] jsonSkillParamArray = (JSON_SkillParam[]) null;
      JSON_BuffEffectParam[] jsonBuffEffectParamArray = (JSON_BuffEffectParam[]) null;
      JSON_TrickParam[] jsonTrickParamArray = (JSON_TrickParam[]) null;
      JSON_CondEffectParam[] jsonCondEffectParamArray = (JSON_CondEffectParam[]) null;
      JSON_GrowParam[] jsonGrowParamArray = (JSON_GrowParam[]) null;
      JSON_AIParam[] jsonAiParamArray = (JSON_AIParam[]) null;
      JSON_ItemParam[] jsonItemParamArray = (JSON_ItemParam[]) null;
      JSON_UnitJobOverwriteParam[] jobOverwriteParamArray = (JSON_UnitJobOverwriteParam[]) null;
      JSON_WeaponParam[] jsonWeaponParamArray = (JSON_WeaponParam[]) null;
      JSON_TipsParam[] jsonTipsParamArray = (JSON_TipsParam[]) null;
      JSON_GeoParam[] jsonGeoParamArray = (JSON_GeoParam[]) null;
      JSON_FixParam[] jsonFixParamArray = (JSON_FixParam[]) null;
      JSON_UnlockParam[] jsonUnlockParamArray = (JSON_UnlockParam[]) null;
      JSON_RarityParam[] jsonRarityParamArray = (JSON_RarityParam[]) null;
      JSON_PlayerParam[] jsonPlayerParamArray = (JSON_PlayerParam[]) null;
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
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
              jsonUnitParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonJobSetParamArray = formatterResolver.GetFormatterWithVerify<JSON_JobSetParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonJobParamArray = formatterResolver.GetFormatterWithVerify<JSON_JobParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonArtifactParamArray = formatterResolver.GetFormatterWithVerify<JSON_ArtifactParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonAbilityParamArray = formatterResolver.GetFormatterWithVerify<JSON_AbilityParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonSkillParamArray = formatterResolver.GetFormatterWithVerify<JSON_SkillParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonBuffEffectParamArray = formatterResolver.GetFormatterWithVerify<JSON_BuffEffectParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonTrickParamArray = formatterResolver.GetFormatterWithVerify<JSON_TrickParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonCondEffectParamArray = formatterResolver.GetFormatterWithVerify<JSON_CondEffectParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              jsonGrowParamArray = formatterResolver.GetFormatterWithVerify<JSON_GrowParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              jsonAiParamArray = formatterResolver.GetFormatterWithVerify<JSON_AIParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              jsonItemParamArray = formatterResolver.GetFormatterWithVerify<JSON_ItemParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              jobOverwriteParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnitJobOverwriteParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              jsonWeaponParamArray = formatterResolver.GetFormatterWithVerify<JSON_WeaponParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              jsonTipsParamArray = formatterResolver.GetFormatterWithVerify<JSON_TipsParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              jsonGeoParamArray = formatterResolver.GetFormatterWithVerify<JSON_GeoParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              jsonFixParamArray = formatterResolver.GetFormatterWithVerify<JSON_FixParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              jsonUnlockParamArray = formatterResolver.GetFormatterWithVerify<JSON_UnlockParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              jsonRarityParamArray = formatterResolver.GetFormatterWithVerify<JSON_RarityParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              jsonPlayerParamArray = formatterResolver.GetFormatterWithVerify<JSON_PlayerParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new EmbeddedTutorialMasterParams.JSON_EmbededMasterParam()
      {
        Unit = jsonUnitParamArray,
        JobSet = jsonJobSetParamArray,
        Job = jsonJobParamArray,
        Artifact = jsonArtifactParamArray,
        Ability = jsonAbilityParamArray,
        Skill = jsonSkillParamArray,
        Buff = jsonBuffEffectParamArray,
        Trick = jsonTrickParamArray,
        Cond = jsonCondEffectParamArray,
        Grow = jsonGrowParamArray,
        AI = jsonAiParamArray,
        Item = jsonItemParamArray,
        UnitJobOverwrite = jobOverwriteParamArray,
        Weapon = jsonWeaponParamArray,
        Tips = jsonTipsParamArray,
        Geo = jsonGeoParamArray,
        Fix = jsonFixParamArray,
        Unlock = jsonUnlockParamArray,
        Rarity = jsonRarityParamArray,
        Player = jsonPlayerParamArray,
        UnitLvTbl = numArray1,
        PlayerLvTbl = numArray2
      };
    }
  }
}
