// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Tap Item", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "Tap Unit", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(102, "Tap Artifact", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(103, "Tap ConceptCard", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 10)]
  public class LimitedShopItemDetail : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 1;
    private const int PIN_OUT_FINISHED = 10;
    private const int PIN_IN_TAP_ITEM = 100;
    private const int PIN_IN_TAP_UNIT = 101;
    private const int PIN_IN_TAP_ARTIFACT = 102;
    private const int PIN_IN_TAP_CONCEPTCARD = 103;
    [SerializeField]
    private GameObject ItemDetailWindow;
    [SerializeField]
    private GameObject ArtifactDetailWindow;
    [SerializeField]
    private GameObject ConceptCardDetail;
    [Space(8f)]
    [SerializeField]
    private GameObject ItemDetailPanel;
    [SerializeField]
    private Transform ItemHolder;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private GameObject UnitTemplate;
    [SerializeField]
    private GameObject ArtifactTemplate;
    [Space(8f)]
    [SerializeField]
    private GameObject ConceptCardDetailPanel;
    [SerializeField]
    private Transform ConceptCardHolder;
    [SerializeField]
    private GameObject ConceptCardTemplate;
    private LimitedShopItemDetail.TicketType mTicketType;

    private void Awake()
    {
      if ((UnityEngine.Object) this.ItemDetailPanel != (UnityEngine.Object) null)
        this.ItemDetailPanel.SetActive(false);
      if ((UnityEngine.Object) this.ConceptCardDetailPanel != (UnityEngine.Object) null)
        this.ConceptCardDetailPanel.SetActive(false);
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.SetActive(false);
      if ((UnityEngine.Object) this.UnitTemplate != (UnityEngine.Object) null)
        this.UnitTemplate.SetActive(false);
      if ((UnityEngine.Object) this.ArtifactTemplate != (UnityEngine.Object) null)
        this.ArtifactTemplate.SetActive(false);
      if (!((UnityEngine.Object) this.ConceptCardTemplate != (UnityEngine.Object) null))
        return;
      this.ConceptCardTemplate.SetActive(false);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.OnItemSelect();
          break;
        case 102:
          this.OnArtifactSelect();
          break;
        case 103:
          this.OnConceptCardSelect();
          break;
        default:
          if (pinID != 1)
            break;
          this.Refresh();
          break;
      }
    }

    private void Refresh()
    {
      ItemParam dataOfClass = DataSource.FindDataOfClass<ItemParam>(this.gameObject, (ItemParam) null);
      if (dataOfClass.type != EItemType.UnitSelectItem && dataOfClass.type != EItemType.ConceptCardSelectItem)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
      else
      {
        if (dataOfClass.type == EItemType.UnitSelectItem)
        {
          if (dataOfClass.iname.StartsWith("IT_SU_"))
            this.mTicketType = LimitedShopItemDetail.TicketType.Unit;
          else if (dataOfClass.iname.StartsWith("IT_SI_"))
            this.mTicketType = LimitedShopItemDetail.TicketType.Item;
          else if (dataOfClass.iname.StartsWith("IT_SA_"))
            this.mTicketType = LimitedShopItemDetail.TicketType.Artifact;
        }
        else if (dataOfClass.type == EItemType.ConceptCardSelectItem && dataOfClass.iname.StartsWith("IT_STS_"))
          this.mTicketType = LimitedShopItemDetail.TicketType.ConceptCard;
        if (this.mTicketType == LimitedShopItemDetail.TicketType.None)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
        }
        else
        {
          switch (this.mTicketType)
          {
            case LimitedShopItemDetail.TicketType.Unit:
              Network.RequestAPI((WebAPI) new ReqMailSelect(dataOfClass.iname, ReqMailSelect.type.unit, new Network.ResponseCallback(this.UnitResponseCallback)), false);
              break;
            case LimitedShopItemDetail.TicketType.Item:
              Network.RequestAPI((WebAPI) new ReqMailSelect(dataOfClass.iname, ReqMailSelect.type.item, new Network.ResponseCallback(this.ItemResponseCallback)), false);
              break;
            case LimitedShopItemDetail.TicketType.Artifact:
              Network.RequestAPI((WebAPI) new ReqMailSelect(dataOfClass.iname, ReqMailSelect.type.artifact, new Network.ResponseCallback(this.ArtifactResponseCallback)), false);
              break;
            case LimitedShopItemDetail.TicketType.ConceptCard:
              Network.RequestAPI((WebAPI) new ReqMailSelect(dataOfClass.iname, ReqMailSelect.type.conceptcard, new Network.ResponseCallback(this.ConceptCardResponseCallback)), false);
              break;
          }
        }
      }
    }

    private void UnitResponseCallback(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_UnitSelectResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_UnitSelectResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          Network.RemoveAPI();
          UnitSelectListData unitSelectListData = new UnitSelectListData();
          unitSelectListData.Deserialize(jsonObject.body);
          this.ItemDetailPanel.SetActive(true);
          this.CreateUnits(unitSelectListData.items);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
        }
      }
    }

    private void ItemResponseCallback(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ItemSelectResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ItemSelectResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          Network.RemoveAPI();
          ItemSelectListData itemSelectListData = new ItemSelectListData();
          itemSelectListData.Deserialize(jsonObject.body);
          this.ItemDetailPanel.SetActive(true);
          this.CreateItems(itemSelectListData.items);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
        }
      }
    }

    private void ArtifactResponseCallback(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ArtifactSelectResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArtifactSelectResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          Network.RemoveAPI();
          ArtifactSelectListData artifactSelectListData = new ArtifactSelectListData();
          artifactSelectListData.Deserialize(jsonObject.body);
          this.ItemDetailPanel.SetActive(true);
          this.CreateArtifacts(artifactSelectListData.items);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
        }
      }
    }

    private void ConceptCardResponseCallback(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqMailSelectConceptCard.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqMailSelectConceptCard.Json>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          Network.RemoveAPI();
          ConceptCardData[] data = new ConceptCardData[jsonObject.body.select.Length];
          for (int index = 0; index < jsonObject.body.select.Length; ++index)
          {
            FlowNode_ReqMailSelectConceptCard.Json_SelectConceptCard selectConceptCard = jsonObject.body.select[index];
            data[index] = ConceptCardData.CreateConceptCardDataForDisplay(selectConceptCard.iname);
            MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(selectConceptCard.iname, selectConceptCard.has_count);
          }
          this.ConceptCardDetailPanel.SetActive(true);
          this.CreateConceptCards(data);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
        }
      }
    }

    private void CreateUnits(List<UnitSelectListItemData> units)
    {
      if ((UnityEngine.Object) this.UnitTemplate == (UnityEngine.Object) null)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < units.Count; ++index)
      {
        UnitSelectListItemData unit = units[index];
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitTemplate);
        gameObject.transform.SetParent(this.ItemHolder, false);
        DataSource.Bind<UnitParam>(gameObject, unit.param, false);
        gameObject.SetActive(true);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void CreateItems(List<ItemSelectListItemData> shopdata)
    {
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      int count = shopdata.Count;
      for (int index = 0; index < count; ++index)
      {
        ItemSelectListItemData data1 = shopdata[index];
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
        gameObject.transform.SetParent(this.ItemHolder, false);
        ItemData data2 = new ItemData();
        int itemAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(data1.param.iname);
        data2.Setup(0L, data1.param, itemAmount);
        DataSource.Bind<ItemData>(gameObject, data2, false);
        DataSource.Bind<ItemSelectListItemData>(gameObject, data1, false);
        gameObject.SetActive(true);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void CreateArtifacts(List<ArtifactSelectListItemData> data)
    {
      if ((UnityEngine.Object) this.ArtifactTemplate == (UnityEngine.Object) null)
        return;
      for (int index = 0; index < data.Count; ++index)
      {
        ArtifactData data1 = new ArtifactData();
        data1.Deserialize(new Json_Artifact()
        {
          iname = data[index].iname,
          rare = data[index].param.rareini
        });
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactTemplate);
        gameObject.transform.SetParent(this.ItemHolder, false);
        DataSource.Bind<ArtifactData>(gameObject, data1, false);
        gameObject.SetActive(true);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void CreateConceptCards(ConceptCardData[] data)
    {
      if ((UnityEngine.Object) this.ConceptCardTemplate == (UnityEngine.Object) null)
        return;
      for (int index = 0; index < data.Length; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ConceptCardTemplate);
        gameObject.transform.SetParent(this.ConceptCardHolder, false);
        gameObject.GetComponentInChildren<ConceptCardIcon>().Setup(data[index]);
        gameObject.SetActive(true);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void OnItemSelect()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      GameObject gameObject = currentValue.GetGameObject("buttonobj");
      if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        return;
      DataSource.Bind<ItemData>(UnityEngine.Object.Instantiate<GameObject>(this.ItemDetailWindow), DataSource.FindDataOfClass<ItemData>(gameObject, (ItemData) null), false);
    }

    private void OnArtifactSelect()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      GameObject gameObject = currentValue.GetGameObject("buttonobj");
      if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        return;
      DataSource.Bind<ArtifactData>(UnityEngine.Object.Instantiate<GameObject>(this.ArtifactDetailWindow), DataSource.FindDataOfClass<ArtifactData>(gameObject, (ArtifactData) null), false);
    }

    private void OnConceptCardSelect()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      GameObject gameObject = currentValue.GetGameObject("buttonobj");
      if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        return;
      ConceptCardIcon componentInChildren = gameObject.GetComponentInChildren<ConceptCardIcon>();
      GlobalVars.SelectedConceptCardData.Set(componentInChildren.ConceptCard);
      UnityEngine.Object.Instantiate<GameObject>(this.ConceptCardDetail);
    }

    private enum TicketType
    {
      None,
      Unit,
      Item,
      Artifact,
      ConceptCard,
    }
  }
}
