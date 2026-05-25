using System.Collections.Generic;
using LibreHardwareMonitor.Hardware.Memory;

namespace LibreHardwareMonitor.Hardware.Memory;

internal class MemoryGroup : IGroup
{
    private readonly List<IHardware> _hardware = new();

    public MemoryGroup(ISettings settings)
    {
        _hardware.Add(new VirtualMemory(settings));
        _hardware.Add(new TotalMemory(settings));
    }

    public IReadOnlyList<IHardware> Hardware => _hardware;
    public string GetReport() => string.Empty;
    public void Close() { }
}
