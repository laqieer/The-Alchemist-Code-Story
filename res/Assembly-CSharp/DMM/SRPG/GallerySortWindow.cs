// Decompiled with JetBrains decompiler
// Type: SRPG.GallerySortWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "ソート順の変更", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Save Setting", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ソート種別の変更", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  public class GallerySortWindow : MonoBehaviour, IFlowInterface
  {
    private const int SORT_ORDER_CHANGE = 0;
    private const int SAVE_SETTING = 1;
    private const int SORT_TYPE_CHANGE = 2;
    private const int OUTPUT_CLOSE = 100;
    [SerializeField]
    private Toggle RarityButton;
    [SerializeField]
    private Toggle NameButton;
    [SerializeField]
    private Toggle AscButton;
    [SerializeField]
    private Toggle DescButton;
    private GallerySettings mSettings;
    private bool mIsRarityAscending;
    private bool mIsNameAscending;
    private GallerySettings.SortType mCurrentSortType;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue1))
            break;
          Toggle uiToggle = currentValue1.GetUIToggle("toggle");
          if (Object.op_Equality((Object) uiToggle, (Object) this.AscButton))
          {
            if (this.mCurrentSortType == GallerySettings.SortType.Rarity)
            {
              this.mIsRarityAscending = uiToggle.isOn;
              break;
            }
            this.mIsNameAscending = uiToggle.isOn;
            break;
          }
          if (this.mCurrentSortType == GallerySettings.SortType.Rarity)
          {
            this.mIsRarityAscending = !uiToggle.isOn;
            break;
          }
          this.mIsNameAscending = !uiToggle.isOn;
          break;
        case 1:
          this.mSettings.sortType = this.mCurrentSortType;
          this.mSettings.isRarityAscending = this.mIsRarityAscending;
          this.mSettings.isNameAscending = this.mIsNameAscending;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 2:
          if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue2))
            break;
          if (Object.op_Equality((Object) currentValue2.GetUIToggle("toggle"), (Object) this.RarityButton))
          {
            this.mCurrentSortType = GallerySettings.SortType.Rarity;
            this.AscButton.isOn = this.mIsRarityAscending;
            this.DescButton.isOn = !this.mIsRarityAscending;
            break;
          }
          this.mCurrentSortType = GallerySettings.SortType.Name;
          this.AscButton.isOn = this.mIsNameAscending;
          this.DescButton.isOn = !this.mIsNameAscending;
          break;
      }
    }

    private void Awake()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      FlowNode_ButtonEvent.currentValue = (object) null;
      this.mSettings = currentValue.GetObject("settings") as GallerySettings;
      if (this.mSettings == null)
        return;
      this.AscButton.isOn = this.mSettings.isRarityAscending;
      this.DescButton.isOn = !this.mSettings.isRarityAscending;
      this.mCurrentSortType = this.mSettings.sortType;
      this.mIsRarityAscending = this.mSettings.isRarityAscending;
      this.mIsNameAscending = this.mSettings.isNameAscending;
      if (this.mCurrentSortType == GallerySettings.SortType.Rarity)
      {
        this.RarityButton.isOn = true;
        this.NameButton.isOn = false;
        this.AscButton.isOn = this.mIsRarityAscending;
        this.DescButton.isOn = !this.mIsRarityAscending;
      }
      else
      {
        this.RarityButton.isOn = false;
        this.NameButton.isOn = true;
        this.AscButton.isOn = this.mIsNameAscending;
        this.DescButton.isOn = !this.mIsNameAscending;
      }
    }
  }
}
