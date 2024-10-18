// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactWindowEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArtifactWindowEventHandler : MonoBehaviour, IGameParameter
  {
    public ArtifactList mArtifactList;
    public Button mBackButton;
    public Button mForwardButton;

    public void OnBackButton(Button button)
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || (UnityEngine.Object) this.mArtifactList == (UnityEngine.Object) null)
        return;
      this.mArtifactList.SelectBack(artifactData);
    }

    public void OnForwardButton(Button button)
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || (UnityEngine.Object) this.mArtifactList == (UnityEngine.Object) null)
        return;
      this.mArtifactList.SelectFoward(artifactData);
    }

    public ArtifactData GetArtifactData()
    {
      return DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
    }

    public void UpdateValue()
    {
      this.UpdateBackButtonIntaractable();
      this.UpdateForwardButtonIntaractable();
    }

    private void UpdateBackButtonIntaractable()
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || !((UnityEngine.Object) this.mArtifactList != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mBackButton != (UnityEngine.Object) null))
        return;
      this.mBackButton.interactable = !this.mArtifactList.CheckStartOfIndex(artifactData);
    }

    private void UpdateForwardButtonIntaractable()
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || !((UnityEngine.Object) this.mArtifactList != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mForwardButton != (UnityEngine.Object) null))
        return;
      this.mForwardButton.interactable = !this.mArtifactList.CheckEndOfIndex(artifactData);
    }
  }
}
