
class Animator 
{
    # keeps track of the current frame
    [int]$frameCounter
    # string array of the frames of the animations
    [string[]]$frames
    # gets the character string for moving the cursor into the proper position
    [string]$backChar

    # constructor for the Animator class
    Animator([string[]]$frames, [bool]$front) 
    {
        $this.frameCounter = 0
        $this.frames = $frames

        if($front)
        {
            $this.SetRenderPositionFront()
        }
        else 
        {
            $this.SetRenderPositionNormal()
        }
    }

    # Writes the next frame to the host
    [void] RenderNextFrame()
    {
        Write-Host "$($this.backChar)$($this.GetNextFrame())" -NoNewline
    }

    # returns the string that is the next frame
    [string] GetNextFrame()
    {
        return "$($this.frames[$this.frameCounter++%$this.frames.Length])"
    }


    # sets the back char to render the animation at the begining of a line
    [void] SetRenderPositionFront()
    {
        $this.backChar = "`r"
    }

    # sets the back char to render the animation at the end of the line
    [void] SetRenderPositionNormal()
    {
        $this.backChar = ""
        for(($i = 0); ($i -lt $this.frames[0].length); ($i++))
        {
            $this.backChar = "$($this.backChar)`b"
        }
    }

    # a blocking while loop that will play the animation forever
    [void] AutoPlay($frameTime)
    {
        while($true)
        {
            $this.RenderNextFrame()
            Start-Sleep -m $frameTime
        }
    }

    # reset the frame count to 1
    [void] Reset()
    {
        $this.frameCounter = 0
    }

    # clear the line
    [void] Clear()
    {
        # we do a backchar to return to begining of a line, then a bunch spaces to clear the line, then a return again to start of the line
        Write-Host "`r                                                                                                                                            `r" -NoNewline
    }
}
