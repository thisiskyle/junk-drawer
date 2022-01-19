

# This class's Play functions take a list of strings that all must be the same length to
# render properly, then displays them in order as an animation using the RenderAnimation function
class Animator 
{
    # keeps track of the current frame
    [int]$frameCounter

    # constructor for the Animator class
    Animator() 
    {
        $this.frameCounter = 0
    }

    # desides what 'back character' to use based on front of line flag, then backs up the approptiate number of spaces
    # and renders the next frame over the previous frame, then waits for frameTime milliseconds
    [void] RenderAnimation([string[]]$frames, [int]$frameTime, [bool]$front)
    {
        if($front)
        {
            $backChar = "`r"
        }
        else 
        {
            $backChar = ""
            for(($i = 0); ($i -lt $frames[0].length); ($i++))
            {
                $backChar = "$($backChar)`b"
            }
        }

        Write-Host "$($backChar)$($frames[$this.frameCounter++%$frames.Length])" -NoNewline
        Start-Sleep -Milliseconds $frameTime
    }

    # plays EOL by default
    [void] Play([string[]]$frames, $frameTime)
    {
        $this.PlayEOL($frames, $frameTime)
    }
    # plays the animation at the end of the line
    [void] PlayEOL([string[]]$frames, [int]$frameTime)
    {
        $this.RenderAnimation($frames, $frameTime, $false)
    }

    # plays the animation at the front of the line by using a carrage return without a newline
    [void] PlayFOL([string[]]$frames, [int]$frameTime)
    {
        $this.RenderAnimation($frames, $frameTime, $true)
    }

    [void] Clear()
    {
        Write-Host "`r                                                  `r" -NoNewline
    }
    

}

function New-Animator()
{
    [Animator]::new()
}


Export-ModuleMember -Function New-Animator
