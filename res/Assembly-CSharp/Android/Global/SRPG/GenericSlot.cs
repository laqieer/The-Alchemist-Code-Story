// Decompiled with JetBrains decompiler
// Type: SRPG.GenericSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GenericSlot : MonoBehaviour
  {
    public GenericSlot.SelectEvent OnSelect;
    [Space(10f)]
    public Graphic MainGraphic;
    public Image BGImage;
    public Sprite EmptyBG;
    public Sprite NonEmptyBG;
    [Space(10f)]
    public SRPG_Button SelectButton;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.SelectButton != (UnityEngine.Object) null))
        return;
      this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
    }

    private void OnButtonClick(SRPG_Button button)
    {
      if (this.OnSelect == null || !button.interactable)
        return;
      this.OnSelect(this, button.IsInteractable());
    }

    public void SetMainColor(Color color)
    {
      if (!((UnityEngine.Object) this.MainGraphic != (UnityEngine.Object) null))
        return;
      this.MainGraphic.color = color;
    }

    public void SetLocked(bool locked)
    {
      GenericSlotFlags[] componentsInChildren = this.GetComponentsInChildren<GenericSlotFlags>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if ((componentsInChildren[index].Flags & GenericSlotFlags.VisibleFlags.Locked) != (GenericSlotFlags.VisibleFlags) 0)
          componentsInChildren[index].gameObject.SetActive(locked);
      }
      if (!((UnityEngine.Object) this.SelectButton != (UnityEngine.Object) null))
        return;
      this.SelectButton.interactable = !locked;
    }

    public void SetSlotData<T>(T data)
    {
      DataSource.Bind<T>(this.gameObject, data);
      bool flag = (object) data == null;
      if ((UnityEngine.Object) this.BGImage != (UnityEngine.Object) null)
        this.BGImage.sprite = !flag ? this.NonEmptyBG : this.EmptyBG;
      GenericSlotFlags[] componentsInChildren = this.GetComponentsInChildren<GenericSlotFlags>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if ((componentsInChildren[index].Flags & GenericSlotFlags.VisibleFlags.Empty) != (GenericSlotFlags.VisibleFlags) 0)
          componentsInChildren[index].gameObject.SetActive(flag);
        if ((componentsInChildren[index].Flags & GenericSlotFlags.VisibleFlags.NonEmpty) != (GenericSlotFlags.VisibleFlags) 0)
          componentsInChildren[index].gameObject.SetActive(!flag);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    public delegate void SelectEvent(GenericSlot slot, bool interactable);
  }
}
