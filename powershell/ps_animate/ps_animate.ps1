class Animator 
{
    # keeps track of the current frame
    [int]$frameCounter
    # string array of the frames of the animations
    [string[]]$frames
    # gets the character string for moving the cursor into the proper position
    [string]$backStr
    [string]$clearStr

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

        # build the string for clearing a line
        $this.clearStr = ""

        for(($i = 0); ($i -lt 500); ($i++))
        {
            $this.clearStr = "$($this.clearStr)"
        }
        $this.clearStr = "`r$($this.clearStr)`r"
    }

    # Writes the next frame to the host
    [void] RenderNextFrame()
    {
        Write-Host "$($this.backStr)$($this.GetNextFrame())" -NoNewline
        #$this.Clear()
        #Write-Host "$($this.GetNextFrame())" -NoNewline
    }

    # returns the string that is the next frame
    [string] GetNextFrame()
    {
        return "$($this.frames[$this.frameCounter++%$this.frames.Length])"
    }


    # sets the back char to render the animation at the begining of a line
    [void] SetRenderPositionFront()
    {
        $this.backStr = "`r"
    }

    # sets the back char to render the animation at the end of the line
    [void] SetRenderPositionNormal()
    {
        $this.backStr = ""
        for(($i = 0); ($i -lt $this.frames[0].length); ($i++))
        {
            $this.backStr = "$($this.backStr)`b"
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
        Write-Host "$($this.clearStr)" -NoNewline
    }

}
