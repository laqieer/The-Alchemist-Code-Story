// Decompiled with JetBrains decompiler
// Type: SRPG.JukeBoxItemNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class JukeBoxItemNode : 
    ContentNode,
    IGameParameter,
    IPointerDownHandler,
    IHoldGesture,
    IEventSystemHandler
  {
    [SerializeField]
    private Text TextMusic;
    [Space(5f)]
    [SerializeField]
    private GameObject GoActive;
    [SerializeField]
    private GameObject NewBadge;
    [SerializeField]
    private GameObject Mylist;
    [Space(5f)]
    [SerializeField]
    private SRPG_Button BtnSelect;
    [Space(5f)]
    [SerializeField]
    private GameObject GoLocked;
    [SerializeField]
    private SRPG_Button BtnLock;
    private JukeBoxItemParam mParam;

    public JukeBoxItemParam Param => this.mParam;

    public void Setup(
      JukeBoxItemParam param,
      bool is_current,
      bool is_new,
      bool is_mylist,
      UnityAction action = null,
      UnityAction lock_action = null,
      UnityAction long_tap_action = null)
    {
      if (param == null || param.ItemData == null || param.ItemData.param == null)
        return;
      this.mParam = param;
      if (Object.op_Implicit((Object) this.TextMusic))
        this.TextMusic.text = param.ItemData.param.Title;
      if (Object.op_Implicit((Object) this.GoLocked))
      {
        this.GoLocked.SetActive(!param.ItemData.is_unlock);
        if (lock_action != null && Object.op_Implicit((Object) this.BtnSelect))
        {
          ((UnityEventBase) this.BtnSelect.onClick).RemoveAllListeners();
          ((UnityEvent) this.BtnSelect.onClick).AddListener(lock_action);
        }
      }
      if (action != null && param.ItemData.is_unlock && Object.op_Implicit((Object) this.BtnSelect))
      {
        ((UnityEventBase) this.BtnSelect.onClick).RemoveAllListeners();
        ((UnityEvent) this.BtnSelect.onClick).AddListener(action);
      }
      if (Object.op_Implicit((Object) this.GoActive))
        this.GoActive.SetActive(false);
      if (Object.op_Implicit((Object) this.NewBadge))
        this.NewBadge.SetActive(false);
      if (Object.op_Implicit((Object) this.Mylist))
        this.Mylist.SetActive(false);
      this.SetCurrent(is_current);
      this.SetNewBadge(is_new);
      this.SetMylist(is_mylist);
    }

    public void SetCurrent(bool is_active)
    {
      if (!Object.op_Implicit((Object) this.GoActive))
        return;
      this.GoActive.SetActive(is_active);
    }

    public bool IsCurrent()
    {
      if (Object.op_Implicit((Object) this.GoActive))
        this.GoActive.GetActive();
      return false;
    }

    public void SetNewBadge(bool is_new)
    {
      if (!Object.op_Implicit((Object) this.NewBadge))
        return;
      this.NewBadge.SetActive(is_new);
    }

    public void SetMylist(bool is_mylist)
    {
      if (!Object.op_Implicit((Object) this.Mylist))
        return;
      this.Mylist.SetActive(is_mylist);
    }

    public virtual bool HasTooltip => true;

    protected void ShowTooltip(Vector2 screen)
    {
      if (this.mParam == null || !this.mParam.ItemData.is_unlock || string.IsNullOrEmpty(this.mParam.ItemData.param.Situation))
        return;
      this.mParam.LongTapAction.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      HoldGestureObserver.StartHoldGesture((IHoldGesture) this);
    }

    public void OnPointerHoldStart()
    {
      if (!this.HasTooltip)
        return;
      RectTransform transform = (RectTransform) ((Component) this).transform;
      Vector2 screen = Vector2.op_Implicit(((Transform) transform).TransformPoint(Vector2.op_Implicit(Vector2.zero)));
      CanvasScaler componentInParent = ((Component) transform).GetComponentInParent<CanvasScaler>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null))
      {
        Vector3 localScale = ((Component) componentInParent).transform.localScale;
        screen.x /= localScale.x;
        screen.y /= localScale.y;
      }
      this.ShowTooltip(screen);
    }

    public void OnPointerHoldEnd()
    {
    }

    public void UpdateValue()
    {
    }
  }
}
