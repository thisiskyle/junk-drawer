
$list = Get-ChildItem -Path '.' -Recurse | Where-Object {$_.PSIsContainer -eq $false}


ForEach($n in $list) {
    #write-host $n.Directory
    New-Item -Path $n.Directory -Name $n.Basename -ItemType "directory"
    Move-Item -Path $n -Destination "$($n.Directory)/$($n.Basename)"
}
