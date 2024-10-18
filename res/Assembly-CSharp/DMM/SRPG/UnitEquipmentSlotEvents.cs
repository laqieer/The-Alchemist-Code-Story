// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEquipmentSlotEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (Animator))]
  public class UnitEquipmentSlotEvents : ListItemEvents
  {
    [HelpBox("スロットの状態にあわせてこの数値を切り替えます。スロットが空=0、装備は持ってる=1、レベル足りない=2、装備してる=3")]
    public string StateIntName = "state";

    public UnitEquipmentSlotEvents.SlotStateTypes StateType
    {
      set
      {
        Animator component = ((Component) this).GetComponent<Animator>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.SetInteger(this.StateIntName, (int) value);
        component.Update(1f);
      }
    }

    public enum SlotStateTypes
    {
      Empty,
      HasEquipment,
      NeedMoreLevel,
      Equipped,
      EnableCraft,
      EnableCraftNeedMoreLevel,
      None,
      EnableCommon,
      EnableCommonSoul,
      EnableCommonSoulNeedMoreLevel,
    }
  }
}
