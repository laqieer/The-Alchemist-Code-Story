// Decompiled with JetBrains decompiler
// Type: SRPG.StatusCoefficientParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class StatusCoefficientParam
  {
    private float mHP;
    private float mAttack;
    private float mDefense;
    private float mMagAttack;
    private float mMagDefense;
    private float mDex;
    private float mSpeed;
    private float mCritical;
    private float mLuck;
    private float mCombo;
    private float mMove;
    private float mJump;

    public float HP => this.mHP;

    public float Attack => this.mAttack;

    public float Defense => this.mDefense;

    public float MagAttack => this.mMagAttack;

    public float MagDefense => this.mMagDefense;

    public float Dex => this.mDex;

    public float Speed => this.mSpeed;

    public float Critical => this.mCritical;

    public float Luck => this.mLuck;

    public float Combo => this.mCombo;

    public float Move => this.mMove;

    public float Jump => this.mJump;

    public void Deserialize(JSON_StatusCoefficientParam json)
    {
      if (json == null)
        return;
      this.mHP = json.hp;
      this.mAttack = json.atk;
      this.mDefense = json.def;
      this.mMagAttack = json.matk;
      this.mMagDefense = json.mdef;
      this.mDex = json.dex;
      this.mSpeed = json.spd;
      this.mCritical = json.cri;
      this.mLuck = json.luck;
      this.mCombo = json.cmb;
      this.mMove = json.move;
      this.mJump = json.jmp;
    }

    public static int CalcTotalStatus(UnitData unit)
    {
      return 0 + (int) ((double) (int) unit.Status.param.hp * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.HP) + (int) ((double) (int) unit.Status.param.atk * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Attack) + (int) ((double) (int) unit.Status.param.def * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Defense) + (int) ((double) (int) unit.Status.param.mag * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagAttack) + (int) ((double) (int) unit.Status.param.mnd * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagDefense) + (int) ((double) (int) unit.Status.param.dex * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Dex) + (int) ((double) (int) unit.Status.param.spd * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Speed) + (int) ((double) (int) unit.Status.param.cri * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Critical) + (int) ((double) (int) unit.Status.param.luk * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Luck) + (int) ((double) unit.GetCombination() * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Combo) + (int) ((double) (int) unit.Status.param.mov * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Move) + (int) ((double) (int) unit.Status.param.jmp * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Jump);
    }
  }
}
