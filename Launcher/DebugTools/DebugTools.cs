using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using Rectangle = System.Windows.Shapes.Rectangle;
using SinRpLauncher.Classes;
using Launcher.Classes;

namespace SinRpLauncher.Debug
{
    /// <summary>
    /// Class for implementation of debug tools for SinRpLauncher
    /// </summary>
    public class DebugTools
    {
        //Rectangle[] dbg_rects = new Rectangle[60];


        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport(@"kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        public static void ShowConsoleWindow()
        {            
            IntPtr handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
            }
            else
            {
                ShowWindow(handle, Opcodes.sw_show);
            }
        }


        public static void HideConsoleWindow(int hideOpCode)
        {
            IntPtr handle = GetConsoleWindow();

            if(handle != IntPtr.Zero)
            {
                ShowWindow(handle, hideOpCode);
            }

            return;
        }


        public static void PrintDebugInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(" |----- SinRP Debug Panel -----|");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            //Console.WriteLine(" Горячие клавиши в ремoнте :). Временно вырезаны.");
            Console.WriteLine(" Left Ctrl + F1\t\t\t - Переключиться на поле ника");
            Console.WriteLine(" Esc\t\t\t\t - Выйти (Работает во всех окнах)");
            Console.WriteLine(" Q\t\t\t\t - Свернуть");
            Console.WriteLine(" F1\t\t\t\t - Настройки");
            Console.WriteLine(" F2\t\t\t\t - Профили");
            Console.WriteLine(" F3\t\t\t\t - Переход в Личный кабинет");
            Console.WriteLine(" F4\t\t\t\t - Переход на форум");
            Console.WriteLine(" F5\t\t\t\t - Переход в тех-поддержку");
            Console.WriteLine(" F6\t\t\t\t - Переход в Discord");
            Console.WriteLine(" F7\t\t\t\t - Переход в YouTube");
            Console.WriteLine(" F8\t\t\t\t - Переход в vk");
            Console.WriteLine(" L.Shift + G\t\t\t - Перейти в игру");
            Console.WriteLine(" L.Shift + R\t\t\t - Обновить сведения об интернете и обновлениях");
            Console.WriteLine(" L.Ctrl + D\t\t\t - Убрать/показать имя билда лаунчера");
            Console.WriteLine(" L.Shift + D\t\t\t - актив./деактив. Режим администратора");
            Console.WriteLine(" стрелка влево\t\t\t - Сменить новость.");
            Console.WriteLine(" стрелка вправо\t\t\t - Сменить новость.");
            Console.WriteLine(" R.Shift + D\t\t\t - Выключить дебаг мод(до перезагрузки).");

            Console.Write("\n\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" !-- Что-бы экстренно(мгновенно) выключить лаунчер, просто закройте эту Debug панель --!");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [!] Однако лаунчер может остаться в оперативной памяти (редко) [!] ");
            Console.ForegroundColor = ConsoleColor.White;
        }


        /// <summary>
        /// Switch build version text to visibility
        /// </summary>
        /// <param name="w"></param>
        public static void SwitchBuildText(Label b)
        {
            if (!Opcodes.proj_label_showed)
            {
                b.Visibility = Visibility.Visible;
                Opcodes.proj_label_showed = true;
            }
            else
            {
                b.Visibility = Visibility.Hidden;
                Opcodes.proj_label_showed = false;
            }
        }


        public static void ToggleElement(FrameworkElement element, int Opcode)
        {
            if(Opcode == Opcodes.elem_show)
                element.Visibility = Visibility.Visible;
            else if(Opcode == Opcodes.elem_hide)
                element.Visibility = Visibility.Hidden;

            return;
        }


        /// <summary>
        /// Turn off debug mode if needed
        /// </summary>
        /// <param name="Opcode"></param>
        public static void OffDebugMode(int Opcode)
        {
            if(Opcode == Opcodes.off_debug_mode)
            {
                InfoClass.IsDebugMode = false;
                HideConsoleWindow(Opcodes.sw_hide);
            }
        }


        /// <summary>
        /// Switches property 'admin mode'
        /// </summary>
        /// <returns>Is admin version statement</returns>
        public static bool SwitchAdminMode()
        {
            if (!Opcodes.adm_ver_on)
                Opcodes.adm_ver_on = true;
            else
                Opcodes.adm_ver_on = false;

            return Opcodes.adm_ver_on;
        }
    }
}
