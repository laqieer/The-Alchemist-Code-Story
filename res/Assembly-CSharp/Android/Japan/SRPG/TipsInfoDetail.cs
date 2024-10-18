// Decompiled with JetBrains decompiler
// Type: SRPG.TipsInfoDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "次のページボタン", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "前のページボタン", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "既読送信の前準備", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "既読送信", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1000, "何もしない", FlowNode.PinTypes.Output, 1000)]
  public class TipsInfoDetail : MonoBehaviour, IFlowInterface
  {
    private List<Toggle> mToggleIconList = new List<Toggle>();
    private List<GameObject> mContents = new List<GameObject>();
    private const int PIN_INIT = 0;
    private const int PIN_NEXT_BUTTON = 1;
    private const int PIN_PREV_BUTTON = 2;
    private const int PIN_REQUEST = 10;
    private const int PIN_OUT_REQUEST = 100;
    private const int PIN_OUT_DONOTHING = 1000;
    [SerializeField]
    private GameObject ContentsHolder;
    [SerializeField]
    private GameObject ContentTemplate;
    [SerializeField]
    private ScrollAutoFit ScrollController;
    [SerializeField]
    private Button PrevButton;
    [SerializeField]
    private Button NextButton;
    [SerializeField]
    private GameObject ParentPageIcon;
    [SerializeField]
    private GameObject TemplatePageIcon;
    [SerializeField]
    private Button CloseButton;
    [SerializeField]
    private BackHandler CloseButtonBackHandler;
    [SerializeField]
    private Text TitleText;
    private TipsParam mTipsParam;
    private int mCurrentPageIndex;
    private bool mInitialized;

    private void Awake()
    {
      this.ScrollController.UseAutoFit = false;
      if ((UnityEngine.Object) this.ContentTemplate != (UnityEngine.Object) null)
        this.ContentTemplate.SetActive(false);
      if ((UnityEngine.Object) this.TemplatePageIcon != (UnityEngine.Object) null)
        this.TemplatePageIcon.SetActive(false);
      this.mTipsParam = ((IEnumerable<TipsParam>) MonoSingleton<GameManager>.Instance.MasterParam.Tips).FirstOrDefault<TipsParam>((Func<TipsParam, bool>) (t => t.iname == GlobalVars.SelectTips));
      if ((UnityEngine.Object) this.TitleText != (UnityEngine.Object) null)
        this.TitleText.text = this.mTipsParam.title;
      this.NextButton.interactable = false;
      this.PrevButton.interactable = false;
      this.EnabledCloseButton(false);
    }

    private void Initialize()
    {
      if ((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null)
        this.EnabledCloseButton(MonoSingleton<GameManager>.Instance.Tips.Contains(this.mTipsParam.iname));
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("Tips/tips_images");
      if ((UnityEngine.Object) spriteSheet != (UnityEngine.Object) null && this.mTipsParam.images != null)
      {
        foreach (string image in this.mTipsParam.images)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ContentTemplate);
          gameObject.GetComponent<Image>().sprite = spriteSheet.GetSprite(image);
          gameObject.transform.SetParent(this.ContentsHolder.transform, false);
          gameObject.SetActive(true);
          this.mContents.Add(gameObject);
        }
      }
      for (int index = 0; index < this.mContents.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TemplatePageIcon);
        Vector2 localScale = (Vector2) gameObject.transform.localScale;
        gameObject.transform.SetParent(this.ParentPageIcon.transform);
        gameObject.transform.localScale = (Vector3) localScale;
        gameObject.gameObject.SetActive(true);
        gameObject.name = this.TemplatePageIcon.name + (index + 1).ToString();
        this.mToggleIconList.Add(gameObject.GetComponent<Toggle>());
      }
      this.mToggleIconList[0].isOn = true;
      this.mCurrentPageIndex = 0;
      this.ChangeButtonInteractable();
      if (this.mContents.Count <= 1)
        this.ScrollController.movementType = ScrollRect.MovementType.Clamped;
      LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
      this.ScrollController.horizontalNormalizedPosition = 0.0f;
      this.ScrollController.verticalNormalizedPosition = 0.0f;
      this.ContentsHolder.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
      this.ScrollController.velocity = Vector2.zero;
      this.ScrollController.StopMovement();
      this.ScrollController.UseAutoFit = true;
      this.mInitialized = true;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Initialize();
          break;
        case 1:
          if (this.mCurrentPageIndex >= this.mContents.Count - 1)
            break;
          ++this.mCurrentPageIndex;
          this.ScrollController.SetScrollToHorizontal(this.mCurrentPageIndex);
          this.ChangeButtonInteractable();
          this.ChangeDot();
          break;
        case 2:
          if (this.mCurrentPageIndex <= 0)
            break;
          --this.mCurrentPageIndex;
          this.ScrollController.SetScrollToHorizontal(this.mCurrentPageIndex);
          this.ChangeButtonInteractable();
          this.ChangeDot();
          break;
        case 10:
          if (!MonoSingleton<GameManager>.Instance.Tips.Contains(this.mTipsParam.iname))
          {
            GlobalVars.RequestTips = this.mTipsParam.iname;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
          break;
      }
    }

    public void OnPageChanged(float value)
    {
      if (!this.mInitialized)
        return;
      float num1 = 1f / (float) Mathf.Max(this.mContents.Count - 1, 1);
      float num2 = (float) this.mCurrentPageIndex * num1;
      float num3 = Mathf.Abs(value - num2);
      Debug.Log((object) ("diff=" + (object) num3));
      if ((double) num3 < (double) num1 * 0.899999976158142)
        return;
      int currentPageIndex = this.mCurrentPageIndex;
      int num4 = (double) num2 - (double) value >= 0.0 ? currentPageIndex - 1 : currentPageIndex + 1;
      if (num4 < 0)
        num4 = 0;
      else if (num4 > this.mContents.Count - 1)
        num4 = this.mContents.Count - 1;
      if (num4 == this.mCurrentPageIndex)
        return;
      this.mCurrentPageIndex = num4;
      this.ChangeButtonInteractable();
      this.ChangeDot();
    }

    private void ChangeButtonInteractable()
    {
      this.NextButton.interactable = false;
      this.PrevButton.interactable = false;
      if (this.mCurrentPageIndex < this.mContents.Count - 1)
        this.NextButton.interactable = true;
      if (this.mCurrentPageIndex > 0)
        this.PrevButton.interactable = true;
      if (this.mCurrentPageIndex < this.mContents.Count - 1)
        return;
      this.EnabledCloseButton(true);
    }

    private void ChangeDot()
    {
      for (int index = 0; index < this.mToggleIconList.Count; ++index)
        this.mToggleIconList[index].isOn = index == this.mCurrentPageIndex;
    }

    private void EnabledCloseButton(bool isEnable)
    {
      this.CloseButton.interactable = isEnable;
      this.CloseButtonBackHandler.enabled = isEnable;
    }
  }
}
