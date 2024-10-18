// Decompiled with JetBrains decompiler
// Type: SRPG.BundleList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [AddComponentMenu("Payment/BundleList")]
  public class BundleList : MonoBehaviour, IFlowInterface
  {
    [Description("Gameobject used as list item")]
    public GameObject ItemTemplate;
    [Description("Gameobject used as detailed screen")]
    public GameObject DetailTemplate;
    [Description("Gameobject used as list item (VIP)")]
    public GameObject ItemVipTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      this.enabled = true;
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if ((UnityEngine.Object) this.DetailTemplate != (UnityEngine.Object) null && this.DetailTemplate.activeInHierarchy)
        this.DetailTemplate.SetActive(false);
      if (!((UnityEngine.Object) this.ItemVipTemplate != (UnityEngine.Object) null) || !this.ItemVipTemplate.activeInHierarchy)
        return;
      this.ItemVipTemplate.SetActive(false);
    }

    private void Start()
    {
      this.RefreshItems();
    }

    public void Refresh()
    {
      this.RefreshItems();
      if (!((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null))
        return;
      ListExtras component = this.ScrollRect.GetComponent<ListExtras>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.SetScrollPos(1f);
      else
        this.ScrollRect.normalizedPosition = Vector2.one;
    }

    private void RefreshItems()
    {
      Transform transform = this.transform;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!((UnityEngine.Object) child == (UnityEngine.Object) null) && child.gameObject.activeSelf)
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) child.gameObject);
      }
      PaymentManager.Bundle[] bundles = FlowNode_PaymentGetBundles.Bundles;
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || bundles == null)
        return;
      GameObject[] gameObjectArray = new GameObject[bundles.Length];
      for (int index = 0; index < bundles.Length; ++index)
      {
        gameObjectArray[index] = !bundles[index].productID.Contains("vip") ? UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate) : UnityEngine.Object.Instantiate<GameObject>(this.ItemVipTemplate);
        gameObjectArray[index].hideFlags = HideFlags.DontSave;
        DataSource.Bind<PaymentManager.Bundle>(gameObjectArray[index], bundles[index]);
        ListItemEvents component = gameObjectArray[index].GetComponent<ListItemEvents>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        }
        gameObjectArray[index].transform.SetParent(transform, false);
        gameObjectArray[index].gameObject.SetActive(true);
        gameObjectArray[index].transform.SetSiblingIndex(bundles[index].displayOrder);
      }
      for (int index = 0; index < gameObjectArray.Length; ++index)
      {
        if (gameObjectArray[index].transform.GetSiblingIndex() != bundles[index].displayOrder)
          gameObjectArray[index].transform.SetSiblingIndex(bundles[index].displayOrder);
      }
    }

    private void OnSelectItem(GameObject go)
    {
      PaymentManager.Bundle dataOfClass = DataSource.FindDataOfClass<PaymentManager.Bundle>(go, (PaymentManager.Bundle) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedProductID = dataOfClass.productID;
      GlobalVars.SelectedProductPrice = dataOfClass.price;
      GlobalVars.SelectedProductIcon = dataOfClass.iconImage;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!((UnityEngine.Object) this.mDetailInfo != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mDetailInfo.gameObject);
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      PaymentManager.Product dataOfClass = DataSource.FindDataOfClass<PaymentManager.Product>(go, (PaymentManager.Product) null);
      if (!((UnityEngine.Object) this.mDetailInfo == (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = UnityEngine.Object.Instantiate<GameObject>(this.DetailTemplate);
      DataSource.Bind<PaymentManager.Product>(this.mDetailInfo, dataOfClass);
      this.mDetailInfo.SetActive(true);
    }

    private void Update()
    {
    }
  }
}
