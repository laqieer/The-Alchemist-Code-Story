// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ShopTimeOutWindow : MonoBehaviour
  {
    [SerializeField]
    private GameObject ItemParent;
    [SerializeField]
    private GameObject ItemTemplate;
    private List<GameObject> mItems = new List<GameObject>();

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      Transform parent = !Object.op_Inequality((Object) this.ItemParent, (Object) null) ? this.ItemTemplate.transform.parent : this.ItemParent.transform;
      if (GlobalVars.TimeOutShopItems != null && GlobalVars.TimeOutShopItems.Count > 0)
      {
        foreach (ShopItem timeOutShopItem in GlobalVars.TimeOutShopItems)
        {
          string empty = string.Empty;
          GameObject gameObject;
          string name;
          if (timeOutShopItem.IsArtifact)
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(timeOutShopItem.iname);
            if (artifactParam != null)
            {
              gameObject = this.InstantiateItem<ArtifactParam>(this.ItemTemplate, parent, artifactParam);
              name = artifactParam.name;
            }
            else
              continue;
          }
          else if (timeOutShopItem.IsConceptCard)
          {
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(timeOutShopItem.iname);
            if (cardDataForDisplay != null)
            {
              gameObject = this.InstantiateItem<ConceptCardData>(this.ItemTemplate, parent, cardDataForDisplay);
              ConceptCardIcon componentInChildren = gameObject.GetComponentInChildren<ConceptCardIcon>();
              if (Object.op_Inequality((Object) componentInChildren, (Object) null))
                componentInChildren.Setup(cardDataForDisplay);
              name = cardDataForDisplay.Param.name;
            }
            else
              continue;
          }
          else
          {
            ItemData itemData = new ItemData();
            if (itemData.Setup(0L, timeOutShopItem.iname, timeOutShopItem.num))
            {
              gameObject = this.InstantiateItem<ItemData>(this.ItemTemplate, parent, itemData);
              name = itemData.Param.name;
            }
            else
              continue;
          }
          ShopTimeOutItem component = gameObject.GetComponent<ShopTimeOutItem>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.SetShopItemInfo(timeOutShopItem, name);
        }
      }
      GameParameter.UpdateAll(((Component) parent).gameObject);
    }

    public GameObject InstantiateItem<BindType>(
      GameObject template,
      Transform parent,
      BindType item)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(template);
      gameObject.transform.SetParent(parent, false);
      DataSource.Bind<BindType>(gameObject, item);
      this.mItems.Add(gameObject);
      gameObject.SetActive(true);
      return gameObject;
    }
  }
}
