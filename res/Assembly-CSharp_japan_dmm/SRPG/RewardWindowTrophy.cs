// Decompiled with JetBrains decompiler
// Type: SRPG.RewardWindowTrophy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RewardWindowTrophy : RewardWindow
  {
    private GameObject AddRewardList(GameObject copy_src, Transform parent)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(copy_src);
      gameObject.transform.SetParent(parent, false);
      gameObject.SetActive(true);
      this.mItems.Add(gameObject);
      return gameObject;
    }

    public override void Refresh()
    {
      RewardData dataOfClass = DataSource.FindDataOfClass<RewardData>(((Component) this).gameObject, (RewardData) null);
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.ExpRow, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ExpRow.transform.parent : this.ItemList.transform;
        if (dataOfClass.Exp > 0)
          this.AddRewardList(this.ExpRow, parent);
      }
      if (Object.op_Inequality((Object) this.GoldRow, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.GoldRow.transform.parent : this.ItemList.transform;
        if (dataOfClass.Gold > 0)
          this.AddRewardList(this.GoldRow, parent);
      }
      if (Object.op_Inequality((Object) this.CoinRow, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.CoinRow.transform.parent : this.ItemList.transform;
        if (dataOfClass.Coin > 0)
          this.AddRewardList(this.CoinRow, parent);
      }
      if (Object.op_Inequality((Object) this.StaminaRow, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.StaminaRow.transform.parent : this.ItemList.transform;
        if (dataOfClass.Stamina > 0)
          this.AddRewardList(this.StaminaRow, parent);
      }
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
      {
        Transform parent1 = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ItemTemplate.transform.parent : this.ItemList.transform;
        Transform parent2 = (Transform) null;
        if (Object.op_Inequality((Object) this.EventCoinTemplate, (Object) null))
          parent2 = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.EventCoinTemplate.transform.parent : this.ItemList.transform;
        for (int index = 0; index < dataOfClass.Items.Count; ++index)
        {
          ItemData data = dataOfClass.Items[index];
          DataSource.Bind<ItemData>(data.ItemType != EItemType.Unit || !Object.op_Inequality((Object) this.UnitTemplate, (Object) null) ? (data.ItemType != EItemType.EventCoin || Object.op_Equality((Object) parent2, (Object) null) ? this.AddRewardList(this.ItemTemplate, parent1) : this.AddRewardList(this.EventCoinTemplate, parent2)) : this.AddRewardList(this.UnitTemplate, parent1), data);
        }
      }
      if (Object.op_Inequality((Object) this.ArtifactTemplate, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ArtifactTemplate.transform.parent : this.ItemList.transform;
        foreach (ArtifactRewardData artifact in dataOfClass.Artifacts)
          DataSource.Bind<ArtifactRewardData>(this.AddRewardList(this.ArtifactTemplate, parent), artifact);
      }
      if (Object.op_Inequality((Object) this.ConceptCardTemplate, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ConceptCardTemplate.transform.parent : this.ItemList.transform;
        foreach (KeyValuePair<string, GiftRecieveItemData> keyValuePair in dataOfClass.GiftRecieveItemDataDic)
        {
          if (keyValuePair.Value.type == GiftTypes.ConceptCard)
          {
            GameObject gameObject = this.AddRewardList(this.ConceptCardTemplate, parent);
            GiftRecieveItem componentInChildren = gameObject.GetComponentInChildren<GiftRecieveItem>();
            DataSource.Bind<GiftRecieveItemData>(gameObject, keyValuePair.Value);
            gameObject.SetActive(true);
            componentInChildren.UpdateValue();
          }
        }
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
