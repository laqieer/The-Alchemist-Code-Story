// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPreview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitPreview : UnitController
  {
    private const string ID_IDLE = "idle";
    private const string ID_ACTION = "action";
    public bool PlayAction;
    private bool mPlayingAction;
    public int DefaultLayer = GameUtility.LayerDefault;
    private bool mLoadThreadFlag;

    public bool LoadThreadFlag => this.mLoadThreadFlag;

    protected override void Start()
    {
      this.KeepUnitHidden = true;
      this.LoadEquipments = true;
      this.mLoadThreadFlag = false;
      base.Start();
      this.ResetAnimation();
    }

    protected override void PostSetup()
    {
      base.PostSetup();
      this.LoadUnitAnimationAsync("idle", "unit_info_idle0", true);
      this.LoadUnitAnimationAsync("action", "unit_info_act0", true);
      this.StartCoroutine(this.LoadThread());
      this.mLoadThreadFlag = true;
    }

    protected override void Update()
    {
      base.Update();
      if (this.IsLoading)
        return;
      if (this.PlayAction)
      {
        this.PlayAction = false;
        this.PlayAnimation("action", false, 0.1f);
        this.mPlayingAction = true;
      }
      else
      {
        if (!this.mPlayingAction || (double) this.GetRemainingTime("action") > 0.0)
          return;
        this.PlayAnimation("idle", true, 0.1f);
        this.mPlayingAction = false;
      }
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.ResetAnimation();
      if (this.IsLoading)
        return;
      this.PlayAnimation("idle", true, 0.0f);
      this.mPlayingAction = false;
    }

    [DebuggerHidden]
    private IEnumerator LoadThread()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitPreview.\u003CLoadThread\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public Transform GetHeadPosition()
    {
      return Object.op_Inequality((Object) this.Rig, (Object) null) ? GameUtility.findChildRecursively(((Component) this).transform, this.Rig.Head) : (Transform) null;
    }
  }
}
