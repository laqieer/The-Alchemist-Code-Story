// Decompiled with JetBrains decompiler
// Type: SRPG.RewardWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      if (!((UnityEngine.Object) this.UnitTemplate != (UnityEngine.Object) null))
        return;
      this.UnitTemplate.gameObject.SetActive(false);
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
        DataSource.Bind<RewardData>(this.gameObject, (RewardData) GlobalVars.LastReward);
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
        using (Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator = dataOfClass.GiftRecieveItemDataDic.Values.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            GiftRecieveItemData current = enumerator.Current;
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactTemplate);
            this.mItems.Add(gameObject);
            DataSource.Bind<GiftRecieveItemData>(gameObject, current);
            gameObject.transform.SetParent(parent, false);
            gameObject.SetActive(true);
          }
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
      if (!((UnityEngine.Object) this.UnitTemplate != (UnityEngine.Object) null))
        return;
      Transform parent1 = this.UnitTemplate.transform.parent;
      this.RefreshUnits(dataOfClass, parent1, this.UnitTemplate);
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
      using (List<ArtifactRewardData>.Enumerator enumerator = reward.Artifacts.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ArtifactRewardData current = enumerator.Current;
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
          gameObject.transform.SetParent(itemParent, false);
          this.mItems.Add(gameObject);
          DataSource.Bind<ArtifactRewardData>(gameObject, current);
          gameObject.SetActive(true);
        }
      }
    }

    private void RefreshUnits(RewardData reward, Transform itemParent, GameObject template)
    {
      using (Dictionary<string, GiftRecieveItemData>.Enumerator enumerator = reward.GiftRecieveItemDataDic.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, GiftRecieveItemData> current = enumerator.Current;
          if (current.Value.type == GiftTypes.Unit)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
            gameObject.transform.SetParent(itemParent, false);
            this.mItems.Add(gameObject);
            GiftRecieveItem componentInChildren = gameObject.GetComponentInChildren<GiftRecieveItem>();
            DataSource.Bind<GiftRecieveItemData>(gameObject, current.Value);
            gameObject.SetActive(true);
            componentInChildren.UpdateValue();
          }
        }
      }
    }
  }
}
