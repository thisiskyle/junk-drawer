. $PSScriptRoot\ps_animate.ps1


$frames = @(
"/",
"-",
"\",
"|"
)


$animator = [Animator]::new($frames, $false)

Write-Host "Test   " -NoNewline
$animator.AutoPlay(50)



