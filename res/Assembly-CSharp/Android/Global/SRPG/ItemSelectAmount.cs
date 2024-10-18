// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectAmount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
