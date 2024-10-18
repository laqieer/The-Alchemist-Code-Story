// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPreviewForRT
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitPreviewForRT : MonoBehaviour
  {
    [Description("3Dユニット投影用テクスチャの描画先")]
    [SerializeField]
    private RawImage m_PreviewImage;
    [Description("3Dユニット撮影カメラ")]
    [SerializeField]
    private Camera m_Camera3D;
    [Description("3Dユニット")]
    [SerializeField]
    private UnitPreview m_UnitController;
    private RenderTexture m_UnitRenderTexture;

    private RenderTexture CreateRenderTexture()
    {
      Rect rect1 = ((Graphic) this.m_PreviewImage).rectTransform.rect;
      int num1 = (int) Mathf.Floor(((Rect) ref rect1).width);
      Rect rect2 = ((Graphic) this.m_PreviewImage).rectTransform.rect;
      int num2 = (int) Mathf.Floor(((Rect) ref rect2).height);
      return RenderTexture.GetTemporary(num1, num2, 16, (RenderTextureFormat) 7);
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.m_Camera3D, (Object) null))
        ((Behaviour) this.m_Camera3D).enabled = false;
      if (!Object.op_Inequality((Object) this.m_PreviewImage, (Object) null))
        return;
      ((Component) this.m_PreviewImage).gameObject.SetActive(false);
    }

    private void Start() => this.Init();

    private void Init() => this.StartCoroutine(this.Setup());

    public void Refresh() => this.StartCoroutine(this.Setup());

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.m_UnitRenderTexture, (Object) null))
        return;
      RenderTexture.ReleaseTemporary(this.m_UnitRenderTexture);
      this.m_UnitRenderTexture = (RenderTexture) null;
    }

    public void SetUnitController(UnitPreview controller)
    {
      if (Object.op_Equality((Object) controller, (Object) null))
        DebugUtility.LogError("HomeUnitControllerが指定されていません.");
      else
        this.m_UnitController = controller;
    }

    [DebuggerHidden]
    private IEnumerator Setup()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitPreviewForRT.\u003CSetup\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
