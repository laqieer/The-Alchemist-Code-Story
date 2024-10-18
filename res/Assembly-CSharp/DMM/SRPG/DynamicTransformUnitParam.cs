// Decompiled with JetBrains decompiler
// Type: SRPG.DynamicTransformUnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
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

    public string Iname => this.mIname;

    public string TrUnitId => this.mTrUnitId;

    public int Turn => this.mTurn;

    public string UpperToAbId => this.mUpperToAbId;

    public string LowerToAbId => this.mLowerToAbId;

    public string ReactToAbId => this.mReactToAbId;

    public string CancelEffect => this.mCancelEffect;

    public int CancelDisMs => this.mCancelDisMs;

    public int CancelAppMs => this.mCancelAppMs;

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

    public bool IsInheritSkin
    {
      get
      {
        return (this.mFlags & DynamicTransformUnitParam.Flags.IsInheritSkin) != (DynamicTransformUnitParam.Flags) 0;
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
      if (json.is_cc_hpf != 0)
        this.mFlags |= DynamicTransformUnitParam.Flags.IsCancelHpFull;
      if (json.is_inh_skin == 0)
        return;
      this.mFlags |= DynamicTransformUnitParam.Flags.IsInheritSkin;
    }

    public enum Flags
    {
      IsNoWeaponAbility = 1,
      IsNoVisionAbility = 2,
      IsNoItems = 4,
      IsTransHpFull = 8,
      IsCancelHpFull = 16, // 0x00000010
      IsInheritSkin = 32, // 0x00000020
    }
  }
}
