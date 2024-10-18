// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "マルチタワートップ", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "階層選択", FlowNode.PinTypes.Input, 0)]
  public class MultiTowerInfo : MonoBehaviour, IFlowInterface
  {
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

    public List<RectTransform> ItemList
    {
      get
      {
        return this.ScrollList.m_ItemList;
      }
    }

    public bool MultiTowerTop
    {
      get
      {
        return this.IsMultiTowerTop;
      }
    }

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
      if (!((UnityEngine.Object) this.AutoFit != (UnityEngine.Object) null))
        return;
      this.AutoFit.OnScrollStop.AddListener(new UnityAction(this.OnScrollStop));
      this.AutoFit.OnScrollBegin.AddListener(new UnityAction(this.OnScrollStart));
    }

    private void Update()
    {
      this.FocusUpdate();
      float itemScale = this.AutoFit.ItemScale;
      int num = Mathf.Abs(Mathf.RoundToInt((this.AutoFit.content.anchoredPosition.y - itemScale * (float) this.OFFSET) / itemScale) + 1 - this.OFFSET);
      if ((UnityEngine.Object) this.ScrollUp != (UnityEngine.Object) null)
        this.ScrollUp.interactable = num < this.max_tower_floor_num;
      if (!((UnityEngine.Object) this.ScrollDw != (UnityEngine.Object) null))
        return;
      this.ScrollDw.interactable = num > 1;
    }

    private void OnScrollStop()
    {
      if ((UnityEngine.Object) this.AutoFit == (UnityEngine.Object) null)
        return;
      float itemScale = this.AutoFit.ItemScale;
      this.Setup(Mathf.Abs(Mathf.RoundToInt((this.AutoFit.content.anchoredPosition.y - itemScale * (float) this.OFFSET) / itemScale) + 1 - this.OFFSET));
    }

    private void Setup(int idx)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, idx);
      if (mtFloorParam == null || !((UnityEngine.Object) this.QuestInfo != (UnityEngine.Object) null))
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
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.Refresh();
      GlobalVars.SelectedMultiTowerID = mtFloorParam.tower_id;
      GlobalVars.SelectedQuestID = questParam.iname;
      GlobalVars.SelectedMultiTowerFloor = (int) mtFloorParam.floor;
      int num2 = questParam.RequiredApWithPlayerLv(instance.Player.Lv, true);
      if ((UnityEngine.Object) this.QuestAP != (UnityEngine.Object) null)
        this.QuestAP.text = num2.ToString();
      if (!((UnityEngine.Object) this.ChangeQuestAP != (UnityEngine.Object) null))
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
      float y = this.AutoFit.viewport.rect.size.y;
      float num1 = (float) ((double) this.AutoFit.rect.size.y * 0.5 - (double) y * 0.5);
      float num2 = 1f;
      float num3 = num1;
      float num4 = num1 + y - this.ListRect.sizeDelta.y;
      float a = num3 * num2;
      float b = num4 * num2;
      float min = Mathf.Min(a, b);
      float max = Mathf.Max(a, b);
      return Mathf.Clamp(pos, min, max);
    }

    public void OnScrollUp(int val)
    {
      this.ScrollToFloor(this.AutoFit.GetCurrent() - val);
    }

    public void OnScrollDw(int val)
    {
      this.ScrollToFloor(this.AutoFit.GetCurrent() + val);
    }

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

    public void OnTapFloor(int index)
    {
      this.ScrollToFloor(this.ConvertFloor(index));
    }

    private void FocusUpdate()
    {
      Rect rect = this.Cursor.rect;
      rect.center = new Vector2(0.0f, (float) ((double) this.AutoFit.ItemScale * 3.0 - (double) this.AutoFit.ItemScale * 0.5));
      rect.size = this.Cursor.sizeDelta;
      using (List<MultiTowerFloorInfo>.Enumerator enumerator = this.ItemList.ConvertAll<MultiTowerFloorInfo>((Converter<RectTransform, MultiTowerFloorInfo>) (item => item.GetComponent<MultiTowerFloorInfo>())).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MultiTowerFloorInfo current = enumerator.Current;
          if (current.gameObject.activeInHierarchy)
          {
            Vector2 anchoredPosition = current.rectTransform.anchoredPosition;
            anchoredPosition.y = this.ListRect.anchoredPosition.y + anchoredPosition.y;
            current.OnFocus(rect.Contains(anchoredPosition));
          }
        }
      }
    }

    public int ConvertFloor(int floor)
    {
      return this.OFFSET + 1 - floor;
    }

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
      }
      this.PassButton.SetActive(this.IsMultiTowerTop);
    }

    public void OnScrollStart()
    {
      this.SetButtonIntractable(false);
    }

    public void SetButtonIntractable(bool intaractable)
    {
      if ((UnityEngine.Object) this.Make != (UnityEngine.Object) null)
        this.Make.interactable = intaractable;
      if (!((UnityEngine.Object) this.OK != (UnityEngine.Object) null))
        return;
      this.OK.interactable = intaractable;
    }

    public int GetCanCharengeFloor()
    {
      List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
      int num = int.MaxValue;
      for (int index = 0; index < roomPlayerList.Count; ++index)
      {
        JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
        if (num > photonPlayerParam.mtChallengeFloor)
          num = photonPlayerParam.mtChallengeFloor;
      }
      return num;
    }
  }
}
