// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "非表示", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "進捗表示", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "表示", FlowNode.PinTypes.Input, 1)]
  public class ChallengeMissionIcon : MonoBehaviour, IFlowInterface
  {
    public GameObject Badge;
    public Text BadgeCount;
    [SerializeField]
    private Image challengeIconIMG;
    [SerializeField]
    private Button btn;
    [SerializeField]
    private Image badgeIMG;
    [SerializeField]
    private Text badgeCountText;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 1:
          this.ShowImages(true);
          break;
        case 2:
          if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
            break;
          this.ShowImages(false);
          break;
      }
    }

    public void ShowImages(bool isShowImages)
    {
      Color color = !isShowImages ? Color.clear : Color.white;
      if ((UnityEngine.Object) this.challengeIconIMG != (UnityEngine.Object) null)
        this.challengeIconIMG.color = color;
      if ((UnityEngine.Object) this.btn != (UnityEngine.Object) null)
        this.btn.enabled = isShowImages;
      if ((UnityEngine.Object) this.badgeIMG != (UnityEngine.Object) null)
        this.badgeIMG.color = color;
      if (!((UnityEngine.Object) this.badgeCountText != (UnityEngine.Object) null))
        return;
      this.badgeCountText.color = color;
    }

    private void Refresh()
    {
      if (!((UnityEngine.Object) MonoSingleton<GameManager>.Instance != (UnityEngine.Object) null))
        return;
      bool flag1 = false;
      bool flag2 = true;
      int num = 0;
      foreach (TrophyParam trophyParam in ChallengeMission.GetRootTrophiesSortedByPriority())
      {
        if (!ChallengeMission.GetTrophyCounter(trophyParam).IsEnded)
        {
          num += this.UncollectedRewardCount(trophyParam);
          flag2 = false;
          if (num > 0)
          {
            flag1 = true;
            break;
          }
        }
      }
      this.Badge.SetActive(flag1);
      if (flag1)
        this.BadgeCount.text = num.ToString();
      this.gameObject.SetActive(!flag2);
      if (flag2)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private int UncollectedRewardCount(TrophyParam rootTrophy)
    {
      TrophyParam[] childeTrophies = ChallengeMission.GetChildeTrophies(rootTrophy);
      int num = 0;
      foreach (TrophyParam trophy in childeTrophies)
      {
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(trophy);
        if (trophyCounter.IsCompleted && !trophyCounter.IsEnded)
          ++num;
      }
      return num;
    }
  }
}
