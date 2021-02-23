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
using System.Collections.Generic;
using Timer = System.Windows.Forms.Timer;

namespace TankOnline
{
	public partial class Battle : Form
	{
		TcpListener tcpListener;
		TcpClient tcpClient;
		NetworkStream networkStream;
		Timer timer = new Timer();
		List<Shells> Bomb = new List<Shells>();
		public static PictureBox MyPictureBox { get; set; }
		IPAddress iPAddress = IPAddress.Parse("127.0.0.1");

		public static Machines machines1;
		public static Machines machines2;
		static bool connect = false;

		public Battle()
		{
			InitializeComponent();
			timer.Interval = 10;
			timer.Tick += Timer_Tick;
			timer.Start();
			MyPictureBox = new PictureBox()
			{
				Image = Image.FromFile("background.jpg"),
				SizeMode = PictureBoxSizeMode.StretchImage,
				Size = ClientSize
			};
			Controls.Add(MyPictureBox);
			
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			foreach (Shells item in Bomb)
			{
				if (item.direction == Keys.Up) item.point.Y -= 8;
				if (item.direction == Keys.Down) item.point.Y += 8;
				if (item.direction == Keys.Left) item.point.X -= 8;
				if (item.direction == Keys.Right) item.point.X += 8;
				MyPictureBox.Invalidate();
			}
			
		}
		//private void Connect()
		//{
		//	tcpListener = new TcpListener(iPAddress, 8000);
		//	tcpListener.Start();
		//	tcpClient = tcpListener.AcceptTcpClient();
		//	networkStream = tcpClient.GetStream();
		//	connect = true;
		//	Thread thread = new Thread(new ThreadStart(GetAction));
		//	thread.Start();
		//}
		private async void buttonWait_Click(object sender, EventArgs e)
		{
			machines1 = new Machines(Brushes.Green);
			machines2 = new Machines(Brushes.Red);

			this.Text = "Server";
			foreach (Control control in Controls)
				if(control is Button || control is TextBox || control is Label)
							control.Visible = false;

			this.Size = MyPictureBox.Size =  new Size(800, 800);
			try
			{
				if (textBox1.Text == "")
				{
					tcpListener = new TcpListener(iPAddress, 8000);
					tcpListener.Start();

					await Task.Run(new Action(() =>
					{
						this.tcpClient = tcpListener.AcceptTcpClient();
						this.networkStream = tcpClient.GetStream();

					}));

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

				}
				Thread thread = new Thread(new ThreadStart(GetAction));
				thread.Start();

				machines1.picture.Image = Image.FromFile("green.png");
				machines1.picture.BackColor = Color.Transparent;
				MyPictureBox.Controls.Add(machines1.picture);

				machines1.Player.X = 300;
				machines1.Player.Y = 700;
				machines1.picture.Location = new Point(machines1.Player.X, machines1.Player.Y);

				machines2.picture.Image = Image.FromFile("red.png");
				machines2.picture.BackColor = Color.Transparent;
				MyPictureBox.Controls.Add(machines2.picture);

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
			machines1 = new Machines(Brushes.Red);
			machines2 = new Machines(Brushes.Green);

			this.Text = "Client";
			foreach (Control control in Controls)
				if (control is Button || control is TextBox || control is Label)
					control.Visible = false;

			this.Size = MyPictureBox.Size = new Size(800, 800);

			try
			{
				if(textBox1.Text == "")
				{
					tcpClient = new TcpClient();
					tcpClient.Connect(iPAddress, 8000);
					networkStream = tcpClient.GetStream();
				}
				else
				{

					tcpClient = new TcpClient();
					tcpClient.Connect(IPAddress.Parse(textBox1.Text), 8000);
					networkStream = tcpClient.GetStream();
				}


				Thread thread = new Thread(new ThreadStart(GetAction));
				thread.Start();


				machines1.picture.Image = Image.FromFile("red.png");
				machines1.picture.BackColor = Color.Transparent;
				MyPictureBox.Controls.Add(machines1.picture);

				machines1.Player.X = 400;
				machines1.Player.Y = 700;
				machines1.picture.Location = new Point(machines1.Player.X,machines1.Player.Y);

				
				machines2.picture.Image = Image.FromFile("green.png");
				machines2.picture.BackColor = Color.Transparent;
				MyPictureBox.Controls.Add(machines2.picture);

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
			if(e.KeyData == Keys.Space ||e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.Right || e.KeyData == Keys.Left)
			{
				if (e.KeyData == Keys.Space)
				{

					if (machines1.Player.Up) { Bomb.Add(new Shells(machines1,Keys.Up)); machines1.Player.KeysShot = Keys.Up; }
					if (machines1.Player.Down) { Bomb.Add(new Shells(machines1,Keys.Down)); machines1.Player.KeysShot = Keys.Down; }
					if (machines1.Player.Righ) { Bomb.Add(new Shells(machines1, Keys.Right)); machines1.Player.KeysShot = Keys.Right; }
					if (machines1.Player.Left) { Bomb.Add(new Shells(machines1, Keys.Left)); machines1.Player.KeysShot = Keys.Left; }
					machines1.Player.KeysShot = e.KeyData;

					machines1.Player.Shot = true;
					
				}
				else if(e.KeyData !=Keys.Space)
				{
					machines1.Player.KeysMove = e.KeyData;
					SendAction();
					machines1.Orientation(e.KeyData);
				}
				SendAction();
				machines1.Player.Shot = false;
				machines1.picture.Location = new Point(machines1.Player.X, machines1.Player.Y);
			}
			
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			networkStream?.Close();
			tcpClient?.Close();
			tcpListener?.Stop();
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

						machines2.Orientation(machines2.Player.KeysMove);

						if (machines2.Player.Shot) Bomb.Add(new Shells(machines2, machines2.Player.KeysShot));

						this.Invoke(new Action(() =>
						{
							machines2.picture.Location = new Point(machines2.Player.X, machines2.Player.Y);
							this.Invalidate();
						}));
					} while (networkStream.DataAvailable);
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
