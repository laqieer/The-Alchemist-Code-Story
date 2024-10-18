// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultThumbnailItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GachaResultThumbnailItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject m_UnitIcon;
    [SerializeField]
    private GameObject m_ItemIcon;
    [SerializeField]
    private GameObject m_ArtifactIcon;
    [SerializeField]
    private GameObject m_ConceptCardIcon;
    [SerializeField]
    private GameObject m_ChangePieceCoin;
    private GameObject mCurrentIconObj;

    public GameObject CurrentIcon => this.mCurrentIconObj;

    public bool Setup(GachaDropData drop, int index)
    {
      if (drop == null)
      {
        DebugUtility.LogError("召喚結果がありません.");
        return false;
      }
      GachaResultThumbnailWindow.GachaResultType gachaResultType = GachaResultThumbnailWindow.GachaResultType.None;
      GameObject gameObject1 = (GameObject) null;
      switch (drop.type)
      {
        case GachaDropData.Type.Item:
          gameObject1 = this.m_ItemIcon;
          DataSource.Bind<ItemData>(gameObject1, ItemData.CreateItemDataForDisplay(drop.item.iname, drop.num));
          ItemIcon component1 = gameObject1.GetComponent<ItemIcon>();
          if (Object.op_Inequality((Object) component1, (Object) null))
            component1.UpdateValue();
          gachaResultType = !string.IsNullOrEmpty(drop.item.Flavor) ? GachaResultThumbnailWindow.GachaResultType.Item : GachaResultThumbnailWindow.GachaResultType.Piece;
          break;
        case GachaDropData.Type.Unit:
          gameObject1 = this.m_UnitIcon;
          DataSource.Bind<UnitData>(gameObject1, UnitData.CreateUnitDataForDisplay(drop.unit));
          gachaResultType = GachaResultThumbnailWindow.GachaResultType.Unit;
          break;
        case GachaDropData.Type.Artifact:
          gameObject1 = this.m_ArtifactIcon;
          DataSource.Bind<ArtifactData>(gameObject1, ArtifactData.CreateArtifactDataForDisplay(drop.artifact, drop.Rare));
          gachaResultType = GachaResultThumbnailWindow.GachaResultType.Artifact;
          break;
        case GachaDropData.Type.ConceptCard:
          gameObject1 = this.m_ConceptCardIcon;
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(drop.conceptcard.iname);
          ConceptCardIcon component2 = gameObject1.GetComponent<ConceptCardIcon>();
          if (Object.op_Inequality((Object) component2, (Object) null))
          {
            component2.Setup(cardDataForDisplay);
            component2.SetCardNum(drop.num);
            SerializeValueBehaviour component3 = gameObject1.GetComponent<SerializeValueBehaviour>();
            if (Object.op_Inequality((Object) component3, (Object) null))
            {
              GameObject gameObject2 = component3.list.GetGameObject("unit_icon");
              if (Object.op_Inequality((Object) gameObject2, (Object) null))
              {
                UnitData data = (UnitData) null;
                if (drop.cardunit != null)
                  data = UnitData.CreateUnitDataForDisplay(drop.cardunit);
                DataSource.Bind<UnitData>(gameObject2, data);
                gameObject2.SetActive(drop.cardunit != null);
              }
              GameObject gameObject3 = component3.list.GetGameObject("skin");
              if (Object.op_Inequality((Object) gameObject3, (Object) null))
              {
                bool flag = false;
                if (drop.conceptcard.effects != null && drop.conceptcard.effects.Length > 0)
                {
                  for (int index1 = 0; index1 < drop.conceptcard.effects.Length; ++index1)
                  {
                    ConceptCardEffectsParam effect = drop.conceptcard.effects[index1];
                    if (effect != null && !string.IsNullOrEmpty(effect.skin))
                    {
                      flag = true;
                      break;
                    }
                  }
                }
                gameObject3.SetActive(flag);
              }
              GameObject gameObject4 = component3.list.GetGameObject("amount");
              if (Object.op_Inequality((Object) gameObject4, (Object) null))
                gameObject4.SetActive(drop.num > 1);
            }
          }
          gachaResultType = GachaResultThumbnailWindow.GachaResultType.ConceptCard;
          break;
      }
      if (Object.op_Equality((Object) gameObject1, (Object) null))
      {
        DebugUtility.LogError("排出結果を元にアイコンのSetupが出来ませんでした.");
        return false;
      }
      SerializeValueBehaviour component4 = gameObject1.GetComponent<SerializeValueBehaviour>();
      if (Object.op_Inequality((Object) component4, (Object) null))
      {
        GameObject gameObject5 = component4.list.GetGameObject("new");
        if (Object.op_Inequality((Object) component4, (Object) null))
          gameObject5.SetActive(drop.isNew);
      }
      ButtonEvent component5 = gameObject1.GetComponent<ButtonEvent>();
      if (Object.op_Inequality((Object) component5, (Object) null))
      {
        ButtonEvent.Event @event = component5.GetEvent("CLICK_ICON");
        if (@event != null)
        {
          @event.valueList.SetField(nameof (index), index);
          @event.valueList.SetField("type", (int) gachaResultType);
          if (gachaResultType == GachaResultThumbnailWindow.GachaResultType.ConceptCard)
            @event.valueList.SetField("is_first_get_unit", drop.cardunit != null);
        }
      }
      if (Object.op_Inequality((Object) this.m_ChangePieceCoin, (Object) null))
      {
        bool flag = drop.ch_piece_coin_num > 0;
        if (flag)
        {
          ItemData data = new ItemData();
          if (string.IsNullOrEmpty((string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GachaChangePieceCoinIname))
          {
            DebugUtility.LogError("FixParam.ch_piece_coin_inameが設定されていません.");
            return false;
          }
          data.Setup(0L, (string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GachaChangePieceCoinIname, drop.ch_piece_coin_num);
          DataSource.Bind<ItemData>(this.m_ChangePieceCoin, data);
        }
        this.m_ChangePieceCoin.SetActive(flag);
        GameParameter.UpdateAll(this.m_ChangePieceCoin);
      }
      gameObject1.SetActive(true);
      this.mCurrentIconObj = gameObject1;
      return true;
    }
  }
}
