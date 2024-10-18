// Decompiled with JetBrains decompiler
// Type: LogKit.Worker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

#nullable disable
namespace LogKit
{
  public class Worker : MonoBehaviour
  {
    private Thread workerThread;
    private List<Logger> loggers;
    private volatile bool destoried;

    public static void LaunchWorker(List<Logger> loggers, GameObject node = null)
    {
      if (Object.op_Equality((Object) node, (Object) null))
      {
        node = new GameObject("LogKit.Worker");
        Object.DontDestroyOnLoad((Object) node);
      }
      if (!Object.op_Equality((Object) node.GetComponent<Worker>(), (Object) null))
        return;
      node.AddComponent<Worker>().loggers = loggers;
    }

    private void Awake()
    {
      this.workerThread = new Thread(new ThreadStart(this.WorkingThreadStart));
    }

    private void OnDestroy() => this.destoried = true;

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Worker.\u003CStart\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void OnApplicationFocus(bool focusState)
    {
      if (focusState)
        return;
      for (int index = 0; index < this.loggers.Count; ++index)
      {
        Logger logger = this.loggers[index];
        logger.Emit();
        logger.Flush();
      }
    }

    private void WorkingThreadStart()
    {
      ushort num = 0;
      do
      {
        Logger logger = this.loggers[(int) num++ % this.loggers.Count];
        logger.Flush();
        logger.Send();
        Thread.Sleep(5000);
      }
      while (!this.destoried);
    }
  }
}
