// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryArtifactDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GalleryArtifactDetailWindow : MonoBehaviour
  {
    private void Start()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      DataSource.Bind<ArtifactParam>(((Component) this).gameObject, DataSource.FindDataOfClass<ArtifactParam>(currentValue.GetGameObject("_self"), (ArtifactParam) null));
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
