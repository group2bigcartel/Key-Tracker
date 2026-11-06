Room Key Tracker (VB.NET, Windows Forms, .NET 8)

REQUIREMENTS
- Windows 10/11
- .NET 8 SDK (https://dotnet.microsoft.com/download) or Visual Studio 2022
- A webcam (optional; a manual code box is built in)

FILES
- RoomKeyTracker.sln      Solution (open this in Visual Studio)
- RoomKeyTracker.vbproj   Project + NuGet packages (restored automatically)
- Program.vb              Data classes, JSON storage, entry point
- MainForm.vb             The whole UI and logic

ADMIN
  The Admin tab (password-protected) holds Key QRs, Register, and Log && Password (clear log, change password). The Log tab itself is public.
  The first time it is opened you create the admin password.

BUILD / RUN
  Visual Studio: open RoomKeyTracker.sln, press F5.
  Command line:  dotnet run

DATA
  Saved to %AppData%\RoomKeyTracker\data.json

NOTES
- The first build needs internet to download NuGet packages
  (QRCoder, ZXing.Net, AForge.Video.DirectShow).
- A NU1701 warning about AForge is expected and suppressed.
