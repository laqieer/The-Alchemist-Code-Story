// Decompiled with JetBrains decompiler
// Type: SRPG.TouchControlArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
    private float sDist;
    private float nDist;
    private float hei;
    private float diag;

    private void Start()
    {
      if ((UnityEngine.Object) this.ResetButton != (UnityEngine.Object) null)
        this.ResetButton.onClick.AddListener(new UnityAction(this.Reset));
      if (!string.IsNullOrEmpty(this.TargetObjID))
        this.TargetObj = GameObjectID.FindGameObject(this.TargetObjID);
      if (!string.IsNullOrEmpty(this.SillObjID))
        this.SillObj = GameObjectID.FindGameObject(this.SillObjID);
      this.wid = (float) (Screen.width / 5);
      this.hei = (float) (Screen.height / 5);
      this.diag = Mathf.Sqrt(Mathf.Pow(this.wid, 2f) + Mathf.Pow(this.hei, 2f));
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
      this.sDist = this.nDist = 0.0f;
      this.v = TouchControlArea.vMin;
    }

    private void GetTouch()
    {
      if (Input.touchCount == 1)
      {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
          this.sPos = (Vector3) touch.position;
          this.sRot = this.TargetObj.transform.rotation;
        }
        else
        {
          if (touch.phase != TouchPhase.Moved && touch.phase != TouchPhase.Stationary)
            return;
          this.tx = (touch.position.x - this.sPos.x) / this.wid;
          this.ty = 0.0f;
          this.TargetObj.transform.rotation = this.sRot;
          this.TargetObj.transform.Rotate(new Vector3(90f * this.ty, -90f * this.tx, 0.0f), Space.World);
        }
      }
      else
      {
        if (Input.touchCount < 2)
          return;
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);
        if (touch2.phase == TouchPhase.Began)
        {
          this.sDist = Vector2.Distance(touch1.position, touch2.position);
        }
        else
        {
          if (touch1.phase != TouchPhase.Moved && touch1.phase != TouchPhase.Stationary || touch2.phase != TouchPhase.Moved && touch2.phase != TouchPhase.Stationary)
            return;
          this.nDist = Vector2.Distance(touch1.position, touch2.position);
          this.v += (this.nDist - this.sDist) / this.diag;
          this.v = (double) this.v <= (double) TouchControlArea.vMax ? this.v : TouchControlArea.vMax;
          this.v = (double) this.v > (double) TouchControlArea.vMin ? this.v : TouchControlArea.vMin;
          this.TargetObj.transform.localScale = this.targetScale * this.v;
          this.SillObj.transform.localScale = this.sillScale * this.v;
        }
      }
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
