using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankOnline
{
	class Shells : IDisposable
	{
		public Keys direction;
		public Point point;
		Brush brush;
		public Shells(Machines machines, Keys keys)
		{
			brush = machines.brush;
			direction = keys;
			point = new Point(machines.picture.Location.X+15,machines.picture.Location.Y+5);
			Battle.MyPictureBox.Paint += MyPictureBox_Paint;
		}
		private void MyPictureBox_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.FillEllipse(brush, point.X, point.Y, 10, 10);
		}

		public void Dispose()
		{
			Battle.MyPictureBox.Paint -= MyPictureBox_Paint;
		}
	}
}
