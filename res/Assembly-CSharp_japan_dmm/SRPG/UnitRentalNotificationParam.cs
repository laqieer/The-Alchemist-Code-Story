// Decompiled with JetBrains decompiler
// Type: SRPG.UnitRentalNotificationParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class UnitRentalNotificationParam
  {
    private string mIname;
    private UnitRentalNotificationDataParam[] mNotiInfos;

    public string Iname => this.mIname;

    public List<UnitRentalNotificationDataParam> NotiList
    {
      get
      {
        return this.mNotiInfos != null ? new List<UnitRentalNotificationDataParam>((IEnumerable<UnitRentalNotificationDataParam>) this.mNotiInfos) : new List<UnitRentalNotificationDataParam>();
      }
    }

    public void Deserialize(JSON_UnitRentalNotificationParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mNotiInfos = (UnitRentalNotificationDataParam[]) null;
      if (json.noti_infos == null || json.noti_infos.Length == 0)
        return;
      this.mNotiInfos = new UnitRentalNotificationDataParam[json.noti_infos.Length];
      for (int index = 0; index < json.noti_infos.Length; ++index)
      {
        this.mNotiInfos[index] = new UnitRentalNotificationDataParam();
        this.mNotiInfos[index].Deserialize(json.noti_infos[index]);
      }
    }

    public static void Deserialize(
      ref Dictionary<string, UnitRentalNotificationParam> dict,
      JSON_UnitRentalNotificationParam[] json)
    {
      if (json == null)
        return;
      if (dict == null)
        dict = new Dictionary<string, UnitRentalNotificationParam>(json.Length);
      dict.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        UnitRentalNotificationParam notificationParam = new UnitRentalNotificationParam();
        notificationParam.Deserialize(json[index]);
        if (!dict.ContainsKey(json[index].iname))
          dict.Add(json[index].iname, notificationParam);
      }
    }

    public static UnitRentalNotificationParam GetParam(string key)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (UnitRentalNotificationParam) null;
      Dictionary<string, UnitRentalNotificationParam> notificationParams = MonoSingleton<GameManager>.Instance.MasterParam.UnitRentalNotificationParams;
      if (notificationParams == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>UnitRentalNotificationParam/GetParam no data!</color>"));
        return (UnitRentalNotificationParam) null;
      }
      try
      {
        return notificationParams[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<UnitRentalNotificationParam>(key);
      }
    }
  }
}
