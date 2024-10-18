// Decompiled with JetBrains decompiler
// Type: SRPG.EquipData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class EquipData
  {
    private long mUniqueID;
    private ItemParam mItemParam;
    private RarityParam mRarityParam;
    private int mExp;
    private int mRank;
    private SkillData mSkill;
    private bool mEquiped;

    public long UniqueID => this.mUniqueID;

    public ItemParam ItemParam => this.mItemParam;

    public RarityParam RarityParam => this.mRarityParam;

    public string ItemID => this.mItemParam != null ? this.mItemParam.iname : (string) null;

    public int Rank => this.mRank;

    public EItemType ItemType => this.mItemParam != null ? this.mItemParam.type : EItemType.Used;

    public int Rarity => this.mItemParam != null ? this.mItemParam.rare : 0;

    public int Exp => this.mExp;

    public SkillData Skill => this.mSkill;

    public bool Setup(string item_iname)
    {
      this.Reset();
      if (string.IsNullOrEmpty(item_iname))
        return false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mItemParam = instance.GetItemParam(item_iname);
      this.mRarityParam = instance.GetRarityParam(this.mItemParam.rare);
      if (!string.IsNullOrEmpty(this.mItemParam.skill))
      {
        int rank = this.CalcRank();
        this.mSkill = new SkillData();
        this.mSkill.Setup(this.mItemParam.skill, rank, (int) this.mRarityParam.EquipEnhanceParam.rankcap);
      }
      return true;
    }

    public void Reset()
    {
      this.mUniqueID = 0L;
      this.mItemParam = (ItemParam) null;
      this.mExp = 0;
      this.mRank = 1;
      this.mSkill = (SkillData) null;
      this.mEquiped = false;
    }

    public void Equip(Json_Equip json)
    {
      if (json == null)
        return;
      this.Equip(json.iname, json.iid, json.exp);
    }

    public void Equip(string iname, long iid, int exp)
    {
      if (!this.IsValid() || this.mItemParam.iname != iname || iid == 0L)
        return;
      this.mUniqueID = iid;
      this.mExp = exp;
      this.mRank = this.CalcRank();
      this.mEquiped = true;
      if (this.mSkill == null && !string.IsNullOrEmpty(this.mItemParam.skill))
        this.mSkill = new SkillData();
      if (this.mSkill == null)
        return;
      this.mSkill.Setup(this.mItemParam.skill, this.mRank, this.GetRankCap());
    }

    public bool IsValid() => this.mItemParam != null;

    public bool IsEquiped() => this.mEquiped;

    public int GetRankCap()
    {
      return this.mRarityParam != null ? (int) this.RarityParam.EquipEnhanceParam.rankcap : 1;
    }

    public int GetNextExp(int rank)
    {
      RarityEquipEnhanceParam equipEnhanceParam = this.RarityParam == null ? (RarityEquipEnhanceParam) null : this.RarityParam.EquipEnhanceParam;
      DebugUtility.Assert((rank <= 0 ? 0 : (rank <= (int) equipEnhanceParam.rankcap ? 1 : 0)) != 0, "アイテムのレアリティ" + (object) this.mItemParam.rare + "には指定ランク" + (object) rank + "の情報に存在しない。");
      int index = rank - 1;
      return index < (int) equipEnhanceParam.rankcap ? (int) equipEnhanceParam.ranks[index].need_point : 0;
    }

    public int GetNeedExp(int rank)
    {
      RarityEquipEnhanceParam equipEnhanceParam = this.RarityParam == null ? (RarityEquipEnhanceParam) null : this.RarityParam.EquipEnhanceParam;
      DebugUtility.Assert((rank <= 0 ? 0 : (rank <= (int) equipEnhanceParam.rankcap ? 1 : 0)) != 0, "アイテムのレアリティ" + (object) this.mItemParam.rare + "には指定ランク" + (object) rank + "の情報に存在しない。");
      int needExp = 0;
      for (int index = 0; index < rank; ++index)
        needExp += (int) equipEnhanceParam.ranks[index].need_point;
      return needExp;
    }

    public int CalcRank() => this.CalcRankFromExp(this.Exp);

    public int CalcRankFromExp(int current)
    {
      int rankCap = this.GetRankCap();
      int num = 0;
      int val1 = 0;
      for (int index = 0; index < rankCap; ++index)
      {
        num += this.GetNextExp(index + 1);
        if (num <= current)
          ++val1;
      }
      return Math.Min(Math.Max(val1, 1), rankCap);
    }

    public void UpdateParam()
    {
      if (this.mSkill == null)
        return;
      this.mSkill.UpdateParam();
    }

    public int GetExp() => this.GetExpFromExp(this.Exp);

    public int GetExpFromExp(int current)
    {
      int needExp = this.GetNeedExp(this.CalcRankFromExp(current));
      return current - needExp;
    }

    public int GetNextExp() => this.GetNextExpFromExp(this.Exp);

    public int GetNextExpFromExp(int current)
    {
      int rankCap = this.GetRankCap();
      int num = 0;
      for (int index = 0; index < rankCap; ++index)
      {
        num += this.GetNextExp(index + 1);
        if (num > current)
          return num - current;
      }
      return 0;
    }

    public void GainExp(int exp)
    {
      this.mExp += exp;
      this.mRank = this.CalcRank();
      if (this.mSkill == null || this.ItemParam == null)
        return;
      this.mSkill.Setup(this.ItemParam.skill, this.mRank, this.GetRankCap());
    }

    public int GetEnhanceCostScale()
    {
      return this.RarityParam == null || this.RarityParam.EquipEnhanceParam == null ? 0 : (int) this.RarityParam.EquipEnhanceParam.cost_scale;
    }

    public List<ItemData> GetReturnItemList()
    {
      if (!this.IsValid() || !this.IsEquiped())
        return (List<ItemData>) null;
      RarityEquipEnhanceParam equipEnhanceParam = this.RarityParam == null ? (RarityEquipEnhanceParam) null : this.RarityParam.EquipEnhanceParam;
      if (equipEnhanceParam == null || equipEnhanceParam.ranks == null)
        return (List<ItemData>) null;
      RarityEquipEnhanceParam.RankParam rankParam = equipEnhanceParam.GetRankParam(this.Rank);
      if (rankParam == null || rankParam.return_item == null)
        return (List<ItemData>) null;
      ReturnItem[] returnItem = rankParam.return_item;
      List<ItemData> returnItemList = new List<ItemData>();
      for (int index = 0; index < returnItem.Length; ++index)
      {
        if (!string.IsNullOrEmpty(returnItem[index].iname) && (int) returnItem[index].num > 0)
        {
          ItemData itemData = new ItemData();
          itemData.Setup(0L, returnItem[index].iname, (int) returnItem[index].num);
          returnItemList.Add(itemData);
        }
      }
      return returnItemList;
    }

    public override string ToString()
    {
      return string.Format("ItemParam=[{0}] ({1})", (object) this.ItemParam, (object) this.GetType().Name);
    }
  }
}
