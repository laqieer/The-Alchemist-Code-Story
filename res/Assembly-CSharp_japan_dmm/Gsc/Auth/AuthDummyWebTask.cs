// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.AuthDummyWebTask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network;
using Gsc.Tasks;
using System;
using System.Collections;

#nullable disable
namespace Gsc.Auth
{
  public class AuthDummyWebTask : IWebTask, IWebTaskBase, ITask, IEnumerator
  {
    public AuthDummyWebTask(WebTaskResult result) => this.Result = result;

    public WebTaskResult Result { get; private set; }

    public byte[] error => (byte[]) null;

    public bool handled => true;

    public bool isDone => true;

    public bool isBreak => throw new NotSupportedException();

    public object Current => throw new NotSupportedException();

    public void Retry() => WebQueue.defaultQueue.Pause(false);

    public void Break() => throw new NotSupportedException();

    public bool IsAcceptResult(WebTaskResult result) => throw new NotSupportedException();

    public bool HasAttributes(WebTaskAttribute attributes) => throw new NotSupportedException();

    public WebInternalTask GetInternalTask() => throw new NotSupportedException();

    public Type GetRequestType() => throw new NotSupportedException();

    public void OnStart() => throw new NotSupportedException();

    public IEnumerator Run() => throw new NotSupportedException();

    public void OnFinish() => throw new NotSupportedException();

    public void Reset() => throw new NotSupportedException();

    public bool MoveNext() => throw new NotSupportedException();
  }
}
