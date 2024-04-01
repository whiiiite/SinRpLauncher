using Launcher;
using SinRpLauncher.Classes;
using SinRpLauncher.DebugTools;
using SinRpLauncher.Extentions;
using SinRpLauncher.PetternsInterfaces;
using SinRpLauncher.Util;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SinRpLauncher.Debug
{
    /// <summary>
    /// Interaction logic for DeveloperConsole.xaml
    /// </summary>
    public partial class DeveloperConsole : Window
    {
        CommandPromptCaretaker _saver;

        private MainWindow MainWnd;

        public DeveloperConsole(MainWindow mw)
        {
            InitializeComponent();
            MainWnd = mw;
            WriteLine("Введите help что бы вывести информацию о командах.");
            _saver = new CommandPromptCaretaker(this);
        }


        private bool FindCommand(string command)
        {
            return DevConsoleCommands.Commands.ContainsValue(command);
        }


        private bool ArgsIsCorrect(params object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null)
                    return false;
            }
            return true;
        }


        private void HandleCommand(string command, params string[] args)
        {
            try
            {
                HandleCommand_R(command, args);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns>Result of command handling, 0 if success, 1 is failed</returns>
        private void HandleCommand_R(string command, params string[] args)
        {
            command = command.Trim();
            if (!FindCommand(command))
                throw new ArgumentException("Command was not found");

            if (args.Length <= 0)
                throw new ArgumentException("Args was not passed");

            if (!ArgsIsCorrect(args))
                throw new ArgumentException("Args is not correct");

            const string ALL_OBJS = "all";
            const string pathFlag = "--path";

            string obj;
            switch (command)
            {
                case DevConsoleCommands.DELETE_CMD:
                    if (args.Length < 3)
                        throw new ArgumentException("Args is too few");

                    obj = args[1];
                    int grid = Convert.ToInt32(args[2]);
                    RemoveControlByGrid(grid, obj);
                    break;

                case DevConsoleCommands.HIDE_CMD:
                    obj = args[1];
                    if (obj == ALL_OBJS)
                        SwitchAllControls(isShow: false);

                    DevConsoleCommands.Controls(MainWnd)[obj].Visibility = Visibility.Hidden;
                    break;

                case DevConsoleCommands.SHOW_CMD:
                    obj = args[1];
                    if (obj == ALL_OBJS)
                        SwitchAllControls(isShow: true);

                    DevConsoleCommands.Controls(MainWnd)[obj].Visibility = Visibility.Visible;
                    break;

                case DevConsoleCommands.SHUTDOWN_CMD:
                    Application.Current.Shutdown();
                    break;

                case DevConsoleCommands.CHANGETORRENT_CMD:
                    string actualFlag = args[1];
                    if (actualFlag != pathFlag)
                        throw new ArgumentException("Path flag is not correct, it needs to be --path");

                    string pathToTorrent = args[2];
                    if (!File.Exists(pathToTorrent) && !pathToTorrent.IsMagnetLink())
                        throw new ArgumentException("Torrent file doesnt exists and is not magnet link");

                    PathRoots.TorrentToDownload = pathToTorrent;
                    break;

                case DevConsoleCommands.HELP_CMD:
                    PrintHelpMenu();
                    break;

                case DevConsoleCommands.OPEN_VIM:
                    throw new ArgumentException("Не открывай. А то не закроешь.");
            }
        }


        private void TopBarGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            SendCommandButton_Handler();
        }


        private void CommandPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SendCommandButton_Handler();
            }

            try
            {
                if (e.Key == Key.LeftShift)
                {
                    _saver.UndoStateUp();
                    SetPromptToEnd();
                }
                if (e.Key == Key.LeftCtrl)
                {
                    _saver.UndoStateDown();
                    SetPromptToEnd();
                }
            }
            catch (IndexOutOfRangeException)
            { }
            catch(Exception) 
            { }
        }


        /// <summary>
        /// Save the command prompt state
        /// </summary>
        /// <returns>Snapshot of state</returns>
        public IMemento SavePromptState()
        {
            return new CommandPromptMemento(CommandPrompt.Text);
        }


        /// <summary>
        /// Restore the previous states of command prompt
        /// </summary>
        /// <param name="memento"></param>
        /// <exception cref="Exception"></exception>
        public void RestorePromptState(IMemento memento)
        {
            if (!(memento is CommandPromptMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            CommandPrompt.Text = memento.GetState();
        }


        private void SendCommandButton_Handler()
        {
            _saver.BackUpState();

            string commandStr = DateTime.Now.ToString();
            if (string.IsNullOrWhiteSpace(CommandPrompt.Text))
            {
                WriteLine(commandStr + ": ]");
                return;
            }

            commandStr += ": " + CommandPrompt.Text; // make full str to show
            WriteLine(commandStr); // write it 

            CommandPrompt.Text = CommandPrompt.Text.Replace('\t', ' '); // normilize the string

            string[] args = GetArgsFromPrompt(CommandPrompt.Text);
            try
            {
                HandleCommand(args[0], args);
            }
            catch (Exception e)
            {
                commandStr = "std_io_err_x64_86: " + e.Message;
            }
            WriteLine(commandStr);
            ScrollLogToBottom();
            ClearPrompt();
            SetPromptToEnd();
        }


        private void WriteLine(string str = "")
        {
            ConsoleLog.Items.Add(str);
        }


        private string[] GetArgsFromPrompt(string promptText)
        {
            return promptText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }


        private void ScrollLogToBottom()
        {
            if (VisualTreeHelper.GetChildrenCount(ConsoleLog) > 0)
            {
                Border border = (Border)VisualTreeHelper.GetChild(ConsoleLog, 0);
                ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }
        }


        private void PrintHelpMenu()
        {
            WriteLine("Commands: ");
            WriteLine("delete arg0 arg1 - удалить контрол - arg0 control name / arg1 grid number");
            WriteLine("hide arg0 - скрыть контрол - arg0 control name");
            WriteLine("show arg0 - показать контрол - arg0 control name");
            WriteLine("chtr --path - Изменить торрент загрузки. Флаг --path указать путь к нему ИЛИ магнет ссылку");
            WriteLine("shutdown - выключение приложения");
            WriteLine("vim - открыть VIM");

            WriteLine();
            WriteLine("Grids: ");
            WriteLine("0 - MainGrid - главный контейнер");
            WriteLine("1 - BottomGrid - нижний контейнер");
            WriteLine("2 - TopGrid - Верхний контейнер");
            WriteLine("3 - NavGrid - Навигационный контейнер");

            WriteLine();
            WriteLine("Controls: ");
            foreach (var control in DevConsoleCommands.Controls(MainWnd))
            {
                WriteLine(control.Key);
            }
        }


        private void SwitchAllControls(bool isShow)
        {
            foreach (var control in DevConsoleCommands.Controls(MainWnd))
            {
                if(isShow)
                    control.Value.Visibility = Visibility.Visible;
                else
                    control.Value.Visibility = Visibility.Hidden;
            }
        }


        private void RemoveControlByGrid(int gridNumber, string controlName)
        {
            if (gridNumber == MainWindowGrids.MainGrid)
            {
                MainWnd.MainGrid.Children.Remove(DevConsoleCommands.Controls(MainWnd)[controlName]);
            }
            else if (gridNumber == MainWindowGrids.BottomGrid)
            {
                MainWnd.BottomBarGrid.Children.Remove(DevConsoleCommands.Controls(MainWnd)[controlName]);
            }
            else if (gridNumber == MainWindowGrids.TopGrid)
            {
                MainWnd.TopBarGrid.Children.Remove(DevConsoleCommands.Controls(MainWnd)[controlName]);
            }
            else if (gridNumber == MainWindowGrids.NavGrid)
            {
                MainWnd.NavBarGrid.Children.Remove(DevConsoleCommands.Controls(MainWnd)[controlName]);
            }
        }


        private void ClearPrompt()
        {
            CommandPrompt.Text = string.Empty;
        }


        private void SetPromptToEnd()
        {
            CommandPrompt.CaretIndex = CommandPrompt.Text.Length;
        }
    }
}
