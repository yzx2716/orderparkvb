Public Class Notice

    Public Sub New(ByVal content, ByVal action)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If action = "exit" Then
            Me.BackColor = Color.DarkSlateGray
        Else
            Me.BackColor = Color.LawnGreen
        End If
    End Sub

End Class