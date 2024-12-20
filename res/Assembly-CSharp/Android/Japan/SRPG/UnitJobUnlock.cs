﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobUnlock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/UnitJobUnlock")]
  public class UnitJobUnlock : MonoBehaviour
  {
    public GameObject JobIcon;
    public Text JobName;

    private void Start()
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      JobData jobData = (JobData) null;
      for (int index = 0; index < unitDataByUniqueId.Jobs.Length; ++index)
      {
        if (unitDataByUniqueId.Jobs[index] != null && unitDataByUniqueId.Jobs[index].UniqueID == (long) GlobalVars.SelectedJobUniqueID)
          jobData = unitDataByUniqueId.Jobs[index];
      }
      if (jobData == null)
        return;
      if ((UnityEngine.Object) this.JobIcon != (UnityEngine.Object) null)
      {
        string str = AssetPath.JobIconSmall(jobData == null ? (JobParam) null : jobData.Param);
        if (!string.IsNullOrEmpty(str))
        {
          IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(this.JobIcon);
          if ((UnityEngine.Object) iconLoader != (UnityEngine.Object) null)
            iconLoader.ResourcePath = str;
        }
      }
      if (!((UnityEngine.Object) this.JobName != (UnityEngine.Object) null))
        return;
      this.JobName.text = jobData.Name;
    }
  }
}
