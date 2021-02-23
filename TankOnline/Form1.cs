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
using Serialization;

namespace TankOnline
{
	public partial class Form1 : Form
	{
		TcpListener tcpListener;
		TcpClient tcpClient;
		NetworkStream networkStream;

		IPAddress iPAddress = IPAddress.Parse("127.0.0.1");

		Machines machines1;
		Machines machines2;

		public Form1()
		{
			InitializeComponent();
		}

		private async void buttonWait_Click(object sender, EventArgs e)
		{
			this.Text = "Server";
			foreach (Control control in Controls)
			control.Visible = false;
			this.Size = new Size(800, 800);
			try
			{
				

				if (textBox1.Text == "")
				{
					tcpListener = new TcpListener(iPAddress, 8000);
					tcpListener.Start();

					await Task.Run(new Action(() => 
					{ 
						tcpClient = tcpListener.AcceptTcpClient();
						networkStream = tcpClient.GetStream();
					}));

					
					
					Thread thread = new Thread(new ThreadStart(GetAction));
					thread.Start();
				}
				else
				{
					tcpListener = new TcpListener(IPAddress.Parse(textBox1.Text), 8000);
					tcpListener.Start();

					await Task.Run(new Action(() =>
					{
						tcpClient = tcpListener.AcceptTcpClient();
						networkStream = tcpClient.GetStream();
					}));

					

					Thread thread = new Thread(new ThreadStart(GetAction));
					thread.Start();
				}
				machines1 = new Machines();
				machines2 = new Machines();

				machines1.picture.Image = Image.FromFile("green.png");
				this.Controls.Add(machines1.picture);
				machines1.Player.X = 300;
				machines1.Player.Y = 700;
				machines1.picture.Location = new Point(machines1.Player.X, machines1.Player.Y);


				machines2.picture.Image = Image.FromFile("red.png");
				this.Controls.Add(machines2.picture);
				machines2.Player.X = 400;
				machines2.Player.Y = 700;
				machines2.picture.Location = new Point(machines2.Player.X, machines2.Player.Y);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message+" buttonWait");
				this.Close();
			}
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			this.Text = "Client";
			foreach (Control control in Controls)
				control.Visible = false;

			this.Size = new Size(800, 800);

			try
			{
				if(textBox1.Text == "")
				{

					machines1 = new Machines();
					tcpClient = new TcpClient();
					tcpClient.Connect(iPAddress, 8000);

					networkStream = tcpClient.GetStream();

					Thread thread = new Thread(new ThreadStart(GetAction));
					thread.Start();
				}
				else
				{
					machines1 = new Machines();

					tcpClient = new TcpClient();
					tcpClient.Connect(IPAddress.Parse(textBox1.Text), 8000);

					networkStream = tcpClient.GetStream();

					Thread thread = new Thread(new ThreadStart(GetAction));
					thread.Start();
				}

				machines1.picture.Image = Image.FromFile("red.png");
				this.Controls.Add(machines1.picture);
				machines1.Player.X = 400;
				machines1.Player.Y = 700;
				machines1.picture.Location = new Point(machines1.Player.X,machines1.Player.Y);

				machines2 = new Machines();
				machines2.picture.Image = Image.FromFile("green.png");
				this.Controls.Add(machines2.picture);
				machines2.Player.X = 300;
				machines2.Player.Y = 700;
				machines2.picture.Location = new Point(machines2.Player.X, machines2.Player.Y);

			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message+ " buttonConnect");
				this.Close();
			}
		}
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keys = e.KeyData;
			machines1.Player.keys = e.KeyData;
			SendAction();
			machines1.Orientation(keys);
			machines1.picture.Location = new Point(machines1.Player.X, machines1.Player.Y);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			//networkStream?.Close();
			//tcpClient?.Close();
			//tcpListener?.Stop();
		}
		private void Shot()
		{

		}
			/// <summary>
			/// Перенес из класса Машина
			/// забыл зачем
			/// </summary>
		protected internal void GetAction()
		{
			try
			{
				while (true)
				{
					byte[] data = new byte[1024];
					do
					{
						networkStream.Read(data, 0, data.Length);
						machines2.Player = (ObjectAction)Serialization.Serialization.ByteArrayToObject(data);
					} while (networkStream.DataAvailable);

					machines2.Orientation(machines2.Player.keys);

					this.Invoke(new Action(() =>
					{
						this.machines2.picture.Location = new Point(this.machines2.Player.X, this.machines2.Player.Y);
						this.Invalidate();
					}));
					
				}

			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message + " GetAction");
			}
		}
		protected internal void SendAction()
		{
			byte[] data = Serialization.Serialization.ObjectToByteArray(machines1.Player);
			networkStream.Write(data, 0, data.Length);
		}
	}
}
