using System.ComponentModel;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Diagnostics;
using System.Xml;

namespace EasyServers
{
	partial class MainForm : Form
	{
		
		private static PictureBox titlePicture = new PictureBox();

		private static Button serverCreateButton = new Button();
		private static Button serverOperationButton = new Button();
		private static Button portOpenFormButton = new Button();
		private static Button exitButton = new Button();
		private static Button nextButton1 = new Button();
		private static Button undoButton1 = new Button();

		private static RadioButton eulaYesButton = new RadioButton();

		private static TextBox serverCreateNameTextBox = new TextBox();

		private static Label versionLabel = new Label();
		private static Label copyrightLabel = new Label();
		private static Label eulaLabel1 = new Label();
		private static Label eulaLabel2 = new Label();
		private static Label serverNameLabel = new Label();
		private static LinkLabel jumpEULALabel = new LinkLabel();

		private static Panel mainMenuPanel = new Panel();
		private static Panel serverCreateScreen_Software = new Panel();
		private static Panel serverCreateScreen_VERFIY = new Panel();
		private static Panel serverCreatePanel_EULA = new Panel();
		private static Panel serverOperationScreen = new Panel();

		public static bool mcEULA = false;

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
			serverOperationButton.Click += new EventHandler(ServerOperationButton_Click);

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
				BackColor = Color.Transparent,
				TabIndex = 6
			};

			copyrightLabel = new Label()
			{
				Name = "Version",
				Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = $"(C) 2024 Shotadft. All rights reserved. ({Program.app_name})\r\n(C) 2011-2024 Mojang AB. (Minecraft)",
				AutoSize = true,
				Location = new Point(512, 413),
				BackColor = Color.Transparent,
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

			serverCreateScreen_VERFIY = new Panel()
			{
				Name = "serverCreateScreen_VERFIY",
				Dock = DockStyle.Fill,
				AutoSize = true,
				Location = new Point(0, 0),
				BorderStyle = BorderStyle.None,
				Enabled = false,
				Visible = false,
				TabIndex = 9
			};

			serverCreatePanel_EULA = new Panel()
			{
				Name = "serverCreatePanel_EULA",
				Location = new Point(12, 12),
				Size = new Size(856, 153),
				BackColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				TabIndex = 10
			};

			serverCreateNameTextBox = new TextBox()
			{
				Name = "serverCreateNameTextBox",
				Text = "無題のサーバー",
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Size = new Size(271, 29),
				Location = new Point(12, 201),
				BackColor = Color.White,
				TabIndex = 11,
				TabStop = false
			};

			eulaLabel1 = new Label()
			{
				Name = "label3",
				Font = new Font("Yu Gothic UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 128),
				Text = "Minecraft EULA",
				AutoSize = true,
				Location = new Point(3, 10),
				TabIndex = 12
			};

			eulaLabel2 = new Label()
			{
				Name = "label4",
				Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "Minecraftのサーバーを使用するにはエンドユーザーライセンス(以下EULA)への同意が必須です。\r\n下のリンクからEULAをよく読み、同意する場合は下のボタンを押してサーバーの作成を開始させてください。",
				AutoSize = true,
				Location = new Point(3, 57),
				TabIndex = 13
			};

			serverNameLabel = new Label()
			{
				Name = "label6",
				Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = "サーバー名",
				AutoSize = true,
				Location = new Point(12, 177),
				TabIndex = 19
			};

			jumpEULALabel = new LinkLabel()
			{
				Name = "label5",
				Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Text = @"https://www.minecraft.net/eula",
				AutoSize = true,
				TabStop = false,
				Location = new Point(3, 116),
				TabIndex = 14
			};
			jumpEULALabel.Click += new EventHandler(JumpEULALabel_Click);

			eulaYesButton = new RadioButton()
			{
				AutoSize = true,
				Location = new Point(12, 236),
				BackColor = Color.Transparent,
				Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Name = "eulaYesButton",
				TabIndex = 15,
				TabStop = true,
				Text = "EULAに同意",
				UseVisualStyleBackColor = true
			};
			eulaYesButton.Click += new EventHandler(EulaYesButton_Click);

			nextButton1 = new Button()
			{
				Name = "nextButton1",
				Text = "作成",
				Size = new Size(75, 23),
				Location = new Point(793, 430),
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				UseVisualStyleBackColor = true,
				Enabled = false,
				TabStop = false,
				TabIndex = 16
			};
			nextButton1.Click += new EventHandler(NextButton1_Click);

			undoButton1 = new Button()
			{
				Name = "undoButton1",
				Text = "戻る",
				Size = new Size(75, 23),
				Location = new Point(712, 430),
				Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128),
				UseVisualStyleBackColor = true,
				TabStop = false,
				TabIndex = 17
			};
			undoButton1.Click += new EventHandler(UndoButton1_Click);

			serverCreatePanel_EULA.Controls.Add(eulaLabel1);
			serverCreatePanel_EULA.Controls.Add(eulaLabel2);
			serverCreatePanel_EULA.Controls.Add(jumpEULALabel);
			serverCreateScreen_VERFIY.Controls.Add(serverCreatePanel_EULA);
			serverCreateScreen_VERFIY.Controls.Add(serverCreateNameTextBox);
			serverCreateScreen_VERFIY.Controls.Add(serverNameLabel);
			serverCreateScreen_VERFIY.Controls.Add(eulaYesButton);
			serverCreateScreen_VERFIY.Controls.Add(nextButton1);
			serverCreateScreen_VERFIY.Controls.Add(undoButton1);

			serverOperationScreen = new Panel()
			{
				Name = "serverOperationScreen",
				Dock = DockStyle.Fill,
				AutoSize = true,
				Location = new Point(0, 0),
				BorderStyle = BorderStyle.None,
				Enabled = false,
				Visible = false,
				TabIndex = 18
			};

			this.Controls.Add(mainMenuPanel);
			this.Controls.Add(serverOperationScreen);
			this.Controls.Add(serverCreateScreen_Software);
			this.Controls.Add(serverCreateScreen_VERFIY);
		}

		private void ServerOperationButton_Click(object? sender, EventArgs e)
		{
			ServerControlPanelForm fm = new ServerControlPanelForm();
			fm.Show();
			this.Hide();
			/*TODO:サーバー一覧画面
			mainMenuPanel.Enabled = false;
			mainMenuPanel.Visible = false;
			serverOperationScreen.Visible = true;
			serverOperationScreen.Enabled = true;
			*/
		}

		private void NextButton1_Click(object? sender, EventArgs e)
		{
			mcEULA = true;
			mainMenuPanel.Enabled = true;
			mainMenuPanel.Visible = true;
			serverCreateScreen_VERFIY.Visible = false;
			serverCreateScreen_VERFIY.Enabled = false;

			ServerSoftwearDownloadForm fm = new ServerSoftwearDownloadForm();
			fm.Show();
			this.Hide();
		}

		private void UndoButton1_Click(object? sender, EventArgs e)
		{
			mainMenuPanel.Enabled = true;
			mainMenuPanel.Visible = true;
			serverCreateScreen_VERFIY.Visible = false;
			serverCreateScreen_VERFIY.Enabled = false;
		}

		private void EulaNoButton_Click(object? sender, EventArgs e)
		{
			nextButton1.Enabled = false;
		}

		private void EulaYesButton_Click(object? sender, EventArgs e)
		{
			nextButton1.Enabled = true;
		}

		private void JumpEULALabel_Click(object? sender, EventArgs e)
		{
			using (Process proc = new Process())
			{
				Uri uri = new Uri(@"https://www.minecraft.net/eula");
				proc.StartInfo.FileName = uri.ToString();
				proc.StartInfo.UseShellExecute = true;
				proc.Start();
			}
		}

		private void ServerCreateButton_Click(object? sender, EventArgs e)
		{
			mainMenuPanel.Enabled = false;
			mainMenuPanel.Visible = false;
			serverCreateScreen_VERFIY.Visible = true;
			serverCreateScreen_VERFIY.Enabled = true;
		}

		private void ExitButton_Click(object? sender, EventArgs e)
		{
			Application.Exit();
		}
	}

	partial class ServerSoftwearDownloadForm : Form
	{
		public static ProgressBar progressBar = new ProgressBar();
		public static Label processLabel = new Label();
		public static Button cancelButton = new Button();

		private static DriveInfo cDrive = new DriveInfo("C");

		public ServerSoftwearDownloadForm()
		{
			this.SuspendLayout();
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.Name = "ServerSoftwearDownloadForm";
			this.Text = Program.app_name;
			this.Size = new Size(370, 130);
			this.ClientSize = new Size(354, 91);
			this.ResumeLayout(false);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.ShowIcon = false;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.MaximizeBox = false;
			this.PerformLayout();

			this.Shown += new EventHandler(ServerSoftwearDownloadForm_Shown);

			processLabel = new Label
			{
				Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
				AutoSize = true,
				Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Location = new Point(12, 9),
				Name = "ProcessLabel",
				Text = "",
				BackColor = Color.Transparent,
				Size = new Size(63, 25),
				TabIndex = 0,
			};

			progressBar = new ProgressBar
			{
				Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
				Location = new Point(12, 37),
				Name = "progressBar",
				Size = new Size(330, 23),
				Maximum = 100,
				Minimum = 0,
				Style = ProgressBarStyle.Blocks,
				Value = 0,
				TabIndex = 1
			};

			cancelButton = new Button
			{
				Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
				Location = new Point(267, 66),
				Name = "Cancel",
				Size = new Size(75, 23),
				TabIndex = 2,
				Enabled = false,
				Text = "キャンセル",
				UseVisualStyleBackColor = true
			};
			this.CancelButton = cancelButton;

			this.Controls.Add(processLabel);
			this.Controls.Add(progressBar);
			this.Controls.Add(cancelButton);
		}

		private static double freeDrive = cDrive.TotalFreeSpace;
		private static double tortalDrive = cDrive.TotalSize;
		private static bool sucsessDownloadServerLap = false;

		private static string _fastStart_Path = "";
		private async void ServerSoftwearDownloadForm_Shown(object? sender, EventArgs e)
		{
			if (await Program.IsNet())
			{
				await ServerCreateProcessAsync();
				if (sucsessDownloadServerLap)
				{
					processLabel.Text = "完了";

					ServerControlPanelForm.fastStart_Path = _fastStart_Path ?? string.Empty;
					ServerControlPanelForm.fastStart = true;

					ServerControlPanelForm controlPanelForm = new ServerControlPanelForm();
					controlPanelForm.Show();
					this.Close();
				}
			}
			else
			{
				MessageBox.Show("インターネットに接続されて居ません！\r\nインターネットに接続されていることを確認してから、もう一度やり直してください。", "インターネット エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		private static async Task ServerCreateProcessAsync()
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
					TaskbarManager.Instance.SetProgressValue(0, 100);
					var progress = new Progress<int>(value =>
					{
						if (value > 0)
						{
							progressBar.Value = value;
							TaskbarManager.Instance.SetProgressValue(value, 100);
						}
					});

					const string url = MinecraftServerDownloads.papermc_serverSoftweare_mc1201_build193_url;

					processLabel.Text = "空き容量を計算しています...";

					double server_softwear_bytes = await GetFileSizeAsync(url);

					string freeDriveStr = Program.ToReadableSize(freeDrive);
					string serverSoftwearByteStr = Program.ToReadableSize(server_softwear_bytes);

					if (freeDrive > server_softwear_bytes)
					{
						processLabel.Text = "ファイルの準備をしています...";
						progressBar.Style = ProgressBarStyle.Marquee;
						string installFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + @"\" + Program.app_name + @"\";
						if (!Directory.Exists(installFolderPath))
						{
							Directory.CreateDirectory(installFolderPath);
						}

						Random rand = new Random();
						string tmpFileName = "dl_" + rand.Next(65535).ToString() + @".dltmp";

						progressBar.Value = 0;
						TaskbarManager.Instance.SetProgressValue(0, 100);

						cancelButton.Enabled = true;

						progressBar.Style = ProgressBarStyle.Blocks;
						processLabel.Text = "ファイルをダウンロードしています...";

						byte[] data = await DownloadDataAsync(url, progress);
						cancelButton.Enabled = false;

						processLabel.Text = "ファイルに書き込んでいます...";
						progressBar.Value = 0;
						TaskbarManager.Instance.SetProgressValue(0, 100);

						string fileName = GetFileNameToUrl(url);
						await WriteDataToFileAsync(data, installFolderPath + tmpFileName);
						File.Move(installFolderPath + tmpFileName, installFolderPath + fileName);
						processLabel.Text = "server.propertiesを書き込んでいます...";
						await ServerPropertiesWriteAsync($"{Path.GetDirectoryName(installFolderPath)}");
						processLabel.Text = "起動用ファイルを書き込んでいます...";
						using (StreamWriter writer = new StreamWriter($"{Path.GetDirectoryName(installFolderPath)}\\run.bat", false))
						{
							await writer.WriteLineAsync("@echo off");
							await writer.WriteLineAsync($"java -Xms4G -Xmx4G -jar \"{Path.GetDirectoryName(installFolderPath)}\\{fileName}\" nogui");
						}
						using (StreamWriter writer = new StreamWriter($"{Path.GetDirectoryName(installFolderPath)}\\run.sh", false))
						{
							string str = $"java -Xms4G -Xmx4G -jar \"{installFolderPath}{fileName}\" nogui";
							str = str.Replace(@"\", @"\\");
							await writer.WriteLineAsync("#!/bin/sh");
							await writer.WriteLineAsync(str);
						}
						processLabel.Text = "後処理をしています...";
						sucsessDownloadServerLap = true;
						_fastStart_Path = installFolderPath + fileName;
						TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
					}
					else
					{
						MessageBox.Show("サーバーソフトウェアをインストールするためのドライブ容量が足りません！\r\n容量を開けてから再度お試しください。\r\n\r\n空き容量：" + freeDriveStr + "\r\n必要な容量：" + serverSoftwearByteStr, Program.app_name + " - エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			catch (Exception ex)
			{
				TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
				MessageBox.Show("ダウンロード中にエラーが発生しました！\r\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		public readonly static string[] gamemode_str = ["survival", "creative", "adventure", "spectator"];
		public readonly static string[] difficulty_str = ["peaceful", "easy", "normal", "hard"];
		public readonly static string[] level_type_str = ["normal", "flat", "large_biome", "amplified", "single_biome_surface"];
		private readonly static string[] properties_str = ["enable-jmx-monitoring", "rcon.port", "level-seed", "gamemode", "enable-command-block", "enable-query", "generator-settings", "enforce-secure-profile", "level-name", "motd", "query.port", "pvp", "generate-structures", "max-chained-neighbor-updates", "difficulty", "network-compression-threshold", "max-tick-time", "require-resource-pack", "use-native-transport", "max-players", "online-mode", "enable-status", "allow-flight", "initial-disabled-packs", "broadcast-rcon-to-ops", "view-distance", "server-ip", "resource-pack-prompt", "allow-nether", "server-port", "enable-rcon", "sync-chunk-writes", "op-permission-level", "prevent-proxy-connections", "hide-online-players", "resource-pack", "entity-broadcast-range-percentage", "simulation-distance", "rcon.password", "player-idle-timeout", "debug", "force-gamemode", "rate-limit", "hardcore", "white-list", "broadcast-console-to-ops", "spawn-npcs", "spawn-animals", "log-ips", "function-permission-level", "initial-enabled-packs", "level-type", "text-filtering-config", "spawn-monsters", "enforce-whitelist", "spawn-protection", "resource-pack-sha1", "max-world-size"];

		public static bool enable_jmx_monitoring_prop = false;
		public static int rconPort_prop = 25575;
		public static int? level_seed_prop = null;
		public static string gamemode_prop = gamemode_str[0];
		public static bool enable_command_block_prop = false;
		public static bool enable_query_prop = false;
		public static string generator_settings_prop = "";
		public static bool enforce_secure_profile_prop = true;
		public static string level_name_prop = "world";
		public static string motd_prop = "A Minecraft Server";
		public static int query_port_prop = 25565;
		public static bool pvp_friendlyFire_prop = true;
		public static bool generate_structures_prop = true;
		public static int max_chained_neighbor_updates_prop = 1000000;
		public static string difficulty_prop = difficulty_str[1];
		public static int network_compression_threshold_prop = 256;
		public static long max_tick_time_prop = 60000;
		public static bool require_resource_pack_prop = false;
		public static bool use_native_transport_prop = true;
		public static long max_players_prop = 20;
		public static bool online_mode_prop = true;
		public static bool enable_status_prop = true;
		public static bool allow_flight_prop = true;
		public static string initial_disabled_packs_prop = "";
		public static bool broadcast_rcon_to_ops_prop = true;
		public static int view_distance_prop = 10;
		public static string server_ip_prop = "";
		public static string resource_pack_prompt_prop = "";
		public static bool allow_nether_prop = true;
		public static int server_port_prop = 25565;
		public static bool enable_rcon_prop = false;
		public static bool sync_chunk_writes_prop = false;
		public static int op_permission_level_prop = 4;
		public static bool prevent_proxy_connections_prop = false;
		public static bool hide_online_players_prop = false;
		public static string resource_pack_prop = "";
		public static int entity_broadcast_range_percentage = 100;
		public static int simulation_distance‌_prop = 10;
		public static string rcon_password_prop = "";
		public static int player_idle_timeout_prop = 0;
		public static bool force_gamemode_prop = false;
		public static int rate_limit_prop = 0;
		public static bool hardcore_prop = false;
		public static bool white_list_prop = false;
		public const bool broadcast_console_to_ops_prop = true;
		public static bool spawn_npcs_prop = true;
		public static bool spawn_animals_prop = true;
		public static int function_permission_level_prop = 2;
		public static string initial_enabled_packs_prop = "vanilla";
		public static string level_type_prop = @"minecraft\:" + level_type_str[0];
		public static string text_filtering_config = "";
		public static bool spawn_monsters = true;
		public static bool enforce_whitelist_prop = false;
		public static int spawn_protection_prop = 16;
		public static string resource_pack_sha1_prop = "";
		public static int max_world_size_prop = 29999984;
		private static async Task ServerPropertiesWriteAsync(string? savePath)
		{
			try
			{
				var propertiesData = new[]
				{
					new {Key = properties_str[0], Value = $"{enable_jmx_monitoring_prop.ToString().ToLower()}"},
					new {Key = properties_str[1], Value = $"{rconPort_prop}"},
					new {Key = properties_str[2], Value = $"{(level_seed_prop != null ? level_seed_prop.ToString() : string.Empty)}"},
					new {Key = properties_str[3], Value = $"{gamemode_prop.ToString().ToLower()}"},
					new {Key = properties_str[4], Value = $"{enable_command_block_prop.ToString().ToLower()}"},
					new {Key = properties_str[5], Value = $"{enable_query_prop.ToString().ToLower()}"},
					new {Key = properties_str[6], Value = @"{"+$"{generator_settings_prop}"+@"}"},
					new {Key = properties_str[7], Value = $"{enforce_secure_profile_prop.ToString().ToLower()}"},
					new {Key = properties_str[8], Value = $"{level_name_prop}"},
					new {Key = properties_str[9], Value = $"{motd_prop}"},
					new {Key = properties_str[10], Value = $"{query_port_prop}"},
					new {Key = properties_str[11], Value = $"{pvp_friendlyFire_prop.ToString().ToLower()}"},
					new {Key = properties_str[12], Value = $"{generate_structures_prop.ToString().ToLower()}"},
					new {Key = properties_str[13], Value = $"{max_chained_neighbor_updates_prop}"},
					new {Key = properties_str[14], Value = $"{difficulty_prop}"},
					new {Key = properties_str[15], Value = $"{network_compression_threshold_prop}"},
					new {Key = properties_str[16], Value = $"{max_tick_time_prop}"},
					new {Key = properties_str[17], Value = $"{require_resource_pack_prop.ToString().ToLower()}"},
					new {Key = properties_str[18], Value = $"{use_native_transport_prop.ToString().ToLower()}"},
					new {Key = properties_str[19], Value = $"{max_players_prop}"},
					new {Key = properties_str[20], Value = $"{online_mode_prop.ToString().ToLower()}"},
					new {Key = properties_str[21], Value = $"{enable_status_prop.ToString().ToLower()}"},
					new {Key = properties_str[22], Value = $"{allow_flight_prop.ToString().ToLower()}"},
					new {Key = properties_str[23], Value = $"{initial_disabled_packs_prop}"},
					new {Key = properties_str[24], Value = $"{broadcast_rcon_to_ops_prop.ToString().ToLower()}"},
					new {Key = properties_str[25], Value = $"{view_distance_prop}"},
					new {Key = properties_str[26], Value = $"{server_ip_prop}"},
					new {Key = properties_str[27], Value = $"{resource_pack_prompt_prop}"},
					new {Key = properties_str[28], Value = $"{allow_nether_prop.ToString().ToLower()}"},
					new {Key = properties_str[29], Value = $"{server_port_prop}"},
					new {Key = properties_str[30], Value = $"{enable_rcon_prop.ToString().ToLower()}"},
					new {Key = properties_str[31], Value = $"{sync_chunk_writes_prop.ToString().ToLower()}"},
					new {Key = properties_str[32], Value = $"{op_permission_level_prop}"},
					new {Key = properties_str[33], Value = $"{prevent_proxy_connections_prop.ToString().ToLower()}"},
					new {Key = properties_str[34], Value = $"{hide_online_players_prop.ToString().ToLower()}"},
					new {Key = properties_str[35], Value = $"{resource_pack_prop}"},
					new {Key = properties_str[36], Value = $"{entity_broadcast_range_percentage}"},
					new {Key = properties_str[37], Value = $"{simulation_distance‌_prop}"},
					new {Key = properties_str[38], Value = $"{rcon_password_prop}"},
					new {Key = properties_str[39], Value = $"{player_idle_timeout_prop}"},
					new {Key = properties_str[40], Value = $"{false.ToString().ToLower()}"},
					new {Key = properties_str[41], Value = $"{force_gamemode_prop.ToString().ToLower()}"},
					new {Key = properties_str[42], Value = $"{rate_limit_prop}"},
					new {Key = properties_str[43], Value = $"{hardcore_prop.ToString().ToLower()}"},
					new {Key = properties_str[44], Value = $"{white_list_prop.ToString().ToLower()}"},
					new {Key = properties_str[45], Value = $"{broadcast_console_to_ops_prop.ToString().ToLower()}"},
					new {Key = properties_str[46], Value = $"{spawn_npcs_prop.ToString().ToLower()}"},
					new {Key = properties_str[47], Value = $"{spawn_animals_prop.ToString().ToLower()}"},
					new {Key = properties_str[48], Value = $"{true.ToString().ToLower()}"},
					new {Key = properties_str[49], Value = $"{function_permission_level_prop}"},
					new {Key = properties_str[50], Value = $"{initial_enabled_packs_prop}"},
					new {Key = properties_str[51], Value = $"{level_type_prop}"},
					new {Key = properties_str[52], Value = $"{text_filtering_config.ToString().ToLower()}"},
					new {Key = properties_str[53], Value = $"{spawn_monsters.ToString().ToLower()}"},
					new {Key = properties_str[54], Value = $"{enforce_whitelist_prop.ToString().ToLower()}"},
					new {Key = properties_str[55], Value = $"{spawn_protection_prop}"},
					new {Key = properties_str[56], Value = $"{resource_pack_sha1_prop}"},
					new {Key = properties_str[57], Value = $"{max_world_size_prop}"}
				};

				using (StreamWriter writer = new StreamWriter(savePath + @"\" + @"server.properties", false))
				{
					await writer.WriteLineAsync("#Minecraft server properties");
					await writer.WriteLineAsync("#"+DateTime.UtcNow.ToString(@"ddd MMM dd HH:mm:ss UTC yyyy"));
					foreach (var pd in propertiesData)
					{
						await writer.WriteLineAsync($"{pd.Key}={pd.Value}");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private static async Task<byte[]> DownloadDataAsync(string uri, IProgress<int> progress)
		{
			Uri url = new Uri(uri);

			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(url.ToString(), HttpCompletionOption.ResponseHeadersRead))
			{
				response.EnsureSuccessStatusCode();

				using (Stream stream = await response.Content.ReadAsStreamAsync())
				using (MemoryStream memoryStream = new MemoryStream())
				{
					long totalBytes = response.Content.Headers.ContentLength ?? -1;
					long bytesRead = 0;
					byte[] buffer = new byte[8192];
					int bytesReadThisTime;

					while ((bytesReadThisTime = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
					{
						memoryStream.Write(buffer, 0, bytesReadThisTime);
						bytesRead += bytesReadThisTime;

						if (totalBytes > 0)
						{
							int percentage = (int)((bytesRead * 100) / totalBytes);
							progress.Report(percentage);
						}
					}

					return memoryStream.ToArray();
				}
			}
		}

		private static async Task WriteDataToFileAsync(byte[] data, string filePath)
		{
			using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				await stream.WriteAsync(data, 0, data.Length);
			}
		}

		private static async Task<double> GetFileSizeAsync(string url)
		{
			Uri uri = new Uri(url);

			using (HttpClient client = new HttpClient())
			{
				try
				{
					HttpResponseMessage response = await client.GetAsync(uri.ToString(), HttpCompletionOption.ResponseHeadersRead);

					if (response.IsSuccessStatusCode)
					{
						if (response.Content.Headers.ContentLength.HasValue)
						{
							long fileSizeInBytes = response.Content.Headers.ContentLength.Value;
							return (double)fileSizeInBytes;
						}
						else
						{
							Console.WriteLine("ファイルサイズが取得できませんでした。");
							return 1;
						}
					}
					else
					{
						Console.WriteLine($"HTTPエラー: {response.StatusCode} - {response.ReasonPhrase}");
						return 1;
					}
				}
				catch (HttpRequestException ex)
				{
					Console.WriteLine($"エラー: {ex.Message}");
					return 1;
				}
			}
		}

		private static string GetFileNameToUrl(string url)
		{
			Uri uri = new Uri(url);
			string filename = Path.GetFileName(uri.LocalPath);
			return filename;
		}
	}
}