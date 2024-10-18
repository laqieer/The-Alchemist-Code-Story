// Decompiled with JetBrains decompiler
// Type: Gsc.Tasks.ParallelWorker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace Gsc.Tasks
{
  public class ParallelWorker : MonoBehaviour
  {
    private List<IEnumerator> tasks = new List<IEnumerator>();

    public int TaskCount => this.tasks.Count;

    public void AddTask(ITask task) => this.AddTask(ParallelWorker._AddTask(task));

    public void AddTask(IEnumerator task) => this.tasks.Add(task);

    [DebuggerHidden]
    private static IEnumerator _AddTask(ITask task)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ParallelWorker.\u003C_AddTask\u003Ec__Iterator0()
      {
        task = task
      };
    }

    private void Start() => this.StartCoroutine(this.Run());

    [DebuggerHidden]
    private IEnumerator Run()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ParallelWorker.\u003CRun\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }
  }
}
