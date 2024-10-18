// Decompiled with JetBrains decompiler
// Type: SRPG.TrickParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TrickParam
  {
    private string mIname;
    private string mName;
    private string mExpr;
    private eTrickDamageType mDamageType;
    private OInt mDamageVal;
    private SkillParamCalcTypes mCalcType;
    private EElement mElem;
    private AttackDetailTypes mAttackDetail;
    private string mBuffId;
    private string mCondId;
    private OInt mKnockBackRate;
    private OInt mKnockBackVal;
    private ESkillTarget mTarget;
    private eTrickVisualType mVisualType;
    private OInt mActionCount;
    private OInt mValidClock;
    private string mMarkerName;
    private string mEffectName;
    private ESkillTarget mEffTarget;
    private ESelectType mEffShape;
    private OInt mEffScope;
    private OInt mEffHeight;
    private eMovType[] mIgnoreMovTypes;
    private EnumBitArray<TrickParam.eFlag> mFlag = new EnumBitArray<TrickParam.eFlag>();

    public string Iname => this.mIname;

    public string Name => this.mName;

    public string Expr => this.mExpr;

    public eTrickDamageType DamageType => this.mDamageType;

    public OInt DamageVal => this.mDamageVal;

    public SkillParamCalcTypes CalcType => this.mCalcType;

    public EElement Elem => this.mElem;

    public AttackDetailTypes AttackDetail => this.mAttackDetail;

    public string BuffId => this.mBuffId;

    public string CondId => this.mCondId;

    public OInt KnockBackRate => this.mKnockBackRate;

    public OInt KnockBackVal => this.mKnockBackVal;

    public ESkillTarget Target => this.mTarget;

    public eTrickVisualType VisualType => this.mVisualType;

    public OInt ActionCount => this.mActionCount;

    public OInt ValidClock => this.mValidClock;

    public bool IsNoOverWrite => this.mFlag.Get(TrickParam.eFlag.IS_NO_OVER_WRITE);

    public string MarkerName => this.mMarkerName;

    public string EffectName => this.mEffectName;

    public ESkillTarget EffTarget => this.mEffTarget;

    public ESelectType EffShape => this.mEffShape;

    public OInt EffScope => this.mEffScope;

    public OInt EffHeight => this.mEffHeight;

    public bool IsAreaEff => (int) this.mEffScope != 0;

    public List<eMovType> IgnoreMovTypeList
    {
      get
      {
        return this.mIgnoreMovTypes != null ? new List<eMovType>((IEnumerable<eMovType>) this.mIgnoreMovTypes) : new List<eMovType>();
      }
    }

    public bool IsReinforcement => this.mFlag.Get(TrickParam.eFlag.IS_REINFORCEMENT);

    public void Deserialize(JSON_TrickParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mExpr = json.expr;
      this.mDamageType = (eTrickDamageType) json.dmg_type;
      this.mDamageVal = (OInt) json.dmg_val;
      this.mCalcType = (SkillParamCalcTypes) json.calc;
      this.mElem = (EElement) json.elem;
      this.mAttackDetail = (AttackDetailTypes) json.atk_det;
      this.mBuffId = json.buff;
      this.mCondId = json.cond;
      this.mKnockBackRate = (OInt) json.kb_rate;
      this.mKnockBackVal = (OInt) json.kb_val;
      this.mTarget = (ESkillTarget) json.target;
      this.mVisualType = (eTrickVisualType) json.visual;
      this.mActionCount = (OInt) json.count;
      this.mValidClock = (OInt) json.clock;
      this.mMarkerName = json.marker;
      this.mEffectName = json.effect;
      this.mEffTarget = (ESkillTarget) json.eff_target;
      this.mEffShape = (ESelectType) json.eff_shape;
      this.mEffScope = (OInt) json.eff_scope;
      this.mEffHeight = (OInt) json.eff_height;
      this.mIgnoreMovTypes = (eMovType[]) null;
      int length = Math.Min(json.ig_mt_num, json.ig_mts == null ? 0 : json.ig_mts.Length);
      if (length != 0)
      {
        this.mIgnoreMovTypes = new eMovType[length];
        for (int index = 0; index < length; ++index)
          this.mIgnoreMovTypes[index] = (eMovType) json.ig_mts[index];
      }
      this.mFlag.SetAll(false);
      this.mFlag.Set(TrickParam.eFlag.IS_NO_OVER_WRITE, json.is_no_ow != 0);
      this.mFlag.Set(TrickParam.eFlag.IS_REINFORCEMENT, json.is_rein != 0);
    }

    private enum eFlag
    {
      IS_NO_OVER_WRITE,
      IS_REINFORCEMENT,
      MAX,
    }
  }
}
