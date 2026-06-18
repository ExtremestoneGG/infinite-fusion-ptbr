using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
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
            Font = new Font("Segoe UI", 9F);

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

            var title = new Label();
            title.Text = "Pokemon Infinite Fusion PT-BR Fan Translation";
            title.Font = new Font(Font.FontFamily, 15F, FontStyle.Bold);
            title.AutoSize = true;
            root.Controls.Add(title);

            var warning = new Label();
            warning.Text = "Unofficial fan-made translation. This tool does not include or download the game. Download Pokemon Infinite Fusion only from the official Pokemon Infinite Fusion Discord.";
            warning.AutoSize = false;
            warning.Height = 48;
            warning.Dock = DockStyle.Fill;
            warning.ForeColor = Color.DarkRed;
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
            backupCheckBox.Text = "Create backup before installing (recommended)";
            backupCheckBox.Checked = true;
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
            installButton.Click += delegate { RunInstall(); };
            buttonPanel.Controls.Add(installButton);

            restoreButton = new Button();
            restoreButton.Text = "Restore Latest Backup";
            restoreButton.AutoSize = true;
            restoreButton.Padding = new Padding(8, 4, 8, 4);
            restoreButton.Click += delegate { RunRestore(); };
            buttonPanel.Controls.Add(restoreButton);

            logBox = new TextBox();
            logBox.Dock = DockStyle.Fill;
            logBox.Multiline = true;
            logBox.ReadOnly = true;
            logBox.ScrollBars = ScrollBars.Vertical;
            logBox.Font = new Font("Consolas", 9F);
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
                browseButton, validateButton, installButton, restoreButton, gameDirBox, backupCheckBox
            });

            Log("Ready. Select your Pokemon Infinite Fusion folder to begin.");
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

            var backupEnabled = backupCheckBox.Checked;
            var confirmation = "Install the PT-BR translation into:\n\n" + folder +
                               "\n\nBackup: " + (backupEnabled ? "enabled" : "disabled") +
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
            var packageRoot = GetPackageRoot();
            var manifestPath = Path.Combine(packageRoot, "manifest.json");
            var patchPath = Path.Combine(packageRoot, "payload", "patches", "text_patches.json");
            if (!File.Exists(manifestPath)) throw new FileNotFoundException("Missing manifest.json", manifestPath);
            if (!File.Exists(patchPath)) throw new FileNotFoundException("Missing text_patches.json", patchPath);

            var backupEntries = new List<BackupEntry>();
            var backedUp = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var backupRoot = Path.Combine(gameRoot, "PTBR_BACKUPS", "ptbr_gui_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            if (backupEnabled) Directory.CreateDirectory(backupRoot);

            log("Copying translated files...");
            var copied = CopyDirectFiles(packageRoot, gameRoot, backupRoot, backupEnabled, backupEntries, backedUp);
            progress(25);

            log("Loading text patch...");
            var patch = DeserializeDictionary(File.ReadAllText(patchPath, Encoding.UTF8));
            var entryMap = BuildMap((object[])patch["entry_translations"]);
            progress(40);

            log("Patching Data\\pokedex\\dex.json...");
            var dexChanged = ApplyEntryPatch(gameRoot, backupRoot, backupEnabled, backupEntries, backedUp, entryMap, Path.Combine("Data", "pokedex", "dex.json"));
            progress(62);

            log("Patching Data\\dex.json...");
            var baseDexChanged = ApplyEntryPatch(gameRoot, backupRoot, backupEnabled, backupEntries, backedUp, entryMap, Path.Combine("Data", "dex.json"));
            progress(74);

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
                    version = "1.0.0",
                    created_on = DateTime.Now.ToString("s"),
                    game_dir = gameRoot,
                    entries = backupEntries
                };
                WriteUtf8NoBom(Path.Combine(backupRoot, "backup_manifest.json"), Serializer.Serialize(backupManifest));
            }
            progress(100);

            return "PT-BR translation installed.\n\n" +
                   "Files copied: " + copied + "\n" +
                   "Pokedex entries patched: " + dexChanged + "\n" +
                   "Base dex entries patched: " + baseDexChanged + "\n" +
                   "Outfit descriptions patched: " + descriptionChanged + "\n" +
                   "Backup: " + (backupEnabled ? backupRoot : "disabled");
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
                if (i % 10 == 0) progress((int)((i + 1) * 100.0 / total));
            }
            progress(100);
            log("Restored backup: " + latest.FullName);

            return "Latest PT-BR backup restored.\n\n" +
                   "Backup: " + latest.FullName + "\n" +
                   "Files restored: " + restored + "\n" +
                   "Created files removed: " + deleted;
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

        private static string GetPackageRoot()
        {
            var exeDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return Directory.GetParent(exeDir).FullName;
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
