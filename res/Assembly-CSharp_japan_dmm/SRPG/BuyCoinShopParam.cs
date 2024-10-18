// Decompiled with JetBrains decompiler
// Type: SRPG.BuyCoinShopParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class BuyCoinShopParam
  {
    private string mShopId;
    private BuyCoinManager.BuyCoinShopType mShopType;
    private string mDisplayName;
    private long mBeginAt;
    private long mEndAt;
    private bool mAlwaysOpen;

    public string ShopId => this.mShopId;

    public BuyCoinManager.BuyCoinShopType ShopType => this.mShopType;

    public string DisplayName => this.mDisplayName;

    public long BeginAt => this.mBeginAt;

    public long EndAt => this.mEndAt;

    public bool AlwaysOpen => this.mAlwaysOpen;

    public bool Deserialize(JSON_BuyCoinShopParam json)
    {
      if (json == null)
        return false;
      DateTime result;
      if (json.begin_at != null)
      {
        DateTime.TryParse(json.begin_at, out result);
        this.mBeginAt = TimeManager.GetUnixSec(result);
      }
      else
        this.mBeginAt = 0L;
      if (json.end_at != null)
      {
        DateTime.TryParse(json.end_at, out result);
        this.mEndAt = TimeManager.GetUnixSec(result);
      }
      else
        this.mEndAt = 0L;
      this.mAlwaysOpen = false;
      if (this.mBeginAt == 0L && this.mEndAt == 0L)
        this.mAlwaysOpen = true;
      this.mShopId = json.shop_id;
      this.mShopType = (BuyCoinManager.BuyCoinShopType) json.shop_type;
      this.mDisplayName = json.display_name;
      return true;
    }
  }
}
