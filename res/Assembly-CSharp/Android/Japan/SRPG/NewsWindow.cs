// Decompiled with JetBrains decompiler
// Type: SRPG.NewsWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NewsWindow : MonoBehaviour
  {
    public bool usegAuth = true;
    private string[] allow_change_scenes = new string[13]{ "MENU_ARENA", "MENU_MULTI", "MENU_INN", "MENU_PARTY", "MENU_SHOP", "MENU_SHOP_TABI", "MENU_SHOP_KIMAGURE", "MENU_SHOP_MONOZUKI", "MENU_UNITLIST", "MENU_QUEST", "MENU_DAILY", "MENU_GACHA", "MENU_REPLAY" };
    public RectTransform WebViewContainer;
    public SerializeValueBehaviour ValueList;
    public Button CloseButton;
    public int testCounter;

    private void Start()
    {
      Debug.Log((object) "[NewsWindow]Start");
      if (!MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
      {
        if ((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null)
          this.CloseButton.interactable = true;
        Debug.Log((object) "[NewsWindow]Not WebView Enable");
      }
      else
      {
        Debug.Log((object) "[NewsWindow]WebView Enable");
        if (!((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null))
          return;
        this.CloseButton.interactable = true;
      }
    }

    private void StartSceneChange(string new_scene)
    {
      foreach (string allowChangeScene in this.allow_change_scenes)
      {
        if (allowChangeScene == new_scene)
        {
          GameObject gameObject = GameObject.Find("Config_Home(Clone)");
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
            UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
          UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
          GlobalEvent.Invoke(new_scene, (object) this);
          break;
        }
      }
    }
  }
}
