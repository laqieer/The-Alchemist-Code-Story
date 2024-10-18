// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobUnlock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (Object.op_Inequality((Object) this.JobIcon, (Object) null))
      {
        string str = AssetPath.JobIconSmall(jobData == null ? (JobParam) null : jobData.Param);
        if (!string.IsNullOrEmpty(str))
        {
          IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(this.JobIcon);
          if (Object.op_Inequality((Object) iconLoader, (Object) null))
            iconLoader.ResourcePath = str;
        }
      }
      if (!Object.op_Inequality((Object) this.JobName, (Object) null))
        return;
      this.JobName.text = jobData.Name;
    }
  }
}
