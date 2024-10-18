// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
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
      get => this.mEnabled;
    }

    protected void ActivateNext() => this.ActivateNext(false);

    protected void ActivateNext(bool keepActive)
    {
      this.enabled = keepActive;
      for (EventAction nextAction = this.NextAction; UnityEngine.Object.op_Inequality((UnityEngine.Object) nextAction, (UnityEngine.Object) null); nextAction = nextAction.NextAction)
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

    public virtual bool Forward() => false;

    public virtual bool OnEventSkip() => this.Forward();

    public virtual void SkipImmediate()
    {
    }

    public virtual void GoToEndState()
    {
    }

    public virtual bool IsPreloadAssets => false;

    public virtual bool ReplaySkipButtonEnable() => true;

    [DebuggerHidden]
    public virtual IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      EventAction.\u003CPreloadAssets\u003Ec__Iterator0 assetsCIterator0 = new EventAction.\u003CPreloadAssets\u003Ec__Iterator0();
      return (IEnumerator) assetsCIterator0;
    }

    public virtual void PreStart()
    {
    }

    public virtual string[] GetStreamingAssets() => (string[]) null;

    public virtual string[] GetUnManagedAssetListData() => (string[]) null;

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
            return new string[2]
            {
              "sound/BGM_" + s + ".awb",
              "sound/BGM_" + s + ".acb"
            };
        }
        else
        {
          if (pair[0].IndexOf("VO_") != -1)
            return (string[]) null;
          return new string[2]
          {
            "sound/" + pair[0] + ".awb",
            "sound/" + pair[0] + ".acb"
          };
        }
      }
      return (string[]) null;
    }

    protected Canvas ActiveCanvas => EventScript.Canvas;

    protected RectTransform EventRootTransform => EventScript.EventRootTransform;

    public static GameObject FindActor(string actorID)
    {
      if (string.IsNullOrEmpty(actorID))
        return (GameObject) null;
      TacticsUnitController byUnitId;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) (byUnitId = TacticsUnitController.FindByUnitID(actorID)), (UnityEngine.Object) null))
        return ((Component) byUnitId).gameObject;
      TacticsUnitController byUniqueName;
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) (byUniqueName = TacticsUnitController.FindByUniqueName(actorID)), (UnityEngine.Object) null) ? ((Component) byUniqueName).gameObject : GameObjectID.FindGameObject(actorID);
    }

    protected static Vector3 PointToWorld(IntVector2 pt)
    {
      Vector3 position;
      // ISSUE: explicit constructor call
      ((Vector3) ref position).\u002Ector((float) pt.x + 0.5f, 0.0f, (float) pt.y + 0.5f);
      position.y = GameUtility.RaycastGround(position).y;
      return position;
    }

    public static bool IsLoading => EventAction.IsPreloading;

    public static string[] GetStreamResources(string[] pair)
    {
      if (pair == null)
        return new string[0];
      if (pair[0].IndexOf("VO_") != 0)
        return new string[0];
      return new string[2]
      {
        pair[0] + ".awb",
        pair[0] + ".acb"
      };
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
  }
}
