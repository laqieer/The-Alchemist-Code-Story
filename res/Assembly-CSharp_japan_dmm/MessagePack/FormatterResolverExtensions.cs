// Decompiled with JetBrains decompiler
// Type: MessagePack.FormatterResolverExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using System.Reflection;

#nullable disable
namespace MessagePack
{
  public static class FormatterResolverExtensions
  {
    public static IMessagePackFormatter<T> GetFormatterWithVerify<T>(
      this IFormatterResolver resolver)
    {
      IMessagePackFormatter<T> formatter;
      try
      {
        formatter = resolver.GetFormatter<T>();
      }
      catch (TypeInitializationException ex)
      {
        Exception exception = (Exception) ex;
        while (exception.InnerException != null)
          exception = exception.InnerException;
        throw exception;
      }
      return formatter != null ? formatter : throw new FormatterNotRegisteredException(typeof (T).FullName + " is not registered in this resolver. resolver:" + resolver.GetType().Name);
    }

    public static object GetFormatterDynamic(this IFormatterResolver resolver, Type type)
    {
      return ReflectionExtensions.GetRuntimeMethod(typeof (IFormatterResolver), "GetFormatter", Type.EmptyTypes).MakeGenericMethod(type).Invoke((object) resolver, (object[]) null);
    }
  }
}
