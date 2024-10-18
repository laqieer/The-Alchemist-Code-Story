// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilityListItemEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitAbilityListItemEvents : ListItemEvents
  {
    private UnitAbilityListItemEvents.ListItemTouchController mTouchController;
    public ListItemEvents.ListItemEvent OnRankUp;
    public ListItemEvents.ListItemEvent OnRankUpBtnPress;
    public ListItemEvents.ListItemEvent OnRankUpBtnUp;
    [HelpBox("アビリティをランクアップ可能であればこのゲームオブジェクトを選択可能にします。")]
    public Selectable RankupButton;
    public RectTransform AbilityPoint;
    public GameObject LabelLevel;
    public GameObject LabelLevelMax;
    private float mLastUpdateTime;

    public UnitAbilityListItemEvents.ListItemTouchController ItemTouchController
    {
      get => this.mTouchController;
    }

    private void Start()
    {
      this.UpdateItemStates();
      if (!Object.op_Inequality((Object) this.RankupButton, (Object) null))
        return;
      this.mTouchController = ((Component) this.RankupButton).gameObject.AddComponent<UnitAbilityListItemEvents.ListItemTouchController>();
      this.mTouchController.OnPointerDownFunc = new UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerDown(this.RankUpPress);
      this.mTouchController.OnPointerUpFunc = new UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerUp(this.RankUpUp);
      this.mTouchController.RankUpFunc = new UnitAbilityListItemEvents.ListItemTouchController.DelegateRankUp(this.RankUp);
    }

    private void OnEnable()
    {
      MonoSingleton<GameManager>.Instance.OnAbilityRankUpCountChange += new GameManager.RankUpCountChangeEvent(this.OnRankUpCountChange);
    }

    private void OnDisable()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnAbilityRankUpCountChange -= new GameManager.RankUpCountChangeEvent(this.OnRankUpCountChange);
    }

    private void OnRankUpCountChange(int count) => this.UpdateItemStates();

    private void RankUpPress(
      UnitAbilityListItemEvents.ListItemTouchController controller)
    {
      if (this.OnRankUpBtnPress == null)
        return;
      this.OnRankUpBtnPress(((Component) this).gameObject);
    }

    private void RankUpUp(
      UnitAbilityListItemEvents.ListItemTouchController controller)
    {
      if (this.OnRankUpBtnUp == null)
        return;
      this.OnRankUpBtnUp(((Component) this).gameObject);
    }

    public void RankUp(
      UnitAbilityListItemEvents.ListItemTouchController controller)
    {
      if (this.OnRankUp == null)
        return;
      this.OnRankUp(((Component) this).gameObject);
    }

    private void UpdateItemStates()
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(((Component) this).gameObject, (AbilityData) null);
      if (dataOfClass == null)
        return;
      bool flag = dataOfClass.Rank >= dataOfClass.GetRankMaxCap();
      if (Object.op_Inequality((Object) this.LabelLevel, (Object) null))
        this.LabelLevel.SetActive(!flag);
      if (Object.op_Inequality((Object) this.LabelLevelMax, (Object) null))
        this.LabelLevelMax.SetActive(flag);
      if (Object.op_Inequality((Object) this.RankupButton, (Object) null))
      {
        ((Component) this.RankupButton).gameObject.SetActive(dataOfClass.Rank < dataOfClass.GetRankCap());
        this.RankupButton.interactable = true & MonoSingleton<GameManager>.Instance.Player.CheckRankUpAbility(dataOfClass);
      }
      if (!Object.op_Inequality((Object) this.AbilityPoint, (Object) null))
        return;
      ((Component) this.AbilityPoint).gameObject.SetActive(dataOfClass.Rank < dataOfClass.GetRankCap());
    }

    public class ListItemTouchController : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
    {
      public UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerDown OnPointerDownFunc;
      public UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerUp OnPointerUpFunc;
      public UnitAbilityListItemEvents.ListItemTouchController.DelegateRankUp RankUpFunc;
      public float HoldDuration;
      public float HoldSpan = 0.25f;
      public bool Holding;
      public bool IsFirstDownFunc;
      private static readonly float FirstSpan = 0.3f;
      private static readonly float SecondSpanMax = 0.3f;
      private static readonly float SecondSpanMin = 0.1f;
      private static readonly float SecondSpanOffset = 0.1f;
      private Vector2 mDragStartPos;

      public void OnPointerDown(PointerEventData eventData)
      {
        if (this.OnPointerDownFunc == null)
          return;
        this.StatusReset();
        this.OnPointerDownFunc(this);
        this.Holding = true;
        this.mDragStartPos = eventData.position;
      }

      public void OnPointerUp()
      {
        if (this.OnPointerUpFunc != null)
          this.OnPointerUpFunc(this);
        this.StatusReset();
      }

      public void OnDestroy()
      {
        this.StatusReset();
        if (this.OnPointerDownFunc != null)
          this.OnPointerDownFunc = (UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerDown) null;
        if (this.OnPointerUpFunc == null)
          return;
        this.OnPointerUpFunc = (UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerUp) null;
      }

      public void UpdatePress(float deltaTime)
      {
        bool mouseButton = Input.GetMouseButton(0);
        if (this.Holding && !mouseButton)
        {
          if ((double) this.HoldDuration < (double) this.HoldSpan)
            this.RankUpFunc(this);
          this.OnPointerUp();
        }
        else
        {
          GameSettings instance = GameSettings.Instance;
          float num = (float) (instance.HoldMargin * instance.HoldMargin);
          Vector2 vector2 = Vector2.op_Subtraction(this.mDragStartPos, Vector2.op_Implicit(Input.mousePosition));
          bool flag = (double) ((Vector2) ref vector2).sqrMagnitude > (double) num;
          if ((double) this.HoldDuration < (double) this.HoldSpan && flag)
          {
            this.OnPointerUp();
          }
          else
          {
            if (!this.Holding)
              return;
            this.HoldDuration += deltaTime;
            if ((double) this.HoldDuration < (double) this.HoldSpan)
              return;
            this.HoldDuration -= this.HoldSpan;
            if (!this.IsFirstDownFunc)
            {
              this.IsFirstDownFunc = true;
              this.HoldSpan = UnitAbilityListItemEvents.ListItemTouchController.SecondSpanMax;
            }
            else
            {
              this.HoldSpan -= UnitAbilityListItemEvents.ListItemTouchController.SecondSpanOffset;
              this.HoldSpan = Mathf.Max(UnitAbilityListItemEvents.ListItemTouchController.SecondSpanMin, this.HoldSpan);
            }
            this.RankUpFunc(this);
          }
        }
      }

      public void StatusReset()
      {
        this.HoldDuration = 0.0f;
        this.Holding = false;
        this.HoldSpan = UnitAbilityListItemEvents.ListItemTouchController.FirstSpan;
        this.IsFirstDownFunc = false;
        ((Vector2) ref this.mDragStartPos).Set(0.0f, 0.0f);
      }

      public delegate void DelegateOnPointerDown(
        UnitAbilityListItemEvents.ListItemTouchController controller);

      public delegate void DelegateOnPointerUp(
        UnitAbilityListItemEvents.ListItemTouchController controller);

      public delegate void DelegateRankUp(
        UnitAbilityListItemEvents.ListItemTouchController controller);
    }
  }
}
