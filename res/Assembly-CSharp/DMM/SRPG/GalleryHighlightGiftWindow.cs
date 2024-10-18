// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryHighlightGiftWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Screen Tap", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Wait Until Show Buttons", FlowNode.PinTypes.Output, 10)]
  public class GalleryHighlightGiftWindow : MonoBehaviour, IFlowInterface
  {
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
    [SerializeField]
    private GameObject ArtifactTemplate;
    private List<GameObject> mItems = new List<GameObject>();

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ExitButton, (Object) null))
        this.ExitButton.SetActive(false);
      if (Object.op_Inequality((Object) this.ReplayButton, (Object) null))
        this.ReplayButton.SetActive(false);
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.GoldTemplate, (Object) null))
        this.GoldTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.CoinTemplate, (Object) null))
        this.CoinTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.AwardTemplate, (Object) null))
        this.AwardTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ConceptCardTemplate, (Object) null))
        this.ConceptCardTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.UnitTemplate, (Object) null))
        this.UnitTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.ArtifactTemplate, (Object) null))
        return;
      this.ArtifactTemplate.SetActive(false);
    }

    public void Refresh(HighlightParam param, bool isReward)
    {
      if (this.mItems != null)
      {
        foreach (Object mItem in this.mItems)
          Object.Destroy(mItem);
        this.mItems.Clear();
      }
      if (Object.op_Inequality((Object) this.ExitButton, (Object) null))
        this.ExitButton.SetActive(false);
      if (Object.op_Inequality((Object) this.ReplayButton, (Object) null))
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
            case HighlightGiftType.Artifact:
              gameObject = this.CreateArtifact(gift2.item, gift2.num);
              break;
          }
          if (!Object.op_Equality((Object) gameObject, (Object) null) && !Object.op_Equality((Object) this.ItemHolder, (Object) null))
          {
            HighlightGiftIcon component = gameObject.GetComponent<HighlightGiftIcon>();
            if (Object.op_Inequality((Object) component, (Object) null))
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
        GameParameter.UpdateAll(((Component) this).gameObject);
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private GameObject CreateItem(string iname, int num)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
      ItemData data = new ItemData();
      data.Setup(0L, iname, num);
      DataSource.Bind<ItemData>(gameObject, data);
      return gameObject;
    }

    private GameObject CreateCoinItem(int num)
    {
      GameObject coinItem = Object.Instantiate<GameObject>(this.CoinTemplate);
      DataSource.Bind<int>(coinItem, num);
      return coinItem;
    }

    private GameObject CreateGoldItem(int num)
    {
      GameObject goldItem = Object.Instantiate<GameObject>(this.GoldTemplate);
      DataSource.Bind<int>(goldItem, num);
      return goldItem;
    }

    private GameObject CreateAwardItem(string iname, int num)
    {
      AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(iname);
      if (awardParam == null)
        return (GameObject) null;
      GameObject awardItem = Object.Instantiate<GameObject>(this.AwardTemplate);
      ItemData data = new ItemData();
      data.Setup(0L, awardParam.ToItemParam(), num);
      DataSource.Bind<ItemData>(awardItem, data);
      return awardItem;
    }

    private GameObject CreateConceptCardItem(string iname, int num)
    {
      GameObject conceptCardItem = Object.Instantiate<GameObject>(this.ConceptCardTemplate);
      QuestResult.DropItemData data = new QuestResult.DropItemData();
      data.SetupDropItemData(EBattleRewardType.ConceptCard, 0L, iname, num);
      DataSource.Bind<QuestResult.DropItemData>(conceptCardItem, data);
      return conceptCardItem;
    }

    private GameObject CreateUnit(string iname, int num)
    {
      GameObject unit = Object.Instantiate<GameObject>(this.UnitTemplate);
      ItemData data = new ItemData();
      data.Setup(0L, iname, num);
      DataSource.Bind<ItemData>(unit, data);
      return unit;
    }

    private GameObject CreateArtifact(string iname, int num)
    {
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(iname);
      if (artifactParam == null)
        return (GameObject) null;
      GameObject artifact = Object.Instantiate<GameObject>(this.ArtifactTemplate);
      DataSource.Bind<ArtifactParam>(artifact, artifactParam);
      DataSource.Bind<int>(artifact, num);
      return artifact;
    }
  }
}
