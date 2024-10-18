// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.Helper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public static class Helper
  {
    public const string ScriptVersion = "1.9.12";

    public static string GetName(Platform platform)
    {
      switch (platform)
      {
        case Platform.MacOSX:
          return "macOS";
        case Platform.WindowsPhone:
          return "Windows Phone";
        case Platform.WindowsUWP:
          return "Windows UWP";
        default:
          return platform.ToString();
      }
    }

    public static string GetErrorMessage(ErrorCode code)
    {
      string errorMessage = string.Empty;
      switch (code)
      {
        case ErrorCode.None:
          errorMessage = "No Error";
          break;
        case ErrorCode.LoadFailed:
          errorMessage = "Loading failed.  File not found, codec not supported, video resolution too high or insufficient system resources.";
          if (SystemInfo.operatingSystem.StartsWith("Windows XP") || SystemInfo.operatingSystem.StartsWith("Windows Vista"))
          {
            errorMessage += " NOTE: Windows XP and Vista don't have native support for H.264 codec.  Consider using an older codec such as DivX or installing 3rd party codecs such as LAV Filters.";
            break;
          }
          break;
        case ErrorCode.DecodeFailed:
          errorMessage = "Decode failed.  Possible codec not supported, video resolution/bit-depth too high, or insufficient system resources.";
          break;
      }
      return errorMessage;
    }

    public static string[] GetPlatformNames()
    {
      return new string[9]
      {
        Helper.GetName(Platform.Windows),
        Helper.GetName(Platform.MacOSX),
        Helper.GetName(Platform.iOS),
        Helper.GetName(Platform.tvOS),
        Helper.GetName(Platform.Android),
        Helper.GetName(Platform.WindowsPhone),
        Helper.GetName(Platform.WindowsUWP),
        Helper.GetName(Platform.WebGL),
        Helper.GetName(Platform.PS4)
      };
    }

    [Conditional("ALWAYS_FALSE")]
    public static void LogInfo(string message, Object context = null)
    {
      if (Object.op_Equality(context, (Object) null))
        Debug.Log((object) ("[AVProVideo] " + message));
      else
        Debug.Log((object) ("[AVProVideo] " + message), context);
    }

    public static string GetTimeString(float totalSeconds, bool showMilliseconds = false)
    {
      int num1 = Mathf.FloorToInt(totalSeconds / 3600f);
      float num2 = (float) ((double) num1 * 60.0 * 60.0);
      int num3 = Mathf.FloorToInt((float) (((double) totalSeconds - (double) num2) / 60.0));
      float num4 = num2 + (float) num3 * 60f;
      int num5 = Mathf.FloorToInt(totalSeconds - num4);
      string timeString;
      if (num1 <= 0)
      {
        if (showMilliseconds)
        {
          int num6 = (int) (((double) totalSeconds - (double) Mathf.Floor(totalSeconds)) * 1000.0);
          timeString = string.Format("{0:00}:{1:00}:{2:000}", (object) num3, (object) num5, (object) num6);
        }
        else
          timeString = string.Format("{0:00}:{1:00}", (object) num3, (object) num5);
      }
      else if (showMilliseconds)
      {
        int num7 = (int) (((double) totalSeconds - (double) Mathf.Floor(totalSeconds)) * 1000.0);
        timeString = string.Format("{2}:{0:00}:{1:00}:{3:000}", (object) num3, (object) num5, (object) num1, (object) num7);
      }
      else
        timeString = string.Format("{2}:{0:00}:{1:00}", (object) num3, (object) num5, (object) num1);
      return timeString;
    }

    public static Orientation GetOrientation(float[] t)
    {
      Orientation orientation = Orientation.Landscape;
      if (t != null)
      {
        if ((double) t[0] == 0.0 && (double) t[1] == 1.0 && (double) t[2] == -1.0 && (double) t[3] == 0.0)
          orientation = Orientation.Portrait;
        else if ((double) t[0] == 0.0 && (double) t[1] == -1.0 && (double) t[2] == 1.0 && (double) t[3] == 0.0)
          orientation = Orientation.PortraitFlipped;
        else if ((double) t[0] == 1.0 && (double) t[1] == 0.0 && (double) t[2] == 0.0 && (double) t[3] == 1.0)
          orientation = Orientation.Landscape;
        else if ((double) t[0] == -1.0 && (double) t[1] == 0.0 && (double) t[2] == 0.0 && (double) t[3] == -1.0)
          orientation = Orientation.LandscapeFlipped;
      }
      return orientation;
    }

    public static Matrix4x4 GetMatrixForOrientation(Orientation ori)
    {
      Matrix4x4 matrix4x4_1 = Matrix4x4.TRS(new Vector3(0.0f, 1f, 0.0f), Quaternion.Euler(0.0f, 0.0f, -90f), Vector3.one);
      Matrix4x4 matrix4x4_2 = Matrix4x4.TRS(new Vector3(1f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90f), Vector3.one);
      Matrix4x4 matrix4x4_3 = Matrix4x4.TRS(new Vector3(1f, 1f, 0.0f), Quaternion.identity, new Vector3(-1f, -1f, 1f));
      Matrix4x4 matrixForOrientation = Matrix4x4.identity;
      switch (ori)
      {
        case Orientation.LandscapeFlipped:
          matrixForOrientation = matrix4x4_3;
          break;
        case Orientation.Portrait:
          matrixForOrientation = matrix4x4_1;
          break;
        case Orientation.PortraitFlipped:
          matrixForOrientation = matrix4x4_2;
          break;
      }
      return matrixForOrientation;
    }

    public static void SetupStereoEyeModeMaterial(Material material, StereoEye mode)
    {
      switch (mode)
      {
        case StereoEye.Both:
          material.DisableKeyword("FORCEEYE_LEFT");
          material.DisableKeyword("FORCEEYE_RIGHT");
          material.EnableKeyword("FORCEEYE_NONE");
          break;
        case StereoEye.Left:
          material.DisableKeyword("FORCEEYE_NONE");
          material.DisableKeyword("FORCEEYE_RIGHT");
          material.EnableKeyword("FORCEEYE_LEFT");
          break;
        case StereoEye.Right:
          material.DisableKeyword("FORCEEYE_NONE");
          material.DisableKeyword("FORCEEYE_LEFT");
          material.EnableKeyword("FORCEEYE_RIGHT");
          break;
      }
    }

    public static void SetupLayoutMaterial(Material material, VideoMapping mapping)
    {
      material.DisableKeyword("LAYOUT_NONE");
      material.DisableKeyword("LAYOUT_EQUIRECT180");
      if (mapping != VideoMapping.EquiRectangular180)
        return;
      material.EnableKeyword("LAYOUT_EQUIRECT180");
    }

    public static void SetupStereoMaterial(
      Material material,
      StereoPacking packing,
      bool displayDebugTinting)
    {
      material.DisableKeyword("STEREO_CUSTOM_UV");
      material.DisableKeyword("STEREO_TOP_BOTTOM");
      material.DisableKeyword("STEREO_LEFT_RIGHT");
      material.DisableKeyword("MONOSCOPIC");
      switch (packing)
      {
        case StereoPacking.TopBottom:
          material.EnableKeyword("STEREO_TOP_BOTTOM");
          break;
        case StereoPacking.LeftRight:
          material.EnableKeyword("STEREO_LEFT_RIGHT");
          break;
        case StereoPacking.CustomUV:
          material.EnableKeyword("STEREO_CUSTOM_UV");
          break;
      }
      if (displayDebugTinting)
        material.EnableKeyword("STEREO_DEBUG");
      else
        material.DisableKeyword("STEREO_DEBUG");
    }

    public static void SetupAlphaPackedMaterial(Material material, AlphaPacking packing)
    {
      material.DisableKeyword("ALPHAPACK_TOP_BOTTOM");
      material.DisableKeyword("ALPHAPACK_LEFT_RIGHT");
      material.DisableKeyword("ALPHAPACK_NONE");
      if (packing == AlphaPacking.None)
        return;
      if (packing != AlphaPacking.TopBottom)
      {
        if (packing != AlphaPacking.LeftRight)
          return;
        material.EnableKeyword("ALPHAPACK_LEFT_RIGHT");
      }
      else
        material.EnableKeyword("ALPHAPACK_TOP_BOTTOM");
    }

    public static void SetupGammaMaterial(Material material, bool playerSupportsLinear)
    {
      if (QualitySettings.activeColorSpace == 1 && !playerSupportsLinear)
        material.EnableKeyword("APPLY_GAMMA");
      else
        material.DisableKeyword("APPLY_GAMMA");
    }

    public static int ConvertTimeSecondsToFrame(float seconds, float frameRate)
    {
      return Mathf.FloorToInt(frameRate * seconds);
    }

    public static float ConvertFrameToTimeSeconds(int frame, float frameRate)
    {
      float num = 1f / frameRate;
      return (float) ((double) frame * (double) num + (double) num * 0.5);
    }

    public static float FindNextKeyFrameTimeSeconds(
      float seconds,
      float frameRate,
      int keyFrameInterval)
    {
      int frame = Helper.ConvertTimeSecondsToFrame(seconds, frameRate);
      return Helper.ConvertFrameToTimeSeconds(keyFrameInterval * Mathf.CeilToInt((float) (frame + 1) / (float) keyFrameInterval), frameRate);
    }

    public static DateTime ConvertSecondsSince1970ToDateTime(double secondsSince1970)
    {
      return new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(secondsSince1970));
    }

    public static void DrawTexture(
      Rect screenRect,
      Texture texture,
      ScaleMode scaleMode,
      AlphaPacking alphaPacking,
      Material material)
    {
      if (Event.current.type != 7)
        return;
      float width = (float) texture.width;
      float height = (float) texture.height;
      switch (alphaPacking)
      {
        case AlphaPacking.TopBottom:
          height *= 0.5f;
          break;
        case AlphaPacking.LeftRight:
          width *= 0.5f;
          break;
      }
      float num1 = width / height;
      Rect rect;
      // ISSUE: explicit constructor call
      ((Rect) ref rect).\u002Ector(0.0f, 0.0f, 1f, 1f);
      if (scaleMode != 1)
      {
        if (scaleMode != 2)
        {
          if (scaleMode == null)
            ;
        }
        else
        {
          float num2 = ((Rect) ref screenRect).width / ((Rect) ref screenRect).height;
          if ((double) num2 > (double) num1)
          {
            float num3 = num1 / num2;
            // ISSUE: explicit constructor call
            ((Rect) ref screenRect).\u002Ector(((Rect) ref screenRect).xMin + (float) ((double) ((Rect) ref screenRect).width * (1.0 - (double) num3) * 0.5), ((Rect) ref screenRect).yMin, num3 * ((Rect) ref screenRect).width, ((Rect) ref screenRect).height);
          }
          else
          {
            float num4 = num2 / num1;
            // ISSUE: explicit constructor call
            ((Rect) ref screenRect).\u002Ector(((Rect) ref screenRect).xMin, ((Rect) ref screenRect).yMin + (float) ((double) ((Rect) ref screenRect).height * (1.0 - (double) num4) * 0.5), ((Rect) ref screenRect).width, num4 * ((Rect) ref screenRect).height);
          }
        }
      }
      else
      {
        float num5 = ((Rect) ref screenRect).width / ((Rect) ref screenRect).height;
        if ((double) num5 > (double) num1)
        {
          float num6 = num1 / num5;
          // ISSUE: explicit constructor call
          ((Rect) ref rect).\u002Ector(0.0f, (float) ((1.0 - (double) num6) * 0.5), 1f, num6);
        }
        else
        {
          float num7 = num5 / num1;
          // ISSUE: explicit constructor call
          ((Rect) ref rect).\u002Ector((float) (0.5 - (double) num7 * 0.5), 0.0f, num7, 1f);
        }
      }
      Graphics.DrawTexture(screenRect, texture, rect, 0, 0, 0, 0, GUI.color, material);
    }

    public static Texture2D GetReadableTexture(
      Texture inputTexture,
      bool requiresVerticalFlip,
      Orientation ori,
      Texture2D targetTexture)
    {
      Texture2D readableTexture = targetTexture;
      RenderTexture active = RenderTexture.active;
      int width = inputTexture.width;
      int height = inputTexture.height;
      RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, (RenderTextureFormat) 0);
      if (ori == Orientation.Landscape)
      {
        if (!requiresVerticalFlip)
        {
          Graphics.Blit(inputTexture, temporary);
        }
        else
        {
          GL.PushMatrix();
          RenderTexture.active = temporary;
          GL.LoadPixelMatrix(0.0f, (float) ((Texture) temporary).width, 0.0f, (float) ((Texture) temporary).height);
          Rect rect1;
          // ISSUE: explicit constructor call
          ((Rect) ref rect1).\u002Ector(0.0f, 0.0f, 1f, 1f);
          Rect rect2;
          // ISSUE: explicit constructor call
          ((Rect) ref rect2).\u002Ector(0.0f, -1f, (float) ((Texture) temporary).width, (float) ((Texture) temporary).height);
          Graphics.DrawTexture(rect2, inputTexture, rect1, 0, 0, 0, 0);
          GL.PopMatrix();
          GL.InvalidateState();
        }
      }
      if (Object.op_Equality((Object) readableTexture, (Object) null))
        readableTexture = new Texture2D(width, height, (TextureFormat) 5, false);
      RenderTexture.active = temporary;
      readableTexture.ReadPixels(new Rect(0.0f, 0.0f, (float) width, (float) height), 0, 0, false);
      readableTexture.Apply(false, false);
      RenderTexture.ReleaseTemporary(temporary);
      RenderTexture.active = active;
      return readableTexture;
    }

    private static int ParseTimeToMs(string text)
    {
      int timeToMs = 0;
      string[] strArray = text.Split(':', ',');
      if (strArray.Length == 4)
      {
        int num1 = int.Parse(strArray[0]);
        int num2 = int.Parse(strArray[1]);
        int num3 = int.Parse(strArray[2]);
        timeToMs = int.Parse(strArray[3]) + (num3 + (num2 + num1 * 60) * 60) * 1000;
      }
      return timeToMs;
    }

    public static List<Subtitle> LoadSubtitlesSRT(string data)
    {
      List<Subtitle> subtitleList = (List<Subtitle>) null;
      if (!string.IsNullOrEmpty(data))
      {
        data = data.Trim();
        string[] strArray1 = new Regex("\n\r|\r\n|\n|\r").Split(data);
        if (strArray1.Length >= 3)
        {
          subtitleList = new List<Subtitle>(256);
          int num1 = 0;
          int num2 = 0;
          Subtitle subtitle1 = (Subtitle) null;
          for (int index = 0; index < strArray1.Length; ++index)
          {
            switch (num2)
            {
              case 0:
                subtitle1 = new Subtitle();
                subtitle1.index = num1;
                break;
              case 1:
                string[] strArray2 = strArray1[index].Split(new string[1]
                {
                  " --> "
                }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray2.Length == 2)
                {
                  subtitle1.timeStartMs = Helper.ParseTimeToMs(strArray2[0]);
                  subtitle1.timeEndMs = Helper.ParseTimeToMs(strArray2[1]);
                  break;
                }
                break;
              default:
                if (!string.IsNullOrEmpty(strArray1[index]))
                {
                  if (num2 == 2)
                  {
                    subtitle1.text = strArray1[index];
                    break;
                  }
                  Subtitle subtitle2 = subtitle1;
                  subtitle2.text = subtitle2.text + "\n" + strArray1[index];
                  break;
                }
                break;
            }
            if (string.IsNullOrEmpty(strArray1[index]) && num2 > 1)
            {
              subtitleList.Add(subtitle1);
              num2 = 0;
              ++num1;
              subtitle1 = (Subtitle) null;
            }
            else
              ++num2;
          }
          if (subtitle1 != null)
            subtitleList.Add(subtitle1);
        }
        else
          Debug.LogWarning((object) "[AVProVideo] SRT format doesn't appear to be valid");
      }
      return subtitleList;
    }
  }
}
