$name = 'CDBurnerXP'
$32BitUrl  = 'http://download.cdburnerxp.se/cdbxp_setup_4.5.1.4003.exe'
$64BitUrl  = 'http://download.cdburnerxp.se/cdbxp_setup_4.5.1.4003_x64.exe'

Install-ChocolateyPackage $name 'EXE' '/verysilent' $32BitUrl $64BitUrl -validExitCodes @(0)