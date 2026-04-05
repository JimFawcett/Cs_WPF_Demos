Add-Type -AssemblyName System.Drawing

Add-Type @"
using System;
using System.Runtime.InteropServices;
using System.Drawing;
public class WinCap {
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT r);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr hWnd, int n);
    [DllImport("user32.dll")] public static extern bool IsIconic(IntPtr hWnd);
    [DllImport("user32.dll")] public static extern bool PrintWindow(IntPtr hWnd, IntPtr hDC, uint flags);
}
public struct RECT { public int Left, Top, Right, Bottom; }
"@

$base    = "C:\github\JimFawcett\NewSite\Code\C#\Cs_WPF_Demos"
$dotnet  = "dotnet"

# [ csproj-relative-path, process-name, screenshot-name ]
$projects = @(
    @("WPF_BarChart\BarChart\9b.Drawing-BarChart.csproj",                          "ColumnChart",                   "screenshot_WPF_BarChart.png"),
    @("WPF_ChangeNotification\WPF_ChangeNotification\WPF_ChangeNotification.csproj","WPF_ChangeNotification",        "screenshot_WPF_ChangeNotification.png"),
    @("WPF_Controls\Controls\6.Controls.csproj",                                   "WpfApplication3",               "screenshot_WPF_Controls.png"),
    @("WPF_ControlTemplate\ControlTemplateDemo\ControlTemplateDemo\9.ControlTemplate.csproj","ControlTemplateDemo","screenshot_WPF_ControlTemplate.png"),
    @("WPF_CustomElement\CustomElement\9a.CustomElement.csproj",                   "CustomElement",                 "screenshot_WPF_CustomElement.png"),
    @("WPF_DataTemplateDemo\7.DataTemplate.csproj",                                "HDI-WPF-ListItemTemplate-cs",   "screenshot_WPF_DataTemplateDemo.png"),
    @("Wpf_DefaultProject\Wpf_DefaultProject\1.Wpf_DefaultProject.csproj",        "Wpf_DefaultProject",            "screenshot_Wpf_DefaultProject.png"),
    @("WPF_DemoPanels\CanvasPanel\Canvas.csproj",                                  "CanvasPanel",                   "screenshot_DemoPanel_Canvas.png"),
    @("WPF_DemoPanels\DockPanel\DockPanel.csproj",                                 "DockPanel",                     "screenshot_DemoPanel_DockPanel.png"),
    @("WPF_DemoPanels\Grid\Grid.csproj",                                           "Grid",                          "screenshot_DemoPanel_Grid.png"),
    @("WPF_DemoPanels\StackPanel\StackPanel.csproj",                               "StackPanel",                    "screenshot_DemoPanel_StackPanel.png"),
    @("WPF_DemoPanels\WrapPanel\WrapPanel.csproj",                                 "DemoPanels",                    "screenshot_DemoPanel_WrapPanel.png"),
    @("WPF_DragDropExample\DragDropExample\DragDropExample.csproj",                "DragDropExample",               "screenshot_WPF_DragDropExample.png"),
    @("Wpf_LabManager\WpfApplication2\WpfApplication2.csproj",                    "WpfApplication2",               "screenshot_Wpf_LabManager.png"),
    @("WPF_MessageHook\MessageHook\MessageHook.csproj",                            "MessageHook",                   "screenshot_WPF_MessageHook.png"),
    @("Wpf_RoutedEvent\RoutedEvent\3.RoutedEvent.csproj",                          "RoutedEvent",                   "screenshot_Wpf_RoutedEvent.png"),
    @("Wpf_Triggers\Wpf_Triggers\5.Triggers.csproj",                              "Wpf_Triggers",                  "screenshot_Wpf_Triggers.png"),
    @("WPF_UserControlDemo\UserControlDemo\8.UserControl.csproj",                  "UserControlDemo",               "screenshot_WPF_UserControlDemo.png")
)

function Capture-ProcessWindow {
    param($ProcessName, $OutPath)
    $procs = Get-Process -Name $ProcessName -ErrorAction SilentlyContinue
    if (-not $procs) { Write-Host "  NOT FOUND: $ProcessName"; return $false }
    $hwnd = $procs[0].MainWindowHandle
    if ($hwnd -eq [IntPtr]::Zero) { Write-Host "  NO WINDOW: $ProcessName"; return $false }
    if ([WinCap]::IsIconic($hwnd)) { [WinCap]::ShowWindow($hwnd, 9) | Out-Null; Start-Sleep -Milliseconds 400 }
    [WinCap]::SetForegroundWindow($hwnd) | Out-Null
    Start-Sleep -Milliseconds 600
    $rect = New-Object RECT
    [WinCap]::GetWindowRect($hwnd, [ref]$rect) | Out-Null
    $w = $rect.Right - $rect.Left; $h = $rect.Bottom - $rect.Top
    if ($w -le 0 -or $h -le 0) { Write-Host "  BAD RECT: $ProcessName"; return $false }
    $bmp = New-Object System.Drawing.Bitmap($w, $h)
    $g   = [System.Drawing.Graphics]::FromImage($bmp)
    $hdc = $g.GetHdc()
    [WinCap]::PrintWindow($hwnd, $hdc, 2) | Out-Null
    $g.ReleaseHdc($hdc); $g.Dispose()
    $bmp.Save($OutPath, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    Write-Host "  Saved: $OutPath"
    return $true
}

foreach ($entry in $projects) {
    $csproj  = $entry[0]
    $pname   = $entry[1]
    $outfile = $entry[2]
    $fullproj = "$base\$csproj"
    $outpath  = "$base\$outfile"

    Write-Host "`n=== $pname ==="

    # Launch
    $proc = Start-Process -FilePath $dotnet -ArgumentList "run --project `"$fullproj`"" -PassThru -WindowStyle Normal
    Start-Sleep -Seconds 4

    # Find window (process name may differ from $pname)
    $captured = Capture-ProcessWindow -ProcessName $pname -OutPath $outpath

    if (-not $captured) {
        # Try to find by window title using all procs with windows
        $allProcs = Get-Process | Where-Object { $_.MainWindowHandle -ne [IntPtr]::Zero -and $_.MainWindowTitle -ne "" }
        Write-Host "  Available windows:"
        $allProcs | Select-Object Name, MainWindowTitle | Format-Table -AutoSize | Out-String | Write-Host
    }

    # Kill launched process tree
    if ($proc -and !$proc.HasExited) {
        Stop-Process -Id $proc.Id -Force -ErrorAction SilentlyContinue
    }
    # Also kill by assembly name in case dotnet spawned a child
    Get-Process -Name $pname -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue
    Start-Sleep -Milliseconds 500
}

Write-Host "`nDone."
