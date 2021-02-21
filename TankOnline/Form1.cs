using System;
using System.Net;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace TankOnline
{
	public partial class Form1 : Form
	{
		TcpListener tcpListener;
		TcpClient tcpClient;
		NetworkStream networkStream;
		IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
		public Form1()
		{
			InitializeComponent();
		}

		private async void buttonWait_Click(object sender, EventArgs e)
		{
			foreach (Control control in Controls)
				control.Visible = false;
			try
			{
				if(textBox1.Text == "")
				{
					tcpListener = new TcpListener(iPAddress, 8000);
					tcpListener.Start();
					await Task.Run(new Action(() => tcpClient = tcpListener.AcceptTcpClient()));
					networkStream = tcpClient.GetStream();
				}
				else
				{
					tcpListener = new TcpListener(IPAddress.Parse(textBox1.Text), 8000);
					tcpListener.Start();
					await Task.Run(new Action(() => tcpClient = tcpListener.AcceptTcpClient()));
					networkStream = tcpClient.GetStream();
				}

			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message+" buttonWait");
			}
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			foreach (Control control in Controls)
				control.Visible = false;
			try
			{

				if(textBox1.Text == "")
				{
					tcpClient = new TcpClient();
					tcpClient.Connect(iPAddress, 8000);
					networkStream = tcpClient.GetStream();
				}
				




			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message+ " buttonConnect");
			}
		}
		private void GetAction()
		{

		}
	}
}
