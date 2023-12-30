using Microsoft.WindowsAPICodePack.Taskbar;
using System.Diagnostics;

namespace EasyServers
{
	internal partial class Program
	{
		public static string? app_name = Application.ProductName;
		public const string app_version = "1.0.0";
		public static Icon ico = Properties.Resources.icon;

		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (Mutex mutex = new Mutex(true, $"{Process.GetCurrentProcess().ProcessName}", out bool createdNew))
			{
				if (createdNew)
				{
					ProgramMainRunning();
				}
				else if (!createdNew)
				{
					DialogResult result1 = MessageBox.Show("同じプログラムがすでに実行されています。\r\n実行しますか？(Y)それとも強制終了させますか？(N)", "重複実行警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
					if (result1 == DialogResult.Yes)
					{
						ProgramMainRunning();
					}
					else if (result1 == DialogResult.No)
					{
						Process.GetCurrentProcess().Kill();
						Application.Exit();
					}
					else if (result1 == DialogResult.Cancel)
					{
						Application.Exit();
					}
				}
			}
		}

		private static void ProgramMainRunning()
		{
			JavaChecks.JavaCheck();
			if (JavaChecks.javaInstalled)
			{
				Application.Run(new MainForm());
			}
		}

		public static string ToReadableSize(double size, int scale = 0, int standard = 1024)
		{
			string[] unit = [ "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" ];
			if (scale == unit.Length - 1 || size <= standard)
			{
				return $"{size.ToString(".##")} {unit[scale]}";
			}
			return ToReadableSize(size / standard, scale + 1, standard);
		}

		public static async Task<bool> IsNet()
		{
			try
			{
				using (var client = new HttpClient())
				using (await client.GetAsync(@"http://clients3.google.com/generate_204"))
				{
					return true;
				}
			}
			catch
			{
				return false;
			}
		}
	}

	partial class JavaChecks
	{
		public static bool javaInstalled = false;
		public static bool jdk8 = false;
		public static bool jdk16 = false;
		public static bool jdk17 = false;
		public static bool jdk8_i = false;
		public static bool jdk16_i = false;
		public static bool jdk17_i = false;

		public static void JavaCheck()
		{
			string programsFile_Folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).ToString();

			if (Directory.Exists(programsFile_Folder + @"\Eclipse Adoptium\jdk-8.0.392.8-hotspot"))
			{
				jdk8 = true;
			}
			if (Directory.Exists(programsFile_Folder + @"\Eclipse Foundation\jdk-16.0.2.7-hotspot"))
			{
				jdk16 = true;
			}
			if (Directory.Exists(programsFile_Folder + @"\Eclipse Adoptium\jdk-17.0.9.9-hotspot"))
			{
				jdk17 = true;
			}

			if (jdk8 && jdk16 && jdk17)
			{
				javaInstalled = true;
			}
			else
			{
				javaInstalled = false;
				JavaDownloads();
			}
		}

		public static void JavaDownloads()
		{
			string? exMessage = "";

			if (!jdk8)
			{
				exMessage += "Java Development Kit 1.8\r\n";
			}
			if (!jdk16)
			{
				exMessage += "Java Development Kit 16\r\n";
			}
			if (!jdk17)
			{
				exMessage += "Java Development Kit 17\r\n";
			}

			DialogResult drest = MessageBox.Show("Javaがインストールされていない、もしくは不足したJavaを検出しました。\r\nMinecraftのサーバーを建てるにはJavaが必須です。\r\n" + "不足したJava：\r\n" + exMessage + "\r\n" + "不足したJava Development Kitをインストールしますか？", "JavaCheck - 警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (drest == DialogResult.Yes)
			{
				JavaDownloadForm downloadForm = new JavaDownloadForm();
				downloadForm.ShowDialog();
			}
			else if (drest == DialogResult.No)
			{
				Application.Exit();
			}
		}
	}

	partial class JavaDownloadForm : Form
	{
		public static ProgressBar progressBar = new ProgressBar();
		public static Label processLabel = new Label();
		public static Button cancelButton = new Button();

		private static DriveInfo cDrive = new DriveInfo("C");

		private System.ComponentModel.IContainer? components = null;
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public JavaDownloadForm()
		{
			this.SuspendLayout();
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.Name = "JavaDownloadForm";
			this.Text = "JavaDownload";
			this.Size = new Size(370, 130);
			this.ClientSize = new Size(354, 91);
			this.ResumeLayout(false);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.ShowIcon = false;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.MaximizeBox = false;
			this.PerformLayout();

			this.Shown += new EventHandler(JavaDownloadForm_Shown);

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
				Enabled = true,
				Text = "キャンセル",
				UseVisualStyleBackColor = true
			};
			cancelButton.Click += new EventHandler(CancelButton_Click);
			this.CancelButton = cancelButton;

			this.Controls.Add(processLabel);
			this.Controls.Add(progressBar);
			this.Controls.Add(cancelButton);
		}

		private static string? log_str = "";
		private static string? net_ret_str = "";
		private static CancellationTokenSource tokenSource = new CancellationTokenSource();
		private static CancellationToken cancelToken = tokenSource.Token;

		private static double freeDrive = cDrive.TotalFreeSpace;
		private static double tortalDrive = cDrive.TotalSize;

		private async void JavaDownloadForm_Shown(object? sender, EventArgs e)
		{
			if (await Program.IsNet())
			{
				net_ret_str += "Online.";

				if (!JavaChecks.jdk8)
				{
					await JDK8InstallAsync();
				}
				if (!JavaChecks.jdk16)
				{
					await JDK16InstallAsync();
				}
				if (!JavaChecks.jdk17)
				{
					await JDK17InstallAsync();
				}
			}
			else
			{
				MessageBox.Show("インターネットに接続されて居ません！\r\nインターネットに接続されていることを確認してから、もう一度やり直してください。", "インターネット エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				net_ret_str += "Internet Not Online.";
			}

			await LoggerCreate_JavaInstall();
			if (JavaChecks.jdk8_i || JavaChecks.jdk16 || JavaChecks.jdk17_i)
			{
				Application.Restart();
			}
		}

		private static async Task LoggerCreate_JavaInstall()
		{
			try
			{
				using (StreamWriter sw = new StreamWriter("java_install.log", true, System.Text.Encoding.UTF8))
				{
					if (JavaChecks.jdk8_i)
					{
						log_str += "JDK 8\r\n";
					}
					if (JavaChecks.jdk16_i)
					{
						log_str += "JDK 16\r\n";
					}
					if (JavaChecks.jdk17_i)
					{
						log_str += "JDK 17\r\n";
					}

					if (log_str == "")
					{
						log_str += "None";
					}

					await sw.WriteLineAsync(DateTime.Now.ToString(@"(zzz) yyyy/MM/dd HH:mm:ss") + " EasyServers Log.");
					await sw.WriteLineAsync("Internet Connect：" + net_ret_str);
					await sw.WriteLineAsync("Install Completed :");
					await sw.WriteLineAsync(log_str);
					await sw.WriteLineAsync("");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("ログの書き込み中にエラーが発生しました！\r\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CancelButton_Click(object? sender, EventArgs e)
		{
			tokenSource.Cancel();
		}

		public static async Task JDK8InstallAsync()
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

					const string url = @"https://github.com/adoptium/temurin8-binaries/releases/download/jdk8u392-b08/OpenJDK8U-jdk_x64_windows_hotspot_8u392b08.msi";

					processLabel.Text = "空き容量を計算しています...";

					double jdk8_installer_bytes = await GetFileSizeAsync(url);

					string freeDriveStr = Program.ToReadableSize(freeDrive);
					string jdk8ByteStr = Program.ToReadableSize(jdk8_installer_bytes);

					if (freeDrive > jdk8_installer_bytes)
					{
						processLabel.Text = "ファイルの準備をしています...";
						progressBar.Style = ProgressBarStyle.Marquee;
						string tmpFilePath = Path.GetTempPath();
						string installFolderPath = tmpFilePath + Program.app_name;
						if (!Directory.Exists(installFolderPath))
						{
							Directory.CreateDirectory(installFolderPath);
							Directory.CreateDirectory(installFolderPath + @"\Java");
						}

						Random rand = new Random();
						string tmpFileName = "dl_" + rand.Next(65535).ToString() + @".dltmp";

						progressBar.Value = 0;
						TaskbarManager.Instance.SetProgressValue(0, 100);

						cancelButton.Enabled = true;

						progressBar.Style = ProgressBarStyle.Blocks;
						processLabel.Text = "ファイルをダウンロードしています...";

						byte[] data = await DownloadDataAsync(url, progress, cancelToken);
						cancelButton.Enabled = false;

						processLabel.Text = "ファイルに書き込んでいます...";
						progressBar.Value = 0;
						TaskbarManager.Instance.SetProgressValue(0, 100);

						string fileName = GetFileNameToUrl(url);
						await WriteDataToFileAsync(data, installFolderPath + @"\Java\" + tmpFileName);
						File.Move(installFolderPath + @"\Java\" + tmpFileName, installFolderPath + @"\Java\" + fileName);
						TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);

						processLabel.Text = "インストーラを展開してます...";
						using (Process process = new Process())
						{
							process.StartInfo.FileName = "msiexec.exe";
							process.StartInfo.Arguments = @"/i """ + installFolderPath + @"\Java\" + fileName + @"""";
							process.StartInfo.Verb = "RunAs";
							process.Start();
							process.WaitForExit();
							if (process.ExitCode == 0)
							{
								JavaChecks.jdk8_i = true;
							}
						}
						if (!JavaChecks.jdk16 || !JavaChecks.jdk17)
						{
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
							JavaChecks.jdk8 = true;
							File.Delete(installFolderPath + @"\Java\" + fileName);
						}
						else
						{
							processLabel.Text = "後処理をしています...";
							JavaChecks.jdk8 = true;
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
							File.Delete(installFolderPath + @"\Java\" + fileName);
							Directory.Delete(installFolderPath, true);
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
						}
					}
					else
					{
						MessageBox.Show("JDK 8をインストールするためのドライブ容量が足りません！\r\n容量を開けてから再度お試しください。\r\n\r\n空き容量：" + freeDriveStr + "\r\n必要な容量：" + jdk8ByteStr, Program.app_name + " - エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
						log_str += "Not Freedrive. Is Canceled.\r\n";
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

		public static async Task JDK16InstallAsync()
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

					const string url = @"https://github.com/adoptium/temurin16-binaries/releases/download/jdk-16.0.2%2B7/OpenJDK16U-jdk_x64_windows_hotspot_16.0.2_7.msi";

					cancelButton.Enabled = true;
					processLabel.Text = "空き容量を計算しています...";

					double jdk16_installer_bytes = await GetFileSizeAsync(url);

					string freeDriveStr = Program.ToReadableSize(freeDrive);
					string jdk16ByteStr = Program.ToReadableSize(jdk16_installer_bytes);

					if (freeDrive > jdk16_installer_bytes)
					{
						processLabel.Text = "ファイルの準備をしています...";
						progressBar.Style = ProgressBarStyle.Marquee;
						string tmpFilePath = Path.GetTempPath();
						string installFolderPath = tmpFilePath + Program.app_name;
						if (!Directory.Exists(installFolderPath))
						{
							Directory.CreateDirectory(installFolderPath);
							Directory.CreateDirectory(installFolderPath + @"\Java");
						}

						Random rand = new Random();
						string tmpFileName = "dl_" + rand.Next(65535).ToString() + @".dltmp";
						File.Create(installFolderPath + @"\Java\" + tmpFileName);

						progressBar.Value = 0;
						TaskbarManager.Instance.SetProgressValue(0, 100);

						cancelButton.Enabled = true;

						progressBar.Style = ProgressBarStyle.Blocks;
						processLabel.Text = "ファイルをダウンロードしています...";

						byte[] data = await DownloadDataAsync(url, progress, cancelToken);
						cancelButton.Enabled = false;

						processLabel.Text = "ファイルに書き込んでいます...";
						progressBar.Value = 0;
						TaskbarManager.Instance.SetProgressValue(0, 100);

						string fileName = GetFileNameToUrl(url);
						await WriteDataToFileAsync(data, installFolderPath + @"\Java\" + tmpFileName);
						File.Move(installFolderPath + @"\Java\" + tmpFileName, installFolderPath + @"\Java\" + fileName);
						TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);

						processLabel.Text = "インストーラを展開してます...";
						using (Process process = new Process())
						{
							process.StartInfo.FileName = "msiexec.exe";
							process.StartInfo.Arguments = @"/i """ + installFolderPath + @"\Java\" + fileName + @"""";
							process.StartInfo.Verb = "RunAs";
							process.Start();
							process.WaitForExit();
							if (process.ExitCode == 0)
							{
								JavaChecks.jdk16_i = true;
							}
						}

						if (!JavaChecks.jdk8 || !JavaChecks.jdk17)
						{
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
							JavaChecks.jdk16 = true;
							File.Delete(installFolderPath + @"\Java\" + fileName);
						}
						else
						{
							processLabel.Text = "後処理をしています...";
							JavaChecks.jdk16 = true;
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
							File.Delete(installFolderPath + @"\Java\" + fileName);
							Directory.Delete(installFolderPath, true);
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
						}
					}
					else
					{
						MessageBox.Show("JDK 16をインストールするためのドライブ容量が足りません！\r\n容量を開けてから再度お試しください。\r\n\r\n空き容量：" + freeDriveStr + "\r\n必要な容量：" + jdk16ByteStr, Program.app_name + " - エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
						log_str += "Not Freedrive. Is Canceled.\r\n";
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

		public static async Task JDK17InstallAsync()
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

					const string url = @"https://github.com/adoptium/temurin17-binaries/releases/download/jdk-17.0.9%2B9.1/OpenJDK17U-jdk_x64_windows_hotspot_17.0.9_9.msi";

					processLabel.Text = "空き容量を計算しています...";

					double jdk17_installer_bytes = await GetFileSizeAsync(url);

					string freeDriveStr = Program.ToReadableSize(freeDrive);
					string jdk17ByteStr = Program.ToReadableSize(jdk17_installer_bytes);

					if (freeDrive > jdk17_installer_bytes)
					{
						processLabel.Text = "ファイルの準備をしています...";
						progressBar.Style = ProgressBarStyle.Marquee;
						string tmpFilePath = Path.GetTempPath();
						string installFolderPath = tmpFilePath + Program.app_name;
						if (!Directory.Exists(installFolderPath))
						{
							Directory.CreateDirectory(installFolderPath);
							Directory.CreateDirectory(installFolderPath + @"\Java");
						}

						Random rand = new Random();
						string tmpFileName = "dl_" + rand.Next(65535).ToString() + @".dltmp";

						progressBar.Value = 0;
						TaskbarManager.Instance.SetProgressValue(0, 100);

						cancelButton.Enabled = true;

						progressBar.Style = ProgressBarStyle.Blocks;
						processLabel.Text = "ファイルをダウンロードしています...";

						byte[] data = await DownloadDataAsync(url, progress, cancelToken);
						cancelButton.Enabled = false;

						processLabel.Text = "ファイルに書き込んでいます...";
						progressBar.Value = 0;
						TaskbarManager.Instance.SetProgressValue(0, 100);

						string fileName = GetFileNameToUrl(url);
						await WriteDataToFileAsync(data, installFolderPath + @"\Java\" + tmpFileName);
						File.Move(installFolderPath + @"\Java\" + tmpFileName, installFolderPath + @"\Java\" + fileName);
						TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);

						processLabel.Text = "インストーラを展開してます...";
						using (Process process = new Process())
						{
							process.StartInfo.FileName = "msiexec.exe";
							process.StartInfo.Arguments = @"/i """ + installFolderPath + @"\Java\" + fileName + @"""";
							process.StartInfo.Verb = "RunAs";
							process.Start();
							process.WaitForExit();
							if (process.ExitCode == 0)
							{
								JavaChecks.jdk17_i = true;
							}
						}

						if (!JavaChecks.jdk8 || !JavaChecks.jdk16)
						{
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
							JavaChecks.jdk17 = true;
							File.Delete(installFolderPath + @"\Java\" + fileName);
						}
						else
						{
							processLabel.Text = "後処理をしています...";
							JavaChecks.jdk17 = true;
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
							File.Delete(installFolderPath + @"\Java\" + fileName);
							Directory.Delete(installFolderPath, true);
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
						}
					}
					else
					{
						MessageBox.Show("JDK 17をインストールするためのドライブ容量が足りません！\r\n容量を開けてから再度お試しください。\r\n\r\n空き容量：" + freeDriveStr + "\r\n必要な容量：" + jdk17ByteStr, Program.app_name + " - エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
						log_str += "Not Freedrive. Is Canceled.\r\n";
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

		private static async Task<byte[]> DownloadDataAsync(string uri, IProgress<int> progress, CancellationToken cancellationToken)
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

						if (cancellationToken.IsCancellationRequested)
						{
							processLabel.Text = "キャンセルしています...";
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
							memoryStream.Dispose();
							TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);

							MessageBox.Show("Canceled!");

							log_str += "Canceled\r\n";
							await LoggerCreate_JavaInstall();
							Application.Exit();
						}

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

	partial class MinecraftServerDownloads
	{
		public static bool offical_minecraft_server = false;
		public static bool spigot_server = false;
		public static bool papermc_server = false;

		// Offical
		public const string minecraft_serverSoftweare_url = @"https://piston-data.mojang.com/v1/objects/8dd1a28015f51b1803213892b50b7b4fc76e594d/server.jar";

		// PaperMC Minecraft v1.20.2
		public const string papermc_serverSoftweare_mc1202_build318_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.2/builds/318/downloads/paper-1.20.2-318.jar";
		public const string papermc_serverSoftweare_mc1202_build317_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.2/builds/317/downloads/paper-1.20.2-317.jar";
		public const string papermc_serverSoftweare_mc1202_build316_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.2/builds/316/downloads/paper-1.20.2-316.jar";
		public const string papermc_serverSoftweare_mc1202_build315_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.2/builds/315/downloads/paper-1.20.2-315.jar";
		public const string papermc_serverSoftweare_mc1202_build314_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.2/builds/314/downloads/paper-1.20.2-314.jar";
		public const string papermc_serverSoftweare_mc1202_build313_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.2/builds/313/downloads/paper-1.20.2-313.jar";

		// PaperMC Minecraft v1.20.1
		public const string papermc_serverSoftweare_mc1201_build196_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.1/builds/196/downloads/paper-1.20.1-196.jar";
		public const string papermc_serverSoftweare_mc1201_build195_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.1/builds/195/downloads/paper-1.20.1-195.jar";
		public const string papermc_serverSoftweare_mc1201_build194_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.1/builds/194/downloads/paper-1.20.1-194.jar";
		public const string papermc_serverSoftweare_mc1201_build193_url = @"https://api.papermc.io/v2/projects/paper/versions/1.20.1/builds/193/downloads/paper-1.20.1-193.jar";
	}
}
