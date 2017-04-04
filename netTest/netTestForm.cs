using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netTest
{
	public partial class netTestForm : Form
	{
		private BackgroundWorker _worker;

		private void job(object sender, DoWorkEventArgs e)
		{
			bool success;
			string ipaddr = "";
			if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
			{
				// Find IPs
				var host = Dns.GetHostEntry(Dns.GetHostName());
				foreach (var ip in host.AddressList)
				{
					//ipaddr += ip.ToString() + Environment.NewLine;
				}

				// Test Router by tring to connect to the web interface.
				var router = netTestMain.GetDefaultGateway();
				//ipaddr += router.ToString() + Environment.NewLine;
				success = netTestMain.probeTcpPort(router, 1000, 80);
				if (!success)
				{
					ipaddr += "Router not responding. Try rebooting the router." + Environment.NewLine;
					e.Result = ipaddr;
					return;
				}
				ipaddr += "Router works!" + Environment.NewLine;

				// Test remote site
				var remote = IPAddress.Parse("8.8.8.8");
				//ipaddr += router.ToString() + Environment.NewLine;
				success = netTestMain.probeTcpPort(remote, 1000, 53);
				if (!success)
				{
					ipaddr += "Remote site not responding. Try rebooting your modem if you have one. If you don't, maybe your router might be the problem. Otherwise, it seems like your ISP is really down right now." + Environment.NewLine;
					e.Result = ipaddr;
					return;
				}
				ipaddr += "Remote site works!" + Environment.NewLine;

				// Test DNS
				try
				{
					IPAddress[] dns = Dns.GetHostAddresses("google.com");
				}
				catch (SocketException se)
				{
					ipaddr += "Default DNS didn't resolve." + Environment.NewLine;

					if(se.SocketErrorCode == SocketError.HostNotFound)
					{
						var opts = new JHSoftware.DnsClient.RequestOptions();
						opts.DnsServers = new IPAddress[]{IPAddress.Parse("8.8.8.8")};
						try
						{
							var ips = JHSoftware.DnsClient.LookupHost("google.com", JHSoftware.DnsClient.IPVersion.IPv4, opts);
							ipaddr += "Custom DNS did resolve. Your ISP DNS servers appear to be down.  You can wait for them to come back up or you can switch to OpenDNS http://208.69.38.205/";
						}
						catch
						{
							ipaddr += "Custom DNS didn't resolve.  This is strange.  Try rebooting your computer/router. If it still doesn't work, something fishy is going on - something like a firewall blocking DNS or something." + Environment.NewLine;
						}
						e.Result = ipaddr;
						return;
					}
				}
				ipaddr += "DNS works!" + Environment.NewLine;
			}
			else
			{
				ipaddr = "Not connected to a network. If you use WiFi and don't see your usual network, try rebooting your router.  If you use ethernet, make sure you're plugged in and also try rebooting.";
				e.Result = ipaddr;
				return;
			}

			ipaddr += "It looks like everything is working.  I'm not sure what's going on.";

			e.Result = ipaddr;
		}

		private void jobComplete(object sender, RunWorkerCompletedEventArgs e)
		{
			string ipaddr = (string)e.Result;
			this.Resolution.Text = ipaddr;

			this.progress.Visible = false;
		}

		public netTestForm()
		{
			InitializeComponent();
		}

		private void netTestForm_KeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}

		private void netTestForm_Load(object sender, EventArgs e)
		{
			this.versionLabel.Text += Assembly.GetExecutingAssembly().GetName().Version;
			this.progress.Visible = true;

			_worker = new BackgroundWorker();
			_worker.DoWork += new DoWorkEventHandler(job);
			_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(jobComplete);
			_worker.WorkerSupportsCancellation = true;

			_worker.RunWorkerAsync();
		}

		private void rescanButton_Click(object sender, EventArgs e)
		{
			this.progress.Visible = true;

			_worker = new BackgroundWorker();
			_worker.DoWork += new DoWorkEventHandler(job);
			_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(jobComplete);
			_worker.WorkerSupportsCancellation = true;

			_worker.RunWorkerAsync();
		}
	}
}
