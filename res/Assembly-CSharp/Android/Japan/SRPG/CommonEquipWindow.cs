// Decompiled with JetBrains decompiler
// Type: SRPG.CommonEquipWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class CommonEquipWindow : MonoBehaviour
  {
    public Text CommonName;
    public Text CommonAmount;
    public Text CommonDescription;
    public Text CommonDescriptionPieceNotEnough;
    public Text CommonCost;
    public GameObject NotEnough;
    public Button ButtonCommonEquip;

    private void Start()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      UnitData unitDataByUniqueId = instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (unitDataByUniqueId == null || instance.MasterParam.FixParam.EquipCommonPieceNum == null || instance.MasterParam.FixParam.EquipCommonPieceCost == null)
        return;
      List<JobData> jobDataList = new List<JobData>((IEnumerable<JobData>) unitDataByUniqueId.Jobs);
      JobData jobData = jobDataList.Find((Predicate<JobData>) (x => (long) GlobalVars.SelectedJobUniqueID == x.UniqueID));
      if (jobData == null)
        return;
      int jobNo = jobDataList.IndexOf(jobData);
      EquipData rankupEquipData = unitDataByUniqueId.GetRankupEquipData(jobNo, (int) GlobalVars.SelectedEquipmentSlot);
      ItemParam commonEquip = instance.MasterParam.GetCommonEquip(rankupEquipData.ItemParam, jobData.Rank == 0);
      if (commonEquip != null)
        return;
      DataSource.Bind<ItemParam>(this.gameObject, commonEquip, false);
      ItemData itemDataByItemParam = instance.Player.FindItemDataByItemParam(commonEquip);
      int num1 = 0;
      if (itemDataByItemParam != null)
      {
        num1 = itemDataByItemParam.Num;
        DataSource.Bind<ItemData>(this.gameObject, itemDataByItemParam, false);
      }
      else
        DataSource.Bind<ItemParam>(this.gameObject, commonEquip, false);
      int num2 = (int) instance.MasterParam.FixParam.EquipCommonPieceNum[commonEquip.rare];
      this.CommonName.text = LocalizedText.Get("sys.COMMON_EQUIP_NAME", (object) commonEquip.name, (object) num2);
      this.CommonAmount.text = LocalizedText.Get("sys.COMMON_EQUIP_NUM", new object[1]
      {
        (object) num1
      });
      this.CommonDescription.text = LocalizedText.Get("sys.COMMON_EQUIP_DESCRIPT", (object) commonEquip.name, (object) num2);
      int num3 = (int) instance.MasterParam.FixParam.EquipCommonPieceCost[rankupEquipData.Rarity];
      this.CommonCost.text = num3.ToString();
      bool flag1 = num3 <= instance.Player.Gold;
      bool flag2 = num1 >= num2;
      this.NotEnough.SetActive(!flag1);
      this.CommonDescription.gameObject.SetActive(flag2);
      this.CommonDescriptionPieceNotEnough.gameObject.SetActive(!flag2);
      this.ButtonCommonEquip.interactable = flag1 && flag2;
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
