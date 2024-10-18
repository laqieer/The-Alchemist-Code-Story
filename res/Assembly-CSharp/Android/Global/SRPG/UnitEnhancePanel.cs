// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEnhancePanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitEnhancePanel : MonoBehaviour
  {
    public UnitEquipmentSlotEvents[] EquipmentSlots;
    public SRPG_Button JobRankUpButton;
    public SRPG_Button JobUnlockButton;
    public SRPG_Button AllEquipButton;
    public GameObject JobRankCapCaution;
    public SRPG_Button JobRankupAllIn;
    [Space(10f)]
    public GenericSlot ArtifactSlot;
    [Space(10f)]
    public GenericSlot ArtifactSlot2;
    [Space(10f)]
    public GenericSlot ArtifactSlot3;
    [Space(10f)]
    public RectTransform ExpItemList;
    public ListItemEvents ExpItemTemplate;
    public SRPG_Button UnitLevelupButton;
    [Space(10f)]
    public UnitAbilityList AbilityList;
    [Space(10f)]
    public UnitAbilityList AbilitySlots;

    private void Awake()
    {
      Canvas component = this.GetComponent<Canvas>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.enabled = false;
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.ExpItemTemplate != (UnityEngine.Object) null))
        return;
      this.ExpItemTemplate.gameObject.SetActive(false);
    }
  }
}
