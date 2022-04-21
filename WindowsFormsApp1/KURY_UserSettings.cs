using System;
using System.Collections.Generic;
using System.Configuration;
using TimSpik;

namespace WindowsFormsApp1 {
    public class KURYUserSettings : ApplicationSettingsBase{

        [UserScopedSetting()]
        [DefaultSettingValue("00000000-0000-0000-0000-000000000000")]
        public Guid defaultGUID {
            get {
                return ((Guid)this["defaultGUID"]);
            }
            set {
                this["defaultGUID"] = (Guid)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("0")]
        public int defaultInputDevice {
            get {
                return ((int)this["defaultInputDevice"]);
            }
            set {
                this["defaultInputDevice"] = (int)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("User")]
        public string Nick {
            get {
                return ((string)this["Nick"]);
            }
            set {
                this["Nick"] = (string)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Default")]
        public string SoundPack {
            get {
                return ((string)this["SoundPack"]);
            }
            set {
                this["SoundPack"] = (string)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("1")]
        public float VolumeMic {
            get {
                return ((float)this["VolumeMic"]);
            }
            set {
                this["VolumeMic"] = (float)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("127.0.0.1")]
        public string ipAddr {
            get {
                return ((string)this["ipAddr"]);
            }
            set {
                this["ipAddr"] = (string)value;
            }
        }
    }
}
