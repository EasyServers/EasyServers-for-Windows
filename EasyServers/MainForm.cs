using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.WindowsAPICodePack.Taskbar;

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
		private static TextBox eulaTextBox = new TextBox();

		private static Label versionLabel = new Label();
		private static Label copyrightLabel = new Label();

		private static Panel serverCreateScreen_Software = new Panel();
		private static Panel serverCreateScreen_EULA = new Panel();

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

			serverCreateScreen_EULA = new Panel()
			{
				Name = "ServerCreateScreen_EULA",
				Dock = DockStyle.Fill,
				AutoSize = true,
				Location = new Point(0, 0),
				BorderStyle = BorderStyle.None,
				Enabled = false,
				Visible = false,
				TabIndex = 9
			};

			eulaTextBox = new TextBox()
			{
				Name = "eulaTextBox",
				Text = Properties.Resources.MinecraftEULA.ToString(),
				WordWrap = true,
				Multiline = true,
				ScrollBars = ScrollBars.Both,
				BorderStyle = BorderStyle.FixedSingle,
				Font = new Font("Yu Gothic UI", 10.75F, FontStyle.Regular, GraphicsUnit.Point, 128),
				Size = new Size(856, 337),
				Location = new Point(12, 59),
				ReadOnly = true,
				BackColor = Color.White,
				TabIndex = 1,
				TabStop = false
			};
			serverCreateScreen_EULA.Controls.Add(eulaTextBox);

			this.Controls.Add(mainMenuPanel);
			this.Controls.Add(serverCreateScreen_Software);
			this.Controls.Add(serverCreateScreen_EULA);
		}

		private void ServerCreateButton_Click(object? sender, EventArgs e)
		{
			mainMenuPanel.Enabled = false;
			mainMenuPanel.Visible = false;
			serverCreateScreen_Software.Visible = true;
			serverCreateScreen_Software.Enabled = true;
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

		private async void ServerSoftwearDownloadForm_Shown(object? sender, EventArgs e)
		{
			if (await Program.IsNet())
			{
				await ServerCreateProcessAsync();
				if (sucsessDownloadServerLap)
				{
					processLabel.Text = "完了";
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
						using (StreamWriter writer = new StreamWriter($"{Path.GetDirectoryName(installFolderPath)}\\run.bat", false, System.Text.Encoding.UTF8))
						{
							await writer.WriteLineAsync("@echo off");
							await writer.WriteLineAsync($"java -Xms4G -Xmx4G -jar \"{Path.GetDirectoryName(installFolderPath)}\\{fileName}\" nogui");
						}
						using (StreamWriter writer = new StreamWriter($"{Path.GetDirectoryName(installFolderPath)}\\run.sh", false, System.Text.Encoding.UTF8))
						{
							installFolderPath = installFolderPath.Replace(@"\", @"\\");
							await writer.WriteLineAsync("#!/bin/sh");
							await writer.WriteLineAsync($"java -Xms4G -Xmx4G -jar \"{installFolderPath}\\{fileName}\" nogui");
						}
						processLabel.Text = "後処理をしています...";
						sucsessDownloadServerLap = true;
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
				MessageBox.Show("ダウンロード中にエラーが発生しました！\r\n" + ex.Message);
				Application.Exit();
			}
		}

		private static string[] gamemode_str = ["survival", "creative", "adventure", "spectator"];
		private static string[] difficulty_str = ["peaceful", "easy", "normal", "hard"];
		private static string[] level_type_str = ["normal", "flat", "large_biome", "amplified", "single_biome_surface"];
		private static async Task ServerPropertiesWriteAsync(string? savePath)
		{
			string[] properties = ["enable-jmx-monitoring", "rcon.port", "level-seed", "gamemode", "enable-command-block", "enable-query", "generator-settings", "enforce-secure-profile", "level-name", "motd", "query.port", "pvp", "generate-structures", "max-chained-neighbor-updates", "difficulty", "network-compression-threshold", "max-tick-time", "require-resource-pack", "use-native-transport", "max-players", "online-mode", "enable-status", "allow-flight", "initial-disabled-packs", "broadcast-rcon-to-ops", "view-distance", "server-ip", "resource-pack-prompt", "allow-nether", "server-port", "enable-rcon", "sync-chunk-writes", "op-permission-level", "prevent-proxy-connections", "hide-online-players", "resource-pack", "entity-broadcast-range-percentage", "simulation-distance", "rcon.password", "player-idle-timeout", "debug", "force-gamemode", "rate-limit", "hardcore", "white-list", "broadcast-console-to-ops", "spawn-npcs", "spawn-animals", "log-ips", "function-permission-level", "initial-enabled-packs", "level-type", "text-filtering-config", "spawn-monsters", "enforce-whitelist", "spawn-protection", "resource-pack-sha1", "max-world-size"];
			bool enable_jmx_monitoring_prop = false;
			int rconPort_prop = 25575;
			string? level_seed_prop = "";
			string gamemode_prop = gamemode_str[0];
			bool enable_command_block_prop = false;
			bool enable_query_prop = false;
			string generator_settings_prop = "";
			bool enforce_secure_profile_prop = true;
			string level_name_prop = "world";
			string motd_prop = "A Minecraft Server";
			int query_port_prop = 25565;
			bool pvp_friendlyFire_prop = true;
			bool generate_structures_prop = true;
			int max_chained_neighbor_updates_prop = 1000000;
			string difficulty_prop = difficulty_str[1];
			int network_compression_threshold_prop = 256;
			long max_tick_time_prop = 60000;
			bool require_resource_pack_prop = false;
			bool use_native_transport_prop = true;
			long max_players_prop = 20;
			bool online_mode_prop = true;
			bool enable_status_prop = true;
			bool allow_flight_prop = true;
			string initial_disabled_packs_prop = "";
			bool broadcast_rcon_to_ops_prop = true;
			int view_distance_prop = 10;
			string server_ip_prop = "";
			string resource_pack_prompt_prop = "";
			bool allow_nether_prop = true;
			int server_port_prop = 25565;
			bool enable_rcon_prop = false;
			bool sync_chunk_writes_prop = false;
			int op_permission_level_prop = 4;
			bool prevent_proxy_connections_prop = false;
			bool hide_online_players_prop = false;
			string resource_pack_prop = "";
			int entity_broadcast_range_percentage = 100;
			int simulation_distance‌_prop = 10;
			string rcon_password_prop = "";
			int player_idle_timeout_prop = 0;
			bool force_gamemode_prop = false;
			int rate_limit_prop = 0;
			bool hardcore_prop = false;
			bool white_list_prop = false;
			const bool broadcast_console_to_ops_prop = true;
			bool spawn_npcs_prop = true;
			bool spawn_animals_prop = true;
			int function_permission_level_prop = 2;
			string initial_enabled_packs_prop = "vanilla";
			string level_type_prop = @"minecraft\:" + level_type_str[0];
			string text_filtering_config = "";
			bool spawn_monsters = true;
			bool enforce_whitelist_prop = false;
			int spawn_protection_prop = 16;
			string resource_pack_sha1_prop = "";
			int max_world_size_prop = 29999984;

			using (StreamWriter writer = new StreamWriter($"{savePath}\\" + @"server.properties", false, System.Text.Encoding.UTF8))
			{
				await writer.WriteLineAsync($"{properties[0]}={enable_jmx_monitoring_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[1]}={rconPort_prop}");
				await writer.WriteLineAsync($"{properties[2]}={(string.IsNullOrEmpty(level_seed_prop) ? "" : level_seed_prop)}");
				await writer.WriteLineAsync($"{properties[3]}={gamemode_prop}");
				await writer.WriteLineAsync($"{properties[4]}={enable_command_block_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[5]}={enable_query_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[6]}={"{"+generator_settings_prop+"}"}");
				await writer.WriteLineAsync($"{properties[7]}={enforce_secure_profile_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[8]}={level_name_prop}");
				await writer.WriteLineAsync($"{properties[9]}={motd_prop}");
				await writer.WriteLineAsync($"{properties[10]}={query_port_prop}");
				await writer.WriteLineAsync($"{properties[11]}={pvp_friendlyFire_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[12]}={generate_structures_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[13]}={max_chained_neighbor_updates_prop}");
				await writer.WriteLineAsync($"{properties[14]}={difficulty_prop}");
				await writer.WriteLineAsync($"{properties[15]}={network_compression_threshold_prop}");
				await writer.WriteLineAsync($"{properties[16]}={max_tick_time_prop}");
				await writer.WriteLineAsync($"{properties[17]}={require_resource_pack_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[18]}={use_native_transport_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[19]}={max_players_prop}");
				await writer.WriteLineAsync($"{properties[20]}={online_mode_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[21]}={enable_status_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[22]}={allow_flight_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[23]}={initial_disabled_packs_prop}");
				await writer.WriteLineAsync($"{properties[24]}={broadcast_rcon_to_ops_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[25]}={view_distance_prop}");
				await writer.WriteLineAsync($"{properties[26]}={server_ip_prop}");
				await writer.WriteLineAsync($"{properties[27]}={resource_pack_prompt_prop}");
				await writer.WriteLineAsync($"{properties[28]}={allow_nether_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[29]}={server_port_prop}");
				await writer.WriteLineAsync($"{properties[30]}={enable_rcon_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[31]}={sync_chunk_writes_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[32]}={op_permission_level_prop}");
				await writer.WriteLineAsync($"{properties[33]}={prevent_proxy_connections_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[34]}={hide_online_players_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[35]}={resource_pack_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[36]}={entity_broadcast_range_percentage}");
				await writer.WriteLineAsync($"{properties[37]}={simulation_distance_prop}");
				await writer.WriteLineAsync($"{properties[38]}={rcon_password_prop}");
				await writer.WriteLineAsync($"{properties[39]}={player_idle_timeout_prop}");
				await writer.WriteLineAsync($"{properties[40]}={force_gamemode_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[41]}={rate_limit_prop}");
				await writer.WriteLineAsync($"{properties[42]}={hardcore_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[43]}={false.ToString().ToLower()}");//debug
				await writer.WriteLineAsync($"{properties[44]}={white_list_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[45]}={broadcast_console_to_ops_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[46]}={spawn_npcs_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[47]}={spawn_animals_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[48]}={true.ToString().ToLower()}");//log-ips
				await writer.WriteLineAsync($"{properties[49]}={function_permission_level_prop}");
				await writer.WriteLineAsync($"{properties[50]}={initial_enabled_packs_prop}");
				await writer.WriteLineAsync($"{properties[51]}={level_type_prop}");
				await writer.WriteLineAsync($"{properties[52]}={text_filtering_config}");
				await writer.WriteLineAsync($"{properties[53]}={spawn_monsters.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[54]}={enforce_whitelist_prop.ToString().ToLower()}");
				await writer.WriteLineAsync($"{properties[55]}={spawn_protection_prop}");
				await writer.WriteLineAsync($"{properties[56]}={resource_pack_sha1_prop}");
				await writer.WriteLineAsync($"{properties[57]}={max_world_size_prop}");
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