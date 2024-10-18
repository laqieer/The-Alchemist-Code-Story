// Decompiled with JetBrains decompiler
// Type: SRPG.MailList2
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
  [FlowNode.Pin(10, "メールリスト空", FlowNode.PinTypes.Output, 10)]
  public class MailList2 : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_REFRESH = 1;
    private const int PIN_ID_LIST_EMPTY = 10;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private ListExtras ScrollView;
    private List<GameObject> mMailListItems;

    private void ActivateOutputLinks(int pinID)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.OnRefresh();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start() => this.InitializeList();

    private void InitializeList()
    {
      if (this.mMailListItems != null)
        return;
      this.mMailListItems = new List<GameObject>();
    }

    private GameObject CreateListItem()
    {
      GameObject listItem = Object.Instantiate<GameObject>(this.ItemTemplate);
      MailListItem component1 = listItem.GetComponent<MailListItem>();
      if (Object.op_Inequality((Object) component1.listItemEvents, (Object) null))
      {
        component1.listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        ListItemEvents component2 = component1.Button.GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component2, (Object) null))
          component2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
      }
      return listItem;
    }

    private void UpdateItems()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || this.mMailListItems == null)
        return;
      List<MailData> currentMails = MonoSingleton<GameManager>.Instance.Player.CurrentMails;
      if (this.mMailListItems.Count < currentMails.Count)
      {
        Transform transform = ((Component) this).transform;
        int num = currentMails.Count - this.mMailListItems.Count;
        for (int index = 0; index < num; ++index)
        {
          GameObject listItem = this.CreateListItem();
          listItem.transform.SetParent(transform, false);
          this.mMailListItems.Add(listItem);
        }
      }
      for (int index1 = 0; index1 < this.mMailListItems.Count; ++index1)
      {
        GameObject mMailListItem = this.mMailListItems[index1];
        if (index1 >= currentMails.Count)
        {
          mMailListItem.SetActive(false);
        }
        else
        {
          mMailListItem.SetActive(true);
          MailData data1 = currentMails[index1];
          DataSource.Bind<MailData>(mMailListItem, data1);
          DataSource.Bind<MailData>(mMailListItem.GetComponent<MailListItem>().Button, data1);
          int num = 0;
          for (int index2 = 0; index2 < data1.gifts.Length; ++index2)
          {
            if (data1.gifts[index2].giftTypes != 0L)
              ++num;
            if (num >= 2)
              break;
          }
          if (num >= 2)
          {
            MailIcon component = mMailListItem.GetComponent<MailIcon>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              if (Object.op_Inequality((Object) component.CurrentIcon, (Object) null))
                component.CurrentIcon.SetActive(false);
              component.CurrentIcon = component.SetIconTemplate;
              component.CurrentIcon.SetActive(true);
            }
          }
          else
          {
            MailIcon component1 = mMailListItem.GetComponent<MailIcon>();
            for (int index3 = 0; index3 < data1.gifts.Length; ++index3)
            {
              GiftData gift = data1.gifts[index3];
              if (!gift.NotSet)
              {
                if (gift.CheckGiftTypeIncluded(GiftTypes.IgnoreReceiveAll | GiftTypes.Item))
                {
                  ItemData data2 = new ItemData();
                  data2.Setup(0L, gift.iname, gift.num);
                  DataSource.Bind<ItemData>(mMailListItem, data2);
                  if (Object.op_Inequality((Object) component1, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component1.CurrentIcon, (Object) null))
                      component1.CurrentIcon.SetActive(false);
                    component1.CurrentIcon = component1.ItemIconTemplate;
                    component1.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
                {
                  ArtifactData artifactData = gift.CreateArtifactData();
                  if (artifactData != null)
                    DataSource.Bind<ArtifactData>(mMailListItem, artifactData);
                  if (Object.op_Inequality((Object) component1, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component1.CurrentIcon, (Object) null))
                      component1.CurrentIcon.SetActive(false);
                    component1.CurrentIcon = component1.ArtifactIconTemplate;
                    component1.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Award))
                {
                  AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(gift.iname);
                  ItemData data3 = new ItemData();
                  data3.Setup(0L, awardParam.ToItemParam(), gift.num);
                  DataSource.Bind<ItemData>(mMailListItem, data3);
                  if (Object.op_Inequality((Object) component1, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component1.CurrentIcon, (Object) null))
                      component1.CurrentIcon.SetActive(false);
                    component1.CurrentIcon = component1.ItemIconTemplate;
                    component1.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Coin))
                {
                  if (Object.op_Inequality((Object) component1, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component1.CurrentIcon, (Object) null))
                      component1.CurrentIcon.SetActive(false);
                    component1.CurrentIcon = component1.CoinIconTemplate;
                    component1.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Gold))
                {
                  if (Object.op_Inequality((Object) component1, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component1.CurrentIcon, (Object) null))
                      component1.CurrentIcon.SetActive(false);
                    component1.CurrentIcon = component1.GoldIconTemplate;
                    component1.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.ArenaCoin))
                {
                  if (Object.op_Inequality((Object) component1, (Object) null))
                  {
                    if (Object.op_Inequality((Object) component1.CurrentIcon, (Object) null))
                      component1.CurrentIcon.SetActive(false);
                    component1.CurrentIcon = component1.ArenaCoinIconTemplate;
                    component1.CurrentIcon.SetActive(true);
                  }
                }
                else
                {
                  if (gift.CheckGiftTypeIncluded(GiftTypes.MultiCoin))
                  {
                    if (Object.op_Inequality((Object) component1, (Object) null))
                    {
                      if (Object.op_Inequality((Object) component1.CurrentIcon, (Object) null))
                        component1.CurrentIcon.SetActive(false);
                      component1.CurrentIcon = component1.MultiCoinIconTemplate;
                      component1.CurrentIcon.SetActive(true);
                      break;
                    }
                    break;
                  }
                  if (gift.CheckGiftTypeIncluded(GiftTypes.KakeraCoin))
                  {
                    if (Object.op_Inequality((Object) component1, (Object) null))
                    {
                      if (Object.op_Inequality((Object) component1.CurrentIcon, (Object) null))
                        component1.CurrentIcon.SetActive(false);
                      component1.CurrentIcon = component1.KakeraCoinIconTemplate;
                      component1.CurrentIcon.SetActive(true);
                      break;
                    }
                    break;
                  }
                  if (gift.CheckGiftTypeIncluded(GiftTypes.ConceptCard))
                  {
                    if (Object.op_Inequality((Object) component1, (Object) null))
                    {
                      if (MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(gift.ConceptCardIname) == null)
                      {
                        DebugUtility.LogError(string.Format("MasterParam.ConceptCardParamに「{0}」が存在しない", (object) gift.ConceptCardIname));
                        break;
                      }
                      ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(gift.ConceptCardIname);
                      if (Object.op_Inequality((Object) component1.CurrentIcon, (Object) null))
                        component1.CurrentIcon.SetActive(false);
                      component1.CurrentIcon = component1.ConceptCardIconTemplate;
                      component1.CurrentIcon.SetActive(true);
                      ConceptCardIcon component2 = component1.CurrentIcon.GetComponent<ConceptCardIcon>();
                      if (Object.op_Inequality((Object) component2, (Object) null))
                      {
                        component2.Setup(cardDataForDisplay);
                        break;
                      }
                      break;
                    }
                    break;
                  }
                }
              }
            }
          }
          MailListItem component3 = mMailListItem.GetComponent<MailListItem>();
          if (Object.op_Inequality((Object) component3, (Object) null))
            component3.Set(data1.IsPeriod, data1.IsReadMail(), data1.post_at, data1.read);
        }
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnRefresh()
    {
      this.UpdateItems();
      if (MonoSingleton<GameManager>.Instance.Player.CurrentMails.Count >= 1)
        return;
      this.ActivateOutputLinks(10);
    }

    private void OnSelect(GameObject go)
    {
      MailData dataOfClass = DataSource.FindDataOfClass<MailData>(go, (MailData) null);
      if (dataOfClass != null)
      {
        GlobalVars.SelectedMailUniqueID.Set(dataOfClass.mid);
        GlobalVars.SelectedMailPeriod.Set(dataOfClass.period);
        FlowNode_OnMailSelect nodeOnMailSelect = ((Component) this).GetComponentInParent<FlowNode_OnMailSelect>();
        if (Object.op_Equality((Object) nodeOnMailSelect, (Object) null))
          nodeOnMailSelect = Object.FindObjectOfType<FlowNode_OnMailSelect>();
        if (Object.op_Inequality((Object) nodeOnMailSelect, (Object) null))
          nodeOnMailSelect.Selected();
      }
      this.UpdateItems();
    }
  }
}
