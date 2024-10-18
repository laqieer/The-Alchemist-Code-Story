// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class AppealItemBase : MonoBehaviour
  {
    private Sprite mAppealSprite;
    public Image AppealObject;

    public Sprite AppealSprite
    {
      get => this.mAppealSprite;
      set => this.mAppealSprite = value;
    }

    protected virtual void Awake()
    {
      if (!Object.op_Inequality((Object) this.AppealObject, (Object) null))
        return;
      ((Component) this.AppealObject).gameObject.SetActive(false);
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

    protected virtual void OnDestroy()
    {
    }

    protected virtual void Refresh()
    {
      if (Object.op_Equality((Object) this.mAppealSprite, (Object) null))
      {
        if (!Object.op_Inequality((Object) this.AppealObject, (Object) null))
          return;
        ((Component) this.AppealObject).gameObject.SetActive(false);
      }
      else
      {
        ((Component) this.AppealObject).gameObject.SetActive(true);
        this.AppealObject.sprite = this.mAppealSprite;
      }
    }
  }
}
