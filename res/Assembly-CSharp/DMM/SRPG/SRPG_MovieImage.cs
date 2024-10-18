// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_MovieImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
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

    protected virtual void Awake()
    {
      ((UIBehaviour) this).Awake();
      GameUtility.SetNeverSleep();
      if (!Application.isPlaying)
        return;
      MyCriManager.Setup();
      this.mMovieController = ((Component) this).GetComponent<CriManaMovieControllerForUI>();
      if (!Object.op_Inequality((Object) this.mMovieController, (Object) null))
        return;
      ((CriManaMovieMaterial) this.mMovieController).moviePath = MyCriManager.GetLoadFileName(((CriManaMovieMaterial) this.mMovieController).moviePath);
    }

    private void Update()
    {
      if (!Object.op_Inequality((Object) this.mMovieController, (Object) null) || ((CriManaMovieMaterial) this.mMovieController).player.status < 5)
        return;
      ((Graphic) this).material = ((CriManaMovieMaterial) this.mMovieController).material;
      ((Graphic) this).UpdateMaterial();
      if (((CriManaMovieMaterial) this.mMovieController).player.status == 5)
      {
        if (this.mPlaying)
          return;
        this.mPlaying = true;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 9);
      }
      else
      {
        if (((CriManaMovieMaterial) this.mMovieController).player.status != 6 || !this.mPlaying)
          return;
        this.mPlaying = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
    }

    protected virtual void OnDestroy()
    {
      GameUtility.SetDefaultSleepSetting();
      ((UIBehaviour) this).OnDestroy();
    }

    public void Activated(int pinID)
    {
    }
  }
}
