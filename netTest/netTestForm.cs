using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
			string ipaddr = "";
			if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
			{
				var host = Dns.GetHostEntry(Dns.GetHostName());
				foreach (var ip in host.AddressList)
				{
					ipaddr += ip.ToString() + Environment.NewLine;
				}
				ipaddr += netTestMain.GetDefaultGateway().ToString() + Environment.NewLine;
			}
			else
			{
				ipaddr = "Not connected to a network.";
			}

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
			this.progress.Visible = true;

			_worker = new BackgroundWorker();
			_worker.DoWork += new DoWorkEventHandler(job);
			_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(jobComplete);

			_worker.RunWorkerAsync();
		}
	}
}
