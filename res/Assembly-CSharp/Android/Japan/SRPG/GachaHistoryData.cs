// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class GachaHistoryData
  {
    public GachaDropData.Type type;
    public UnitParam unit;
    public ItemParam item;
    public ArtifactParam artifact;
    public ConceptCardParam conceptcard;
    public int num;
    public bool isConvert;
    public bool isNew;
    public int rarity;

    public void Init()
    {
      this.type = GachaDropData.Type.None;
      this.unit = (UnitParam) null;
      this.item = (ItemParam) null;
      this.artifact = (ArtifactParam) null;
      this.conceptcard = (ConceptCardParam) null;
      this.num = 0;
      this.isNew = false;
      this.rarity = 0;
    }

    public bool Deserialize(Json_GachaHistoryItem json)
    {
      this.Init();
      if (json == null)
        return false;
      switch (json.itype)
      {
        case "item":
          this.type = GachaDropData.Type.Item;
          this.item = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetItemParam(json.iname);
          if (this.item == null)
          {
            DebugUtility.LogError("iname:" + json.iname + " => Not ItemParam!");
            return false;
          }
          this.rarity = this.item.rare;
          break;
        case "unit":
          this.type = GachaDropData.Type.Unit;
          this.unit = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitParam(json.iname);
          if (this.unit == null)
          {
            DebugUtility.LogError("iname:" + json.iname + " => Not UnitParam!");
            return false;
          }
          this.rarity = (int) this.unit.rare;
          break;
        case "artifact":
          this.type = GachaDropData.Type.Artifact;
          this.artifact = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(json.iname);
          if (this.artifact == null)
          {
            DebugUtility.LogError("iname:" + json.iname + " => Not ArtifactParam!");
            return false;
          }
          if (json.rare != -1 && json.rare > this.artifact.raremax)
            DebugUtility.LogError("武具:" + this.artifact.name + "の最大レアリティより大きい値が設定されています.");
          else if (json.rare != -1 && json.rare < this.artifact.rareini)
            DebugUtility.LogError("武具:" + this.artifact.name + "の初期レアリティより小さい値が設定されています.");
          this.rarity = json.rare <= -1 ? this.artifact.rareini : Mathf.Min(Mathf.Max(this.artifact.rareini, json.rare), this.artifact.raremax);
          break;
        case "concept_card":
          this.type = GachaDropData.Type.ConceptCard;
          this.conceptcard = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
          if (this.conceptcard == null)
          {
            DebugUtility.LogError("iname:" + json.iname + " => Not ConceptCardParam!");
            return false;
          }
          this.rarity = this.conceptcard.rare;
          break;
      }
      this.num = json.num;
      this.isConvert = json.convert_piece == 1;
      this.isNew = json.is_new == 1;
      return true;
    }
  }
}
