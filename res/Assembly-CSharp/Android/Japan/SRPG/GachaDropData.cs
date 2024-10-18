// Decompiled with JetBrains decompiler
// Type: SRPG.GachaDropData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class GachaDropData
  {
    public GachaDropData.Type type;
    public UnitParam unit;
    public ItemParam item;
    public ArtifactParam artifact;
    public int num;
    public UnitParam unitOrigin;
    public bool isNew;
    public int[] excites;
    private int rarity;
    public ConceptCardParam conceptcard;
    public UnitParam cardunit;
    public bool isGift;
    public bool isFeatureItem;

    public bool isCoin
    {
      get
      {
        return this.type == GachaDropData.Type.Coin;
      }
    }

    public bool isGold
    {
      get
      {
        return this.type == GachaDropData.Type.Gold;
      }
    }

    public int Rare
    {
      get
      {
        return this.rarity;
      }
      set
      {
        this.rarity = value;
      }
    }

    public void Init()
    {
      this.type = GachaDropData.Type.None;
      this.unit = (UnitParam) null;
      this.item = (ItemParam) null;
      this.artifact = (ArtifactParam) null;
      this.num = 0;
      this.unitOrigin = (UnitParam) null;
      this.isNew = false;
      this.rarity = 0;
      this.excites = new int[3];
      this.isGift = false;
      this.isFeatureItem = false;
    }

    public bool Deserialize(Json_DropInfo json)
    {
      this.Init();
      if (json == null)
        return false;
      switch (json.type)
      {
        case "item":
          this.type = GachaDropData.Type.Item;
          this.item = MonoSingleton<GameManager>.Instance.GetItemParam(json.iname);
          if (this.item == null)
          {
            DebugUtility.LogError("iname:" + json.iname + " => Not ItemParam!");
            return false;
          }
          this.rarity = this.item.rare;
          break;
        case "unit":
          this.unit = MonoSingleton<GameManager>.Instance.GetUnitParam(json.iname);
          if (this.unit == null)
          {
            DebugUtility.LogError("iname:" + json.iname + " => Not UnitParam!");
            return false;
          }
          this.rarity = (int) this.unit.rare;
          this.type = GachaDropData.Type.Unit;
          break;
        case "artifact":
          this.artifact = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.iname);
          if (this.artifact == null)
          {
            DebugUtility.LogError("iname:" + json.iname + " => Not ArtifactParam!");
            return false;
          }
          if (json.rare != -1 && json.rare > this.artifact.raremax)
            DebugUtility.LogError("武具:" + this.artifact.name + "の最大レアリティより大きい値が設定されています.");
          else if (json.rare != -1 && json.rare < this.artifact.rareini)
            DebugUtility.LogError("武具:" + this.artifact.name + "の初期レアリティより小さい値が設定されています.");
          this.rarity = json.rare <= -1 ? this.artifact.rareini : Math.Min(Math.Max(this.artifact.rareini, json.rare), this.artifact.raremax);
          this.type = GachaDropData.Type.Artifact;
          break;
        case "concept_card":
          this.conceptcard = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
          if (this.conceptcard == null)
          {
            DebugUtility.LogError("iname:" + json.iname + " => Not ConceptCardParam!");
            return false;
          }
          this.rarity = this.conceptcard.rare;
          this.type = GachaDropData.Type.ConceptCard;
          break;
        case "coin":
          this.type = GachaDropData.Type.Coin;
          break;
        case "gold":
          this.type = GachaDropData.Type.Gold;
          break;
      }
      this.num = json.num;
      if (0 < json.iname_origin.Length)
        this.unitOrigin = MonoSingleton<GameManager>.Instance.GetUnitParam(json.iname_origin);
      this.isNew = 1 == json.is_new;
      if (this.type == GachaDropData.Type.ConceptCard && !string.IsNullOrEmpty(json.get_unit))
      {
        this.cardunit = MonoSingleton<GameManager>.Instance.GetUnitParam(json.get_unit);
        if (this.cardunit == null)
        {
          DebugUtility.LogError("get_unit:" + json.get_unit + " => Not UnitParam!");
          return false;
        }
      }
      this.isGift = json.is_gift == 1;
      this.isFeatureItem = json.is_feature_item == 1;
      return true;
    }

    public override string ToString()
    {
      string str = "type: " + (object) this.type + "\n";
      switch (this.type)
      {
        case GachaDropData.Type.Item:
          str = str + "name: " + this.item.name + " rare: " + (object) this.item.rare;
          break;
        case GachaDropData.Type.Unit:
          str = str + "name: " + this.unit.name + " rare: " + (object) this.unit.rare;
          break;
        case GachaDropData.Type.Artifact:
          str = str + "name: " + this.artifact.name + " rare: " + (object) this.artifact.rareini;
          break;
      }
      if (this.unitOrigin != null)
        str = str + " origin: " + this.unitOrigin.name;
      return str + " isNew: " + (object) this.isNew;
    }

    public enum Type
    {
      None,
      Item,
      Unit,
      Artifact,
      ConceptCard,
      Coin,
      Gold,
      End,
    }
  }
}
