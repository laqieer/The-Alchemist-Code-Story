// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueRankEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "RankUp", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "RankDown", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "強制終了", FlowNode.PinTypes.Output, 100)]
  public class GvGLeagueRankEffect : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_START = 1;
    public const int PIN_OUTPUT_RANKUP = 10;
    public const int PIN_OUTPUT_RANKDOWN = 11;
    public const int PIN_OUTPUT_FINISH = 100;
    [SerializeField]
    private GameObject mRankUpRoot;
    [SerializeField]
    private GameObject mRankDownRoot;
    [SerializeField]
    private GameObject mRankDownFrom;
    [SerializeField]
    private GameObject mRankDownTo;
    [SerializeField]
    private GameObject mRankUpIcon;
    [SerializeField]
    private GameObject mRankUpText;
    [SerializeField]
    private GameObject mRankUpMsg;

    public void Initialize(bool isRankUp)
    {
      GameUtility.SetGameObjectActive(this.mRankUpRoot, isRankUp);
      GameUtility.SetGameObjectActive(this.mRankDownRoot, !isRankUp);
      GvGManager instance = GvGManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.ResultLeague == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        if (isRankUp)
        {
          GvGLeagueViewGuild data = new GvGLeagueViewGuild();
          data.league = instance.ResultLeague.To;
          DataSource.Bind<GvGLeagueViewGuild>(this.mRankUpIcon, data);
          DataSource.Bind<GvGLeagueViewGuild>(this.mRankUpText, data);
          DataSource.Bind<GvGLeagueViewGuild>(this.mRankUpMsg, data);
        }
        else
        {
          DataSource.Bind<GvGLeagueViewGuild>(this.mRankDownFrom, new GvGLeagueViewGuild()
          {
            league = instance.ResultLeague.From
          });
          DataSource.Bind<GvGLeagueViewGuild>(this.mRankDownTo, new GvGLeagueViewGuild()
          {
            league = instance.ResultLeague.To
          });
        }
        GameParameter.UpdateAll(((Component) this).gameObject);
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      bool isRankUp = true;
      GvGManager instance = GvGManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.ResultLeague == null || instance.ResultLeague.From == null || instance.ResultLeague.To == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        if (instance.ResultLeague.From.Id != instance.ResultLeague.To.Id && instance.ResultLeague.From.Rate > instance.ResultLeague.To.Rate)
          isRankUp = false;
        this.Initialize(isRankUp);
        if (isRankUp)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      }
    }
  }
}
