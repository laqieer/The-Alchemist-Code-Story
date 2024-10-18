// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusItemListDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class LoginBonusItemListDetailWindow : MonoBehaviour
  {
    public void Refresh()
    {
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(GlobalVars.SelectedItemID);
      if (itemDataByItemId != null)
      {
        DataSource.Bind<ItemData>(this.gameObject, itemDataByItemId, false);
      }
      else
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(GlobalVars.SelectedItemID);
        if (itemParam == null)
          return;
        DataSource.Bind<ItemParam>(this.gameObject, itemParam, false);
      }
      GameParameter.UpdateAll(this.gameObject);
      this.enabled = true;
    }
  }
}
