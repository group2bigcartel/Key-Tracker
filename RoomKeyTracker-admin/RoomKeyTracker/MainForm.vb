Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Text.RegularExpressions
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports QRCoder

Public Class MainForm

    Private ReadOnly GreenFg As Color = Color.FromArgb(21, 128, 61)
    Private ReadOnly GreenBg As Color = Color.FromArgb(220, 252, 231)
    Private ReadOnly RedFg As Color = Color.FromArgb(185, 28, 28)
    Private ReadOnly RedBg As Color = Color.FromArgb(254, 226, 226)
    ' ISU theme colors (from the SIAS Online page)
    Private ReadOnly Accent As Color = ColorTranslator.FromHtml("#256020")      ' main green (buttons)
    Private ReadOnly AccentDark As Color = ColorTranslator.FromHtml("#1F5E1A")  ' header bar
    Private ReadOnly AccentHover As Color = ColorTranslator.FromHtml("#2F7A29")
    Private ReadOnly PageBg As Color = ColorTranslator.FromHtml("#F1F7F0")      ' very light green tint
    Private ReadOnly Gold As Color = ColorTranslator.FromHtml("#C9A227")        ' gold accent line

    Private S As AppState = AppState.Load()

    Private pnlConfirm As FlowLayoutPanel
    Private _cam As VideoCaptureDevice
    Private _locked As Boolean
    Private _gotFrame As Boolean
    Private ReadOnly _camMonikers As New List(Of String)
    Private ReadOnly camWatch As New Timer With {.Interval = 6000}
    Private _step As Integer = 1
    Private _user As User
    Private _linkMode As Boolean
    Private _linkName As String = ""
    Private _linkRole As String = "FAC"
    Private ReadOnly _reader As New ZXing.Windows.Compatibility.BarcodeReader()

    Private ReadOnly refresh As New Timer With {.Interval = 30000}

    Private _adminUnlocked As Boolean
    Private _prevTab As TabPage

    Public Sub New()
        InitializeComponent()

        _reader.AutoRotate = True
        _reader.Options.TryHarder = True
        _reader.Options.TryInverted = True
        _reader.Options.PossibleFormats = New List(Of ZXing.BarcodeFormat) From {ZXing.BarcodeFormat.QR_CODE}

        pnlConfirm = New FlowLayoutPanel With {.Dock = DockStyle.Fill, .FlowDirection = FlowDirection.TopDown,
                                               .WrapContents = False, .Visible = False}
        tpScan.Controls.Add(pnlConfirm)

        cboRole.SelectedIndex = 0
        LoadCams()
        ApplyTheme()
        SetFullscreen(True)   ' start fullscreen; press F11 to toggle

        _prevTab = tpRooms
        AddHandler tabs.SelectedIndexChanged, AddressOf OnTabChanged
        AddHandler tabsAdmin.SelectedIndexChanged, Sub() DrawAdminTab()
        AddHandler refresh.Tick, Sub() If tabs.SelectedTab Is tpRooms Then DrawRooms()
        AddHandler camWatch.Tick,
            Sub()
                camWatch.Stop()
                If _cam IsNot Nothing AndAlso Not _gotFrame Then
                    ShowErr("No video received from the camera. It may be in use by another app, " &
                            "blocked in Windows Settings > Privacy > Camera (allow desktop apps), or not compatible.")
                    btnStart.Visible = True : btnStart.Text = "Start camera"
                End If
            End Sub
        refresh.Start()
        OnTabChanged(Nothing, EventArgs.Empty)
    End Sub

    Private _fullscreen As Boolean

    Private Sub SetFullscreen(enable As Boolean)
        _fullscreen = enable
        If enable Then
            Me.WindowState = FormWindowState.Normal   ' reset first so the borderless change applies cleanly
            Me.FormBorderStyle = FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.FormBorderStyle = FormBorderStyle.Sizable
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.F11 Then
            SetFullscreen(Not _fullscreen)
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub ApplyTheme()
        Me.BackColor = PageBg

        ' Header bar with a gold line underneath, like the SIAS page
        Dim header As New Panel With {.Dock = DockStyle.Top, .Height = 52, .BackColor = AccentDark}
        Dim goldLine As New Panel With {.Dock = DockStyle.Bottom, .Height = 3, .BackColor = Gold}
        Dim title As New Label With {.Text = "Room Key Tracker", .ForeColor = Color.White, .AutoSize = False,
                                     .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft,
                                     .Font = New Font("Segoe UI", 15, FontStyle.Bold), .Padding = New Padding(14, 0, 0, 0)}
        header.Controls.Add(title)
        header.Controls.Add(goldLine)
        Me.Controls.Add(header)
        header.SendToBack()   ' dock above the tabs

        ThemeControls(Me)
    End Sub

    Private Sub ThemeControls(parent As Control)
        For Each c As Control In parent.Controls
            Dim b = TryCast(c, Button)
            If b IsNot Nothing Then
                b.FlatStyle = FlatStyle.Flat
                b.FlatAppearance.BorderSize = 0
                b.BackColor = Accent
                b.ForeColor = Color.White
                b.FlatAppearance.MouseOverBackColor = AccentHover
                b.FlatAppearance.MouseDownBackColor = AccentDark
                b.Cursor = Cursors.Hand
            ElseIf TypeOf c Is TabPage Then
                c.BackColor = PageBg
            End If
            ThemeControls(c)
        Next
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        StopCam()
        MyBase.OnFormClosing(e)
    End Sub

    Private _switching As Boolean

    Private Sub OnTabChanged(sender As Object, e As EventArgs)
        If _switching Then Return
        Dim cur = tabs.SelectedTab

        If cur IsNot tpScan Then StopCam() : _linkMode = False
        ' Leaving Admin locks it again (except while registering via an existing QR).
        If _prevTab Is tpAdmin AndAlso cur IsNot tpAdmin AndAlso Not _linkMode Then _adminUnlocked = False

        If cur Is tpAdmin AndAlso Not _adminUnlocked Then
            If RequirePassword("open the Admin page") Then
                _adminUnlocked = True
            Else
                _switching = True
                tabs.SelectedTab = If(_prevTab IsNot Nothing AndAlso _prevTab IsNot tpAdmin, _prevTab, tpRooms)
                _switching = False
                cur = tabs.SelectedTab
                If cur IsNot tpScan Then StopCam()
            End If
        End If

        _prevTab = cur
        If cur Is tpRooms Then
            DrawRooms()
        ElseIf cur Is tpScan Then
            ResetScan()
        ElseIf cur Is tpLog Then
            DrawLog()
        ElseIf cur Is tpAdmin Then
            DrawAdminTab()
        End If
    End Sub

    Private Sub DrawAdminTab()
        If tabsAdmin.SelectedTab Is tpRegister Then
            DrawUsers()
        ElseIf tabsAdmin.SelectedTab Is tpKeys Then
            DrawKeys()
        End If
    End Sub

    Private Shared Function Fmt(t As DateTime?) As String
        Return If(t.HasValue, t.Value.ToString("MMM d, h:mm tt"), "")
    End Function

    Private Shared Function RoleName(r As String) As String
        Return If(r = "FAC", "Faculty", "OJT")
    End Function

    Private Shared Function MakeQr(text As String, pixelsPerModule As Integer) As Bitmap
        Using gen As New QRCodeGenerator()
            Using data = gen.CreateQrCode(text, QRCodeGenerator.ECCLevel.M)
                Using code As New QRCoder.QRCode(data)
                    Return code.GetGraphic(pixelsPerModule)
                End Using
            End Using
        End Using
    End Function

    Private Shared Sub ClearPanel(p As Control)
        For i = p.Controls.Count - 1 To 0 Step -1
            Dim c = p.Controls(i)
            DisposeImages(c)
            p.Controls.RemoveAt(i)
            c.Dispose()
        Next
    End Sub

    Private Shared Sub DisposeImages(c As Control)
        Dim pb = TryCast(c, PictureBox)
        If pb IsNot Nothing AndAlso pb.Image IsNot Nothing Then
            pb.Image.Dispose()
            pb.Image = Nothing
        End If
        For Each child As Control In c.Controls
            DisposeImages(child)
        Next
    End Sub

    Private Function NewLabel(text As String, Optional size As Single = 10, Optional bold As Boolean = False,
                              Optional color As Color = Nothing) As Label
        Dim l As New Label With {
            .Text = text, .AutoSize = True,
            .Font = New Font("Segoe UI", size, If(bold, FontStyle.Bold, FontStyle.Regular)),
            .Margin = New Padding(3, 4, 3, 4)}
        If color <> Nothing Then l.ForeColor = color
        Return l
    End Function

    Private Function NewButton(text As String, primary As Boolean) As Button
        Dim b As New Button With {.Text = text, .AutoSize = True, .Padding = New Padding(10, 4, 10, 4),
                                  .FlatStyle = FlatStyle.Flat, .Margin = New Padding(3)}
        If primary Then
            b.BackColor = Accent : b.ForeColor = Color.White
        End If
        Return b
    End Function

    Private Sub DrawRooms()
        Dim occ = S.Rooms.Where(Function(r) r.Holder IsNot Nothing).Count()
        lblVac.Text = $" {S.Rooms.Count - occ} vacant "
        lblOcc.Text = $" {occ} occupied "
        flpRooms.SuspendLayout()
        ClearPanel(flpRooms)
        For Each r In S.Rooms
            Dim isOcc = r.Holder IsNot Nothing
            Dim card As New Panel With {.Width = 170, .Height = 110, .Margin = New Padding(6),
                .BorderStyle = BorderStyle.FixedSingle,
                .BackColor = If(isOcc, RedBg, GreenBg)}
            Dim fg = If(isOcc, RedFg, GreenFg)
            Dim detail As String
            If isOcc Then
                Dim u = S.Users.FirstOrDefault(Function(x) x.Id = r.Holder)
                detail = "OCCUPIED" & vbCrLf & If(u IsNot Nothing, u.Name, r.Holder) & vbCrLf & "since " & Fmt(r.Since)
            Else
                detail = "VACANT" & vbCrLf & "Key available"
            End If
            Dim d As New Label With {.Dock = DockStyle.Fill, .Text = detail, .ForeColor = fg, .Padding = New Padding(8, 0, 4, 4)}
            Dim t As New Label With {.Dock = DockStyle.Top, .Height = 36, .Text = "Room " & r.Id, .ForeColor = fg,
                .Font = New Font("Segoe UI", 14, FontStyle.Bold), .Padding = New Padding(8, 6, 0, 0)}
            card.Controls.Add(d)
            card.Controls.Add(t)
            flpRooms.Controls.Add(card)
        Next
        flpRooms.ResumeLayout()
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        StartCam()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ResetScan()
    End Sub

    Private Sub btnManual_Click(sender As Object, e As EventArgs) Handles btnManual.Click
        Dim v = txtManual.Text.Trim()
        If v <> "" Then
            StopCam() : _locked = True
            HandleCode(v)
            txtManual.Clear()
        End If
    End Sub

    Private Sub LoadCams()
        cboCam.Items.Clear()
        _camMonikers.Clear()
        Try
            Dim devs As New FilterInfoCollection(FilterCategory.VideoInputDevice)
            Dim best = -1
            For i = 0 To devs.Count - 1
                cboCam.Items.Add(devs(i).Name)
                _camMonikers.Add(devs(i).MonikerString)
                Dim n = devs(i).Name.ToLowerInvariant()
                Dim skip = n.Contains("virtual") OrElse n.Contains("obs") OrElse n.Contains("snap") OrElse
                           n.Contains("droidcam") OrElse n.Contains("iriun") OrElse n.Contains("phone link") OrElse
                           n.Contains("ir camera") OrElse n.Contains(" ir ") OrElse n.Contains("infrared")
                If best = -1 AndAlso Not skip Then best = i
            Next
            If cboCam.Items.Count > 0 Then cboCam.SelectedIndex = If(best >= 0, best, 0)
        Catch
        End Try
    End Sub

    Private Sub ShowErr(msg As String)
        lblErr.Text = msg
    End Sub

    Private Sub ResetScan()
        StopCam()
        _locked = False : _step = 1 : _user = Nothing
        pnlConfirm.Visible = False : pnlScan.Visible = True
        lblErr.Text = ""
        lblStep.Text = If(_linkMode, $"Registering {_linkName}: scan their existing QR", "Step 1 of 2: scan the ID QR (faculty or OJT)")
        lblMsg.Text = "Camera is off"
        btnStart.Visible = True : btnStart.Text = "Start camera"
    End Sub

    Private Sub StartCam()
        StopCam()
        _locked = False : _gotFrame = False : lblErr.Text = ""
        Try
            If _camMonikers.Count = 0 Then LoadCams()
            If _camMonikers.Count = 0 Then
                ShowErr("No camera found. Use the manual code box.")
                Return
            End If
            Dim idx = If(cboCam.SelectedIndex >= 0, cboCam.SelectedIndex, 0)
            _cam = New VideoCaptureDevice(_camMonikers(idx))
            Dim resText = ""
            Try
                Dim caps = _cam.VideoCapabilities
                If caps IsNot Nothing AndAlso caps.Length > 0 Then
                    Dim pick = caps.Where(Function(c) c.FrameSize.Width <= 1280).
                                    OrderByDescending(Function(c) c.FrameSize.Width * c.FrameSize.Height).
                                    FirstOrDefault()
                    If pick Is Nothing Then pick = caps.OrderBy(Function(c) c.FrameSize.Width * c.FrameSize.Height).First()
                    _cam.VideoResolution = pick
                    resText = $" {pick.FrameSize.Width}x{pick.FrameSize.Height}"
                End If
            Catch
            End Try
            AddHandler _cam.NewFrame, AddressOf Cam_NewFrame
            AddHandler _cam.VideoSourceError, AddressOf Cam_Error
            _cam.Start()
            lblMsg.Text = $"Starting {cboCam.Items(idx)}{resText}…"
            btnStart.Visible = False
            camWatch.Stop() : camWatch.Start()
        Catch ex As Exception
            _cam = Nothing
            ShowErr("Camera error: " & ex.GetType().Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub Cam_Error(sender As Object, e As VideoSourceErrorEventArgs)
        Try
            BeginInvoke(Sub()
                            ShowErr("Camera error: " & e.Description)
                            btnStart.Visible = True : btnStart.Text = "Start camera"
                        End Sub)
        Catch
        End Try
    End Sub

    Private Sub StopCam()
        camWatch.Stop()
        If _cam Is Nothing Then Return
        Dim c = _cam
        _cam = Nothing
        Try
            RemoveHandler c.NewFrame, AddressOf Cam_NewFrame
            RemoveHandler c.VideoSourceError, AddressOf Cam_Error
            c.SignalToStop()
            c.WaitForStop()
        Catch
        End Try
    End Sub

    Private Sub Cam_NewFrame(sender As Object, e As NewFrameEventArgs)
        If _locked OrElse Not IsHandleCreated Then Return
        Dim bmp = DirectCast(e.Frame.Clone(), Bitmap)
        Dim txt As String = Nothing
        Try
            Dim r = _reader.Decode(bmp)
            If r IsNot Nothing Then txt = r.Text
        Catch
        End Try
        Try
            BeginInvoke(Sub() OnFrame(bmp, txt))
        Catch
            bmp.Dispose()
        End Try
    End Sub

    Private Sub OnFrame(bmp As Bitmap, txt As String)
        If _cam Is Nothing OrElse _locked Then
            bmp.Dispose()
            Return
        End If
        If Not _gotFrame Then
            _gotFrame = True
            camWatch.Stop()
            lblMsg.Text = "Point at the QR code…"
        End If
        Dim old = pic.Image
        pic.Image = bmp
        If old IsNot Nothing Then old.Dispose()
        If txt IsNot Nothing Then
            _locked = True
            StopCam()
            lblMsg.Text = "Scanned. Camera stopped."
            HandleCode(txt.Trim())
        End If
    End Sub

    Private Sub HandleCode(code As String)
        lblErr.Text = ""
        If _linkMode Then
            LinkExisting(code)
            Return
        End If
        If _step = 1 Then
            Dim u = S.Users.FirstOrDefault(Function(x) "USER:" & x.Id = code OrElse (Not String.IsNullOrEmpty(x.Code) AndAlso x.Code = code))
            If u Is Nothing Then
                ShowErr("Not a registered user ID. Register first, then scan again.")
                btnStart.Visible = True : btnStart.Text = "Scan again"
                Return
            End If
            _user = u : _step = 2
            lblStep.Text = "Step 2 of 2: scan the room key QR"
            lblMsg.Text = $"Hello, {u.Name} ({RoleName(u.Role)})"
            btnStart.Visible = True : btnStart.Text = "Scan key"
        Else
            Dim m = Regex.Match(code, "^ROOM:(.+)$")
            Dim room As Room = Nothing
            If m.Success Then room = S.Rooms.FirstOrDefault(Function(r) r.Id = m.Groups(1).Value)
            If room Is Nothing Then
                ShowErr("Not a valid room key QR.")
                btnStart.Visible = True : btnStart.Text = "Scan again"
                Return
            End If
            ConfirmPage(room, _user)
        End If
    End Sub

    Private Sub ConfirmPage(room As Room, u As User)
        pnlScan.Visible = False
        pnlConfirm.Visible = True
        ClearPanel(pnlConfirm)

        Dim mode As String, icon As String, title As String, extra As String, fg As Color
        If room.Holder Is Nothing Then
            mode = "borrow" : fg = GreenFg : icon = "🟢"
            title = $"Room {room.Id} is VACANT"
            extra = "Confirm to borrow this key."
        ElseIf room.Holder = u.Id Then
            mode = "return" : fg = RedFg : icon = "🔴"
            title = $"Room {room.Id} is OCCUPIED (by you)"
            extra = "Confirm to return the key and mark the room vacant."
        Else
            Dim h = S.Users.FirstOrDefault(Function(x) x.Id = room.Holder)
            mode = "blocked" : fg = RedFg : icon = "⛔"
            title = $"Room {room.Id} is OCCUPIED"
            extra = $"Key is with {If(h IsNot Nothing, h.Name, room.Holder)}. It cannot be borrowed."
        End If

        pnlConfirm.Controls.Add(NewLabel(icon, 32))
        pnlConfirm.Controls.Add(NewLabel(title, 16, True, fg))
        pnlConfirm.Controls.Add(NewLabel($"Name: {u.Name}" & vbCrLf & $"ID: {u.Id}" & vbCrLf &
                                         $"Type: {RoleName(u.Role)}" & vbCrLf & $"Room: {room.Id}", 11))
        pnlConfirm.Controls.Add(NewLabel(extra, 10))

        Dim row As New FlowLayoutPanel With {.AutoSize = True}
        If mode <> "blocked" Then
            Dim ok = NewButton(If(mode = "borrow", "Confirm borrow", "Confirm return"), True)
            AddHandler ok.Click,
                Sub()
                    If mode = "borrow" Then
                        room.Holder = u.Id : room.Since = DateTime.Now
                    Else
                        room.Holder = Nothing : room.Since = Nothing
                    End If
                    S.Log.Insert(0, New LogEntry With {.T = DateTime.Now, .Room = room.Id, .User = u.Name, .Id = u.Id, .Act = mode})
                    If S.Log.Count > 200 Then S.Log.RemoveRange(200, S.Log.Count - 200)
                    S.Save()

                    ClearPanel(pnlConfirm)
                    pnlConfirm.Controls.Add(NewLabel("✅", 32))
                    pnlConfirm.Controls.Add(NewLabel(If(mode = "borrow", "Key borrowed", "Key returned"), 16, True,
                                                     If(mode = "borrow", RedFg, GreenFg)))
                    pnlConfirm.Controls.Add(NewLabel($"Room {room.Id} is now " & If(mode = "borrow", "OCCUPIED.", "VACANT.")))
                    Dim done = NewButton("Done", True)
                    AddHandler done.Click, Sub() tabs.SelectedTab = tpRooms
                    pnlConfirm.Controls.Add(done)
                End Sub
            row.Controls.Add(ok)
        End If
        Dim no = NewButton(If(mode = "blocked", "Back", "Cancel"), False)
        AddHandler no.Click, Sub() ResetScan()
        row.Controls.Add(no)
        pnlConfirm.Controls.Add(row)
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim fullName = txtName.Text.Trim()
        If fullName = "" Then Return
        Dim role = If(cboRole.SelectedIndex = 0, "FAC", "OJT")
        Dim id = NextId(role)
        S.Users.Add(New User With {.Id = id, .Name = fullName, .Role = role})
        S.Save()
        txtName.Clear()
        If picOut.Image IsNot Nothing Then picOut.Image.Dispose()
        picOut.Image = MakeQr("USER:" & id, 8)
        lblOut.Text = $"{fullName}{vbCrLf}{id} · {RoleName(role)}"
        picOut.Visible = True : lblOut.Visible = True : btnSaveId.Visible = True
        DrawUsers()
    End Sub

    Private Sub btnExisting_Click(sender As Object, e As EventArgs) Handles btnExisting.Click
        Dim fullName = txtName.Text.Trim()
        If fullName = "" Then
            MessageBox.Show("Type the person's name first, then press Use existing QR.", "Use existing QR",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        _linkName = fullName
        _linkRole = If(cboRole.SelectedIndex = 0, "FAC", "OJT")
        _linkMode = True
        tabs.SelectedTab = tpScan
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If lvUsers.SelectedItems.Count = 0 Then
            MessageBox.Show("Select a person in the list first.", "Remove person",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim uid = lvUsers.SelectedItems(0).Text
        Dim u = S.Users.FirstOrDefault(Function(x) x.Id = uid)
        If u Is Nothing Then Return
        Dim held = S.Rooms.Where(Function(r) r.Holder = u.Id).Select(Function(r) r.Id).ToList()
        If held.Count > 0 Then
            MessageBox.Show($"{u.Name} still has the key to room {String.Join(", ", held)}. " &
                            "Return the key first, then remove the person.", "Remove person",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If MessageBox.Show($"Remove {u.Name} ({u.Id})?" & vbCrLf & "Their ID QR will stop working.",
                           "Remove person", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        If Not RequirePassword("remove a person") Then Return
        S.Users.Remove(u)
        S.Log.Insert(0, New LogEntry With {.T = DateTime.Now, .Room = "", .User = u.Name, .Id = u.Id, .Act = "person removed"})
        If S.Log.Count > 200 Then S.Log.RemoveRange(200, S.Log.Count - 200)
        S.Save()
        DrawUsers()
    End Sub

    Private Sub btnSaveId_Click(sender As Object, e As EventArgs) Handles btnSaveId.Click
        If picOut.Image Is Nothing Then Return
        Using dlg As New SaveFileDialog With {.Filter = "PNG image|*.png", .FileName = "id-qr.png"}
            If dlg.ShowDialog() = DialogResult.OK Then picOut.Image.Save(dlg.FileName, Imaging.ImageFormat.Png)
        End Using
    End Sub

    Private Function NextId(role As String) As String
        Dim last = 0
        S.Counters.TryGetValue(role, last)
        Dim maxExisting = S.Users.Where(Function(x) x.Role = role).
                                  Select(Function(x) IdNum(x.Id)).DefaultIfEmpty(0).Max()
        Dim n = Math.Max(last, maxExisting) + 1
        S.Counters(role) = n
        Return role & "-" & n.ToString().PadLeft(3, "0"c)
    End Function

    Private Sub LinkExisting(code As String)
        If code.StartsWith("ROOM:", StringComparison.OrdinalIgnoreCase) Then
            ShowErr("That is a room key QR, not a person's QR.")
            btnStart.Visible = True : btnStart.Text = "Scan again"
            Return
        End If
        Dim taken = S.Users.FirstOrDefault(Function(x) "USER:" & x.Id = code OrElse (Not String.IsNullOrEmpty(x.Code) AndAlso x.Code = code))
        If taken IsNot Nothing Then
            ShowErr($"This QR is already registered to {taken.Name}.")
            btnStart.Visible = True : btnStart.Text = "Scan again"
            Return
        End If
        Dim id = NextId(_linkRole)
        S.Users.Add(New User With {.Id = id, .Name = _linkName, .Role = _linkRole, .Code = code})
        S.Save()
        Dim who = _linkName
        _linkMode = False
        _linkName = ""
        MessageBox.Show($"{who} is registered as {id}. Their existing QR now works for scanning.", "Use existing QR",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        txtName.Clear()
        tabsAdmin.SelectedTab = tpRegister
        tabs.SelectedTab = tpAdmin
    End Sub

    Private Shared Function IdNum(id As String) As Integer
        Dim n As Integer = 0
        Dim p = id.LastIndexOf("-"c)
        If p >= 0 Then Integer.TryParse(id.Substring(p + 1), n)
        Return n
    End Function

    Private Sub DrawUsers()
        lvUsers.Items.Clear()
        For Each u In S.Users
            lvUsers.Items.Add(New ListViewItem({u.Id, u.Name, RoleName(u.Role), If(String.IsNullOrEmpty(u.Code), "Generated", "Existing")}))
        Next
    End Sub

    Private Sub btnAddRoom_Click(sender As Object, e As EventArgs) Handles btnAddRoom.Click
        Dim id = Regex.Replace(txtRoom.Text.Trim(), "[^\w\- ]", "")
        If id = "" OrElse S.Rooms.Any(Function(r) r.Id = id) Then Return
        S.Rooms.Add(New Room With {.Id = id})
        S.Save() : txtRoom.Clear() : DrawKeys()
    End Sub

    Private Sub btnRemoveRoom_Click(sender As Object, e As EventArgs) Handles btnRemoveRoom.Click
        Dim id = TryCast(cboRemoveRoom.SelectedItem, String)
        If String.IsNullOrEmpty(id) Then
            MessageBox.Show("Choose a room to remove first.", "Remove room",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim room = S.Rooms.FirstOrDefault(Function(r) r.Id = id)
        If room Is Nothing Then Return
        If S.Rooms.Count <= 1 Then
            MessageBox.Show("At least one room must remain, so the last room cannot be removed.", "Remove room",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If room.Holder IsNot Nothing Then
            Dim h = S.Users.FirstOrDefault(Function(x) x.Id = room.Holder)
            MessageBox.Show($"The key to room {id} is still borrowed by {If(h IsNot Nothing, h.Name, room.Holder)}. " &
                            "Return the key first, then remove the room.", "Remove room",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If MessageBox.Show($"Remove room {id}?" & vbCrLf & "Its key QR will stop working. Earlier log entries for this room are kept.",
                           "Remove room", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        If Not RequirePassword("remove a room") Then Return
        S.Rooms.Remove(room)
        S.Log.Insert(0, New LogEntry With {.T = DateTime.Now, .Room = id, .User = "Admin", .Id = "", .Act = "room removed"})
        If S.Log.Count > 200 Then S.Log.RemoveRange(200, S.Log.Count - 200)
        S.Save()
        DrawKeys()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintKeys()
    End Sub

    Private Sub DrawKeys()
        cboRemoveRoom.Items.Clear()
        For Each rm In S.Rooms
            cboRemoveRoom.Items.Add(rm.Id)
        Next
        If cboRemoveRoom.Items.Count > 0 Then cboRemoveRoom.SelectedIndex = 0
        flpKeys.SuspendLayout()
        ClearPanel(flpKeys)
        For Each r In S.Rooms
            Dim card As New Panel With {.Width = 160, .Height = 190, .Margin = New Padding(6),
                                        .BorderStyle = BorderStyle.FixedSingle, .BackColor = Color.White}
            Dim lbl As New Label With {.Dock = DockStyle.Bottom, .Height = 28, .TextAlign = ContentAlignment.MiddleCenter,
                                       .Text = "Room " & r.Id, .Font = New Font("Segoe UI", 10, FontStyle.Bold)}
            Dim pb As New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.Zoom,
                                           .Image = MakeQr("ROOM:" & r.Id, 6)}
            card.Controls.Add(pb)
            card.Controls.Add(lbl)
            flpKeys.Controls.Add(card)
        Next
        flpKeys.ResumeLayout()
    End Sub

    Private Sub PrintKeys()
        Dim idx = 0
        Dim ids = S.Rooms.Select(Function(r) r.Id).ToList()
        Using pd As New PrintDocument()
            AddHandler pd.PrintPage,
                Sub(sender, e)
                    Dim g = e.Graphics
                    g.InterpolationMode = InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = PixelOffsetMode.Half
                    Dim cellW = 200, cellH = 230
                    Dim cols = Math.Max(1, e.MarginBounds.Width \ cellW)
                    Dim col = 0, y = e.MarginBounds.Top
                    e.HasMorePages = False
                    Using f As New Font("Segoe UI", 12, FontStyle.Bold)
                        While idx < ids.Count
                            If col = 0 AndAlso y + cellH > e.MarginBounds.Bottom Then
                                e.HasMorePages = True
                                Return
                            End If
                            Dim x = e.MarginBounds.Left + col * cellW
                            Using bmp = MakeQr("ROOM:" & ids(idx), 8)
                                g.DrawImage(bmp, x + 20, y, 160, 160)
                            End Using
                            g.DrawString("Room " & ids(idx), f, Brushes.Black, x + 55, y + 165)
                            col += 1
                            If col >= cols Then col = 0 : y += cellH
                            idx += 1
                        End While
                    End Using
                End Sub
            Using dlg As New PrintPreviewDialog With {.Document = pd, .Width = 900, .Height = 700}
                dlg.ShowDialog()
            End Using
        End Using
    End Sub

    Private Function PasswordDialog(title As String, prompt As String, confirm As Boolean) As String
        Using f As New Form With {.Text = title, .FormBorderStyle = FormBorderStyle.FixedDialog,
                                  .StartPosition = FormStartPosition.CenterParent,
                                  .MaximizeBox = False, .MinimizeBox = False, .ShowInTaskbar = False,
                                  .ClientSize = New Size(380, If(confirm, 190, 130))}
            Dim l As New Label With {.Text = prompt, .Left = 12, .Top = 12, .Width = 356, .Height = 22}
            Dim t1 As New TextBox With {.Left = 12, .Top = 38, .Width = 356, .UseSystemPasswordChar = True}
            Dim t2 As TextBox = Nothing
            Dim y = 72
            f.Controls.AddRange({l, t1})
            If confirm Then
                t2 = New TextBox With {.Left = 12, .Top = 72, .Width = 356, .UseSystemPasswordChar = True,
                                       .PlaceholderText = "Type it again to confirm"}
                f.Controls.Add(t2)
                y = 106
            End If
            Dim ok As New Button With {.Text = "OK", .Left = 212, .Top = y, .Width = 75, .DialogResult = DialogResult.OK}
            Dim cancel As New Button With {.Text = "Cancel", .Left = 293, .Top = y, .Width = 75, .DialogResult = DialogResult.Cancel}
            f.Controls.AddRange({ok, cancel})
            f.AcceptButton = ok : f.CancelButton = cancel
            ' Cover the whole screen with a plain white window behind the password box
            Dim cover As New Form With {.FormBorderStyle = FormBorderStyle.None, .BackColor = Color.White,
                                        .ShowInTaskbar = False, .StartPosition = FormStartPosition.Manual,
                                        .Bounds = Screen.FromControl(Me).Bounds}
            cover.Show(Me)
            Try
            Do
                If f.ShowDialog(cover) <> DialogResult.OK Then Return Nothing
                If t1.Text.Length = 0 Then Continue Do
                If confirm Then
                    If t1.Text.Length < 4 Then
                        MessageBox.Show("Use at least 4 characters.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Continue Do
                    End If
                    If t1.Text <> t2.Text Then
                        MessageBox.Show("The two passwords do not match.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Continue Do
                    End If
                End If
                Return t1.Text
            Loop
            Finally
                cover.Close()
                cover.Dispose()
            End Try
        End Using
    End Function

    Private Function RequirePassword(action As String) As Boolean
        If _adminUnlocked Then Return True
        If Not S.HasPassword() Then
            Dim np = PasswordDialog("Set admin password",
                                    "No admin password is set yet. Create one to " & action & ":", True)
            If np Is Nothing Then Return False
            S.SetPassword(np) : S.Save()
            MessageBox.Show("Admin password saved. Keep it safe: it is needed to open the Admin page or clear the log.",
                            "Admin password", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        End If
        Dim pw = PasswordDialog("Admin password", "Enter the admin password to " & action & ":", False)
        If pw Is Nothing Then Return False
        If S.CheckPassword(pw) Then Return True
        MessageBox.Show("Incorrect password.", "Admin password", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Return False
    End Function

    Private Sub btnChangePw_Click(sender As Object, e As EventArgs) Handles btnChangePw.Click
        If S.HasPassword() Then
            Dim cur = PasswordDialog("Change password", "Enter the current admin password:", False)
            If cur Is Nothing Then Return
            If Not S.CheckPassword(cur) Then
                MessageBox.Show("Incorrect password.", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If
        Dim np = PasswordDialog("Change password", "Enter the new admin password:", True)
        If np Is Nothing Then Return
        S.SetPassword(np) : S.Save()
        MessageBox.Show("Admin password changed.", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnClearLog_Click(sender As Object, e As EventArgs) Handles btnClearLog.Click
        If S.Log.Count = 0 Then Return
        If Not RequirePassword("clear the log") Then Return
        If MessageBox.Show("Clear all log entries? This cannot be undone.", "Clear log",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            S.Log.Clear()
            S.Save()
            DrawLog()
        End If
    End Sub

    Private Shared Function ActText(act As String) As String
        Select Case act
            Case "borrow" : Return "Borrowed"
            Case "return" : Return "Returned"
            Case "room removed" : Return "Room removed"
            Case "person removed" : Return "Person removed"
            Case Else : Return act
        End Select
    End Function

    Private Sub DrawLog()
        lvLog.Items.Clear()
        For Each l In S.Log
            lvLog.Items.Add(New ListViewItem({Fmt(l.T), l.Room, l.User, ActText(l.Act)}))
        Next
    End Sub

End Class
