﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemEventShopUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqItemEventShopUpdate : WebAPI
  {
    public ReqItemEventShopUpdate(string iname, string costiname, Network.ResponseCallback response)
    {
      this.name = "shop/update";
      this.body = "\"iname\":\"" + iname + "\",";
      ReqItemEventShopUpdate itemEventShopUpdate = this;
      itemEventShopUpdate.body = itemEventShopUpdate.body + "\"costiname\":\"" + costiname + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}