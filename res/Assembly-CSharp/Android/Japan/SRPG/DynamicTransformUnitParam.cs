// Decompiled with JetBrains decompiler
// Type: SRPG.DynamicTransformUnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class DynamicTransformUnitParam
  {
    private string mIname;
    private string mTrUnitId;
    private int mTurn;
    private string mUpperToAbId;
    private string mLowerToAbId;
    private string mReactToAbId;
    private string mCancelEffect;
    private int mCancelDisMs;
    private int mCancelAppMs;
    private DynamicTransformUnitParam.Flags mFlags;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public string TrUnitId
    {
      get
      {
        return this.mTrUnitId;
      }
    }

    public int Turn
    {
      get
      {
        return this.mTurn;
      }
    }

    public string UpperToAbId
    {
      get
      {
        return this.mUpperToAbId;
      }
    }

    public string LowerToAbId
    {
      get
      {
        return this.mLowerToAbId;
      }
    }

    public string ReactToAbId
    {
      get
      {
        return this.mReactToAbId;
      }
    }

    public string CancelEffect
    {
      get
      {
        return this.mCancelEffect;
      }
    }

    public int CancelDisMs
    {
      get
      {
        return this.mCancelDisMs;
      }
    }

    public int CancelAppMs
    {
      get
      {
        return this.mCancelAppMs;
      }
    }

    public bool IsNoWeaponAbility
    {
      get
      {
        return (this.mFlags & DynamicTransformUnitParam.Flags.IsNoWeaponAbility) != (DynamicTransformUnitParam.Flags) 0;
      }
    }

    public bool IsNoVisionAbility
    {
      get
      {
        return (this.mFlags & DynamicTransformUnitParam.Flags.IsNoVisionAbility) != (DynamicTransformUnitParam.Flags) 0;
      }
    }

    public bool IsNoItems
    {
      get
      {
        return (this.mFlags & DynamicTransformUnitParam.Flags.IsNoItems) != (DynamicTransformUnitParam.Flags) 0;
      }
    }

    public bool IsTransHpFull
    {
      get
      {
        return (this.mFlags & DynamicTransformUnitParam.Flags.IsTransHpFull) != (DynamicTransformUnitParam.Flags) 0;
      }
    }

    public bool IsCancelHpFull
    {
      get
      {
        return (this.mFlags & DynamicTransformUnitParam.Flags.IsCancelHpFull) != (DynamicTransformUnitParam.Flags) 0;
      }
    }

    public void Deserialize(JSON_DynamicTransformUnitParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mTrUnitId = json.tr_unit_id;
      this.mTurn = json.turn;
      this.mUpperToAbId = json.upper_to_abid;
      this.mLowerToAbId = json.lower_to_abid;
      this.mReactToAbId = json.react_to_abid;
      this.mCancelEffect = json.ct_eff;
      this.mCancelDisMs = json.ct_dis_ms;
      this.mCancelAppMs = json.ct_app_ms;
      this.mFlags = (DynamicTransformUnitParam.Flags) 0;
      if (json.is_no_wa != 0)
        this.mFlags |= DynamicTransformUnitParam.Flags.IsNoWeaponAbility;
      if (json.is_no_va != 0)
        this.mFlags |= DynamicTransformUnitParam.Flags.IsNoVisionAbility;
      if (json.is_no_item != 0)
        this.mFlags |= DynamicTransformUnitParam.Flags.IsNoItems;
      if (json.is_tr_hpf != 0)
        this.mFlags |= DynamicTransformUnitParam.Flags.IsTransHpFull;
      if (json.is_cc_hpf == 0)
        return;
      this.mFlags |= DynamicTransformUnitParam.Flags.IsCancelHpFull;
    }

    public enum Flags
    {
      IsNoWeaponAbility = 1,
      IsNoVisionAbility = 2,
      IsNoItems = 4,
      IsTransHpFull = 8,
      IsCancelHpFull = 16, // 0x00000010
    }
  }
}
