// Decompiled with JetBrains decompiler
// Type: SRPG.GvGEndWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGEndWindow : MonoBehaviour
  {
    [SerializeField]
    private GvGEndWindow.eGvGEndResultType mResultType;
    [SerializeField]
    private Text mRankText;
    [SerializeField]
    private Text mPointText;
    [SerializeField]
    private Text mCaptureNodeCountText;

    private void Awake() => this.Init();

    private void Init() => this.Setup(this.GetResultData(this.mResultType));

    private void Setup(GvGResultData result_data)
    {
      if (result_data == null)
        return;
      if (Object.op_Inequality((Object) this.mRankText, (Object) null))
        this.mRankText.text = result_data.Rank.ToString();
      if (Object.op_Inequality((Object) this.mPointText, (Object) null))
        this.mPointText.text = result_data.Point.ToString();
      if (!Object.op_Inequality((Object) this.mCaptureNodeCountText, (Object) null))
        return;
      this.mCaptureNodeCountText.text = result_data.CaptureNodes.Count.ToString();
    }

    private GvGResultData GetResultData(GvGEndWindow.eGvGEndResultType result_type)
    {
      GvGManager instance = GvGManager.Instance;
      if (Object.op_Inequality((Object) instance, (Object) null))
      {
        if (result_type == GvGEndWindow.eGvGEndResultType.Daily)
          return instance.ResultDaily;
        if (result_type == GvGEndWindow.eGvGEndResultType.Season)
          return instance.ResultSeason;
      }
      return (GvGResultData) null;
    }

    public enum eGvGEndResultType
    {
      None,
      Daily,
      Season,
    }
  }
}
