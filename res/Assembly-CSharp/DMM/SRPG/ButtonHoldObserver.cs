// Decompiled with JetBrains decompiler
// Type: SRPG.ButtonHoldObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  public class ButtonHoldObserver : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
  {
    private float[] HoldSpan = new float[5]
    {
      0.55f,
      0.25f,
      0.25f,
      0.1f,
      0.05f
    };
    private float HoldDuration;
    private bool Holding;
    private int ActionCount;
    private Vector2 mDragStartPos;
    public ButtonHoldObserver.DelegateOnHoldEvent OnHoldStart;
    public ButtonHoldObserver.DelegateOnHoldEvent OnHoldEnd;
    public ButtonHoldObserver.DelegateOnHoldEvent OnHoldUpdate;

    public void OnPointerDown(PointerEventData eventData)
    {
      if (this.OnHoldStart == null)
        return;
      this.OnHoldStart();
      this.Holding = true;
      this.mDragStartPos = eventData.position;
    }

    public void OnPointerUp()
    {
      if (this.OnHoldEnd != null)
        this.OnHoldEnd();
      this.StatusReset();
    }

    public void StatusReset()
    {
      this.Holding = false;
      this.ActionCount = 0;
      this.HoldDuration = 0.0f;
      ((Vector2) ref this.mDragStartPos).Set(0.0f, 0.0f);
    }

    public void Update()
    {
      if (this.OnHoldUpdate == null)
        return;
      float unscaledDeltaTime = Time.unscaledDeltaTime;
      if (this.Holding && !Input.GetMouseButton(0))
      {
        this.OnPointerUp();
      }
      else
      {
        GameSettings instance = GameSettings.Instance;
        float num = (float) (instance.HoldMargin * instance.HoldMargin);
        Vector2 vector2 = Vector2.op_Subtraction(this.mDragStartPos, Vector2.op_Implicit(Input.mousePosition));
        bool flag = (double) ((Vector2) ref vector2).sqrMagnitude > (double) num;
        if ((double) this.HoldDuration < (double) this.HoldSpan[this.ActionCount] && this.ActionCount < 1 && flag)
        {
          this.StatusReset();
        }
        else
        {
          if (!this.Holding)
            return;
          this.HoldDuration += unscaledDeltaTime;
          if ((double) this.HoldDuration < (double) this.HoldSpan[this.ActionCount])
            return;
          this.HoldDuration -= this.HoldSpan[this.ActionCount];
          this.OnHoldUpdate();
          if (this.ActionCount >= this.HoldSpan.Length - 1)
            return;
          ++this.ActionCount;
        }
      }
    }

    public void OnDestroy()
    {
      this.StatusReset();
      this.OnHoldStart = (ButtonHoldObserver.DelegateOnHoldEvent) null;
      this.OnHoldEnd = (ButtonHoldObserver.DelegateOnHoldEvent) null;
    }

    public delegate void DelegateOnHoldEvent();
  }
}
