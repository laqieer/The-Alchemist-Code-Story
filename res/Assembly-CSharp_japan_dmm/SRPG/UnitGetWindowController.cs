// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetWindowController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitGetWindowController : MonoBehaviour
  {
    private UnitGetWindow mController;
    private bool mIsEnd;

    public bool IsEnd => this.mIsEnd;

    public void Init(UnitGetParam rewards = null)
    {
      UnitGetParam unitGetParam = rewards == null ? GlobalVars.UnitGetReward : rewards;
      if (unitGetParam == null || unitGetParam.Params.Count <= 0)
      {
        this.mIsEnd = true;
      }
      else
      {
        bool flag = true;
        string[] unitIds = new string[unitGetParam.Params.Count];
        bool[] isConvert = new bool[unitGetParam.Params.Count];
        int[] covertPieces = new int[unitGetParam.Params.Count];
        for (int index = 0; index < unitGetParam.Params.Count; ++index)
        {
          if (unitGetParam.Params[index].ItemType != EItemType.Unit)
          {
            unitIds[index] = string.Empty;
          }
          else
          {
            if (flag)
              flag = false;
            unitIds[index] = unitGetParam.Params[index].ItemId;
            isConvert[index] = unitGetParam.Params[index].IsConvert;
            covertPieces[index] = unitGetParam.Params[index].ConvertPieceNum;
            if (!isConvert[index])
            {
              DownloadUtility.DownloadUnit(unitGetParam.Params[index].UnitParam);
              FlowNode_ExtraUnitOpenPopup.ReserveOpenExtraQuestPopup(unitGetParam.Params[index].UnitParam.iname);
            }
          }
        }
        this.mIsEnd = flag;
        if (this.mIsEnd)
          return;
        this.StartCoroutine(this.SpawnEffectAsync(unitIds, isConvert, covertPieces));
      }
    }

    [DebuggerHidden]
    private IEnumerator SpawnEffectAsync(string[] unitIds, bool[] isConvert, int[] covertPieces)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitGetWindowController.\u003CSpawnEffectAsync\u003Ec__Iterator0()
      {
        unitIds = unitIds,
        isConvert = isConvert,
        covertPieces = covertPieces,
        \u0024this = this
      };
    }
  }
}
