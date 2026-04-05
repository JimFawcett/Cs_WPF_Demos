Add-Type -AssemblyName System.Drawing

Add-Type @"
using System;
using System.Runtime.InteropServices;
public class WinCap2 {
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT r);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr hWnd, int n);
    [DllImport("user32.dll")] public static extern bool IsIconic(IntPtr hWnd);
    [DllImport("user32.dll")] public static extern bool PrintWindow(IntPtr hWnd, IntPtr hDC, uint flags);
}
public struct RECT { public int Left, Top, Right, Bottom; }
"@

$base = "C:\github\JimFawcett\NewSite\Code\C#\Cs_WPF_Demos"

# [ exe-path, process-name (no .exe), screenshot-filename ]
$projects = @(
    @("WPF_BarChart\BarChart\bin\Debug\net10.0-windows\ColumnChart.exe",                         "ColumnChart",                 "screenshot_WPF_BarChart.png"),
    @("WPF_ChangeNotification\WPF_ChangeNotification\bin\Debug\net10.0-windows\WPF_ChangeNotification.exe", "WPF_ChangeNotification",    "screenshot_WPF_ChangeNotification.png"),
    @("WPF_Controls\Controls\bin\Debug\net10.0-windows\WpfApplication3.exe",                     "WpfApplication3",             "screenshot_WPF_Controls.png"),
    @("WPF_ControlTemplate\ControlTemplateDemo\ControlTemplateDemo\bin\Debug\net10.0-windows\ControlTemplateDemo.exe", "ControlTemplateDemo", "screenshot_WPF_ControlTemplate.png"),
    @("WPF_DataTemplateDemo\bin\Debug\net10.0-windows\HDI-WPF-ListItemTemplate-cs.exe",          "HDI-WPF-ListItemTemplate-cs", "screenshot_WPF_DataTemplateDemo.png"),
    @("WPF_MessageHook\MessageHook\bin\Debug\net10.0-windows\MessageHook.exe",                   "MessageHook",                 "screenshot_WPF_MessageHook.png"),
    @("Wpf_RoutedEvent\RoutedEvent\bin\Debug\net10.0-windows\RoutedEvent.exe",                   "RoutedEvent",                 "screenshot_Wpf_RoutedEvent.png"),
    @("WPF_DemoPanels\StackPanel\bin\Debug\net10.0-windows\StackPanel.exe",                      "StackPanel",                  "screenshot_DemoPanel_StackPanel.png")
)

function Capture-Win {
    param($ProcessName, $OutPath)
    # Wait up to 8s for window
    $hwnd = [IntPtr]::Zero
    for ($i = 0; $i -lt 16; $i++) {
        $p = Get-Process -Name $ProcessName -ErrorAction SilentlyContinue | Where-Object { $_.MainWindowHandle -ne [IntPtr]::Zero }
        if ($p) { $hwnd = $p[0].MainWindowHandle; break }
        Start-Sleep -Milliseconds 500
    }
    if ($hwnd -eq [IntPtr]::Zero) { Write-Host "  TIMEOUT waiting for window: $ProcessName"; return $false }
    if ([WinCap2]::IsIconic($hwnd)) { [WinCap2]::ShowWindow($hwnd, 9) | Out-Null; Start-Sleep -Milliseconds 400 }
    [WinCap2]::SetForegroundWindow($hwnd) | Out-Null
    Start-Sleep -Milliseconds 600
    $rect = New-Object RECT
    [WinCap2]::GetWindowRect($hwnd, [ref]$rect) | Out-Null
    $w = $rect.Right - $rect.Left; $h = $rect.Bottom - $rect.Top
    if ($w -le 0 -or $h -le 0) { Write-Host "  BAD RECT: $ProcessName"; return $false }
    $bmp = New-Object System.Drawing.Bitmap($w, $h)
    $g   = [System.Drawing.Graphics]::FromImage($bmp)
    $hdc = $g.GetHdc()
    [WinCap2]::PrintWindow($hwnd, $hdc, 2) | Out-Null
    $g.ReleaseHdc($hdc); $g.Dispose()
    $bmp.Save($OutPath, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    Write-Host "  Saved: $OutPath"
    return $true
}

foreach ($entry in $projects) {
    $exePath = "$base\$($entry[0])"
    $pname   = $entry[1]
    $outpath = "$base\$($entry[2])"
    Write-Host "`n=== $pname ==="

    $proc = Start-Process -FilePath $exePath -PassThru -WindowStyle Normal
    $ok   = Capture-Win -ProcessName $pname -OutPath $outpath

    # Kill
    if ($proc -and !$proc.HasExited) { Stop-Process -Id $proc.Id -Force -ErrorAction SilentlyContinue }
    Get-Process -Name $pname -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue
    Start-Sleep -Milliseconds 300
}

Write-Host "`nDone."
