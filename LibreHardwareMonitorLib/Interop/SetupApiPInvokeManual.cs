// Manual P/Invoke definitions for Windows Setup API.
// CsWin32 v0.3.275 may fail to generate these on certain SDK/CI configurations.

using System;
using System.Runtime.InteropServices;
using Windows.Win32.Devices.DeviceAndDriverInstallation;
using Windows.Win32.Foundation;

namespace Windows.Win32.PInvoke
{
    internal static partial class PInvoke
    {
        [DllImport("SETUPAPI.dll", SetLastError = true, ExactSpelling = true)]
        public static extern unsafe BOOL SetupDiEnumDeviceInterfaces(
            HDEVINFO DeviceInfoSet,
            SP_DEVINFO_DATA* DeviceInfoData,
            Guid* InterfaceClassGuid,
            uint MemberIndex,
            SP_DEVICE_INTERFACE_DATA* DeviceInterfaceData);

        [DllImport("SETUPAPI.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL SetupDiGetDeviceInterfaceDetail(
            HDEVINFO DeviceInfoSet,
            SP_DEVICE_INTERFACE_DATA* DeviceInterfaceData,
            SP_DEVICE_INTERFACE_DETAIL_DATA_W* DeviceInterfaceDetailData,
            uint DeviceInterfaceDetailDataSize,
            uint* RequiredSize,
            SP_DEVINFO_DATA* DeviceInfoData);

        [DllImport("SETUPAPI.dll", SetLastError = true, ExactSpelling = true)]
        public static extern unsafe BOOL SetupDiEnumDeviceInfo(
            HDEVINFO DeviceInfoSet,
            uint MemberIndex,
            SP_DEVINFO_DATA* DeviceInfoData);

        [DllImport("SETUPAPI.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern unsafe BOOL SetupDiGetDeviceRegistryProperty(
            HDEVINFO DeviceInfoSet,
            SP_DEVINFO_DATA* DeviceInfoData,
            SETUP_DI_REGISTRY_PROPERTY Property,
            REG_DATA_TYPE* PropertyRegDataType,
            byte* PropertyBuffer,
            uint PropertyBufferSize,
            uint* RequiredSize);

        [DllImport("SETUPAPI.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern HDEVINFO SetupDiGetClassDevs(
            Guid* ClassGuid,
            [Optional] char* Enumerator,
            HWND hwndParent,
            SETUP_DI_GET_CLASS_DEVS_FLAGS Flags);

        [DllImport("SETUPAPI.dll", SetLastError = true, ExactSpelling = true)]
        public static extern BOOL SetupDiDestroyDeviceInfoList(HDEVINFO DeviceInfoSet);
    }

    internal enum SETUP_DI_REGISTRY_PROPERTY : uint
    {
        SPDRP_DEVICEDESC = 0x00000000,
        SPDRP_HARDWAREID = 0x00000001,
        SPDRP_COMPATIBLEIDS = 0x00000002,
        SPDRP_UNUSED0 = 0x00000003,
        SPDRP_SERVICE = 0x00000004,
        SPDRP_UNUSED1 = 0x00000005,
        SPDRP_UNUSED2 = 0x00000006,
        SPDRP_CLASS = 0x00000007,
        SPDRP_CLASSGUID = 0x00000008,
        SPDRP_DRIVER = 0x00000009,
        SPDRP_MFG = 0x0000000A,
        SPDRP_FRIENDLYNAME = 0x0000000B,
        SPDRP_LOCATION_INFORMATION = 0x0000000C,
        SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E,
        SPDRP_CAPABILITIES = 0x0000000F,
        SPDRP_UI_NUMBER = 0x00000010,
        SPDRP_UPPERFILTERS = 0x00000011,
    }

    internal enum REG_DATA_TYPE : uint
    {
        REG_NONE = 0,
        REG_SZ = 1,
        REG_EXPAND_SZ = 2,
        REG_BINARY = 3,
        REG_DWORD = 4,
        REG_DWORD_LITTLE_ENDIAN = 4,
        REG_DWORD_BIG_ENDIAN = 5,
        REG_LINK = 6,
        REG_MULTI_SZ = 7,
    }
}
