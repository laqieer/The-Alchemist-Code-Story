// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.PurchaseFlow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      get
      {
        if (PurchaseFlow._Handler != null)
          return PurchaseFlow._Handler.initialized;
        return false;
      }
    }

    public static bool unavailabled
    {
      get
      {
        if (PurchaseFlow._Handler != null)
          return PurchaseFlow._Handler.unavailabled;
        return false;
      }
    }

    public static ProductInfo[] ProductList
    {
      get
      {
        if (PurchaseFlow._Handler != null)
          return PurchaseFlow._Handler.ProductList;
        return (ProductInfo[]) null;
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

    public bool Purchase(string productId)
    {
      return PurchaseFlow._Handler.Purchase(productId);
    }

    public void InputBirthday(int year, int month, int date)
    {
      PurchaseFlow._Handler.InputBirthday(year, month, date);
    }

    public void Confirmed(bool isOK)
    {
      PurchaseFlow._Handler.Confirmed(isOK);
    }

    public static bool IsEnable(ProductInfo productInfo)
    {
      if (PurchaseFlow._Handler.HasCreditLimit)
        return (double) productInfo.Price <= (double) PurchaseFlow._Handler.CreditLimit;
      return true;
    }
  }
}
