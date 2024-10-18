﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusViewPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class VersusViewPlayerInfo : MonoBehaviour
  {
    public GameObject EmptyObj;
    public GameObject ValidObj;
    public GameObject LeaderUnit;
    public GameObject ReadyObj;
    public GameObject Award;
    public Text Name;
    public Text Lv;
    public Text Total;

    private void Start()
    {
    }

    public void Refresh()
    {
      JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.SearchRoom(JSON_MyPhotonRoomParam.Parse(MonoSingleton<GameManager>.Instance.AudienceRoom.json).roomid).json);
      JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(((Component) this).gameObject, (JSON_MyPhotonPlayerParam) null);
      if (dataOfClass != null)
      {
        if (Object.op_Inequality((Object) this.EmptyObj, (Object) null))
          this.EmptyObj.SetActive(false);
        if (Object.op_Inequality((Object) this.ValidObj, (Object) null))
          this.ValidObj.SetActive(true);
        if (Object.op_Inequality((Object) this.LeaderUnit, (Object) null))
        {
          if (myPhotonRoomParam.draft_type == 1 && !string.IsNullOrEmpty(dataOfClass.support_unit))
          {
            Json_Unit jsonObject = JSONParser.parseJSONObject<Json_Unit>(dataOfClass.support_unit);
            if (jsonObject != null)
            {
              UnitData data = new UnitData();
              data.Deserialize(jsonObject);
              DataSource.Bind<UnitData>(this.LeaderUnit.gameObject, data);
            }
          }
          else if (dataOfClass.units != null)
          {
            dataOfClass.SetupUnits();
            DataSource.Bind<UnitData>(this.LeaderUnit, dataOfClass.units[0].unit);
          }
        }
        if (Object.op_Inequality((Object) this.Name, (Object) null))
          this.Name.text = dataOfClass.playerName;
        if (Object.op_Inequality((Object) this.Lv, (Object) null))
          this.Lv.text = dataOfClass.playerLevel.ToString();
        if (Object.op_Inequality((Object) this.Total, (Object) null))
          this.Total.text = dataOfClass.totalStatus.ToString();
        if (Object.op_Inequality((Object) this.ReadyObj, (Object) null))
          this.ReadyObj.SetActive(dataOfClass.state != 4);
        if (Object.op_Inequality((Object) this.Award, (Object) null))
        {
          this.Award.gameObject.SetActive(false);
          this.Award.gameObject.SetActive(true);
        }
        GameParameter.UpdateAll(((Component) this).gameObject);
      }
      else
      {
        if (Object.op_Inequality((Object) this.EmptyObj, (Object) null))
          this.EmptyObj.SetActive(true);
        if (Object.op_Inequality((Object) this.ValidObj, (Object) null))
          this.ValidObj.SetActive(false);
        if (!Object.op_Inequality((Object) this.ReadyObj, (Object) null))
          return;
        this.ReadyObj.SetActive(false);
      }
    }
  }
}
