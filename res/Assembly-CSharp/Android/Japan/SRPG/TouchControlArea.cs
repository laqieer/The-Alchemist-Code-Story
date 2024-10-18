// Decompiled with JetBrains decompiler
// Type: SRPG.TouchControlArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class TouchControlArea : MonoBehaviour
  {
    private static readonly float vMin = 1f;
    private static readonly float vMax = 1.4f;
    public string TargetObjID = "UNITPREVIEW";
    public string SillObjID = "UNITPREVIEWBASE";
    [SerializeField]
    private Button ResetButton;
    private GameObject TargetObj;
    private GameObject SillObj;
    private Vector3 sPos;
    private Quaternion sRot;
    private float tx;
    private float ty;
    private float v;
    private Vector3 targetScale;
    private Vector3 sillScale;
    private float wid;

    private void Start()
    {
      if ((UnityEngine.Object) this.ResetButton != (UnityEngine.Object) null)
        this.ResetButton.onClick.AddListener(new UnityAction(this.Reset));
      if (!string.IsNullOrEmpty(this.TargetObjID))
        this.TargetObj = GameObjectID.FindGameObject(this.TargetObjID);
      if (!string.IsNullOrEmpty(this.SillObjID))
        this.SillObj = GameObjectID.FindGameObject(this.SillObjID);
      this.wid = (float) (Screen.width / 5);
      if ((UnityEngine.Object) this.TargetObj != (UnityEngine.Object) null)
        this.targetScale = this.TargetObj.transform.localScale;
      if (!((UnityEngine.Object) this.SillObj != (UnityEngine.Object) null))
        return;
      this.sillScale = this.SillObj.transform.localScale;
    }

    private void Update()
    {
      this.GetTouch();
    }

    public void Reset()
    {
      this.TargetObj.transform.rotation = Quaternion.identity;
      this.TargetObj.transform.Rotate(new Vector3(0.0f, 180f, 0.0f), Space.World);
      this.TargetObj.transform.localScale = this.targetScale;
      this.SillObj.transform.localScale = this.sillScale;
      this.v = TouchControlArea.vMin;
    }

    private void GetTouch()
    {
      TouchControlArea.TouchState touchState = TouchControlArea.TouchState.None;
      if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        touchState = TouchControlArea.TouchState.Began;
      if (touchState == TouchControlArea.TouchState.None && (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)))
        touchState = TouchControlArea.TouchState.Moved;
      if (touchState == TouchControlArea.TouchState.None && (Input.GetMouseButtonUp(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)))
        touchState = TouchControlArea.TouchState.Ended;
      switch (touchState)
      {
        case TouchControlArea.TouchState.Began:
          this.sPos = Input.mousePosition;
          this.sRot = this.TargetObj.transform.rotation;
          break;
        case TouchControlArea.TouchState.Moved:
          float num1 = (Input.mousePosition.x - this.sPos.x) / this.wid;
          float num2 = 0.0f;
          this.TargetObj.transform.rotation = this.sRot;
          this.TargetObj.transform.Rotate(new Vector3(90f * num2, -90f * num1, 0.0f), Space.World);
          break;
      }
      float axis = Input.GetAxis("Mouse ScrollWheel");
      if ((double) axis == 0.0)
        return;
      this.v += axis;
      if ((double) this.v >= (double) TouchControlArea.vMax)
        this.v = TouchControlArea.vMax;
      if ((double) this.v < (double) TouchControlArea.vMin)
        this.v = TouchControlArea.vMin;
      this.TargetObj.transform.localScale = this.targetScale * this.v;
      this.SillObj.transform.localScale = this.sillScale * this.v;
    }

    public enum TouchState
    {
      None = -1,
      Began = 0,
      Moved = 1,
      Stationary = 2,
      Ended = 3,
      Canceled = 4,
    }
  }
}
