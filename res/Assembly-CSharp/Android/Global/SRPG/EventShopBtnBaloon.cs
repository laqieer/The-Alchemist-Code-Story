// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBtnBaloon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.BaloonChar != (UnityEngine.Object) null)
        this.BaloonChar.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.BaloonTextLeft != (UnityEngine.Object) null)
        this.BaloonTextLeft.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.BaloonTextRight != (UnityEngine.Object) null)
        this.BaloonTextRight.gameObject.SetActive(false);
      this.RefreshBaloonImage();
      this.UpdatePosition();
    }

    private void RefreshBaloonImage()
    {
      if ((UnityEngine.Object) this.BaloonChar != (UnityEngine.Object) null)
      {
        this.BaloonChar.sprite = !((UnityEngine.Object) this.CurrentCharSprite != (UnityEngine.Object) null) ? this.BaloonChar.sprite : this.CurrentCharSprite;
        this.BaloonChar.gameObject.SetActive(true);
      }
      if ((UnityEngine.Object) this.BaloonTextLeft != (UnityEngine.Object) null)
      {
        this.BaloonTextLeft.sprite = !((UnityEngine.Object) this.CurrentTextLeftSprite != (UnityEngine.Object) null) ? this.BaloonTextLeft.sprite : this.CurrentTextLeftSprite;
        this.BaloonTextLeft.gameObject.SetActive(true);
      }
      if (!((UnityEngine.Object) this.BaloonTextRight != (UnityEngine.Object) null))
        return;
      this.BaloonTextRight.sprite = !((UnityEngine.Object) this.CurrentTextRightSprite != (UnityEngine.Object) null) ? this.BaloonTextRight.sprite : this.CurrentTextRightSprite;
      this.BaloonTextRight.gameObject.SetActive(true);
    }

    private void UpdatePosition()
    {
      if (string.IsNullOrEmpty(this.ReverseObjectID))
        return;
      GameObject gameObject = GameObjectID.FindGameObject(this.ReverseObjectID);
      if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
        return;
      UIProjector component = gameObject.GetComponent<UIProjector>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.ReStart();
    }
  }
}
