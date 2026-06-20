using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace InfiniteFusionPtbrInstaller
{
    public class BackupEntry
    {
        public string path { get; set; }
        public string action { get; set; }
    }

    public class BackupManifest
    {
        public string version { get; set; }
        public string created_on { get; set; }
        public string game_dir { get; set; }
        public List<BackupEntry> entries { get; set; }
    }

    public class SaveLanguageResetResult
    {
        public int savesFound { get; set; }
        public int savesBackedUp { get; set; }
        public int savesChanged { get; set; }
        public int alreadyEnglish { get; set; }
        public string backupDir { get; set; }
    }

    public class InstallerForm : Form
    {
        private readonly TextBox gameDirBox;
        private readonly Button browseButton;
        private readonly Button validateButton;
        private readonly Button installButton;
        private readonly Button restoreButton;
        private readonly CheckBox backupCheckBox;
        private readonly TextBox logBox;
        private readonly ProgressBar progressBar;
        private readonly Label statusLabel;

        private readonly List<Control> actionControls = new List<Control>();

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InstallerForm());
        }

        public InstallerForm()
        {
            Text = "Pokemon Infinite Fusion PT-BR Installer";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(760, 560);
            Size = new Size(840, 620);
            Font = new Font("Segoe UI", 9.25F);
            BackColor = Color.FromArgb(246, 248, 250);

            var root = new TableLayoutPanel();
            root.Dock = DockStyle.Fill;
            root.ColumnCount = 1;
            root.RowCount = 6;
            root.Padding = new Padding(16);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(root);

            var header = new TableLayoutPanel();
            header.Dock = DockStyle.Top;
            header.Height = 108;
            header.ColumnCount = 1;
            header.RowCount = 3;
            header.Padding = new Padding(18, 14, 18, 12);
            header.BackColor = Color.FromArgb(13, 17, 23);
            header.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            header.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            header.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.Controls.Add(header);

            var title = new Label();
            title.Text = "Infinite Fusion PT-BR Mod";
            title.Font = new Font(Font.FontFamily, 18F, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.AutoSize = true;
            header.Controls.Add(title);

            var subtitle = new Label();
            subtitle.Text = "Fan-made Brazilian Portuguese translation installer";
            subtitle.ForeColor = Color.FromArgb(201, 209, 217);
            subtitle.AutoSize = true;
            header.Controls.Add(subtitle);

            var badgePanel = new FlowLayoutPanel();
            badgePanel.AutoSize = true;
            badgePanel.Margin = new Padding(0, 10, 0, 0);
            badgePanel.Controls.Add(CreateBadge("v1.1.2", Color.FromArgb(9, 105, 218), Color.White));
            badgePanel.Controls.Add(CreateBadge("Backup required", Color.FromArgb(191, 135, 0), Color.White));
            badgePanel.Controls.Add(CreateBadge("Unofficial fan mod", Color.FromArgb(130, 80, 223), Color.White));
            header.Controls.Add(badgePanel);

            var warning = new Label();
            warning.Text = "This tool does not include or download the game. Download Pokemon Infinite Fusion only from the official Pokemon Infinite Fusion Discord.";
            warning.AutoSize = false;
            warning.Height = 40;
            warning.Dock = DockStyle.Fill;
            warning.ForeColor = Color.FromArgb(130, 80, 0);
            root.Controls.Add(warning);

            var folderPanel = new TableLayoutPanel();
            folderPanel.Dock = DockStyle.Top;
            folderPanel.ColumnCount = 3;
            folderPanel.RowCount = 2;
            folderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            folderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            folderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            folderPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            folderPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.Controls.Add(folderPanel);

            var folderLabel = new Label();
            folderLabel.Text = "Game folder";
            folderLabel.AutoSize = true;
            folderPanel.Controls.Add(folderLabel, 0, 0);

            gameDirBox = new TextBox();
            gameDirBox.Dock = DockStyle.Fill;
            folderPanel.Controls.Add(gameDirBox, 0, 1);

            browseButton = new Button();
            browseButton.Text = "Browse...";
            browseButton.AutoSize = true;
            browseButton.Click += delegate { BrowseGameFolder(); };
            folderPanel.Controls.Add(browseButton, 1, 1);

            validateButton = new Button();
            validateButton.Text = "Validate";
            validateButton.AutoSize = true;
            validateButton.Click += delegate { ValidateSelectedFolderWithMessage(); };
            folderPanel.Controls.Add(validateButton, 2, 1);

            backupCheckBox = new CheckBox();
            backupCheckBox.Text = "Backup is required and will be saved in PTBR_BACKUPS inside the selected game folder";
            backupCheckBox.Checked = true;
            backupCheckBox.Enabled = false;
            backupCheckBox.AutoSize = true;
            backupCheckBox.Margin = new Padding(0, 12, 0, 8);
            root.Controls.Add(backupCheckBox);

            var buttonPanel = new FlowLayoutPanel();
            buttonPanel.FlowDirection = FlowDirection.LeftToRight;
            buttonPanel.AutoSize = true;
            buttonPanel.Dock = DockStyle.Top;
            root.Controls.Add(buttonPanel);

            installButton = new Button();
            installButton.Text = "Install PT-BR Translation";
            installButton.AutoSize = true;
            installButton.Padding = new Padding(8, 4, 8, 4);
            installButton.BackColor = Color.FromArgb(35, 134, 54);
            installButton.ForeColor = Color.White;
            installButton.FlatStyle = FlatStyle.Flat;
            installButton.FlatAppearance.BorderSize = 0;
            installButton.Click += delegate { RunInstall(); };
            buttonPanel.Controls.Add(installButton);

            restoreButton = new Button();
            restoreButton.Text = "Uninstall Translation";
            restoreButton.AutoSize = true;
            restoreButton.Padding = new Padding(8, 4, 8, 4);
            restoreButton.BackColor = Color.FromArgb(87, 96, 106);
            restoreButton.ForeColor = Color.White;
            restoreButton.FlatStyle = FlatStyle.Flat;
            restoreButton.FlatAppearance.BorderSize = 0;
            restoreButton.Click += delegate { RunRestore(); };
            buttonPanel.Controls.Add(restoreButton);

            logBox = new TextBox();
            logBox.Dock = DockStyle.Fill;
            logBox.Multiline = true;
            logBox.ReadOnly = true;
            logBox.ScrollBars = ScrollBars.Vertical;
            logBox.Font = new Font("Consolas", 9F);
            logBox.BackColor = Color.FromArgb(13, 17, 23);
            logBox.ForeColor = Color.FromArgb(201, 209, 217);
            root.Controls.Add(logBox);

            var statusPanel = new TableLayoutPanel();
            statusPanel.ColumnCount = 1;
            statusPanel.RowCount = 2;
            statusPanel.Dock = DockStyle.Fill;
            statusPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            statusPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.Controls.Add(statusPanel);

            statusLabel = new Label();
            statusLabel.Text = "Ready.";
            statusLabel.AutoSize = true;
            statusPanel.Controls.Add(statusLabel);

            progressBar = new ProgressBar();
            progressBar.Dock = DockStyle.Fill;
            progressBar.Style = ProgressBarStyle.Continuous;
            statusPanel.Controls.Add(progressBar);

            actionControls.AddRange(new Control[] {
                browseButton, validateButton, installButton, restoreButton, gameDirBox
            });

            Log("Ready. Select your Pokemon Infinite Fusion folder to begin.");
        }

        private static Label CreateBadge(string text, Color backColor, Color foreColor)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                BackColor = backColor,
                ForeColor = foreColor,
                Font = new Font("Segoe UI", 8.25F, FontStyle.Bold),
                Padding = new Padding(8, 3, 8, 3),
                Margin = new Padding(0, 0, 8, 0)
            };
        }

        private void BrowseGameFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select your Pokemon Infinite Fusion game folder.";
                dialog.ShowNewFolderButton = false;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    gameDirBox.Text = dialog.SelectedPath;
                }
            }
        }

        private void ValidateSelectedFolderWithMessage()
        {
            try
            {
                var folder = ValidateGameDir(gameDirBox.Text);
                MessageBox.Show(this, "Folder looks valid:\n" + folder, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log("Folder validated: " + folder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation failed: " + ex.Message);
            }
        }

        private void RunInstall()
        {
            string folder;
            try
            {
                folder = ValidateGameDir(gameDirBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Install stopped", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var backupEnabled = true;
            var confirmation = "Install the PT-BR translation into:\n\n" + folder +
                               "\n\nBackup: required and saved in PTBR_BACKUPS" +
                               "\n\nThis is an unofficial fan-made patch. It does not download anything and does not include the game.\n\nContinue?";
            if (MessageBox.Show(this, confirmation, "Install PT-BR Translation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            RunBackground("Installing...", delegate
            {
                var result = InstallerCore.Install(folder, backupEnabled, LogThreadSafe, SetProgressThreadSafe);
                MessageThreadSafe(result, "Install complete", MessageBoxIcon.Information);
            });
        }

        private void RunRestore()
        {
            string folder;
            try
            {
                folder = ValidateGameDir(gameDirBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Restore stopped", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(this, "Restore the latest PT-BR backup for:\n\n" + folder + "\n\nContinue?", "Restore backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            RunBackground("Restoring...", delegate
            {
                var result = InstallerCore.RestoreLatest(folder, LogThreadSafe, SetProgressThreadSafe);
                MessageThreadSafe(result, "Restore complete", MessageBoxIcon.Information);
            });
        }

        private void RunBackground(string status, Action work)
        {
            SetBusy(true);
            statusLabel.Text = status;
            progressBar.Style = ProgressBarStyle.Marquee;
            Task.Run(delegate
            {
                try
                {
                    work();
                    SetStatusThreadSafe("Done.");
                }
                catch (Exception ex)
                {
                    LogThreadSafe("Error: " + ex.Message);
                    MessageThreadSafe(ex.Message, "Installer error", MessageBoxIcon.Error);
                    SetStatusThreadSafe("Error.");
                }
                finally
                {
                    SetBusyThreadSafe(false);
                    SetProgressThreadSafe(0);
                }
            });
        }

        private string ValidateGameDir(string rawPath)
        {
            if (string.IsNullOrWhiteSpace(rawPath))
            {
                throw new InvalidOperationException("Select the Pokemon Infinite Fusion folder first.");
            }
            var full = Path.GetFullPath(rawPath);
            if (!Directory.Exists(full))
            {
                throw new InvalidOperationException("Folder does not exist: " + full);
            }
            if (!Directory.Exists(Path.Combine(full, "Data")))
            {
                throw new InvalidOperationException("This does not look like the game folder: missing Data folder.");
            }
            if (!Directory.Exists(Path.Combine(full, "Data", "Scripts")))
            {
                throw new InvalidOperationException("This does not look like the game folder: missing Data\\Scripts folder.");
            }
            var exeFound = File.Exists(Path.Combine(full, "Game.exe")) ||
                           File.Exists(Path.Combine(full, "InfiniteFusion.exe")) ||
                           File.Exists(Path.Combine(full, "infinitefusion.exe"));
            if (!exeFound)
            {
                Log("Warning: common game executable not found, but Data and Data\\Scripts exist.");
            }
            return full;
        }

        private void SetBusy(bool busy)
        {
            foreach (var control in actionControls)
            {
                control.Enabled = !busy;
            }
        }

        private void Log(string message)
        {
            logBox.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + message + Environment.NewLine);
        }

        private void LogThreadSafe(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(LogThreadSafe), message);
                return;
            }
            Log(message);
        }

        private void SetBusyThreadSafe(bool busy)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<bool>(SetBusyThreadSafe), busy);
                return;
            }
            SetBusy(busy);
        }

        private void SetProgressThreadSafe(int value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<int>(SetProgressThreadSafe), value);
                return;
            }
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = Math.Max(progressBar.Minimum, Math.Min(progressBar.Maximum, value));
        }

        private void SetStatusThreadSafe(string status)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(SetStatusThreadSafe), status);
                return;
            }
            statusLabel.Text = status;
        }

        private void MessageThreadSafe(string message, string title, MessageBoxIcon icon)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string, string, MessageBoxIcon>(MessageThreadSafe), message, title, icon);
                return;
            }
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, icon);
        }
    }

    public static class InstallerCore
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer
        {
            MaxJsonLength = int.MaxValue,
            RecursionLimit = 1000
        };

        public static string Install(string gameRoot, bool backupEnabled, Action<string> log, Action<int> progress)
        {
            backupEnabled = true;
            var packageRoot = GetPackageRoot(gameRoot);
            var manifestPath = Path.Combine(packageRoot, "manifest.json");
            var patchPath = Path.Combine(packageRoot, "payload", "patches", "text_patches.json");
            if (!File.Exists(manifestPath)) throw new FileNotFoundException("Missing manifest.json", manifestPath);
            if (!File.Exists(patchPath)) throw new FileNotFoundException("Missing text_patches.json", patchPath);

            var backupEntries = new List<BackupEntry>();
            var backedUp = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var backupRoot = Path.Combine(gameRoot, "PTBR_BACKUPS", "ptbr_gui_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            Directory.CreateDirectory(backupRoot);

            log("Copying translated files...");
            var copied = CopyDirectFiles(packageRoot, gameRoot, backupRoot, backupEnabled, backupEntries, backedUp);
            progress(25);

            log("Loading outfit text patch...");
            var patch = DeserializeDictionary(File.ReadAllText(patchPath, Encoding.UTF8));
            progress(40);

            log("Skipping Pokedex descriptions in v1.1.2. They are planned as future work.");
            progress(62);

            log("Patching outfit descriptions...");
            var descriptionChanged = 0;
            foreach (Dictionary<string, object> section in (object[])patch["outfit_description_translations"])
            {
                var relative = Convert.ToString(section["path"]).Replace('/', Path.DirectorySeparatorChar);
                var map = BuildMap((object[])section["translations"]);
                descriptionChanged += ApplyDescriptionPatch(gameRoot, backupRoot, backupEnabled, backupEntries, backedUp, map, relative);
            }
            progress(92);

            if (backupEnabled && backupEntries.Count > 0)
            {
                var backupManifest = new BackupManifest
                {
                    version = "1.1.2",
                    created_on = DateTime.Now.ToString("s"),
                    game_dir = gameRoot,
                    entries = backupEntries
                };
                WriteUtf8NoBom(Path.Combine(backupRoot, "backup_manifest.json"), Serializer.Serialize(backupManifest));
            }
            progress(100);

            CleanupEmbeddedPackage(gameRoot, packageRoot);

            return "PT-BR translation installed.\n\n" +
                   "Files copied: " + copied + "\n" +
                   "Pokedex descriptions: skipped for v1.1.2 future work\n" +
                   "Outfit descriptions patched: " + descriptionChanged + "\n" +
                   "Backup: " + backupRoot;
        }

        public static string RestoreLatest(string gameRoot, Action<string> log, Action<int> progress)
        {
            var backupBase = Path.Combine(gameRoot, "PTBR_BACKUPS");
            if (!Directory.Exists(backupBase)) throw new InvalidOperationException("No PTBR_BACKUPS folder found.");
            var latest = new DirectoryInfo(backupBase).GetDirectories()
                .OrderByDescending(d => d.LastWriteTimeUtc)
                .FirstOrDefault();
            if (latest == null) throw new InvalidOperationException("No backups found.");

            var manifestPath = Path.Combine(latest.FullName, "backup_manifest.json");
            if (!File.Exists(manifestPath)) throw new FileNotFoundException("Backup manifest not found", manifestPath);

            log("Backing up save files and resetting save language to English...");
            var saveReset = ResetSaveLanguageToEnglish(gameRoot, log);
            progress(5);

            var manifest = DeserializeDictionary(File.ReadAllText(manifestPath, Encoding.UTF8));
            var entries = (object[])manifest["entries"];
            var restored = 0;
            var deleted = 0;
            var total = Math.Max(1, entries.Length);
            for (var i = 0; i < entries.Length; i++)
            {
                var entry = (Dictionary<string, object>)entries[i];
                var relative = Convert.ToString(entry["path"]);
                var action = Convert.ToString(entry["action"]);
                var target = JoinUnderRoot(gameRoot, relative);
                var backupFile = JoinUnderRoot(latest.FullName, relative);
                if (action == "restore_file")
                {
                    if (File.Exists(backupFile))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(target));
                        File.Copy(backupFile, target, true);
                        restored++;
                    }
                }
                else if (action == "delete_created")
                {
                    if (File.Exists(target))
                    {
                        File.Delete(target);
                        deleted++;
                    }
                }
                if (i % 10 == 0) progress(5 + (int)((i + 1) * 95.0 / total));
            }
            progress(100);
            log("Restored backup: " + latest.FullName);

            return "Latest PT-BR backup restored.\n\n" +
                   "Backup: " + latest.FullName + "\n" +
                   "Save files found: " + saveReset.savesFound + "\n" +
                   "Save files changed to English: " + saveReset.savesChanged + "\n" +
                   "Save backup: " + (string.IsNullOrEmpty(saveReset.backupDir) ? "not needed" : saveReset.backupDir) + "\n" +
                   "Files restored: " + restored + "\n" +
                   "Created files removed: " + deleted;
        }

        private static SaveLanguageResetResult ResetSaveLanguageToEnglish(string gameRoot, Action<string> log)
        {
            var result = new SaveLanguageResetResult();
            var saveDirs = FindSaveDirectories(gameRoot);
            var saveFiles = new List<string>();
            foreach (var dir in saveDirs)
            {
                try
                {
                    saveFiles.AddRange(Directory.GetFiles(dir, "*.rxdata", SearchOption.TopDirectoryOnly));
                }
                catch (Exception ex)
                {
                    log("Could not read save folder " + dir + ": " + ex.Message);
                }
            }

            saveFiles = saveFiles
                .Where(IsLikelySaveFile)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                .ToList();

            result.savesFound = saveFiles.Count;
            if (saveFiles.Count == 0)
            {
                log("No save files found for language reset.");
                return result;
            }

            result.backupDir = Path.Combine(gameRoot, "PTBR_BACKUPS", "save_backups", "save_language_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            Directory.CreateDirectory(result.backupDir);

            foreach (var saveFile in saveFiles)
            {
                var backupPath = MakeUniqueBackupPath(result.backupDir, Path.GetFileName(saveFile));
                File.Copy(saveFile, backupPath, false);
                result.savesBackedUp++;

                var bytes = File.ReadAllBytes(saveFile);
                int oldLanguage;
                bool detected;
                var changed = TryPatchPokemonSystemLanguage(bytes, out oldLanguage, out detected);
                if (changed)
                {
                    File.WriteAllBytes(saveFile, bytes);
                    result.savesChanged++;
                    log("Save language reset to English: " + Path.GetFileName(saveFile) + " (" + oldLanguage + " -> 0)");
                }
                else if (detected)
                {
                    result.alreadyEnglish++;
                    log("Save language already English: " + Path.GetFileName(saveFile));
                }
                else
                {
                    log("Save language field not detected, backup kept: " + Path.GetFileName(saveFile));
                }
            }

            log("Save backup created: " + result.backupDir);
            return result;
        }

        private static List<string> FindSaveDirectories(string gameRoot)
        {
            var dirs = new List<string>();
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            AddDirectoryIfExists(dirs, Path.Combine(appData, "infinitefusion"));
            AddDirectoryIfExists(dirs, Path.Combine(Environment.GetEnvironmentVariable("APPDATA") ?? "", "infinitefusion"));
            AddDirectoryIfExists(dirs, gameRoot);
            AddDirectoryIfExists(dirs, Path.Combine(gameRoot, "Savefile"));
            AddDirectoryIfExists(dirs, Path.Combine(gameRoot, "savefiles"));
            return dirs;
        }

        private static void AddDirectoryIfExists(List<string> dirs, string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return;
            try
            {
                var full = Path.GetFullPath(path);
                if (Directory.Exists(full) && !dirs.Contains(full, StringComparer.OrdinalIgnoreCase))
                {
                    dirs.Add(full);
                }
            }
            catch
            {
            }
        }

        private static bool IsLikelySaveFile(string path)
        {
            var name = Path.GetFileName(path);
            if (string.IsNullOrEmpty(name)) return false;
            if (name.Equals("Game.rxdata", StringComparison.OrdinalIgnoreCase)) return true;
            if (name.StartsWith("File ", StringComparison.OrdinalIgnoreCase) && name.EndsWith(".rxdata", StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        private static string MakeUniqueBackupPath(string backupDir, string fileName)
        {
            var target = Path.Combine(backupDir, fileName);
            if (!File.Exists(target)) return target;
            var stem = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            for (var i = 2; ; i++)
            {
                target = Path.Combine(backupDir, stem + "_" + i + ext);
                if (!File.Exists(target)) return target;
            }
        }

        private static bool TryPatchPokemonSystemLanguage(byte[] bytes, out int oldLanguage, out bool detected)
        {
            oldLanguage = -1;
            detected = false;
            var className = Encoding.ASCII.GetBytes("PokemonSystem");
            var screensizeName = Encoding.ASCII.GetBytes("@screensize");
            var searchStart = 0;
            while (true)
            {
                var classOffset = IndexOfBytes(bytes, className, searchStart);
                if (classOffset < 0) return false;
                var screenOffset = IndexOfBytes(bytes, screensizeName, classOffset);
                if (screenOffset < 0 || screenOffset - classOffset > 1024)
                {
                    searchStart = classOffset + 1;
                    continue;
                }

                var pos = screenOffset + screensizeName.Length;
                if (pos >= bytes.Length || bytes[pos] != (byte)'i')
                {
                    searchStart = classOffset + 1;
                    continue;
                }
                int ignored;
                int afterScreensize;
                if (!TryReadMarshalFixnum(bytes, pos + 1, out ignored, out afterScreensize))
                {
                    searchStart = classOffset + 1;
                    continue;
                }

                pos = afterScreensize;
                if (pos >= bytes.Length)
                {
                    searchStart = classOffset + 1;
                    continue;
                }
                if (bytes[pos] == (byte)':')
                {
                    if (!TrySkipMarshalSymbol(bytes, pos + 1, out pos))
                    {
                        searchStart = classOffset + 1;
                        continue;
                    }
                }
                else if (bytes[pos] == (byte)';')
                {
                    if (!TryReadMarshalFixnum(bytes, pos + 1, out ignored, out pos))
                    {
                        searchStart = classOffset + 1;
                        continue;
                    }
                }
                else
                {
                    searchStart = classOffset + 1;
                    continue;
                }

                if (pos >= bytes.Length || bytes[pos] != (byte)'i')
                {
                    searchStart = classOffset + 1;
                    continue;
                }

                var valueOffset = pos + 1;
                int afterLanguage;
                if (!TryReadMarshalFixnum(bytes, valueOffset, out oldLanguage, out afterLanguage))
                {
                    searchStart = classOffset + 1;
                    continue;
                }
                detected = true;
                if (oldLanguage == 0) return false;

                // Language values are small Ruby Marshal fixnums: 0 => 00, 1 => 06, 2 => 07.
                if (afterLanguage == valueOffset + 1 && oldLanguage >= 0 && oldLanguage <= 2)
                {
                    bytes[valueOffset] = 0x00;
                    return true;
                }
                return false;
            }
        }

        private static int IndexOfBytes(byte[] source, byte[] needle, int start)
        {
            if (source == null || needle == null || needle.Length == 0) return -1;
            for (var i = Math.Max(0, start); i <= source.Length - needle.Length; i++)
            {
                var ok = true;
                for (var j = 0; j < needle.Length; j++)
                {
                    if (source[i + j] != needle[j])
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok) return i;
            }
            return -1;
        }

        private static bool TrySkipMarshalSymbol(byte[] bytes, int offset, out int next)
        {
            next = offset;
            int length;
            if (!TryReadMarshalFixnum(bytes, offset, out length, out next)) return false;
            if (length < 0 || next + length > bytes.Length) return false;
            next += length;
            return true;
        }

        private static bool TryReadMarshalFixnum(byte[] bytes, int offset, out int value, out int next)
        {
            value = 0;
            next = offset;
            if (offset >= bytes.Length) return false;
            var first = (int)(sbyte)bytes[offset];
            next = offset + 1;
            if (first == 0)
            {
                value = 0;
                return true;
            }
            if (first > 5)
            {
                value = first - 5;
                return true;
            }
            if (first < -4)
            {
                value = first + 5;
                return true;
            }
            if (first > 0)
            {
                if (next + first > bytes.Length) return false;
                var raw = 0;
                for (var i = 0; i < first; i++)
                {
                    raw |= bytes[next + i] << (8 * i);
                }
                value = raw;
                next += first;
                return true;
            }
            var count = -first;
            if (next + count > bytes.Length) return false;
            var negative = -1;
            for (var i = 0; i < count; i++)
            {
                negative &= ~(0xFF << (8 * i));
                negative |= bytes[next + i] << (8 * i);
            }
            value = negative;
            next += count;
            return true;
        }

        private static int CopyDirectFiles(string packageRoot, string gameRoot, string backupRoot, bool backupEnabled, List<BackupEntry> backupEntries, HashSet<string> backedUp)
        {
            var payloadRoot = Path.Combine(packageRoot, "payload", "files");
            if (!Directory.Exists(payloadRoot)) throw new DirectoryNotFoundException("Missing payload\\files folder.");
            var count = 0;
            foreach (var source in Directory.GetFiles(payloadRoot, "*", SearchOption.AllDirectories))
            {
                var relative = source.Substring(payloadRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                BackupRelativeFile(gameRoot, backupRoot, relative, backupEnabled, backupEntries, backedUp);
                var target = JoinUnderRoot(gameRoot, relative);
                Directory.CreateDirectory(Path.GetDirectoryName(target));
                File.Copy(source, target, true);
                count++;
            }
            return count;
        }

        private static int ApplyEntryPatch(string gameRoot, string backupRoot, bool backupEnabled, List<BackupEntry> backupEntries, HashSet<string> backedUp, Dictionary<string, string> map, string relative)
        {
            var target = JoinUnderRoot(gameRoot, relative);
            if (!File.Exists(target)) return 0;
            BackupRelativeFile(gameRoot, backupRoot, relative, backupEnabled, backupEntries, backedUp);

            var rows = DeserializeArray(File.ReadAllText(target, Encoding.UTF8));
            var changed = 0;
            foreach (Dictionary<string, object> row in rows)
            {
                object value;
                if (row.TryGetValue("entry", out value) && value != null)
                {
                    var old = Convert.ToString(value);
                    string translated;
                    if (map.TryGetValue(old, out translated))
                    {
                        row["entry"] = translated;
                        changed++;
                    }
                }
            }
            if (changed > 0) WriteUtf8NoBom(target, Serializer.Serialize(rows));
            return changed;
        }

        private static int ApplyDescriptionPatch(string gameRoot, string backupRoot, bool backupEnabled, List<BackupEntry> backupEntries, HashSet<string> backedUp, Dictionary<string, string> map, string relative)
        {
            var target = JoinUnderRoot(gameRoot, relative);
            if (!File.Exists(target)) return 0;
            BackupRelativeFile(gameRoot, backupRoot, relative, backupEnabled, backupEntries, backedUp);

            var rows = DeserializeArray(File.ReadAllText(target, Encoding.UTF8));
            var changed = 0;
            foreach (Dictionary<string, object> row in rows)
            {
                object value;
                if (row.TryGetValue("description", out value) && value != null)
                {
                    var old = Convert.ToString(value);
                    string translated;
                    if (map.TryGetValue(old, out translated))
                    {
                        row["description"] = translated;
                        changed++;
                    }
                }
            }
            if (changed > 0) WriteUtf8NoBom(target, Serializer.Serialize(rows));
            return changed;
        }

        private static void BackupRelativeFile(string gameRoot, string backupRoot, string relative, bool backupEnabled, List<BackupEntry> backupEntries, HashSet<string> backedUp)
        {
            if (!backupEnabled) return;
            if (backedUp.Contains(relative)) return;

            var target = JoinUnderRoot(gameRoot, relative);
            var backupTarget = JoinUnderRoot(backupRoot, relative);
            Directory.CreateDirectory(Path.GetDirectoryName(backupTarget));
            if (File.Exists(target))
            {
                File.Copy(target, backupTarget, true);
                backupEntries.Add(new BackupEntry { path = relative, action = "restore_file" });
            }
            else
            {
                backupEntries.Add(new BackupEntry { path = relative, action = "delete_created" });
            }
            backedUp.Add(relative);
        }

        private static Dictionary<string, string> BuildMap(object[] pairs)
        {
            var map = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (Dictionary<string, object> pair in pairs)
            {
                var source = Convert.ToString(pair["source"]);
                var target = Convert.ToString(pair["target"]);
                if (!map.ContainsKey(source)) map.Add(source, target);
            }
            return map;
        }

        private static Dictionary<string, object> DeserializeDictionary(string text)
        {
            return (Dictionary<string, object>)Serializer.DeserializeObject(text);
        }

        private static List<Dictionary<string, object>> DeserializeArray(string text)
        {
            var array = (object[])Serializer.DeserializeObject(text);
            var rows = new List<Dictionary<string, object>>(array.Length);
            foreach (var item in array)
            {
                rows.Add((Dictionary<string, object>)item);
            }
            return rows;
        }

        private static string GetPackageRoot(string gameRoot)
        {
            var exeDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var candidates = new List<string>();
            candidates.Add(exeDir);
            var parent = Directory.GetParent(exeDir);
            if (parent != null) candidates.Add(parent.FullName);

            foreach (var candidate in candidates)
            {
                if (File.Exists(Path.Combine(candidate, "manifest.json")) &&
                    Directory.Exists(Path.Combine(candidate, "payload", "files")) &&
                    File.Exists(Path.Combine(candidate, "payload", "patches", "text_patches.json")))
                {
                    return candidate;
                }
            }

            return ExtractEmbeddedPackage(gameRoot);
        }

        private static string ExtractEmbeddedPackage(string gameRoot)
        {
            var tempRoot = Path.Combine(gameRoot, "PTBR_INSTALLER_TEMP");
            var packageRoot = Path.Combine(tempRoot, "package");
            if (Directory.Exists(tempRoot)) Directory.Delete(tempRoot, true);
            Directory.CreateDirectory(packageRoot);

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("ptbr_payload.zip"))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("Missing embedded PT-BR package. Download the full installer package again.");
                }
                var zipPath = Path.Combine(tempRoot, "payload.zip");
                using (var output = File.Create(zipPath))
                {
                    stream.CopyTo(output);
                }
                ZipFile.ExtractToDirectory(zipPath, packageRoot);
            }
            return packageRoot;
        }

        private static void CleanupEmbeddedPackage(string gameRoot, string packageRoot)
        {
            var tempRoot = Path.Combine(gameRoot, "PTBR_INSTALLER_TEMP");
            var fullTemp = Path.GetFullPath(tempRoot).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var fullPackage = Path.GetFullPath(packageRoot).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            if (fullPackage.StartsWith(fullTemp, StringComparison.OrdinalIgnoreCase) && Directory.Exists(fullTemp))
            {
                Directory.Delete(fullTemp, true);
            }
        }

        private static string JoinUnderRoot(string root, string relative)
        {
            var rootFull = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
            var combined = Path.GetFullPath(Path.Combine(rootFull, relative));
            if (!combined.StartsWith(rootFull, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Unsafe path outside root: " + relative);
            }
            return combined;
        }

        private static void WriteUtf8NoBom(string path, string text)
        {
            File.WriteAllText(path, text + Environment.NewLine, new UTF8Encoding(false));
        }
    }
}
