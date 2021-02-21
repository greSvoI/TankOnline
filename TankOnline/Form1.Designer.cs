
namespace TankOnline
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonWait = new System.Windows.Forms.Button();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.labelIP = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonWait
			// 
			this.buttonWait.Location = new System.Drawing.Point(16, 45);
			this.buttonWait.Name = "buttonWait";
			this.buttonWait.Size = new System.Drawing.Size(75, 23);
			this.buttonWait.TabIndex = 0;
			this.buttonWait.Text = "Wait";
			this.buttonWait.UseVisualStyleBackColor = true;
			this.buttonWait.Click += new System.EventHandler(this.buttonWait_Click);
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(324, 45);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(75, 23);
			this.buttonConnect.TabIndex = 1;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// labelIP
			// 
			this.labelIP.AutoSize = true;
			this.labelIP.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelIP.Location = new System.Drawing.Point(12, 13);
			this.labelIP.Name = "labelIP";
			this.labelIP.Size = new System.Drawing.Size(32, 22);
			this.labelIP.TabIndex = 2;
			this.labelIP.Text = "IP";
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox1.Location = new System.Drawing.Point(50, 13);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(349, 26);
			this.textBox1.TabIndex = 3;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(418, 90);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.labelIP);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.buttonWait);
			this.KeyPreview = true;
			this.Name = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonWait;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Label labelIP;
		private System.Windows.Forms.TextBox textBox1;
	}
}

