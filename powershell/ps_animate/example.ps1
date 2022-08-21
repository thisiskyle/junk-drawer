Using module S:\dev\junk-drawer\powershell\ps_animate\ps_animate.psm1


$frames = @(
"/",
"-",
"\",
"|"
)


$animator = [Animator]::new($frames, $true)

$animator.AutoPlay(50)


