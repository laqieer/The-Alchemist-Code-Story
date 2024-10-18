// Decompiled with JetBrains decompiler
// Type: SRPG.SNSController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class SNSController : MonoBehaviour
  {
    public static readonly string SCREENSHOT_IMAGE_SAVE_DIR = "screenshot";
    public static readonly string SCREENSHOT_IMAGE_NAME = "ss.png";
    [SerializeField]
    private string ShutterSECueID = string.Empty;
    private bool mIsProcessingCapture;
    private static bool mIsInstalled_Twitter;
    private static SNSController mInstance;

    public bool IsProcessingCapture => this.mIsProcessingCapture;

    public static bool IsInstalled_Twitter => SNSController.mIsInstalled_Twitter;

    public static SNSController Instance
    {
      get
      {
        if (Object.op_Equality((Object) SNSController.mInstance, (Object) null))
        {
          SNSController snsController1 = AssetManager.Load<SNSController>("UI/SNSController");
          if (Object.op_Equality((Object) snsController1, (Object) null))
          {
            DebugUtility.LogError("SNSControllerのロードに失敗しました。");
            return (SNSController) null;
          }
          SNSController snsController2 = Object.Instantiate<SNSController>(snsController1);
          SNSController.mInstance = snsController2;
          Object.DontDestroyOnLoad((Object) ((Component) snsController2).gameObject);
        }
        return SNSController.mInstance;
      }
    }

    public static void RefreshInstalled_Twitter()
    {
    }

    public void ScreenCapture(
      string message,
      string append_img_path,
      GameObject hide_obj = null,
      bool is_test = false)
    {
      this.StartCoroutine(this._ScreenCapture(message, append_img_path, hide_obj, is_test));
    }

    [DebuggerHidden]
    private IEnumerator _ScreenCapture(
      string message,
      string append_img_path,
      GameObject hide_obj = null,
      bool is_test = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SNSController.\u003C_ScreenCapture\u003Ec__Iterator0()
      {
        hide_obj = hide_obj,
        append_img_path = append_img_path,
        is_test = is_test,
        message = message,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator PlayShutterEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SNSController.\u003CPlayShutterEffect\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    private Texture2D AppendImage_RightBottom(Texture2D target, string append_img_path)
    {
      if (Object.op_Equality((Object) target, (Object) null))
        return (Texture2D) null;
      Texture2D tex = AssetManager.Load<Texture2D>(append_img_path);
      if (Object.op_Equality((Object) tex, (Object) null))
        return target;
      float num = (float) Screen.width / 1334f;
      float w = (float) ((Texture) tex).width * num;
      float h = (float) ((Texture) tex).height * num;
      Texture2D texture2D = this.ResizeTexture(tex, (int) w, (int) h);
      Color[] pixels1 = texture2D.GetPixels();
      Color[] pixels2 = target.GetPixels();
      bool flag = false;
      int index1 = 0;
      for (int index2 = 0; index2 < ((Texture) target).height && !flag; ++index2)
      {
        for (int index3 = 0; index3 < ((Texture) target).width; ++index3)
        {
          if (index3 >= ((Texture) target).width - ((Texture) texture2D).width)
          {
            int index4 = ((Texture) target).width * index2 + index3;
            if ((double) pixels1[index1].a > 0.0)
              pixels2.SetValue((object) Color.Lerp(pixels2[index4], pixels1[index1], pixels1[index1].a), index4);
            ++index1;
            if (index1 >= pixels1.Length)
            {
              flag = true;
              break;
            }
          }
        }
      }
      AssetBundleUnloader.ReserveUnload(false);
      target.SetPixels(pixels2);
      return target;
    }

    private Texture2D ResizeTexture(Texture2D tex, int w, int h)
    {
      RenderTexture temporary = RenderTexture.GetTemporary(w, h);
      Graphics.Blit((Texture) tex, temporary);
      RenderTexture active = RenderTexture.active;
      RenderTexture.active = temporary;
      Texture2D texture2D = new Texture2D(w, h);
      texture2D.ReadPixels(new Rect(0.0f, 0.0f, (float) w, (float) h), 0, 0);
      texture2D.Apply();
      RenderTexture.active = active;
      RenderTexture.ReleaseTemporary(temporary);
      return texture2D;
    }
  }
}
