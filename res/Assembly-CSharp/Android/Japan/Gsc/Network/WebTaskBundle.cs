// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebTaskBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Gsc.Network
{
  public class WebTaskBundle : IEnumerable<IWebTask>, IEnumerable, IEnumerator
  {
    private readonly List<IWebTask> Tasks;

    public WebTaskBundle()
    {
      this.Tasks = new List<IWebTask>();
    }

    public WebTaskBundle(IEnumerable<IWebTask> tasks)
    {
      this.Tasks = new List<IWebTask>(tasks);
    }

    public WebTaskBundle(List<IWebTask> tasks)
    {
      this.Tasks = tasks;
    }

    public bool isDone { get; protected set; }

    public event Action OnFinish;

    public T Add<T>(T task) where T : IWebTask
    {
      this.Tasks.Add((IWebTask) task);
      return task;
    }

    public void Retry()
    {
      for (int index = 0; index < this.Tasks.Count; ++index)
        this.Tasks[index].Retry();
    }

    public bool HasResult(WebTaskResult result)
    {
      for (int index = 0; index < this.Tasks.Count; ++index)
      {
        if (this.Tasks[index].Result == result)
          return true;
      }
      return false;
    }

    public IEnumerator<IWebTask> GetEnumerator()
    {
      return (IEnumerator<IWebTask>) this.Tasks.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public object Current
    {
      get
      {
        return (object) null;
      }
    }

    public void Reset()
    {
    }

    public bool MoveNext()
    {
      if (this.isDone)
        return false;
      for (int index = 0; index < this.Tasks.Count; ++index)
      {
        if (!this.Tasks[index].isDone)
          return true;
      }
      this.isDone = true;
      // ISSUE: reference to a compiler-generated field
      if (this.OnFinish != null)
      {
        // ISSUE: reference to a compiler-generated field
        this.OnFinish();
      }
      return false;
    }
  }
}
