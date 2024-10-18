// Decompiled with JetBrains decompiler
// Type: SRPG.VersusViewPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(this.gameObject, (JSON_MyPhotonPlayerParam) null);
      if (dataOfClass != null)
      {
        if ((UnityEngine.Object) this.EmptyObj != (UnityEngine.Object) null)
          this.EmptyObj.SetActive(false);
        if ((UnityEngine.Object) this.ValidObj != (UnityEngine.Object) null)
          this.ValidObj.SetActive(true);
        if ((UnityEngine.Object) this.LeaderUnit != (UnityEngine.Object) null && dataOfClass.units != null)
        {
          dataOfClass.SetupUnits();
          DataSource.Bind<UnitData>(this.LeaderUnit, dataOfClass.units[0].unit);
        }
        if ((UnityEngine.Object) this.Name != (UnityEngine.Object) null)
          this.Name.text = dataOfClass.playerName;
        if ((UnityEngine.Object) this.Lv != (UnityEngine.Object) null)
          this.Lv.text = dataOfClass.playerLevel.ToString();
        if ((UnityEngine.Object) this.Total != (UnityEngine.Object) null)
          this.Total.text = dataOfClass.totalAtk.ToString();
        if ((UnityEngine.Object) this.ReadyObj != (UnityEngine.Object) null)
          this.ReadyObj.SetActive(dataOfClass.state != 4);
        if ((UnityEngine.Object) this.Award != (UnityEngine.Object) null)
        {
          this.Award.gameObject.SetActive(false);
          this.Award.gameObject.SetActive(true);
        }
        GameParameter.UpdateAll(this.gameObject);
      }
      else
      {
        if ((UnityEngine.Object) this.EmptyObj != (UnityEngine.Object) null)
          this.EmptyObj.SetActive(true);
        if ((UnityEngine.Object) this.ValidObj != (UnityEngine.Object) null)
          this.ValidObj.SetActive(false);
        if (!((UnityEngine.Object) this.ReadyObj != (UnityEngine.Object) null))
          return;
        this.ReadyObj.SetActive(false);
      }
    }
  }
}
