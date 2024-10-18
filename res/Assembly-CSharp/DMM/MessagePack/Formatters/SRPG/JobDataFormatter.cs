// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JobDataFormatter
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
  public sealed class JobDataFormatter : IMessagePackFormatter<JobData>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JobDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Owner",
          0
        },
        {
          "Param",
          1
        },
        {
          "UniqueID",
          2
        },
        {
          "JobID",
          3
        },
        {
          "Name",
          4
        },
        {
          "Rank",
          5
        },
        {
          "JobType",
          6
        },
        {
          "RoleType",
          7
        },
        {
          "JobResourceID",
          8
        },
        {
          "Equips",
          9
        },
        {
          "LearnAbilitys",
          10
        },
        {
          "AbilitySlots",
          11
        },
        {
          "Artifacts",
          12
        },
        {
          "ArtifactDatas",
          13
        },
        {
          "SelectedSkin",
          14
        },
        {
          "SelectSkinData",
          15
        },
        {
          "IsActivated",
          16
        },
        {
          "JobMaster",
          17
        }
      };
      this.____stringByteKeys = new byte[18][]
      {
        MessagePackBinary.GetEncodedStringBytes("Owner"),
        MessagePackBinary.GetEncodedStringBytes("Param"),
        MessagePackBinary.GetEncodedStringBytes("UniqueID"),
        MessagePackBinary.GetEncodedStringBytes("JobID"),
        MessagePackBinary.GetEncodedStringBytes("Name"),
        MessagePackBinary.GetEncodedStringBytes("Rank"),
        MessagePackBinary.GetEncodedStringBytes("JobType"),
        MessagePackBinary.GetEncodedStringBytes("RoleType"),
        MessagePackBinary.GetEncodedStringBytes("JobResourceID"),
        MessagePackBinary.GetEncodedStringBytes("Equips"),
        MessagePackBinary.GetEncodedStringBytes("LearnAbilitys"),
        MessagePackBinary.GetEncodedStringBytes("AbilitySlots"),
        MessagePackBinary.GetEncodedStringBytes("Artifacts"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactDatas"),
        MessagePackBinary.GetEncodedStringBytes("SelectedSkin"),
        MessagePackBinary.GetEncodedStringBytes("SelectSkinData"),
        MessagePackBinary.GetEncodedStringBytes("IsActivated"),
        MessagePackBinary.GetEncodedStringBytes("JobMaster")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JobData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 18);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<UnitData>().Serialize(ref bytes, offset, value.Owner, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JobParam>().Serialize(ref bytes, offset, value.Param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.UniqueID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.JobID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Rank);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<JobTypes>().Serialize(ref bytes, offset, value.JobType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<RoleTypes>().Serialize(ref bytes, offset, value.RoleType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.JobResourceID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<EquipData[]>().Serialize(ref bytes, offset, value.Equips, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Serialize(ref bytes, offset, value.LearnAbilitys, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<long[]>().Serialize(ref bytes, offset, value.AbilitySlots, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<long[]>().Serialize(ref bytes, offset, value.Artifacts, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<ArtifactData[]>().Serialize(ref bytes, offset, value.ArtifactDatas, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SelectedSkin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<ArtifactData>().Serialize(ref bytes, offset, value.SelectSkinData, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsActivated);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.JobMaster, formatterResolver);
      return offset - num;
    }

    public JobData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JobData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      UnitData unitData = (UnitData) null;
      JobParam jobParam = (JobParam) null;
      long num3 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      int num4 = 0;
      JobTypes jobTypes = JobTypes.None;
      RoleTypes roleTypes = RoleTypes.None;
      string str3 = (string) null;
      EquipData[] equipDataArray = (EquipData[]) null;
      List<AbilityData> abilityDataList = (List<AbilityData>) null;
      long[] numArray1 = (long[]) null;
      long[] numArray2 = (long[]) null;
      ArtifactData[] artifactDataArray = (ArtifactData[]) null;
      string str4 = (string) null;
      ArtifactData artifactData = (ArtifactData) null;
      bool flag = false;
      SkillData skillData = (SkillData) null;
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
              unitData = formatterResolver.GetFormatterWithVerify<UnitData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jobParam = formatterResolver.GetFormatterWithVerify<JobParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 3:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              jobTypes = formatterResolver.GetFormatterWithVerify<JobTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              roleTypes = formatterResolver.GetFormatterWithVerify<RoleTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              equipDataArray = formatterResolver.GetFormatterWithVerify<EquipData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              abilityDataList = formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              numArray1 = formatterResolver.GetFormatterWithVerify<long[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              numArray2 = formatterResolver.GetFormatterWithVerify<long[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              artifactDataArray = formatterResolver.GetFormatterWithVerify<ArtifactData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              artifactData = formatterResolver.GetFormatterWithVerify<ArtifactData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 17:
              skillData = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JobData()
      {
        UniqueID = num3,
        SelectedSkin = str4,
        SelectSkinData = artifactData
      };
    }
  }
}
