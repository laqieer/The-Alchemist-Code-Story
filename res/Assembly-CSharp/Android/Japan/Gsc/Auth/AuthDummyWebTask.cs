// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.AuthDummyWebTask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Network;
using Gsc.Tasks;
using System;
using System.Collections;

namespace Gsc.Auth
{
  public class AuthDummyWebTask : IWebTask, IWebTaskBase, ITask, IEnumerator
  {
    public AuthDummyWebTask(WebTaskResult result)
    {
      this.Result = result;
    }

    public WebTaskResult Result { get; private set; }

    public byte[] error
    {
      get
      {
        return (byte[]) null;
      }
    }

    public bool handled
    {
      get
      {
        return true;
      }
    }

    public bool isDone
    {
      get
      {
        return true;
      }
    }

    public bool isBreak
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    public object Current
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    public void Retry()
    {
      WebQueue.defaultQueue.Pause(false);
    }

    public void Break()
    {
      throw new NotSupportedException();
    }

    public bool IsAcceptResult(WebTaskResult result)
    {
      throw new NotSupportedException();
    }

    public bool HasAttributes(WebTaskAttribute attributes)
    {
      throw new NotSupportedException();
    }

    public WebInternalTask GetInternalTask()
    {
      throw new NotSupportedException();
    }

    public Type GetRequestType()
    {
      throw new NotSupportedException();
    }

    public void OnStart()
    {
      throw new NotSupportedException();
    }

    public IEnumerator Run()
    {
      throw new NotSupportedException();
    }

    public void OnFinish()
    {
      throw new NotSupportedException();
    }

    public void Reset()
    {
      throw new NotSupportedException();
    }

    public bool MoveNext()
    {
      throw new NotSupportedException();
    }
  }
}
