using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace InfiniteFusionPtbrInstaller
{
    public class ModernInstallerApp : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new ModernInstallerApp();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run(new ModernInstallerWindow());
        }
    }

    public class ModernInstallerWindow : Window
    {
        private TextBox gameDirBox;
        private Button chooseButton;
        private Button scanButton;
        private Button validateButton;
        private Button installButton;
        private Button restoreButton;
        private Button backupButton;
        private ProgressBar progressBar;
        private TextBlock statusTitle;
        private TextBlock statusDetails;
        private TextBox logBox;
        private readonly List<Control> actionControls = new List<Control>();
        private CancellationTokenSource scanCancellation;
        private Button maximizeButton;

        private static readonly Brush PageBackground = BrushFromRgb(13, 17, 23);
        private static readonly Brush TitleBackground = BrushFromRgb(1, 4, 9);
        private static readonly Brush CardBackground = BrushFromRgb(22, 27, 34);
        private static readonly Brush ElevatedCard = BrushFromRgb(28, 33, 40);
        private static readonly Brush BorderColor = BrushFromRgb(48, 54, 61);
        private static readonly Brush MutedText = BrushFromRgb(139, 148, 158);
        private static readonly Brush NormalText = BrushFromRgb(230, 237, 243);
        private static readonly Brush WarningText = BrushFromRgb(247, 178, 75);
        private static readonly Brush Green = BrushFromRgb(35, 134, 54);
        private static readonly Brush Blue = BrushFromRgb(9, 105, 218);
        private static readonly Brush Purple = BrushFromRgb(130, 80, 223);
        private static readonly Brush Danger = BrushFromRgb(218, 54, 51);
        private static readonly Brush Cyan = BrushFromRgb(57, 197, 207);
        private static readonly Brush GrayButton = BrushFromRgb(48, 54, 61);
        private static Style cachedButtonStyle;

        public ModernInstallerWindow()
        {
            Title = "Infinite Fusion PT-BR Mod Installer";
            Width = 1040;
            Height = 760;
            MinWidth = 920;
            MinHeight = 660;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.CanResizeWithGrip;
            Background = PageBackground;
            Foreground = NormalText;
            FontFamily = new FontFamily("Segoe UI");
            Icon = LoadIcon();
            StateChanged += delegate { UpdateMaximizeButton(); };

            TextOptions.SetTextFormattingMode(this, TextFormattingMode.Display);
            TextOptions.SetTextRenderingMode(this, TextRenderingMode.ClearType);

            var shell = new Grid();
            shell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            shell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            Content = shell;

            var titleBar = CreateTitleBar();
            Grid.SetRow(titleBar, 0);
            shell.Children.Add(titleBar);

            var root = new Grid();
            root.Margin = new Thickness(22, 18, 22, 22);
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            Grid.SetRow(root, 1);
            shell.Children.Add(root);

            var header = CreateHeader();
            Grid.SetRow(header, 0);
            root.Children.Add(header);

            var warning = CreateWarning();
            Grid.SetRow(warning, 1);
            root.Children.Add(warning);

            var actions = CreateActionCard();
            Grid.SetRow(actions, 2);
            root.Children.Add(actions);

            var logCard = CreateLogCard();
            Grid.SetRow(logCard, 3);
            root.Children.Add(logCard);

            actionControls.AddRange(new Control[] {
                chooseButton, scanButton, validateButton, installButton, restoreButton, backupButton, gameDirBox
            });

            SetResult("Pronto para instalar", "Escolha o Game.exe do Pokemon Infinite Fusion ou use a busca automática.");
            Log("Pronto. Escolha o Game.exe ou clique em Escanear.");
        }

        private UIElement CreateTitleBar()
        {
            var bar = new Border
            {
                Background = TitleBackground,
                BorderBrush = BorderColor,
                BorderThickness = new Thickness(0, 0, 0, 1),
                Height = 44
            };
            bar.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
            {
                if (e.ChangedButton != MouseButton.Left) return;
                if (e.ClickCount == 2)
                {
                    ToggleMaximize();
                    return;
                }
                DragMove();
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            bar.Child = grid;

            var identity = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(14, 0, 0, 0)
            };
            Grid.SetColumn(identity, 0);
            grid.Children.Add(identity);

            identity.Children.Add(new Image
            {
                Source = LoadImage("installer_icon.png"),
                Width = 24,
                Height = 24,
                Margin = new Thickness(0, 0, 9, 0)
            });
            identity.Children.Add(new TextBlock
            {
                Text = "Infinite Fusion PT-BR Mod",
                Foreground = NormalText,
                FontSize = 14,
                FontWeight = FontWeights.SemiBold,
                VerticalAlignment = VerticalAlignment.Center
            });
            identity.Children.Add(new TextBlock
            {
                Text = "  v1.1.5",
                Foreground = Cyan,
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Center
            });

            var titleActions = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            Grid.SetColumn(titleActions, 2);
            grid.Children.Add(titleActions);

            var minimize = CreateWindowButton("-", GrayButton);
            minimize.Click += delegate { WindowState = WindowState.Minimized; };
            titleActions.Children.Add(minimize);

            maximizeButton = CreateWindowButton("□", GrayButton);
            maximizeButton.Click += delegate { ToggleMaximize(); };
            titleActions.Children.Add(maximizeButton);

            var close = CreateWindowButton("X", Danger);
            close.Click += delegate { Close(); };
            titleActions.Children.Add(close);
            return bar;
        }

        private Button CreateWindowButton(string text, Brush hoverBrush)
        {
            var button = new Button
            {
                Content = text,
                Width = 46,
                Height = 34,
                Background = TitleBackground,
                Foreground = NormalText,
                BorderBrush = TitleBackground,
                FontWeight = FontWeights.SemiBold,
                Padding = new Thickness(0),
                Style = GetButtonStyle(),
                Cursor = Cursors.Hand
            };
            button.MouseEnter += delegate
            {
                button.Background = hoverBrush;
                button.BorderBrush = hoverBrush;
            };
            button.MouseLeave += delegate
            {
                button.Background = TitleBackground;
                button.BorderBrush = TitleBackground;
            };
            return button;
        }

        private void ToggleMaximize()
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            UpdateMaximizeButton();
        }

        private void UpdateMaximizeButton()
        {
            if (maximizeButton != null)
            {
                maximizeButton.Content = WindowState == WindowState.Maximized ? "❐" : "□";
            }
        }

        private UIElement CreateHeader()
        {
            var card = CreateCard();
            card.Padding = new Thickness(20);
            card.Margin = new Thickness(0, 0, 0, 14);

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            card.Child = grid;

            var icon = new Image
            {
                Source = LoadImage("installer_icon.png"),
                Width = 76,
                Height = 76,
                Margin = new Thickness(0, 0, 18, 0)
            };
            Grid.SetColumn(icon, 0);
            grid.Children.Add(icon);

            var textStack = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
            Grid.SetColumn(textStack, 1);
            grid.Children.Add(textStack);

            textStack.Children.Add(new TextBlock
            {
                Text = "Infinite Fusion PT-BR Mod",
                Foreground = NormalText,
                FontSize = 28,
                FontWeight = FontWeights.Bold
            });
            textStack.Children.Add(new TextBlock
            {
                Text = "Instalador fan-made em Português do Brasil",
                Foreground = MutedText,
                FontSize = 14,
                Margin = new Thickness(0, 2, 0, 12)
            });

            var badges = new StackPanel { Orientation = Orientation.Horizontal };
            badges.Children.Add(CreateBadge("v1.1.5", Blue));
            badges.Children.Add(CreateBadge("Backup obrigatório", BrushFromRgb(191, 135, 0)));
            badges.Children.Add(CreateBadge("Fan-made", Purple));
            badges.Children.Add(CreateBadge("Sem download do jogo", BrushFromRgb(218, 54, 51)));
            badges.Children.Add(CreateBadge("Pokédex futura", GrayButton));
            textStack.Children.Add(badges);
            return card;
        }

        private UIElement CreateWarning()
        {
            var border = new Border
            {
                Background = BrushFromRgb(33, 26, 12),
                BorderBrush = BrushFromRgb(80, 56, 10),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(14),
                Margin = new Thickness(0, 0, 0, 14)
            };
            border.Child = new TextBlock
            {
                Text = "Este instalador não inclui e não baixa o jogo. Baixe Pokemon Infinite Fusion somente pelo Discord oficial do projeto. Depois de atualizar o jogo pelo updater oficial, rode este instalador de novo antes de abrir seu save.",
                TextWrapping = TextWrapping.Wrap,
                Foreground = WarningText,
                FontSize = 13.5
            };
            return border;
        }

        private UIElement CreateActionCard()
        {
            var card = CreateCard();
            card.Padding = new Thickness(18);
            card.Margin = new Thickness(0, 0, 0, 14);

            var stack = new StackPanel();
            card.Child = stack;

            stack.Children.Add(new TextBlock
            {
                Text = "Pasta do jogo",
                Foreground = NormalText,
                FontSize = 17,
                FontWeight = FontWeights.SemiBold
            });
            stack.Children.Add(new TextBlock
            {
                Text = "Selecione o Game.exe. O instalador usa a pasta dele automaticamente e também consegue atualizar uma tradução já instalada.",
                Foreground = MutedText,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 3, 0, 12)
            });

            var pathGrid = new Grid();
            pathGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            pathGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            pathGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            pathGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            stack.Children.Add(pathGrid);

            gameDirBox = new TextBox
            {
                Height = 38,
                Background = BrushFromRgb(13, 17, 23),
                Foreground = NormalText,
                BorderBrush = BorderColor,
                CaretBrush = NormalText,
                FontSize = 14,
                Padding = new Thickness(10, 7, 10, 6),
                VerticalContentAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(gameDirBox, 0);
            pathGrid.Children.Add(gameDirBox);

            chooseButton = CreateButton("Escolher Game.exe", Blue, 0xE8B7);
            chooseButton.Margin = new Thickness(10, 0, 0, 0);
            chooseButton.Click += delegate { ChooseGameExecutable(); };
            Grid.SetColumn(chooseButton, 1);
            pathGrid.Children.Add(chooseButton);

            scanButton = CreateButton("Escanear", GrayButton, 0xE721);
            scanButton.Margin = new Thickness(8, 0, 0, 0);
            scanButton.Click += async delegate
            {
                if (scanCancellation != null)
                {
                    scanCancellation.Cancel();
                    return;
                }
                await ScanForGameFolder();
            };
            Grid.SetColumn(scanButton, 2);
            pathGrid.Children.Add(scanButton);

            validateButton = CreateButton("Validar", GrayButton, 0xE73E);
            validateButton.Margin = new Thickness(8, 0, 0, 0);
            validateButton.Click += delegate { ValidateSelectedFolderWithMessage(); };
            Grid.SetColumn(validateButton, 3);
            pathGrid.Children.Add(validateButton);

            var buttonGrid = new Grid { Margin = new Thickness(0, 16, 0, 0) };
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            stack.Children.Add(buttonGrid);

            installButton = CreateButton("Instalar / Atualizar PT-BR", Green, 0xE896);
            installButton.Click += async delegate { await RunInstall(); };
            Grid.SetColumn(installButton, 0);
            buttonGrid.Children.Add(installButton);

            restoreButton = CreateButton("Desinstalar tradução", Danger, 0xE74D);
            restoreButton.Margin = new Thickness(10, 0, 0, 0);
            restoreButton.Click += async delegate { await RunRestore(); };
            Grid.SetColumn(restoreButton, 1);
            buttonGrid.Children.Add(restoreButton);

            backupButton = CreateButton("Abrir backups", GrayButton, 0xE8A7);
            backupButton.Margin = new Thickness(10, 0, 0, 0);
            backupButton.Click += delegate { OpenBackupFolder(); };
            Grid.SetColumn(backupButton, 2);
            buttonGrid.Children.Add(backupButton);

            var backupNotice = new TextBlock
            {
                Text = "Backup sempre obrigatório em PTBR_BACKUPS dentro da pasta do jogo.",
                Foreground = MutedText,
                Margin = new Thickness(0, 13, 0, 0)
            };
            stack.Children.Add(backupNotice);

            statusTitle = new TextBlock
            {
                Foreground = NormalText,
                FontSize = 15,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(0, 18, 0, 2)
            };
            stack.Children.Add(statusTitle);

            statusDetails = new TextBlock
            {
                Foreground = MutedText,
                TextWrapping = TextWrapping.Wrap
            };
            stack.Children.Add(statusDetails);

            progressBar = new ProgressBar
            {
                Height = 9,
                Minimum = 0,
                Maximum = 100,
                Margin = new Thickness(0, 14, 0, 0)
            };
            stack.Children.Add(progressBar);
            return card;
        }

        private UIElement CreateLogCard()
        {
            var card = CreateCard();
            card.Padding = new Thickness(14);

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            card.Child = grid;

            grid.Children.Add(new TextBlock
            {
                Text = "Registro",
                Foreground = NormalText,
                FontSize = 15,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(0, 0, 0, 8)
            });

            logBox = new TextBox
            {
                IsReadOnly = true,
                AcceptsReturn = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Background = BrushFromRgb(1, 4, 9),
                Foreground = BrushFromRgb(201, 209, 217),
                BorderBrush = BorderColor,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 12.5,
                Padding = new Thickness(10)
            };
            Grid.SetRow(logBox, 1);
            grid.Children.Add(logBox);
            return card;
        }

        private async Task RunInstall()
        {
            string folder;
            try
            {
                folder = ValidateGameDir(gameDirBox.Text);
            }
            catch (Exception ex)
            {
                ShowWarn("Instalação interrompida", ex.Message);
                return;
            }

            var alreadyInstalled = File.Exists(Path.Combine(folder, "Data", "portuguese.dat"));
            var verb = alreadyInstalled ? "atualizar" : "instalar";
            var message = "Vamos " + verb + " a tradução PT-BR em:\n\n" + folder +
                          "\n\nUm backup obrigatório será salvo em PTBR_BACKUPS antes de qualquer alteração.\n\nContinuar?";
            if (MessageBox.Show(this, message, "Instalar / atualizar tradução", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            await RunBackground(alreadyInstalled ? "Atualizando tradução..." : "Instalando tradução...", delegate
            {
                return InstallerCore.Install(folder, true, LogThreadSafe, SetProgressThreadSafe);
            });
        }

        private async Task RunRestore()
        {
            string folder;
            try
            {
                folder = ValidateGameDir(gameDirBox.Text);
            }
            catch (Exception ex)
            {
                ShowWarn("Desinstalação interrompida", ex.Message);
                return;
            }

            var message = "Desinstalar a tradução PT-BR e voltar o jogo para inglês?\n\n" +
                          "O instalador vai restaurar o backup original em PTBR_BACKUPS, remover arquivos criados pela tradução e voltar o idioma salvo para inglês.\n\n" +
                          "Recomendado depois da desinstalação: rode o .bat oficial de atualização do jogo para verificar e reorganizar os arquivos originais antes de jogar.\n\nContinuar?";
            if (MessageBox.Show(this, message, "Desinstalar tradução", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            await RunBackground("Desinstalando tradução...", delegate
            {
                return InstallerCore.RestoreLatest(folder, LogThreadSafe, SetProgressThreadSafe);
            });
        }
        private async Task RunBackground(string title, Func<string> work)
        {
            SetBusy(true);
            progressBar.IsIndeterminate = true;
            SetResult(title, "Aguarde. Em computadores mais antigos isso pode levar alguns minutos.");
            try
            {
                var result = await Task.Run(work);
                progressBar.IsIndeterminate = false;
                progressBar.Value = 100;
                SetResult("Concluído", result.Replace("\n", Environment.NewLine));
                Log("Concluído.");
            }
            catch (Exception ex)
            {
                progressBar.IsIndeterminate = false;
                progressBar.Value = 0;
                SetResult("Erro", ex.Message);
                Log("Erro: " + ex.Message);
                ShowWarn("Erro do instalador", ex.Message);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void ChooseGameExecutable()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Selecione o Game.exe do Pokemon Infinite Fusion";
            dialog.Filter = "Executável do jogo|Game.exe;InfiniteFusion.exe;infinitefusion.exe|Executáveis|*.exe|Todos os arquivos|*.*";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog(this) == true)
            {
                var folder = Path.GetDirectoryName(dialog.FileName);
                gameDirBox.Text = folder;
                ValidateAndUpdateStatus(false);
            }
        }

        private async Task ScanForGameFolder()
        {
            scanCancellation = new CancellationTokenSource();
            SetBusy(true);
            scanButton.IsEnabled = true;
            scanButton.Content = "Cancelar scan";
            progressBar.IsIndeterminate = true;
            SetResult("Escaneando pastas comuns...", "Procurando Game.exe junto de Data\\Scripts. Isso pode levar um pouco.");
            Log("Escaneando pastas comuns em busca do Pokemon Infinite Fusion.");
            try
            {
                var token = scanCancellation.Token;
                var found = await Task.Run(delegate { return FindGameFolders(token); }, token);
                progressBar.IsIndeterminate = false;
                progressBar.Value = 0;
                if (found.Count == 0)
                {
                    SetResult("Nenhuma instalação encontrada", "Use Escolher Game.exe e selecione o executável do jogo manualmente.");
                    Log("Nenhuma pasta válida encontrada pelo escaneamento.");
                    return;
                }
                string selected = found[0];
                if (found.Count > 1)
                {
                    var picker = new DetectedFolderWindow(found);
                    picker.Owner = this;
                    picker.Icon = Icon;
                    if (picker.ShowDialog() == true && !string.IsNullOrEmpty(picker.SelectedFolder))
                    {
                        selected = picker.SelectedFolder;
                    }
                    else
                    {
                        SetResult("Escaneamento concluído", found.Count + " pastas encontradas. Nenhuma foi selecionada.");
                        return;
                    }
                }
                gameDirBox.Text = selected;
                ValidateAndUpdateStatus(false);
                Log("Pasta encontrada: " + selected);
            }
            catch (Exception ex)
            {
                progressBar.IsIndeterminate = false;
                if (ex is OperationCanceledException)
                {
                    SetResult("Escaneamento cancelado", "Você pode escolher o Game.exe manualmente ou tentar escanear de novo.");
                    Log("Escaneamento cancelado pelo usuário.");
                }
                else
                {
                    SetResult("Erro no escaneamento", ex.Message);
                    Log("Erro no escaneamento: " + ex.Message);
                }
            }
            finally
            {
                if (scanCancellation != null)
                {
                    scanCancellation.Dispose();
                    scanCancellation = null;
                }
                scanButton.Content = "Escanear";
                SetBusy(false);
            }
        }

        private List<string> FindGameFolders(CancellationToken token)
        {
            var results = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var roots = BuildSearchRoots();
            foreach (var root in roots)
            {
                token.ThrowIfCancellationRequested();
                ScanRoot(root, results, 5, 22000, token);
                if (results.Count >= 20) break;
            }
            return results.OrderBy(x => x).ToList();
        }

        private List<string> BuildSearchRoots()
        {
            var roots = new List<string>();
            AddRoot(roots, AppDomain.CurrentDomain.BaseDirectory);
            AddRoot(roots, Directory.GetCurrentDirectory());
            AddRoot(roots, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            AddRoot(roots, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"));
            AddRoot(roots, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Fixed && drive.IsReady)
                {
                    AddRoot(roots, drive.RootDirectory.FullName);
                }
            }
            return roots;
        }

        private static void AddRoot(List<string> roots, string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return;
            try
            {
                var full = Path.GetFullPath(path);
                if (Directory.Exists(full) && !roots.Contains(full, StringComparer.OrdinalIgnoreCase))
                {
                    roots.Add(full);
                }
            }
            catch
            {
            }
        }

        private void ScanRoot(string root, HashSet<string> results, int maxDepth, int maxDirectories, CancellationToken token)
        {
            var queue = new Queue<Tuple<string, int>>();
            queue.Enqueue(Tuple.Create(root, 0));
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var scanned = 0;
            while (queue.Count > 0 && scanned < maxDirectories && results.Count < 20)
            {
                token.ThrowIfCancellationRequested();
                var item = queue.Dequeue();
                var dir = item.Item1;
                var depth = item.Item2;
                if (!seen.Add(dir)) continue;
                scanned++;

                if (LooksLikeGameFolder(dir))
                {
                    results.Add(dir);
                    continue;
                }
                if (depth >= maxDepth) continue;
                if (ShouldSkipDirectory(dir)) continue;

                string[] children;
                try
                {
                    children = Directory.GetDirectories(dir);
                }
                catch
                {
                    continue;
                }
                foreach (var child in children)
                {
                    queue.Enqueue(Tuple.Create(child, depth + 1));
                }
            }
        }

        private static bool ShouldSkipDirectory(string dir)
        {
            var name = Path.GetFileName(dir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            if (string.IsNullOrEmpty(name)) return false;
            var skip = new[] {
                "Windows", "Program Files", "Program Files (x86)", "ProgramData", "$Recycle.Bin",
                "System Volume Information", "AppData", "node_modules", ".git", ".cache"
            };
            return skip.Any(x => string.Equals(x, name, StringComparison.OrdinalIgnoreCase));
        }

        private bool LooksLikeGameFolder(string folder)
        {
            return Directory.Exists(Path.Combine(folder, "Data", "Scripts")) &&
                   (File.Exists(Path.Combine(folder, "Game.exe")) ||
                    File.Exists(Path.Combine(folder, "InfiniteFusion.exe")) ||
                    File.Exists(Path.Combine(folder, "infinitefusion.exe")));
        }

        private void ValidateSelectedFolderWithMessage()
        {
            ValidateAndUpdateStatus(true);
        }

        private void ValidateAndUpdateStatus(bool showMessage)
        {
            try
            {
                var folder = ValidateGameDir(gameDirBox.Text);
                var installed = File.Exists(Path.Combine(folder, "Data", "portuguese.dat"));
                var detail = installed
                    ? "Pasta válida. Tradução PT-BR já detectada; o botão Instalar / Atualizar fará backup e atualizará os arquivos."
                    : "Pasta válida. Pronta para instalar a tradução PT-BR com backup obrigatório.";
                SetResult("Pasta validada", detail);
                Log("Pasta validada: " + folder);
                if (showMessage) MessageBox.Show(this, detail, "Validação", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                SetResult("Pasta inválida", ex.Message);
                Log("Validação falhou: " + ex.Message);
                if (showMessage) ShowWarn("Validação falhou", ex.Message);
            }
        }

        private string ValidateGameDir(string rawPath)
        {
            if (string.IsNullOrWhiteSpace(rawPath))
            {
                throw new InvalidOperationException("Escolha a pasta do jogo primeiro.");
            }
            var full = Path.GetFullPath(rawPath);
            if (File.Exists(full))
            {
                full = Path.GetDirectoryName(full);
            }
            if (!Directory.Exists(full))
            {
                throw new InvalidOperationException("A pasta não existe: " + full);
            }
            if (!Directory.Exists(Path.Combine(full, "Data")))
            {
                throw new InvalidOperationException("Essa pasta não parece ser a pasta do jogo: falta a pasta Data.");
            }
            if (!Directory.Exists(Path.Combine(full, "Data", "Scripts")))
            {
                throw new InvalidOperationException("Essa pasta não parece ser a pasta do jogo: falta Data\\Scripts.");
            }
            if (!LooksLikeGameFolder(full))
            {
                Log("Aviso: Game.exe não encontrado, mas Data e Data\\Scripts existem.");
            }
            return full;
        }

        private void OpenBackupFolder()
        {
            try
            {
                var folder = ValidateGameDir(gameDirBox.Text);
                var backupFolder = Path.Combine(folder, "PTBR_BACKUPS");
                Directory.CreateDirectory(backupFolder);
                System.Diagnostics.Process.Start(backupFolder);
            }
            catch (Exception ex)
            {
                ShowWarn("Não consegui abrir backups", ex.Message);
            }
        }

        private void SetBusy(bool busy)
        {
            foreach (var control in actionControls)
            {
                control.IsEnabled = !busy;
            }
        }

        private void SetResult(string title, string details)
        {
            statusTitle.Text = title;
            statusDetails.Text = details;
        }

        private void Log(string message)
        {
            logBox.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + message + Environment.NewLine);
            logBox.ScrollToEnd();
        }

        private void LogThreadSafe(string message)
        {
            Dispatcher.BeginInvoke(new Action(delegate { Log(message); }));
        }

        private void SetProgressThreadSafe(int value)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                progressBar.IsIndeterminate = false;
                progressBar.Value = Math.Max(progressBar.Minimum, Math.Min(progressBar.Maximum, value));
            }));
        }

        private void ShowWarn(string title, string message)
        {
            MessageBox.Show(this, message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private static Border CreateCard()
        {
            return new Border
            {
                Background = CardBackground,
                BorderBrush = BorderColor,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(12)
            };
        }

        private static Border CreateBadge(string text, Brush background)
        {
            return new Border
            {
                Background = background,
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(9, 4, 9, 4),
                Margin = new Thickness(0, 0, 8, 0),
                Child = new TextBlock
                {
                    Text = text,
                    Foreground = Brushes.White,
                    FontSize = 12,
                    FontWeight = FontWeights.Bold
                }
            };
        }

        private static Button CreateButton(string text, Brush background)
        {
            return CreateButton(text, background, 0);
        }

        private static Button CreateButton(string text, Brush background, int iconCode)
        {
            var button = new Button
            {
                Background = background,
                Foreground = Brushes.White,
                BorderBrush = background,
                FontWeight = FontWeights.SemiBold,
                Padding = new Thickness(14, 9, 14, 9),
                MinHeight = 40,
                Style = GetButtonStyle(),
                Cursor = Cursors.Hand
            };
            button.Content = iconCode > 0 ? (object)CreateButtonContent(text, iconCode) : text;
            button.MouseEnter += delegate { button.Opacity = 0.92; };
            button.MouseLeave += delegate { button.Opacity = 1.0; };
            return button;
        }

        private static UIElement CreateButtonContent(string text, int iconCode)
        {
            var stack = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            stack.Children.Add(new TextBlock
            {
                Text = char.ConvertFromUtf32(iconCode),
                FontFamily = new FontFamily("Segoe MDL2 Assets"),
                FontSize = 14,
                Margin = new Thickness(0, 0, 8, 0),
                VerticalAlignment = VerticalAlignment.Center
            });
            stack.Children.Add(new TextBlock
            {
                Text = text,
                FontSize = 13.5,
                VerticalAlignment = VerticalAlignment.Center
            });
            return stack;
        }

        private static Style GetButtonStyle()
        {
            if (cachedButtonStyle != null) return cachedButtonStyle;

            var style = new Style(typeof(Button));
            var template = new ControlTemplate(typeof(Button));
            var border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(9));
            border.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            border.SetBinding(Border.BackgroundProperty, new Binding("Background") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) });
            border.SetBinding(Border.BorderBrushProperty, new Binding("BorderBrush") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) });

            var presenter = new FrameworkElementFactory(typeof(ContentPresenter));
            presenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            presenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            presenter.SetValue(ContentPresenter.RecognizesAccessKeyProperty, true);
            presenter.SetBinding(FrameworkElement.MarginProperty, new Binding("Padding") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) });
            border.AppendChild(presenter);

            template.VisualTree = border;
            style.Setters.Add(new Setter(Button.TemplateProperty, template));
            style.Setters.Add(new Setter(Button.FocusVisualStyleProperty, null));
            style.Setters.Add(new Setter(Button.SnapsToDevicePixelsProperty, true));
            cachedButtonStyle = style;
            return cachedButtonStyle;
        }

        private static SolidColorBrush BrushFromRgb(byte r, byte g, byte b)
        {
            var brush = new SolidColorBrush(Color.FromRgb(r, g, b));
            brush.Freeze();
            return brush;
        }

        private static ImageSource LoadIcon()
        {
            return LoadImage("installer_icon.png");
        }

        private static ImageSource LoadImage(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }
    }

    public class DetectedFolderWindow : Window
    {
        private readonly ListBox listBox;
        public string SelectedFolder { get; private set; }

        public DetectedFolderWindow(IEnumerable<string> folders)
        {
            Title = "Instalações encontradas";
            Width = 720;
            Height = 420;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Background = ModernInstallerWindowBrushes.PageBackground;
            Foreground = ModernInstallerWindowBrushes.NormalText;
            FontFamily = new FontFamily("Segoe UI");

            var root = new Grid { Margin = new Thickness(18) };
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            Content = root;

            root.Children.Add(new TextBlock
            {
                Text = "Escolha a instalação do Pokemon Infinite Fusion",
                Foreground = ModernInstallerWindowBrushes.NormalText,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 12)
            });

            listBox = new ListBox
            {
                ItemsSource = folders.ToList(),
                Background = ModernInstallerWindowBrushes.CardBackground,
                Foreground = ModernInstallerWindowBrushes.NormalText,
                BorderBrush = ModernInstallerWindowBrushes.BorderColor,
                FontSize = 13
            };
            listBox.MouseDoubleClick += delegate { Accept(); };
            Grid.SetRow(listBox, 1);
            root.Children.Add(listBox);

            var buttons = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 14, 0, 0)
            };
            Grid.SetRow(buttons, 2);
            root.Children.Add(buttons);

            var cancel = new Button { Content = "Cancelar", Padding = new Thickness(14, 8, 14, 8), Margin = new Thickness(0, 0, 8, 0) };
            cancel.Click += delegate { DialogResult = false; Close(); };
            buttons.Children.Add(cancel);

            var ok = new Button { Content = "Usar esta pasta", Padding = new Thickness(14, 8, 14, 8), FontWeight = FontWeights.SemiBold };
            ok.Click += delegate { Accept(); };
            buttons.Children.Add(ok);

            if (listBox.Items.Count > 0) listBox.SelectedIndex = 0;
        }

        private void Accept()
        {
            if (listBox.SelectedItem == null) return;
            SelectedFolder = Convert.ToString(listBox.SelectedItem);
            DialogResult = true;
            Close();
        }
    }

    internal static class ModernInstallerWindowBrushes
    {
        public static readonly Brush PageBackground = Create(13, 17, 23);
        public static readonly Brush CardBackground = Create(22, 27, 34);
        public static readonly Brush BorderColor = Create(48, 54, 61);
        public static readonly Brush NormalText = Create(230, 237, 243);

        private static Brush Create(byte r, byte g, byte b)
        {
            var brush = new SolidColorBrush(Color.FromRgb(r, g, b));
            brush.Freeze();
            return brush;
        }
    }
}
