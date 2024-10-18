// Decompiled with JetBrains decompiler
// Type: SRPG.SupportSettingUsedWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class SupportSettingUsedWindow : MonoBehaviour
  {
    [SerializeField]
    private GameObject mTemplete;
    private static SupportSettingUsedWindow mInstance;

    public static SupportSettingUsedWindow Instance => SupportSettingUsedWindow.mInstance;

    public List<SupportUnitUsed> mSupportUsed { get; private set; }

    private void Awake()
    {
      SupportSettingUsedWindow.mInstance = this;
      GameUtility.SetGameObjectActive(this.mTemplete, false);
    }

    private void OnDestroy()
    {
      SupportSettingUsedWindow.mInstance = (SupportSettingUsedWindow) null;
    }

    private void Refresh()
    {
      if (this.mSupportUsed == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTemplete, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.mSupportUsed.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mTemplete, this.mTemplete.transform.parent);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          DataSource.Bind<SupportUnitUsed>(gameObject, this.mSupportUsed[index]);
          gameObject.SetActive(true);
        }
      }
    }

    public void SetupSupportUsed(JSON_SupportHistory[] json)
    {
      if (json == null)
        return;
      if (this.mSupportUsed == null)
        this.mSupportUsed = new List<SupportUnitUsed>();
      this.mSupportUsed.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        SupportUnitUsed supportUnitUsed = new SupportUnitUsed();
        supportUnitUsed.unit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(json[index].iid);
        supportUnitUsed.from = DateTime.MinValue;
        if (json[index].from > 0)
          supportUnitUsed.from = TimeManager.FromUnixTime((long) json[index].from);
        supportUnitUsed.last = DateTime.MinValue;
        if (json[index].last > 0)
          supportUnitUsed.last = TimeManager.FromUnixTime((long) json[index].last);
        supportUnitUsed.times = json[index].times;
        supportUnitUsed.gold = json[index].gold <= 999999999 ? json[index].gold : 999999999;
        supportUnitUsed.element = (EElement) json[index].elem;
        eOverWritePartyType party_type = UnitOverWriteUtility.Element2OverWritePartyType(supportUnitUsed.element);
        supportUnitUsed.unit = UnitOverWriteUtility.Apply(supportUnitUsed.unit, party_type);
        this.mSupportUsed.Add(supportUnitUsed);
      }
      this.Refresh();
    }
  }
}
