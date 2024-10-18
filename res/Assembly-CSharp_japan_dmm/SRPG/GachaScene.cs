// Decompiled with JetBrains decompiler
// Type: SRPG.GachaScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "終了", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "表示開始", FlowNode.PinTypes.Output, 100)]
  public class GachaScene : SceneRoot, IFlowInterface
  {
    public GameObject Result2D;
    public GameObject Result3D;
    public GridLayoutGroup GridLayout;
    public int MaxGridColumnCount = 5;
    public string[] PreviewUnitID;
    public string[] PreviewItemID;
    private GachaScene.DropClasses mDropClass;
    private string[] mDropID;
    private bool mDropSet;

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.StartCoroutine(this.ExitGachaAsync());
    }

    public void DropUnits(string[] unitID)
    {
      this.mDropClass = GachaScene.DropClasses.Unit;
      this.mDropID = unitID;
      this.mDropSet = true;
    }

    public void DropItems(string[] itemID)
    {
      this.mDropClass = GachaScene.DropClasses.Item;
      this.mDropID = itemID;
      this.mDropSet = true;
    }

    protected override void Awake() => base.Awake();

    private void Start()
    {
      if (Object.op_Inequality((Object) this.Result2D, (Object) null))
        this.Result2D.SetActive(false);
      this.StartCoroutine(this.AsyncUpdate());
    }

    [DebuggerHidden]
    private IEnumerator AsyncUpdate()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaScene.\u003CAsyncUpdate\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator ExitGachaAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaScene.\u003CExitGachaAsync\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    public enum DropClasses
    {
      Unit,
      Item,
    }
  }
}
