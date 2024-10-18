// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.PurchaseFlow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace Gsc.Purchase
{
  public class PurchaseFlow
  {
    public static PurchaseFlow Instance = new PurchaseFlow();
    private static PurchaseHandler _Handler = PurchaseHandler.Instance;

    private PurchaseFlow()
    {
    }

    public static bool initialized
    {
      get => PurchaseFlow._Handler != null && PurchaseFlow._Handler.initialized;
    }

    public static bool unavailabled
    {
      get => PurchaseFlow._Handler != null && PurchaseFlow._Handler.unavailabled;
    }

    public static ProductInfo[] ProductList
    {
      get
      {
        return PurchaseFlow._Handler != null ? PurchaseFlow._Handler.ProductList : (ProductInfo[]) null;
      }
    }

    public static IPurchaseGlobalListener Listener { get; private set; }

    public static void Init(string[] productIds, IPurchaseGlobalListener listener)
    {
      if (PurchaseFlow.initialized)
        return;
      PurchaseFlow.Listener = listener;
      PurchaseFlow._Handler.Init(productIds);
    }

    public static void UpdateProducts(string[] productIds)
    {
      if (!PurchaseFlow.initialized)
        return;
      PurchaseFlow._Handler.UpdateProducts(productIds);
    }

    public static void Resume()
    {
      if (!PurchaseFlow.initialized)
        return;
      PurchaseFlow._Handler.Resume();
    }

    public static void LaunchFlow<T>(T listener) where T : MonoBehaviour, IPurchaseFlowListener
    {
      PurchaseFlow.LaunchFlow<T>(listener, false);
    }

    public static void LaunchFlow<T>(T listener, bool enableInactiveCallback) where T : MonoBehaviour, IPurchaseFlowListener
    {
      if (!PurchaseFlow.initialized)
        return;
      PurchaseFlow._Handler.Launch((IPurchaseFlowListener) listener, enableInactiveCallback);
    }

    public bool Purchase(string productId) => PurchaseFlow._Handler.Purchase(productId);

    public void InputBirthday(int year, int month, int date)
    {
      PurchaseFlow._Handler.InputBirthday(year, month, date);
    }

    public void Confirmed(bool isOK) => PurchaseFlow._Handler.Confirmed(isOK);

    public static bool IsEnable(ProductInfo productInfo)
    {
      return !PurchaseFlow._Handler.HasCreditLimit || (double) productInfo.Price <= (double) PurchaseFlow._Handler.CreditLimit;
    }
  }
}
