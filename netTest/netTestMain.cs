using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netTest
{
	static class netTestMain
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new netTestForm());
		}

		public static IPAddress GetDefaultGateway()
		{
			return NetworkInterface
				.GetAllNetworkInterfaces()
				.Where(n => n.OperationalStatus == OperationalStatus.Up)
				.SelectMany(n => n.GetIPProperties().GatewayAddresses)
				.Select(g => g.Address)
				.FirstOrDefault(a => a != null);
		}

		public static bool probeTcpPort(IPAddress router, int timeout, int port)
		{
			Socket s = new Socket(AddressFamily.Unspecified, SocketType.Stream, ProtocolType.Tcp);
			//s.ReceiveTimeout = timeout;
			//s.SendTimeout = timeout;
			//IPEndPoint ipe = new IPEndPoint(new IPAddress(new byte[] { 192, 168, 1, 1 }), 80);
			IPEndPoint ipe = new IPEndPoint(router, port);
			IAsyncResult result = s.BeginConnect(ipe, null, null);
			bool success = result.AsyncWaitHandle.WaitOne(timeout, true);
			s.Close();
			return success;
			//Debug.Print(router.ToString());
			//byte[] buf = new byte[1024];
			//try
			//{
			//	s.Receive(buf);
			//	return success;
			//}
			//catch (SocketException e)
			//{
			//	if (e.SocketErrorCode == SocketError.TimedOut)
			//		return success;
			//	return false;
			//}
			//finally
			//{
			//	s.Close();
			//}
		}
	}
}
