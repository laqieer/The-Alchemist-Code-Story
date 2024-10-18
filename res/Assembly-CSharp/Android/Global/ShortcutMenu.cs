// Decompiled with JetBrains decompiler
// Type: ShortcutMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;

[FlowNode.Pin(101, "InputON", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(111, "InputON_Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(110, "InputOFF_Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(100, "InputOFF", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 0)]
public class ShortcutMenu : MonoBehaviour, IFlowInterface
{
  private bool IsInput = true;
  private RectTransform mRectTransform;
  public GameObject Badge_MenuOpen;
  public GameObject Badge_MenuClose;
  public GameObject Badge_Unit;
  public GameObject Badge_Gift;
  public GameObject Badge_DailyMission;
  private bool showTrophyBadge;

  private void Start()
  {
    this.mRectTransform = this.transform as RectTransform;
    if ((UnityEngine.Object) this.Badge_MenuOpen != (UnityEngine.Object) null)
      this.Badge_MenuOpen.SetActive(false);
    if ((UnityEngine.Object) this.Badge_MenuClose != (UnityEngine.Object) null)
      this.Badge_MenuClose.SetActive(false);
    if ((UnityEngine.Object) this.Badge_Unit != (UnityEngine.Object) null)
      this.Badge_Unit.SetActive(false);
    if ((UnityEngine.Object) this.Badge_Gift != (UnityEngine.Object) null)
      this.Badge_Gift.SetActive(false);
    if (!((UnityEngine.Object) this.Badge_DailyMission != (UnityEngine.Object) null))
      return;
    this.Badge_DailyMission.SetActive(false);
  }

  private void Update()
  {
    if (this.IsInput && Input.GetMouseButtonDown(0))
    {
      Vector2 localPoint;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(this.mRectTransform, (Vector2) Input.mousePosition, (UnityEngine.Camera) null, out localPoint);
      if (!this.mRectTransform.rect.Contains(localPoint) && (!MonoSingleton<GameManager>.Instance.IsTutorial() || (UnityEngine.Object) SGHighlightObject.Instance() == (UnityEngine.Object) null))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    if (!((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null))
      return;
    bool flag1 = false;
    if (!instanceDirect.CheckBusyBadges(GameManager.BadgeTypes.Unit | GameManager.BadgeTypes.UnitUnlock) && (UnityEngine.Object) this.Badge_Unit != (UnityEngine.Object) null)
    {
      this.Badge_Unit.SetActive(instanceDirect.CheckBadges(GameManager.BadgeTypes.Unit | GameManager.BadgeTypes.UnitUnlock));
      if (this.Badge_Unit.GetActive())
        flag1 = true;
    }
    if (!instanceDirect.CheckBusyBadges(GameManager.BadgeTypes.GiftBox) && (UnityEngine.Object) this.Badge_Gift != (UnityEngine.Object) null)
    {
      this.Badge_Gift.SetActive(instanceDirect.CheckBadges(GameManager.BadgeTypes.GiftBox));
      if (this.Badge_Gift.GetActive())
        flag1 = true;
    }
    if ((UnityEngine.Object) this.Badge_DailyMission != (UnityEngine.Object) null && instanceDirect.Player.UpdateTrophyBadge)
    {
      this.showTrophyBadge = instanceDirect.Player.UpdateTrophyBadge;
      instanceDirect.Player.UpdateTrophyBadge = false;
      this.Badge_DailyMission.SetActive(this.showTrophyBadge);
      if (this.showTrophyBadge)
        flag1 = true;
      instanceDirect.Player.UpdateTrophyBadge = false;
    }
    bool flag2 = (UnityEngine.Object) this.Badge_Gift != (UnityEngine.Object) null && this.Badge_Gift.GetActive() || (UnityEngine.Object) this.Badge_DailyMission != (UnityEngine.Object) null && this.Badge_DailyMission.GetActive() || (UnityEngine.Object) this.Badge_Unit != (UnityEngine.Object) null && this.Badge_Unit.GetActive() || flag1;
    if ((UnityEngine.Object) this.Badge_MenuOpen != (UnityEngine.Object) null)
      this.Badge_MenuOpen.SetActive(flag2);
    if (!((UnityEngine.Object) this.Badge_MenuClose != (UnityEngine.Object) null))
      return;
    this.Badge_MenuClose.SetActive(flag2);
  }

  public void Activated(int pinID)
  {
    if (pinID == 100)
    {
      this.IsInput = false;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
    }
    else
    {
      if (pinID != 101)
        return;
      this.IsInput = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }
  }
}
