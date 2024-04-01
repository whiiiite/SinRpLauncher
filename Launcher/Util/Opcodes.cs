namespace SinRpLauncher.Classes
{
    /// <summary>
    /// Constains useful codes for identify operations
    /// </summary>
    public static class Opcodes
    {
        public const int sw_hide = 0x0;
        public const int sw_show = 0x5;

        public const int elem_hide = 1552;
        public const int elem_show = 1553;

        private static bool _proj_label_showed = false;
        private static bool _adm_ver_on = false;

        public static bool proj_label_showed
        {
            get { return _proj_label_showed; }
            set { _proj_label_showed = value; }
        }

        public static bool adm_ver_on
        {
            get { return _adm_ver_on; }
            set { _adm_ver_on = value; } 
        }

        public const int off_debug_mode = 1442;

        public const int RefreshLauncher        = 1111;
        public const int ToGame                 = 1112;
        public const int CloseLauncher          = 1113;
        public const int MinimizeLauncher       = 1114;
        public const int ToSettingsLauncher     = 1115;
        public const int ToProfiles             = 1116;
        public const int ToPresonalCabinetURL   = 101;
        public const int ToForumURL             = 102;
        public const int ToTechSupportURL       = 103;
        public const int ToDiscordURL           = 104;
        public const int ToYouTubeURL           = 105;
        public const int ToVkURL                = 106;
        public const int ChangeNewsRight        = 1901;
        public const int ChangeNewsLeft         = 1902;
        public const int FocusNickNameBox       = 1811;

        public const int UpdateLauncher = 333;
        public const int DownloadGame   = 334;

        public const string DevCmdCallStr = "__spawn_devcmd";
    }
}
