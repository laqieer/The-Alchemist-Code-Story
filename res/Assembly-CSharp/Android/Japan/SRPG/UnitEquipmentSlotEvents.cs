// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEquipmentSlotEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
        Animator component = this.GetComponent<Animator>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
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
