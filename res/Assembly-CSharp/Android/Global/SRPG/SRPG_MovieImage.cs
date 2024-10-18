// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_MovieImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (CriManaMovieControllerForUI))]
  [FlowNode.Pin(9, "Started", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 1)]
  public class SRPG_MovieImage : RawImage, IFlowInterface
  {
    public const int PINID_STARTED = 9;
    public const int PINID_FINISHED = 10;
    private CriManaMovieControllerForUI mMovieController;
    private bool mPlaying;

    protected override void Awake()
    {
      base.Awake();
      GameUtility.SetNeverSleep();
      if (!Application.isPlaying)
        return;
      MyCriManager.Setup(false);
      this.mMovieController = this.GetComponent<CriManaMovieControllerForUI>();
      if (!((UnityEngine.Object) this.mMovieController != (UnityEngine.Object) null))
        return;
      this.mMovieController.moviePath = MyCriManager.GetLoadFileName(this.mMovieController.moviePath, false);
    }

    private void Update()
    {
      if (!((UnityEngine.Object) this.mMovieController != (UnityEngine.Object) null) || this.mMovieController.player.status < CriMana.Player.Status.Playing)
        return;
      this.material = this.mMovieController.material;
      this.UpdateMaterial();
      if (this.mMovieController.player.status == CriMana.Player.Status.Playing)
      {
        if (this.mPlaying)
          return;
        this.mPlaying = true;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 9);
      }
      else
      {
        if (this.mMovieController.player.status != CriMana.Player.Status.PlayEnd || !this.mPlaying)
          return;
        this.mPlaying = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
    }

    protected override void OnDestroy()
    {
      GameUtility.SetDefaultSleepSetting();
      base.OnDestroy();
    }

    public void Activated(int pinID)
    {
    }
  }
}
