// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilityListItemEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
      get
      {
        return this.mTouchController;
      }
    }

    private void Start()
    {
      this.UpdateItemStates();
      if (!((UnityEngine.Object) this.RankupButton != (UnityEngine.Object) null))
        return;
      this.mTouchController = this.RankupButton.gameObject.AddComponent<UnitAbilityListItemEvents.ListItemTouchController>();
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
      if (!((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() != (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnAbilityRankUpCountChange -= new GameManager.RankUpCountChangeEvent(this.OnRankUpCountChange);
    }

    private void OnRankUpCountChange(int count)
    {
      this.UpdateItemStates();
    }

    private void RankUpPress(UnitAbilityListItemEvents.ListItemTouchController controller)
    {
      if (this.OnRankUpBtnPress == null)
        return;
      this.OnRankUpBtnPress(this.gameObject);
    }

    private void RankUpUp(UnitAbilityListItemEvents.ListItemTouchController controller)
    {
      if (this.OnRankUpBtnUp == null)
        return;
      this.OnRankUpBtnUp(this.gameObject);
    }

    public void RankUp(UnitAbilityListItemEvents.ListItemTouchController controller)
    {
      if (this.OnRankUp == null)
        return;
      this.OnRankUp(this.gameObject);
    }

    private void UpdateItemStates()
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(this.gameObject, (AbilityData) null);
      if (dataOfClass == null)
        return;
      bool flag = dataOfClass.Rank >= dataOfClass.GetRankMaxCap();
      if ((UnityEngine.Object) this.LabelLevel != (UnityEngine.Object) null)
        this.LabelLevel.SetActive(!flag);
      if ((UnityEngine.Object) this.LabelLevelMax != (UnityEngine.Object) null)
        this.LabelLevelMax.SetActive(flag);
      if ((UnityEngine.Object) this.RankupButton != (UnityEngine.Object) null)
      {
        this.RankupButton.gameObject.SetActive(dataOfClass.Rank < dataOfClass.GetRankCap());
        this.RankupButton.interactable = true & MonoSingleton<GameManager>.Instance.Player.CheckRankUpAbility(dataOfClass);
      }
      if (!((UnityEngine.Object) this.AbilityPoint != (UnityEngine.Object) null))
        return;
      this.AbilityPoint.gameObject.SetActive(dataOfClass.Rank < dataOfClass.GetRankCap());
    }

    public class ListItemTouchController : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
    {
      private static readonly float FirstSpan = 0.3f;
      private static readonly float SecondSpanMax = 0.3f;
      private static readonly float SecondSpanMin = 0.1f;
      private static readonly float SecondSpanOffset = 0.1f;
      public float HoldSpan = 0.25f;
      public UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerDown OnPointerDownFunc;
      public UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerUp OnPointerUpFunc;
      public UnitAbilityListItemEvents.ListItemTouchController.DelegateRankUp RankUpFunc;
      public float HoldDuration;
      public bool Holding;
      public bool IsFirstDownFunc;
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
        if (this.Holding && !Input.GetMouseButton(0))
        {
          if ((double) this.HoldDuration < (double) this.HoldSpan)
            this.RankUpFunc(this);
          this.OnPointerUp();
        }
        else
        {
          GameSettings instance = GameSettings.Instance;
          if ((double) this.HoldDuration < (double) this.HoldSpan && (double) (this.mDragStartPos - (Vector2) Input.mousePosition).sqrMagnitude > (double) (instance.HoldMargin * instance.HoldMargin))
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
        this.mDragStartPos.Set(0.0f, 0.0f);
      }

      public delegate void DelegateOnPointerDown(UnitAbilityListItemEvents.ListItemTouchController controller);

      public delegate void DelegateOnPointerUp(UnitAbilityListItemEvents.ListItemTouchController controller);

      public delegate void DelegateRankUp(UnitAbilityListItemEvents.ListItemTouchController controller);
    }
  }
}
