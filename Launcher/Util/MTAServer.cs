using ED;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using SinRpLauncher.Classes;
using SinRpLauncher.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows;

namespace Launcher.Classes
{
    public class MTAServer : BaseClasses.MainBaseClass
    {
        private const string WebFormat = "mtasa://";
        public string IP { get; set; }
        public uint Port { get; set; }

        public MTAServer(string IP, uint Port) 
        {
            this.IP = IP;
            this.Port = Port;
        }

        public MTAServer() { }


        /// <summary>
        /// Constructs url for enter to server by cmd argument
        /// <para>Need initialized IP address and port fields</para>
        /// </summary>
        /// <returns>
        /// IP of the MTA server for enter to it.
        /// </returns>
        public string GetIPEnterServer()
        {
            IP ??= "127.0.0.1"; // if ip == null then set it to 127.0.0.1
            if(Port == default) 
                Port = 22003;

            return (WebFormat.Trim() + IP.Trim() + ":" + Port.ToString().Trim()).Trim();
        }


        /// <returns>
        /// IP of the MTA server for enter to it.
        /// </returns>
        public string GetIPEnterServer(string ip, uint port)
        {
            return (WebFormat.Trim() + ip.Trim() + ":" + port.ToString().Trim()).Trim();
        }


        /// <summary>
        /// Set specified registers to HKEY LOCAL MACHINE. Only for <b>Windows OS</b>
        /// </summary>
        public void SetRegistersForServer()
        {
            RegistryKey rkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node", true).CreateSubKey("Multi Theft Auto: Sin All");
            RegistryKey rCommon = rkey.CreateSubKey("Common");
            rCommon.SetValue("File Cache Path", GameFilesSystem.GameDir + '\\' + GameFilesSystem.MTADir + '\\' + "mods" + '\\' + "deathmatch");
            rCommon.SetValue("GTA:SA Path", GameFilesSystem.GameDir);

            rkey = rkey.CreateSubKey("1.5");
            rkey.SetValue("Installer Language", "1049");
            rkey.SetValue("Last Install Location", " ");
            rkey.SetValue("Last Run Location", " ");
            rkey.SetValue("Last Run Path", " ");
            rkey.SetValue("Last Run Path Hash", " ");
            rkey.SetValue("Last Run Path Version", "1.5");
            rkey.SetValue("OnQuitCommand", " ");
            rkey.SetValue("OnRestartCommand", "1.5.9-9.21476");
            rkey.SetValue("PostUpdateConnect", "host=&time=1673732794");

            rkey = rkey.CreateSubKey("Settings");

            RegistryKey rkeyDiagnostics = rkey.CreateSubKey("diagnostics");
            rkeyDiagnostics.SetValue("bsod-detect-skip", "0");
            rkeyDiagnostics.SetValue("crash-data", " ");
            rkeyDiagnostics.SetValue("crash-data1", " ");
            rkeyDiagnostics.SetValue("crash-data2", " ");
            rkeyDiagnostics.SetValue("createdevice-last-status", "3");
            rkeyDiagnostics.SetValue("d3dlegacy", "381");
            rkeyDiagnostics.SetValue("d3dver", "1");
            rkeyDiagnostics.SetValue("debug-setting", "none");
            rkeyDiagnostics.SetValue("game-begin-time", " ");
            rkeyDiagnostics.SetValue("game-connect-time", "0");
            rkeyDiagnostics.SetValue("gta-fopen-fail", " ");
            rkeyDiagnostics.SetValue("gta-fopen-last", " ");
            rkeyDiagnostics.SetValue("gta-model-fail", " ");
            rkeyDiagnostics.SetValue("gta-upgrade-fail", " ");
            rkeyDiagnostics.SetValue("img-file-corruptt", " ");
            rkeyDiagnostics.SetValue("last-crash-info", "Version = 1.5.9-release-21476.0.000\r\nTime = Wed Jan 11 22:56:51 2023\r\nModule = D:\\mta1\\Sin Role Play\\MTA\\mta\\game_sa.dll\r\nCode = 0xC0000005\r\nOffset = 0x0000FA5E\r\n\r\nEAX=00AB4E94  EBX=000000A2  ECX=0880B020  EDX=00000000  ESI=0880B020\r\nEDI=00533560  EBP=0177FCD8  ESP=0177FC94  EIP=0575FA5E  FLG=00210216\r\nCS=0023   DS=002B  SS=002B  ES=002B   FS=0053  GS=002B\r\n\r\n");
            rkeyDiagnostics.SetValue("last-crash-reason", " ");
            rkeyDiagnostics.SetValue("last-dump-extra", "added-anim-task");
            rkeyDiagnostics.SetValue("last-dump-save", "");
            rkeyDiagnostics.SetValue("preloading-upgrades-hiscore", "1194");
            rkeyDiagnostics.SetValue("send-dumps", "yes");

            RegistryKey rkeyGeneral = rkey.CreateSubKey("general");
            rkeyGeneral.SetValue("admin2user_comms", "_arg_0=mtasa://51.77.68.54:2020&_argc=1&_pc_label=appcompat_end:&_pc_offset=2");
            rkeyGeneral.SetValue("aero-changeable", "1");
            rkeyGeneral.SetValue("aero-enabled", "1");
            rkeyGeneral.SetValue("bsod-markers", @"\u0002#00#00#00#00#00#00#00dne#00");
            rkeyGeneral.SetValue("cachechecksum", "58F23CBB5B800F22:D96449162CG:9G349F1F1FFDG8F6864ACEF092A4CE706E6");
            rkeyGeneral.SetValue("customized-sa-files-request", "0");
            rkeyGeneral.SetValue("customized-sa-files-show", "1");
            rkeyGeneral.SetValue("customized-sa-files-using", "0");
            rkeyGeneral.SetValue("debugfilter", "-all,+{500~2000},+5412,+5413,+6311,+6450,+6451,+7123,+7124,+8542,-7711,-9423,-9423");
            rkeyGeneral.SetValue("device-selection-disabled", "0");
            rkeyGeneral.SetValue("gta-exe-md5", "170B3A9108687B26DA2D8901C6948A18");
            rkeyGeneral.SetValue("is-admin", "1");
            rkeyGeneral.SetValue("last-server-ip", "1");
            rkeyGeneral.SetValue("last-server-port", "22052");
            rkeyGeneral.SetValue("last-server-time", "1673470605");
            rkeyGeneral.SetValue("locale", "ru");
            rkeyGeneral.SetValue("mta-version-ext", "1.5.9-9.21476.0.000");
            rkeyGeneral.SetValue("news-install", " ");
            rkeyGeneral.SetValue("news-updated", "1");
            rkeyGeneral.SetValue("no-cycle-event-log", "0");
            rkeyGeneral.SetValue("os-version", "6.2");
            rkeyGeneral.SetValue("pending-browse-to-solution", " ");
            rkeyGeneral.SetValue("real-os-build", "17763");
            rkeyGeneral.SetValue("real-os-version", "10.0");
            rkeyGeneral.SetValue("reportsettings", "filter2@+all,-{1000~2007},-2050,-2051,-{3211},-{4002},-4023,-5132,-5133,-7011,-7043,-7411,-7420,-7601,-7744,-7745,-7801,-{7832~7833},-{7842~7845},-7940,-8070,-8613,-7711,-9423;max@88001;min@11");
            rkeyGeneral.SetValue("reset-settings-revision", "21486");
            rkeyGeneral.SetValue("serial", "9C85338051BF98F238E0E0EECF7E5753");
            rkeyGeneral.SetValue("times-connected", "10");
            rkeyGeneral.SetValue("trouble-url", "http://updatesa.multitheftauto.com/sa/trouble/?v=%VERSION%&id=%ID%&tr=%TROUBLE%");
            rkeyGeneral.SetValue("Win8Color16", "0");
            rkeyGeneral.SetValue("Win8MouseFix", "0");

            RegistryKey rkeyNvhacks = rkey.CreateSubKey("nvhacks");
            rkeyNvhacks.SetValue("nvidia", "0");
            rkeyNvhacks.SetValue("optimus", "1");
            rkeyNvhacks.SetValue("optimus-alt-startup", "0");
            rkeyNvhacks.SetValue("optimus-export-enablement", "1");
            rkeyNvhacks.SetValue("optimus-force-windowed", "0");
            rkeyNvhacks.SetValue("optimus-remember-option", "1");
            rkeyNvhacks.SetValue("optimus-rename-exe", "0");
            rkeyNvhacks.SetValue("optimus-startup-option", "0");

            RegistryKey rkeyWatchdog = rkey.CreateSubKey("watchdog");
            rkeyWatchdog.SetValue("CR1", "0");
            rkeyWatchdog.SetValue("CR2", "0");
            rkeyWatchdog.SetValue("CR3", "0");
            rkeyWatchdog.SetValue("L0", "0");
            rkeyWatchdog.SetValue("L1", "1");
            rkeyWatchdog.SetValue("L2", "0");
            rkeyWatchdog.SetValue("L3", "0");
            rkeyWatchdog.SetValue("L4", "0");
            rkeyWatchdog.SetValue("L5", "0");
            rkeyWatchdog.SetValue("lastruncrash", "0");
            rkeyWatchdog.SetValue("Q0", "0");
            rkeyWatchdog.SetValue("uncleanstop", "0");
        }


        /// <summary>
        /// Parse response from http server for get info about players on MTA server 
        /// </summary>
        /// <param name="indexServerInXml"></param>
        /// <returns>Int32 Array with data about players server. 0 - current players, 1 - all players</returns>
        public (int current, int max) GetPlayersData(int indexServerInXml)
        {
            try
            {
                string pathXml = InfoClass.CurrentDir + '\\' + PathRoots.DataDirectory + '\\' + PathRoots.MainDataFile;
                (string url, string maxp) = HttpHelper.GetHttpServerData(pathXml, indexServerInXml);  // get http from xml
                string httpUrl = url + HttpHelper.adminNode + '/' + HttpHelper.aboutHtmPage; // build url 

                // get string with data
                string resp = HttpHelper.GetReponseStringFromServer(httpUrl);

                // process of parsing players count
                resp = StringHelper.StripHtml(resp).Trim(); // replace all html tags
                string[] split_resp = resp.Split(',');      // split by comma
                resp = split_resp[0];                       // first index is players array
                resp += '}';                                // add '}' to end
                resp = resp.Remove(0, 1);                   // replace first '[' char
                resp = resp.Trim();                         // Trim end and start

                // deserialize to map table string and list(array) of players
                Dictionary<string, List<string>> players = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(resp);
                int[] data = new int[2];
                data[0] = players["players"].Count;
                data[1] = Convert.ToInt32(maxp);
                return (data[0], data[1]);
            }
            catch(Exception)
            {
                throw; // just trust me!
            }
        }
    }
}
