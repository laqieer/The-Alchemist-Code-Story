// Decompiled with JetBrains decompiler
// Type: SRPG.ItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class ItemData
  {
    private long mUniqueID;
    private ItemParam mItemParam;
    private RarityParam mRarityParam;
    private SkillData mSkill;
    private ItemData.ItemFlags mFlags;
    protected int mNum;
    private bool mIsNew;

    public int No => this.mItemParam.no;

    public long UniqueID => this.mUniqueID;

    public ItemParam Param => this.mItemParam;

    public string ItemID => this.mItemParam != null ? this.mItemParam.iname : (string) null;

    public int Num
    {
      get => this.mItemParam != null ? Math.Min(this.mNum, this.mItemParam.cap) : this.mNum;
    }

    public int NumNonCap => this.mNum;

    public SkillData Skill => this.mSkill;

    public bool IsUsed => this.mNum > 0;

    public EItemType ItemType => this.mItemParam != null ? this.mItemParam.type : EItemType.Used;

    public int Rarity => this.mItemParam != null ? this.mItemParam.rare : 0;

    public RarityParam RarityParam => this.mRarityParam;

    public int HaveCap => this.mItemParam != null ? this.mItemParam.cap : 0;

    public int InventoryCap => this.mItemParam != null ? this.mItemParam.invcap : 0;

    public int Buy => this.mItemParam != null ? this.mItemParam.buy : 0;

    public int Sell => this.mItemParam != null ? this.mItemParam.sell : 0;

    public RecipeParam Recipe
    {
      get => this.mItemParam != null ? this.mItemParam.Recipe : (RecipeParam) null;
    }

    public void SetFlag(ItemData.ItemFlags flag) => this.mFlags |= flag;

    public void ResetFlag(ItemData.ItemFlags flag) => this.mFlags &= ~flag;

    public void ResetAllFlag(ItemData.ItemFlags flag) => this.mFlags = (ItemData.ItemFlags) 0;

    public bool GetFlag(ItemData.ItemFlags flag) => (ItemData.ItemFlags) 0 != (this.mFlags & flag);

    public bool IsNew
    {
      get => this.GetFlag(ItemData.ItemFlags.NewItem);
      set
      {
        if (value)
          this.SetFlag(ItemData.ItemFlags.NewItem);
        else
          this.ResetFlag(ItemData.ItemFlags.NewItem);
      }
    }

    public bool IsNewSkin
    {
      get => this.GetFlag(ItemData.ItemFlags.NewSkin);
      set
      {
        if (value)
          this.SetFlag(ItemData.ItemFlags.NewSkin);
        else
          this.ResetFlag(ItemData.ItemFlags.NewSkin);
      }
    }

    public bool Deserialize(Json_Item json)
    {
      return json != null && this.Setup(json.iid, json.iname, json.num);
    }

    public bool Setup(long iid, string iname, int num)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      this.mItemParam = instanceDirect.GetItemParam(iname);
      DebugUtility.Assert(this.mItemParam != null, "Failed ItemParam iname \"" + iname + "\" not found.");
      this.mRarityParam = instanceDirect.GetRarityParam(this.mItemParam.rare);
      this.mUniqueID = iid;
      this.mNum = num;
      if (!string.IsNullOrEmpty(this.mItemParam.skill))
      {
        int rank = 1;
        this.mSkill = new SkillData();
        this.mSkill.Setup(this.mItemParam.skill, rank, this.GetRankCap());
      }
      return true;
    }

    public bool Setup(long iid, ItemParam itemParam, int num)
    {
      this.mItemParam = itemParam;
      this.mUniqueID = iid;
      this.mNum = num;
      if (!string.IsNullOrEmpty(this.mItemParam.skill))
      {
        int rank = 1;
        this.mSkill = new SkillData();
        this.mSkill.Setup(this.mItemParam.skill, rank, this.GetRankCap());
      }
      return true;
    }

    public void Gain(int num) => this.mNum = Math.Max(this.mNum + num, 0);

    public void Used(int num) => this.mNum = Math.Max(this.mNum - num, 0);

    public void SetNum(int num) => this.mNum = Math.Max(num, 0);

    public override string ToString()
    {
      return (this.mItemParam == null ? "None" : this.mItemParam.name) + this.GetType().FullName;
    }

    public bool CheckEquipEnhanceMaterial()
    {
      return this.mItemParam != null && this.mItemParam.CheckEquipEnhanceMaterial();
    }

    public int GetRankCap()
    {
      return this.mRarityParam != null ? (int) this.mRarityParam.EquipEnhanceParam.rankcap : 1;
    }

    public static ItemData CreateItemDataForDisplay(string iname, int num = 0)
    {
      ItemData itemDataForDisplay = new ItemData();
      itemDataForDisplay.Setup(1L, iname, num);
      return itemDataForDisplay;
    }

    [Flags]
    public enum ItemFlags
    {
      NewItem = 1,
      NewSkin = 2,
    }
  }
}
