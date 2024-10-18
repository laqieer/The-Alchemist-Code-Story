// Decompiled with JetBrains decompiler
// Type: SRPG.EquipEnhanceConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EquipEnhanceConfirmWindow : MonoBehaviour
  {
    private const int PIN_OUT_CLOSE = 0;
    [SerializeField]
    private GameObject mTemplateItem;
    [SerializeField]
    private GameObject mCostErrorObj;
    [SerializeField]
    private Text mCostText;
    [SerializeField]
    private Button mSelectButton;
    public EquipEnhanceConfirmWindow.OnEnhanceSelectedEvent OnSelectedEvent;
    public EquipEnhanceConfirmWindow.OnEnhanceSelectedEvent OnCanceledEvent;

    public void OnDecide()
    {
      if (this.OnSelectedEvent == null)
        return;
      this.OnSelectedEvent();
    }

    public void OnCancel()
    {
      if (this.OnCanceledEvent == null)
        return;
      this.OnCanceledEvent();
    }

    public void SetupItem(ItemData list, int num)
    {
      if (list == null)
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(this.mTemplateItem, this.mTemplateItem.transform.parent);
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      EquipEnhanceConfirmList componentInChildren = gameObject.GetComponentInChildren<EquipEnhanceConfirmList>();
      if (Object.op_Inequality((Object) componentInChildren.mEnhanceNum, (Object) null))
        componentInChildren.mEnhanceNum.text = num.ToString();
      DataSource.Bind<ItemParam>(gameObject, list.Param);
      gameObject.SetActive(true);
    }

    public void SetupCost(int cost)
    {
      if (Object.op_Inequality((Object) this.mCostText, (Object) null))
        this.mCostText.text = cost.ToString();
      if (Object.op_Equality((Object) this.mCostErrorObj, (Object) null) || Object.op_Equality((Object) this.mSelectButton, (Object) null))
        return;
      if (cost > MonoSingleton<GameManager>.Instance.Player.Gold)
      {
        this.mCostErrorObj.SetActive(true);
        ((Selectable) this.mSelectButton).interactable = false;
      }
      else
      {
        this.mCostErrorObj.SetActive(false);
        ((Selectable) this.mSelectButton).interactable = true;
      }
    }

    public delegate void OnEnhanceSelectedEvent();
  }
}
