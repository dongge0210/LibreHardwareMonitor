// Manual definitions for Windows Setup API types.
// CsWin32 v0.3.275 may fail to generate these on certain SDK/CI configurations.
// These are internal fallback types — remove if CsWin32 starts generating them.

using System;
using System.Runtime.InteropServices;

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
    internal struct SP_DEVICE_INTERFACE_DETAIL_DATA_W
    {
        public uint cbSize;
        public char DevicePath; // first char of variable-length string
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
