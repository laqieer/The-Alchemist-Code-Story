﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Tasks.ParallelWorker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Gsc.Tasks
{
  public class ParallelWorker : MonoBehaviour
  {
    private List<IEnumerator> tasks = new List<IEnumerator>();

    public int TaskCount
    {
      get
      {
        return this.tasks.Count;
      }
    }

    public void AddTask(ITask task)
    {
      this.AddTask(ParallelWorker._AddTask(task));
    }

    public void AddTask(IEnumerator task)
    {
      this.tasks.Add(task);
    }

    [DebuggerHidden]
    private static IEnumerator _AddTask(ITask task)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ParallelWorker.\u003C_AddTask\u003Ec__Iterator1F()
      {
        task = task,
        \u003C\u0024\u003Etask = task
      };
    }

    private void Start()
    {
      this.StartCoroutine(this.Run());
    }

    [DebuggerHidden]
    private IEnumerator Run()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ParallelWorker.\u003CRun\u003Ec__Iterator20()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
