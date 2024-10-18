// Decompiled with JetBrains decompiler
// Type: MyMetaps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System.Text;
using UnityEngine;

#nullable disable
public static class MyMetaps
{
  private static MetapsAnalyticsScript mMetaps;
  private static readonly Encoding encoding = Encoding.GetEncoding("utf-8");

  private static bool IsEnable => !Object.op_Equality((Object) MyMetaps.mMetaps, (Object) null);

  public static void Setup()
  {
    if (!Object.op_Equality((Object) MyMetaps.mMetaps, (Object) null))
      return;
    MetapsAnalyticsScript metapsAnalyticsScript = Resources.Load<MetapsAnalyticsScript>("metaps");
    if (!Object.op_Inequality((Object) metapsAnalyticsScript, (Object) null))
      return;
    MyMetaps.mMetaps = Object.Instantiate<MetapsAnalyticsScript>(metapsAnalyticsScript);
  }

  public static void Destroy()
  {
    if (!Object.op_Inequality((Object) MyMetaps.mMetaps, (Object) null))
      return;
    GameUtility.DestroyGameObject((Component) MyMetaps.mMetaps);
    MyMetaps.mMetaps = (MetapsAnalyticsScript) null;
  }

  public static bool TrackEvent(string category, string name)
  {
    FlowNode_SendLogMessage.TrackEvent(category, name);
    if (!MyMetaps.IsEnable)
      return false;
    MetapsAnalyticsScript.SetLogEnabled(true);
    if (!MyMetaps.Validate(category) || !MyMetaps.Validate(name))
      return false;
    MetapsAnalyticsScript.TrackEvent(category, name);
    return true;
  }

  public static bool TrackEvent(string category, string name, int value)
  {
    FlowNode_SendLogMessage.TrackEvent(category, name, value);
    if (!MyMetaps.IsEnable)
      return false;
    MetapsAnalyticsScript.SetLogEnabled(true);
    if (!MyMetaps.Validate(category) || !MyMetaps.Validate(name))
      return false;
    MetapsAnalyticsScript.TrackEvent(category, name, value);
    return true;
  }

  public static bool TrackPurchase(string productId, string currency, double price = 0.0)
  {
    FlowNode_SendLogMessage.TrackPurchase(productId, currency, price);
    if (!MyMetaps.IsEnable)
      return false;
    MetapsAnalyticsScript.SetLogEnabled(true);
    if (!MyMetaps.Validate(productId) || !MyMetaps.Validate(currency) || price <= 0.0)
      return false;
    MetapsAnalyticsScript.TrackPurchase(productId, price, currency);
    return true;
  }

  public static bool TrackSpend(string category, string name, int value)
  {
    FlowNode_SendLogMessage.TrackSpend(category, name, value);
    if (!MyMetaps.IsEnable)
      return false;
    MetapsAnalyticsScript.SetLogEnabled(true);
    if (!MyMetaps.Validate(category) || !MyMetaps.Validate(name))
      return false;
    MetapsAnalyticsScript.TrackSpend(category, name, value);
    return true;
  }

  public static bool Validate(string data)
  {
    return !string.IsNullOrEmpty(data) && MyMetaps.encoding.GetByteCount(data) == data.Length && data.Length <= 64;
  }

  public static bool TrackTutorialBegin() => MyMetaps.TrackEvent("tutorial", "start");

  public static bool TrackTutorialPoint(string point) => MyMetaps.TrackEvent("tutorial", point);

  public static bool TrackTutorialEnd() => MyMetaps.TrackEvent("tutorial", "end");

  public static bool TrackDebugInstall() => MyMetaps.TrackEvent("debug_install", "installed");

  public static bool TrackSpendCoin(string name, int value)
  {
    return MyMetaps.TrackSpend(ESaleType.Coin.ToString(), name, value);
  }

  public static bool TrackSpendShop(ESaleType sale_type, EShopType shop_type, int value)
  {
    string name = "ShopBuy." + shop_type.ToString();
    return MyMetaps.TrackSpend(sale_type.ToString(), name, value);
  }

  public static bool TrackSpendShopUpdate(ESaleType sale_type, EShopType shop_type, int value)
  {
    string name = "ShopUpdate." + shop_type.ToString();
    return MyMetaps.TrackSpend(sale_type.ToString(), name, value);
  }
}
