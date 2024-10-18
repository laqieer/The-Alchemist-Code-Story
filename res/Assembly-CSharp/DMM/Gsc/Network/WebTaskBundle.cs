// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebTaskBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Network
{
  public class WebTaskBundle : IEnumerable<IWebTask>, IEnumerable, IEnumerator
  {
    private readonly List<IWebTask> Tasks;

    public WebTaskBundle() => this.Tasks = new List<IWebTask>();

    public WebTaskBundle(IEnumerable<IWebTask> tasks) => this.Tasks = new List<IWebTask>(tasks);

    public WebTaskBundle(List<IWebTask> tasks) => this.Tasks = tasks;

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

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public object Current => (object) null;

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
      if (this.OnFinish != null)
        this.OnFinish();
      return false;
    }
  }
}
