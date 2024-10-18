// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryItemDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class GalleryItemDetailWindow : MonoBehaviour
  {
    private void Start()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      DataSource.Bind<ItemParam>(this.gameObject, DataSource.FindDataOfClass<ItemParam>(currentValue.GetGameObject("item"), (ItemParam) null), false);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
