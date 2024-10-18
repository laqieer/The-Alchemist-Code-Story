// Decompiled with JetBrains decompiler
// Type: SRPG.EvaluationParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class EvaluationParam
  {
    public string iname;
    public OInt value;
    public StatusParam status = new StatusParam();

    public bool Deserialize(JSON_EvaluationParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.value = (OInt) json.val;
      this.status.Clear();
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
