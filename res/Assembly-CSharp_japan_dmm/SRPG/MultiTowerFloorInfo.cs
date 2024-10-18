﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerFloorInfo
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
  public class MultiTowerFloorInfo : MonoBehaviour
  {
    public GameObject Lock;
    public GameObject Clear;
    public Text Floor;
    public SRPG_Button Root;
    [SerializeField]
    private GameObject mBody;
    private RectTransform mBodyTransform;
    public RectTransform rectTransform;
    [SerializeField]
    private ImageArray[] mBanner;
    public CanvasRenderer Source;
    private Color UnknownColor = new Color(0.4f, 0.4f, 0.4f, 1f);
    public MultiTowerInfo MultiTower;
    public GameObject[] NowCharengeFloor;

    public void Start()
    {
      if (Object.op_Inequality((Object) this.mBody, (Object) null))
        this.mBodyTransform = this.mBody.GetComponent<RectTransform>();
      this.rectTransform = ((Component) this).GetComponent<RectTransform>();
      // ISSUE: method pointer
      ((UnityEvent) this.Root.onClick).AddListener(new UnityAction((object) this, __methodptr(SetFloor)));
    }

    public void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(((Component) this).gameObject, (MultiTowerFloorParam) null);
      int mtChallengeFloor = instance.GetMTChallengeFloor();
      int mtClearedMaxFloor = instance.GetMTClearedMaxFloor();
      for (int index = 0; index < this.NowCharengeFloor.Length; ++index)
        this.NowCharengeFloor[index].SetActive(false);
      if (this.MultiTower.MultiTowerTop)
      {
        this.SetFloorInfo(dataOfClass, mtChallengeFloor, mtClearedMaxFloor);
      }
      else
      {
        int min_floor = int.MaxValue;
        if (dataOfClass != null)
        {
          List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
          for (int index = 0; index < roomPlayerList.Count; ++index)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
            if (min_floor > photonPlayerParam.mtChallengeFloor)
              min_floor = photonPlayerParam.mtChallengeFloor;
            if (this.NowCharengeFloor.Length > photonPlayerParam.playerIndex - 1 && photonPlayerParam.playerIndex > 0)
              this.NowCharengeFloor[photonPlayerParam.playerIndex - 1].SetActive((int) dataOfClass.floor == photonPlayerParam.mtChallengeFloor);
          }
        }
        this.SetFloorInfo(dataOfClass, mtChallengeFloor, mtClearedMaxFloor, min_floor);
      }
    }

    private void SetVisible(MultiTowerFloorInfo.Type type)
    {
      GameUtility.SetGameObjectActive(this.Clear, false);
      GameUtility.SetGameObjectActive(this.Lock, false);
      GameUtility.SetGameObjectActive((Component) this.Floor, false);
      switch (type)
      {
        case MultiTowerFloorInfo.Type.Locked:
          this.Source.SetColor(Color.gray);
          this.mBanner[0].ImageIndex = 1;
          this.mBanner[1].ImageIndex = 1;
          GameUtility.SetGameObjectActive(this.Lock, true);
          GameUtility.SetGameObjectActive((Component) this.Floor, true);
          break;
        case MultiTowerFloorInfo.Type.Cleared:
          this.Source.SetColor(Color.white);
          this.mBanner[0].ImageIndex = 0;
          this.mBanner[1].ImageIndex = 0;
          GameUtility.SetGameObjectActive(this.Clear, true);
          GameUtility.SetGameObjectActive((Component) this.Floor, true);
          break;
        case MultiTowerFloorInfo.Type.Current:
          this.Source.SetColor(Color.white);
          this.mBanner[0].ImageIndex = 0;
          this.mBanner[1].ImageIndex = 0;
          GameUtility.SetGameObjectActive((Component) this.Floor, true);
          break;
        case MultiTowerFloorInfo.Type.Unknown:
          this.Source.SetColor(this.UnknownColor);
          this.mBanner[0].ImageIndex = 1;
          this.mBanner[1].ImageIndex = 1;
          break;
        case MultiTowerFloorInfo.Type.PartnerLocked:
          this.Source.SetColor(Color.gray);
          this.mBanner[0].ImageIndex = 1;
          this.mBanner[1].ImageIndex = 1;
          GameUtility.SetGameObjectActive((Component) this.Floor, true);
          break;
      }
    }

    public void OnFocus(bool value)
    {
      if (!Object.op_Inequality((Object) this.mBodyTransform, (Object) null))
        return;
      if (value)
        ((Transform) this.mBodyTransform).localScale = new Vector3(1f, 1f, 1f);
      else
        ((Transform) this.mBodyTransform).localScale = new Vector3(0.9f, 0.9f, 1f);
    }

    public void SetFloor()
    {
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(((Component) this).gameObject, (MultiTowerFloorParam) null);
      if (dataOfClass == null)
        return;
      this.MultiTower.OnTapFloor((int) dataOfClass.floor);
    }

    public void SetFloorInfo(
      MultiTowerFloorParam param,
      int challenge,
      int cleared,
      int min_floor = 2147483647)
    {
      if (param != null)
      {
        if (Object.op_Inequality((Object) this.Floor, (Object) null))
        {
          ((Component) this.Floor).gameObject.SetActive(true);
          this.Floor.text = param.floor.ToString() + "!";
        }
        if ((int) param.floor > challenge)
          this.SetVisible(MultiTowerFloorInfo.Type.Locked);
        else if ((int) param.floor > min_floor)
          this.SetVisible(MultiTowerFloorInfo.Type.PartnerLocked);
        else if ((int) param.floor <= cleared)
          this.SetVisible(MultiTowerFloorInfo.Type.Cleared);
        else
          this.SetVisible(MultiTowerFloorInfo.Type.Current);
      }
      else
        this.SetVisible(MultiTowerFloorInfo.Type.Unknown);
    }

    private enum Type
    {
      Locked,
      Cleared,
      Current,
      Unknown,
      PartnerLocked,
      TypeEnd,
    }
  }
}
