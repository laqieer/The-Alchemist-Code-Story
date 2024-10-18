// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPieceShopParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class UnitPieceShopParam
  {
    public string Iname { get; private set; }

    public string CostIname { get; private set; }

    public string Banner { get; private set; }

    public DateTime BeginAt { get; private set; }

    public DateTime EndAt { get; private set; }

    public bool IsWithinPeriod()
    {
      DateTime serverTime = TimeManager.ServerTime;
      return this.BeginAt <= serverTime && serverTime <= this.EndAt;
    }

    public bool Deserialize(JSON_UnitPieceShopParam json)
    {
      if (json == null)
        return false;
      this.Iname = json.iname;
      this.CostIname = json.cost_iname;
      this.Banner = json.banner;
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.BeginAt = DateTime.Parse(json.begin_at);
        if (!string.IsNullOrEmpty(json.end_at))
          this.EndAt = DateTime.Parse(json.end_at);
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("開始日時、終了日時の設定が正しくありません!! id : " + json.iname);
        return false;
      }
      return true;
    }

    public static void Deserialize(
      ref List<UnitPieceShopParam> param,
      JSON_UnitPieceShopParam[] json)
    {
      if (json == null)
        return;
      param = new List<UnitPieceShopParam>(json.Length);
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          UnitPieceShopParam unitPieceShopParam = new UnitPieceShopParam();
          if (unitPieceShopParam.Deserialize(json[index]))
            param.Add(unitPieceShopParam);
        }
      }
    }

    public static UnitPieceShopParam GetCurrentParam()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (UnitPieceShopParam) null;
      if (MonoSingleton<GameManager>.Instance.MasterParam.UnitPieceShop != null)
        return MonoSingleton<GameManager>.Instance.MasterParam.UnitPieceShop.FindLast((Predicate<UnitPieceShopParam>) (prm => prm.IsWithinPeriod()));
      DebugUtility.Log(string.Format("<color=yellow>UnitPieceShopParam/GetCurrentParam no data!</color>"));
      return (UnitPieceShopParam) null;
    }
  }
}
