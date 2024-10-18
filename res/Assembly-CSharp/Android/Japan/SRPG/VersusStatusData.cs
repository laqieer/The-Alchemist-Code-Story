// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStatusData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusStatusData
  {
    public int Hp;
    public int Atk;
    public int Def;
    public int Matk;
    public int Mdef;
    public int Dex;
    public int Spd;
    public int Cri;
    public int Luck;
    public int Cmb;
    public int Move;
    public int Jmp;

    public void Add(StatusParam status, int comb)
    {
      this.Hp += (int) status.hp;
      this.Atk += (int) status.atk;
      this.Def += (int) status.def;
      this.Matk += (int) status.mag;
      this.Mdef += (int) status.mnd;
      this.Dex += (int) status.dex;
      this.Spd += (int) status.spd;
      this.Cri += (int) status.cri;
      this.Luck += (int) status.luk;
      this.Move += (int) status.mov;
      this.Jmp += (int) status.jmp;
      this.Cmb += comb;
    }
  }
}
