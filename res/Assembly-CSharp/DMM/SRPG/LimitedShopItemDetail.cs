// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
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
      if (Object.op_Inequality((Object) this.ItemDetailPanel, (Object) null))
        this.ItemDetailPanel.SetActive(false);
      if (Object.op_Inequality((Object) this.ConceptCardDetailPanel, (Object) null))
        this.ConceptCardDetailPanel.SetActive(false);
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.UnitTemplate, (Object) null))
        this.UnitTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ArtifactTemplate, (Object) null))
        this.ArtifactTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.ConceptCardTemplate, (Object) null))
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
      ItemParam dataOfClass = DataSource.FindDataOfClass<ItemParam>(((Component) this).gameObject, (ItemParam) null);
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
              Network.RequestAPI((WebAPI) new ReqMailSelect(dataOfClass.iname, ReqMailSelect.type.unit, new Network.ResponseCallback(this.UnitResponseCallback)));
              break;
            case LimitedShopItemDetail.TicketType.Item:
              Network.RequestAPI((WebAPI) new ReqMailSelect(dataOfClass.iname, ReqMailSelect.type.item, new Network.ResponseCallback(this.ItemResponseCallback)));
              break;
            case LimitedShopItemDetail.TicketType.Artifact:
              Network.RequestAPI((WebAPI) new ReqMailSelect(dataOfClass.iname, ReqMailSelect.type.artifact, new Network.ResponseCallback(this.ArtifactResponseCallback)));
              break;
            case LimitedShopItemDetail.TicketType.ConceptCard:
              Network.RequestAPI((WebAPI) new ReqMailSelect(dataOfClass.iname, ReqMailSelect.type.conceptcard, new Network.ResponseCallback(this.ConceptCardResponseCallback)));
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
      if (Object.op_Equality((Object) this.UnitTemplate, (Object) null))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < units.Count; ++index)
      {
        UnitSelectListItemData unit = units[index];
        GameObject gameObject = Object.Instantiate<GameObject>(this.UnitTemplate);
        gameObject.transform.SetParent(this.ItemHolder, false);
        DataSource.Bind<UnitParam>(gameObject, unit.param);
        gameObject.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void CreateItems(List<ItemSelectListItemData> shopdata)
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      int count = shopdata.Count;
      for (int index = 0; index < count; ++index)
      {
        ItemSelectListItemData data1 = shopdata[index];
        GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
        gameObject.transform.SetParent(this.ItemHolder, false);
        ItemData data2 = new ItemData();
        int itemAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(data1.param.iname);
        data2.Setup(0L, data1.param, itemAmount);
        DataSource.Bind<ItemData>(gameObject, data2);
        DataSource.Bind<ItemSelectListItemData>(gameObject, data1);
        gameObject.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void CreateArtifacts(List<ArtifactSelectListItemData> data)
    {
      if (Object.op_Equality((Object) this.ArtifactTemplate, (Object) null))
        return;
      for (int index = 0; index < data.Count; ++index)
      {
        ArtifactData data1 = new ArtifactData();
        data1.Deserialize(new Json_Artifact()
        {
          iname = data[index].iname,
          rare = data[index].param.rareini
        });
        GameObject gameObject = Object.Instantiate<GameObject>(this.ArtifactTemplate);
        gameObject.transform.SetParent(this.ItemHolder, false);
        DataSource.Bind<ArtifactData>(gameObject, data1);
        gameObject.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void CreateConceptCards(ConceptCardData[] data)
    {
      if (Object.op_Equality((Object) this.ConceptCardTemplate, (Object) null))
        return;
      for (int index = 0; index < data.Length; ++index)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.ConceptCardTemplate);
        gameObject.transform.SetParent(this.ConceptCardHolder, false);
        gameObject.GetComponentInChildren<ConceptCardIcon>().Setup(data[index]);
        gameObject.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnItemSelect()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      GameObject gameObject = currentValue.GetGameObject("buttonobj");
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(gameObject, (ItemData) null);
      DataSource.Bind<ItemData>(Object.Instantiate<GameObject>(this.ItemDetailWindow), dataOfClass);
    }

    private void OnArtifactSelect()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      GameObject gameObject = currentValue.GetGameObject("buttonobj");
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(gameObject, (ArtifactData) null);
      DataSource.Bind<ArtifactData>(Object.Instantiate<GameObject>(this.ArtifactDetailWindow), dataOfClass);
    }

    private void OnConceptCardSelect()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      GameObject gameObject = currentValue.GetGameObject("buttonobj");
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      ConceptCardIcon componentInChildren = gameObject.GetComponentInChildren<ConceptCardIcon>();
      GlobalVars.SelectedConceptCardData.Set(componentInChildren.ConceptCard);
      Object.Instantiate<GameObject>(this.ConceptCardDetail);
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
