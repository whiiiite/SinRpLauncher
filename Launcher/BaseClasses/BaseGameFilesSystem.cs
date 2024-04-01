using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Launcher.BaseClasses
{
    /// <summary>
    /// Base class for contain some helpful info. Like files and directories of a game
    /// </summary>
    public class BaseGameFilesSystem : IDisposable
    {

        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        static private string[] GetDirs(string gamePath)
        {
            string[] dirs = new string[]
            {
                gamePath,
                gamePath + "\\anim",
                gamePath + "\\audio",
                gamePath + "\\data",
                gamePath + "\\ImVehFt",
                gamePath + "\\models",
                gamePath + "\\movies",
                gamePath + "\\scripts",
                gamePath + "\\text",
                gamePath + "\\audio\\CONFIG",
                gamePath + "\\audio\\SFX",
                gamePath + "\\audio\\streams",
                gamePath + "\\data\\Decision",
                gamePath + "\\data\\Icons",
                gamePath + "\\data\\maps",
                gamePath + "\\data\\Paths",
                gamePath + "\\data\\script",
                gamePath + "\\ImVehFt\\colors",
                gamePath + "\\ImVehFt\\effect",
                gamePath + "\\ImVehFt\\eml",
                gamePath + "\\ImVehFt\\grunge",
                gamePath + "\\ImVehFt\\plates",
                gamePath + "\\ImVehFt\\shadows",
                gamePath + "\\ImVehFt\\tyres",
                gamePath + "\\models\\coll",
                gamePath + "\\models\\generic",
                gamePath + "\\models\\grass",
                gamePath + "\\models\\txd",
            };

            return dirs;
        }


        static private string[] GetFiles(string gamePath)
        {
            string[] files = new string[] 
            {
                gamePath + "\\1c.url",
                gamePath + "\\eax.dll",
                gamePath + "\\GameuxInstallHelper.dll",
                gamePath + "\\gta-sa.url",
                gamePath + "\\GTASAGDF.dll",
                gamePath + "\\gta_sa.exe",
                gamePath + "\\GTA_SA_PC_MAP_RU.pdf",
                gamePath + "\\ImVehFt.asi",
                gamePath + "\\ImVehFt.log",
                gamePath + "\\licence.txt",
                gamePath + "\\ogg.dll",
                gamePath + "\\Readme.txt",
                gamePath + "\\Rockstar.url",
                gamePath + "\\stream.ini",
                gamePath + "\\StreamMemFix2.2_test1.asi",
                gamePath + "\\unins000.dat",
                gamePath + "\\unins000.exe",
                gamePath + "\\vorbis.dll",
                gamePath + "\\vorbisFile.dll",
                gamePath + "\\vorbisHooked.dll",
                gamePath + "\\anim\\anim.img",
                gamePath + "\\anim\\cuts.img",
                gamePath + "\\anim\\ped.ifp",
                gamePath + "\\data\\animgrp.dat",
                gamePath + "\\data\\animviewer.dat",
                gamePath + "\\data\\ar_stats.dat",
                gamePath + "\\data\\AudioEvents.txt",
                gamePath + "\\data\\carcols.dat",
                gamePath + "\\data\\cargrp.dat",
                gamePath + "\\data\\carmods.dat",
                gamePath + "\\data\\clothes.dat",
                gamePath + "\\data\\default.dat",
                gamePath + "\\data\\default.ide",
                gamePath + "\\data\\fonts.dat",
                gamePath + "\\data\\furnitur.dat",
                gamePath + "\\data\\gridref.dat",
                gamePath + "\\data\\gta.dat",
                gamePath + "\\data\\gta_quick.dat",
                gamePath + "\\data\\handling.cfg",
                gamePath + "\\data\\info.zon",
                gamePath + "\\data\\main.sc",
                gamePath + "\\data\\map.zon",
                gamePath + "\\data\\melee.dat",
                gamePath + "\\data\\numplate.dat",
                gamePath + "\\data\\object.dat",
                gamePath + "\\data\\ped.dat",
                gamePath + "\\data\\pedgrp.dat",
                gamePath + "\\data\\peds.ide",
                gamePath + "\\data\\pedstats.dat",
                gamePath + "\\data\\plants.dat",
                gamePath + "\\data\\polydensity.dat",
                gamePath + "\\data\\popcycle.dat",
                gamePath + "\\data\\procobj.dat",
                gamePath + "\\data\\shopping.dat",
                gamePath + "\\data\\statdisp.dat",
                gamePath + "\\data\\surface.dat",
                gamePath + "\\data\\surfaud.dat",
                gamePath + "\\data\\surfinfo.dat",
                gamePath + "\\data\\timecyc.dat",
                gamePath + "\\data\\timecycp.dat",
                gamePath + "\\data\\txdcut.ide",
                gamePath + "\\data\\vehicles.ide",
                gamePath + "\\data\\water.dat",
                gamePath + "\\data\\water1.dat",
                gamePath + "\\data\\weapon.dat",
                gamePath + "\\ImVehFt\\ImVehFt.ini",
                gamePath + "\\models\\cutscene.img",
                gamePath + "\\models\\effects.fxp",
                gamePath + "\\models\\effectsPC.txd",
                gamePath + "\\models\\fonts.txd",
                gamePath + "\\models\\fronten1.txd",
                gamePath + "\\models\\fronten2.txd",
                gamePath + "\\models\\fronten3.txd",
                gamePath + "\\models\\fronten_pc.txd",
                gamePath + "\\models\\gta3.img",
                gamePath + "\\models\\gta_int.img",
                gamePath + "\\models\\hud.txd",
                gamePath + "\\models\\map1.img",
                gamePath + "\\models\\misc.txd",
                gamePath + "\\models\\particle.txd",
                gamePath + "\\models\\pcbtns.txd",
                gamePath + "\\models\\player.img",
                gamePath + "\\models\\transport.img",
                gamePath + "\\movies\\GTAtitles.mpg",
                gamePath + "\\movies\\Logo.mpg",
                gamePath + "\\ReadMe\\Readme.txt",
                gamePath + "\\scripts\\GTASA.WidescreenFix.asi",
                gamePath + "\\scripts\\GTASA.WidescreenFix.ini",
                gamePath + "\\text\\american.gxt",

            };

            return files;
        }


        protected string[] files
        {
            get { return GetFiles(Classes.GameFilesSystem.GameDir); }
        }


        protected string[] directories
        {
            get { return GetDirs(Classes.GameFilesSystem.GameDir); }
        }


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle.Dispose();
                }

                _disposedValue = true;
            }
        }
    }
}
