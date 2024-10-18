// Decompiled with JetBrains decompiler
// Type: PaymentManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using Gsc.Purchase;
using SRPG;
using System.Collections.Generic;
using System.Diagnostics;

public class PaymentManager : MonoSingleton<PaymentManager>
{
  private Dictionary<string, ProductInfo> ProductOnStores = new Dictionary<string, ProductInfo>();
  private PaymentManager.ERequestPurchaseResult purchaseResult = PaymentManager.ERequestPurchaseResult.NONE;
  private bool isSetupOK;
  public PaymentManager.ShowItemsDelegate OnShowItems;
  public PaymentManager.RequestPurchaseDelegate OnRequestPurchase;
  public PaymentManager.RegisterBirthdayDelegate OnRegisterBirthday;
  public PaymentManager.RequestSucceededDelegate OnRequestSucceeded;
  public PaymentManager.RequestProcessingDelegate OnRequestProcessing;

  public bool IsAvailable
  {
    get
    {
      return this.isSetupOK;
    }
  }

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
    if (productId == null)
      return (PaymentManager.Product) null;
    return PaymentManager.Product.Create(productId);
  }

  public List<PaymentManager.Product> GetProducts()
  {
    List<PaymentManager.Product> productList = new List<PaymentManager.Product>();
    if (this.ProductMasters == null)
      return productList;
    foreach (ProductParam productMaster in this.ProductMasters)
    {
      PaymentManager.Product product = PaymentManager.Product.Create(productMaster.ProductId);
      if (product != null)
        productList.Add(product);
    }
    return productList;
  }

  private string AgreedVer
  {
    get
    {
      return (string) MonoSingleton<UserInfoManager>.Instance.GetValue("payment_agreed_ver");
    }
    set
    {
      MonoSingleton<UserInfoManager>.Instance.SetValue("payment_agreed_ver", (object) value, true);
    }
  }

  protected override void Initialize()
  {
    PaymentManager.MyDebug.PushMessage("PaymentManager.Initialize");
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
  }

  protected override void Release()
  {
    PaymentManager.MyDebug.PushMessage("PaymentManager.Release");
  }

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
    if (1900 > year || 2100 < year || (0 > month || 12 < month) || (0 > day || 31 < day))
      return false;
    MonoSingleton<PurchaseManager>.Instance.InputBirthday(year, month, day);
    return true;
  }

  public void OnAgree()
  {
    this.AgreedVer = SRPG.Network.Version;
  }

  public bool IsAgree()
  {
    if (this.AgreedVer != null)
      return this.AgreedVer.Length > 0;
    return false;
  }

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
      MyMetaps.TrackPurchase(productInfo.ID, productInfo.CurrencyCode, (double) productInfo.Price);
    if ((UnityEngine.Object) MonoSingleton<GameManager>.Instance != (UnityEngine.Object) null && MonoSingleton<GameManager>.Instance.Player != null)
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
    this.OnRequestPurchase(this.purchaseResult, (PaymentManager.CoinRecord) null);
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

    public string productID { get; private set; }

    public string name
    {
      get
      {
        return this.master.Name;
      }
    }

    public string desc
    {
      get
      {
        return this.master.Description;
      }
    }

    public string price
    {
      get
      {
        return this.store.LocalizedPrice;
      }
    }

    public double sellPrice
    {
      get
      {
        return (double) this.store.Price;
      }
    }

    public int numPaid
    {
      get
      {
        return this.master.AdditionalPaidCoin;
      }
    }

    public int numFree
    {
      get
      {
        return this.master.AdditionalFreeCoin;
      }
    }

    public static PaymentManager.Product Create(string productId_)
    {
      PaymentManager.Product product = new PaymentManager.Product();
      if (!product.Reflesh(productId_))
        return (PaymentManager.Product) null;
      return product;
    }

    public bool Reflesh(string productId_ = null)
    {
      if (productId_ != null)
        this.productID = productId_;
      PaymentManager instance = MonoSingleton<PaymentManager>.Instance;
      bool flag = instance.ProductOnStores.TryGetValue(this.productID, out this.store);
      this.master = instance.GetProductMaster(this.productID);
      PaymentManager.MyDebug.PushMessage("Product.Reflesh " + this.ToString());
      if (flag)
        return null != this.master;
      return false;
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

    public CoinRecord(string[] productIds_, int currentPaidCoin_, int currentFreeCoin_, int additionalPaidCoin_, int additionalFreeCoin_)
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

  public delegate void ShowItemsDelegate(PaymentManager.EShowItemsResult result, PaymentManager.Product[] products);

  public enum ERequestPurchaseResult
  {
    NONE = -1,
    SUCCESS = 0,
    CANCEL = 1,
    ALREADY_OWN = 2,
    DEFERRED = 3,
    INSUFFICIENT_BALANCES = 4,
    OVER_LIMITED = 5,
    NEED_BIRTHDAY = 6,
    ERROR = 7,
  }

  public delegate void RequestPurchaseDelegate(PaymentManager.ERequestPurchaseResult result, PaymentManager.CoinRecord record = null);

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
