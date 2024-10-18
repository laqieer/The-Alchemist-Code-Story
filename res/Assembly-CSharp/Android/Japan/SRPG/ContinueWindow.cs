// Decompiled with JetBrains decompiler
// Type: SRPG.ContinueWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ContinueWindow : MonoBehaviour
  {
    private List<GameObject> m_TreasureListNodes = new List<GameObject>();
    public Text Message;
    public Text CoinNum;
    public Button ButtonOk;
    public Button ButtonCancel;
    public GameObject Prefab_NewItemBadge;
    public GameObject TreasureList;
    public GameObject TreasureListNode;
    private Animator m_Animator;
    private ContinueWindow.Result m_Result;
    private bool m_Destroy;
    private ContinueWindow.ResultEvent OnClickOk;
    private ContinueWindow.ResultEvent OnClickCancel;
    private static Canvas m_ModalCanvas;

    public static bool Create(GameObject res, ContinueWindow.ResultEvent ok, ContinueWindow.ResultEvent cancel)
    {
      ContinueWindow.Destroy();
      GameObject original = res;
      if ((UnityEngine.Object) original != (UnityEngine.Object) null)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
        if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
        {
          ContinueWindow.m_ModalCanvas = UIUtility.PushCanvas(false, -1);
          gameObject.transform.SetParent(ContinueWindow.m_ModalCanvas.transform, false);
          ContinueWindow component = gameObject.GetComponent<ContinueWindow>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            component.OnClickOk = ok;
            component.OnClickCancel = cancel;
            component.Open();
            return true;
          }
          ContinueWindow.Destroy();
        }
      }
      return false;
    }

    public static void Destroy()
    {
      if ((UnityEngine.Object) ContinueWindow.m_ModalCanvas != (UnityEngine.Object) null)
      {
        UIUtility.PopCanvas(true);
        UnityEngine.Object.Destroy((UnityEngine.Object) ContinueWindow.m_ModalCanvas.gameObject);
      }
      ContinueWindow.m_ModalCanvas = (Canvas) null;
    }

    public static void ForceClose()
    {
      if (!((UnityEngine.Object) ContinueWindow.m_ModalCanvas != (UnityEngine.Object) null))
        return;
      ContinueWindow componentInChildren = ContinueWindow.m_ModalCanvas.gameObject.GetComponentInChildren<ContinueWindow>();
      if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null))
        return;
      componentInChildren.Close(true);
    }

    private void Start()
    {
      SceneBattle instance = SceneBattle.Instance;
      this.m_Animator = this.GetComponent<Animator>();
      if ((UnityEngine.Object) this.Message != (UnityEngine.Object) null)
      {
        this.Message.text = LocalizedText.Get("sys.CONTINUE_MSG", new object[1]
        {
          (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost
        });
        if ((UnityEngine.Object) instance != (UnityEngine.Object) null && instance.Battle != null)
        {
          if (instance.Battle.IsMultiTower)
            this.Message.text = LocalizedText.Get("sys.CONTINUE_MSG", new object[1]
            {
              (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMultiTower
            });
          else if (instance.Battle.IsMultiPlay)
            this.Message.text = LocalizedText.Get("sys.CONTINUE_MSG", new object[1]
            {
              (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMulti
            });
        }
      }
      if ((UnityEngine.Object) this.CoinNum != (UnityEngine.Object) null)
        this.CoinNum.text = MonoSingleton<GameManager>.Instance.Player.Coin.ToString();
      ConfigWindow.SetupTreasureList(this.TreasureList, this.TreasureListNode, this.Prefab_NewItemBadge, this.gameObject, this.m_TreasureListNodes);
      UIUtility.AddEventListener(this.ButtonOk.gameObject, (UnityEvent) this.ButtonOk.onClick, new UIUtility.EventListener(this.OnClickButton));
      UIUtility.AddEventListener(this.ButtonCancel.gameObject, (UnityEvent) this.ButtonCancel.onClick, new UIUtility.EventListener(this.OnClickButton));
      GameParameter.UpdateAll(this.gameObject);
    }

    public void Update()
    {
      if (!this.IsClosed() || !this.m_Destroy)
        return;
      if (this.m_Result == ContinueWindow.Result.OK)
      {
        if (this.OnClickOk != null)
          this.OnClickOk(this.gameObject);
      }
      else if (this.m_Result == ContinueWindow.Result.CANCEL && this.OnClickCancel != null)
        this.OnClickCancel(this.gameObject);
      ContinueWindow.Destroy();
    }

    public void Open()
    {
      if ((UnityEngine.Object) this.m_Animator != (UnityEngine.Object) null)
        this.m_Animator.SetBool("close", false);
      this.m_Result = ContinueWindow.Result.NONE;
      this.m_Destroy = false;
    }

    public void Close(bool destroy)
    {
      if ((UnityEngine.Object) this.m_Animator != (UnityEngine.Object) null)
        this.m_Animator.SetBool("close", true);
      this.m_Destroy = destroy;
    }

    public bool IsOpend()
    {
      if ((UnityEngine.Object) this.m_Animator == (UnityEngine.Object) null || (UnityEngine.Object) this.m_Animator.runtimeAnimatorController == (UnityEngine.Object) null || (this.m_Animator.runtimeAnimatorController.animationClips == null || this.m_Animator.runtimeAnimatorController.animationClips.Length == 0))
        return true;
      if (!this.m_Animator.GetBool("close"))
        return this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("opened");
      return false;
    }

    public bool IsClosed()
    {
      if ((UnityEngine.Object) this.m_Animator == (UnityEngine.Object) null || (UnityEngine.Object) this.m_Animator.runtimeAnimatorController == (UnityEngine.Object) null || (this.m_Animator.runtimeAnimatorController.animationClips == null || this.m_Animator.runtimeAnimatorController.animationClips.Length == 0))
        return true;
      if (this.m_Animator.GetBool("close"))
        return this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("closed");
      return false;
    }

    private void OnClickButton(GameObject obj)
    {
      this.Close(true);
      if ((UnityEngine.Object) obj == (UnityEngine.Object) this.ButtonOk.gameObject)
        this.m_Result = ContinueWindow.Result.OK;
      else
        this.m_Result = ContinueWindow.Result.CANCEL;
    }

    public delegate void ResultEvent(GameObject dialog);

    private enum Result
    {
      NONE,
      OK,
      CANCEL,
    }
  }
}
