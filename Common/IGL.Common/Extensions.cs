﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGL
{
    public static class Extensions
    {
        public static string GetFullMessage(this Exception ex)
        {
            var error = string.Format("({0}) System running into a problem:", DateTime.Now.ToString("F")) + Environment.NewLine;
            error += "Source Infomation is:" + ex.Source + Environment.NewLine;
            error += "Error Detail: " + Environment.NewLine;
            error += ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine;
            var exception = ex.InnerException;
            while (exception != null)
            {
                error = error + Environment.NewLine + (string.IsNullOrEmpty(exception.Message) ? "" : exception.Message) + Environment.NewLine + (string.IsNullOrEmpty(exception.StackTrace) ? "" : exception.StackTrace) + Environment.NewLine;
                exception = exception.InnerException;
            }
            error += "**********************************************************************************************" + Environment.NewLine;
            return error;
        }

        public static T CloneJson<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }
    }
}

