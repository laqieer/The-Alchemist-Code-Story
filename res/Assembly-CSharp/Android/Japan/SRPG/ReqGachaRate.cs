﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGachaRate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqGachaRate : WebAPI
  {
    public ReqGachaRate(string gachaid, Network.ResponseCallback response)
    {
      this.name = "gacha/slot_info";
      this.body = WebAPI.GetRequestString("\"gachaid\":\"" + gachaid + "\"");
      this.callback = response;
    }
  }
}