// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_OperateStandChara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/編集(2D)", "立ち絵2を編集します。", 5592405, 4473992)]
  public class Event2dAction_OperateStandChara : EventAction
  {
    public static Dictionary<string, Color> CharaColorDic = new Dictionary<string, Color>();
    private bool CharaIDsFoldout = true;
    private List<EventStandCharaController2> mStandCharaList = new List<EventStandCharaController2>();
    private List<RectTransform> mStandCharaTransformList = new List<RectTransform>();
    private List<GameObject> charaList = new List<GameObject>();
    private List<Color> BodyColorList = new List<Color>();
    private List<Color> FaceColorList = new List<Color>();
    [HideInInspector]
    public bool MoveEnabled = true;
    [HideInInspector]
    public AnimationCurve MoveCurve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
    [HideInInspector]
    public Vector2 MoveTo = new Vector2(0.0f, 0.0f);
    private bool MoveFoldout = true;
    private List<Vector2> FromAnchorMinList = new List<Vector2>();
    private List<Vector2> FromAnchorMaxList = new List<Vector2>();
    private List<Vector2> mToAnchorList = new List<Vector2>();
    [HideInInspector]
    public AnimationCurve ScaleCurve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
    [HideInInspector]
    public Vector2 ScaleTo = new Vector2(1f, 1f);
    private List<float> FromWidthList = new List<float>();
    private List<float> FromHeightList = new List<float>();
    [HideInInspector]
    public AnimationCurve ColorCurve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
    [HideInInspector]
    public Color ColorTo = Color.white;
    [HideInInspector]
    public string[] CharaIDs;
    [HideInInspector]
    public bool async;
    [HideInInspector]
    public bool Flip;
    [HideInInspector]
    public float MoveTime;
    [HideInInspector]
    public bool Relative;
    private float MoveOffset;
    [HideInInspector]
    public bool ScaleEnabled;
    [HideInInspector]
    public float ScaleTime;
    private bool ScaleFoldout;
    private float ScaleOffset;
    private float mToWidth;
    private float mToHeght;
    [HideInInspector]
    public bool ColorEnabled;
    [HideInInspector]
    public float ColorTime;
    private bool ColorFoldout;
    private float ColorOffset;
    private Color mToColor;
    private bool mMoveEnabled;
    private bool mScaleEnabled;
    private bool mColorEnabled;

    public override void PreStart()
    {
      for (int index = 0; index < this.CharaIDs.Length; ++index)
        this.mStandCharaList.Add(EventStandCharaController2.FindInstances(this.CharaIDs[index]));
    }

    public override void OnActivate()
    {
      if (this.mStandCharaList.Count <= 0)
      {
        this.ActivateNext();
      }
      else
      {
        this.mMoveEnabled = this.MoveEnabled;
        this.mScaleEnabled = this.ScaleEnabled;
        this.mColorEnabled = this.ColorEnabled;
        for (int index = 0; index < this.mStandCharaList.Count; ++index)
        {
          if ((UnityEngine.Object) this.mStandCharaList[index] != (UnityEngine.Object) null)
            this.mStandCharaTransformList.Add(this.mStandCharaList[index].GetComponent<RectTransform>());
        }
        if (this.Flip)
        {
          for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
            this.mStandCharaTransformList[index].Rotate(new Vector3(0.0f, 180f, 0.0f));
        }
        if (this.mMoveEnabled)
        {
          for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
          {
            this.FromAnchorMinList.Add(this.mStandCharaTransformList[index].anchorMin);
            this.FromAnchorMaxList.Add(this.mStandCharaTransformList[index].anchorMax);
            if (this.Relative)
              this.mToAnchorList.Add(this.mStandCharaTransformList[index].anchorMin + Vector2.Scale(this.MoveTo, new Vector2(0.5f, 1f)));
            else
              this.mToAnchorList.Add(this.convertPosition(this.MoveTo));
          }
          if ((double) this.MoveTime <= 0.0)
            this.MoveOffset = 1f;
        }
        if (this.mScaleEnabled)
        {
          for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
          {
            this.FromWidthList.Add(this.mStandCharaTransformList[index].localScale.x);
            this.FromHeightList.Add(this.mStandCharaTransformList[index].localScale.y);
          }
          this.mToWidth = this.ScaleTo.x;
          this.mToHeght = this.ScaleTo.y;
          if ((double) this.ScaleTime <= 0.0)
            this.ScaleOffset = 1f;
        }
        if (this.mColorEnabled)
        {
          for (int index = 0; index < this.mStandCharaList.Count; ++index)
          {
            if ((UnityEngine.Object) this.mStandCharaList[index] != (UnityEngine.Object) null)
              this.charaList.AddRange((IEnumerable<GameObject>) this.mStandCharaList[index].StandCharaList);
          }
          for (int index = 0; index < this.charaList.Count; ++index)
          {
            if ((UnityEngine.Object) this.charaList[index].GetComponent<EventStandChara2>().BodyObject != (UnityEngine.Object) null)
              this.BodyColorList.Add(this.charaList[index].GetComponent<EventStandChara2>().BodyObject.GetComponent<RawImage>().color);
            else
              this.BodyColorList.Add(Color.white);
            if ((UnityEngine.Object) this.charaList[index].GetComponent<EventStandChara2>().FaceObject != (UnityEngine.Object) null)
              this.FaceColorList.Add(this.charaList[index].GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().color);
            else
              this.FaceColorList.Add(Color.white);
          }
          this.mToColor = this.ColorTo;
          for (int index = 0; index < this.CharaIDs.Length; ++index)
          {
            string charaId = this.CharaIDs[index];
            if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
              Event2dAction_OperateStandChara.CharaColorDic[charaId] = this.ColorTo;
            else
              Event2dAction_OperateStandChara.CharaColorDic.Add(charaId, this.ColorTo);
          }
          if ((double) this.ColorTime <= 0.0)
            this.ColorOffset = 1f;
        }
        if (!this.async)
          return;
        this.ActivateNext(true);
      }
    }

    public override void Update()
    {
      if (this.mMoveEnabled)
      {
        if ((double) this.MoveOffset >= 1.0)
        {
          this.MoveOffset = 1f;
          this.mMoveEnabled = false;
        }
        float num = this.MoveCurve.Evaluate(this.MoveOffset);
        for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
        {
          this.mStandCharaTransformList[index].anchorMin = this.FromAnchorMinList[index] + Vector2.Scale(this.mToAnchorList[index] - this.FromAnchorMinList[index], new Vector2(num, num));
          this.mStandCharaTransformList[index].anchorMax = this.FromAnchorMaxList[index] + Vector2.Scale(this.mToAnchorList[index] - this.FromAnchorMaxList[index], new Vector2(num, num));
        }
        this.MoveOffset += Time.deltaTime / this.MoveTime;
      }
      if (this.mScaleEnabled)
      {
        if ((double) this.ScaleOffset >= 1.0)
        {
          this.ScaleOffset = 1f;
          this.mScaleEnabled = false;
        }
        float num = this.ScaleCurve.Evaluate(this.ScaleOffset);
        for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
        {
          Vector3 vector3 = new Vector3(this.FromWidthList[index] + (this.mToWidth - this.FromWidthList[index]) * num, this.FromHeightList[index] + (this.mToHeght - this.FromHeightList[index]) * num, 1f);
          this.mStandCharaTransformList[index].localScale = vector3;
        }
        this.ScaleOffset += Time.deltaTime / this.ScaleTime;
      }
      if (this.mColorEnabled)
      {
        if ((double) this.ColorOffset >= 1.0)
        {
          this.ColorOffset = 1f;
          this.mColorEnabled = false;
        }
        float t = this.ColorCurve.Evaluate(this.ColorOffset);
        for (int index = 0; index < this.charaList.Count; ++index)
        {
          if ((UnityEngine.Object) this.charaList[index].GetComponent<EventStandChara2>().BodyObject != (UnityEngine.Object) null)
          {
            Color color = Color.Lerp(this.BodyColorList[index], this.mToColor, t);
            this.charaList[index].GetComponent<EventStandChara2>().BodyObject.GetComponent<RawImage>().color = color;
          }
          if ((UnityEngine.Object) this.charaList[index].GetComponent<EventStandChara2>().FaceObject != (UnityEngine.Object) null)
          {
            Color color = Color.Lerp(this.FaceColorList[index], this.mToColor, t);
            this.charaList[index].GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().color = color;
          }
        }
        this.ColorOffset += Time.deltaTime / this.ColorTime;
      }
      if (this.mMoveEnabled || this.mScaleEnabled || this.mColorEnabled)
        return;
      if (this.async)
        this.enabled = false;
      else
        this.ActivateNext();
    }

    private Vector2 convertPosition(Vector2 pos)
    {
      return Vector2.Scale(new Vector2(pos.x + 1f, pos.y * 2f), new Vector2(0.5f, 0.5f));
    }
  }
}
