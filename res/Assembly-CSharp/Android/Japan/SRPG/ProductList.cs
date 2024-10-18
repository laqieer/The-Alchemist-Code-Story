// Decompiled with JetBrains decompiler
// Type: SRPG.ProductList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("Payment/ProductList")]
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "選択された", FlowNode.PinTypes.Output, 0)]
  public class ProductList : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    [Description("リストアイテム(VIP)として使用するゲームオブジェクト")]
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
      this.RefreshItems(true);
    }

    public void Refresh()
    {
      this.RefreshItems(false);
      if (!((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null))
        return;
      ListExtras component = this.ScrollRect.GetComponent<ListExtras>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.SetScrollPos(1f);
      else
        this.ScrollRect.normalizedPosition = Vector2.one;
    }

    private void RefreshItems(bool is_start)
    {
      Transform transform = this.transform;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!((UnityEngine.Object) child == (UnityEngine.Object) null) && child.gameObject.activeSelf)
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) child.gameObject);
      }
      PaymentManager.Product[] products = FlowNode_PaymentGetProducts.Products;
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || products == null)
        return;
      for (int index = 0; index < products.Length; ++index)
      {
        if (!(products[index].productID == (string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.PremiumProduct) && !false)
        {
          GameObject gameObject = !(products[index].productID == (string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.VipCardProduct) && !false ? UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate) : UnityEngine.Object.Instantiate<GameObject>(this.ItemVipTemplate);
          gameObject.hideFlags = HideFlags.DontSave;
          DataSource.Bind<PaymentManager.Product>(gameObject, products[index], false);
          if (!is_start)
          {
            ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
              component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
              component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            }
          }
          gameObject.transform.SetParent(transform, false);
          gameObject.gameObject.SetActive(true);
        }
      }
    }

    private void OnSelectItem(GameObject go)
    {
      PaymentManager.Product dataOfClass = DataSource.FindDataOfClass<PaymentManager.Product>(go, (PaymentManager.Product) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedProductID = dataOfClass.productID;
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
      DataSource.Bind<PaymentManager.Product>(this.mDetailInfo, dataOfClass, false);
      this.mDetailInfo.SetActive(true);
    }

    private void Update()
    {
    }
  }
}
