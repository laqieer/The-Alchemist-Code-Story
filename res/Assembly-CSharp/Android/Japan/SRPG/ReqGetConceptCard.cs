﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGetConceptCard : WebAPI
  {
    public ReqGetConceptCard(long last_card_iid, Network.ResponseCallback response)
    {
      this.name = "unit/concept";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"last_iid\":");
      stringBuilder.Append(last_card_iid);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
