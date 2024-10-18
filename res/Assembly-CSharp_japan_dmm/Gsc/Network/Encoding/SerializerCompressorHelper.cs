// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Encoding.SerializerCompressorHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using System.IO;
using UnityEngine;

#nullable disable
namespace Gsc.Network.Encoding
{
  public static class SerializerCompressorHelper
  {
    public static byte[] Encode<T>(
      T objectToEncode,
      bool serializeWithMessagePack = false,
      CompressMode compressMode = CompressMode.Lz4,
      bool useFromJson = false)
    {
      try
      {
        byte[] numArray = (byte[]) null;
        if (!serializeWithMessagePack && compressMode == CompressMode.None)
          return (byte[]) (object) objectToEncode;
        using (MemoryStream outputStream = new MemoryStream())
        {
          if (serializeWithMessagePack && compressMode == CompressMode.Lz4)
          {
            if (useFromJson)
              SerializerCompressorHelper.LZ4SerializeFromJson((Stream) outputStream, (string) (object) objectToEncode);
            else
              SerializerCompressorHelper.LZ4Serialize<T>((Stream) outputStream, objectToEncode);
          }
          else if (serializeWithMessagePack && compressMode != CompressMode.Lz4)
          {
            if (useFromJson)
            {
              SerializerCompressorHelper.SerializeFromJson((Stream) outputStream, (string) (object) objectToEncode);
              outputStream.Position = 0L;
              numArray = outputStream.ToArray();
            }
            else
            {
              SerializerCompressorHelper.Serialize<T>((Stream) outputStream, objectToEncode);
              outputStream.Position = 0L;
              numArray = outputStream.ToArray();
            }
          }
          else if (!serializeWithMessagePack && (object) typeof (T) == (object) typeof (byte[]))
            numArray = (byte[]) (object) objectToEncode;
          numArray = outputStream.ToArray();
        }
        return numArray;
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex);
        return (byte[]) null;
      }
    }

    public static T Decode<T>(
      byte[] dataToDecode,
      bool deserializeWithMessagePack = false,
      CompressMode decompressMode = CompressMode.Lz4,
      bool useToJson = false,
      bool printExceptions = true)
    {
      try
      {
        if (!deserializeWithMessagePack && decompressMode == CompressMode.None)
          return (T) Convert.ChangeType((object) dataToDecode, typeof (T));
        T obj;
        using (MemoryStream inputStream = decompressMode == CompressMode.Lz4 || decompressMode == CompressMode.None && deserializeWithMessagePack ? new MemoryStream(dataToDecode) : new MemoryStream())
        {
          if (decompressMode == CompressMode.Lz4 && deserializeWithMessagePack)
          {
            obj = !useToJson ? SerializerCompressorHelper.LZ4Deserialize<T>((Stream) inputStream) : (T) Convert.ChangeType((object) SerializerCompressorHelper.LZ4DeserializeToJson((Stream) inputStream), typeof (T));
            return obj;
          }
          if (decompressMode == CompressMode.None && deserializeWithMessagePack)
          {
            obj = !useToJson ? SerializerCompressorHelper.Deserialize<T>((Stream) inputStream) : (T) Convert.ChangeType((object) SerializerCompressorHelper.DeserializeToJson((Stream) inputStream), typeof (T));
            return obj;
          }
          obj = (T) Convert.ChangeType((object) inputStream.ToArray(), typeof (T));
        }
        return obj;
      }
      catch (Exception ex)
      {
        if (printExceptions)
          Debug.LogError((object) ex);
        throw ex;
      }
    }

    private static void LZ4Serialize<T>(Stream outputStream, T objectToSerialize)
    {
      LZ4MessagePackSerializer.Serialize<T>(outputStream, objectToSerialize);
    }

    private static void LZ4SerializeFromJson(Stream outputStream, string jsonToSerialize)
    {
      byte[] buffer = LZ4MessagePackSerializer.FromJson(jsonToSerialize);
      outputStream.Write(buffer, 0, buffer.Length);
    }

    private static void Serialize<T>(Stream outputStream, T objectToSerialize)
    {
      MessagePackSerializer.Serialize<T>(outputStream, objectToSerialize);
    }

    private static void SerializeFromJson(Stream outputStream, string jsonToSerialize)
    {
      byte[] buffer = MessagePackSerializer.FromJson(jsonToSerialize);
      outputStream.Write(buffer, 0, buffer.Length);
    }

    private static T LZ4Deserialize<T>(Stream inputStream)
    {
      return LZ4MessagePackSerializer.Deserialize<T>(inputStream);
    }

    private static string LZ4DeserializeToJson(Stream inputStream)
    {
      byte[] bytes;
      using (BinaryReader binaryReader = new BinaryReader(inputStream))
        bytes = binaryReader.ReadBytes((int) inputStream.Length);
      return LZ4MessagePackSerializer.ToJson(bytes);
    }

    private static T Deserialize<T>(Stream inputStream)
    {
      return MessagePackSerializer.Deserialize<T>(inputStream);
    }

    private static string DeserializeToJson(Stream inputStream)
    {
      byte[] bytes;
      using (BinaryReader binaryReader = new BinaryReader(inputStream))
        bytes = binaryReader.ReadBytes((int) inputStream.Length);
      return MessagePackSerializer.ToJson(bytes);
    }
  }
}
