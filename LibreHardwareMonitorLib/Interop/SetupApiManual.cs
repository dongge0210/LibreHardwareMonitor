// Manual definitions for Windows Setup API types and P/Invoke methods.
// CsWin32 v0.3.275 may fail to generate these on certain SDK/CI configurations.
// If CsWin32-generated types appear, the 'internal' visibility may cause
// duplicate-definition errors — remove this file at that point.

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Windows.Win32.Devices.DeviceAndDriverInstallation
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SP_DEVINFO_DATA
    {
        public uint cbSize;
        public Guid ClassGuid;
        public uint DevInst;
        public nint Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SP_DEVICE_INTERFACE_DATA
    {
        public uint cbSize;
        public Guid InterfaceClassGuid;
        public uint Flags;
        public nint Reserved;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal unsafe struct SP_DEVICE_INTERFACE_DETAIL_DATA_W
    {
        public uint cbSize;
        public fixed char DevicePath[1]; // variable-length
    }

    [Flags]
    internal enum SETUP_DI_GET_CLASS_DEVS_FLAGS : uint
    {
        DIGCF_DEFAULT = 0x00000001,
        DIGCF_PRESENT = 0x00000002,
        DIGCF_ALLCLASSES = 0x00000004,
        DIGCF_PROFILE = 0x00000008,
        DIGCF_DEVICEINTERFACE = 0x00000010,
    }
}

namespace Windows.Win32.Devices.DeviceAndDriverInstallation
{
    internal enum SETUP_DI_REGISTRY_PROPERTY : uint { }
}

namespace Windows.Win32.Foundation
{
    internal readonly partial struct HDEVINFO
    {
        public readonly nint Value;
        public HDEVINFO(nint value) => Value = value;
        public static explicit operator HDEVINFO(nint v) => new(v);
        public static implicit operator nint(HDEVINFO h) => h.Value;
        public static readonly HDEVINFO Null = default;
    }

    internal sealed class SetupDiDestroyDeviceInfoListSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SetupDiDestroyDeviceInfoListSafeHandle() : base(true) { }
        protected override bool ReleaseHandle()
        {
            return SetupDiDestroyDeviceInfoList(handle);
        }

        [DllImport("setupapi.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool SetupDiDestroyDeviceInfoList(nint DeviceInfoSet);
    }
}

namespace Windows.Win32
{
    internal static partial class PInvoke
    {
        // GUIDs
        internal static readonly Guid GUID_DEVCLASS_PORTS = new(0x4d36e978, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18);
        internal static readonly Guid GUID_DEVICE_BATTERY = new(0x72631E54, 0x78A4, 0x11D0, 0xBC, 0xF7, 0x00, 0xAA, 0x00, 0xB7, 0xB3, 0x2A);

        // Setup API P/Invokes
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern Foundation.SetupDiDestroyDeviceInfoListSafeHandle SetupDiGetClassDevs(
            in Guid ClassGuid,
            string? Enumerator,
            Foundation.HWND hwndParent,
            Devices.DeviceAndDriverInstallation.SETUP_DI_GET_CLASS_DEVS_FLAGS Flags);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiEnumDeviceInterfaces(
            Foundation.SetupDiDestroyDeviceInfoListSafeHandle DeviceInfoSet,
            void* DeviceInfoData, // optional
            in Guid InterfaceClassGuid,
            uint MemberIndex,
            ref Devices.DeviceAndDriverInstallation.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern unsafe bool SetupDiGetDeviceInterfaceDetail(
            Foundation.HDEVINFO DeviceInfoSet,
            Devices.DeviceAndDriverInstallation.SP_DEVICE_INTERFACE_DATA* DeviceInterfaceData,
            Devices.DeviceAndDriverInstallation.SP_DEVICE_INTERFACE_DETAIL_DATA_W* DeviceInterfaceDetailData,
            uint DeviceInterfaceDetailDataSize,
            uint* RequiredSize,
            void* DeviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiEnumDeviceInfo(
            Foundation.SetupDiDestroyDeviceInfoListSafeHandle DeviceInfoSet,
            uint MemberIndex,
            ref Devices.DeviceAndDriverInstallation.SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern unsafe bool SetupDiGetDeviceRegistryProperty(
            Foundation.SetupDiDestroyDeviceInfoListSafeHandle DeviceInfoSet,
            in Devices.DeviceAndDriverInstallation.SP_DEVINFO_DATA DeviceInfoData,
            Devices.DeviceAndDriverInstallation.SETUP_DI_REGISTRY_PROPERTY Property,
            uint* PropertyRegDataType, // optional
            byte* PropertyBuffer,
            uint PropertyBufferSize,
            uint* RequiredSize);
    }
}
