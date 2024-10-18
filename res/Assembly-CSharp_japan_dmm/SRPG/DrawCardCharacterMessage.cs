// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardCharacterMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class DrawCardCharacterMessage : MonoBehaviour
  {
    private static DrawCardCharacterMessage mInstance;
    [SerializeField]
    private GameObject mMessageParent;
    [SerializeField]
    private Text mMessageText;
    private const float MESSAGE_SPEED = 0.05f;
    private string mMessageString = string.Empty;
    private int mMessageIndex;
    private float mMessageSeconds;

    private void Awake()
    {
      DrawCardCharacterMessage.mInstance = this;
      if (Object.op_Inequality((Object) this.mMessageParent, (Object) null))
        this.mMessageParent.SetActive(false);
      if (Object.op_Inequality((Object) this.mMessageText, (Object) null))
        this.mMessageText.text = string.Empty;
      this.mMessageString = string.Empty;
      this.mMessageIndex = 0;
      this.mMessageSeconds = 0.0f;
    }

    private void Update()
    {
      if (string.IsNullOrEmpty(this.mMessageString) || this.mMessageString.Length <= this.mMessageIndex)
        return;
      if (Input.anyKeyDown)
      {
        this.mMessageIndex = this.mMessageString.Length;
      }
      else
      {
        this.mMessageSeconds += Time.deltaTime;
        if ((double) this.mMessageSeconds >= 0.05000000074505806)
        {
          this.mMessageSeconds -= 0.05f;
          ++this.mMessageIndex;
        }
      }
      if (Object.op_Equality((Object) this.mMessageText, (Object) null))
        return;
      this.mMessageText.text = this.mMessageString.Substring(0, this.mMessageIndex);
    }

    public static bool IsMessaging
    {
      get
      {
        return !Object.op_Equality((Object) DrawCardCharacterMessage.mInstance, (Object) null) && !string.IsNullOrEmpty(DrawCardCharacterMessage.mInstance.mMessageString) && DrawCardCharacterMessage.mInstance.mMessageString.Length > DrawCardCharacterMessage.mInstance.mMessageIndex;
      }
    }

    public static void ShowMessage(string message)
    {
      if (Object.op_Equality((Object) DrawCardCharacterMessage.mInstance, (Object) null))
        DebugUtility.LogError("It is uninitialized.");
      else
        DrawCardCharacterMessage.mInstance._showMessage(message);
    }

    public static void HiddenMessage()
    {
      if (Object.op_Equality((Object) DrawCardCharacterMessage.mInstance, (Object) null))
      {
        DebugUtility.LogError("It is uninitialized.");
      }
      else
      {
        if (Object.op_Equality((Object) DrawCardCharacterMessage.mInstance.mMessageParent, (Object) null))
          return;
        DrawCardCharacterMessage.mInstance.mMessageParent.SetActive(false);
      }
    }

    private void _showMessage(string message)
    {
      if (Object.op_Equality((Object) this.mMessageParent, (Object) null))
        return;
      this.mMessageParent.SetActive(true);
      if (Object.op_Equality((Object) this.mMessageText, (Object) null))
        return;
      this.mMessageText.text = string.Empty;
      this.mMessageString = LocalizedText.Get(message);
      this.mMessageIndex = 1;
      this.mMessageSeconds = 0.0f;
    }
  }
}
