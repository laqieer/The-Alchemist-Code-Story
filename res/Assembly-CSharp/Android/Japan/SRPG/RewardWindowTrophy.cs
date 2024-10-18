// Decompiled with JetBrains decompiler
// Type: SRPG.RewardWindowTrophy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class RewardWindowTrophy : RewardWindow
  {
    private GameObject AddRewardList(GameObject copy_src, Transform parent)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(copy_src);
      gameObject.transform.SetParent(parent, false);
      gameObject.SetActive(true);
      this.mItems.Add(gameObject);
      return gameObject;
    }

    public override void Refresh()
    {
      RewardData dataOfClass = DataSource.FindDataOfClass<RewardData>(this.gameObject, (RewardData) null);
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      if (dataOfClass == null)
        return;
      if ((UnityEngine.Object) this.ExpRow != (UnityEngine.Object) null)
      {
        Transform parent = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.ExpRow.transform.parent : this.ItemList.transform;
        if (dataOfClass.Exp > 0)
          this.AddRewardList(this.ExpRow, parent);
      }
      if ((UnityEngine.Object) this.GoldRow != (UnityEngine.Object) null)
      {
        Transform parent = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.GoldRow.transform.parent : this.ItemList.transform;
        if (dataOfClass.Gold > 0)
          this.AddRewardList(this.GoldRow, parent);
      }
      if ((UnityEngine.Object) this.CoinRow != (UnityEngine.Object) null)
      {
        Transform parent = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.CoinRow.transform.parent : this.ItemList.transform;
        if (dataOfClass.Coin > 0)
          this.AddRewardList(this.CoinRow, parent);
      }
      if ((UnityEngine.Object) this.StaminaRow != (UnityEngine.Object) null)
      {
        Transform parent = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.StaminaRow.transform.parent : this.ItemList.transform;
        if (dataOfClass.Stamina > 0)
          this.AddRewardList(this.StaminaRow, parent);
      }
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
      {
        Transform parent1 = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.ItemTemplate.transform.parent : this.ItemList.transform;
        Transform parent2 = (Transform) null;
        if ((UnityEngine.Object) this.EventCoinTemplate != (UnityEngine.Object) null)
          parent2 = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.EventCoinTemplate.transform.parent : this.ItemList.transform;
        for (int index = 0; index < dataOfClass.Items.Count; ++index)
        {
          ItemData data = dataOfClass.Items[index];
          DataSource.Bind<ItemData>(data.ItemType != EItemType.Unit || !((UnityEngine.Object) this.UnitTemplate != (UnityEngine.Object) null) ? (data.ItemType != EItemType.EventCoin || (UnityEngine.Object) parent2 == (UnityEngine.Object) null ? this.AddRewardList(this.ItemTemplate, parent1) : this.AddRewardList(this.EventCoinTemplate, parent2)) : this.AddRewardList(this.UnitTemplate, parent1), data, false);
        }
      }
      if ((UnityEngine.Object) this.ArtifactTemplate != (UnityEngine.Object) null)
      {
        Transform parent = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.ArtifactTemplate.transform.parent : this.ItemList.transform;
        foreach (ArtifactRewardData artifact in dataOfClass.Artifacts)
          DataSource.Bind<ArtifactRewardData>(this.AddRewardList(this.ArtifactTemplate, parent), artifact, false);
      }
      if ((UnityEngine.Object) this.ConceptCardTemplate != (UnityEngine.Object) null)
      {
        Transform parent = !((UnityEngine.Object) this.ItemList != (UnityEngine.Object) null) ? this.ConceptCardTemplate.transform.parent : this.ItemList.transform;
        foreach (KeyValuePair<string, GiftRecieveItemData> keyValuePair in dataOfClass.GiftRecieveItemDataDic)
        {
          if (keyValuePair.Value.type == GiftTypes.ConceptCard)
          {
            GameObject gameObject = this.AddRewardList(this.ConceptCardTemplate, parent);
            GiftRecieveItem componentInChildren = gameObject.GetComponentInChildren<GiftRecieveItem>();
            DataSource.Bind<GiftRecieveItemData>(gameObject, keyValuePair.Value, false);
            gameObject.SetActive(true);
            componentInChildren.UpdateValue();
          }
        }
      }
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
