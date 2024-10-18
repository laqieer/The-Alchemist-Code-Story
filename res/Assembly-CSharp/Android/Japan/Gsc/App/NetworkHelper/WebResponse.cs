﻿// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.WebResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Network;
using rapidjson;
using SRPG;
using System.Text;

namespace Gsc.App.NetworkHelper
{
  public class WebResponse : ApiResponse<WebResponse>
  {
    public readonly byte[] payload;
    public readonly SRPG.Network.EErrCode ErrorCode;
    public readonly string ErrorMessage;
    public readonly long ServerTime;
    public readonly WWWResult Result;

    public WebResponse(WebInternalResponse response)
      : this(response.Payload)
    {
    }

    public WebResponse(byte[] payload)
    {
      try
      {
        this.payload = payload;
        using (Gsc.DOM.Json.Document document = Gsc.DOM.Json.Document.Parse(payload))
        {
          this.ErrorCode = (SRPG.Network.EErrCode) document.Root.GetValueByPointer("/stat", 1);
          this.ErrorMessage = document.Root.GetValueByPointer("/stat_msg", (string) null);
          this.ServerTime = document.Root.GetValueByPointer("/time", 0L);
        }
      }
      catch (DocumentParseError ex)
      {
        this.ErrorCode = SRPG.Network.EErrCode.Failed;
        this.ErrorMessage = LocalizedText.Get("embed.NETWORKERR");
      }
      if (payload == null)
        return;
      this.Result = new WWWResult(Encoding.UTF8.GetString(payload));
    }
  }
}
