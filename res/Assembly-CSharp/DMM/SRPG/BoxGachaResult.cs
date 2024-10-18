// Decompiled with JetBrains decompiler
// Type: SRPG.BoxGachaResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/BoxGachaResult", 32741)]
  [FlowNode.Pin(0, "IN 演出初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "OT 演出初期化", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "IN アイコン表示開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "OT アイコン表示終了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(2, "IN スキップ", FlowNode.PinTypes.Input, 2)]
  public class BoxGachaResult : FlowNodePersistent
  {
    private const int PIN_IN_INITALIZE_ICON_ANIM = 0;
    private const int PIN_IN_PLAY_ICON_ANIM = 1;
    private const int PIN_IN_SKIP_ICON_ANIM = 2;
    private const int PIN_OT_INITALIZE_ICON_ANIM = 10;
    private const int PIN_OT_PLAY_ICON_ANIM = 11;
    [SerializeField]
    private string m_TemplateIconPath = "UI/GenesisRewardIcon";
    [SerializeField]
    [Header("アイコンの親リスト(上段)")]
    private GameObject m_ListParent;
    [SerializeField]
    [Header("アイコンの親リスト(下段)")]
    private GameObject m_ListParent2;
    [SerializeField]
    [Header("アイコンテンプレート")]
    private GameObject m_TemplateItem;
    [SerializeField]
    [Header("親オブジェクト")]
    private GameObject m_Window;
    [SerializeField]
    [Header("エフェクトテンプレート")]
    private GameObject m_TemplateEffect;
    [Space(10f)]
    [SerializeField]
    [Header("アイコンアニメ再生前のInterval")]
    private float m_StartInterval;
    [SerializeField]
    [Header("次のアイコンアニメ再生までのInterval")]
    private float m_IconOpenInterval;
    [SerializeField]
    [Header("アイコンアニメ再生後のInterval")]
    private float m_EndInterval;
    [SerializeField]
    [Header("アイコンアニメ終了と判断するState名")]
    private string[] m_EndIconAnimStateName;
    [SerializeField]
    [Header("通常再生用のTrigger")]
    private string m_IconAnimDefaultTrigger = "default";
    [SerializeField]
    [Header("PickUp再生用のTrigger")]
    private string m_IconAnimPickupTrigger = "pickup";
    [SerializeField]
    [Header("アイコンアニメスキップ用のTrigger")]
    private string m_IconAnimSkipTrigger = "skip";
    [SerializeField]
    [Header("Pickupアイコンアニメスキップ用のTrigger")]
    private string m_IconAnimPickupSkipTrigger = "skip_pickup";
    private List<GameObject> m_IconList;
    private float m_Timer;
    private Coroutine m_IconAnimCoroutine;
    private List<GameObject> m_ActiveIcons = new List<GameObject>();

    protected override void Awake()
    {
      if (!Object.op_Inequality((Object) this.m_TemplateItem, (Object) null))
        return;
      this.m_TemplateItem.SetActive(false);
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.StartCoroutine(this.Initalize());
          break;
        case 1:
          if (this.m_IconAnimCoroutine != null)
            this.StopCoroutine(this.m_IconAnimCoroutine);
          this.m_IconAnimCoroutine = this.StartCoroutine(this.IconAnimation());
          break;
        case 2:
          if (this.m_IconAnimCoroutine != null)
            this.StopCoroutine(this.m_IconAnimCoroutine);
          this.IconAnimSkip();
          this.ActivateOutputLinks(11);
          break;
      }
    }

    [DebuggerHidden]
    private IEnumerator Initalize()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new BoxGachaResult.\u003CInitalize\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator IconAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new BoxGachaResult.\u003CIconAnimation\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    private bool IsAnimEnd(Animator anim)
    {
      bool flag = false;
      if (this.m_EndIconAnimStateName == null || this.m_EndIconAnimStateName.Length <= 0)
        flag = true;
      else if (!GameUtility.IsAnimatorRunning((Component) anim))
      {
        for (int index = 0; index < this.m_EndIconAnimStateName.Length; ++index)
        {
          if (GameUtility.CompareAnimatorStateName((Component) anim, this.m_EndIconAnimStateName[index]))
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    private void IconAnimSkip()
    {
      if (string.IsNullOrEmpty(this.m_IconAnimSkipTrigger))
      {
        DebugUtility.LogError("SKIP用のTriggerが指定されていません.");
      }
      else
      {
        if (this.m_IconList != null)
        {
          for (int index = 0; index < this.m_IconList.Count; ++index)
          {
            string str = this.m_IconAnimSkipTrigger;
            SerializeValueBehaviour component1 = this.m_IconList[index].GetComponent<SerializeValueBehaviour>();
            if (Object.op_Inequality((Object) component1, (Object) null))
              str = !component1.list.GetBool("is_pickup") ? this.m_IconAnimSkipTrigger : this.m_IconAnimPickupSkipTrigger;
            Animator component2 = this.m_IconList[index].GetComponent<Animator>();
            if (Object.op_Inequality((Object) component2, (Object) null))
              component2.SetTrigger(str);
          }
        }
        if (!Object.op_Inequality((Object) this.m_Window, (Object) null))
          return;
        Animator component = this.m_Window.GetComponent<Animator>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.SetTrigger(this.m_IconAnimSkipTrigger);
      }
    }

    private bool IsNeedActiveIcon(int index, int drop_count)
    {
      bool flag;
      if (drop_count < 5)
      {
        flag = drop_count > index;
      }
      else
      {
        switch (drop_count)
        {
          case 5:
            flag = index <= 5 ? index % 5 < 3 : index % 5 < 2;
            break;
          case 6:
            flag = index % 5 < 3;
            break;
          case 7:
            flag = index <= 5 ? index % 5 < 4 : index % 5 < 3;
            break;
          case 8:
            flag = index % 5 < 4;
            break;
          default:
            flag = index / drop_count <= 0;
            break;
        }
      }
      return flag;
    }
  }
}
