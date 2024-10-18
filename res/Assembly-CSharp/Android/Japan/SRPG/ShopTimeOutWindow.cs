// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ShopTimeOutWindow : MonoBehaviour
  {
    private List<GameObject> mItems = new List<GameObject>();
    [SerializeField]
    private GameObject ItemParent;
    [SerializeField]
    private GameObject ItemTemplate;

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      Transform parent = !((UnityEngine.Object) this.ItemParent != (UnityEngine.Object) null) ? this.ItemTemplate.transform.parent : this.ItemParent.transform;
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
              if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
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
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.SetShopItemInfo(timeOutShopItem, name);
        }
      }
      GameParameter.UpdateAll(parent.gameObject);
    }

    public GameObject InstantiateItem<BindType>(GameObject template, Transform parent, BindType item)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
      gameObject.transform.SetParent(parent, false);
      DataSource.Bind<BindType>(gameObject, item, false);
      this.mItems.Add(gameObject);
      gameObject.SetActive(true);
      return gameObject;
    }
  }
}
