// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerFloorInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiTowerFloorInfo : MonoBehaviour
  {
    private Color UnknownColor = new Color(0.4f, 0.4f, 0.4f, 1f);
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
    public MultiTowerInfo MultiTower;
    public GameObject[] NowCharengeFloor;

    public void Start()
    {
      if ((UnityEngine.Object) this.mBody != (UnityEngine.Object) null)
        this.mBodyTransform = this.mBody.GetComponent<RectTransform>();
      this.rectTransform = this.GetComponent<RectTransform>();
      this.Root.onClick.AddListener(new UnityAction(this.SetFloor));
    }

    public void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(this.gameObject, (MultiTowerFloorParam) null);
      int mtChallengeFloor = instance.GetMTChallengeFloor();
      int mtClearedMaxFloor = instance.GetMTClearedMaxFloor();
      for (int index = 0; index < this.NowCharengeFloor.Length; ++index)
        this.NowCharengeFloor[index].SetActive(false);
      if (this.MultiTower.MultiTowerTop)
      {
        this.SetFloorInfo(dataOfClass, mtChallengeFloor, mtClearedMaxFloor, int.MaxValue);
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
      if (!((UnityEngine.Object) this.mBodyTransform != (UnityEngine.Object) null))
        return;
      if (value)
        this.mBodyTransform.localScale = new Vector3(1f, 1f, 1f);
      else
        this.mBodyTransform.localScale = new Vector3(0.9f, 0.9f, 1f);
    }

    public void SetFloor()
    {
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(this.gameObject, (MultiTowerFloorParam) null);
      if (dataOfClass == null)
        return;
      this.MultiTower.OnTapFloor((int) dataOfClass.floor);
    }

    public void SetFloorInfo(MultiTowerFloorParam param, int challenge, int cleared, int min_floor = 2147483647)
    {
      if (param != null)
      {
        if ((UnityEngine.Object) this.Floor != (UnityEngine.Object) null)
        {
          this.Floor.gameObject.SetActive(true);
          this.Floor.text = ((int) param.floor).ToString() + "!";
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
