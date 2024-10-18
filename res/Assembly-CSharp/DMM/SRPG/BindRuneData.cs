// Decompiled with JetBrains decompiler
// Type: SRPG.BindRuneData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class BindRuneData
  {
    public long iid;
    public bool is_selected;
    public bool is_disable;
    public bool is_check;
    public bool is_owner_disable;
    public bool is_use_copy;
    private RuneData CopyRune;

    public BindRuneData()
    {
    }

    public BindRuneData(long _iid) => this.iid = _iid;

    public RuneData Rune
    {
      get
      {
        return this.is_use_copy ? this.CopyRune : MonoSingleton<GameManager>.Instance.Player.FindRuneByUniqueID(this.iid);
      }
    }

    public RuneParam RuneParam => this.Rune?.RuneParam;

    public RuneMaterial RuneMaterialList => this.Rune?.RuneMaterialList;

    public RuneCost EnhanceCost => this.Rune?.EnhanceCost;

    public int DisassemblyZeny
    {
      get
      {
        RuneData rune = this.Rune;
        return rune == null ? 0 : rune.DisassemblyZeny;
      }
    }

    public RuneCost EvoCost => this.Rune?.EvoCost;

    public RuneCost[] ResetParamBaseCost => this.Rune?.ResetParamBaseCost;

    public RuneCost[] ResetStatusEvoCost => this.Rune?.ResetStatusEvoCost;

    public RuneCost[] ParamEnhEvoCost => this.Rune?.ParamEnhEvoCost;

    public UnitData GetOwner() => this.Rune?.GetOwner();

    public RuneMaterial RuneMaterial => this.Rune?.RuneMaterial;

    public UnitData UnitData => this.Rune?.UnitData;

    public int EnhanceNum
    {
      get
      {
        RuneData rune = this.Rune;
        return rune == null ? 0 : rune.EnhanceNum;
      }
    }

    public bool IsEvoNext
    {
      get
      {
        RuneData rune = this.Rune;
        return rune != null && rune.IsEvoNext;
      }
    }

    public bool IsCanEvo
    {
      get
      {
        RuneData rune = this.Rune;
        return rune != null && rune.IsCanEvo;
      }
    }

    public int EvoNum
    {
      get
      {
        RuneData rune = this.Rune;
        return rune == null ? 0 : rune.EvoNum;
      }
    }

    public ItemParam Item => this.Rune?.Item;

    public int Rarity
    {
      get
      {
        RuneData rune = this.Rune;
        return rune == null ? 0 : rune.Rarity;
      }
    }

    public bool IsFavorite
    {
      get
      {
        RuneData rune = this.Rune;
        return rune != null && rune.IsFavorite;
      }
    }

    public BindRuneData CreateCopyRune()
    {
      BindRuneData copyRune = new BindRuneData();
      copyRune.iid = this.iid;
      copyRune.is_selected = this.is_selected;
      copyRune.is_disable = this.is_disable;
      copyRune.is_check = this.is_check;
      copyRune.is_owner_disable = this.is_owner_disable;
      RuneData rune = this.Rune;
      if (rune != null)
      {
        copyRune.CopyRune = new RuneData();
        copyRune.CopyRune.Deserialize(rune.Serialize());
        copyRune.is_use_copy = true;
      }
      return copyRune;
    }

    public void ResetOptionParam()
    {
      this.is_selected = false;
      this.is_disable = false;
      this.is_check = false;
    }
  }
}
