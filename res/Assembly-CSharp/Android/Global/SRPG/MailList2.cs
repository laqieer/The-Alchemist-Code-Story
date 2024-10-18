// Decompiled with JetBrains decompiler
// Type: SRPG.MailList2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "メールリスト空", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class MailList2 : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_REFRESH = 1;
    private const int PIN_ID_LIST_EMPTY = 10;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private ListExtras ScrollView;
    public GameObject NoMail;
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
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
      this.InitializeList();
    }

    private void InitializeList()
    {
      if (this.mMailListItems != null)
        return;
      this.mMailListItems = new List<GameObject>();
    }

    private GameObject CreateListItem()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
      MailListItem component1 = gameObject.GetComponent<MailListItem>();
      if ((UnityEngine.Object) component1.listItemEvents != (UnityEngine.Object) null)
      {
        component1.listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        ListItemEvents component2 = component1.Button.GetComponent<ListItemEvents>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          component2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
      }
      return gameObject;
    }

    private void UpdateItems()
    {
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || this.mMailListItems == null)
        return;
      List<MailData> currentMails = MonoSingleton<GameManager>.Instance.Player.CurrentMails;
      this.NoMail.SetActive(currentMails.Count == 0);
      if (this.mMailListItems.Count < currentMails.Count)
      {
        Transform transform = this.transform;
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
            GiftData gift = data1.gifts[index2];
            if (gift.CheckGiftTypeIncluded(GiftTypes.Item))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Unit))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Coin))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Gold))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.ArenaCoin))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.MultiCoin))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.KakeraCoin))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Award))
              ++num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
              ++num;
            if (num > 1)
              break;
          }
          if (num >= 2)
          {
            MailIcon component = mMailListItem.GetComponent<MailIcon>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              if ((UnityEngine.Object) component.CurrentIcon != (UnityEngine.Object) null)
                component.CurrentIcon.SetActive(false);
              component.CurrentIcon = component.SetIconTemplate;
              component.CurrentIcon.SetActive(true);
            }
          }
          else
          {
            MailIcon component = mMailListItem.GetComponent<MailIcon>();
            for (int index2 = 0; index2 < data1.gifts.Length; ++index2)
            {
              GiftData gift = data1.gifts[index2];
              if (!gift.NotSet)
              {
                if (gift.CheckGiftTypeIncluded(GiftTypes.Item | GiftTypes.Unit | GiftTypes.SelectUnitItem | GiftTypes.SelectItem | GiftTypes.SelectArtifactItem))
                {
                  ItemData data2 = new ItemData();
                  data2.Setup(0L, gift.iname, gift.num);
                  DataSource.Bind<ItemData>(mMailListItem, data2);
                  if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                  {
                    if ((UnityEngine.Object) component.CurrentIcon != (UnityEngine.Object) null)
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.ItemIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
                {
                  ArtifactData artifactData = gift.CreateArtifactData();
                  if (artifactData != null)
                    DataSource.Bind<ArtifactData>(mMailListItem, artifactData);
                  if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                  {
                    if ((UnityEngine.Object) component.CurrentIcon != (UnityEngine.Object) null)
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.ArtifactIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Award))
                {
                  AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(gift.iname);
                  ItemData data2 = new ItemData();
                  data2.Setup(0L, awardParam.ToItemParam(), gift.num);
                  DataSource.Bind<ItemData>(mMailListItem, data2);
                  if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                  {
                    if ((UnityEngine.Object) component.CurrentIcon != (UnityEngine.Object) null)
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.ItemIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Coin))
                {
                  if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                  {
                    if ((UnityEngine.Object) component.CurrentIcon != (UnityEngine.Object) null)
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.CoinIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.Gold))
                {
                  if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                  {
                    if ((UnityEngine.Object) component.CurrentIcon != (UnityEngine.Object) null)
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.GoldIconTemplate;
                    component.CurrentIcon.SetActive(true);
                    break;
                  }
                  break;
                }
                if (gift.CheckGiftTypeIncluded(GiftTypes.ArenaCoin))
                {
                  if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                  {
                    if ((UnityEngine.Object) component.CurrentIcon != (UnityEngine.Object) null)
                      component.CurrentIcon.SetActive(false);
                    component.CurrentIcon = component.ArenaCoinIconTemplate;
                    component.CurrentIcon.SetActive(true);
                  }
                }
                else
                {
                  if (gift.CheckGiftTypeIncluded(GiftTypes.MultiCoin))
                  {
                    if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                    {
                      if ((UnityEngine.Object) component.CurrentIcon != (UnityEngine.Object) null)
                        component.CurrentIcon.SetActive(false);
                      component.CurrentIcon = component.MultiCoinIconTemplate;
                      component.CurrentIcon.SetActive(true);
                      break;
                    }
                    break;
                  }
                  if (gift.CheckGiftTypeIncluded(GiftTypes.KakeraCoin))
                  {
                    if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                    {
                      if ((UnityEngine.Object) component.CurrentIcon != (UnityEngine.Object) null)
                        component.CurrentIcon.SetActive(false);
                      component.CurrentIcon = component.KakeraCoinIconTemplate;
                      component.CurrentIcon.SetActive(true);
                      break;
                    }
                    break;
                  }
                }
              }
            }
          }
          MailListItem component1 = mMailListItem.GetComponent<MailListItem>();
          if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
            component1.Set(data1.IsPeriod, data1.IsReadMail(), data1.post_at, data1.read);
        }
      }
      GameParameter.UpdateAll(this.gameObject);
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
        FlowNode_OnMailSelect nodeOnMailSelect = this.GetComponentInParent<FlowNode_OnMailSelect>();
        if ((UnityEngine.Object) nodeOnMailSelect == (UnityEngine.Object) null)
          nodeOnMailSelect = UnityEngine.Object.FindObjectOfType<FlowNode_OnMailSelect>();
        if ((UnityEngine.Object) nodeOnMailSelect != (UnityEngine.Object) null)
          nodeOnMailSelect.Selected();
      }
      this.UpdateItems();
    }
  }
}
