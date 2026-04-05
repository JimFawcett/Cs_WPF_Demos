Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

Add-Type @"
using System;
using System.Runtime.InteropServices;
public class WinAPI {
    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [DllImport("user32.dll")]
    public static extern bool IsIconic(IntPtr hWnd);
}
public struct RECT {
    public int Left, Top, Right, Bottom;
}
"@

function Capture-Window {
    param($ProcessName, $OutPath)

    $procs = Get-Process -Name $ProcessName -ErrorAction SilentlyContinue
    if (-not $procs) {
        Write-Host "Process '$ProcessName' not found."
        return $false
    }

    $hwnd = $procs[0].MainWindowHandle
    if ($hwnd -eq [IntPtr]::Zero) {
        Write-Host "No main window for '$ProcessName'."
        return $false
    }

    # Restore if minimized
    if ([WinAPI]::IsIconic($hwnd)) {
        [WinAPI]::ShowWindow($hwnd, 9) | Out-Null
        Start-Sleep -Milliseconds 500
    }

    [WinAPI]::SetForegroundWindow($hwnd) | Out-Null
    Start-Sleep -Milliseconds 800

    $rect = New-Object RECT
    [WinAPI]::GetWindowRect($hwnd, [ref]$rect) | Out-Null

    $width  = $rect.Right  - $rect.Left
    $height = $rect.Bottom - $rect.Top

    if ($width -le 0 -or $height -le 0) {
        Write-Host "Invalid window dimensions for '$ProcessName'."
        return $false
    }

    $bmp = New-Object System.Drawing.Bitmap($width, $height)
    $g   = [System.Drawing.Graphics]::FromImage($bmp)
    $g.CopyFromScreen($rect.Left, $rect.Top, 0, 0, [System.Drawing.Size]::new($width, $height))
    $g.Dispose()
    $bmp.Save($OutPath, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()

    Write-Host "Saved: $OutPath"
    return $true
}

$dir = Split-Path -Parent $MyInvocation.MyCommand.Path

Capture-Window -ProcessName "WPF_AllCode"        -OutPath "$dir\screenshot_WPF_AllCode.png"
Capture-Window -ProcessName "Wpf_AttachedProperties" -OutPath "$dir\screenshot_AttachedProperties.png"
