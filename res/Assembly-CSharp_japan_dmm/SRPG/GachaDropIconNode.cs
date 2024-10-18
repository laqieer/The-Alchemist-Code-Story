// Decompiled with JetBrains decompiler
// Type: SRPG.GachaDropIconNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GachaDropIconNode : ContentNode
  {
    [Header("ユニットアイコン")]
    [SerializeField]
    private GameObject DropUnit;
    [Header("アイテムアイコン")]
    [SerializeField]
    private GameObject DropItem;
    [Header("武具アイコン")]
    [SerializeField]
    private GameObject DropArtifact;
    [Header("真理念装アイコン")]
    [SerializeField]
    private GameObject DropConceptCard;
    [Header("選択中バッジ")]
    [SerializeField]
    private GameObject SelectedBadge;
    private GachaDropData mDropData;

    public GachaDropData DropData => this.mDropData;

    public void Reset()
    {
      DataSource.Bind<UnitData>(((Component) this).gameObject, (UnitData) null);
      DataSource.Bind<ItemParam>(((Component) this).gameObject, (ItemParam) null);
      DataSource.Bind<ArtifactParam>(((Component) this).gameObject, (ArtifactParam) null);
      if (Object.op_Inequality((Object) this.DropConceptCard, (Object) null))
      {
        ConceptCardIcon component = this.DropConceptCard.GetComponent<ConceptCardIcon>();
        if (Object.op_Implicit((Object) component))
          component.ResetIcon();
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
      GameUtility.SetGameObjectActive(this.DropUnit, false);
      GameUtility.SetGameObjectActive(this.DropItem, false);
      GameUtility.SetGameObjectActive(this.DropArtifact, false);
      GameUtility.SetGameObjectActive(this.DropConceptCard, false);
      GameUtility.SetGameObjectActive(this.SelectedBadge, false);
      this.mDropData = (GachaDropData) null;
    }

    public void Select(bool select) => GameUtility.SetGameObjectActive(this.SelectedBadge, select);

    public void Setup(GachaDropData drop_data, bool is_select = false)
    {
      if (drop_data == null)
        return;
      this.Reset();
      this.mDropData = drop_data;
      GameObject gameObject = (GameObject) null;
      switch (drop_data.type)
      {
        case GachaDropData.Type.Item:
          if (Object.op_Equality((Object) this.DropItem, (Object) null))
            return;
          gameObject = this.DropItem;
          DataSource.Bind<ItemParam>(((Component) this).gameObject, drop_data.item);
          break;
        case GachaDropData.Type.Unit:
          if (Object.op_Equality((Object) this.DropUnit, (Object) null))
            return;
          gameObject = this.DropUnit;
          DataSource.Bind<UnitData>(((Component) this).gameObject, UnitData.CreateUnitDataForDisplay(drop_data.unit == null ? string.Empty : drop_data.unit.iname));
          break;
        case GachaDropData.Type.Artifact:
          if (Object.op_Equality((Object) this.DropArtifact, (Object) null))
            return;
          gameObject = this.DropArtifact;
          DataSource.Bind<ArtifactParam>(((Component) this).gameObject, drop_data.artifact);
          break;
        case GachaDropData.Type.ConceptCard:
          if (Object.op_Equality((Object) this.DropConceptCard, (Object) null))
            return;
          gameObject = this.DropConceptCard;
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(drop_data.conceptcard.iname);
          ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.Setup(cardDataForDisplay);
            break;
          }
          break;
      }
      GameUtility.SetGameObjectActive(gameObject, true);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
