// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.WebResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network;
using Gsc.Network.Encoding;
using rapidjson;
using SRPG;

#nullable disable
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
      : this(response.Payload, response.ContentType, response.ContentEncoding)
    {
    }

    public WebResponse(byte[] payload, Gsc.Network.ContentType contentType, ContentEncoding contentEncoding)
    {
      if (contentType == Gsc.Network.ContentType.ApplicationOctetStream_MessagePack || contentType == Gsc.Network.ContentType.ApplicationOctetStream_MessagePack_AES)
      {
        if (payload != null)
        {
          WebAPI.JSON_BaseResponse jsonBaseResponse = SerializerCompressorHelper.Decode<WebAPI.JSON_BaseResponse>(payload, true, contentEncoding != ContentEncoding.Lz4 ? CompressMode.None : CompressMode.Lz4, printExceptions: false);
          this.ErrorCode = (SRPG.Network.EErrCode) jsonBaseResponse.stat;
          this.ErrorMessage = jsonBaseResponse.stat_msg;
          this.ServerTime = jsonBaseResponse.time;
        }
        this.Result = new WWWResult(payload);
      }
      else
      {
        string empty = string.Empty;
        try
        {
          this.payload = payload;
          this.payload = payload = SerializerCompressorHelper.Decode<byte[]>(payload, decompressMode: CompressMode.None);
          empty = System.Text.Encoding.UTF8.GetString(payload);
          using (Gsc.DOM.Json.Document document = Gsc.DOM.Json.Document.Parse(empty))
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
        if (payload != null)
          this.Result = new WWWResult(empty);
      }
    }
  }
}
