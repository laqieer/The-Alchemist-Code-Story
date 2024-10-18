// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBuySetConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class EventShopBuySetConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private List<EventShopSetItemListElement> event_shop_item_set_list = new List<EventShopSetItemListElement>();
    public int AbilityListItem_Unlocked = 2;
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

    private void Awake()
    {
    }

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      EventShopItem data = MonoSingleton<GameManager>.Instance.Player.GetEventShopData().items[GlobalVars.ShopBuyIndex];
      this.ItemWindow.SetActive(!data.IsArtifact);
      this.ArtifactWindow.SetActive(data.IsArtifact);
      if ((UnityEngine.Object) this.AmountNum != (UnityEngine.Object) null)
        this.AmountNum.text = data.remaining_num.ToString();
      if ((UnityEngine.Object) this.Sold != (UnityEngine.Object) null)
        this.Sold.SetActive(!data.IsNotLimited);
      if (data.IsArtifact)
      {
        ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data.iname);
        DataSource.Bind<ArtifactParam>(this.gameObject, artifactParam);
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
        this.ArtifactStatus.SetValues(fixed_status, scale_status);
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
          DataSource.Bind<AbilityParam>(this.ArtifactAbility, abilityParam);
          if (flag)
            this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
          else
            this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        }
        else
          this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
      }
      else
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(data.iname);
        this.event_shop_item_set_list.Clear();
        if (data.IsSet)
        {
          for (int index = 0; index < data.children.Length; ++index)
          {
            GameObject gameObject = index >= this.event_shop_item_set_list.Count ? UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate) : this.event_shop_item_set_list[index].gameObject;
            if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
            {
              gameObject.SetActive(true);
              Vector3 localScale = gameObject.transform.localScale;
              gameObject.transform.SetParent(this.ItemParent.transform);
              gameObject.transform.localScale = localScale;
              EventShopSetItemListElement component = gameObject.GetComponent<EventShopSetItemListElement>();
              StringBuilder stringBuilder = GameUtility.GetStringBuilder();
              if (data.children[index].IsArtifact)
              {
                ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data.children[index].iname);
                if (artifactParam != null)
                  stringBuilder.Append(artifactParam.name);
                component.ArtifactParam = artifactParam;
              }
              else
              {
                ItemData itemData = new ItemData();
                itemData.Setup(0L, data.children[index].iname, data.children[index].num);
                if (itemData != null)
                  stringBuilder.Append(itemData.Param.name);
                component.itemData = itemData;
              }
              stringBuilder.Append("×");
              stringBuilder.Append(data.children[index].num.ToString());
              component.itemName.text = stringBuilder.ToString();
              component.SetShopItemDesc(data.children[index]);
              this.event_shop_item_set_list.Add(component);
            }
          }
        }
        DataSource.Bind<ItemData>(this.gameObject, itemDataByItemId);
        DataSource.Bind<ItemParam>(this.gameObject, MonoSingleton<GameManager>.Instance.GetItemParam(data.iname));
      }
      DataSource.Bind<EventShopItem>(this.gameObject, data);
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
  }
}
