// Decompiled with JetBrains decompiler
// Type: Slack.SlackAPI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Diagnostics;

#nullable disable
namespace Slack
{
  public static class SlackAPI
  {
    [DebuggerHidden]
    public static IEnumerator PostMessage(
      PostMessageData data,
      Action onSuccess = null,
      Action<string> onError = null)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SlackAPI.\u003CPostMessage\u003Ec__Iterator0()
      {
        data = data,
        onError = onError,
        onSuccess = onSuccess
      };
    }

    [DebuggerHidden]
    public static IEnumerator UploadScreenShot(
      UploadData data,
      byte[] contents = null,
      Action onSuccess = null,
      Action<string> onError = null)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SlackAPI.\u003CUploadScreenShot\u003Ec__Iterator1()
      {
        contents = contents,
        data = data,
        onError = onError,
        onSuccess = onSuccess
      };
    }

    [DebuggerHidden]
    public static IEnumerator UploadScreenRecording(
      UploadData data,
      Action onSuccess = null,
      Action<string> onError = null)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SlackAPI.\u003CUploadScreenRecording\u003Ec__Iterator2()
      {
        data = data,
        onError = onError,
        onSuccess = onSuccess
      };
    }
  }
}
