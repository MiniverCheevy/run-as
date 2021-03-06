; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Run As Wrapper"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Shawn Doucet"
#define MyAppURL "https://github.com/MiniverCheevy/run-as"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{3842230E-11EF-45E2-BD91-67D219C3D3D5}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputBaseFilename=ra.setup
SetupIconFile="C:\DropBox\Lib\Projects\ra - Copy\ra\Circle_Green.ico"
Compression=lzma
SolidCompression=yes
LicenseFile=C:\DropBox\Lib\Projects\ra - Copy\ra\MITLicense.txt
VersionInfoCopyright=Shawn Doucet, 2014
VersionInfoProductName=Run As Wrapper

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\config.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\config.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\config.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\config.ra"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Microsoft.AspNet.SignalR.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Microsoft.AspNet.SignalR.Owin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Microsoft.Owin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Microsoft.Owin.FileSystems.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Microsoft.Owin.Host.HttpListener.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Microsoft.Owin.Hosting.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Microsoft.Owin.Security.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Microsoft.Owin.StaticFiles.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Nancy.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Nancy.Owin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Owin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\ra.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\ra.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\ra.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\RunAsWrapper.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\RunAsWrapper.Core.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\RunAsWrapper.Core.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\System.Net.Http.Formatting.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\System.Web.Http.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\System.Web.Http.Owin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\Voodoo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\DropBox\Lib\Projects\ra - Copy\build-outputs\web\*"; DestDir: "{app}\web"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"


[Registry]
Root: HKLM; Subkey: "SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
    ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};ExpandConstant({app})";
    Check: NeedsAddPath(ExpandConstant({app}))

[Code]
function NeedsAddPath(Param: string): boolean;
var
  OrigPath: string;
begin
  if not RegQueryStringValue(HKEY_LOCAL_MACHINE,
    'SYSTEM\CurrentControlSet\Control\Session Manager\Environment',
    'Path', OrigPath)
  then begin
    Result := True;
    exit;
  end;
  // look for the path with leading and trailing semicolon
  // Pos() returns 0 if not found
  Result := Pos(';' + Param + ';', ';' + OrigPath + ';') = 0;
end;