using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public class LoggerIO:ILoggerIO
    {
        string path = @"C:\Users\Onshore\Documents\Logger\SelfHelpLogger.txt";

        public void LogError(string errorType, string message, string location)
        {
            string[] errorMsg = new string[] { DateTime.Now.ToString(), errorType, message, location };
            File.AppendAllLines(path, errorMsg);
        }
        public List<MessageDO> ReadLog()
        {
            List<MessageDO> lgs = new List<MessageDO>();
            string[] log = File.ReadAllLines(path);
            for (int i = 0; i < log.Length; i++)
            {
                MessageDO mes = new MessageDO();
                mes.DateTime = log[i];
                i++;
                mes.ErrorType = log[i];
                i++;
                mes.ErrorMessage = log[i];
                i++;
                mes.Layer = log[i];
                lgs.Add(mes);
            }
            return lgs;
        }
    }
}
