using System.Diagnostics;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace EasyServers
{
	partial class MainForm : Form
	{
		private static Panel mainMenuPanel = new Panel();
		private static PictureBox titlePicture = new PictureBox();
		private static Button serverCreateButton = new Button();
		private static Button serverOperationButton = new Button();
		private static Button portOpenFormButton = new Button();
		private static Button exitButton = new Button();

		private static Label versionLabel = new Label();
		private static Label copyrightLabel = new Label();

		private static Panel serverCreateScreen_Software = new Panel();

		private IContainer? components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public MainForm()
		{
			this.SuspendLayout();
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.Name = "MainWindow";
			this.Text = Program.app_name;
			this.Size = new Size(896, 504);
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.ClientSize = new Size(880, 465);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Icon = Program.ico;
			this.MaximizeBox = false;
			this.ResumeLayout(false);

			titlePicture = new PictureBox()
			{
				Name = "Title",
				Image = Properties.Resources.title,
				Location = new Point(0, 0),
				Size = new Size(880, 105),
				SizeMode = PictureBoxSizeMode.Zoom,
				Dock = DockStyle.Top,
				BackColor = Color.Transparent,
				TabStop = false,
				TabIndex = 1
			};
			titlePicture.ResumeLayout(false);

			serverCreateButton = new Button()
			{
				Name = "ServerCreate",
				Text = "サーバーを作成",
				Size = new Size(227, 87),
				Location = new Point(225 - 20, 167),
				Font = new Font("Yu Gothic UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128),
				UseVisualStyleBackColor = true,
				TabStop = false,
				TabIndex = 2
			};
			serverCreateButton.Click += new EventHandler(ServerCreateButton_Click);

			serverOperationButton = new Button()
			{
				Name = "ServerOperation",
				Text = "サーバーを操作",
				Size = new Size(227, 87),
				Location = new Point(450 + 20, 167),
				Font = new Font("Yu Gothic UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128),
				UseVisualStyleBackColor = true,
				TabStop = false,
				TabIndex = 3
			};

			portOpenFormButton = new Button()
			{
				Name = "PortOpenFormOpen",
				Text = "ポート開放",
				Size = new Size(227, 87),
				Location = new Point(225 - 20, 281),
				Font = new Font("Yu Gothic UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128),
				UseVisualStyleBackColor = true,
				TabStop = false,
				TabIndex = 4
			};

			exitButton = new Button()
			{
				Name = "Exit",
				Text = "終了",
				Size = new Size(227, 87),
				Location = new Point(450 + 20, 281),
				Font = new Font("Yu Gothic UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128),
				UseVisualStyleBackColor = true,
				TabStop = false,
				TabIndex = 5
			};
			exitButton.Click += new EventHandler(ExitButton_Click);

			versionLabel = new Label()
			{
				Name = "Version",
				Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 128),
				Text = "v" + Program.app_version,
				AutoSize = true,
				Location = new Point(12, 432),
				TabIndex = 6
			};

			copyrightLabel = new Label()
			{
				Name = "Version",
				Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "(C) 2023 Shotadft. All rights reserved. (EasyServers)\r\n(C) 2011-2023 Mojang AB. (Minecraft)",
				AutoSize = true,
				Location = new Point(512, 413),
				TabIndex = 7
			};

			mainMenuPanel = new Panel()
			{
				Name = "MainMenu",
				Dock = DockStyle.Fill,
				AutoSize = true,
				Location = new Point(0, 0),
				BorderStyle = BorderStyle.None,
				Enabled = true,
				Visible = true,
				TabIndex = 0
			};
			mainMenuPanel.ResumeLayout(false);
			mainMenuPanel.PerformLayout();
			mainMenuPanel.Controls.Add(titlePicture);
			mainMenuPanel.Controls.Add(serverCreateButton);
			mainMenuPanel.Controls.Add(serverOperationButton);
			mainMenuPanel.Controls.Add(portOpenFormButton);
			mainMenuPanel.Controls.Add(exitButton);
			mainMenuPanel.Controls.Add(versionLabel);
			mainMenuPanel.Controls.Add(copyrightLabel);

			serverCreateScreen_Software = new Panel()
			{
				Name = "ServerCreateScreen_Software",
				Dock = DockStyle.Fill,
				AutoSize = true,
				Location = new Point(0, 0),
				BorderStyle = BorderStyle.None,
				Enabled = false,
				Visible = false,
				TabIndex = 8
			};

			this.Controls.Add(mainMenuPanel);
		}

		private void ServerCreateButton_Click(object? sender, EventArgs e)
		{
			ServerControlPanelForm controlPanelForm = new ServerControlPanelForm();
			controlPanelForm.Show();
			this.Hide();
			/*
			mainMenuPanel.Enabled = false;
			mainMenuPanel.Visible = false;
			serverCreateScreen_Software.Visible = true;
			serverCreateScreen_Software.Enabled = true;
			*/
		}

		private void ExitButton_Click(object? sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}