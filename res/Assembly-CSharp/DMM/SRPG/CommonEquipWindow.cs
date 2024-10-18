// Decompiled with JetBrains decompiler
// Type: SRPG.CommonEquipWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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

    private void Start() => this.Refresh();

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
      DataSource.Bind<ItemParam>(((Component) this).gameObject, commonEquip);
      ItemData itemDataByItemParam = instance.Player.FindItemDataByItemParam(commonEquip);
      int num1 = 0;
      if (itemDataByItemParam != null)
      {
        num1 = itemDataByItemParam.Num;
        DataSource.Bind<ItemData>(((Component) this).gameObject, itemDataByItemParam);
      }
      else
        DataSource.Bind<ItemParam>(((Component) this).gameObject, commonEquip);
      int num2 = (int) instance.MasterParam.FixParam.EquipCommonPieceNum[commonEquip.rare];
      this.CommonName.text = LocalizedText.Get("sys.COMMON_EQUIP_NAME", (object) commonEquip.name, (object) num2);
      this.CommonAmount.text = LocalizedText.Get("sys.COMMON_EQUIP_NUM", (object) num1);
      this.CommonDescription.text = LocalizedText.Get("sys.COMMON_EQUIP_DESCRIPT", (object) commonEquip.name, (object) num2);
      int num3 = (int) instance.MasterParam.FixParam.EquipCommonPieceCost[rankupEquipData.Rarity];
      this.CommonCost.text = num3.ToString();
      bool flag1 = num3 <= instance.Player.Gold;
      bool flag2 = num1 >= num2;
      this.NotEnough.SetActive(!flag1);
      ((Component) this.CommonDescription).gameObject.SetActive(flag2);
      ((Component) this.CommonDescriptionPieceNotEnough).gameObject.SetActive(!flag2);
      ((Selectable) this.ButtonCommonEquip).interactable = flag1 && flag2;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
