// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace Gsc.Network
{
  public interface IRequest
  {
    CustomHeaders CustomHeaders { get; }

    bool IsUseEncryption { get; }

    byte[] UnencryptedPayload { get; set; }

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
