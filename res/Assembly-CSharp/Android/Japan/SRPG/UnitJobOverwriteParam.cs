// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobOverwriteParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class UnitJobOverwriteParam
  {
    private StatusParam status;
    public string mUnitIname;
    public string mJobIname;
    public int mAvoid;
    public int mInimp;

    public StatusParam mStatus
    {
      get
      {
        return this.status;
      }
    }

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
