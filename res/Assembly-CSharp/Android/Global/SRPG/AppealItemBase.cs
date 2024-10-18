// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AppealItemBase : MonoBehaviour
  {
    private Sprite mAppealSprite;
    public Image AppealObject;

    public Sprite AppealSprite
    {
      get
      {
        return this.mAppealSprite;
      }
      set
      {
        this.mAppealSprite = value;
      }
    }

    protected virtual void Awake()
    {
      if (!((UnityEngine.Object) this.AppealObject != (UnityEngine.Object) null))
        return;
      this.AppealObject.gameObject.SetActive(false);
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void Destroy()
    {
    }

    protected virtual void Refresh()
    {
      if ((UnityEngine.Object) this.mAppealSprite == (UnityEngine.Object) null)
      {
        if (!((UnityEngine.Object) this.AppealObject != (UnityEngine.Object) null))
          return;
        this.AppealObject.gameObject.SetActive(false);
      }
      else
      {
        this.AppealObject.gameObject.SetActive(true);
        this.AppealObject.sprite = this.mAppealSprite;
      }
    }
  }
}
