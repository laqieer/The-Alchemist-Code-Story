// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusItemListDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class LoginBonusItemListDetailWindow : MonoBehaviour
  {
    public void Refresh()
    {
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(GlobalVars.SelectedItemID);
      if (itemDataByItemId != null)
      {
        DataSource.Bind<ItemData>(((Component) this).gameObject, itemDataByItemId);
      }
      else
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(GlobalVars.SelectedItemID);
        if (itemParam == null)
          return;
        DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
      ((Behaviour) this).enabled = true;
    }
  }
}
