$Global:Solution = "TaskManager"
$Global:Dependencies = @()

function CreateSolution() {
    # TEMP
    if (Test-Path $($Global:Solution) -PathType Container) {
        Remove-Item $($Global:Solution) -Recurse -Force
    }

    if (-not(Test-Path "$($Global:Solution)" -PathType Container)) {
        New-Item -ItemType Directory "$($Global:Solution)" | Out-Null
        Set-Location "$($Global:Solution)"
        dotnet new sln
        Set-Location ..
    }
}

function CreateProject($Type, $Name, $Dependencies) {
    $FullName = "$($Global:Solution).$($Name)"
    $Directory = "$($Global:Solution)\$FullName"
    if (-not(Test-Path $Directory -PathType Container)) {
        New-Item -ItemType Directory "$Directory" | Out-Null
        Set-Location "$Directory"
        dotnet new $Type
        Set-Location ..
        dotnet sln add "$FullName\$FullName.csproj"
        Set-Location ..
    }
    foreach($Dependency in $Dependencies) {
        $d = @{}
        $d.From = $Name
        $d.To = $Dependency
        $Global:Dependencies += $d
    }
}

function AddReference ($Src, $Dst) {
    $SrcPath = "$($Global:Solution)\$($Global:Solution).$($Src)\$($Global:Solution).$($Src).csproj"
    $DstPath = "$($Global:Solution)\$($Global:Solution).$($Dst)\$($Global:Solution).$($Dst).csproj"
    dotnet add "$($SrcPath)" reference "$($DstPath)"
}

function AddReferences {
    $Global:Dependencies | % {
        #Write-Host "Adding reference from $($_.From) to $($_.To)" -ForegroundColor Yellow
        AddReference "$($_.From)" "$($_.To)"
    }
}

# Solution

CreateSolution

# Projets

CreateProject "classlib" "Models"

CreateProject "mvc" "Web" @("Contract.ViewModel", "Web.Bootstrap")
CreateProject "classlib" "Web.Bootstrap" @("Contract.ViewModel", "ViewModel", "Contract.Utility", "Utility", "Contract.Data", "Data.Json", "Data.Sqlite", "Contract.Business", "Business", "Models")
CreateProject "classlib" "Contract.ViewModel" @()
CreateProject "classlib" "Contract.Utility" @()
CreateProject "classlib" "Contract.Data" @("Models")
CreateProject "classlib" "Contract.Business" @("Models")

CreateProject "classlib" "ViewModel" @("Contract.ViewModel")
CreateProject "xunit"    "ViewModel.Tests" @("ViewModel")

CreateProject "classlib" "Utility" @()
CreateProject "xunit"    "Utility.Tests" @("Utility")

CreateProject "classlib" "Data.Json" @("Contract.Data")
CreateProject "xunit"    "Data.Json.Tests" @("Data.Json")

CreateProject "classlib" "Data.Sqlite" @("Contract.Data")
CreateProject "xunit"    "Data.Sqlite.Tests" @("Data.Sqlite")

CreateProject "classlib" "Business" @("Contract.Business")
CreateProject "xunit"    "Business.Tests" @("Business")

AddReferences
