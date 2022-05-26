# define the wallpaper handler
$setwallpapersrc = @"
    using System.Runtime.InteropServices;

    public static class WallpaperHandler
    {
        public const int SetDesktopWallpaper = 20;
        public const int UpdateIniFile = 0x01;
        public const int SendWinIniChange = 0x02;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo (int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void SetWallpaper ( string path )
        {
            SystemParametersInfo( SetDesktopWallpaper, 0, path, UpdateIniFile | SendWinIniChange );
        }
    }
"@

Add-Type -TypeDefinition $setwallpapersrc



$sunrise = "S:\dev\arch-setup\wallpaper\outset_sunrise.png"
$sunset = "S:\dev\arch-setup\wallpaper\outset_sunset.png"
$day = "S:\dev\arch-setup\wallpaper\outset_day.png"
$night = "S:\dev\arch-setup\wallpaper\outset_night.png"

$dayStart = Get-Date '10:00' 
$sunsetStart = Get-Date '17:00' 
$nightStart = Get-Date '22:00' 
$sunriseStart = Get-Date '07:00' 


while(1)
{
    $now = Get-Date

    # day
    if($now.TimeOfDay -ge $dayStart.TimeofDay -and $now.TimeOfDay -lt $sunsetStart.TimeOfDay)
    {
        [WallpaperHandler]::SetWallpaper($day)
    }
    # sunset
    elseif($now.TimeOfDay -ge $sunsetStart.TimeofDay -and $now.TimeOfDay -lt $nightStart.TimeOfDay)
    {
        [WallpaperHandler]::SetWallpaper($sunset)
    }
    # night
    elseif($now.TimeOfDay -ge $nightStart.TimeofDay -and $now.TimeOfDay -lt $sunriseStart.TimeOfDay)
    {
        [WallpaperHandler]::SetWallpaper($night)
    }
    # sunrise
    else
    {
        [WallpaperHandler]::SetWallpaper($sunrise)
    }

    Start-Sleep -Seconds (1*60)
}
