// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RuneSlot : MonoBehaviour
  {
    [SerializeField]
    private GameObject mSelectedGO;
    [SerializeField]
    private ImageArray mSlotNumberImageArray;
    [SerializeField]
    private GameObject mEquipGO;
    [SerializeField]
    private GameObject mNoEquipGO;
    [SerializeField]
    private RuneIcon mRuneIcon;
    [SerializeField]
    private GameObject mEquipEffect;
    private BindRuneData mRuneData;
    private RuneSlotIndex mSlotIndex;

    public BindRuneData RuneData => this.mRuneData;

    private void Awake()
    {
      if (Object.op_Equality((Object) this.mSelectedGO, (Object) null))
        DebugUtility.LogError("mSelectedGO is unable to attach.");
      if (Object.op_Equality((Object) this.mSlotNumberImageArray, (Object) null))
        DebugUtility.LogError("mSlotNumberImageArray is unable to attach.");
      if (Object.op_Equality((Object) this.mEquipGO, (Object) null))
        DebugUtility.LogError("mEquipGO is unable to attach.");
      if (Object.op_Equality((Object) this.mNoEquipGO, (Object) null))
        DebugUtility.LogError("mNoEquipGO is unable to attach.");
      if (Object.op_Equality((Object) this.mRuneIcon, (Object) null))
        DebugUtility.LogError("コンポーネントに RuneIcon が設定されていません");
      this.mEquipGO.SetActive(false);
      this.mNoEquipGO.SetActive(false);
    }

    public void Initialize(BindRuneData rune_data, RuneSlotIndex slot_index, bool is_play_effect = true)
    {
      this.mRuneData = rune_data;
      this.mSlotIndex = slot_index;
      this.Selected(false);
      this.Refresh(is_play_effect);
    }

    private void Refresh(bool is_play_effect = true)
    {
      this.RefreshSlotNumber(this.mSlotIndex);
      if ((byte) 0 > (byte) this.mSlotIndex || (byte) this.mSlotIndex > (byte) 5)
        return;
      this.RefreshSlotEquip(this.mRuneData != null);
      this.mRuneIcon.Setup(this.mRuneData);
      if (!is_play_effect)
        return;
      this.PlayEquipEffect();
    }

    public void Selected(bool is_selected) => this.mSelectedGO.SetActive(is_selected);

    private void RefreshSlotNumber(RuneSlotIndex slot_index)
    {
      if ((byte) 0 > (byte) slot_index || (int) (byte) slot_index >= this.mSlotNumberImageArray.Images.Length)
        return;
      this.mSlotNumberImageArray.ImageIndex = (int) (byte) slot_index;
    }

    private void RefreshSlotEquip(bool is_equip)
    {
      this.mEquipGO.SetActive(is_equip);
      this.mNoEquipGO.SetActive(!is_equip);
    }

    private void PlayEquipEffect()
    {
      if (!Object.op_Inequality((Object) this.mEquipEffect, (Object) null))
        return;
      UIUtility.SpawnParticle(this.mEquipEffect, ((Component) this).transform as RectTransform, new Vector2(0.5f, 0.5f));
    }
  }
}
