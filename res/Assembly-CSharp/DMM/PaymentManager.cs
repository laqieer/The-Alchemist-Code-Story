// Decompiled with JetBrains decompiler
// Type: PaymentManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Purchase;
using SRPG;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
public class PaymentManager : MonoSingleton<PaymentManager>
{
  private bool isSetupOK;
  private Dictionary<string, ProductInfo> ProductOnStores = new Dictionary<string, ProductInfo>();
  public PaymentManager.ShowItemsDelegate OnShowItems;
  private PaymentManager.ERequestPurchaseResult purchaseResult = PaymentManager.ERequestPurchaseResult.NONE;
  public PaymentManager.RequestPurchaseDelegate OnRequestPurchase;
  public PaymentManager.RegisterBirthdayDelegate OnRegisterBirthday;
  public PaymentManager.RequestSucceededDelegate OnRequestSucceeded;
  public PaymentManager.RequestProcessingDelegate OnRequestProcessing;

  public bool IsAvailable => this.isSetupOK;

  private List<ProductParam> ProductMasters { get; set; }

  private ProductParam GetProductMaster(string productId)
  {
    foreach (ProductParam productMaster in this.ProductMasters)
    {
      if (productId == productMaster.ProductId)
        return productMaster;
    }
    return (ProductParam) null;
  }

  public PaymentManager.Product GetProduct(string productId)
  {
    return productId == null ? (PaymentManager.Product) null : PaymentManager.Product.Create(productId);
  }

  public List<PaymentManager.Product> GetProducts()
  {
    List<PaymentManager.Product> products = new List<PaymentManager.Product>();
    if (this.ProductMasters == null)
      return products;
    foreach (ProductParam productMaster in this.ProductMasters)
    {
      PaymentManager.Product product = PaymentManager.Product.Create(productMaster.ProductId);
      if (product != null)
        products.Add(product);
    }
    return products;
  }

  private string AgreedVer
  {
    get => (string) MonoSingleton<UserInfoManager>.Instance.GetValue("payment_agreed_ver");
    set => MonoSingleton<UserInfoManager>.Instance.SetValue("payment_agreed_ver", (object) value);
  }

  protected override void Initialize()
  {
    PaymentManager.MyDebug.PushMessage("PaymentManager.Initialize");
    Object.DontDestroyOnLoad((Object) this);
  }

  protected override void Release() => PaymentManager.MyDebug.PushMessage("PaymentManager.Release");

  public bool InitOnlyProductMaster(bool isResetMaster = false, ProductParamResponse res = null)
  {
    if (isResetMaster)
    {
      if (res != null)
        this.ProductMasters = res.products;
      else
        this.ProductMasters.Clear();
    }
    return true;
  }

  public bool Init(bool isResetMaster = false, ProductParamResponse res = null)
  {
    PaymentManager.MyDebug.PushMessage("PaymentManager.Init");
    this.InitOnlyProductMaster(isResetMaster, res);
    MonoSingleton<PurchaseManager>.Instance.Init(this.ProductMasters);
    DebugUtility.LogWarning("PaymentManager:isSetupOK=>" + this.isSetupOK.ToString());
    return true;
  }

  public bool ShowItems()
  {
    this.OnShowItems(PaymentManager.EShowItemsResult.SUCCESS, this.GetProducts().ToArray());
    return true;
  }

  public bool RequestPurchase(string productId)
  {
    PaymentManager.MyDebug.PushMessage(nameof (RequestPurchase));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.NONE;
    MonoSingleton<PurchaseManager>.Instance.Purchase(productId);
    return true;
  }

  public bool RegisterBirthday(int year, int month, int day)
  {
    PaymentManager.MyDebug.PushMessage(nameof (RegisterBirthday));
    if (1900 > year || 2100 < year || 0 > month || 12 < month || 0 > day || 31 < day)
      return false;
    MonoSingleton<PurchaseManager>.Instance.InputBirthday(year, month, day);
    return true;
  }

  public void OnAgree() => this.AgreedVer = Network.Version;

  public bool IsAgree() => this.AgreedVer != null && this.AgreedVer.Length > 0;

  public void OnAgeResponse(PaymentManager.ERegisterBirthdayResult result)
  {
    PaymentManager.MyDebug.PushMessage("OnAgeResponsePurchase");
    if (this.OnRegisterBirthday == null)
      return;
    this.OnRegisterBirthday(result);
  }

  public void OnUpdateProductDetails(ProductInfo[] products)
  {
    PaymentManager.MyDebug.PushMessage("OnUpdateProductDetailsPurchase");
    if (products == null)
      return;
    this.ProductOnStores.Clear();
    foreach (ProductInfo product in products)
      this.ProductOnStores.Add(product.ID, product);
    this.isSetupOK = true;
  }

  public void OnPurchaseSucceeded(FulfillmentResult.OrderInfo order)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseSucceeded));
    ProductInfo productInfo;
    if (this.ProductOnStores.TryGetValue(order.ProductId, out productInfo))
    {
      MyMetaps.TrackPurchase(productInfo.ID, productInfo.CurrencyCode, (double) productInfo.Price);
      AdjustWrapper.TrackRevenue("q8j451", productInfo.CurrencyCode, (double) productInfo.Price, order.TransactionId);
    }
    if (Object.op_Inequality((Object) MonoSingleton<GameManager>.Instance, (Object) null) && MonoSingleton<GameManager>.Instance.Player != null)
      MonoSingleton<GameManager>.Instance.Player.SetOrderResult(order);
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.SUCCESS;
    if (this.OnRequestSucceeded == null)
      return;
    this.OnRequestSucceeded();
  }

  public void OnPurchaseFailed()
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseFailed));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.ERROR;
  }

  public void OnInsufficientBalances()
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnInsufficientBalances));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.INSUFFICIENT_BALANCES;
  }

  public void OnPurchaseCanceled(string message)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseCanceled));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.CANCEL;
  }

  public void OnPurchaseAlreadyOwned(string message)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseAlreadyOwned));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.ALREADY_OWN;
  }

  public void OnPurchaseDeferred()
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseDeferred));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.DEFERRED;
  }

  public void OnOverCreditLimited()
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnOverCreditLimited));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.OVER_LIMITED;
  }

  public void OnPurchaseFinished(bool isSuccess)
  {
    PaymentManager.MyDebug.PushMessage("OnPurchaseFinished " + (object) isSuccess);
    if (this.OnRequestPurchase == null)
      return;
    this.OnRequestPurchase(this.purchaseResult);
  }

  public void OnPurchaseProcessing()
  {
    if (this.OnRequestProcessing == null)
      return;
    this.OnRequestProcessing();
  }

  public void OnNeedBirthday()
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnNeedBirthday));
    if (this.OnRequestProcessing != null)
    {
      this.purchaseResult = PaymentManager.ERequestPurchaseResult.NEED_BIRTHDAY;
      this.OnRequestProcessing();
    }
    else
    {
      if (this.OnRegisterBirthday == null)
        return;
      this.OnRegisterBirthday(PaymentManager.ERegisterBirthdayResult.ERROR);
    }
  }

  public enum ECheckChargeLimitResult
  {
    SUCCESS,
    NONAGE,
    NEED_CHECK,
    ERROR,
    NEED_BIRTHDAY,
    LIMIT_OVER,
  }

  public class Product
  {
    private ProductInfo store;
    private ProductParam master;

    private Product()
    {
    }

    public string ID => this.master.Id;

    public string productID { get; private set; }

    public string name => this.master.Name;

    public string desc => this.master.Description;

    public string price => this.store.LocalizedPrice;

    public double sellPrice => (double) this.store.Price;

    public int numPaid => this.master.AdditionalPaidCoin;

    public int numFree => this.master.AdditionalFreeCoin;

    public int remainNum => this.master.RemainNum;

    public static PaymentManager.Product Create(string productId_)
    {
      PaymentManager.Product product = new PaymentManager.Product();
      return !product.Reflesh(productId_) ? (PaymentManager.Product) null : product;
    }

    public bool Reflesh(string productId_ = null)
    {
      if (productId_ != null)
        this.productID = productId_;
      PaymentManager instance = MonoSingleton<PaymentManager>.Instance;
      bool flag = instance.ProductOnStores.TryGetValue(this.productID, out this.store);
      this.master = instance.GetProductMaster(this.productID);
      PaymentManager.MyDebug.PushMessage("Product.Reflesh " + this.ToString());
      return flag && null != this.master;
    }

    public override string ToString()
    {
      string str = "Product: id=" + this.productID;
      if (this.store != null)
        str = str + " store_id=" + this.store.ID;
      if (this.master != null)
        str = str + " master_name=" + this.master.Name;
      return str;
    }
  }

  public class CoinRecord
  {
    public string[] productIds;
    public int currentPaidCoin;
    public int currentFreeCoin;
    public int additionalPaidCoin;
    public int additionalFreeCoin;

    public CoinRecord(
      string[] productIds_,
      int currentPaidCoin_,
      int currentFreeCoin_,
      int additionalPaidCoin_,
      int additionalFreeCoin_)
    {
      this.productIds = productIds_;
      this.currentPaidCoin = currentPaidCoin_;
      this.currentFreeCoin = currentFreeCoin_;
      this.additionalPaidCoin = additionalPaidCoin_;
      this.additionalFreeCoin = additionalFreeCoin_;
    }
  }

  public enum EShowItemsResult
  {
    SUCCESS,
    ERROR,
  }

  public delegate void ShowItemsDelegate(
    PaymentManager.EShowItemsResult result,
    PaymentManager.Product[] products);

  public enum ERequestPurchaseResult
  {
    NONE = -1, // 0xFFFFFFFF
    SUCCESS = 0,
    CANCEL = 1,
    ALREADY_OWN = 2,
    DEFERRED = 3,
    INSUFFICIENT_BALANCES = 4,
    OVER_LIMITED = 5,
    NEED_BIRTHDAY = 6,
    ERROR = 7,
  }

  public delegate void RequestPurchaseDelegate(
    PaymentManager.ERequestPurchaseResult result,
    PaymentManager.CoinRecord record = null);

  public enum ERegisterBirthdayResult
  {
    SUCCESS,
    ERROR,
  }

  public delegate void RegisterBirthdayDelegate(PaymentManager.ERegisterBirthdayResult result);

  private class MyDebug
  {
    private static PaymentManager.MyDebug self = new PaymentManager.MyDebug();
    private int max = 10;
    private List<string> contents = new List<string>();

    public static void PushMessage(string msg)
    {
      PaymentManager.MyDebug.self.contents.Add(msg);
      if (PaymentManager.MyDebug.self.contents.Count <= PaymentManager.MyDebug.self.max)
        return;
      PaymentManager.MyDebug.self.contents.RemoveAt(0);
    }

    [DebuggerHidden]
    public static IEnumerable<string> EachMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PaymentManager.MyDebug.\u003CEachMessage\u003Ec__Iterator0 messageCIterator0_1 = new PaymentManager.MyDebug.\u003CEachMessage\u003Ec__Iterator0();
      // ISSUE: variable of a compiler-generated type
      PaymentManager.MyDebug.\u003CEachMessage\u003Ec__Iterator0 messageCIterator0_2 = messageCIterator0_1;
      // ISSUE: reference to a compiler-generated field
      messageCIterator0_2.\u0024PC = -2;
      return (IEnumerable<string>) messageCIterator0_2;
    }
  }

  public delegate void RequestSucceededDelegate();

  public delegate void RequestProcessingDelegate();
}
