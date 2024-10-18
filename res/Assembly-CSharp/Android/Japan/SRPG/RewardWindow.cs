// Decompiled with JetBrains decompiler
// Type: SRPG.RewardWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  public class RewardWindow : MonoBehaviour, IFlowInterface
  {
    protected List<GameObject> mItems = new List<GameObject>();
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

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.EventCoinTemplate != (UnityEngine.Object) null)
        this.EventCoinTemplate.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.ArtifactTemplate2 != (UnityEngine.Object) null)
        this.ArtifactTemplate2.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.ConceptCardTemplate != (UnityEngine.Object) null)
        this.ConceptCardTemplate.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.UnitTemplate != (UnityEngine.Object) null)
        this.UnitTemplate.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this.Caution != (UnityEngine.Object) null))
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
        DataSource.Bind<RewardData>(this.gameObject, (RewardData) GlobalVars.LastReward, false);
      RewardData dataOfClass = DataSource.FindDataOfClass<RewardData>(this.gameObject, (RewardData) null);
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      if (dataOfClass == null)
        return;
      if ((UnityEngine.Object) this.ArenaMedalRow != (UnityEngine.Object) null)
        this.ArenaMedalRow.SetActive(dataOfClass.ArenaMedal > 0);
      if ((UnityEngine.Object) this.MultiCoinRow != (UnityEngine.Object) null)
        this.MultiCoinRow.SetActive(dataOfClass.MultiCoin > 0);
      if ((UnityEngine.Object) this.KakeraCoinRow != (UnityEngine.Object) null)
        this.KakeraCoinRow.SetActive(dataOfClass.KakeraCoin > 0);
      if ((UnityEngine.Object) this.ExpRow != (UnityEngine.Object) null)
        this.ExpRow.SetActive(dataOfClass.Exp > 0);
      if ((UnityEngine.Object) this.GoldRow != (UnityEngine.Object) null)
        this.GoldRow.SetActive(dataOfClass.Gold > 0);
      if ((UnityEngine.Object) this.CoinRow != (UnityEngine.Object) null)
        this.CoinRow.SetActive(dataOfClass.Coin > 0);
      if ((UnityEngine.Object) this.StaminaRow != (UnityEngine.Object) null)
        this.StaminaRow.SetActive(dataOfClass.Stamina > 0);
      GameParameter.UpdateAll(this.gameObject);
      if ((UnityEngine.Object) this.ItemSeparator != (UnityEngine.Object) null)
        this.ItemSeparator.SetActive(dataOfClass.Items.Count > 0);
      if ((UnityEngine.Object) this.ArtifactTemplate != (UnityEngine.Object) null)
      {
        Transform parent = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.ArtifactTemplate.transform.parent : this.ItemList.transform;
        foreach (GiftRecieveItemData data in dataOfClass.GiftRecieveItemDataDic.Values)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactTemplate);
          this.mItems.Add(gameObject);
          DataSource.Bind<GiftRecieveItemData>(gameObject, data, false);
          gameObject.transform.SetParent(parent, false);
          gameObject.SetActive(true);
        }
      }
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
      {
        Transform itemParent = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.ItemTemplate.transform.parent : this.ItemList.transform;
        this.RefreshItems(dataOfClass, itemParent, this.ItemTemplate);
      }
      if ((UnityEngine.Object) this.ArtifactTemplate2 != (UnityEngine.Object) null)
      {
        Transform parent = this.ArtifactTemplate2.transform.parent;
        this.RefreshArtifacts(dataOfClass, parent, this.ArtifactTemplate2);
      }
      if ((UnityEngine.Object) this.ConceptCardTemplate != (UnityEngine.Object) null)
      {
        Transform parent = this.ConceptCardTemplate.transform.parent;
        this.RefreshConceptCards(dataOfClass, parent, this.ConceptCardTemplate);
      }
      if ((UnityEngine.Object) this.UnitTemplate != (UnityEngine.Object) null)
      {
        Transform parent = this.UnitTemplate.transform.parent;
        this.RefreshUnits(dataOfClass, parent, this.UnitTemplate);
      }
      if (!((UnityEngine.Object) this.Caution != (UnityEngine.Object) null) || !dataOfClass.IsOverLimit)
        return;
      this.Caution.transform.SetSiblingIndex(this.Caution.transform.parent.childCount);
      this.Caution.gameObject.SetActive(true);
    }

    private void RefreshItems(RewardData reward, Transform itemParent, GameObject template)
    {
      if (reward.Items == null || reward.Items.Count <= 0)
        return;
      Transform parent = (Transform) null;
      if ((UnityEngine.Object) this.EventCoinTemplate != (UnityEngine.Object) null)
        parent = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.EventCoinTemplate.transform.parent : this.ItemList.transform;
      List<ItemParam> itemParamList = (List<ItemParam>) null;
      for (int index = 0; index < reward.Items.Count; ++index)
      {
        ItemData data = reward.Items[index];
        GameObject gameObject;
        if (data.ItemType != EItemType.EventCoin || (UnityEngine.Object) parent == (UnityEngine.Object) null)
        {
          gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
          gameObject.transform.SetParent(itemParent, false);
        }
        else
        {
          gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EventCoinTemplate);
          gameObject.transform.SetParent(parent, false);
        }
        this.mItems.Add(gameObject);
        DataSource.Bind<ItemData>(gameObject, data, false);
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
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
        gameObject.transform.SetParent(itemParent, false);
        this.mItems.Add(gameObject);
        DataSource.Bind<ArtifactRewardData>(gameObject, artifact, false);
        gameObject.SetActive(true);
      }
    }

    private void RefreshConceptCards(RewardData reward, Transform itemParent, GameObject template)
    {
      foreach (KeyValuePair<string, GiftRecieveItemData> keyValuePair in reward.GiftRecieveItemDataDic)
      {
        if (keyValuePair.Value.type == GiftTypes.ConceptCard)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
          gameObject.transform.SetParent(itemParent, false);
          this.mItems.Add(gameObject);
          GiftRecieveItem componentInChildren = gameObject.GetComponentInChildren<GiftRecieveItem>();
          DataSource.Bind<GiftRecieveItemData>(gameObject, keyValuePair.Value, false);
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
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
          gameObject.transform.SetParent(itemParent, false);
          this.mItems.Add(gameObject);
          GiftRecieveItem componentInChildren = gameObject.GetComponentInChildren<GiftRecieveItem>();
          DataSource.Bind<GiftRecieveItemData>(gameObject, keyValuePair.Value, false);
          gameObject.SetActive(true);
          componentInChildren.UpdateValue();
        }
      }
    }
  }
}
