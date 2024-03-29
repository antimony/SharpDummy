﻿using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SharpDummy.Web.Core.Mvc
{
    public class FailJsonNetResult:JsonNetResult
    {
        public FailJsonNetResult()
        {
            Data = new { status = "fail" };
        }

        public FailJsonNetResult(string message)
        {
            Data = new {status = "fail", message};
        }
    }

    public class SuccessJsonNetResult:JsonNetResult
    {
        public SuccessJsonNetResult()
        {
            Data = new {status = "success"};
        }
    }

    public class MessageJsonNetResult:JsonNetResult
    {
        public MessageJsonNetResult(string message)
        {
            Data = new {status = "success", message};
        }
    }

    public class JsonNetResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }

        public JsonSerializerSettings SerializerSettings { get; set; }
        public Formatting Formatting { get; set; }

        public JsonNetResult()
        {
            SerializerSettings = new JsonSerializerSettings();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType)
              ? ContentType
              : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

                JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
                serializer.Serialize(writer, Data);

                writer.Flush();
            }
        }
    }
}
