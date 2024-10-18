// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerFloorInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class VersusTowerFloorInfo : MonoBehaviour
  {
    private readonly int ScrollMargin = 3;
    private readonly float ScrollSpd = 50f;
    public GameObject Keytemplate;
    public GameObject parent;
    public GameObject playerInfo;
    public ScrollListController ScrollCtrl;
    public Button currentButton;
    public ScrollRect Scroll;
    private float AutoScrollGoal;
    private bool AutoScroll;

    private void Start()
    {
      if (Object.op_Inequality((Object) this.currentButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.currentButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickScroll)));
      }
      this.Refresh();
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.Keytemplate, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int versusTowerKey = instance.Player.VersusTowerKey;
      VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam();
      if (versusTowerParam == null)
        return;
      int num = 0;
      while (num < (int) versusTowerParam.RankupNum)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.Keytemplate);
        if (!Object.op_Equality((Object) gameObject, (Object) null))
        {
          gameObject.SetActive(true);
          if (Object.op_Inequality((Object) this.parent, (Object) null))
            gameObject.transform.SetParent(this.parent.transform, false);
          Transform transform1 = gameObject.transform.Find("on");
          Transform transform2 = gameObject.transform.Find("off");
          if (Object.op_Inequality((Object) transform1, (Object) null))
            ((Component) transform1).gameObject.SetActive(versusTowerKey > 0);
          if (Object.op_Inequality((Object) transform2, (Object) null))
            ((Component) transform2).gameObject.SetActive(versusTowerKey <= 0);
        }
        ++num;
        --versusTowerKey;
      }
      this.Keytemplate.SetActive(false);
    }

    public void Update()
    {
      if (this.AutoScroll && Object.op_Inequality((Object) this.ScrollCtrl, (Object) null) && this.ScrollCtrl.MovePos(this.AutoScrollGoal, this.ScrollSpd))
      {
        this.AutoScroll = false;
        if (Object.op_Inequality((Object) this.Scroll, (Object) null))
          this.Scroll.inertia = true;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "STOP_CURRENT_SCROLL");
      }
      if (!Object.op_Inequality((Object) this.ScrollCtrl, (Object) null) || !Object.op_Inequality((Object) this.playerInfo, (Object) null))
        return;
      bool flag = false;
      List<RectTransform> itemList = this.ScrollCtrl.ItemList;
      int versusTowerFloor = MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor;
      for (int index = 0; index < itemList.Count; ++index)
      {
        VersusTowerFloor component = ((Component) itemList[index]).gameObject.GetComponent<VersusTowerFloor>();
        if (Object.op_Inequality((Object) component, (Object) null) && component.Floor == versusTowerFloor)
        {
          flag = true;
          component.SetPlayerObject(this.playerInfo);
          break;
        }
      }
      if (flag)
        return;
      this.playerInfo.SetActive(false);
    }

    private void OnClickScroll()
    {
      if (!Object.op_Inequality((Object) this.ScrollCtrl, (Object) null))
        return;
      int num = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - this.ScrollMargin, 0);
      float itemScale = this.ScrollCtrl.ItemScale;
      this.AutoScrollGoal = Mathf.Min((float) -((double) num * (double) itemScale + (double) itemScale * 0.5), 0.0f);
      this.AutoScroll = true;
      if (Object.op_Inequality((Object) this.Scroll, (Object) null))
        this.Scroll.inertia = false;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "START_CURRENT_SCROLL");
    }
  }
}
