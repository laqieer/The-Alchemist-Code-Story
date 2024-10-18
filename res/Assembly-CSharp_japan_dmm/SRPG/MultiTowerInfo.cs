// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "マルチタワートップ", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "階層選択", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "部屋一覧から階層決定", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "部屋一覧から閉じた", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "再描画(階層状況反映)", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(10, "部屋一覧から階層選択完了", FlowNode.PinTypes.Output, 10)]
  public class MultiTowerInfo : MonoBehaviour, IFlowInterface
  {
    private const int PININ_MULTI_TOWER_TOP = 0;
    private const int PININ_SELECT_FLOOR = 1;
    private const int PININ_SELECT_FLOOR_FROM_LIST = 2;
    private const int PININ_CLOSE_LIST = 3;
    private const int PININ_REFRESH = 4;
    private const int PINOUT_SELECTED_FLOOR = 10;
    private readonly int OFFSET = 2;
    public ScrollAutoFit AutoFit;
    public GameObject QuestInfo;
    public Button ScrollUp;
    public Button ScrollDw;
    public SRPG_Button Make;
    public SRPG_Button OK;
    public RectTransform ListRect;
    public ScrollListController ScrollList;
    public RectTransform Cursor;
    private bool IsMultiTowerTop;
    private int max_tower_floor_num;
    public Text RewardText;
    public GameObject ItemRoot;
    public GameObject ArtifactRoot;
    public GameObject CoinRoot;
    public Text QuestAP;
    public Text ChangeQuestAP;
    public GameObject PassButton;
    private bool mIsActivatePinAfterSelectedFloor;

    public List<RectTransform> ItemList => this.ScrollList.m_ItemList;

    public bool MultiTowerTop => this.IsMultiTowerTop;

    private void Start()
    {
    }

    public void Init()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num = !this.IsMultiTowerTop ? GlobalVars.SelectedMultiTowerFloor : instance.GetMTChallengeFloor();
      this.Setup(num);
      this.ScrollToFloorQuick(this.ConvertFloor(num));
      this.max_tower_floor_num = instance.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID).Count;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      this.AutoFit.OnScrollStop.AddListener(new UnityAction((object) this, __methodptr(OnScrollStop)));
      // ISSUE: method pointer
      this.AutoFit.OnScrollBegin.AddListener(new UnityAction((object) this, __methodptr(OnScrollStart)));
    }

    private void Update()
    {
      this.FocusUpdate();
      float itemScale = this.AutoFit.ItemScale;
      int num = Mathf.Abs(Mathf.RoundToInt((this.AutoFit.content.anchoredPosition.y - itemScale * (float) this.OFFSET) / itemScale) + 1 - this.OFFSET);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollUp, (UnityEngine.Object) null))
        ((Selectable) this.ScrollUp).interactable = num < this.max_tower_floor_num;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollDw, (UnityEngine.Object) null))
        return;
      ((Selectable) this.ScrollDw).interactable = num > 1;
    }

    private void OnScrollStop()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null))
        return;
      float itemScale = this.AutoFit.ItemScale;
      this.Setup(Mathf.Abs(Mathf.RoundToInt((this.AutoFit.content.anchoredPosition.y - itemScale * (float) this.OFFSET) / itemScale) + 1 - this.OFFSET));
    }

    private void Setup(int idx)
    {
      if (this.mIsActivatePinAfterSelectedFloor)
      {
        this.mIsActivatePinAfterSelectedFloor = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, idx);
      if (mtFloorParam == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestInfo, (UnityEngine.Object) null))
        return;
      int mtChallengeFloor = instance.GetMTChallengeFloor();
      int mtClearedMaxFloor = instance.GetMTClearedMaxFloor();
      int num1 = int.MaxValue;
      if (!this.IsMultiTowerTop)
        num1 = this.GetCanCharengeFloor();
      this.SetButtonIntractable(((int) mtFloorParam.floor <= mtClearedMaxFloor || (int) mtFloorParam.floor == mtChallengeFloor) && (int) mtFloorParam.floor <= num1);
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(this.QuestInfo, (MultiTowerFloorParam) null);
      if (dataOfClass != null && (int) mtFloorParam.floor == (int) dataOfClass.floor)
        return;
      DebugUtility.Log("設定" + mtFloorParam.name);
      QuestParam questParam = mtFloorParam.GetQuestParam();
      DataSource.Bind<MultiTowerFloorParam>(this.QuestInfo, mtFloorParam);
      DataSource.Bind<QuestParam>(this.QuestInfo, questParam);
      GameParameter.UpdateAll(this.QuestInfo);
      MultiTowerQuestInfo component = this.QuestInfo.GetComponent<MultiTowerQuestInfo>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.Refresh();
      GlobalVars.SelectedMultiTowerID = mtFloorParam.tower_id;
      GlobalVars.SelectedQuestID = questParam.iname;
      GlobalVars.SelectedMultiTowerFloor = (int) mtFloorParam.floor;
      int num2 = questParam.RequiredApWithPlayerLv(instance.Player.Lv);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestAP, (UnityEngine.Object) null))
        this.QuestAP.text = num2.ToString();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChangeQuestAP, (UnityEngine.Object) null))
        return;
      this.ChangeQuestAP.text = num2.ToString();
    }

    private void _SetScrollTo(float pos)
    {
      pos = this.Clamp(pos);
      this.AutoFit.SetScrollTo(pos);
    }

    private void _SetScrollToQuick(float pos)
    {
      pos = this.Clamp(pos);
      Vector2 anchoredPosition = this.ListRect.anchoredPosition;
      anchoredPosition.y = pos;
      this.ListRect.anchoredPosition = anchoredPosition;
    }

    public float Clamp(float pos)
    {
      Rect rect1 = this.AutoFit.viewport.rect;
      float y = ((Rect) ref rect1).size.y;
      Rect rect2 = this.AutoFit.rect;
      float num1 = (float) ((double) ((Rect) ref rect2).size.y * 0.5 - (double) y * 0.5);
      float num2 = 1f;
      float num3 = num1;
      float num4 = num1 + y - this.ListRect.sizeDelta.y;
      float num5 = num3 * num2;
      float num6 = num4 * num2;
      float num7 = Mathf.Min(num5, num6);
      float num8 = Mathf.Max(num5, num6);
      return Mathf.Clamp(pos, num7, num8);
    }

    public void OnScrollUp(int val) => this.ScrollToFloor(this.AutoFit.GetCurrent() - val);

    public void OnScrollDw(int val) => this.ScrollToFloor(this.AutoFit.GetCurrent() + val);

    public void OnCurrentScroll()
    {
      this.ScrollToFloor(this.ConvertFloor(MonoSingleton<GameManager>.Instance.GetMTChallengeFloor()));
    }

    private void ScrollToFloor(int index)
    {
      this.SetButtonIntractable(false);
      this._SetScrollTo((float) index * this.AutoFit.ItemScale + this.AutoFit.Offset);
    }

    private void ScrollToFloorQuick(int index)
    {
      this._SetScrollToQuick((float) index * this.AutoFit.ItemScale + this.AutoFit.Offset);
    }

    public void OnTapFloor(int floor) => this.ScrollToFloor(this.ConvertFloor(floor));

    private void FocusUpdate()
    {
      Rect rect = this.Cursor.rect;
      ((Rect) ref rect).center = new Vector2(0.0f, (float) ((double) this.AutoFit.ItemScale * 3.0 - (double) this.AutoFit.ItemScale * 0.5));
      ((Rect) ref rect).size = this.Cursor.sizeDelta;
      foreach (MultiTowerFloorInfo multiTowerFloorInfo in this.ItemList.ConvertAll<MultiTowerFloorInfo>((Converter<RectTransform, MultiTowerFloorInfo>) (item => ((Component) item).GetComponent<MultiTowerFloorInfo>())))
      {
        if (((Component) multiTowerFloorInfo).gameObject.activeInHierarchy)
        {
          Vector2 anchoredPosition = multiTowerFloorInfo.rectTransform.anchoredPosition;
          anchoredPosition.y = this.ListRect.anchoredPosition.y + anchoredPosition.y;
          multiTowerFloorInfo.OnFocus(((Rect) ref rect).Contains(anchoredPosition));
        }
      }
    }

    public int ConvertFloor(int floor) => this.OFFSET + 1 - floor;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.IsMultiTowerTop = true;
          break;
        case 1:
          this.IsMultiTowerTop = false;
          break;
        case 2:
          if (GlobalVars.SelectedMultiTowerFloor <= 0)
            return;
          this.mIsActivatePinAfterSelectedFloor = true;
          this.Setup(GlobalVars.SelectedMultiTowerFloor);
          return;
        case 3:
          this.OnScrollStop();
          return;
        case 4:
          GlobalVars.SelectedMultiTowerFloor = MonoSingleton<GameManager>.Instance.GetMTChallengeFloor();
          if (GlobalVars.SelectedMultiTowerFloor <= 0)
            return;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ScrollList))
            this.ScrollList.Refresh();
          this.OnTapFloor(GlobalVars.SelectedMultiTowerFloor);
          return;
      }
      this.PassButton.SetActive(this.IsMultiTowerTop);
    }

    public void OnScrollStart() => this.SetButtonIntractable(false);

    public void SetButtonIntractable(bool intaractable)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Make, (UnityEngine.Object) null))
        ((Selectable) this.Make).interactable = intaractable;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OK, (UnityEngine.Object) null))
        return;
      ((Selectable) this.OK).interactable = intaractable;
    }

    public int GetCanCharengeFloor()
    {
      List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
      int canCharengeFloor = int.MaxValue;
      for (int index = 0; index < roomPlayerList.Count; ++index)
      {
        JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
        if (canCharengeFloor > photonPlayerParam.mtChallengeFloor)
          canCharengeFloor = photonPlayerParam.mtChallengeFloor;
      }
      return canCharengeFloor;
    }
  }
}
