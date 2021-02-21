using Serialization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankOnline
{
	public class Machines
	{

		public PictureBox picture;
		public ObjectAction Player;
		TcpClient tcpClient;
		NetworkStream networkStream;
		public Machines(TcpListener tcpListener, TcpClient tcpClient)
		{
			picture = new PictureBox();
			picture.Size = new Size(40,40);
			picture.SizeMode = PictureBoxSizeMode.StretchImage;
			Player = new ObjectAction();
			this.tcpClient = tcpClient;
			this.networkStream = this.tcpClient.GetStream();
		}
		public  Machines(IPAddress iPAddress)
		{
			picture = new PictureBox();
			picture.Size = new Size(40, 40);
			picture.SizeMode = PictureBoxSizeMode.StretchImage;
			Player = new ObjectAction();
			tcpClient = new TcpClient();
			tcpClient.Connect(iPAddress, 8000);
			networkStream = tcpClient.GetStream();
		}
		public Machines()
		{
			picture = new PictureBox();
			picture.Size = new Size(40, 40);
			picture.SizeMode = PictureBoxSizeMode.StretchImage;
			Player = new ObjectAction();
		}

		/// <summary>
		/// UP->Right
		/// Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
		/// UP->Down
		/// Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
		/// UP->Left
		/// Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
		/// </summary>
		/// <param name="keys"></param>
		protected internal void Orientation(Keys keys)
		{
			Bitmap bitmap = new Bitmap(picture.Image);
			if (keys == Keys.Down)
			{
				Player.Down = true;
				Player.Y += 5;
				picture.Location = new Point(Player.X, Player.Y);
				if (Player.Left == true)
				{
					Player.Left = false;
					bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
					picture.Image = bitmap;

				}
				if (Player.Righ == true)
				{
					Player.Righ = false;
					bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
					picture.Image = bitmap;
				}
				if (Player.Up == true)
				{
					Player.Up = false;
					bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
					picture.Image = bitmap;
				}
			}
			else if(keys == Keys.Up)
			{
				Player.Y -= 5;
				picture.Location = new Point(Player.X, Player.Y);
				Player.Up = true;
				if (Player.Left == true) 
				{
					Player.Left = false;
					
					bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
					picture.Image = bitmap;
				}
				if (Player.Righ == true)
				{
					Player.Righ = false;
					
					bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
					picture.Image = bitmap;
				}
				if (Player.Down == true)
				{
					Player.Down = false;
					bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
					picture.Image = bitmap;
				}

			}
			
		}
		protected internal void Disconnect()
		{
			networkStream?.Close();
			tcpClient?.Close();
			
		}
	}

}
