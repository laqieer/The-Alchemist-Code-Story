// Decompiled with JetBrains decompiler
// Type: SRPG.TutorialGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "PlayAnim", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "ShowResult", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "RedrawGacha", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "SelectRedraw", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "CancelRedraw", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(10, "PlayAnim", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "RedrawGacha", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "ShowResult", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "CancelRedraw", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "Re PlayAnim", FlowNode.PinTypes.Output, 14)]
  public class TutorialGacha : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    [StringIsResourcePath(typeof (GachaController))]
    public string GachaAnimPrefab = "UI/GachaAnim2";
    [SerializeField]
    [StringIsResourcePath(typeof (GameObject))]
    public string GachaResultPrefab = "UI/GachaResult";
    private const int PIN_IN_PLAY_ANIM = 0;
    private const int PIN_IN_SHOW_RESULT = 1;
    private const int PIN_IN_REDRAW_GACHA = 2;
    private const int PIN_IN_SELECT_REDRAW = 3;
    private const int PIN_IN_CANCEL_REDRAW = 4;
    private const int PIN_OU_PLAY_ANIM = 10;
    private const int PIN_OU_REDRAW_GACHA = 11;
    private const int PIN_OU_SHOW_RESULT = 12;
    private const int PIN_OU_CANCEL_REDRAW = 13;
    private const int PIN_OU_RE_PLAY_ANIM = 14;
    [SerializeField]
    private Transform UIRoot;
    [SerializeField]
    private GameObject[] ResultOptionObject;
    private GachaController m_GachaController;
    private GameObject m_GachaResult;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.StartCoroutine(this.PlayGachaAsync(10));
          break;
        case 1:
          this.StartCoroutine(this.ShowGachaResult());
          break;
        case 2:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
        case 3:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
        case 4:
          this.DecideTutorialGacha();
          break;
      }
    }

    [DebuggerHidden]
    private IEnumerator PlayGachaAsync(int pinID)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TutorialGacha.\u003CPlayGachaAsync\u003Ec__Iterator0() { pinID = pinID, \u0024this = this };
    }

    private bool SetupDropData(UnitParam _unit)
    {
      if (_unit == null)
      {
        DebugUtility.LogError("召喚したいユニットの情報がありません.");
        return false;
      }
      GachaResultData.Init(new List<GachaDropData>()
      {
        new GachaDropData()
        {
          type = GachaDropData.Type.Unit,
          unit = _unit,
          Rare = (int) _unit.rare
        }
      }, (List<GachaDropData>) null, (List<int>) null, (GachaReceiptData) null, false, -1, -1);
      return true;
    }

    [DebuggerHidden]
    private IEnumerator ShowGachaResult()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TutorialGacha.\u003CShowGachaResult\u003Ec__Iterator1() { \u0024this = this };
    }

    private void SetupOptionObject(bool _enable)
    {
      if (this.ResultOptionObject == null || this.ResultOptionObject.Length <= 0)
      {
        DebugUtility.LogWarning("召喚結果で表示するオブションオブジェクトの指定がありません.");
      }
      else
      {
        for (int index = 0; index < this.ResultOptionObject.Length; ++index)
          this.ResultOptionObject[index].SetActive(_enable);
      }
    }

    private void DecideTutorialGacha()
    {
      if ((UnityEngine.Object) this.m_GachaController != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_GachaController.gameObject);
        this.m_GachaController = (GachaController) null;
      }
      if ((UnityEngine.Object) this.m_GachaResult != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_GachaResult);
        this.m_GachaResult = (GameObject) null;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
    }
  }
}
