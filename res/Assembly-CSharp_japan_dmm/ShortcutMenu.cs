// Decompiled with JetBrains decompiler
// Type: ShortcutMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(100, "InputOFF", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(101, "InputON", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(110, "InputOFF_Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(111, "InputON_Output", FlowNode.PinTypes.Output, 0)]
public class ShortcutMenu : MonoBehaviour, IFlowInterface
{
  private RectTransform mRectTransform;
  public GameObject Badge_MenuOpen;
  public GameObject Badge_MenuClose;
  public GameObject Badge_Unit;
  public GameObject Badge_Gift;
  public GameObject Badge_DailyMission;
  private bool IsInput = true;

  private void Start()
  {
    this.mRectTransform = ((Component) this).transform as RectTransform;
    if (Object.op_Inequality((Object) this.Badge_MenuOpen, (Object) null))
      this.Badge_MenuOpen.SetActive(false);
    if (Object.op_Inequality((Object) this.Badge_MenuClose, (Object) null))
      this.Badge_MenuClose.SetActive(false);
    if (Object.op_Inequality((Object) this.Badge_Unit, (Object) null))
      this.Badge_Unit.SetActive(false);
    if (Object.op_Inequality((Object) this.Badge_Gift, (Object) null))
      this.Badge_Gift.SetActive(false);
    if (!Object.op_Inequality((Object) this.Badge_DailyMission, (Object) null))
      return;
    this.Badge_DailyMission.SetActive(false);
  }

  private void Update()
  {
    if (this.IsInput && Input.GetMouseButtonDown(0))
    {
      Vector2 vector2;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(this.mRectTransform, Vector2.op_Implicit(Input.mousePosition), (Camera) null, ref vector2);
      Rect rect = this.mRectTransform.rect;
      if (!((Rect) ref rect).Contains(vector2))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    if (!Object.op_Inequality((Object) instanceDirect, (Object) null))
      return;
    bool flag1 = false;
    if (!instanceDirect.CheckBusyBadges(GameManager.BadgeTypes.Unit | GameManager.BadgeTypes.UnitUnlock) && Object.op_Inequality((Object) this.Badge_Unit, (Object) null))
    {
      this.Badge_Unit.SetActive(instanceDirect.CheckBadges(GameManager.BadgeTypes.Unit | GameManager.BadgeTypes.UnitUnlock));
      if (this.Badge_Unit.GetActive())
        flag1 = true;
    }
    if (!instanceDirect.CheckBusyBadges(GameManager.BadgeTypes.GiftBox) && Object.op_Inequality((Object) this.Badge_Gift, (Object) null))
    {
      this.Badge_Gift.SetActive(instanceDirect.CheckBadges(GameManager.BadgeTypes.GiftBox));
      if (this.Badge_Gift.GetActive())
        flag1 = true;
    }
    if (Object.op_Inequality((Object) this.Badge_DailyMission, (Object) null))
    {
      bool flag2 = false;
      List<TrophyState> trophyStatesList = instanceDirect.Player.TrophyData.TrophyStatesList;
      for (int index = 0; index < trophyStatesList.Count; ++index)
      {
        if (trophyStatesList[index].Param.IsShowBadge(trophyStatesList[index]))
        {
          flag2 = true;
          break;
        }
      }
      if (!flag2 && instanceDirect.Player.IsCanGetRewardTrophyStarMission())
        flag2 = true;
      if (this.Badge_DailyMission.GetActive() != flag2)
        this.Badge_DailyMission.SetActive(flag2);
      if (flag2)
        flag1 = true;
    }
    if (Object.op_Inequality((Object) this.Badge_MenuOpen, (Object) null))
      this.Badge_MenuOpen.SetActive(flag1);
    if (!Object.op_Inequality((Object) this.Badge_MenuClose, (Object) null))
      return;
    this.Badge_MenuClose.SetActive(flag1);
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
