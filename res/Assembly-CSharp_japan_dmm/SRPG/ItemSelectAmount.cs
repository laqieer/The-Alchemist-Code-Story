// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectAmount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ItemSelectAmount : MonoBehaviour, IGameParameter
  {
    public void UpdateValue()
    {
      ItemSelectListItemData dataOfClass = DataSource.FindDataOfClass<ItemSelectListItemData>(((Component) this).gameObject, (ItemSelectListItemData) null);
      Text component = ((Component) this).gameObject.GetComponent<Text>();
      if (!Object.op_Inequality((Object) component, (Object) null) || dataOfClass == null)
        return;
      component.text = dataOfClass.num.ToString();
    }
  }
}
