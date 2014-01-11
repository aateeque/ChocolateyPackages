try {
	cinstm notepadplusplus
	cinstm fiddler4
	cinstm git-credential-winstore
	cinstm git
	cinstm poshgit
	cinstm gitextensions 
	cinstm NugetPackageExplorer 
	cinstm ChocolateyGUI 
	cinstm githubforwindows 
	cinstm PowerGUI
	cinstm powershell
 
	# fix the "WARNING: Could not find ssh-agent" in PowerShell console as per here:
	# http://stackoverflow.com/questions/7470385/git-in-powershell-saying-could-not-find-ssh-agent
	$currentPathVariable = [environment]::GetEnvironmentVariable("PATH", "Machine")
	[System.Environment]::SetEnvironmentVariable("PATH", $currentPathVariable + ";C:\Program Files (x86)\Git\bin", "Machine")
	
	if(!(Test-Path "c:\github")) {
		New-Item -Type Directory "C:\github"
	}
	
	cd "C:\github"
	
	if(!(Test-Path "c:\github\chocolatey-coreteampackages")){
		git clone https://github.com/chocolatey/chocolatey-coreteampackages.git
	}
	
	if(!(Test-Path "c:\github\chocolatey.org")){
		git clone https://github.com/chocolatey/chocolatey.org.git
	}
	
	if(!(Test-Path "c:\github\chocolatey-webhooks")){
		git clone https://github.com/chocolatey/chocolatey-webhooks.git
	}
	
	if(!(Test-Path "c:\github\puppet-chocolatey")){
		git clone https://github.com/chocolatey/puppet-chocolatey.git
	}
	
	if(!(Test-Path "c:\github\puppet-chocolatey-handsonlab")){
		git clone https://github.com/chocolatey/puppet-chocolatey-handsonlab.git
	}
	
	if(!(Test-Path "c:\github\chocolatey")){
		git clone https://github.com/chocolatey/chocolatey.git
	}
	
	if(!(Test-Path "c:\github\chocolateytemplates")){
		git clone https://github.com/chocolatey/chocolateytemplates.git
	}
 
	if(!(Test-Path "c:\github\chocolatey.github.com")){
		git clone https://github.com/chocolatey/chocolatey.github.com.git
	}
	
	if(!(Test-Path "c:\github\chocolatey-cookbook")){
		git clone https://github.com/chocolatey/chocolatey-cookbook.git
	}
	
	if(!(Test-Path "c:\github\chocolatey.web")){
		git clone https://github.com/chocolatey/chocolatey.web.git
	}
	
	if(!(Test-Path "c:\github\chocolatey-Explorer")){
		git clone https://github.com/gep13/chocolatey-Explorer.git
	}
	
	Install-ChocolateyPinnedTaskBarItem "C:\Program Files (x86)\Notepad++\notepad++.exe"
	Install-ChocolateyPinnedTaskBarItem "C:\Program Files\Internet Explorer\iexplore.exe"
	
	# Enable Nuget Package Restore
	
	# Setup Database
	
    Write-ChocolateySuccess 'gep13.ChocolateyDev'
} catch {
	Write-ChocolateyFailure 'gep13.ChocolateyDev' $($_.Exception.Message)
	throw
}