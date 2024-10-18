// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerMatchInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Search", FlowNode.PinTypes.Input, 2)]
  public class VersusTowerMatchInfo : MonoBehaviour, IFlowInterface
  {
    private readonly string PVP_URL = "notice/detail/pvp/tower";
    public GameObject template;
    public GameObject winbonus;
    public GameObject keyrateup;
    public GameObject parent;
    public GameObject keyinfo;
    public GameObject keyname;
    public GameObject lastfloor;
    public UnityEngine.UI.Text nowKey;
    public UnityEngine.UI.Text maxKey;
    public UnityEngine.UI.Text floor;
    public UnityEngine.UI.Text bonusRate;
    public UnityEngine.UI.Text winCnt;
    public UnityEngine.UI.Text endAt;
    public Button detailBtn;

    private void Start()
    {
      if ((UnityEngine.Object) this.template == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.detailBtn != (UnityEngine.Object) null)
        this.detailBtn.onClick.AddListener(new UnityAction(this.OnClickDetail));
      this.RefreshData();
    }

    private void RefreshData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      List<GameObject> gameObjectList = new List<GameObject>();
      int versusTowerKey = player.VersusTowerKey;
      VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam(-1);
      if (versusTowerParam != null)
      {
        int num = 0;
        while (num < (int) versusTowerParam.RankupNum)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.template);
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
            gameObjectList.Add(gameObject);
          }
          ++num;
          --versusTowerKey;
        }
        this.template.SetActive(false);
        if ((UnityEngine.Object) this.nowKey != (UnityEngine.Object) null)
          this.nowKey.text = GameUtility.HalfNum2FullNum(player.VersusTowerKey.ToString());
        if ((UnityEngine.Object) this.maxKey != (UnityEngine.Object) null)
          this.maxKey.text = GameUtility.HalfNum2FullNum(versusTowerParam.RankupNum.ToString());
        if ((UnityEngine.Object) this.floor != (UnityEngine.Object) null)
          this.floor.text = player.VersusTowerFloor.ToString();
        if ((UnityEngine.Object) this.winbonus != (UnityEngine.Object) null)
          this.winbonus.SetActive(player.VersusTowerWinBonus > 1);
        if ((UnityEngine.Object) this.keyrateup != (UnityEngine.Object) null)
          this.keyrateup.SetActive(player.VersusTowerWinBonus > 0 && (int) versusTowerParam.RankupNum > 0);
        if ((UnityEngine.Object) this.bonusRate != (UnityEngine.Object) null && player.VersusTowerWinBonus > 0 && (int) versusTowerParam.WinNum > 0)
          this.bonusRate.text = (((int) versusTowerParam.WinNum + (int) versusTowerParam.BonusNum) / (int) versusTowerParam.WinNum).ToString();
        if ((UnityEngine.Object) this.winCnt != (UnityEngine.Object) null)
          this.winCnt.text = player.VersusTowerWinBonus.ToString();
        if ((UnityEngine.Object) this.endAt != (UnityEngine.Object) null)
        {
          DateTime dateTime = TimeManager.FromUnixTime(instance.VersusTowerMatchEndAt);
          this.endAt.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_END_AT"), (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute);
        }
        if ((UnityEngine.Object) this.keyinfo != (UnityEngine.Object) null)
          this.keyinfo.SetActive((int) versusTowerParam.RankupNum != 0);
        if ((UnityEngine.Object) this.keyname != (UnityEngine.Object) null)
          this.keyname.SetActive((int) versusTowerParam.RankupNum != 0);
        if (!((UnityEngine.Object) this.lastfloor != (UnityEngine.Object) null))
          return;
        this.lastfloor.SetActive((int) versusTowerParam.RankupNum == 0 && instance.VersusTowerMatchBegin);
      }
      else
      {
        if (!((UnityEngine.Object) this.lastfloor != (UnityEngine.Object) null))
          return;
        this.lastfloor.SetActive(false);
      }
    }

    private void OnClickDetail()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Network.NewsHost);
      stringBuilder.Append(this.PVP_URL);
      FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.MULTI_VERSUS_TOWER_DETAIL"));
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", stringBuilder.ToString());
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "TOWERMATCH_DETAIL");
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Search();
    }

    private void Search()
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      int selectedMultiPlayRoomId = GlobalVars.SelectedMultiPlayRoomID;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      instance1.AudienceRoom = instance2.SearchRoom(selectedMultiPlayRoomId);
      if (instance1.AudienceRoom != null)
      {
        JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(instance1.AudienceRoom.json);
        if (myPhotonRoomParam != null && myPhotonRoomParam.audience == 0)
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "AudienceDisable");
        else if (instance1.AudienceRoom.battle)
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "AlreadyStartFriendMode");
        else
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FindRoom");
      }
      else
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "NotFindRoom");
    }
  }
}
