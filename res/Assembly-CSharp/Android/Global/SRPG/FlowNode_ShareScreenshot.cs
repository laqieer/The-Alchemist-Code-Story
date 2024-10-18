// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ShareScreenshot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "ShareText", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "ShareScreenshot", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ShareScreenshot", 32741)]
  public class FlowNode_ShareScreenshot : FlowNode
  {
    private static bool isSharingImage = true;
    private string shareSubject = "The Alchemist Code";
    private string shareText = "Join us in our adventure at The Alchemist Code! ";
    private string share_url = "https://alchemist.onelink.me/mSun";
    private string imageName = "Screenshot";
    internal static ScreenCapture capture;
    public bool changeOrientation;
    private bool isProcessing;

    public override void OnActivate(int pinID)
    {
      this.shareSubject = LocalizedText.Get("sys.SHARE_SUBJECT");
      this.shareText = LocalizedText.Get("sys.SHARE_MESSAGE");
      this.share_url = LocalizedText.Get("sys.SHARE_URL");
      if (pinID == 0)
      {
        FlowNode_ShareScreenshot.isSharingImage = true;
        this.startSharing();
      }
      else
      {
        if (pinID != 1)
          return;
        FlowNode_ShareScreenshot.isSharingImage = false;
        this.startSharing();
      }
    }

    private void startSharing()
    {
      if ((UnityEngine.Object) FlowNode_ShareScreenshot.capture == (UnityEngine.Object) null)
        FlowNode_ShareScreenshot.capture = this.gameObject.AddComponent<ScreenCapture>();
      if (this.isProcessing)
        return;
      this.StartCoroutine(this.ShareScreenshot());
    }

    [DebuggerHidden]
    private IEnumerator ShareScreenshot()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ShareScreenshot.\u003CShareScreenshot\u003Ec__Iterator44() { \u003C\u003Ef__this = this };
    }
  }
}
