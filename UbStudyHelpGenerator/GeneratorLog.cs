using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStandardObjects;

namespace UbStudyHelpGenerator
{


    internal class GeneratorLog : Log
    {

        public override void Clear()
        {
            FireShowMessage(null);
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Error(string message)
        {
            FireShowMessage(message, true);
        }

        public override void Error(string message, Exception ex)
        {
            FireShowMessage(message, true);
        }

        public override void FatalError(string message)
        {
            FireShowMessage(message, true, true);
        }

        public override void Info(string message)
        {
            FireShowMessage(message);
        }


        public override void Initialize(string path, bool append = false)
        {
        }

        public override void NonFatalError(string message)
        {
        }

        public override void ShowLog()
        {
            
        }

        public override void Warn(string message)
        {
        }
    }
}
