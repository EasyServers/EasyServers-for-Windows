using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyServers
{
	partial class ServerControlPanelForm : Form
	{
		private static OpenFileDialog ofd = new OpenFileDialog();

		private static TextBox cmdLogTextBox = new TextBox();
		private static TextBox cmdInputTextBox = new TextBox();
		private static Button serverSendButton = new Button();

		private static Process proc = new Process();
		private static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

		public static string cmdLog = "";
		private static bool serverSendTextVaild = false;

		public ServerControlPanelForm()
		{
			this.SuspendLayout();
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.Name = "Server_ControlPanel";
			this.Text = Program.app_name + " - " + "MCコントロールパネル";
			this.Size = new Size(896, 504);
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.ClientSize = new Size(880, 465);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Icon = Program.ico;
			this.MaximizeBox = false;
			this.ResumeLayout(false);

			this.Shown += new EventHandler(ServerControlPanelForm_Shown);
			this.FormClosing += new FormClosingEventHandler(ServerControlPanelForm_FormClosing);
			this.FormClosed += new FormClosedEventHandler(ServerControlPanelForm_FormClosed);

			cmdLogTextBox = new TextBox()
			{
				Name = "cmdLog",
				Text = "",
				Multiline = true,
				ScrollBars = ScrollBars.Vertical,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Yu Gothic UI", 10.75F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Size = new Size(495, 320),
				Location = new Point(373, 45),
				ReadOnly = true,
				BackColor = Color.White,
				TabIndex = 0,
				TabStop = false
			};

			cmdInputTextBox = new TextBox()
			{
				Name = "cmdInput",
				Text = "",
				PlaceholderText = "サーバーに送るコマンドやメッセージをここに記入",
				Multiline = false,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Size = new Size(414, 23),
				Location = new Point(373, 371),
				TabIndex = 1,
				TabStop = true
			};
			cmdInputTextBox.KeyPress += new KeyPressEventHandler(CmdInputTextBox_KeyPress);
			cmdInputTextBox.KeyDown += new KeyEventHandler(CmdInputTextBox_KeyDown);

			serverSendButton = new Button()
			{
				Location = new Point(793, 371),
				Name = "ServerSend Button",
				Size = new Size(75, 23),
				TabIndex = 2,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "送信",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			serverSendButton.Click += new EventHandler(ServerSendButton_Click);

			timer = new System.Windows.Forms.Timer()
			{
				Interval = 100,
				Enabled = true
			};
			timer.Tick += new EventHandler(Timer_Tick);

			this.Controls.Add(cmdLogTextBox);
			this.Controls.Add(cmdInputTextBox);
			this.Controls.Add(serverSendButton);
		}

		private static async void ServerControlPanelForm_FormClosing(object? sender, FormClosingEventArgs e)
		{
			await FromClosing_Task();
		}

		private static async Task FromClosing_Task()
		{
			try
			{
				if (Process.GetProcessesByName("java").Length > 0)
				{
					if (proc != null && !proc.HasExited)
					{
						if (Regex.IsMatch(cmdLogTextBox.Text, @"\[[0-9]+\:[0-9]+\:[0-9]+ INFO\]\: Done"))
						{
							await proc.StandardInput.WriteLineAsync("stop");
						}
						proc.WaitForExit();
						proc.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("エラー:\r\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				if (Process.GetProcessesByName("java").Length > 0)
				{
					if (proc != null && !proc.HasExited)
					{
						proc.Kill();
						proc.Close();
					}
				}
				Application.Exit();
			}
		}

		private void ServerControlPanelForm_FormClosed(object? sender, FormClosedEventArgs e)
		{
			MainForm mfm = new MainForm();
			mfm.Show();
		}

		private bool sDoneSwitch = false;
		private bool sCloseSwitch = false;
		private void Timer_Tick(object? sender, EventArgs e)
		{
			if (Regex.IsMatch(cmdLogTextBox.Text, @"^\[[0-9]+\:[0-9]+\:[0-9]+ INFO\]\: Done \([0-9]+\.[0-9]{1,3}s\)\! For help, type ""help""", RegexOptions.Multiline) && !sDoneSwitch)
			{
				sDoneSwitch = true;
				serverSendButton.Enabled = true;
			}
			else if (Regex.IsMatch(cmdLogTextBox.Text, @"^\[[0-9]+\:[0-9]+\:[0-9]+ INFO\]\: Closing Server", RegexOptions.Multiline) && !sCloseSwitch)
			{
				sCloseSwitch = true;
				serverSendButton.Enabled = false;
			}
			if (serverSendTextVaild)
			{
				serverSendButton.Enabled = false;
			}
			else if (!serverSendTextVaild)
			{
				serverSendButton.Enabled = true;
			}
		}

		private void ServerSendButton_Click(object? sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(cmdInputTextBox.Text))
			{
				serverSendTextVaild = true;
			}
		}

		private void CmdInputTextBox_KeyPress(object? sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}

		private void CmdInputTextBox_KeyDown(object? sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					if (!string.IsNullOrEmpty(cmdInputTextBox.Text))
					{
						serverSendTextVaild = true;
					}
					break;
			}
		}

		private async void ServerControlPanelForm_Shown(object? sender, EventArgs e)
		{
			ofd = new OpenFileDialog()
			{
				FileName = "",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments).ToString(),
				Filter = "Java 実行ファイル(*.jar)|*.jar|All Files(*.*)|*.*",
				FilterIndex = 1,
				RestoreDirectory = true,
				CheckFileExists = true,
				CheckPathExists = true
			};

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				await ExecuteCommandAsync(ofd.FileName, 4, 4);
			}
			else
			{
				this.Close();
			}
		}

		private async Task ExecuteCommandAsync(string? jarFilePath, int xms = 2, int xmx = 2)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			using (proc = new Process())
			{
				proc.StartInfo = new ProcessStartInfo()
				{
					FileName = "cmd.exe",
					UseShellExecute = false,
					CreateNoWindow = true,
					Arguments = $"/c cd {Path.GetDirectoryName(jarFilePath)} & java -Xms{xms.ToString()}G -Xmx{xmx.ToString()}G -jar \"{Path.GetFileName(jarFilePath)}\" nogui",
					Verb = "RunAs",
					RedirectStandardOutput = true,
					RedirectStandardInput = true,
					StandardOutputEncoding = Encoding.GetEncoding("Shift_JIS"),
					StandardInputEncoding = Encoding.GetEncoding("Shift_JIS")
				};
				proc.Start();

				var tasks = new List<Task>();
				tasks.Add(OutputCmdLogAsync());
				tasks.Add(ServerSendAsync());

				await Task.WhenAll(tasks);
			}
		}

		private async Task OutputCmdLogAsync()
		{
			try
			{
				using (StreamReader srd = proc.StandardOutput)
				{
					while (!srd.EndOfStream)
					{
						string? _output = await srd.ReadLineAsync();
						if (!string.IsNullOrEmpty(_output))
						{
							Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
							byte[] bytesData = Encoding.UTF8.GetBytes(_output);
							Encoding utf8Enc = Encoding.UTF8;
							string output = utf8Enc.GetString(bytesData);

							output = output.Replace("\r\r\n", "\r\n");
							cmdLogTextBox.AppendText(output + Environment.NewLine);
						}
					}
				}
			}
			catch (Exception ex)
			{
				cmdLogTextBox.Text += "[" + DateTime.Now.ToString(@"HH:mm:ss") + " EasyServer Output Error]: " + ex.Message + "\r\n";
			}
		}

		private async Task ServerSendAsync()
		{
			await Task.Run(async () =>
			{
				try
				{
					string? command = cmdInputTextBox.Text;
					using (StreamWriter sinput = proc.StandardInput)
					{
						while (!sCloseSwitch)
						{
							if (!string.IsNullOrEmpty(command) && serverSendTextVaild)
							{
								serverSendTextVaild = false;
								await sinput.WriteLineAsync(command);
								cmdInputTextBox.Text = "";
							}
						}
					}
				}
				catch (Exception ex)
				{
					cmdLogTextBox.Text += "[" + DateTime.Now.ToString(@"HH:mm:ss") + " EasyServer Input Error]: " + ex.Message + "\r\n";
					serverSendTextVaild = false;
				}
			});
		}
	}
}