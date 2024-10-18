// Decompiled with JetBrains decompiler
// Type: SRPG.ArtiSortDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "Select None", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Select Arms", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "Select Armor", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "Select Accessory", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(100, "Select None", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Select Arms", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Select Armor", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "Select Accessory", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(111, "Open", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(121, "Close", FlowNode.PinTypes.Output, 121)]
  public class ArtiSortDropdown : Pulldown, IFlowInterface
  {
    private const int PININPUT_NONE = 10;
    private const int PININPUT_ARMS = 11;
    private const int PININPUT_ARMOR = 12;
    private const int PININPUT_ACCESSORY = 13;
    private const int PINOUTPUT_NONE = 100;
    private const int PINOUTPUT_ARMS = 101;
    private const int PINOUTPUT_ARMOR = 102;
    private const int PINOUTPUT_ACCESSORY = 103;
    private const int PINOUTPUT_OPEN = 111;
    private const int PINOUTPUT_CLOSE = 121;
    public static ArtiSortDropdown.SortChangeEvent OnSortChange = (ArtiSortDropdown.SortChangeEvent) (id => { });
    public bool RefreshOnStart = true;
    public GameObject GameParamterRoot;
    public ArtiSortDropdown.ParentObjectEvent UpdateValue;
    private ArtifactTypes mInitSelection;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.mInitSelection = ArtifactTypes.None;
          break;
        case 11:
          this.mInitSelection = ArtifactTypes.Arms;
          break;
        case 12:
          this.mInitSelection = ArtifactTypes.Armor;
          break;
        case 13:
          this.mInitSelection = ArtifactTypes.Accessory;
          break;
      }
    }

    protected override void Start()
    {
      base.Start();
      ArtiSortDropdown artiSortDropdown = this;
      // ISSUE: method pointer
      artiSortDropdown.OnSelectionChange = (UnityAction<int>) System.Delegate.Combine((System.Delegate) artiSortDropdown.OnSelectionChange, (System.Delegate) new UnityAction<int>((object) this, __methodptr(SortChanged)));
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    private void SortChanged(int value)
    {
      switch (value)
      {
        case 0:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 1:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
        case 2:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
        case 3:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
          break;
      }
    }

    protected override void OpenPulldown()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
      base.OpenPulldown();
    }

    protected override void ClosePulldown(bool se = true)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 121);
      base.ClosePulldown(se);
    }

    public void Refresh()
    {
      this.ClearItems();
      for (int index = 0; index <= 3; ++index)
      {
        string empty = string.Empty;
        switch (index)
        {
          case 0:
            empty = LocalizedText.Get("sys.FILTER_AF_NONE");
            break;
          case 1:
            empty = LocalizedText.Get("sys.FILTER_AF_WEAPON");
            break;
          case 2:
            empty = LocalizedText.Get("sys.FILTER_AF_ARMOR");
            break;
          case 3:
            empty = LocalizedText.Get("sys.FILTER_AF_ACCESSORY");
            break;
        }
        this.AddItem(empty, index);
      }
      this.Selection = (int) this.mInitSelection;
    }

    public delegate void SortChangeEvent(int id);

    public delegate void ParentObjectEvent();
  }
}
