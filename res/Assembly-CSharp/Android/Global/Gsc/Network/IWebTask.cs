// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IWebTask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Tasks;
using System;
using System.Collections;

namespace Gsc.Network
{
  public interface IWebTask : IEnumerator, IWebTaskBase, ITask
  {
    bool handled { get; }

    WebTaskResult Result { get; }

    byte[] error { get; }

    void Retry();

    bool IsAcceptResult(WebTaskResult result);

    bool HasAttributes(WebTaskAttribute attributes);

    WebInternalTask GetInternalTask();

    Type GetRequestType();
  }
}
