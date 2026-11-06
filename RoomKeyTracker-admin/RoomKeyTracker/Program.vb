Imports System.IO
Imports System.Security.Cryptography
Imports System.Text.Json

Public Class Room
    Public Property Id As String = ""
    Public Property Holder As String
    Public Property Since As DateTime?
End Class

Public Class User
    Public Property Id As String = ""
    Public Property Name As String = ""
    Public Property Role As String = "FAC"
    Public Property Code As String = ""
End Class

Public Class LogEntry
    Public Property T As DateTime
    Public Property Room As String = ""
    Public Property User As String = ""
    Public Property Id As String = ""
    Public Property Act As String = ""
End Class

Public Class AppState
    Public Property Rooms As New List(Of Room)
    Public Property Users As New List(Of User)
    Public Property Log As New List(Of LogEntry)

    Public Property PassSalt As String = ""
    Public Property PassHash As String = ""

    Public Property Counters As New Dictionary(Of String, Integer)

    Private Shared Function Derive(pw As String, salt As Byte()) As Byte()
        Using k As New Rfc2898DeriveBytes(pw, salt, 100000, HashAlgorithmName.SHA256)
            Return k.GetBytes(32)
        End Using
    End Function

    Public Function HasPassword() As Boolean
        Return PassHash <> "" AndAlso PassSalt <> ""
    End Function

    Public Sub SetPassword(pw As String)
        Dim salt(15) As Byte
        Using rng = RandomNumberGenerator.Create()
            rng.GetBytes(salt)
        End Using
        PassSalt = Convert.ToBase64String(salt)
        PassHash = Convert.ToBase64String(Derive(pw, salt))
    End Sub

    Public Function CheckPassword(pw As String) As Boolean
        If Not HasPassword() Then Return False
        Try
            Dim h = Derive(pw, Convert.FromBase64String(PassSalt))
            Return CryptographicOperations.FixedTimeEquals(h, Convert.FromBase64String(PassHash))
        Catch
            Return False
        End Try
    End Function

    Private Shared ReadOnly FilePath As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "RoomKeyTracker", "data.json")

    Private Shared ReadOnly JsonOpts As New JsonSerializerOptions With {.WriteIndented = True}

    Public Shared Function Load() As AppState
        Try
            If File.Exists(FilePath) Then
                Dim s = JsonSerializer.Deserialize(Of AppState)(File.ReadAllText(FilePath))
                If s IsNot Nothing AndAlso s.Rooms.Count > 0 Then Return s
            End If
        Catch
            Try
                If File.Exists(FilePath) Then File.Copy(FilePath, FilePath & ".bak", True)
            Catch
            End Try
        End Try
        Dim d As New AppState()
        For Each id In {"101", "102", "103", "104", "201", "202", "203", "204"}
            d.Rooms.Add(New Room With {.Id = id})
        Next
        Return d
    End Function

    Public Sub Save()
        Try
            Dim dir = Path.GetDirectoryName(FilePath)
            If Not String.IsNullOrEmpty(dir) Then Directory.CreateDirectory(dir)
            Dim json = JsonSerializer.Serialize(Me, JsonOpts)
            Dim tmp = FilePath & ".tmp"
            File.WriteAllText(tmp, json)
            File.Move(tmp, FilePath, True)
        Catch ex As Exception
            Debug.WriteLine("Save failed: " & ex.Message)
        End Try
    End Sub
End Class

Module Program
    <STAThread>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New MainForm())
    End Sub
End Module
