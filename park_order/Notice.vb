Public Class Notice

    Dim startSoundPlayer = New System.Media.SoundPlayer("horse.wav")

    Public Sub New(ByVal tran_type, ByVal content, ByVal time)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If tran_type = "enter" Then
            Me.BackColor = Color.LawnGreen
        Else
            Me.BackColor = Color.DarkSlateGray
        End If

    End Sub



    Private Sub Notice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '播放音效
        startSoundPlayer.Play()

    End Sub
End Class