﻿using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyServers
{
	partial class ServerControlPanelForm : Form
	{
		private static OpenFileDialog ofd = new OpenFileDialog();

		private static TextBox cmdLogTextBox = new TextBox();
		public static TextBox cmdInputTextBox = new TextBox();

		private static Button serverStartButton = new Button();
		private static Button serverSendButton = new Button();
		private static Button serverStopButton = new Button();
		private static Button serverAdvStopButton = new Button();

		private static Button shortcutButton1 = new Button();
		private static Button shortcutButton2 = new Button();
		private static Button shortcutButton3 = new Button();
		private static Button shortcutButton4 = new Button();
		private static Button shortcutButton5 = new Button();
		private static Button shortcutButton6 = new Button();
		private static Button shortcutButton7 = new Button();
		private static Button shortcutButton8 = new Button();

		private static Process proc = new Process();
		private static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

		public static bool serverSendTextVaild = false;

		public static bool fastStart = false;
		public static string fastStart_Path = "";

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
				//PlaceholderText = "console",
				WordWrap = true,
				Multiline = true,
				ScrollBars = ScrollBars.Vertical,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Yu Gothic UI", 10.75F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Size = new Size(495, 320),
				Location = new Point(373, 45),
				ReadOnly = true,
				BackColor = Color.White,
				TabIndex = 1,
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
				TabIndex = 2,
				TabStop = true
			};
			cmdInputTextBox.KeyPress += new KeyPressEventHandler(CmdInputTextBox_KeyPress);
			cmdInputTextBox.KeyDown += new KeyEventHandler(CmdInputTextBox_KeyDown);

			serverStartButton = new Button()
			{
				Location = new Point(633, 12),
				Name = "ServerSendButton",
				Size = new Size(100, 31),
				TabIndex = 0,
				Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 128),
				Text = "サーバー起動",
				BackColor = Color.Green,
				Enabled = true,
				UseVisualStyleBackColor = true
			};
			serverStartButton.Click += new EventHandler(ServerStartButton_Click);

			serverSendButton = new Button()
			{
				Location = new Point(793, 400),
				Name = "ServerSendButton",
				Size = new Size(75, 23),
				TabIndex = 3,
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
				TabIndex = 4,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "メッセージ送信",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			shortcutButton1.Click += new EventHandler(ShortcutButton1_Click);

			shortcutButton2 = new Button()
			{
				Location = new Point(471, 371),
				Name = "W/TShortCutButton",
				Size = new Size(66, 23),
				TabIndex = 5,
				Font = new Font("Yu Gothic UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "時間/天気",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			shortcutButton2.Click += new EventHandler(ShortcutButton2_Click);

			shortcutButton3 = new Button()
			{
				Location = new Point(542, 371),
				Name = "OPShortCutButton",
				Size = new Size(45, 23),
				TabIndex = 6,
				Font = new Font("Yu Gothic UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "OP",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			shortcutButton3.Click += new EventHandler(ShortcutButton3_Click);

			shortcutButton4 = new Button()
			{
				Location = new Point(593, 371),
				Name = "KillShortCutButton",
				Size = new Size(44, 23),
				TabIndex = 7,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "Kill",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			shortcutButton4.Click += new EventHandler(ShortcutButton4_Click);

			shortcutButton5 = new Button()
			{
				Location = new Point(643, 371),
				Name = "TeleportShortCutButton",
				Size = new Size(44, 23),
				TabIndex = 8,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "TP",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			shortcutButton5.Click += new EventHandler(ShortcutButton5_Click);

			shortcutButton6 = new Button()
			{
				Location = new Point(693, 371),
				Name = "GamemodeShortCutButton",
				Size = new Size(44, 23),
				TabIndex = 9,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "GM",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			shortcutButton6.Click += new EventHandler(ShortcutButton6_Click);

			shortcutButton7 = new Button()
			{
				Location = new Point(743, 371),
				Name = "BANorKickShortCutButton",
				Size = new Size(44, 23),
				TabIndex = 10,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "B/K",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			shortcutButton7.Click += new EventHandler(ShortcutButton7_Click);

			shortcutButton8 = new Button()
			{
				Location = new Point(793, 371),
				Name = "ServerReloadShortCutButton",
				Size = new Size(75, 23),
				TabIndex = 13,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "リロード",
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			shortcutButton8.Click += new EventHandler(ShortcutButton8_Click);

			serverAdvStopButton = new Button()
			{
				Location = new Point(798, 12),
				Name = "BANandKickShortCutButton",
				Size = new Size(70, 31),
				TabIndex = 12,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "強制停止",
				BackColor = Color.Red,
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			serverAdvStopButton.Click += new EventHandler(ServerAdvStopButton_Click);

			serverStopButton = new Button()
			{
				Location = new Point(739, 12),
				Name = "SayShortCutButton",
				Size = new Size(53, 31),
				TabIndex = 11,
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "停止",
				BackColor = Color.Red,
				Enabled = false,
				UseVisualStyleBackColor = true
			};
			serverStopButton.Click += new EventHandler(ServerStopButton_Click);

			this.Controls.Add(cmdLogTextBox);
			this.Controls.Add(cmdInputTextBox);
			this.Controls.Add(serverStartButton);
			this.Controls.Add(serverSendButton);
			this.Controls.Add(shortcutButton1);
			this.Controls.Add(shortcutButton2);
			this.Controls.Add(shortcutButton3);
			this.Controls.Add(shortcutButton4);
			this.Controls.Add(shortcutButton5);
			this.Controls.Add(shortcutButton6);
			this.Controls.Add(shortcutButton7);
			this.Controls.Add(shortcutButton8);
			this.Controls.Add(serverStopButton);
			this.Controls.Add(serverAdvStopButton);
		}

		public static bool sDoneSwitch = false;
		public static bool sCloseSwitch = false;

		private async void ServerControlPanelForm_Shown(object? sender, EventArgs e)
		{
			if (fastStart && !string.IsNullOrEmpty(fastStart_Path))
			{
				serverStartButton.Enabled = false;
				await MCServerStartProcessAsync(fastStart_Path);
			}
		}

		private async void ServerStartButton_Click(object? sender, EventArgs e)
		{
			serverStartButton.Enabled = false;
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
				await MCServerStartProcessAsync(ofd.FileName);
			}
			else
			{
				serverStartButton.Enabled = true;
				ofd.Dispose();
			}
		}

		private async Task MCServerStartProcessAsync(string path)
		{
			try
			{
				cmdLogTextBox.Text = "";
				sDoneSwitch = false;
				sCloseSwitch = false;
				if (MainForm.mcEULA)
				{
					using (StreamWriter writer = new StreamWriter(@$"{Path.GetDirectoryName(path)}\eula.txt", false))
					{
						await writer.WriteLineAsync(@"#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://aka.ms/MinecraftEULA).");
						await writer.WriteLineAsync("eula=true");
					}
				}
				await ExecuteCommandAsync(path, 4, 4);
			}
			catch (Exception ex)
			{
				cmdLogTextBox.Text = "";
				sDoneSwitch = false;
				sCloseSwitch = false;
				cmdLogTextBox.Text += "[" + DateTime.Now.ToString(@"HH:mm:ss") + " EasyServer System Error]: " + ex.Message + "\r\n";
			}
		}

		private async void ServerAdvStopButton_Click(object? sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("本当にサーバーを強制停止させますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (result == DialogResult.Yes)
			{
				await ServerAdvStopAsync();
			}
		}

		private async Task ServerAdvStopAsync()
		{
			try
			{
				if (proc != null && !proc.HasExited)
				{
					proc.Kill();
					await proc.WaitForExitAsync();
					proc.Close();

					cmdLogTextBox.Text += $"[{DateTime.Now.ToString(@"HH:mm:ss")} {Program.app_name} System]: サーバーの強制終了が実行されました。\r\n";
				}
			}
			catch (Exception ex)
			{
				cmdLogTextBox.Text += $"[{DateTime.Now.ToString(@"HH:mm:ss")} {Program.app_name} System Error]: 強制終了の実行中にエラーが発生しました。\r\n{ex.Message}\r\n";
			}
			finally
			{
				sDoneSwitch = false;
				sCloseSwitch = false;
			}
		}

		private void ServerStopButton_Click(object? sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("サーバーを停止させますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

		private void ShortcutButton2_Click(object? sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void ShortcutButton3_Click(object? sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void ShortcutButton4_Click(object? sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void ShortcutButton5_Click(object? sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void ShortcutButton6_Click(object? sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void ShortcutButton7_Click(object? sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void ShortcutButton8_Click(object? sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("サーバーをリロードさせますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				cmdInputTextBox.Text = "minecraft:reload";
				serverSendTextVaild = true;
			}
		}

		private async void ServerControlPanelForm_FormClosing(object? sender, FormClosingEventArgs e)
		{
			await FromClosing_Task();
		}

		private async Task FromClosing_Task()
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
						await proc.WaitForExitAsync();
						proc.Close();
					}
				}
			}
			finally
			{
				cmdLogTextBox.Text = "";
				sDoneSwitch = false;
				sCloseSwitch = false;
			}
		}

		private void ServerControlPanelForm_FormClosed(object? sender, FormClosedEventArgs e)
		{
			MainForm mfm = new MainForm();
			mfm.Show();
		}

		private void Timer_Tick(object? sender, EventArgs e)
		{
			if (Regex.IsMatch(cmdLogTextBox.Text, @"^\[[0-9]+\:[0-9]+\:[0-9]+ INFO\]\: Done \([0-9]+\.[0-9]{1,3}s\)\! For help, type ""help""", RegexOptions.Multiline) && !sDoneSwitch)
			{
				sDoneSwitch = true;
				serverSendButton.Enabled = true;
				serverStopButton.Enabled = true;
				shortcutButton1.Enabled = true;
				shortcutButton2.Enabled = true;
				shortcutButton3.Enabled = true;
				shortcutButton4.Enabled = true;
				shortcutButton5.Enabled = true;
				shortcutButton6.Enabled = true;
				shortcutButton7.Enabled = true;
				shortcutButton8.Enabled = true;
			}
			else if (Regex.IsMatch(cmdLogTextBox.Text, @"^\[[0-9]+\:[0-9]+\:[0-9]+ INFO\]\: Closing Server", RegexOptions.Multiline) && !sCloseSwitch)
			{
				sCloseSwitch = true;
				serverSendButton.Enabled = false;
				serverStopButton.Enabled = false;
				serverAdvStopButton.Enabled = false;
				shortcutButton1.Enabled = false;
				shortcutButton2.Enabled = false;
				shortcutButton3.Enabled = false;
				shortcutButton4.Enabled = false;
				shortcutButton5.Enabled = false;
				shortcutButton6.Enabled = false;
				shortcutButton7.Enabled = false;
				shortcutButton8.Enabled = false;
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
					if (!string.IsNullOrEmpty(cmdInputTextBox.Text) && sDoneSwitch)
						serverSendTextVaild = true;
					break;
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
						Arguments = @$"/c cd {Path.GetDirectoryName(jarFilePath)} & java -Xms{xms}G -Xmx{xmx}G -jar ""{Path.GetFileName(jarFilePath)}"" nogui",
						Verb = "RunAs",
						RedirectStandardOutput = true,
						RedirectStandardInput = true,
						StandardOutputEncoding = Encoding.GetEncoding("Shift_JIS"),
						StandardInputEncoding = Encoding.GetEncoding("Shift_JIS")
					};
					proc.Start();
					serverAdvStopButton.Enabled = true;

					await Task.Run(async () =>
					{
						await Task.WhenAll(OutputCmdLogAsync(), ServerSendAsync());
						if (proc.HasExited)
						{
							if (!sCloseSwitch)
							{
								sCloseSwitch = true;
							}
							serverStartButton.Enabled = true;
							serverSendButton.Enabled = false;
							serverStopButton.Enabled = false;
							serverAdvStopButton.Enabled = false;
							shortcutButton1.Enabled = false;
							shortcutButton2.Enabled = false;
							shortcutButton3.Enabled = false;
							shortcutButton4.Enabled = false;
							shortcutButton5.Enabled = false;
							shortcutButton6.Enabled = false;
							shortcutButton7.Enabled = false;
							shortcutButton8.Enabled = false;
						}
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
				cmdLogTextBox.Text += $"[{DateTime.Now.ToString(@"HH:mm:ss")} {Program.app_name} Output Error]: {ex.Message}\r\n";
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
								try
								{
									await sinput.WriteLineAsync(command);
								}
								finally
								{
									serverSendTextVaild = false;
									command = "";
									cmdInputTextBox.Text = "";
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					cmdLogTextBox.Text += $"[{DateTime.Now.ToString(@"HH:mm:ss")} {Program.app_name} Input Error]: {ex.Message}\r\n";
					if (serverSendTextVaild)
						serverSendTextVaild = false;
				}
			});
		}
	}

	partial class SayCommandShortForm : Form
	{
		private static Label label = new Label();
		private static Button sendButton = new Button();
		private static TextBox inputTextBox = new TextBox();

		private static bool saySendTextVaild = false;

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
				Name = "sendButton",
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
				Text = "メッセージ送信ショートカット",
				Size = new Size(63, 25),
				TabIndex = 0,
			};

			this.Controls.Add(sendButton);
			this.Controls.Add(inputTextBox);
			this.Controls.Add(label);
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
			if (!saySendTextVaild)
			{
				string? str = inputTextBox.Text;
				SendCommand(str);
			}
		}

		private async void SendCommand(string str)
		{
			if (!string.IsNullOrEmpty(str) && ServerControlPanelForm.sDoneSwitch)
			{
				ServerControlPanelForm.cmdInputTextBox.Text = "say " + str;
				ServerControlPanelForm.serverSendTextVaild = true;
				inputTextBox.Text = "";
				saySendTextVaild = true;
				if (saySendTextVaild)
				{
					sendButton.Enabled = false;
					await Task.Run(SendTimer);
				}
			}
		}

		private async Task SendTimer()
		{
			if (saySendTextVaild)
			{
				try
				{
					for (int i = 4; i > 0; i--)
					{
						if (i == 1)
						{
							sendButton.Enabled = true;
							sendButton.Text = "送信";
							break;
						}
						sendButton.Enabled = false;
						sendButton.Text = $"送信({i - 1})";
						await Task.Delay(1000);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"エラーが発生しました:\r\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}