// Manual P/Invoke definitions for Windows Setup API.
// CsWin32 v0.3.275 fails to generate these on CI environment.

using System;
using System.Runtime.InteropServices;
using Windows.Win32.Devices.DeviceAndDriverInstallation;
using Windows.Win32.Foundation;

namespace LibreHardwareMonitor.Interop
{
    internal static class SetupApi
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
            uint Property,
            uint* PropertyRegDataType,
            byte* PropertyBuffer,
            uint PropertyBufferSize,
            uint* RequiredSize);

        [DllImport("SETUPAPI.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern unsafe HDEVINFO SetupDiGetClassDevs(
            Guid* ClassGuid,
            [Optional] char* Enumerator,
            HWND hwndParent,
            SETUP_DI_GET_CLASS_DEVS_FLAGS Flags);

        [DllImport("SETUPAPI.dll", SetLastError = true, ExactSpelling = true)]
        public static extern BOOL SetupDiDestroyDeviceInfoList(HDEVINFO DeviceInfoSet);
    }
}
