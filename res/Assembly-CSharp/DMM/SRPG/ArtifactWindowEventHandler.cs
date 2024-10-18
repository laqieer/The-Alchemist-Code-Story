// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactWindowEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArtifactWindowEventHandler : MonoBehaviour, IGameParameter
  {
    public ArtifactList mArtifactList;
    public ArtifactScrollList mArtifactScrollList;
    public Button mBackButton;
    public Button mForwardButton;

    public void OnBackButton(Button button)
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || Object.op_Equality((Object) this.mArtifactList, (Object) null) && Object.op_Equality((Object) this.mArtifactScrollList, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mArtifactScrollList, (Object) null))
        this.mArtifactScrollList.SelectBack(artifactData);
      else
        this.mArtifactList.SelectBack(artifactData);
    }

    public void OnForwardButton(Button button)
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null || Object.op_Equality((Object) this.mArtifactList, (Object) null) && Object.op_Equality((Object) this.mArtifactScrollList, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mArtifactScrollList, (Object) null))
        this.mArtifactScrollList.SelectFoward(artifactData);
      else
        this.mArtifactList.SelectFoward(artifactData);
    }

    public ArtifactData GetArtifactData()
    {
      return DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
    }

    public void UpdateValue()
    {
      this.UpdateBackButtonIntaractable();
      this.UpdateForwardButtonIntaractable();
    }

    private void UpdateBackButtonIntaractable()
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null)
        return;
      if (Object.op_Inequality((Object) this.mArtifactList, (Object) null))
      {
        if (!Object.op_Inequality((Object) this.mBackButton, (Object) null))
          return;
        ((Selectable) this.mBackButton).interactable = !this.mArtifactList.CheckStartOfIndex(artifactData);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mArtifactScrollList, (Object) null) || !Object.op_Inequality((Object) this.mBackButton, (Object) null))
          return;
        ((Selectable) this.mBackButton).interactable = !this.mArtifactScrollList.CheckStartOfIndex(artifactData);
      }
    }

    private void UpdateForwardButtonIntaractable()
    {
      ArtifactData artifactData = this.GetArtifactData();
      if (artifactData == null)
        return;
      if (Object.op_Inequality((Object) this.mArtifactList, (Object) null))
      {
        if (!Object.op_Inequality((Object) this.mForwardButton, (Object) null))
          return;
        ((Selectable) this.mForwardButton).interactable = !this.mArtifactList.CheckEndOfIndex(artifactData);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mArtifactScrollList, (Object) null) || !Object.op_Inequality((Object) this.mForwardButton, (Object) null))
          return;
        ((Selectable) this.mForwardButton).interactable = !this.mArtifactScrollList.CheckEndOfIndex(artifactData);
      }
    }
  }
}
