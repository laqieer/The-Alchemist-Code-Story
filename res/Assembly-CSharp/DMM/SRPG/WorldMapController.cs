// Decompiled with JetBrains decompiler
// Type: SRPG.WorldMapController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class WorldMapController : MonoBehaviour
  {
    public AreaMapController[] AreaMaps = new AreaMapController[0];
    public RawImage[] Images = new RawImage[0];
    public RadialBlurEffect RadialBlurEffect;
    public bool AutoSelectArea;
    private RectTransform mTransform;
    private Vector2 mInitScale;
    private Vector2 mInitPos;
    private AreaMapController mCurrentArea;
    private AreaMapController mPrevArea;
    private AreaMapController mNextArea;
    public float TransitionTime = 1f;
    public AnimationCurve RadialBlurCurve;
    private StateMachine<WorldMapController> mStateMachine;
    private static WorldMapController mInstance;

    public static WorldMapController FindInstance(string gameobjectID)
    {
      GameObject gameObject = GameObjectID.FindGameObject(gameobjectID);
      return Object.op_Inequality((Object) gameObject, (Object) null) ? gameObject.GetComponent<WorldMapController>() : (WorldMapController) null;
    }

    public static WorldMapController Instance => WorldMapController.mInstance;

    public void GotoArea(string areaID)
    {
      for (int index = 0; index < this.AreaMaps.Length; ++index)
      {
        if (this.AreaMaps[index].MapID == areaID)
        {
          this.mCurrentArea = this.AreaMaps[index];
          return;
        }
      }
      this.mCurrentArea = (AreaMapController) null;
    }

    private void Awake()
    {
      WorldMapController.mInstance = this;
      this.mTransform = ((Component) this).transform as RectTransform;
      this.mInitPos = new Vector2(this.mTransform.anchoredPosition.x, this.mTransform.anchoredPosition.y);
      this.mInitScale = new Vector2(((Transform) this.mTransform).localScale.x, ((Transform) this.mTransform).localScale.y);
    }

    public void Refresh()
    {
      this.mStateMachine = new StateMachine<WorldMapController>(this);
      bool flag = MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart();
      if (this.AutoSelectArea && !flag)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index1 = 0; index1 < instance.Chapters.Length; ++index1)
        {
          if (instance.Chapters[index1].section == (string) GlobalVars.SelectedSection && (instance.Chapters[index1].iname == (string) GlobalVars.SelectedChapter || string.IsNullOrEmpty((string) GlobalVars.SelectedChapter)))
          {
            for (int index2 = 0; index2 < this.AreaMaps.Length; ++index2)
            {
              if (this.AreaMaps[index2].MapID == instance.Chapters[index1].world)
              {
                this.mCurrentArea = this.AreaMaps[index2];
                break;
              }
            }
            break;
          }
        }
        if (Object.op_Inequality((Object) this.mCurrentArea, (Object) null))
        {
          this.mStateMachine.GotoState<WorldMapController.State_World2Area>();
          return;
        }
      }
      else if (flag)
        this.AutoSelectArea = false;
      this.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
    }

    private void Start() => this.Refresh();

    private void OnDestroy() => WorldMapController.mInstance = (WorldMapController) null;

    public void ResetAreaAll()
    {
      for (int index = 0; index < this.AreaMaps.Length; ++index)
      {
        if (!Object.op_Equality((Object) this.AreaMaps[index], (Object) null))
          this.AreaMaps[index].SetOpacity(0.0f);
      }
    }

    private void SetRadialBlurStrength(float t)
    {
      if (this.RadialBlurCurve != null && this.RadialBlurCurve.keys.Length > 0)
        this.RadialBlurEffect.Strength = this.RadialBlurCurve.Evaluate(t);
      else
        this.RadialBlurEffect.Strength = Mathf.Sin(t * 3.14159274f);
    }

    private void Update() => this.mStateMachine.Update();

    private class State_WorldSelect : State<WorldMapController>
    {
      public override void Begin(WorldMapController self)
      {
        if (!Object.op_Inequality((Object) self.mNextArea, (Object) null))
          return;
        self.mCurrentArea = self.mNextArea;
        self.mNextArea = (AreaMapController) null;
        self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
      }

      public override void Update(WorldMapController self)
      {
        if (!Object.op_Inequality((Object) self.mCurrentArea, (Object) null))
          return;
        self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
      }
    }

    private class State_World2Area : State<WorldMapController>
    {
      private float mTransition;
      private AreaMapController mTarget;
      private Vector2 mDesiredScale;
      private Vector2 mDesiredPosition;
      private Vector2 mTargetPosition;

      public override void Begin(WorldMapController self)
      {
        if (Object.op_Equality((Object) self.mCurrentArea, (Object) null))
          self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
        this.mTarget = self.mCurrentArea;
        RectTransform transform = ((Component) this.mTarget).transform as RectTransform;
        float num1 = 1f / ((Transform) transform).localScale.x * self.mInitScale.x;
        float num2 = 1f / ((Transform) transform).localScale.y * self.mInitScale.y;
        this.mDesiredScale = Vector2.op_Implicit(new Vector3(num1, num2));
        this.mTargetPosition = transform.anchoredPosition;
        this.mDesiredPosition = Vector2.op_UnaryNegation(transform.anchoredPosition);
        this.mDesiredPosition.x *= num1;
        this.mDesiredPosition.y *= num2;
        if (!self.AutoSelectArea)
          return;
        self.AutoSelectArea = false;
        this.mTarget.SetOpacity(1f);
        self.mTransform.anchoredPosition = this.mDesiredPosition;
        ((Transform) self.mTransform).localScale = Vector2.op_Implicit(this.mDesiredScale);
        self.mStateMachine.GotoState<WorldMapController.State_AreaSelect>();
      }

      public override void Update(WorldMapController self)
      {
        if (Object.op_Equality((Object) self.mCurrentArea, (Object) null))
        {
          self.mPrevArea = this.mTarget;
          self.mNextArea = (AreaMapController) null;
          self.mStateMachine.GotoState<WorldMapController.State_Area2World>();
        }
        else
        {
          this.mTransition = Mathf.Clamp01(this.mTransition + 1f / self.TransitionTime * Time.deltaTime);
          float opacity = Mathf.Sin((float) ((double) this.mTransition * 3.1415927410125732 * 0.5));
          this.mTarget.SetOpacity(opacity);
          self.mTransform.anchoredPosition = Vector2.Lerp(self.mInitPos, this.mDesiredPosition, opacity);
          ((Transform) self.mTransform).localScale = Vector2.op_Implicit(Vector2.Lerp(self.mInitScale, this.mDesiredScale, opacity));
          self.SetRadialBlurStrength(this.mTransition);
          if (Object.op_Inequality((Object) self.RadialBlurEffect, (Object) null))
          {
            double x = (double) this.mTargetPosition.x;
            Rect rect1 = self.mTransform.rect;
            double width = (double) ((Rect) ref rect1).width;
            float num1 = (float) (x / width + 0.5);
            double y = (double) this.mTargetPosition.y;
            Rect rect2 = self.mTransform.rect;
            double height = (double) ((Rect) ref rect2).height;
            float num2 = (float) (y / height + 0.5);
            self.RadialBlurEffect.Focus = new Vector2(num1, num2);
          }
          if ((double) this.mTransition < 1.0)
            return;
          self.mStateMachine.GotoState<WorldMapController.State_AreaSelect>();
        }
      }
    }

    private class State_AreaSelect : State<WorldMapController>
    {
      private AreaMapController mArea;

      public override void Begin(WorldMapController self) => this.mArea = self.mCurrentArea;

      public override void Update(WorldMapController self)
      {
        if (!Object.op_Inequality((Object) self.mCurrentArea, (Object) this.mArea))
          return;
        self.mPrevArea = this.mArea;
        self.mNextArea = self.mCurrentArea;
        self.mStateMachine.GotoState<WorldMapController.State_Area2World>();
      }
    }

    private class State_Area2World : State<WorldMapController>
    {
      private float mTransition;
      private Vector2 mStartScale;
      private Vector2 mStartPosition;

      public override void Begin(WorldMapController self)
      {
        if (Object.op_Inequality((Object) self.mCurrentArea, (Object) null))
        {
          self.mPrevArea.SetOpacity(0.0f);
          self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
        }
        else
        {
          this.mStartScale = Vector2.op_Implicit(((Transform) self.mTransform).localScale);
          this.mStartPosition = Vector2.op_Implicit(((Transform) self.mTransform).localPosition);
        }
      }

      public override void Update(WorldMapController self)
      {
        if (Object.op_Inequality((Object) self.mCurrentArea, (Object) null))
        {
          self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
        }
        else
        {
          this.mTransition = Mathf.Clamp01(this.mTransition + 1f / self.TransitionTime * Time.deltaTime);
          float num = Mathf.Sin((float) ((double) this.mTransition * 3.1415927410125732 * 0.5));
          self.mPrevArea.SetOpacity(1f - num);
          self.mTransform.anchoredPosition = Vector2.Lerp(this.mStartPosition, self.mInitPos, num);
          ((Transform) self.mTransform).localScale = Vector2.op_Implicit(Vector2.Lerp(this.mStartScale, self.mInitScale, num));
          self.SetRadialBlurStrength(this.mTransition);
          if ((double) this.mTransition < 1.0)
            return;
          self.mPrevArea.SetOpacity(0.0f);
          self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
        }
      }
    }
  }
}
