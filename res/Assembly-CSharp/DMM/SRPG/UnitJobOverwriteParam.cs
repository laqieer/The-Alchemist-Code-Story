// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobOverwriteParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class UnitJobOverwriteParam
  {
    private StatusParam status;
    public string mUnitIname;
    public string mJobIname;
    public int mAvoid;
    public int mInimp;

    public StatusParam mStatus => this.status;

    public bool Deserialize(JSON_UnitJobOverwriteParam json)
    {
      if (json == null)
        return false;
      this.mUnitIname = json.unit_iname;
      this.mJobIname = json.job_iname;
      this.mAvoid = json.avoid;
      this.mInimp = json.inimp;
      this.status = new StatusParam();
      this.status.hp = (OInt) json.hp;
      this.status.mp = (OShort) json.mp;
      this.status.atk = (OShort) json.atk;
      this.status.def = (OShort) json.def;
      this.status.mag = (OShort) json.mag;
      this.status.mnd = (OShort) json.mnd;
      this.status.dex = (OShort) json.dex;
      this.status.spd = (OShort) json.spd;
      this.status.cri = (OShort) json.cri;
      this.status.luk = (OShort) json.luk;
      return true;
    }
  }
}
