// Decompiled with JetBrains decompiler
// Type: WatchManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WatchManager : MonoSingleton<WatchManager>
{
  public List<DateTime> mealTimes = new List<DateTime>();
  private static readonly bool IS_DEBUG;

  public bool IsAvailable { get; private set; }

  public List<int> GetMealHours()
  {
    if (!this.IsAvailable)
      return (List<int>) null;
    DateTime serverTime = TimeManager.ServerTime;
    return this.mealTimes.ConvertAll<int>((Converter<DateTime, int>) (meal =>
    {
      if (meal.Equals(DateTime.MinValue) || serverTime.Day != meal.Day || serverTime.Hour < meal.Hour)
        return -1;
      return meal.Hour;
    }));
  }

  public void Clear()
  {
    this.mealTimes.Clear();
  }

  private static bool connectToWatch()
  {
    return false;
  }

  public void Init()
  {
    WatchManager.MyDebug.PushMessage("WatchManager.Init", false);
    this.IsAvailable = false;
    if (!WatchManager.connectToWatch())
      return;
    WatchManager.MyDebug.PushMessage("### WatchManager connected to watch.", true);
    this.IsAvailable = true;
  }

  protected override void Initialize()
  {
    WatchManager.MyDebug.PushMessage("WatchManager.Initialize", false);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    this.Init();
  }

  protected override void Release()
  {
    WatchManager.MyDebug.PushMessage("WatchManager.Release", false);
  }

  public void OnEatMeal(string message)
  {
    WatchManager.MyDebug.PushMessage("### WatchManager.OnEatMeal " + message, true);
    DateTime ateTime = DateTime.Parse(message);
    if (this.mealTimes.Exists((Predicate<DateTime>) (meal => meal.Hour == ateTime.Hour)))
      return;
    this.mealTimes.Add(ateTime);
  }

  private class MyDebug
  {
    private static WatchManager.MyDebug self = new WatchManager.MyDebug();
    private List<string> contents = new List<string>();

    public static void PushMessage(string msg, bool is_log = false)
    {
      if (!WatchManager.IS_DEBUG)
        return;
      WatchManager.MyDebug.self.contents.Add(msg);
      if (!is_log)
        return;
      Debug.Log((object) msg);
    }

    [DebuggerHidden]
    public static IEnumerable<string> EachMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      WatchManager.MyDebug.\u003CEachMessage\u003Ec__Iterator0 messageCIterator0_1 = new WatchManager.MyDebug.\u003CEachMessage\u003Ec__Iterator0();
      // ISSUE: variable of a compiler-generated type
      WatchManager.MyDebug.\u003CEachMessage\u003Ec__Iterator0 messageCIterator0_2 = messageCIterator0_1;
      // ISSUE: reference to a compiler-generated field
      messageCIterator0_2.\u0024PC = -2;
      return (IEnumerable<string>) messageCIterator0_2;
    }

    public static void Clear()
    {
      WatchManager.MyDebug.self.contents.Clear();
    }
  }
}
