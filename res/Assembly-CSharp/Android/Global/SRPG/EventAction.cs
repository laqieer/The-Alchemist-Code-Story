// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [ExecuteInEditMode]
  public abstract class EventAction : ScriptableObject
  {
    [NonSerialized]
    public EventScript.Sequence Sequence;
    private bool mEnabled;
    [HideInInspector]
    public bool Skip;
    [NonSerialized]
    public EventAction NextAction;
    public static bool IsPreloading;
    private static Dictionary<string, List<string>> beltData;

    public bool enabled
    {
      set
      {
        if (this.mEnabled == value)
          return;
        this.mEnabled = value;
        if (this.mEnabled)
          this.OnActivate();
        else
          this.OnInactivate();
      }
      get
      {
        return this.mEnabled;
      }
    }

    protected void ActivateNext()
    {
      this.ActivateNext(false);
    }

    protected void ActivateNext(bool keepActive)
    {
      this.enabled = keepActive;
      for (EventAction nextAction = this.NextAction; (UnityEngine.Object) nextAction != (UnityEngine.Object) null; nextAction = nextAction.NextAction)
      {
        if (!nextAction.Skip)
        {
          nextAction.enabled = true;
          break;
        }
      }
    }

    public virtual void OnActivate()
    {
    }

    public virtual void OnInactivate()
    {
    }

    public virtual void Update()
    {
    }

    protected virtual void OnDestroy()
    {
    }

    public virtual bool Forward()
    {
      return false;
    }

    public virtual void SkipImmediate()
    {
      this.ActivateNext();
    }

    public virtual void GoToEndState()
    {
    }

    public virtual bool IsPreloadAssets
    {
      get
      {
        return false;
      }
    }

    public virtual bool ReplaySkipButtonEnable()
    {
      return true;
    }

    [DebuggerHidden]
    public virtual IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      EventAction.\u003CPreloadAssets\u003Ec__Iterator3E assetsCIterator3E = new EventAction.\u003CPreloadAssets\u003Ec__Iterator3E();
      return (IEnumerator) assetsCIterator3E;
    }

    public virtual void PreStart()
    {
    }

    public virtual string[] GetUnManagedAssetListData()
    {
      return (string[]) null;
    }

    public static string[] GetUnManagedStreamAssets(string[] pair, bool isBGM = false)
    {
      if (pair != null)
      {
        if (isBGM)
        {
          string s = pair[0];
          if (s.IndexOf("BGM_") != -1)
            s = pair[0].Replace("BGM_", string.Empty);
          int result = -1;
          if (int.TryParse(s, out result) && result >= EventAction_BGM.DEMO_BGM_ST && result <= EventAction_BGM.DEMO_BGM_ED)
            return new string[2]{ "sound/BGM_" + s + ".awb", "sound/BGM_" + s + ".acb" };
        }
        else
        {
          if (pair[0].IndexOf("VO_") != -1)
            return (string[]) null;
          return new string[2]{ "sound/" + pair[0] + ".awb", "sound/" + pair[0] + ".acb" };
        }
      }
      return (string[]) null;
    }

    protected Canvas ActiveCanvas
    {
      get
      {
        return EventScript.Canvas;
      }
    }

    public static GameObject FindActor(string actorID)
    {
      if (string.IsNullOrEmpty(actorID))
        return (GameObject) null;
      TacticsUnitController byUnitId;
      if ((UnityEngine.Object) (byUnitId = TacticsUnitController.FindByUnitID(actorID)) != (UnityEngine.Object) null)
        return byUnitId.gameObject;
      TacticsUnitController byUniqueName;
      if ((UnityEngine.Object) (byUniqueName = TacticsUnitController.FindByUniqueName(actorID)) != (UnityEngine.Object) null)
        return byUniqueName.gameObject;
      return GameObjectID.FindGameObject(actorID);
    }

    protected static Vector3 PointToWorld(IntVector2 pt)
    {
      Vector3 position = new Vector3((float) pt.x + 0.5f, 0.0f, (float) pt.y + 0.5f);
      position.y = GameUtility.RaycastGround(position).y;
      return position;
    }

    public static bool IsLoading
    {
      get
      {
        return EventAction.IsPreloading;
      }
    }

    public static string[] GetStreamResources(string[] pair)
    {
      if (pair == null)
        return new string[0];
      if (pair[0].IndexOf("VO_") != 0)
        return new string[0];
      return new string[2]{ pair[0] + ".awb", pair[0] + ".acb" };
    }

    public static bool IsUnManagedAssets(string name, bool isBGM = false)
    {
      if (string.IsNullOrEmpty(name))
        return false;
      if (isBGM)
      {
        string s = name;
        if (s.IndexOf("BGM_") != -1)
          s = name.Replace("BGM_", string.Empty);
        int result = -1;
        return int.TryParse(s, out result) && result >= EventAction_BGM.DEMO_BGM_ST && result <= EventAction_BGM.DEMO_BGM_ED;
      }
      return name.IndexOf("VO_") == -1;
    }

    private static void LoadBeltData()
    {
      if (EventAction.beltData != null)
        return;
      TextAsset textAsset = UnityEngine.Resources.Load("CustomItems/event2dblackbelt") as TextAsset;
      UnityEngine.Debug.Log((object) textAsset);
      string[] strArray = textAsset.text.Split('\n');
      EventAction.beltData = new Dictionary<string, List<string>>();
      string key = string.Empty;
      string pattern1 = "(\\[.*\\])";
      string pattern2 = "\\[(.*)\\]";
      foreach (string input in strArray)
      {
        if (Regex.Match(input, pattern1, RegexOptions.IgnoreCase).Success)
        {
          key = Regex.Replace(input, pattern2, "$1", RegexOptions.IgnoreCase);
        }
        else
        {
          if (!EventAction.beltData.ContainsKey(key))
            EventAction.beltData[key] = new List<string>();
          if (input.Trim().Length != 0)
            EventAction.beltData[key].Add(input.Trim());
        }
      }
    }

    public static void SetupBlackBelt(string name, string resId, GameObject go)
    {
      EventAction.LoadBeltData();
      List<string> stringList;
      if (!EventAction.beltData.TryGetValue(name, out stringList))
        return;
      string pattern = "(" + string.Join("|", stringList.ToArray()) + ")";
      if (!Regex.Match(resId, pattern, RegexOptions.IgnoreCase).Success)
        return;
      Image image = go.GetComponent<Image>();
      if ((UnityEngine.Object) image == (UnityEngine.Object) null)
        image = go.AddComponent<Image>();
      image.color = Color.black;
    }
  }
}
