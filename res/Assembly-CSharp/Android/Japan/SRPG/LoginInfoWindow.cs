// Decompiled with JetBrains decompiler
// Type: SRPG.LoginInfoWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "None", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "Gacha", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "LimitedShop", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "EventQuest", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "TowerQuest", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "BuyCoin", FlowNode.PinTypes.Output, 5)]
  public class LoginInfoWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Button Move;
    [SerializeField]
    private Text MoveBtnText;
    [SerializeField]
    private Image InfoImage;
    [SerializeField]
    private Toggle CheckToggle;
    [SerializeField]
    private Button CloseBtn;
    private LoginInfoParam.SelectScene mSelectScene;
    private bool mLoaded;
    private bool mRefresh;

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.Move != (UnityEngine.Object) null)
        this.Move.interactable = false;
      if ((UnityEngine.Object) this.InfoImage != (UnityEngine.Object) null)
        this.InfoImage.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this.CheckToggle != (UnityEngine.Object) null))
        return;
      this.CheckToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChange));
    }

    private void Start()
    {
      LoginInfoParam[] activeLoginInfos = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetActiveLoginInfos();
      if (activeLoginInfos == null || activeLoginInfos.Length <= 0)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
      }
      else
      {
        int index = Random.Range(0, activeLoginInfos.Length);
        this.mSelectScene = activeLoginInfos[index].scene;
        string[] strArray = activeLoginInfos[index].path.Split('/');
        if (strArray == null || strArray.Length < 2)
          return;
        this.StartCoroutine(this.LoadImages(strArray[0], strArray[1]));
      }
    }

    private void Update()
    {
      if (!this.mLoaded || this.mRefresh)
        return;
      this.mRefresh = true;
      this.Refresh();
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.Move != (UnityEngine.Object) null && (UnityEngine.Object) this.MoveBtnText != (UnityEngine.Object) null)
      {
        this.MoveBtnText.text = this.mSelectScene != LoginInfoParam.SelectScene.Gacha ? (this.mSelectScene != LoginInfoParam.SelectScene.LimitedShop ? (this.mSelectScene != LoginInfoParam.SelectScene.EventQuest ? (this.mSelectScene != LoginInfoParam.SelectScene.TowerQuest ? (this.mSelectScene != LoginInfoParam.SelectScene.BuyShop ? LocalizedText.Get("sys.OK") : LocalizedText.Get("sys.TEXT_LOGININFO_BUYCOIN")) : LocalizedText.Get("sys.TEXT_LOGININFO_TOWERQUEST")) : LocalizedText.Get("sys.TEXT_LOGININFO_EVENTQUEST")) : LocalizedText.Get("sys.TEXT_LOGININFO_LIMITEDSHOP")) : LocalizedText.Get("sys.TEXT_LOGININFO_GACHA");
        this.Move.onClick.AddListener(new UnityAction(this.OnMoveScene));
        this.Move.interactable = true;
      }
      if (!((UnityEngine.Object) this.InfoImage != (UnityEngine.Object) null))
        return;
      this.InfoImage.gameObject.SetActive((UnityEngine.Object) this.InfoImage.sprite != (UnityEngine.Object) null);
    }

    private void OnMoveScene()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, (int) this.mSelectScene);
    }

    private void OnValueChange(bool value)
    {
      string empty = string.Empty;
      if (value)
        empty = TimeManager.ServerTime.ToString("yyyy/MM/dd");
      GameUtility.setLoginInfoRead(empty);
    }

    [DebuggerHidden]
    private IEnumerator LoadImages(string path, string img)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginInfoWindow.\u003CLoadImages\u003Ec__Iterator0() { path = path, img = img, \u0024this = this };
    }

    public enum SelectScene : byte
    {
      None,
      Gacha,
      LimitedShop,
      EventQuest,
      TowerQuest,
      BuyShop,
    }
  }
}
