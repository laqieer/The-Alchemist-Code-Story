// Decompiled with JetBrains decompiler
// Type: SRPG.GenericSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (!Object.op_Inequality((Object) this.SelectButton, (Object) null))
        return;
      this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
    }

    private void OnButtonClick(SRPG_Button button)
    {
      if (this.OnSelect == null || !((Selectable) button).interactable)
        return;
      this.OnSelect(this, ((Selectable) button).IsInteractable());
    }

    public void SetMainColor(Color color)
    {
      if (!Object.op_Inequality((Object) this.MainGraphic, (Object) null))
        return;
      this.MainGraphic.color = color;
    }

    public void SetLocked(bool locked)
    {
      GenericSlotFlags[] componentsInChildren = ((Component) this).GetComponentsInChildren<GenericSlotFlags>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if ((componentsInChildren[index].Flags & GenericSlotFlags.VisibleFlags.Locked) != (GenericSlotFlags.VisibleFlags) 0)
          ((Component) componentsInChildren[index]).gameObject.SetActive(locked);
      }
      if (!Object.op_Inequality((Object) this.SelectButton, (Object) null))
        return;
      ((Selectable) this.SelectButton).interactable = !locked;
    }

    public void SetSlotData<T>(T data)
    {
      DataSource.Bind<T>(((Component) this).gameObject, data);
      bool flag = (object) data == null;
      if (Object.op_Inequality((Object) this.BGImage, (Object) null))
        this.BGImage.sprite = !flag ? this.NonEmptyBG : this.EmptyBG;
      GenericSlotFlags[] componentsInChildren = ((Component) this).GetComponentsInChildren<GenericSlotFlags>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if ((componentsInChildren[index].Flags & GenericSlotFlags.VisibleFlags.Empty) != (GenericSlotFlags.VisibleFlags) 0)
          ((Component) componentsInChildren[index]).gameObject.SetActive(flag);
        if ((componentsInChildren[index].Flags & GenericSlotFlags.VisibleFlags.NonEmpty) != (GenericSlotFlags.VisibleFlags) 0)
          ((Component) componentsInChildren[index]).gameObject.SetActive(!flag);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public delegate void SelectEvent(GenericSlot slot, bool interactable);
  }
}
