// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryHighlightGiftWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Screen Tap", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Wait Until Show Buttons", FlowNode.PinTypes.Output, 10)]
  public class GalleryHighlightGiftWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mItems = new List<GameObject>();
    private const int PIN_IN_SCREEN_TAP = 0;
    private const int PIN_OUT_WAIT_UNTIL_SHOW_BUTTONS = 10;
    [SerializeField]
    private GameObject ExitButton;
    [SerializeField]
    private GameObject ReplayButton;
    [SerializeField]
    private CanvasGroup TopCanvas;
    [SerializeField]
    private GameObject ItemHolder;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private GameObject GoldTemplate;
    [SerializeField]
    private GameObject CoinTemplate;
    [SerializeField]
    private GameObject AwardTemplate;
    [SerializeField]
    private GameObject ConceptCardTemplate;
    [SerializeField]
    private GameObject UnitTemplate;

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.ExitButton != (UnityEngine.Object) null)
        this.ExitButton.SetActive(false);
      if ((UnityEngine.Object) this.ReplayButton != (UnityEngine.Object) null)
        this.ReplayButton.SetActive(false);
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.SetActive(false);
      if ((UnityEngine.Object) this.GoldTemplate != (UnityEngine.Object) null)
        this.GoldTemplate.SetActive(false);
      if ((UnityEngine.Object) this.CoinTemplate != (UnityEngine.Object) null)
        this.CoinTemplate.SetActive(false);
      if ((UnityEngine.Object) this.AwardTemplate != (UnityEngine.Object) null)
        this.AwardTemplate.SetActive(false);
      if ((UnityEngine.Object) this.ConceptCardTemplate != (UnityEngine.Object) null)
        this.ConceptCardTemplate.SetActive(false);
      if (!((UnityEngine.Object) this.UnitTemplate != (UnityEngine.Object) null))
        return;
      this.UnitTemplate.SetActive(false);
    }

    public void Refresh(HighlightParam param, bool isReward)
    {
      if (this.mItems != null)
      {
        foreach (UnityEngine.Object mItem in this.mItems)
          UnityEngine.Object.Destroy(mItem);
        this.mItems.Clear();
      }
      if ((UnityEngine.Object) this.ExitButton != (UnityEngine.Object) null)
        this.ExitButton.SetActive(false);
      if ((UnityEngine.Object) this.ReplayButton != (UnityEngine.Object) null)
        this.ReplayButton.SetActive(false);
      HighlightGift gift1 = param.gift;
      if (gift1 != null && gift1.gifts != null && gift1.gifts.Length > 0)
      {
        foreach (HighlightGiftData gift2 in gift1.gifts)
        {
          GameObject gameObject = (GameObject) null;
          switch (gift2.type)
          {
            case HighlightGiftType.Item:
              gameObject = this.CreateItem(gift2.item, gift2.num);
              break;
            case HighlightGiftType.Gold:
              gameObject = this.CreateGoldItem(gift2.num);
              break;
            case HighlightGiftType.Coin:
              gameObject = this.CreateCoinItem(gift2.num);
              break;
            case HighlightGiftType.Award:
              gameObject = this.CreateAwardItem(gift2.item, gift2.num);
              break;
            case HighlightGiftType.Unit:
              gameObject = this.CreateUnit(gift2.item, gift2.num);
              break;
            case HighlightGiftType.ConceptCard:
              gameObject = this.CreateConceptCardItem(gift2.item, gift2.num);
              break;
          }
          if (!((UnityEngine.Object) gameObject == (UnityEngine.Object) null) && !((UnityEngine.Object) this.ItemHolder == (UnityEngine.Object) null))
          {
            HighlightGiftIcon component = gameObject.GetComponent<HighlightGiftIcon>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              component.Initialize();
              if (!isReward)
                component.SetStamp(false);
            }
            this.mItems.Add(gameObject);
            gameObject.transform.SetParent(this.ItemHolder.transform, false);
            gameObject.SetActive(true);
          }
        }
        GameParameter.UpdateAll(this.gameObject);
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private GameObject CreateItem(string iname, int num)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
      ItemData data = new ItemData();
      data.Setup(0L, iname, num);
      DataSource.Bind<ItemData>(gameObject, data, false);
      return gameObject;
    }

    private GameObject CreateCoinItem(int num)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CoinTemplate);
      DataSource.Bind<int>(gameObject, num, false);
      return gameObject;
    }

    private GameObject CreateGoldItem(int num)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GoldTemplate);
      DataSource.Bind<int>(gameObject, num, false);
      return gameObject;
    }

    private GameObject CreateAwardItem(string iname, int num)
    {
      AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(iname);
      if (awardParam == null)
        return (GameObject) null;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AwardTemplate);
      ItemData data = new ItemData();
      data.Setup(0L, awardParam.ToItemParam(), num);
      DataSource.Bind<ItemData>(gameObject, data, false);
      return gameObject;
    }

    private GameObject CreateConceptCardItem(string iname, int num)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ConceptCardTemplate);
      QuestResult.DropItemData data = new QuestResult.DropItemData();
      data.SetupDropItemData(EBattleRewardType.ConceptCard, 0L, iname, num);
      DataSource.Bind<QuestResult.DropItemData>(gameObject, data, false);
      return gameObject;
    }

    private GameObject CreateUnit(string iname, int num)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitTemplate);
      ItemData data = new ItemData();
      data.Setup(0L, iname, num);
      DataSource.Bind<ItemData>(gameObject, data, false);
      return gameObject;
    }
  }
}
