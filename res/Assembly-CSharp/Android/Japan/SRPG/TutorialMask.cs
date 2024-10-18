// Decompiled with JetBrains decompiler
// Type: SRPG.TutorialMask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class TutorialMask : MonoBehaviour
  {
    private const string DESTROY_MASK_EVENT_NAME = "CLOSE_TUTORIAL_MASK";
    [SerializeField]
    private GameObject mMask;
    [SerializeField]
    private Button mEnableArea;
    [SerializeField]
    private Button[] mDisableAreas;
    [SerializeField]
    private GameObject mArrow;
    [SerializeField]
    private GameObject mTextRoot;
    [SerializeField]
    private Text mText;
    private bool mIsFinishSetup;
    private RectTransform mMaskRectTransform;
    private Vector3 mTargetWorldPos;
    private Vector3 mTargetScreenPos;
    private TutorialMask.eActionType mActionType;
    private bool mIsWorld2Screen;
    private float mNoResponseTime;
    private Vector2 mMaskSize;
    private Animator mAnimator;
    public TutorialMask.OpendMethod mOpendMethod;

    private void Awake()
    {
      if ((UnityEngine.Object) this.mMask != (UnityEngine.Object) null)
      {
        this.mMaskRectTransform = this.mMask.transform as RectTransform;
        this.mMask.SetActive(false);
      }
      this.mAnimator = this.GetComponent<Animator>();
    }

    private void Update()
    {
      if (!this.mIsFinishSetup)
        return;
      RectTransform transform = this.transform as RectTransform;
      Vector3 vector3_1 = new Vector3(this.mTargetWorldPos.x, this.mTargetWorldPos.y, this.mTargetWorldPos.z);
      if (this.mIsWorld2Screen)
      {
        vector3_1 = (Vector3) RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, vector3_1);
        this.mTargetScreenPos = vector3_1;
      }
      Vector3 vector3_2 = transform.InverseTransformPoint(vector3_1);
      this.mMaskRectTransform.anchoredPosition = (Vector2) new Vector3(vector3_2.x, vector3_2.y, vector3_2.z);
      this.mNoResponseTime = Mathf.Max(0.0f, this.mNoResponseTime - Time.deltaTime);
      this.Resize();
      if (this.mOpendMethod == null || !((UnityEngine.Object) this.mAnimator != (UnityEngine.Object) null) || (double) this.mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
        return;
      this.mOpendMethod();
      this.mOpendMethod = (TutorialMask.OpendMethod) null;
    }

    public void Setup(TutorialMask.eActionType act_type, Vector3 world_pos, bool is_world2screen, string text = null)
    {
      if ((UnityEngine.Object) this.mMask == (UnityEngine.Object) null || (UnityEngine.Object) this.mMaskRectTransform == (UnityEngine.Object) null)
        return;
      this.mMask.SetActive(true);
      this.mIsFinishSetup = true;
      this.mTargetWorldPos = world_pos;
      this.mActionType = act_type;
      this.mIsWorld2Screen = is_world2screen;
      bool flag = !string.IsNullOrEmpty(text);
      this.mArrow.gameObject.SetActive(!flag);
      this.mTextRoot.gameObject.SetActive(flag);
      if (flag)
        this.mText.text = text;
      this.mEnableArea.onClick.AddListener(new UnityAction(this.OnClick_EnableArea));
      for (int index = 0; index < this.mDisableAreas.Length; ++index)
        this.mDisableAreas[index].onClick.AddListener(new UnityAction(this.OnClick_DisableArea));
    }

    public void SetupMaskSize(Vector2 size)
    {
      this.mMaskSize = size;
    }

    private void Resize()
    {
      if ((double) this.mMaskSize.x == 0.0 && (double) this.mMaskSize.y == 0.0)
        return;
      Vector2 vector2_1 = new Vector2(this.mMaskRectTransform.anchoredPosition.x - this.mMaskSize.x / 2f, this.mMaskRectTransform.anchoredPosition.y - this.mMaskSize.y / 2f);
      Vector2 vector2_2 = new Vector2(this.mMaskRectTransform.anchoredPosition.x + this.mMaskSize.x / 2f, this.mMaskRectTransform.anchoredPosition.y + this.mMaskSize.y / 2f);
      RectTransform transform = this.transform as RectTransform;
      Vector3 vector3_1 = transform.InverseTransformPoint((Vector3) vector2_1);
      Vector3 vector3_2 = transform.InverseTransformPoint((Vector3) vector2_2);
      (this.mMask.transform as RectTransform).sizeDelta = new Vector2(Mathf.Abs(vector3_2.x - vector3_1.x), Mathf.Abs(vector3_2.y - vector3_1.y));
    }

    public void SetupNoResponseTime(float second)
    {
      this.mNoResponseTime = second;
    }

    private void OnClick_EnableArea()
    {
      if ((double) this.mNoResponseTime > 0.0)
        return;
      switch (this.mActionType)
      {
        case TutorialMask.eActionType.MOVE_UNIT:
          this.MoveUnit();
          break;
        case TutorialMask.eActionType.ATTACK_TARGET_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.NORMAL_ATTACK_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.ABILITY_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.TAP_NORMAL_ATTACK:
          this.TapNormalAtk();
          break;
        case TutorialMask.eActionType.EXEC_NORMAL_ATTACK:
          this.ExecNormalAtk();
          break;
        case TutorialMask.eActionType.SELECT_DIR:
          this.SelectDir();
          break;
      }
    }

    private void OnClick_DisableArea()
    {
      if ((double) this.mNoResponseTime > 0.0)
        return;
      switch (this.mActionType)
      {
        case TutorialMask.eActionType.ATTACK_TARGET_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.NORMAL_ATTACK_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.ABILITY_DESC:
          this.DestroyMask();
          break;
      }
    }

    private void MoveUnit()
    {
      if (!((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null) || SceneBattle.Instance.VirtualStickMoveInput == null)
        return;
      SceneBattle.Instance.VirtualStickMoveInput.MoveUnit(this.mTargetScreenPos);
      this.DestroyMask();
    }

    private void TapNormalAtk()
    {
      SceneBattle.Instance.BattleUI.CommandWindow.AttackButton.GetComponentInChildren<Button>().onClick.Invoke();
      this.DestroyMask();
    }

    private void ExecNormalAtk()
    {
      SceneBattle.Instance.BattleUI.CommandWindow.OKButton.GetComponentInChildren<Button>().onClick.Invoke();
      this.DestroyMask();
    }

    private void SelectDir()
    {
      this.DestroyMask();
    }

    private void DestroyMask()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CLOSE_TUTORIAL_MASK");
    }

    public enum eActionType
    {
      MOVE_UNIT,
      ATTACK_TARGET_DESC,
      NORMAL_ATTACK_DESC,
      ABILITY_DESC,
      TAP_NORMAL_ATTACK,
      EXEC_NORMAL_ATTACK,
      SELECT_DIR,
    }

    public delegate void OpendMethod();
  }
}
