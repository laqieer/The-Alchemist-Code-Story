// Decompiled with JetBrains decompiler
// Type: SRPG.NewsWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class NewsWindow : MonoBehaviour
  {
    public RectTransform WebViewContainer;
    public bool usegAuth = true;
    public SerializeValueBehaviour ValueList;
    private string[] allow_change_scenes = new string[13]
    {
      "MENU_ARENA",
      "MENU_MULTI",
      "MENU_INN",
      "MENU_PARTY",
      "MENU_SHOP",
      "MENU_SHOP_TABI",
      "MENU_SHOP_KIMAGURE",
      "MENU_SHOP_MONOZUKI",
      "MENU_UNITLIST",
      "MENU_QUEST",
      "MENU_DAILY",
      "MENU_GACHA",
      "MENU_REPLAY"
    };
    public Button CloseButton;

    private void Start()
    {
      Debug.Log((object) "[NewsWindow]Start");
      if (!MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
      {
        if (Object.op_Inequality((Object) this.CloseButton, (Object) null))
          ((Selectable) this.CloseButton).interactable = true;
        Debug.Log((object) "[NewsWindow]Not WebView Enable");
      }
      else
      {
        Debug.Log((object) "[NewsWindow]WebView Enable");
        if (!Object.op_Inequality((Object) this.CloseButton, (Object) null))
          return;
        ((Selectable) this.CloseButton).interactable = true;
      }
    }

    private void StartSceneChange(string new_scene)
    {
      foreach (string allowChangeScene in this.allow_change_scenes)
      {
        if (allowChangeScene == new_scene)
        {
          GameObject gameObject = GameObject.Find("Config_Home(Clone)");
          if (Object.op_Inequality((Object) gameObject, (Object) null))
            Object.Destroy((Object) gameObject);
          Object.Destroy((Object) ((Component) this).gameObject);
          GlobalEvent.Invoke(new_scene, (object) this);
          break;
        }
      }
    }

    private Rect GetRect()
    {
      Vector3[] vector3Array = new Vector3[4];
      this.WebViewContainer.GetWorldCorners(vector3Array);
      float num1 = 1f;
      float num2 = 1f;
      Rect rect;
      // ISSUE: explicit constructor call
      ((Rect) ref rect).\u002Ector(vector3Array[1].x * num1, ((float) Screen.height - vector3Array[1].y) * num2, (vector3Array[3].x - vector3Array[1].x) * num1, (vector3Array[1].y - vector3Array[3].y) * num2);
      return rect;
    }
  }
}
