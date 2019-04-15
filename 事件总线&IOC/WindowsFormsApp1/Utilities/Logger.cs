using System;
using System.Collections.Generic;

namespace Utilities
{
    public class Logger
    {
        //实例化日志对象
        private static log4net.ILog s_logger = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Logger()
        {

        }

        public static void LoadConfig()
        {
            //运行配置
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void WriteInformation(string message)
        {
            LoadConfig();

            string processId = GetProcessId();

            s_logger.Info($"{processId} -【Info】: {message}", null);
        }

        public static void WriteError(Exception exception, string repairType, string message = "")
        {
            LoadConfig();

            string processId = GetProcessId();

            s_logger.Error($"{processId} -【{repairType}】: {message}", exception);
        }
        
        private static string GetProcessId()
        {
            try
            {
                return System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
            }
            catch
            {
                return "Unknow";
            }
        }
    }
}
