
namespace HttpWebServer.Classes.HttpServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Sockets;
    using System.Net;
    using HttpWebServer.Interfaces;
    using System.Threading;

    public class HttpServer : IHttpServer
    {
        private const int _defaultPort = 8080;
        private int _serverPort;
        private TcpListener _listerner;
        private bool _isServerRunning = false;
        private Thread _tread;
        private TcpClient _tcpClient;
        public HttpServer() 
        {
            this._serverPort = _defaultPort;
            this._listerner = new TcpListener(IPAddress.Any,this._serverPort);
        }
        public HttpServer(int port)
        {
            this._serverPort = port;
            this._listerner = new TcpListener(IPAddress.Any, this._serverPort);
        }
        /// <summary>
        /// Return the port that current server is listening
        /// </summary>
        public int GetPort
        {
            get
            {
                return this._serverPort;
            }
        }
        /// <summary>
        /// Return boolen if the server is listening for incoming request to a specific port
        /// </summary>
        public bool IsCurrenInstanceOfTheServerRunning
        {
            get
            {
                return this._isServerRunning;
            }
        }

        public bool Start()
        {
            this._isServerRunning = true;
            this._tread = new Thread(new ThreadStart(Run));
            this._tread.Start();
            return this._isServerRunning;
        }
        #region Private Server Methods 
        private void Run()
        {
            this._listerner.Start(); // Server start to listen for incoming request on given port
            while(this._isServerRunning)
            {
                this._tcpClient = this._listerner.AcceptTcpClient();
                string serverResponse = this.HandeClient(this._tcpClient);
                this._tcpClient.Close();
            }
            this._isServerRunning = false;
        }
        private string HandeClient(TcpClient client)
        {
            return "";
        }
        #endregion
    }
}
