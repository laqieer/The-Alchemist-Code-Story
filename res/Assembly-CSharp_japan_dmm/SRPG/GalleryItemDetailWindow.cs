// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryItemDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GalleryItemDetailWindow : MonoBehaviour
  {
    private void Start()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      DataSource.Bind<ItemParam>(((Component) this).gameObject, DataSource.FindDataOfClass<ItemParam>(currentValue.GetGameObject("_self"), (ItemParam) null));
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
