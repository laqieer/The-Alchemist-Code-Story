// Decompiled with JetBrains decompiler
// Type: SRPG.PartyOverWrite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class PartyOverWrite
  {
    private eOverWritePartyType mPatryType;
    private UnitOverWriteData[] mOverWriteDatas;

    public eOverWritePartyType PatryType => this.mPatryType;

    public UnitOverWriteData[] Units => this.mOverWriteDatas;

    public void Deserialize(JSON_PartyOverWrite json)
    {
      this.mPatryType = UnitOverWriteUtility.String2OverWritePartyType(json.ptype);
      this.mOverWriteDatas = (UnitOverWriteData[]) null;
      if (json == null || json.unit_ow == null)
        return;
      this.mOverWriteDatas = new UnitOverWriteData[json.unit_ow.Length];
      for (int index = 0; index < json.unit_ow.Length; ++index)
      {
        UnitOverWriteData unitOverWriteData = new UnitOverWriteData();
        unitOverWriteData.Deserialize(json.unit_ow[index]);
        this.mOverWriteDatas[index] = unitOverWriteData;
      }
    }
  }
}
