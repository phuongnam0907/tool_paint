using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    public static class SerialCommunicator
    {
        // https://stackoverflow.com/questions/29973082/one-serial-port-in-multiple-forms 
        private static SerialPort _serialPort = new SerialPort();

        public static SerialPort SerialPort
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }
    }
}
