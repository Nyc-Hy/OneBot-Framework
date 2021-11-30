using System;
using System.Globalization;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
//using YukariToolBox.FormatLog;
using YukariToolBox.LightLog;


namespace OneBot.CommandRoute.Utils
{
    public class YukariToolBoxLogger : ILogService
    {
        public ILogger<YukariToolBoxLogger> Logger { get; }

        public void Debug(string source, string message)
        {
            Logger.LogDebug($"[{source}]{message}");
        }

        public void Debug<T>(string source, string message, T context)
        {
            Logger.LogDebug($"[{context?.GetType()}{source}]{message}");
        }

        public void Error(string source, string message)
        {
            Logger.LogError($"[{source}]{message}");
        }

        public void Error(Exception exception, string source, string message)
        {
            Logger.LogError($"{JsonConvert.SerializeObject(exception)}[{source}]{message}");
        }

        public void Error<T>(string source, string message, T context)
        {
            Logger.LogError($"[{context?.GetType()}{source}]{message}");
        }

        public void Error<T>(Exception exception, string source, string message, T context)
        {
            Logger.LogError($"{context?.GetType()}{JsonConvert.SerializeObject(exception)}[{source}]{message}");
        }

        public void Fatal(Exception exception, string source, string message)
        {
            Logger.Log(Microsoft.Extensions.Logging.LogLevel.None,$"[{source}]{message}");
        }

        public void Fatal<T>(Exception exception, string source, string message, T context)
        {
            throw new NotImplementedException();
        }

        public void Info(string source, string message)
        {
            throw new NotImplementedException();
        }

        public void Info<T>(string source, string message, T context)
        {
            throw new NotImplementedException();
        }

        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        public void UnhandledExceptionLog(UnhandledExceptionEventArgs args)
        {
            StringBuilder errorLogBuilder = new StringBuilder();
            errorLogBuilder.Append("检测到未处理的异常");
            if (args.IsTerminating)
                errorLogBuilder.Append("，服务器将停止运行");
            Logger.LogError(0, args.ExceptionObject as Exception, errorLogBuilder.ToString());
            Warning("Sora", "将在5s后自动退出");
            Thread.Sleep(5000);
            Environment.Exit(-1);
        }

        public void Verbos(string source, string message)
        {
            throw new NotImplementedException();
        }

        public void Verbos<T>(string source, string message, T context)
        {
            throw new NotImplementedException();
        }

        public void Warning(string source, string message)
        {
            throw new NotImplementedException();
        }

        public void Warning<T>(string source, string message, T context)
        {
            throw new NotImplementedException();
        }

        #region 之前的日志逻辑 Ps：已注释
        //public YukariToolBoxLogger(ILogger<YukariToolBoxLogger> logger)
        //{
        //    Logger = logger;
        //}

        //public void Info(object type, object message)
        //{
        //    Logger.LogInformation($"[{type}]{message}");
        //}

        //public void Warning(object type, object message)
        //{
        //    Logger.LogWarning($"[{type}]{message}");

        //}

        //public void Error(object type, object message)
        //{
        //    Logger.LogError($"[{type}]{message}");

        //}

        //public void Fatal(object type, object message)
        //{
        //    Logger.LogCritical($"[{type}]{message}");
        //}

        //public void Debug(object type, object message)
        //{
        //    Logger.LogDebug($"[{type}]{message}");
        //}

        //public void UnhandledExceptionLog(UnhandledExceptionEventArgs args)
        //{
        //    StringBuilder errorLogBuilder = new StringBuilder();
        //    errorLogBuilder.Append("检测到未处理的异常");
        //    if (args.IsTerminating)
        //        errorLogBuilder.Append("，服务器将停止运行");
        //    Logger.LogError(0, args.ExceptionObject as Exception, errorLogBuilder.ToString());
        //    Warning("Sora", "将在5s后自动退出");
        //    Thread.Sleep(5000);
        //    Environment.Exit(-1);
        //}
        #endregion


    }
}
