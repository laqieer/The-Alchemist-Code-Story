// Decompiled with JetBrains decompiler
// Type: SRPG.ShopBuySetConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Slider Plus", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Slider Minus", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "武具詳細情報セット(in)", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "武具詳細情報セット(out)", FlowNode.PinTypes.Output, 101)]
  public class ShopBuySetConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private List<ShopSetItemListElement> shop_item_set_list = new List<ShopSetItemListElement>();
    public int AbilityListItem_Unlocked = 2;
    private const int PINID_REFRESH = 1;
    private const int PINID_SLIDER_PLUS = 10;
    private const int PINID_SLIDER_MINUS = 11;
    private const int PINID_ARTIFACT_DETAIL_SET_INPUT = 100;
    private const int PINID_ARTIFACT_DETAIL_SET_OUTPUT = 101;
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    public GameObject ItemParent;
    public GameObject ItemWindow;
    public GameObject ArtifactWindow;
    public StatusList ArtifactStatus;
    private ArtifactParam mArtifactParam;
    private bool mIsShowArtifactJob;
    public GameObject ArtifactAbility;
    public Animator ArtifactAbilityAnimation;
    public string AbilityListItemState;
    public int AbilityListItem_Hidden;
    public UnityEngine.UI.Text AmountNum;
    public GameObject Sold;
    [Space(20f)]
    public GameObject ItemAmountSliderHolder;
    public Slider ItemAmountSlider;
    public UnityEngine.UI.Text ItemAmountSliderNum;
    public Button ItemIncrementButton;
    public Button ItemDecrementButton;
    [Space(20f)]
    public GameObject ArtifactAmountSliderHolder;
    public Slider ArtifactAmountSlider;
    public UnityEngine.UI.Text ArtifactAmountSliderNum;
    public Button ArtifactIncrementButton;
    public Button ArtifactDecrementButton;
    [Space(20f)]
    public UnityEngine.UI.Text LimitedItemPriceText;
    [HeaderBar("▼セット効果確認用のボタン")]
    [SerializeField]
    private Button m_SetEffectsButton;
    private GameObject AmountSliderHolder;
    private Slider AmountSlider;
    private UnityEngine.UI.Text AmountSliderNum;
    private Button IncrementButton;
    private Button DecrementButton;
    private bool mEnabledSlider;
    private ShopItem mShopitem;

    private void Awake()
    {
    }

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Refresh();
          break;
        case 10:
          this.IncrementSliderValue();
          break;
        case 11:
          this.DecrementSliderValue();
          break;
        case 100:
          this.SetArtifactDetailData();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
      }
    }

    private void Refresh()
    {
      this.mShopitem = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType).items.FirstOrDefault<ShopItem>((Func<ShopItem, bool>) (item => item.id == GlobalVars.ShopBuyIndex));
      this.ItemWindow.SetActive(!this.mShopitem.IsArtifact);
      this.ArtifactWindow.SetActive(this.mShopitem.IsArtifact);
      if ((UnityEngine.Object) this.AmountNum != (UnityEngine.Object) null)
        this.AmountNum.text = this.mShopitem.remaining_num.ToString();
      if ((UnityEngine.Object) this.Sold != (UnityEngine.Object) null)
        this.Sold.SetActive(!this.mShopitem.IsNotLimited);
      if (this.mShopitem.IsArtifact)
      {
        ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.mShopitem.iname);
        DataSource.Bind<ArtifactParam>(this.gameObject, artifactParam, false);
        this.mArtifactParam = artifactParam;
        ArtifactData artifactData = new ArtifactData();
        artifactData.Deserialize(new Json_Artifact()
        {
          iname = artifactParam.iname,
          rare = artifactParam.rareini
        });
        BaseStatus fixed_status = new BaseStatus();
        BaseStatus scale_status = new BaseStatus();
        artifactData.GetHomePassiveBuffStatus(ref fixed_status, ref scale_status, (UnitData) null, 0, true);
        this.ArtifactStatus.SetValues(fixed_status, scale_status, false);
        if (artifactParam.abil_inames != null && artifactParam.abil_inames.Length > 0)
        {
          AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(artifactParam.abil_inames[0]);
          List<AbilityData> learningAbilities = artifactData.LearningAbilities;
          bool flag = false;
          if (learningAbilities != null)
          {
            for (int index = 0; index < learningAbilities.Count; ++index)
            {
              AbilityData abilityData = learningAbilities[index];
              if (abilityData != null && abilityParam.iname == abilityData.Param.iname)
              {
                flag = true;
                break;
              }
            }
          }
          DataSource.Bind<AbilityParam>(this.ArtifactAbility, abilityParam, false);
          if (flag)
            this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
          else
            this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        }
        else
          this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        if ((UnityEngine.Object) this.m_SetEffectsButton != (UnityEngine.Object) null && (UnityEngine.Object) this.m_SetEffectsButton != (UnityEngine.Object) null && artifactParam != null)
        {
          this.m_SetEffectsButton.interactable = MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(artifactParam.iname);
          if (this.m_SetEffectsButton.interactable)
            ArtifactSetList.SetSelectedArtifactParam(artifactParam);
        }
      }
      else
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mShopitem.iname);
        this.shop_item_set_list.Clear();
        if (this.mShopitem.IsSet)
        {
          for (int index = 0; index < this.mShopitem.children.Length; ++index)
          {
            GameObject gameObject = index >= this.shop_item_set_list.Count ? UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate) : this.shop_item_set_list[index].gameObject;
            if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
            {
              gameObject.SetActive(true);
              Vector3 localScale = gameObject.transform.localScale;
              gameObject.transform.SetParent(this.ItemParent.transform);
              gameObject.transform.localScale = localScale;
              ShopSetItemListElement component = gameObject.GetComponent<ShopSetItemListElement>();
              StringBuilder stringBuilder = GameUtility.GetStringBuilder();
              if (this.mShopitem.children[index].IsArtifact)
              {
                ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.mShopitem.children[index].iname);
                if (artifactParam != null)
                  stringBuilder.Append(artifactParam.name);
                component.ArtifactParam = artifactParam;
              }
              else if (this.mShopitem.children[index].IsConceptCard)
              {
                ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(this.mShopitem.children[index].iname);
                if (cardDataForDisplay != null)
                  stringBuilder.Append(cardDataForDisplay.Param.name);
                component.SetupConceptCard(cardDataForDisplay);
              }
              else
              {
                ItemData itemData = new ItemData();
                itemData.Setup(0L, this.mShopitem.children[index].iname, this.mShopitem.children[index].num);
                if (itemData != null)
                  stringBuilder.Append(itemData.Param.name);
                component.itemData = itemData;
              }
              stringBuilder.Append("×");
              stringBuilder.Append(this.mShopitem.children[index].num.ToString());
              component.itemName.text = stringBuilder.ToString();
              component.SetShopItemDesc(this.mShopitem.children[index]);
              this.shop_item_set_list.Add(component);
            }
          }
        }
        DataSource.Bind<ItemData>(this.gameObject, itemDataByItemId, false);
        DataSource.Bind<ItemParam>(this.gameObject, MonoSingleton<GameManager>.Instance.GetItemParam(this.mShopitem.iname), false);
      }
      if (this.mShopitem.IsArtifact)
      {
        this.AmountSliderHolder = this.ArtifactAmountSliderHolder;
        this.AmountSlider = this.ArtifactAmountSlider;
        this.AmountSliderNum = this.ArtifactAmountSliderNum;
        this.IncrementButton = this.ArtifactIncrementButton;
        this.DecrementButton = this.ArtifactDecrementButton;
      }
      else
      {
        this.AmountSliderHolder = this.ItemAmountSliderHolder;
        this.AmountSlider = this.ItemAmountSlider;
        this.AmountSliderNum = this.ItemAmountSliderNum;
        this.IncrementButton = this.ItemIncrementButton;
        this.DecrementButton = this.ItemDecrementButton;
      }
      this.mEnabledSlider = false;
      if ((UnityEngine.Object) this.AmountSliderHolder != (UnityEngine.Object) null && (UnityEngine.Object) this.AmountSlider != (UnityEngine.Object) null && (UnityEngine.Object) this.AmountSliderNum != (UnityEngine.Object) null)
      {
        if (!this.mShopitem.IsNotLimited && this.mShopitem.remaining_num > 1)
        {
          this.mEnabledSlider = true;
          GameParameter component = this.LimitedItemPriceText.GetComponent<GameParameter>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.enabled = false;
          this.AmountSliderHolder.SetActive(true);
          int remainingCurrency = ShopData.GetRemainingCurrency(this.mShopitem);
          int buyPrice = ShopData.GetBuyPrice(this.mShopitem);
          int num1 = 1;
          if (buyPrice > 0)
          {
            while (buyPrice * num1 <= remainingCurrency)
            {
              ++num1;
              if (this.mShopitem.remaining_num + 1 < num1)
                break;
            }
          }
          int num2 = Math.Max(Math.Min(num1 - 1, this.mShopitem.remaining_num), 1);
          this.AmountSlider.minValue = 1f;
          this.AmountSlider.maxValue = (float) num2;
          this.SetSliderValue(1f);
          this.AmountSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnSliderValueChanged));
        }
        else
        {
          this.mEnabledSlider = false;
          this.AmountSliderHolder.SetActive(false);
        }
      }
      DataSource.Bind<ShopItem>(this.gameObject, this.mShopitem, false);
      GameParameter.UpdateAll(this.gameObject);
    }

    public void ShowJobList()
    {
      if (this.mIsShowArtifactJob || this.mArtifactParam == null)
        return;
      GlobalVars.ConditionJobs = this.mArtifactParam.condition_jobs;
      this.mIsShowArtifactJob = true;
    }

    public void CloseJobList()
    {
      this.mIsShowArtifactJob = false;
    }

    private void IncrementSliderValue()
    {
      this.SetSliderValue(this.AmountSlider.value + 1f);
    }

    private void DecrementSliderValue()
    {
      this.SetSliderValue(this.AmountSlider.value - 1f);
    }

    private void SetSliderValue(float newValue)
    {
      this.AmountSlider.value = newValue;
      this.AmountSliderNum.text = ((int) this.AmountSlider.value).ToString();
      if ((double) this.AmountSlider.value <= (double) this.AmountSlider.minValue)
        this.DecrementButton.interactable = false;
      else
        this.DecrementButton.interactable = true;
      if ((double) this.AmountSlider.value >= (double) this.AmountSlider.maxValue)
        this.IncrementButton.interactable = false;
      else
        this.IncrementButton.interactable = true;
      if ((double) this.AmountSlider.maxValue == 1.0 && (double) this.AmountSlider.minValue == 1.0)
        this.AmountSlider.interactable = false;
      else
        this.AmountSlider.interactable = true;
      this.LimitedItemPriceText.text = (ShopData.GetBuyPrice(this.mShopitem) * (int) this.AmountSlider.value).ToString();
    }

    private void SetArtifactDetailData()
    {
      if (!this.mShopitem.IsArtifact)
        return;
      ArtifactDetailWindow.SetArtifactParam(MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.mShopitem.iname));
    }

    private void OnSliderValueChanged(float newValue)
    {
      this.SetSliderValue(newValue);
    }

    public void UpdateBuyAmount()
    {
      if (this.mEnabledSlider)
        GlobalVars.ShopBuyAmount = (int) this.AmountSlider.value;
      else
        GlobalVars.ShopBuyAmount = 1;
    }
  }
}
