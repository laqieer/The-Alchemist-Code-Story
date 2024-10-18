// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
