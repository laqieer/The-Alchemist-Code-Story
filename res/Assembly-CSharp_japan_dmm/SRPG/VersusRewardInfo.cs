// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class VersusRewardInfo : MonoBehaviour
  {
    private readonly float SPACE_SCALE = 1.1f;
    public Toggle arrivedTgl;
    public Toggle seasonTgl;
    public GameObject ArrivalView;
    public GameObject SeasonView;
    public ScrollRect Scroll;
    public RectTransform ListParent;

    private void Start()
    {
      if (Object.op_Equality((Object) this.ArrivalView, (Object) null) || Object.op_Equality((Object) this.SeasonView, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.arrivedTgl, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.arrivedTgl.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeArrival)));
      }
      if (!Object.op_Inequality((Object) this.seasonTgl, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.seasonTgl.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeSeason)));
    }

    public void OnChangeArrival(bool flg)
    {
      if (!flg || !Object.op_Inequality((Object) this.SeasonView, (Object) null) || !Object.op_Inequality((Object) this.ArrivalView, (Object) null))
        return;
      this.SeasonView.SetActive(false);
      this.ArrivalView.SetActive(true);
      this.SetScrollRect(this.ArrivalView.GetComponent<RectTransform>());
      ScrollListController component = this.ArrivalView.GetComponent<ScrollListController>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Refresh();
      float num1 = this.ArrivalView.GetComponent<RectTransform>().sizeDelta.y;
      if (Object.op_Inequality((Object) this.ListParent, (Object) null))
      {
        double num2 = (double) num1;
        Rect rect = this.ListParent.rect;
        double height = (double) ((Rect) ref rect).height;
        num1 = (float) (num2 - height);
      }
      int num3 = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - 1, 0);
      float num4 = component.ItemScale * this.SPACE_SCALE;
      float num5 = Mathf.Min(num1 - num4 * (float) num3, num1);
      component.MovePos(num5, num5);
    }

    public void OnChangeSeason(bool flg)
    {
      if (!flg || !Object.op_Inequality((Object) this.SeasonView, (Object) null) || !Object.op_Inequality((Object) this.ArrivalView, (Object) null))
        return;
      this.ArrivalView.SetActive(false);
      this.SeasonView.SetActive(true);
      this.SetScrollRect(this.SeasonView.GetComponent<RectTransform>());
      ScrollListController component = this.SeasonView.GetComponent<ScrollListController>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Refresh();
      float num1 = this.SeasonView.GetComponent<RectTransform>().sizeDelta.y;
      if (Object.op_Inequality((Object) this.ListParent, (Object) null))
      {
        double num2 = (double) num1;
        Rect rect = this.ListParent.rect;
        double height = (double) ((Rect) ref rect).height;
        num1 = (float) (num2 - height);
      }
      int num3 = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - 1, 0);
      float num4 = component.ItemScale * this.SPACE_SCALE;
      float num5 = Mathf.Min(num1 - num4 * (float) num3, num1);
      component.MovePos(num5, num5);
    }

    private void SetScrollRect(RectTransform rect)
    {
      if (!Object.op_Inequality((Object) this.Scroll, (Object) null) || !Object.op_Inequality((Object) rect, (Object) null))
        return;
      Vector2 anchoredPosition = rect.anchoredPosition;
      anchoredPosition.y = 0.0f;
      rect.anchoredPosition = anchoredPosition;
      this.Scroll.content = rect;
    }
  }
}
