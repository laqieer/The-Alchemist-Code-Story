// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectAmount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ItemSelectAmount : MonoBehaviour, IGameParameter
  {
    public void UpdateValue()
    {
      ItemSelectListItemData dataOfClass = DataSource.FindDataOfClass<ItemSelectListItemData>(this.gameObject, (ItemSelectListItemData) null);
      Text component = this.gameObject.GetComponent<Text>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || dataOfClass == null)
        return;
      component.text = dataOfClass.num.ToString();
    }
  }
}
