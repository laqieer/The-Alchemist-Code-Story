// Decompiled with JetBrains decompiler
// Type: SRPG.RewardWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  public class RewardWindow : MonoBehaviour, IFlowInterface
  {
    public bool RefreshOnStart;
    public bool UseGlobalVar;
    public bool UseBindDataOnly;
    public GameObject ExpRow;
    public GameObject GoldRow;
    public GameObject CoinRow;
    public GameObject ArenaMedalRow;
    public GameObject MultiCoinRow;
    public GameObject KakeraCoinRow;
    public GameObject StaminaRow;
    public GameObject ItemSeparator;
    public GameObject ItemList;
    public GameObject ItemTemplate;
    public GameObject EventCoinTemplate;
    public GameObject ArtifactTemplate;
    public GameObject ArtifactTemplate2;
    public GameObject UnitTemplate;
    public GameObject ConceptCardTemplate;
    public GameObject Caution;
    protected List<GameObject> mItems = new List<GameObject>();

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.EventCoinTemplate, (Object) null))
        this.EventCoinTemplate.gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.ArtifactTemplate2, (Object) null))
        this.ArtifactTemplate2.gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.ConceptCardTemplate, (Object) null))
        this.ConceptCardTemplate.gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.UnitTemplate, (Object) null))
        this.UnitTemplate.gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.Caution, (Object) null))
        return;
      this.Caution.SetActive(false);
    }

    private void Start()
    {
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    public virtual void Refresh()
    {
      if (this.UseGlobalVar)
        DataSource.Bind<RewardData>(((Component) this).gameObject, (RewardData) GlobalVars.LastReward);
      RewardData dataOfClass = DataSource.FindDataOfClass<RewardData>(((Component) this).gameObject, (RewardData) null);
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.ArenaMedalRow, (Object) null))
        this.ArenaMedalRow.SetActive(dataOfClass.ArenaMedal > 0);
      if (Object.op_Inequality((Object) this.MultiCoinRow, (Object) null))
        this.MultiCoinRow.SetActive(dataOfClass.MultiCoin > 0);
      if (Object.op_Inequality((Object) this.KakeraCoinRow, (Object) null))
        this.KakeraCoinRow.SetActive(dataOfClass.KakeraCoin > 0);
      if (Object.op_Inequality((Object) this.ExpRow, (Object) null))
        this.ExpRow.SetActive(dataOfClass.Exp > 0);
      if (Object.op_Inequality((Object) this.GoldRow, (Object) null))
        this.GoldRow.SetActive(dataOfClass.Gold > 0);
      if (Object.op_Inequality((Object) this.CoinRow, (Object) null))
        this.CoinRow.SetActive(dataOfClass.Coin > 0);
      if (Object.op_Inequality((Object) this.StaminaRow, (Object) null))
        this.StaminaRow.SetActive(dataOfClass.Stamina > 0);
      GameParameter.UpdateAll(((Component) this).gameObject);
      if (Object.op_Inequality((Object) this.ItemSeparator, (Object) null))
        this.ItemSeparator.SetActive(dataOfClass.Items.Count > 0);
      if (Object.op_Inequality((Object) this.ArtifactTemplate, (Object) null))
      {
        Transform transform = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ArtifactTemplate.transform.parent : this.ItemList.transform;
        foreach (GiftRecieveItemData data in dataOfClass.GiftRecieveItemDataDic.Values)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ArtifactTemplate);
          this.mItems.Add(gameObject);
          DataSource.Bind<GiftRecieveItemData>(gameObject, data);
          gameObject.transform.SetParent(transform, false);
          gameObject.SetActive(true);
        }
      }
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
      {
        Transform itemParent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ItemTemplate.transform.parent : this.ItemList.transform;
        this.RefreshItems(dataOfClass, itemParent, this.ItemTemplate);
      }
      if (Object.op_Inequality((Object) this.ArtifactTemplate2, (Object) null))
      {
        Transform parent = this.ArtifactTemplate2.transform.parent;
        this.RefreshArtifacts(dataOfClass, parent, this.ArtifactTemplate2);
      }
      if (Object.op_Inequality((Object) this.ConceptCardTemplate, (Object) null))
      {
        Transform parent = this.ConceptCardTemplate.transform.parent;
        this.RefreshConceptCards(dataOfClass, parent, this.ConceptCardTemplate);
      }
      if (Object.op_Inequality((Object) this.UnitTemplate, (Object) null))
      {
        Transform parent = this.UnitTemplate.transform.parent;
        this.RefreshUnits(dataOfClass, parent, this.UnitTemplate);
      }
      if (!Object.op_Inequality((Object) this.Caution, (Object) null) || !dataOfClass.IsOverLimit)
        return;
      this.Caution.transform.SetSiblingIndex(this.Caution.transform.parent.childCount);
      this.Caution.gameObject.SetActive(true);
    }

    private void RefreshItems(RewardData reward, Transform itemParent, GameObject template)
    {
      if (reward.Items == null || reward.Items.Count <= 0)
        return;
      Transform transform = (Transform) null;
      if (Object.op_Inequality((Object) this.EventCoinTemplate, (Object) null))
        transform = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.EventCoinTemplate.transform.parent : this.ItemList.transform;
      List<ItemParam> itemParamList = (List<ItemParam>) null;
      for (int index = 0; index < reward.Items.Count; ++index)
      {
        ItemData data = reward.Items[index];
        GameObject gameObject;
        if (data.ItemType != EItemType.EventCoin || Object.op_Equality((Object) transform, (Object) null))
        {
          gameObject = Object.Instantiate<GameObject>(template);
          gameObject.transform.SetParent(itemParent, false);
        }
        else
        {
          gameObject = Object.Instantiate<GameObject>(this.EventCoinTemplate);
          gameObject.transform.SetParent(transform, false);
        }
        this.mItems.Add(gameObject);
        DataSource.Bind<ItemData>(gameObject, data);
        gameObject.SetActive(true);
        if (!this.UseBindDataOnly)
        {
          if (itemParamList == null)
            itemParamList = new List<ItemParam>();
          itemParamList.Add(data.Param);
        }
      }
    }

    private void RefreshArtifacts(RewardData reward, Transform itemParent, GameObject template)
    {
      if (reward.Artifacts == null || reward.Artifacts.Count <= 0)
        return;
      foreach (ArtifactRewardData artifact in reward.Artifacts)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(template);
        gameObject.transform.SetParent(itemParent, false);
        this.mItems.Add(gameObject);
        DataSource.Bind<ArtifactRewardData>(gameObject, artifact);
        gameObject.SetActive(true);
      }
    }

    private void RefreshConceptCards(RewardData reward, Transform itemParent, GameObject template)
    {
      foreach (KeyValuePair<string, GiftRecieveItemData> keyValuePair in reward.GiftRecieveItemDataDic)
      {
        if (keyValuePair.Value.type == GiftTypes.ConceptCard)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(template);
          gameObject.transform.SetParent(itemParent, false);
          this.mItems.Add(gameObject);
          GiftRecieveItem componentInChildren = gameObject.GetComponentInChildren<GiftRecieveItem>();
          DataSource.Bind<GiftRecieveItemData>(gameObject, keyValuePair.Value);
          gameObject.SetActive(true);
          componentInChildren.UpdateValue();
        }
      }
    }

    private void RefreshUnits(RewardData reward, Transform itemParent, GameObject template)
    {
      foreach (KeyValuePair<string, GiftRecieveItemData> keyValuePair in reward.GiftRecieveItemDataDic)
      {
        if (keyValuePair.Value.type == GiftTypes.Unit)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(template);
          gameObject.transform.SetParent(itemParent, false);
          this.mItems.Add(gameObject);
          GiftRecieveItem componentInChildren = gameObject.GetComponentInChildren<GiftRecieveItem>();
          DataSource.Bind<GiftRecieveItemData>(gameObject, keyValuePair.Value);
          gameObject.SetActive(true);
          componentInChildren.UpdateValue();
        }
      }
    }
  }
}
