// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakuseiWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "表示を更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "ユニットが覚醒した", FlowNode.PinTypes.Output, 100)]
  public class UnitKakuseiWindow : MonoBehaviour, IFlowInterface
  {
    public UnitKakuseiWindow.KakuseiWindowEvent OnKakuseiAccept;
    public UnitData Unit;
    public JobParam UnlockJobParam;
    public Button KakuseiButton;
    public Text KakeraMsg;
    public Text KakeraCharaMsg;
    public Text KakeraElementMsg;
    public Text KakeraCommonMsg;
    public GameObject JobUnlock;
    private UnitData mCurrentUnit;
    private ItemParam mElementKakera;
    private ItemParam mCommonKakera;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Start()
    {
      this.Refresh();
      if (!((UnityEngine.Object) this.KakuseiButton != (UnityEngine.Object) null))
        return;
      this.KakuseiButton.onClick.AddListener(new UnityAction(this.OnKakuseiClick));
    }

    private void OnKakuseiClick()
    {
      if (this.mElementKakera != null)
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_ELEMENT_KAKERA"), (object) this.mElementKakera.name), new UIUtility.DialogResultEvent(this.AcceptElementKakera), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.AcceptElementKakera((GameObject) null);
    }

    private void AcceptElementKakera(GameObject go)
    {
      if (this.mCommonKakera != null)
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_COMMON_KAKERA"), (object) this.mCommonKakera.name), new UIUtility.DialogResultEvent(this.AcceptCommonKakera), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.AcceptCommonKakera((GameObject) null);
    }

    private void AcceptCommonKakera(GameObject go)
    {
      this.KakuseiAccept();
    }

    private void KakuseiAccept()
    {
      if (this.OnKakuseiAccept != null)
      {
        this.OnKakuseiAccept();
      }
      else
      {
        MonoSingleton<GameManager>.Instance.Player.AwakingUnit(this.mCurrentUnit);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    public void Refresh()
    {
      this.mCurrentUnit = this.Unit == null ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID) : this.Unit;
      if (this.mCurrentUnit == null || this.mCurrentUnit.GetAwakeLevelCap() <= this.mCurrentUnit.AwakeLv)
        return;
      DataSource.Bind<UnitData>(this.gameObject, this.mCurrentUnit);
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam((string) this.mCurrentUnit.UnitParam.piece);
      int itemAmount1 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount((string) this.mCurrentUnit.UnitParam.piece);
      int awakeNeedPieces = this.mCurrentUnit.GetAwakeNeedPieces();
      if ((UnityEngine.Object) this.KakuseiButton != (UnityEngine.Object) null)
        this.KakuseiButton.interactable = this.mCurrentUnit.CheckUnitAwaking();
      if ((UnityEngine.Object) this.KakeraMsg != (UnityEngine.Object) null)
        this.KakeraMsg.text = string.Format(LocalizedText.Get("sys.CONFIRM_KAKUSEI"), (object) itemParam.name, (object) awakeNeedPieces, (object) itemAmount1);
      int num1 = awakeNeedPieces;
      int itemAmount2 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount((string) this.mCurrentUnit.UnitParam.piece);
      int num2 = itemAmount2 < awakeNeedPieces ? itemAmount2 : awakeNeedPieces;
      if ((UnityEngine.Object) this.KakeraCharaMsg != (UnityEngine.Object) null)
      {
        if (num2 > 0)
        {
          this.KakeraCharaMsg.text = string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_CHARA"), (object) itemParam.name, (object) num2, (object) itemAmount2);
          this.KakeraCharaMsg.gameObject.SetActive(true);
        }
        else
          this.KakeraCharaMsg.gameObject.SetActive(false);
      }
      int num3 = Mathf.Max(0, num1 - num2);
      ItemParam elementPieceParam = this.mCurrentUnit.GetElementPieceParam();
      int elementPieces = this.mCurrentUnit.GetElementPieces();
      int num4 = elementPieces < num3 ? elementPieces : num3;
      this.mElementKakera = (ItemParam) null;
      if ((UnityEngine.Object) this.KakeraElementMsg != (UnityEngine.Object) null && elementPieceParam != null)
      {
        if (num4 > 0)
        {
          this.KakeraElementMsg.text = string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_ELEMENT"), (object) elementPieceParam.name, (object) num4, (object) elementPieces);
          this.KakeraElementMsg.gameObject.SetActive(true);
          this.mElementKakera = elementPieceParam;
        }
        else
          this.KakeraElementMsg.gameObject.SetActive(false);
      }
      int num5 = Mathf.Max(0, num3 - num4);
      ItemParam commonPieceParam = this.mCurrentUnit.GetCommonPieceParam();
      int commonPieces = this.mCurrentUnit.GetCommonPieces();
      int num6 = commonPieces < num5 ? commonPieces : num5;
      this.mCommonKakera = (ItemParam) null;
      if ((UnityEngine.Object) this.KakeraCommonMsg != (UnityEngine.Object) null && commonPieceParam != null)
      {
        if (num6 > 0)
        {
          this.KakeraCommonMsg.text = string.Format(LocalizedText.Get("sys.KAKUSEI_KAKERA_COMMON"), (object) commonPieceParam.name, (object) num6, (object) commonPieces);
          this.KakeraCommonMsg.gameObject.SetActive(true);
          this.mCommonKakera = commonPieceParam;
        }
        else
          this.KakeraCommonMsg.gameObject.SetActive(false);
      }
      Mathf.Max(0, num5 - num6);
      if ((UnityEngine.Object) this.JobUnlock != (UnityEngine.Object) null)
      {
        bool flag = false;
        if (this.UnlockJobParam != null)
        {
          DataSource.Bind<JobParam>(this.JobUnlock, this.UnlockJobParam);
          flag = true;
        }
        this.JobUnlock.SetActive(flag);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    public delegate void KakuseiWindowEvent();
  }
}
