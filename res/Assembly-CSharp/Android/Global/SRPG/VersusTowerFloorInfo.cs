// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerFloorInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.currentButton != (UnityEngine.Object) null)
        this.currentButton.onClick.AddListener(new UnityAction(this.OnClickScroll));
      this.Refresh();
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.Keytemplate == (UnityEngine.Object) null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int versusTowerKey = instance.Player.VersusTowerKey;
      VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam(-1);
      if (versusTowerParam == null)
        return;
      int num = 0;
      while (num < (int) versusTowerParam.RankupNum)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Keytemplate);
        if (!((UnityEngine.Object) gameObject == (UnityEngine.Object) null))
        {
          gameObject.SetActive(true);
          if ((UnityEngine.Object) this.parent != (UnityEngine.Object) null)
            gameObject.transform.SetParent(this.parent.transform, false);
          Transform child1 = gameObject.transform.FindChild("on");
          Transform child2 = gameObject.transform.FindChild("off");
          if ((UnityEngine.Object) child1 != (UnityEngine.Object) null)
            child1.gameObject.SetActive(versusTowerKey > 0);
          if ((UnityEngine.Object) child2 != (UnityEngine.Object) null)
            child2.gameObject.SetActive(versusTowerKey <= 0);
        }
        ++num;
        --versusTowerKey;
      }
      this.Keytemplate.SetActive(false);
    }

    public void Update()
    {
      if (this.AutoScroll && (UnityEngine.Object) this.ScrollCtrl != (UnityEngine.Object) null && this.ScrollCtrl.MovePos(this.AutoScrollGoal, this.ScrollSpd))
      {
        this.AutoScroll = false;
        if ((UnityEngine.Object) this.Scroll != (UnityEngine.Object) null)
          this.Scroll.inertia = true;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "STOP_CURRENT_SCROLL");
      }
      if (!((UnityEngine.Object) this.ScrollCtrl != (UnityEngine.Object) null) || !((UnityEngine.Object) this.playerInfo != (UnityEngine.Object) null))
        return;
      bool flag = false;
      List<RectTransform> itemList = this.ScrollCtrl.ItemList;
      int versusTowerFloor = MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor;
      for (int index = 0; index < itemList.Count; ++index)
      {
        VersusTowerFloor component = itemList[index].gameObject.GetComponent<VersusTowerFloor>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && component.Floor == versusTowerFloor)
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
      if (!((UnityEngine.Object) this.ScrollCtrl != (UnityEngine.Object) null))
        return;
      int num = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - this.ScrollMargin, 0);
      float itemScale = this.ScrollCtrl.ItemScale;
      this.AutoScrollGoal = Mathf.Min((float) -((double) num * (double) itemScale + (double) itemScale * 0.5), 0.0f);
      this.AutoScroll = true;
      if ((UnityEngine.Object) this.Scroll != (UnityEngine.Object) null)
        this.Scroll.inertia = false;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "START_CURRENT_SCROLL");
    }
  }
}
