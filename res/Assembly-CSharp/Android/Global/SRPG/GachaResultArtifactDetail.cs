// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultArtifactDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(2, "Refreshed", FlowNode.PinTypes.Output, 2)]
  public class GachaResultArtifactDetail : MonoBehaviour, IFlowInterface
  {
    private readonly int OUT_CLOSE_DETAIL = 100;
    public GameObject ArtifactInfo;
    public GameObject Bg;
    private ArtifactData mCurrentArtifact;
    [SerializeField]
    private Button BackBtn;

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.BackBtn != (UnityEngine.Object) null))
        return;
      this.BackBtn.onClick.AddListener(new UnityAction(this.OnCloseDetail));
    }

    private void OnEnable()
    {
      Animator component1 = this.GetComponent<Animator>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        component1.SetBool("close", false);
      if (!((UnityEngine.Object) this.Bg != (UnityEngine.Object) null))
        return;
      CanvasGroup component2 = this.Bg.GetComponent<CanvasGroup>();
      if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
        return;
      component2.interactable = true;
      component2.blocksRaycasts = true;
    }

    private void OnCloseDetail()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_CLOSE_DETAIL);
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.ArtifactInfo == (UnityEngine.Object) null)
        return;
      int index = int.Parse(FlowNode_Variable.Get("GachaResultDataIndex"));
      ArtifactParam artifact = GachaResultData.drops[index].artifact;
      if (artifact == null)
        return;
      this.mCurrentArtifact = new ArtifactData();
      this.mCurrentArtifact = this.CreateArtifactData(artifact, GachaResultData.drops[index].Rare);
      DataSource.Bind<ArtifactData>(this.ArtifactInfo, this.mCurrentArtifact);
      GameParameter.UpdateAll(this.ArtifactInfo);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }

    private ArtifactData CreateArtifactData(ArtifactParam param, int rarity)
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = param.iname,
        fav = 0,
        rare = Math.Min(Math.Max(param.rareini, rarity), param.raremax)
      });
      return artifactData;
    }
  }
}
