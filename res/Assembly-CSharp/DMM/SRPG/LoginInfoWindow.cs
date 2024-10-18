// Decompiled with JetBrains decompiler
// Type: SRPG.LoginInfoWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "None", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "Gacha", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "LimitedShop", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "EventQuest", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "TowerQuest", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "BuyCoin", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(10, "MoiveStart", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(12, "Url Load", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "Url Load End", FlowNode.PinTypes.Output, 13)]
  public class LoginInfoWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_PLAYMOVIE = 10;
    private const int PIN_INPUT_URLJUMP = 12;
    private const int PIN_OUTPUT_URLJUMP = 13;
    [SerializeField]
    private Button Move;
    [SerializeField]
    private Text MoveBtnText;
    [SerializeField]
    private GameObject Url;
    [SerializeField]
    private GameObject Movie;
    [SerializeField]
    private Image InfoImage;
    [SerializeField]
    private Toggle CheckToggle;
    [SerializeField]
    private Button CloseBtn;
    private LoginInfoParam.SelectScene mSelectScene;
    private bool mLoaded;
    private bool mRefresh;
    private string mUrlFileName;
    private string mMovieFileName;
    private bool AutoFade;
    private const float FadeTime = 1f;
    public Color FadeColor = Color.black;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          if (Application.internetReachability == null)
          {
            UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_MOVIE_RETRY"), new UIUtility.DialogResultEvent(this.OnRetry), new UIUtility.DialogResultEvent(this.OnCancelRetry), systemModal: true);
            break;
          }
          this.PlayMovie(this.mMovieFileName, true);
          break;
        case 12:
          FlowNode_Variable.Set("LOGIN_NEWS_URL", this.mUrlFileName);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
          break;
      }
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.Move, (Object) null))
        ((Selectable) this.Move).interactable = false;
      if (Object.op_Inequality((Object) this.Url, (Object) null))
        this.Url.SetActive(false);
      if (Object.op_Inequality((Object) this.Movie, (Object) null))
        this.Movie.SetActive(false);
      if (Object.op_Inequality((Object) this.InfoImage, (Object) null))
        ((Component) this.InfoImage).gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.CheckToggle, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.CheckToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnValueChange)));
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
        if (activeLoginInfos[0].priority > 0)
        {
          index = 0;
          string key = activeLoginInfos[index].iname + (object) activeLoginInfos[index].begin_at;
          if (PlayerPrefsUtility.HasKey(key))
            PlayerPrefsUtility.SetInt(key, PlayerPrefsUtility.GetInt(key) + 1, true);
          else
            PlayerPrefsUtility.SetInt(key, 1, true);
        }
        this.mSelectScene = activeLoginInfos[index].scene;
        string[] strArray = activeLoginInfos[index].path.Split('/');
        if (!string.IsNullOrEmpty(activeLoginInfos[index].url))
        {
          this.mUrlFileName = activeLoginInfos[index].url;
          if (Object.op_Inequality((Object) this.Url, (Object) null))
            this.Url.SetActive(true);
        }
        if (!string.IsNullOrEmpty(activeLoginInfos[index].movie))
        {
          this.mMovieFileName = activeLoginInfos[index].movie;
          if (Object.op_Inequality((Object) this.Movie, (Object) null))
            this.Movie.SetActive(true);
        }
        if (strArray != null && strArray.Length >= 2)
          this.StartCoroutine(this.LoadImages(strArray[0], strArray[1]));
        if (!activeLoginInfos[index].movie_compel || activeLoginInfos[index].priority == 0 || string.IsNullOrEmpty(this.mMovieFileName))
          return;
        if (Application.internetReachability == null)
          UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_MOVIE_RETRY"), new UIUtility.DialogResultEvent(this.OnRetry), new UIUtility.DialogResultEvent(this.OnCancelRetry), systemModal: true);
        else
          this.PlayMovie(this.mMovieFileName);
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
      if (Object.op_Inequality((Object) this.Move, (Object) null) && Object.op_Inequality((Object) this.MoveBtnText, (Object) null))
      {
        this.MoveBtnText.text = this.mSelectScene != LoginInfoParam.SelectScene.Gacha ? (this.mSelectScene != LoginInfoParam.SelectScene.LimitedShop ? (this.mSelectScene != LoginInfoParam.SelectScene.EventQuest ? (this.mSelectScene != LoginInfoParam.SelectScene.TowerQuest ? (this.mSelectScene != LoginInfoParam.SelectScene.BuyShop ? LocalizedText.Get("sys.OK") : LocalizedText.Get("sys.TEXT_LOGININFO_BUYCOIN")) : LocalizedText.Get("sys.TEXT_LOGININFO_TOWERQUEST")) : LocalizedText.Get("sys.TEXT_LOGININFO_EVENTQUEST")) : LocalizedText.Get("sys.TEXT_LOGININFO_LIMITEDSHOP")) : LocalizedText.Get("sys.TEXT_LOGININFO_GACHA");
        // ISSUE: method pointer
        ((UnityEvent) this.Move.onClick).AddListener(new UnityAction((object) this, __methodptr(OnMoveScene)));
        ((Selectable) this.Move).interactable = true;
      }
      if (!Object.op_Inequality((Object) this.InfoImage, (Object) null))
        return;
      ((Component) this.InfoImage).gameObject.SetActive(Object.op_Inequality((Object) this.InfoImage.sprite, (Object) null));
    }

    private void OnRetry(GameObject go) => this.PlayMovie(this.mMovieFileName, true);

    private void OnCancelRetry(GameObject go)
    {
    }

    private void OnCancelReplay(GameObject go)
    {
    }

    private void PlayMovie(string fileName, bool fade = false)
    {
      ((Behaviour) this).enabled = true;
      this.AutoFade = fade;
      if (this.AutoFade)
      {
        SRPG_TouchInputModule.LockInput();
        CriticalSection.Enter();
        FadeController.Instance.FadeTo(this.FadeColor, 1f);
        this.StartCoroutine(this.PlayDelayed(fileName, new StreamingMovie.OnFinished(this.OnFinished)));
      }
      else
      {
        MonoSingleton<StreamingMovie>.Instance.StopSound();
        MonoSingleton<StreamingMovie>.Instance.Play(fileName, new StreamingMovie.OnFinished(this.OnFinished));
      }
    }

    public void OnFinished(bool is_replay = false)
    {
      if (this.AutoFade)
        FadeController.Instance.FadeTo(Color.clear, 1f);
      ((Behaviour) this).enabled = false;
      if (!is_replay)
        return;
      UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_MOVIE_REPLAY"), new UIUtility.DialogResultEvent(this.OnRetry), new UIUtility.DialogResultEvent(this.OnCancelReplay), systemModal: true);
    }

    [DebuggerHidden]
    private IEnumerator PlayDelayed(string filename, StreamingMovie.OnFinished callback)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginInfoWindow.\u003CPlayDelayed\u003Ec__Iterator0()
      {
        filename = filename,
        callback = callback
      };
    }

    private void OnMoveScene()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (this.mSelectScene == LoginInfoParam.SelectScene.EventQuest && LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest) || this.mSelectScene == LoginInfoParam.SelectScene.TowerQuest && LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.TowerQuest))
        return;
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
      return (IEnumerator) new LoginInfoWindow.\u003CLoadImages\u003Ec__Iterator1()
      {
        path = path,
        img = img,
        \u0024this = this
      };
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
