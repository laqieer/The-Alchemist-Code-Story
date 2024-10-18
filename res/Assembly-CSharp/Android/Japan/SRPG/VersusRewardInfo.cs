// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.ArrivalView == (UnityEngine.Object) null || (UnityEngine.Object) this.SeasonView == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.arrivedTgl != (UnityEngine.Object) null)
        this.arrivedTgl.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeArrival));
      if (!((UnityEngine.Object) this.seasonTgl != (UnityEngine.Object) null))
        return;
      this.seasonTgl.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeSeason));
    }

    public void OnChangeArrival(bool flg)
    {
      if (!flg || !((UnityEngine.Object) this.SeasonView != (UnityEngine.Object) null) || !((UnityEngine.Object) this.ArrivalView != (UnityEngine.Object) null))
        return;
      this.SeasonView.SetActive(false);
      this.ArrivalView.SetActive(true);
      this.SetScrollRect(this.ArrivalView.GetComponent<RectTransform>());
      ScrollListController component = this.ArrivalView.GetComponent<ScrollListController>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Refresh();
      float y = this.ArrivalView.GetComponent<RectTransform>().sizeDelta.y;
      if ((UnityEngine.Object) this.ListParent != (UnityEngine.Object) null)
        y -= this.ListParent.rect.height;
      int num1 = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - 1, 0);
      float num2 = component.ItemScale * this.SPACE_SCALE;
      float num3 = Mathf.Min(y - num2 * (float) num1, y);
      component.MovePos(num3, num3);
    }

    public void OnChangeSeason(bool flg)
    {
      if (!flg || !((UnityEngine.Object) this.SeasonView != (UnityEngine.Object) null) || !((UnityEngine.Object) this.ArrivalView != (UnityEngine.Object) null))
        return;
      this.ArrivalView.SetActive(false);
      this.SeasonView.SetActive(true);
      this.SetScrollRect(this.SeasonView.GetComponent<RectTransform>());
      ScrollListController component = this.SeasonView.GetComponent<ScrollListController>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Refresh();
      float y = this.SeasonView.GetComponent<RectTransform>().sizeDelta.y;
      if ((UnityEngine.Object) this.ListParent != (UnityEngine.Object) null)
        y -= this.ListParent.rect.height;
      int num1 = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - 1, 0);
      float num2 = component.ItemScale * this.SPACE_SCALE;
      float num3 = Mathf.Min(y - num2 * (float) num1, y);
      component.MovePos(num3, num3);
    }

    private void SetScrollRect(RectTransform rect)
    {
      if (!((UnityEngine.Object) this.Scroll != (UnityEngine.Object) null) || !((UnityEngine.Object) rect != (UnityEngine.Object) null))
        return;
      Vector2 anchoredPosition = rect.anchoredPosition;
      anchoredPosition.y = 0.0f;
      rect.anchoredPosition = anchoredPosition;
      this.Scroll.content = rect;
    }
  }
}
