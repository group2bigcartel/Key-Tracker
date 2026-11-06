<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        tabs = New TabControl()
        tpRooms = New TabPage()
        flpRooms = New FlowLayoutPanel()
        flpRoomsTop = New FlowLayoutPanel()
        lblVac = New Label()
        lblOcc = New Label()
        tpScan = New TabPage()
        pnlScan = New FlowLayoutPanel()
        lblStep = New Label()
        pic = New PictureBox()
        lblMsg = New Label()
        cboCam = New ComboBox()
        lblErr = New Label()
        flpScanBtns = New FlowLayoutPanel()
        btnStart = New Button()
        btnReset = New Button()
        flpManual = New FlowLayoutPanel()
        txtManual = New TextBox()
        btnManual = New Button()
        tpRegister = New TabPage()
        lvUsers = New ListView()
        colUserId = New ColumnHeader()
        colUserName = New ColumnHeader()
        colUserType = New ColumnHeader()
        colUserQr = New ColumnHeader()
        flpRegTop = New FlowLayoutPanel()
        flpRegRow = New FlowLayoutPanel()
        txtName = New TextBox()
        cboRole = New ComboBox()
        btnGenerate = New Button()
        btnExisting = New Button()
        btnRemove = New Button()
        picOut = New PictureBox()
        lblOut = New Label()
        btnSaveId = New Button()
        tpKeys = New TabPage()
        flpKeys = New FlowLayoutPanel()
        flpKeysTop = New FlowLayoutPanel()
        txtRoom = New TextBox()
        btnAddRoom = New Button()
        btnPrint = New Button()
        cboRemoveRoom = New ComboBox()
        btnRemoveRoom = New Button()
        tpLog = New TabPage()
        tpAdmin = New TabPage()
        tpTools = New TabPage()
        tabsAdmin = New TabControl()
        lvLog = New ListView()
        colWhen = New ColumnHeader()
        colRoom = New ColumnHeader()
        colWho = New ColumnHeader()
        colAction = New ColumnHeader()
        flpLogTop = New FlowLayoutPanel()
        btnClearLog = New Button()
        btnChangePw = New Button()
        tabs.SuspendLayout()
        tpRooms.SuspendLayout()
        flpRoomsTop.SuspendLayout()
        tpScan.SuspendLayout()
        pnlScan.SuspendLayout()
        CType(pic, ComponentModel.ISupportInitialize).BeginInit()
        flpScanBtns.SuspendLayout()
        flpManual.SuspendLayout()
        tpRegister.SuspendLayout()
        flpRegTop.SuspendLayout()
        flpRegRow.SuspendLayout()
        CType(picOut, ComponentModel.ISupportInitialize).BeginInit()
        tpKeys.SuspendLayout()
        flpKeysTop.SuspendLayout()
        tpLog.SuspendLayout()
        tpAdmin.SuspendLayout()
        tpTools.SuspendLayout()
        tabsAdmin.SuspendLayout()
        flpLogTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' tabs
        ' 
        tabs.Controls.Add(tpRooms)
        tabs.Controls.Add(tpScan)
        tabs.Controls.Add(tpLog)
        tabs.Controls.Add(tpAdmin)
        tabs.Dock = DockStyle.Fill
        tabs.Location = New Point(0, 0)
        tabs.Name = "tabs"
        tabs.SelectedIndex = 0
        tabs.Size = New Size(944, 681)
        tabs.TabIndex = 0
        ' 
        ' tpRooms
        ' 
        tpRooms.Controls.Add(flpRooms)
        tpRooms.Controls.Add(flpRoomsTop)
        tpRooms.Location = New Point(4, 32)
        tpRooms.Name = "tpRooms"
        tpRooms.Padding = New Padding(8)
        tpRooms.Size = New Size(936, 645)
        tpRooms.TabIndex = 0
        tpRooms.Text = "Rooms"
        tpRooms.UseVisualStyleBackColor = True
        ' 
        ' flpRooms
        ' 
        flpRooms.AutoScroll = True
        flpRooms.Dock = DockStyle.Fill
        flpRooms.Location = New Point(8, 52)
        flpRooms.Name = "flpRooms"
        flpRooms.Size = New Size(920, 585)
        flpRooms.TabIndex = 1
        ' 
        ' flpRoomsTop
        ' 
        flpRoomsTop.Controls.Add(lblVac)
        flpRoomsTop.Controls.Add(lblOcc)
        flpRoomsTop.Dock = DockStyle.Top
        flpRoomsTop.Location = New Point(8, 8)
        flpRoomsTop.Name = "flpRoomsTop"
        flpRoomsTop.Size = New Size(920, 44)
        flpRoomsTop.TabIndex = 0
        ' 
        ' lblVac
        ' 
        lblVac.AutoSize = True
        lblVac.BackColor = Color.FromArgb(CByte(220), CByte(252), CByte(231))
        lblVac.Font = New Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblVac.ForeColor = Color.FromArgb(CByte(21), CByte(128), CByte(61))
        lblVac.Location = New Point(3, 4)
        lblVac.Margin = New Padding(3, 4, 3, 4)
        lblVac.Name = "lblVac"
        lblVac.Size = New Size(87, 23)
        lblVac.TabIndex = 0
        lblVac.Text = " 6 vacant "
        ' 
        ' lblOcc
        ' 
        lblOcc.AutoSize = True
        lblOcc.BackColor = Color.FromArgb(CByte(254), CByte(226), CByte(226))
        lblOcc.Font = New Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblOcc.ForeColor = Color.FromArgb(CByte(185), CByte(28), CByte(28))
        lblOcc.Location = New Point(96, 4)
        lblOcc.Margin = New Padding(3, 4, 3, 4)
        lblOcc.Name = "lblOcc"
        lblOcc.Size = New Size(107, 23)
        lblOcc.TabIndex = 1
        lblOcc.Text = " 0 occupied "
        ' 
        ' tpScan
        ' 
        tpScan.Controls.Add(pnlScan)
        tpScan.Location = New Point(4, 32)
        tpScan.Name = "tpScan"
        tpScan.Padding = New Padding(8)
        tpScan.Size = New Size(936, 645)
        tpScan.TabIndex = 1
        tpScan.Text = "Scan"
        tpScan.UseVisualStyleBackColor = True
        ' 
        ' pnlScan
        ' 
        pnlScan.AutoScroll = True
        pnlScan.Controls.Add(lblStep)
        pnlScan.Controls.Add(pic)
        pnlScan.Controls.Add(lblMsg)
        pnlScan.Controls.Add(cboCam)
        pnlScan.Controls.Add(lblErr)
        pnlScan.Controls.Add(flpScanBtns)
        pnlScan.Controls.Add(flpManual)
        pnlScan.Dock = DockStyle.Fill
        pnlScan.FlowDirection = FlowDirection.TopDown
        pnlScan.Location = New Point(8, 8)
        pnlScan.Name = "pnlScan"
        pnlScan.Size = New Size(920, 629)
        pnlScan.TabIndex = 0
        pnlScan.WrapContents = False
        ' 
        ' lblStep
        ' 
        lblStep.AutoSize = True
        lblStep.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblStep.ForeColor = Color.FromArgb(CByte(36), CByte(86), CByte(214))
        lblStep.Location = New Point(3, 4)
        lblStep.Margin = New Padding(3, 4, 3, 4)
        lblStep.Name = "lblStep"
        lblStep.Size = New Size(388, 25)
        lblStep.TabIndex = 0
        lblStep.Text = "Step 1 of 2: scan the ID QR (faculty or OJT)"
        ' 
        ' pic
        ' 
        pic.BackColor = Color.Black
        pic.Location = New Point(3, 36)
        pic.Name = "pic"
        pic.Size = New Size(380, 285)
        pic.SizeMode = PictureBoxSizeMode.Zoom
        pic.TabIndex = 1
        pic.TabStop = False
        ' 
        ' lblMsg
        ' 
        lblMsg.AutoSize = True
        lblMsg.Location = New Point(3, 328)
        lblMsg.Margin = New Padding(3, 4, 3, 4)
        lblMsg.Name = "lblMsg"
        lblMsg.Size = New Size(110, 23)
        lblMsg.TabIndex = 2
        lblMsg.Text = "Camera is off"
        ' 
        ' cboCam
        ' 
        cboCam.DropDownStyle = ComboBoxStyle.DropDownList
        cboCam.FormattingEnabled = True
        cboCam.Location = New Point(3, 358)
        cboCam.Name = "cboCam"
        cboCam.Size = New Size(380, 31)
        cboCam.TabIndex = 3
        ' 
        ' lblErr
        ' 
        lblErr.AutoSize = True
        lblErr.Font = New Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblErr.ForeColor = Color.FromArgb(CByte(185), CByte(28), CByte(28))
        lblErr.Location = New Point(3, 396)
        lblErr.Margin = New Padding(3, 4, 3, 4)
        lblErr.Name = "lblErr"
        lblErr.Size = New Size(228, 23)
        lblErr.TabIndex = 4
        lblErr.Text = "(error messages show here)"
        ' 
        ' flpScanBtns
        ' 
        flpScanBtns.AutoSize = True
        flpScanBtns.Controls.Add(btnStart)
        flpScanBtns.Controls.Add(btnReset)
        flpScanBtns.Location = New Point(3, 426)
        flpScanBtns.Name = "flpScanBtns"
        flpScanBtns.Size = New Size(245, 49)
        flpScanBtns.TabIndex = 5
        ' 
        ' btnStart
        ' 
        btnStart.AutoSize = True
        btnStart.BackColor = Color.FromArgb(CByte(36), CByte(86), CByte(214))
        btnStart.FlatStyle = FlatStyle.Flat
        btnStart.ForeColor = Color.White
        btnStart.Location = New Point(3, 3)
        btnStart.Name = "btnStart"
        btnStart.Padding = New Padding(10, 4, 10, 4)
        btnStart.Size = New Size(138, 43)
        btnStart.TabIndex = 0
        btnStart.Text = "Start camera"
        btnStart.UseVisualStyleBackColor = False
        ' 
        ' btnReset
        ' 
        btnReset.AutoSize = True
        btnReset.FlatStyle = FlatStyle.Flat
        btnReset.Location = New Point(147, 3)
        btnReset.Name = "btnReset"
        btnReset.Padding = New Padding(10, 4, 10, 4)
        btnReset.Size = New Size(95, 43)
        btnReset.TabIndex = 1
        btnReset.Text = "Restart"
        btnReset.UseVisualStyleBackColor = True
        ' 
        ' flpManual
        ' 
        flpManual.AutoSize = True
        flpManual.Controls.Add(txtManual)
        flpManual.Controls.Add(btnManual)
        flpManual.Location = New Point(3, 481)
        flpManual.Name = "flpManual"
        flpManual.Size = New Size(368, 49)
        flpManual.TabIndex = 6
        ' 
        ' txtManual
        ' 
        txtManual.Location = New Point(3, 3)
        txtManual.Name = "txtManual"
        txtManual.PlaceholderText = "No camera? e.g. USER:FAC-001 or ROOM:101"
        txtManual.Size = New Size(260, 30)
        txtManual.TabIndex = 0
        ' 
        ' btnManual
        ' 
        btnManual.AutoSize = True
        btnManual.FlatStyle = FlatStyle.Flat
        btnManual.Location = New Point(269, 3)
        btnManual.Name = "btnManual"
        btnManual.Padding = New Padding(10, 4, 10, 4)
        btnManual.Size = New Size(96, 43)
        btnManual.TabIndex = 1
        btnManual.Text = "Submit"
        btnManual.UseVisualStyleBackColor = True
        ' 
        ' tpAdmin
        ' 
        tpAdmin.Controls.Add(tabsAdmin)
        tpAdmin.Location = New Point(4, 32)
        tpAdmin.Name = "tpAdmin"
        tpAdmin.Padding = New Padding(8)
        tpAdmin.Size = New Size(936, 645)
        tpAdmin.TabIndex = 5
        tpAdmin.Text = "Admin"
        tpAdmin.UseVisualStyleBackColor = True
        ' 
        ' tabsAdmin
        ' 
        tabsAdmin.Controls.Add(tpKeys)
        tabsAdmin.Controls.Add(tpRegister)
        tabsAdmin.Controls.Add(tpTools)
        tabsAdmin.Dock = DockStyle.Fill
        tabsAdmin.Location = New Point(8, 8)
        tabsAdmin.Name = "tabsAdmin"
        tabsAdmin.SelectedIndex = 0
        tabsAdmin.Size = New Size(920, 629)
        tabsAdmin.TabIndex = 0
        ' 
        ' tpRegister
        ' 
        tpRegister.Controls.Add(lvUsers)
        tpRegister.Controls.Add(flpRegTop)
        tpRegister.Location = New Point(4, 32)
        tpRegister.Name = "tpRegister"
        tpRegister.Padding = New Padding(8)
        tpRegister.Size = New Size(936, 645)
        tpRegister.TabIndex = 1
        tpRegister.Text = "Register"
        tpRegister.UseVisualStyleBackColor = True
        ' 
        ' lvUsers
        ' 
        lvUsers.Columns.AddRange(New ColumnHeader() {colUserId, colUserName, colUserType, colUserQr})
        lvUsers.Dock = DockStyle.Fill
        lvUsers.FullRowSelect = True
        lvUsers.Location = New Point(8, 174)
        lvUsers.Name = "lvUsers"
        lvUsers.Size = New Size(920, 463)
        lvUsers.TabIndex = 1
        lvUsers.UseCompatibleStateImageBehavior = False
        lvUsers.View = View.Details
        ' 
        ' colUserId
        ' 
        colUserId.Text = "ID"
        colUserId.Width = 100
        ' 
        ' colUserName
        ' 
        colUserName.Text = "Name"
        colUserName.Width = 300
        ' 
        ' colUserType
        ' 
        colUserType.Text = "Type"
        colUserType.Width = 100
        ' 
        ' colUserQr
        ' 
        colUserQr.Text = "QR"
        colUserQr.Width = 100
        ' 
        ' flpRegTop
        ' 
        flpRegTop.AutoSize = True
        flpRegTop.Controls.Add(flpRegRow)
        flpRegTop.Controls.Add(picOut)
        flpRegTop.Controls.Add(lblOut)
        flpRegTop.Controls.Add(btnSaveId)
        flpRegTop.Dock = DockStyle.Top
        flpRegTop.FlowDirection = FlowDirection.TopDown
        flpRegTop.Location = New Point(8, 8)
        flpRegTop.Name = "flpRegTop"
        flpRegTop.Size = New Size(920, 166)
        flpRegTop.TabIndex = 0
        ' 
        ' flpRegRow
        ' 
        flpRegRow.AutoSize = True
        flpRegRow.Controls.Add(txtName)
        flpRegRow.Controls.Add(cboRole)
        flpRegRow.Controls.Add(btnGenerate)
        flpRegRow.Controls.Add(btnExisting)
        flpRegRow.Controls.Add(btnRemove)
        flpRegRow.Location = New Point(3, 3)
        flpRegRow.Name = "flpRegRow"
        flpRegRow.Size = New Size(855, 49)
        flpRegRow.TabIndex = 0
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(3, 3)
        txtName.Name = "txtName"
        txtName.PlaceholderText = "Full name"
        txtName.Size = New Size(260, 30)
        txtName.TabIndex = 0
        ' 
        ' cboRole
        ' 
        cboRole.DropDownStyle = ComboBoxStyle.DropDownList
        cboRole.FormattingEnabled = True
        cboRole.Items.AddRange(New Object() {"Faculty", "OJT"})
        cboRole.Location = New Point(269, 3)
        cboRole.Name = "cboRole"
        cboRole.Size = New Size(100, 31)
        cboRole.TabIndex = 1
        ' 
        ' btnGenerate
        ' 
        btnGenerate.AutoSize = True
        btnGenerate.BackColor = Color.FromArgb(CByte(36), CByte(86), CByte(214))
        btnGenerate.FlatStyle = FlatStyle.Flat
        btnGenerate.ForeColor = Color.White
        btnGenerate.Location = New Point(375, 3)
        btnGenerate.Name = "btnGenerate"
        btnGenerate.Padding = New Padding(10, 4, 10, 4)
        btnGenerate.Size = New Size(134, 43)
        btnGenerate.TabIndex = 2
        btnGenerate.Text = "Generate ID"
        btnGenerate.UseVisualStyleBackColor = False
        ' 
        ' btnExisting
        ' 
        btnExisting.AutoSize = True
        btnExisting.FlatStyle = FlatStyle.Flat
        btnExisting.Location = New Point(515, 3)
        btnExisting.Name = "btnExisting"
        btnExisting.Padding = New Padding(10, 4, 10, 4)
        btnExisting.Size = New Size(161, 43)
        btnExisting.TabIndex = 3
        btnExisting.Text = "Use existing QR"
        btnExisting.UseVisualStyleBackColor = True
        ' 
        ' btnRemove
        ' 
        btnRemove.AutoSize = True
        btnRemove.FlatStyle = FlatStyle.Flat
        btnRemove.Location = New Point(682, 3)
        btnRemove.Name = "btnRemove"
        btnRemove.Padding = New Padding(10, 4, 10, 4)
        btnRemove.Size = New Size(170, 43)
        btnRemove.TabIndex = 4
        btnRemove.Text = "Remove selected"
        btnRemove.UseVisualStyleBackColor = True
        ' 
        ' picOut
        ' 
        picOut.Location = New Point(864, 3)
        picOut.Name = "picOut"
        picOut.Size = New Size(160, 160)
        picOut.SizeMode = PictureBoxSizeMode.Zoom
        picOut.TabIndex = 1
        picOut.TabStop = False
        picOut.Visible = False
        ' 
        ' lblOut
        ' 
        lblOut.AutoSize = True
        lblOut.Font = New Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblOut.Location = New Point(1030, 4)
        lblOut.Margin = New Padding(3, 4, 3, 4)
        lblOut.Name = "lblOut"
        lblOut.Size = New Size(115, 23)
        lblOut.TabIndex = 2
        lblOut.Text = "Name and ID"
        lblOut.Visible = False
        ' 
        ' btnSaveId
        ' 
        btnSaveId.AutoSize = True
        btnSaveId.FlatStyle = FlatStyle.Flat
        btnSaveId.Location = New Point(1030, 34)
        btnSaveId.Name = "btnSaveId"
        btnSaveId.Padding = New Padding(10, 4, 10, 4)
        btnSaveId.Size = New Size(166, 43)
        btnSaveId.TabIndex = 3
        btnSaveId.Text = "Save QR as PNG"
        btnSaveId.UseVisualStyleBackColor = True
        btnSaveId.Visible = False
        ' 
        ' tpKeys
        ' 
        tpKeys.Controls.Add(flpKeys)
        tpKeys.Controls.Add(flpKeysTop)
        tpKeys.Location = New Point(4, 32)
        tpKeys.Name = "tpKeys"
        tpKeys.Padding = New Padding(8)
        tpKeys.Size = New Size(936, 645)
        tpKeys.TabIndex = 0
        tpKeys.Text = "Key QRs"
        tpKeys.UseVisualStyleBackColor = True
        ' 
        ' flpKeys
        ' 
        flpKeys.AutoScroll = True
        flpKeys.Dock = DockStyle.Fill
        flpKeys.Location = New Point(8, 54)
        flpKeys.Name = "flpKeys"
        flpKeys.Size = New Size(920, 583)
        flpKeys.TabIndex = 1
        ' 
        ' flpKeysTop
        ' 
        flpKeysTop.Controls.Add(txtRoom)
        flpKeysTop.Controls.Add(btnAddRoom)
        flpKeysTop.Controls.Add(btnPrint)
        flpKeysTop.Controls.Add(cboRemoveRoom)
        flpKeysTop.Controls.Add(btnRemoveRoom)
        flpKeysTop.Dock = DockStyle.Top
        flpKeysTop.Location = New Point(8, 8)
        flpKeysTop.Name = "flpKeysTop"
        flpKeysTop.Size = New Size(920, 46)
        flpKeysTop.TabIndex = 0
        ' 
        ' txtRoom
        ' 
        txtRoom.Location = New Point(3, 3)
        txtRoom.Name = "txtRoom"
        txtRoom.PlaceholderText = "Room number, e.g. 107"
        txtRoom.Size = New Size(220, 30)
        txtRoom.TabIndex = 0
        ' 
        ' btnAddRoom
        ' 
        btnAddRoom.AutoSize = True
        btnAddRoom.BackColor = Color.FromArgb(CByte(36), CByte(86), CByte(214))
        btnAddRoom.FlatStyle = FlatStyle.Flat
        btnAddRoom.ForeColor = Color.White
        btnAddRoom.Location = New Point(229, 3)
        btnAddRoom.Name = "btnAddRoom"
        btnAddRoom.Padding = New Padding(10, 4, 10, 4)
        btnAddRoom.Size = New Size(119, 43)
        btnAddRoom.TabIndex = 1
        btnAddRoom.Text = "Add room"
        btnAddRoom.UseVisualStyleBackColor = False
        ' 
        ' btnPrint
        ' 
        btnPrint.AutoSize = True
        btnPrint.FlatStyle = FlatStyle.Flat
        btnPrint.Location = New Point(354, 3)
        btnPrint.Name = "btnPrint"
        btnPrint.Padding = New Padding(10, 4, 10, 4)
        btnPrint.Size = New Size(90, 43)
        btnPrint.TabIndex = 2
        btnPrint.Text = "Print…"
        btnPrint.UseVisualStyleBackColor = True
        ' 
        ' cboRemoveRoom
        ' 
        cboRemoveRoom.DropDownStyle = ComboBoxStyle.DropDownList
        cboRemoveRoom.FormattingEnabled = True
        cboRemoveRoom.Location = New Point(450, 3)
        cboRemoveRoom.Name = "cboRemoveRoom"
        cboRemoveRoom.Size = New Size(120, 31)
        cboRemoveRoom.TabIndex = 3
        ' 
        ' btnRemoveRoom
        ' 
        btnRemoveRoom.AutoSize = True
        btnRemoveRoom.FlatStyle = FlatStyle.Flat
        btnRemoveRoom.Location = New Point(576, 3)
        btnRemoveRoom.Name = "btnRemoveRoom"
        btnRemoveRoom.Padding = New Padding(10, 4, 10, 4)
        btnRemoveRoom.Size = New Size(149, 43)
        btnRemoveRoom.TabIndex = 4
        btnRemoveRoom.Text = "Remove room"
        btnRemoveRoom.UseVisualStyleBackColor = True
        ' 
        ' tpTools
        ' 
        tpTools.Controls.Add(flpLogTop)
        tpTools.Location = New Point(4, 32)
        tpTools.Name = "tpTools"
        tpTools.Padding = New Padding(8)
        tpTools.Size = New Size(936, 645)
        tpTools.TabIndex = 2
        tpTools.Text = "Log && Password"
        tpTools.UseVisualStyleBackColor = True
        ' 
        ' tpLog
        ' 
        tpLog.Controls.Add(lvLog)
        tpLog.Location = New Point(4, 32)
        tpLog.Name = "tpLog"
        tpLog.Padding = New Padding(8)
        tpLog.Size = New Size(936, 645)
        tpLog.TabIndex = 2
        tpLog.Text = "Log"
        tpLog.UseVisualStyleBackColor = True
        ' 
        ' lvLog
        ' 
        lvLog.Columns.AddRange(New ColumnHeader() {colWhen, colRoom, colWho, colAction})
        lvLog.Dock = DockStyle.Fill
        lvLog.FullRowSelect = True
        lvLog.Location = New Point(8, 52)
        lvLog.Name = "lvLog"
        lvLog.Size = New Size(920, 585)
        lvLog.TabIndex = 1
        lvLog.UseCompatibleStateImageBehavior = False
        lvLog.View = View.Details
        ' 
        ' colWhen
        ' 
        colWhen.Text = "When"
        colWhen.Width = 180
        ' 
        ' colRoom
        ' 
        colRoom.Text = "Room"
        colRoom.Width = 80
        ' 
        ' colWho
        ' 
        colWho.Text = "Who"
        colWho.Width = 260
        ' 
        ' colAction
        ' 
        colAction.Text = "Action"
        colAction.Width = 100
        ' 
        ' flpLogTop
        ' 
        flpLogTop.Controls.Add(btnClearLog)
        flpLogTop.Controls.Add(btnChangePw)
        flpLogTop.Dock = DockStyle.Top
        flpLogTop.Location = New Point(8, 8)
        flpLogTop.Name = "flpLogTop"
        flpLogTop.Size = New Size(920, 44)
        flpLogTop.TabIndex = 0
        ' 
        ' btnClearLog
        ' 
        btnClearLog.AutoSize = True
        btnClearLog.FlatStyle = FlatStyle.Flat
        btnClearLog.Location = New Point(3, 3)
        btnClearLog.Name = "btnClearLog"
        btnClearLog.Padding = New Padding(10, 4, 10, 4)
        btnClearLog.Size = New Size(110, 43)
        btnClearLog.TabIndex = 0
        btnClearLog.Text = "Clear log"
        btnClearLog.UseVisualStyleBackColor = True
        ' 
        ' btnChangePw
        ' 
        btnChangePw.AutoSize = True
        btnChangePw.FlatStyle = FlatStyle.Flat
        btnChangePw.Location = New Point(119, 3)
        btnChangePw.Name = "btnChangePw"
        btnChangePw.Padding = New Padding(10, 4, 10, 4)
        btnChangePw.Size = New Size(177, 43)
        btnChangePw.TabIndex = 1
        btnChangePw.Text = "Change password"
        btnChangePw.UseVisualStyleBackColor = True
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(120F, 120F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(944, 681)
        Controls.Add(tabs)
        Font = New Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Name = "MainForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Room Key Tracker"
        tabs.ResumeLayout(False)
        tpRooms.ResumeLayout(False)
        flpRoomsTop.ResumeLayout(False)
        flpRoomsTop.PerformLayout()
        tpScan.ResumeLayout(False)
        pnlScan.ResumeLayout(False)
        pnlScan.PerformLayout()
        CType(pic, ComponentModel.ISupportInitialize).EndInit()
        flpScanBtns.ResumeLayout(False)
        flpScanBtns.PerformLayout()
        flpManual.ResumeLayout(False)
        flpManual.PerformLayout()
        tpRegister.ResumeLayout(False)
        tpRegister.PerformLayout()
        flpRegTop.ResumeLayout(False)
        flpRegTop.PerformLayout()
        flpRegRow.ResumeLayout(False)
        flpRegRow.PerformLayout()
        CType(picOut, ComponentModel.ISupportInitialize).EndInit()
        tpKeys.ResumeLayout(False)
        flpKeysTop.ResumeLayout(False)
        flpKeysTop.PerformLayout()
        tpLog.ResumeLayout(False)
        tpTools.ResumeLayout(False)
        tabsAdmin.ResumeLayout(False)
        tpAdmin.ResumeLayout(False)
        flpLogTop.ResumeLayout(False)
        flpLogTop.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents tabs As System.Windows.Forms.TabControl
    Friend WithEvents tpRooms As System.Windows.Forms.TabPage
    Friend WithEvents flpRooms As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents flpRoomsTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblVac As System.Windows.Forms.Label
    Friend WithEvents lblOcc As System.Windows.Forms.Label
    Friend WithEvents tpScan As System.Windows.Forms.TabPage
    Friend WithEvents pnlScan As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblStep As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents cboCam As System.Windows.Forms.ComboBox
    Friend WithEvents lblErr As System.Windows.Forms.Label
    Friend WithEvents flpScanBtns As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents flpManual As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtManual As System.Windows.Forms.TextBox
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents tpRegister As System.Windows.Forms.TabPage
    Friend WithEvents lvUsers As System.Windows.Forms.ListView
    Friend WithEvents colUserId As System.Windows.Forms.ColumnHeader
    Friend WithEvents colUserName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colUserType As System.Windows.Forms.ColumnHeader
    Friend WithEvents colUserQr As System.Windows.Forms.ColumnHeader
    Friend WithEvents flpRegTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents flpRegRow As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents cboRole As System.Windows.Forms.ComboBox
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents btnExisting As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents picOut As System.Windows.Forms.PictureBox
    Friend WithEvents lblOut As System.Windows.Forms.Label
    Friend WithEvents btnSaveId As System.Windows.Forms.Button
    Friend WithEvents tpKeys As System.Windows.Forms.TabPage
    Friend WithEvents flpKeys As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents flpKeysTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtRoom As System.Windows.Forms.TextBox
    Friend WithEvents btnAddRoom As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents cboRemoveRoom As System.Windows.Forms.ComboBox
    Friend WithEvents btnRemoveRoom As System.Windows.Forms.Button
    Friend WithEvents tpTools As System.Windows.Forms.TabPage
    Friend WithEvents tpAdmin As System.Windows.Forms.TabPage
    Friend WithEvents tabsAdmin As System.Windows.Forms.TabControl
    Friend WithEvents tpLog As System.Windows.Forms.TabPage
    Friend WithEvents lvLog As System.Windows.Forms.ListView
    Friend WithEvents colWhen As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRoom As System.Windows.Forms.ColumnHeader
    Friend WithEvents colWho As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAction As System.Windows.Forms.ColumnHeader
    Friend WithEvents flpLogTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnClearLog As System.Windows.Forms.Button
    Friend WithEvents btnChangePw As System.Windows.Forms.Button

End Class
