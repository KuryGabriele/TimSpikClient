using System;
using System.Configuration;

namespace WindowsFormsApp1 {
    public class VBANUserSettings : ApplicationSettingsBase{

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
        [DefaultSettingValue("1")]
        public float Volume {
            get {
                return ((float)this["Volume"]);
            }
            set {
                this["Volume"] = (float)value;
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
    }
}
