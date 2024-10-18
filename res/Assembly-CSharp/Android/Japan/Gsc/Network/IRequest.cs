// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace Gsc.Network
{
  public interface IRequest
  {
    CustomHeaders CustomHeaders { get; }

    bool isDone { get; }

    WebTaskResult GetResult();

    string GetRequestID();

    string GetHost();

    string GetUrl();

    string GetPath();

    string GetMethod();

    IWebTask Cast();

    IWebTask Send();

    void Retry();

    byte[] GetPayload();

    Type GetErrorResponseType();

    WebTaskResult InquireResult(WebTaskResult result, WebInternalResponse response);
  }
}
