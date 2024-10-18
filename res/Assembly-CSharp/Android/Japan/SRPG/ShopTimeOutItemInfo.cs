// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class ShopTimeOutItemInfo
  {
    public string ShopId;
    public int ItemId;
    public long End;

    public ShopTimeOutItemInfo(string shopId, int itemId, long end)
    {
      this.ShopId = shopId;
      this.ItemId = itemId;
      this.End = end;
    }
  }
}
