// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebQueue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth;
using Gsc.Auth.GAuth.GAuth.API.Request;
using Gsc.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

#nullable disable
namespace Gsc.Network
{
  public class WebQueue : IEnumerator
  {
    private static bool _sceneLoaded;
    private bool _lazyStarted;
    private object subroutine;
    private IRefreshTokenTask authTask;
    private Type execludRetryType = typeof (AccessTokenVerify);
    private bool needAuthorization = true;
    private readonly List<IWebTask> executedTasks;
    private readonly WebQueue.SerialQueue serialQueue;
    private readonly WebQueue.ParallelQueue parallelQueue;
    private bool pause;
    private bool running;
    private bool activeStarted;

    public WebQueue()
    {
      this.executedTasks = new List<IWebTask>();
      this.serialQueue = new WebQueue.SerialQueue(this.executedTasks, new Action<List<IWebTask>>(this.OnCompleted));
      this.parallelQueue = new WebQueue.ParallelQueue(this.executedTasks, new Action<List<IWebTask>>(this.OnCompleted));
    }

    public IWebQueueObserver Observer { get; set; }

    public static WebQueue defaultQueue { get; private set; }

    public bool isPause => this.pause;

    public bool isRunning => this.HasVisibleTasks();

    public static void Init(IWebQueueObserver observer)
    {
      WebQueue.defaultQueue = new WebQueue();
      WebQueue.defaultQueue.Observer = observer;
    }

    public void Add(IWebTask task)
    {
      if (task.HasAttributes(WebTaskAttribute.Parallel))
        this.parallelQueue.Add(task);
      else
        this.serialQueue.Add(task);
      this.Start();
    }

    private bool HasNext()
    {
      return this.needAuthorization || this.serialQueue.HasNext() || this.parallelQueue.HasNext();
    }

    private bool HasVisibleTasks() => this.serialQueue.isRunning || this.parallelQueue.isRunning;

    public void Start()
    {
      if (!WebQueue._sceneLoaded)
      {
        if (this._lazyStarted)
          return;
        this._lazyStarted = true;
        RootObject.Instance.StartCoroutine(this.LazyStart());
      }
      else
      {
        if (this.pause)
          return;
        if (!this.running && this.HasNext())
        {
          this.running = true;
          RootObject.Instance.StartCoroutine((IEnumerator) this);
        }
        if (this.activeStarted || this.Observer == null || !this.HasVisibleTasks())
          return;
        this.activeStarted = true;
        this.Observer.OnStart();
      }
    }

    [DebuggerHidden]
    private IEnumerator LazyStart()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WebQueue.\u003CLazyStart\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [RuntimeInitializeOnLoadMethod]
    private static void SceneLoaded() => WebQueue._sceneLoaded = true;

    private static WebTaskResult GetTaskResults(List<IWebTask> tasks)
    {
      WebTaskResult taskResults = WebTaskResult.None;
      for (int index = 0; index < tasks.Count; ++index)
        taskResults |= tasks[index].Result;
      return taskResults;
    }

    private void OnCompleted(List<IWebTask> executedTasks)
    {
      if (executedTasks.Count == 0)
        return;
      if (WebQueue.GetTaskResults(executedTasks) == WebTaskResult.Success)
      {
        executedTasks.Clear();
      }
      else
      {
        List<IWebTask> list = executedTasks.Where<IWebTask>((Func<IWebTask, bool>) (x => !x.handled && !x.IsAcceptResult(x.Result) && x.HasAttributes(WebTaskAttribute.Reliable) && (x.Result & WebTaskResult.kLocalResult) == WebTaskResult.None)).ToList<IWebTask>();
        executedTasks.Clear();
        if ((WebQueue.GetTaskResults(list) & (WebTaskResult.kUnableToContinue | WebTaskResult.kCreticalError)) > WebTaskResult.None)
        {
          this.Reset();
          this.Pause();
          this.Notify(list.Where<IWebTask>((Func<IWebTask, bool>) (x => (x.Result & (WebTaskResult.kUnableToContinue | WebTaskResult.kCreticalError)) > WebTaskResult.None)).ToList<IWebTask>());
        }
        else
        {
          if ((WebQueue.GetTaskResults(list) & WebTaskResult.InternalExpiredTokenError) > WebTaskResult.None)
          {
            this.needAuthorization = true;
            foreach (IWebTask webTask in list.Where<IWebTask>((Func<IWebTask, bool>) (x => x.Result == WebTaskResult.InternalExpiredTokenError && !x.GetRequestType().Equals(this.execludRetryType))))
              webTask.Retry();
            this.parallelQueue.CancelAndRequeueAll();
            list = list.Where<IWebTask>((Func<IWebTask, bool>) (x => x.Result != WebTaskResult.InternalExpiredTokenError)).ToList<IWebTask>();
          }
          if (WebQueue.GetTaskResults(list) <= WebTaskResult.None)
            return;
          this.Pause();
          this.Notify(list);
        }
      }
    }

    private void Notify(List<IWebTask> tasks)
    {
      if (this.Observer == null || tasks.Count <= 0)
        return;
      this.Observer.OnReceiveUnhandledTasks(new WebTaskBundle(tasks));
    }

    public void Pause(bool state = true)
    {
      this.pause = state;
      if (this.pause)
        return;
      this.Start();
    }

    public void Reset()
    {
      this.serialQueue.Reset();
      this.parallelQueue.Reset();
      this.executedTasks.Clear();
      this.pause = false;
    }

    public object Current => this.subroutine;

    public bool MoveNext()
    {
      if (!SDK.Initialized)
      {
        this.running = this.serialQueue.HasNext();
        if (this.running)
          this.subroutine = (object) this.serialQueue;
        return this.running;
      }
      this.subroutine = this.GetSubRoutine();
      if (this.subroutine != null)
        return true;
      if (!this.pause)
        this.parallelQueue.Update();
      this.running = !this.pause && this.HasNext();
      if (this.activeStarted && this.Observer != null && !this.HasVisibleTasks())
      {
        this.activeStarted = false;
        this.Observer.OnFinish();
      }
      return this.running;
    }

    private object GetSubRoutine()
    {
      if (this.needAuthorization)
      {
        if (this.authTask == null)
        {
          this.authTask = Session.DefaultSession.GetRefreshTokenTask();
          return (object) this.authTask.Run();
        }
        WebTaskResult result = this.authTask.Result;
        this.authTask = (IRefreshTokenTask) null;
        this.needAuthorization = result != WebTaskResult.Success;
        if (this.needAuthorization)
        {
          this.Notify(new List<IWebTask>()
          {
            (IWebTask) new AuthDummyWebTask(result)
          });
          this.pause = true;
          return (object) null;
        }
      }
      return this.serialQueue.HasNext() ? (object) this.serialQueue : (object) null;
    }

    private class SerialQueue : IEnumerator
    {
      private readonly LinkedList<IWebTask> queuedTasks;
      private readonly List<IWebTask> executedTasks;
      private readonly Action<List<IWebTask>> onCompleted;
      private IWebTask currentTask;

      public SerialQueue(List<IWebTask> executedTasks, Action<List<IWebTask>> onCompleted)
      {
        this.queuedTasks = new LinkedList<IWebTask>();
        this.executedTasks = executedTasks;
        this.onCompleted = onCompleted;
      }

      public bool isRunning
      {
        get
        {
          return this.currentTask != null && !this.currentTask.HasAttributes(WebTaskAttribute.Silent) || this.queuedTasks.Where<IWebTask>((Func<IWebTask, bool>) (x => !x.HasAttributes(WebTaskAttribute.Silent))).Count<IWebTask>() > 0;
        }
      }

      public bool HasNext() => this.currentTask != null || this.queuedTasks.Count > 0;

      public void Add(IWebTask task)
      {
        if (task.HasAttributes(WebTaskAttribute.Interrupt))
          this.queuedTasks.AddFirst(task);
        else
          this.queuedTasks.AddLast(task);
      }

      public void Update()
      {
        if (this.currentTask == null)
        {
          this.currentTask = this.queuedTasks.First.Value;
          this.currentTask.OnStart();
          this.queuedTasks.RemoveFirst();
        }
        if (!this.currentTask.GetInternalTask().isDone)
          return;
        this.currentTask.OnFinish();
        this.executedTasks.Add(this.currentTask);
        this.currentTask = (IWebTask) null;
        this.onCompleted(this.executedTasks);
      }

      public object Current
      {
        get => this.currentTask != null ? (object) this.currentTask.Run() : (object) null;
      }

      public bool MoveNext()
      {
        this.Update();
        return this.HasNext();
      }

      public void Reset()
      {
        this.queuedTasks.Clear();
        this.currentTask = (IWebTask) null;
      }
    }

    private class ParallelQueue
    {
      private readonly List<IWebTask> queuedTasks;
      private readonly List<IWebTask> runningTasks;
      private readonly List<IWebTask> executedTasks;
      private readonly Action<List<IWebTask>> onCompleted;

      public ParallelQueue(List<IWebTask> executedTasks, Action<List<IWebTask>> onCompleted)
      {
        this.queuedTasks = new List<IWebTask>();
        this.runningTasks = new List<IWebTask>();
        this.executedTasks = executedTasks;
        this.onCompleted = onCompleted;
      }

      public bool isRunning
      {
        get
        {
          return this.queuedTasks.Where<IWebTask>((Func<IWebTask, bool>) (x => !x.HasAttributes(WebTaskAttribute.Silent))).Count<IWebTask>() + this.runningTasks.Where<IWebTask>((Func<IWebTask, bool>) (x => !x.HasAttributes(WebTaskAttribute.Silent))).Count<IWebTask>() > 0;
        }
      }

      public bool HasNext() => this.queuedTasks.Count + this.runningTasks.Count > 0;

      public void Add(IWebTask task) => this.queuedTasks.Add(task);

      public void Update()
      {
        int count = this.executedTasks.Count;
        for (int index = 0; index < this.queuedTasks.Count; ++index)
        {
          IWebTask queuedTask = this.queuedTasks[index];
          queuedTask.OnStart();
          this.runningTasks.Add(queuedTask);
        }
        this.queuedTasks.Clear();
        for (int index = 0; index < this.runningTasks.Count; ++index)
        {
          IWebTask runningTask = this.runningTasks[index];
          WebInternalTask internalTask = runningTask.GetInternalTask();
          if (!internalTask.MoveNext() && internalTask.isDone)
          {
            runningTask.OnFinish();
            this.executedTasks.Add(runningTask);
          }
        }
        for (int index = count; index < this.executedTasks.Count; ++index)
          this.runningTasks.Remove(this.executedTasks[index]);
        this.onCompleted(this.executedTasks);
      }

      public void CancelAndRequeueAll()
      {
        for (int index = 0; index < this.runningTasks.Count; ++index)
        {
          IWebTask runningTask = this.runningTasks[index];
          runningTask.GetInternalTask().Reset();
          this.queuedTasks.Add(runningTask);
        }
        this.runningTasks.Clear();
      }

      public void Reset()
      {
        this.queuedTasks.Clear();
        this.runningTasks.Clear();
      }
    }
  }
}
