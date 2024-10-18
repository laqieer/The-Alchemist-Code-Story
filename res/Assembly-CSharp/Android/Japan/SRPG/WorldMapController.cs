// Decompiled with JetBrains decompiler
// Type: SRPG.WorldMapController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class WorldMapController : MonoBehaviour
  {
    public AreaMapController[] AreaMaps = new AreaMapController[0];
    public RawImage[] Images = new RawImage[0];
    public float TransitionTime = 1f;
    public RadialBlurEffect RadialBlurEffect;
    public bool AutoSelectArea;
    public QuestSectionList SectionList;
    private RectTransform mTransform;
    private Vector2 mDefaultPosition;
    private Vector2 mDefaultScale;
    private AreaMapController mCurrentArea;
    private AreaMapController mPrevArea;
    private AreaMapController mNextArea;
    public AnimationCurve RadialBlurCurve;
    private StateMachine<WorldMapController> mStateMachine;

    public static WorldMapController FindInstance(string gameobjectID)
    {
      GameObject gameObject = GameObjectID.FindGameObject(gameobjectID);
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
        return gameObject.GetComponent<WorldMapController>();
      return (WorldMapController) null;
    }

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

    private void Start()
    {
      this.mTransform = this.transform as RectTransform;
      this.mDefaultPosition = this.mTransform.anchoredPosition;
      this.mDefaultScale = (Vector2) this.mTransform.localScale;
      this.mStateMachine = new StateMachine<WorldMapController>(this);
      bool flag = MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart();
      if (this.AutoSelectArea && !flag)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (string.IsNullOrEmpty((string) GlobalVars.SelectedSection))
        {
          QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
          if (lastStoryQuest != null)
            GlobalVars.SelectedSection.Set(lastStoryQuest.Chapter.section);
        }
        if (string.IsNullOrEmpty((string) GlobalVars.SelectedSection) && string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
        {
          GlobalVars.SelectedSection.Set(instance.Sections[0].iname);
          for (int index = 0; index < instance.Chapters.Length; ++index)
          {
            if (instance.Chapters[index].section == (string) GlobalVars.SelectedSection)
            {
              GlobalVars.SelectedChapter.Set(instance.Chapters[index].iname);
              break;
            }
          }
        }
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
        if ((UnityEngine.Object) this.mCurrentArea != (UnityEngine.Object) null)
        {
          this.mStateMachine.GotoState<WorldMapController.State_World2Area>();
          return;
        }
      }
      else if (flag)
        this.AutoSelectArea = false;
      this.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
    }

    private void SetRadialBlurStrength(float t)
    {
      if (this.RadialBlurCurve != null && this.RadialBlurCurve.keys.Length > 0)
        this.RadialBlurEffect.Strength = this.RadialBlurCurve.Evaluate(t);
      else
        this.RadialBlurEffect.Strength = Mathf.Sin(t * 3.141593f);
    }

    private void Update()
    {
      this.mStateMachine.Update();
    }

    private class State_WorldSelect : State<WorldMapController>
    {
      public override void Begin(WorldMapController self)
      {
        if (!((UnityEngine.Object) self.mNextArea != (UnityEngine.Object) null))
          return;
        self.mCurrentArea = self.mNextArea;
        self.mNextArea = (AreaMapController) null;
        self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
      }

      public override void Update(WorldMapController self)
      {
        if (!((UnityEngine.Object) self.mCurrentArea != (UnityEngine.Object) null))
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
        if ((UnityEngine.Object) self.mCurrentArea == (UnityEngine.Object) null)
          self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
        this.mTarget = self.mCurrentArea;
        RectTransform transform = this.mTarget.transform as RectTransform;
        float x = 1f / transform.localScale.x * self.mDefaultScale.x;
        float y = 1f / transform.localScale.y * self.mDefaultScale.y;
        this.mDesiredScale = (Vector2) new Vector3(x, y);
        this.mTargetPosition = transform.anchoredPosition;
        this.mDesiredPosition = -transform.anchoredPosition;
        this.mDesiredPosition.x *= x;
        this.mDesiredPosition.y *= y;
        if (!self.AutoSelectArea)
          return;
        self.AutoSelectArea = false;
        this.mTarget.SetOpacity(1f);
        self.mTransform.anchoredPosition = this.mDesiredPosition;
        self.mTransform.localScale = (Vector3) this.mDesiredScale;
        self.mStateMachine.GotoState<WorldMapController.State_AreaSelect>();
      }

      public override void Update(WorldMapController self)
      {
        if ((UnityEngine.Object) self.mCurrentArea == (UnityEngine.Object) null)
        {
          self.mPrevArea = this.mTarget;
          self.mNextArea = (AreaMapController) null;
          self.mStateMachine.GotoState<WorldMapController.State_Area2World>();
        }
        else
        {
          this.mTransition = Mathf.Clamp01(this.mTransition + 1f / self.TransitionTime * Time.deltaTime);
          float num = Mathf.Sin((float) ((double) this.mTransition * 3.14159274101257 * 0.5));
          this.mTarget.SetOpacity(num);
          self.mTransform.anchoredPosition = Vector2.Lerp(self.mDefaultPosition, this.mDesiredPosition, num);
          self.mTransform.localScale = (Vector3) Vector2.Lerp(self.mDefaultScale, this.mDesiredScale, num);
          self.SetRadialBlurStrength(this.mTransition);
          if ((UnityEngine.Object) self.RadialBlurEffect != (UnityEngine.Object) null)
          {
            float x = (float) ((double) this.mTargetPosition.x / (double) self.mTransform.rect.width + 0.5);
            float y = (float) ((double) this.mTargetPosition.y / (double) self.mTransform.rect.height + 0.5);
            self.RadialBlurEffect.Focus = new Vector2(x, y);
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

      public override void Begin(WorldMapController self)
      {
        this.mArea = self.mCurrentArea;
      }

      public override void Update(WorldMapController self)
      {
        if (!((UnityEngine.Object) self.mCurrentArea != (UnityEngine.Object) this.mArea))
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
        if ((UnityEngine.Object) self.mCurrentArea != (UnityEngine.Object) null)
        {
          self.mPrevArea.SetOpacity(0.0f);
          self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
        }
        else
        {
          this.mStartScale = (Vector2) self.mTransform.localScale;
          this.mStartPosition = (Vector2) self.mTransform.localPosition;
        }
      }

      public override void Update(WorldMapController self)
      {
        if ((UnityEngine.Object) self.mCurrentArea != (UnityEngine.Object) null)
        {
          self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
        }
        else
        {
          this.mTransition = Mathf.Clamp01(this.mTransition + 1f / self.TransitionTime * Time.deltaTime);
          float t = Mathf.Sin((float) ((double) this.mTransition * 3.14159274101257 * 0.5));
          self.mPrevArea.SetOpacity(1f - t);
          self.mTransform.anchoredPosition = Vector2.Lerp(this.mStartPosition, self.mDefaultPosition, t);
          self.mTransform.localScale = (Vector3) Vector2.Lerp(this.mStartScale, self.mDefaultScale, t);
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
