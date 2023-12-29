using CoreRCON;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyServers
{
	partial class ServerControlPanelForm : Form
	{
		private static OpenFileDialog ofd = new OpenFileDialog();

		private static TextBox cmdLogTextBox = new TextBox();
		public static TextBox cmdInputTextBox = new TextBox();
		private static Button serverSendButton = new Button();
		private static Button shortcutButton1 = new Button();
		private static Button serverStopButton = new Button();

		private static Process proc = new Process();
		private static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

		public static string cmdLog = "";
		public static bool serverSendTextVaild = false;

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
				WordWrap = true,
				Multiline = true,
				ScrollBars = ScrollBars.Both,
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
				PlaceholderText = "サーバーに送るコマンドをここに記入",
				Multiline = false,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Size = new Size(414, 23),
				Location = new Point(373, 400),
				TabIndex = 1,
				TabStop = true
			};
			cmdInputTextBox.KeyPress += new KeyPressEventHandler(CmdInputTextBox_KeyPress);
			cmdInputTextBox.KeyDown += new KeyEventHandler(CmdInputTextBox_KeyDown);

			serverSendButton = new Button()
			{
				Location = new Point(793, 400),
				Name = "ServerSendButton",
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

			shortcutButton1 = new Button()
			{
				Location = new Point(373, 371),
				Name = "SayShortCutButton",
				Size = new Size(92, 23),
				TabIndex = 3,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "メッセージ送信",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			shortcutButton1.Click += new EventHandler(ShortcutButton1_Click);

			serverStopButton = new Button()
			{
				Location = new Point(793, 371),
				Name = "SayShortCutButton",
				Size = new Size(75, 23),
				TabIndex = 3,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "停止",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			serverStopButton.Click += new EventHandler(ServerStopButton_Click);

			this.Controls.Add(cmdLogTextBox);
			this.Controls.Add(cmdInputTextBox);
			this.Controls.Add(serverSendButton);
			this.Controls.Add(shortcutButton1);
			this.Controls.Add(serverStopButton);
		}

		private void ServerStopButton_Click(object? sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("本当にサーバーを停止させますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (result == DialogResult.Yes)
			{
				cmdInputTextBox.Text = "stop";
				serverSendTextVaild = true;
			}
		}

		private void ShortcutButton1_Click(object? sender, EventArgs e)
		{
			SayCommandShortForm form = new SayCommandShortForm();
			form.Show(this);
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
						if (Regex.IsMatch(cmdLogTextBox.Text, @"^\[[0-9]+\:[0-9]+\:[0-9]+ INFO\]\: Done \([0-9]+\.[0-9]{1,3}s\)\! For help, type ""help""", RegexOptions.Multiline))
						{
							await proc.StandardInput.WriteLineAsync("stop");
							proc.WaitForExit();
						}
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
				serverStopButton.Enabled = true;
				shortcutButton1.Enabled = true;
			}
			else if (Regex.IsMatch(cmdLogTextBox.Text, @"^\[[0-9]+\:[0-9]+\:[0-9]+ INFO\]\: Closing Server", RegexOptions.Multiline) && !sCloseSwitch)
			{
				sCloseSwitch = true;
				serverSendButton.Enabled = false;
				serverStopButton.Enabled = false;
				shortcutButton1.Enabled = false;
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
			try
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

					await Task.Run(async () =>
					{
						await Task.WhenAll(OutputCmdLogAsync(), ServerSendAsync());
					});
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("エラーが発生しました！\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					using (StreamWriter sinput = proc.StandardInput)
					{
						while (!sCloseSwitch)
						{
							string? command = cmdInputTextBox.Text;
							if (!string.IsNullOrEmpty(command) && serverSendTextVaild)
							{
								serverSendTextVaild = false;
								await sinput.WriteLineAsync(command);
								command = "";
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

	partial class SayCommandShortForm : Form
	{
		public static Label label = new Label();
		private static Button sendButton = new Button();
		private static TextBox inputTextBox = new TextBox();

		public SayCommandShortForm()
		{
			this.SuspendLayout();
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.Name = "SayCommandShortForm";
			this.Text = "ショートカット";
			this.Size = new Size(370, 130);
			this.ClientSize = new Size(354, 91);
			this.ResumeLayout(false);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.ShowIcon = false;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.MaximizeBox = false;
			this.PerformLayout();

			sendButton = new Button()
			{
				Location = new Point(267, 62),
				Name = "button3",
				Size = new Size(75, 23),
				TabIndex = 0,
				Text = "送信",
				UseVisualStyleBackColor = true,
			};
			sendButton.Click += new EventHandler(SendButton_Click);
			this.AcceptButton = sendButton;

			inputTextBox = new TextBox()
			{
				Location = new Point(12, 33),
				Name = "inputTextBox",
				Text = "",
				PlaceholderText = "ここにサーバーに送りたいメッセージを記入",
				Size = new Size(330, 23),
				TabIndex = 1,
				WordWrap = false
			};
			inputTextBox.KeyPress += new KeyPressEventHandler(InputTextBox_KeyPress);
			inputTextBox.KeyDown += new KeyEventHandler(InputTextBox_KeyDown);

			label = new Label
			{
				Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
				AutoSize = true,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Location = new Point(12, 9),
				Name = "label1",
				Text = "",
				Size = new Size(63, 25),
				TabIndex = 0,
			};

			this.Controls.Add(sendButton);
			this.Controls.Add(inputTextBox);
		}

		private void InputTextBox_KeyPress(object? sender, KeyPressEventArgs e)
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

		private void InputTextBox_KeyDown(object? sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					string? str = inputTextBox.Text;
					if (!string.IsNullOrEmpty(str))
					{
						SendCommand(str);
					}
					break;
			}
		}

		private void SendButton_Click(object? sender, EventArgs e)
		{
			string? str = inputTextBox.Text;
			SendCommand(str);
		}

		private void SendCommand(string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				ServerControlPanelForm.cmdInputTextBox.Text = "say " + str;
				ServerControlPanelForm.serverSendTextVaild = true;
				this.Close();
			}
		}
	}
}