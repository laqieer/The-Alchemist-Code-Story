// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBtnBaloon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventShopBtnBaloon : MonoBehaviour
  {
    [SerializeField]
    private Image BaloonChar;
    [SerializeField]
    private Image BaloonTextLeft;
    [SerializeField]
    private Image BaloonTextRight;
    [SerializeField]
    private string ReverseObjectID;
    [HideInInspector]
    public Sprite CurrentTextLeftSprite;
    [HideInInspector]
    public Sprite CurrentTextRightSprite;
    [HideInInspector]
    public Sprite CurrentCharSprite;
    private GameObject mBaloonChar;
    private GameObject mBaloonTextLeft;
    private GameObject mBaloonTextRight;

    private void Start()
    {
      if (Object.op_Inequality((Object) this.BaloonChar, (Object) null))
        ((Component) this.BaloonChar).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.BaloonTextLeft, (Object) null))
        ((Component) this.BaloonTextLeft).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.BaloonTextRight, (Object) null))
        ((Component) this.BaloonTextRight).gameObject.SetActive(false);
      this.RefreshBaloonImage();
      this.UpdatePosition();
    }

    private void RefreshBaloonImage()
    {
      if (Object.op_Inequality((Object) this.BaloonChar, (Object) null))
      {
        this.BaloonChar.sprite = !Object.op_Inequality((Object) this.CurrentCharSprite, (Object) null) ? this.BaloonChar.sprite : this.CurrentCharSprite;
        ((Component) this.BaloonChar).gameObject.SetActive(true);
      }
      if (Object.op_Inequality((Object) this.BaloonTextLeft, (Object) null))
      {
        this.BaloonTextLeft.sprite = !Object.op_Inequality((Object) this.CurrentTextLeftSprite, (Object) null) ? this.BaloonTextLeft.sprite : this.CurrentTextLeftSprite;
        ((Component) this.BaloonTextLeft).gameObject.SetActive(true);
      }
      if (!Object.op_Inequality((Object) this.BaloonTextRight, (Object) null))
        return;
      this.BaloonTextRight.sprite = !Object.op_Inequality((Object) this.CurrentTextRightSprite, (Object) null) ? this.BaloonTextRight.sprite : this.CurrentTextRightSprite;
      ((Component) this.BaloonTextRight).gameObject.SetActive(true);
    }

    private void UpdatePosition()
    {
      if (string.IsNullOrEmpty(this.ReverseObjectID))
        return;
      GameObject gameObject = GameObjectID.FindGameObject(this.ReverseObjectID);
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      UIProjector component = gameObject.GetComponent<UIProjector>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.ReStart();
    }
  }
}
