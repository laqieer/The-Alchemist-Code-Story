// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.PurchaseManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Auth;
using Gsc.Network.Encoding;
using SRPG;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Purchase
{
  public class PurchaseManager : MonoSingleton<PurchaseManager>
  {
    private PurchaseFlow saveFlow;
    private EncodingTypes.ESerializeCompressMethod __serializeCompressMethod;

    public PurchaseManager.OnProductsSchedule onProductsSchedule { get; set; }

    private List<string> GetProductList(List<ProductParam> ProductMasters)
    {
      List<string> productList = new List<string>();
      foreach (ProductParam productMaster in ProductMasters)
      {
        if (!(productMaster.Platform != Device.Platform))
          productList.Add(productMaster.ProductId);
      }
      if (productList.Count == 0)
      {
        foreach (ProductParam productMaster in ProductMasters)
        {
          if (!(productMaster.Platform != "windows"))
            productList.Add(productMaster.ProductId);
        }
      }
      return productList;
    }

    public void Init(List<ProductParam> ProductMasters)
    {
      PurchaseFlow.Init(this.GetProductList(ProductMasters).ToArray(), (IPurchaseGlobalListener) new PurchaseManager.PurchaseGlobalListener());
    }

    public void InputBirthday(int year, int month, int day)
    {
      this.onProductsSchedule = PurchaseManager.OnProductsSchedule.REQUEST_API;
      PurchaseFlow.Instance.InputBirthday(year, month, day);
    }

    public void ResponseBirthday(PaymentManager.ERegisterBirthdayResult result)
    {
      MonoSingleton<PaymentManager>.Instance.OnAgeResponse(result);
    }

    public void Purchase(string productId) => PurchaseFlow.Instance.Purchase(productId);

    public void UpdateProduct(List<ProductParam> ProductMasters)
    {
      PurchaseFlow.UpdateProducts(this.GetProductList(ProductMasters).ToArray());
    }

    public void Resume() => PurchaseFlow.Resume();

    private EncodingTypes.ESerializeCompressMethod SerializeCompressMethod
    {
      get
      {
        return GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? GlobalVars.SelectedSerializeCompressMethod : this.__serializeCompressMethod;
      }
      set => this.__serializeCompressMethod = value;
    }

    public void Confirm(PurchaseFlow flow, ProductInfo product)
    {
      UIUtility.ConfirmBox(LocalizedText.ReplaceTag(LocalizedText.Get("sys.MSG_PURCHASE_CONFIRM", (object) product.LocalizedTitle, (object) product.LocalizedPrice)), (UIUtility.DialogResultEvent) (obj =>
      {
        this.saveFlow = flow;
        this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
        SRPG.Network.RequestAPI((WebAPI) new ReqProductChargePrepare(GlobalVars.SelectedProductIname, new SRPG.Network.ResponseCallback(this.ProductChargePrepareCallback), this.SerializeCompressMethod));
      }), (UIUtility.DialogResultEvent) (obj => flow.Confirmed(false)), systemModal: true);
    }

    public void OnProducts(PurchaseFlow flow, ProductInfo[] products)
    {
      if (this.onProductsSchedule == PurchaseManager.OnProductsSchedule.REQUEST_API)
        this.ResponseBirthday(PaymentManager.ERegisterBirthdayResult.SUCCESS);
      this.onProductsSchedule = PurchaseManager.OnProductsSchedule.NONE;
      MonoSingleton<PaymentManager>.Instance.OnUpdateProductDetails(PurchaseFlow.ProductList);
    }

    private void ProductChargePrepareCallback(WWWResult www)
    {
      string errMsg = SRPG.Network.ErrMsg;
      if (!EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod))
      {
        WebAPI.JSON_BaseResponse jsonBaseResponse = SerializerCompressorHelper.Decode<WebAPI.JSON_BaseResponse>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
        DebugUtility.Assert(jsonBaseResponse != null, "mp_res == null");
        SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) jsonBaseResponse.stat;
        string statMsg = jsonBaseResponse.stat_msg;
        if (stat != SRPG.Network.EErrCode.Success)
          SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
      }
      if (SRPG.Network.IsError)
      {
        this.saveFlow.Confirmed(false);
        UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) null, systemModal: true);
        FlowNode_SendLogMessage.PurchaseFlow(string.Empty, "charge/prepare", "NetworkError:" + SRPG.Network.ErrCode.ToString());
      }
      else
      {
        this.saveFlow.Confirmed(true);
        MonoSingleton<PaymentManager>.Instance.OnPurchaseProcessing();
        FlowNode_SendLogMessage.PurchaseFlow(string.Empty, "charge/prepare", "Success");
      }
      this.saveFlow = (PurchaseFlow) null;
      SRPG.Network.RemoveAPI();
      SRPG.Network.ResetError();
    }

    public enum OnProductsSchedule
    {
      NONE,
      REQUEST_API,
    }

    public class PurchaseGlobalListener : IPurchaseGlobalListener, IPurchaseResultListener
    {
      public void OnInitialized()
      {
        MonoSingleton<PaymentManager>.Instance.OnUpdateProductDetails(PurchaseFlow.ProductList);
      }

      private void OnPurchaseSucceeded(FulfillmentResult.OrderInfo order)
      {
        MonoSingleton<PaymentManager>.Instance.OnPurchaseSucceeded(order);
      }

      public void OnPurchaseSucceeded(FulfillmentResult result)
      {
        foreach (FulfillmentResult.OrderInfo succeededTransaction in result.SucceededTransactions)
          this.OnPurchaseSucceeded(succeededTransaction);
        foreach (FulfillmentResult.OrderInfo duplicatedTransaction in result.DuplicatedTransactions)
          this.OnPurchaseSucceeded(duplicatedTransaction);
        MonoSingleton<GameManager>.Instance.Player.SetCoinPurchaseResult(result);
        GlobalEvent.Invoke("REFRESH_COIN_STATUS", (object) this);
        GlobalVars.AfterCoin = MonoSingleton<GameManager>.Instance.Player.Coin;
        PaymentManager.Product product = MonoSingleton<PaymentManager>.Instance.GetProduct(GlobalVars.SelectedProductID);
        if (product == null)
          return;
        GlobalVars.BeforeCoin = MonoSingleton<GameManager>.Instance.Player.Coin - (product.numFree + product.numPaid);
      }

      public void OnPurchaseFailed()
      {
      }

      public void OnPurchaseCanceled()
      {
      }

      public void OnPurchaseAlreadyOwned()
      {
      }

      public void OnPurchaseDeferred()
      {
      }

      public void OnOverCreditLimited()
      {
      }

      public void OnInsufficientBalances()
      {
      }

      public void OnFinished(bool isSuccess)
      {
      }
    }
  }
}
