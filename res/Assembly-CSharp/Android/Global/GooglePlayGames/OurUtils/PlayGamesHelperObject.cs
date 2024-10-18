// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.OurUtils.PlayGamesHelperObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
  public class PlayGamesHelperObject : MonoBehaviour
  {
    private static PlayGamesHelperObject instance = (PlayGamesHelperObject) null;
    private static bool sIsDummy = false;
    private static List<Action> sQueue = new List<Action>();
    private static volatile bool sQueueEmpty = true;
    private static List<Action<bool>> sPauseCallbackList = new List<Action<bool>>();
    private static List<Action<bool>> sFocusCallbackList = new List<Action<bool>>();

    public static void CreateObject()
    {
      if ((UnityEngine.Object) PlayGamesHelperObject.instance != (UnityEngine.Object) null)
        return;
      if (Application.isPlaying)
      {
        GameObject gameObject = new GameObject("PlayGames_QueueRunner");
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) gameObject);
        PlayGamesHelperObject.instance = gameObject.AddComponent<PlayGamesHelperObject>();
      }
      else
      {
        PlayGamesHelperObject.instance = new PlayGamesHelperObject();
        PlayGamesHelperObject.sIsDummy = true;
      }
    }

    public void Awake()
    {
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    }

    public void OnDisable()
    {
      if (!((UnityEngine.Object) PlayGamesHelperObject.instance == (UnityEngine.Object) this))
        return;
      PlayGamesHelperObject.instance = (PlayGamesHelperObject) null;
    }

    public static void RunCoroutine(IEnumerator action)
    {
      if (!((UnityEngine.Object) PlayGamesHelperObject.instance != (UnityEngine.Object) null))
        return;
      PlayGamesHelperObject.instance.StartCoroutine(action);
    }

    public static void RunOnGameThread(Action action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      if (PlayGamesHelperObject.sIsDummy)
        return;
      lock (PlayGamesHelperObject.sQueue)
      {
        PlayGamesHelperObject.sQueue.Add(action);
        PlayGamesHelperObject.sQueueEmpty = false;
      }
    }

    public void Update()
    {
      if (PlayGamesHelperObject.sIsDummy || PlayGamesHelperObject.sQueueEmpty)
        return;
      List<Action> actionList = new List<Action>();
      lock (PlayGamesHelperObject.sQueue)
      {
        actionList.AddRange((IEnumerable<Action>) PlayGamesHelperObject.sQueue);
        PlayGamesHelperObject.sQueue.Clear();
        PlayGamesHelperObject.sQueueEmpty = true;
      }
      actionList.ForEach((Action<Action>) (a => a()));
    }

    public void OnApplicationFocus(bool focused)
    {
      using (List<Action<bool>>.Enumerator enumerator = PlayGamesHelperObject.sFocusCallbackList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Action<bool> current = enumerator.Current;
          try
          {
            current(focused);
          }
          catch (Exception ex)
          {
            Debug.LogError((object) ("Exception in OnApplicationFocus:" + ex.Message + "\n" + ex.StackTrace));
          }
        }
      }
    }

    public void OnApplicationPause(bool paused)
    {
      using (List<Action<bool>>.Enumerator enumerator = PlayGamesHelperObject.sPauseCallbackList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Action<bool> current = enumerator.Current;
          try
          {
            current(paused);
          }
          catch (Exception ex)
          {
            Debug.LogError((object) ("Exception in OnApplicationPause:" + ex.Message + "\n" + ex.StackTrace));
          }
        }
      }
    }

    public static void AddFocusCallback(Action<bool> callback)
    {
      if (PlayGamesHelperObject.sFocusCallbackList.Contains(callback))
        return;
      PlayGamesHelperObject.sFocusCallbackList.Add(callback);
    }

    public static bool RemoveFocusCallback(Action<bool> callback)
    {
      return PlayGamesHelperObject.sFocusCallbackList.Remove(callback);
    }

    public static void AddPauseCallback(Action<bool> callback)
    {
      if (PlayGamesHelperObject.sPauseCallbackList.Contains(callback))
        return;
      PlayGamesHelperObject.sPauseCallbackList.Add(callback);
    }

    public static bool RemovePauseCallback(Action<bool> callback)
    {
      return PlayGamesHelperObject.sPauseCallbackList.Remove(callback);
    }
  }
}
