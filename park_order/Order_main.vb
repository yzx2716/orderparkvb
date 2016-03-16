Imports System.Net
Imports System.IO
Imports System.Text

Imports System.Xml
Imports System.Collections
Public Class Order_main

    Dim main_title As String = My.Settings.main_name
    Dim host_url As String = My.Settings.host_url
    Dim WebClass As New WebRequestClass

    Private Declare Function OpenIcon Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean
    Private Declare Function IsIconic Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean
    Private Declare Function SetForegroundWindow Lib "user32.dll" (ByVal hWnd As IntPtr) As Integer
    Private Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal strclassName As String, ByVal strWindowName As String) As IntPtr

    ''' <summary>
    ''' 主程序加载
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Order_main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Me.main_title

        Me.Operate_Notice()
        

    End Sub

    ''' <summary>
    ''' 窗口的最大化，最小化，托盘显示
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <remarks></remarks>
    Sub FindAndOpenWindow(ByVal Title As String)   '任务栏上有这个程序,打开
        Dim hWnd As IntPtr = FindWindow(Nothing, Title)
        If Not hWnd.Equals(IntPtr.Zero) Then  '任务栏上有这个程序
            Dim isIcon As Boolean = IsIconic(hWnd)  ''窗口未最小化，返回值为零；如果窗口已最小化，返回值为非零
            If Not isIcon Then    '在任务栏,也最小化
                SetForegroundWindow(hWnd)  '前置并拥有焦点
            Else                      '在任务栏,但没最小化
                OpenIcon(hWnd)
            End If
        End If
    End Sub

    

    '最小化到托盘
    Private Sub Order_main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True   '点击关闭窗体
        'Timer1.Enabled = False
        Me.Hide()
    End Sub

    '托盘操作
    Private Sub NotifyIcon1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        'Dim s As String = "Sample"
        If e.Button = MouseButtons.Left Then  '鼠标左键单击托盘图标,功能:全部显示
            Me.Show()
            FindAndOpenWindow(Me.main_title)
        End If
        If e.Button = MouseButtons.Right Then
        End If
    End Sub

    '托盘菜单退出操作
    Private Sub 退出ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 退出ToolStripMenuItem.Click
        NotifyIcon1.Dispose()  '卸载系统图盘图标
        End
    End Sub

    '托盘菜单主面板操作
    Private Sub 主面板ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 主面板ToolStripMenuItem.Click
        Me.Show()
        FindAndOpenWindow(Me.main_title)
    End Sub

    ''' <summary>
    ''' 定时任务操作，每秒执行一次
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick




    End Sub

    Private Sub Operate_Notice()
        '数组用于保存已处理过的数据
        Dim hasNoticeList As New ArrayList
        '获取服务器数据
        Dim mes_xml As String = Me.WebClass.getRequest(Me.host_url)
        Dim xmlDoc As New XmlDocument
        xmlDoc.LoadXml(mes_xml)
        Dim xn As XmlNode = xmlDoc.SelectSingleNode("order_info")
        Dim xnl As XmlNodeList = xn.ChildNodes
        Dim xnf As XmlNode
        For Each xnf In xnl
            Dim mes_id = xnf.Attributes.GetNamedItem("mes_id").Value
            '已处理数据
            If hasNoticeList.Contains(mes_id) Then
                Continue For
            End If

            Dim tran_type As XmlNode = xnf.SelectSingleNode("tran_type")
            Dim content As XmlNode = xnf.SelectSingleNode("content")
            Dim time As XmlNode = xnf.SelectSingleNode("time")
            Dim notice As New Notice(tran_type, content, time)
            '加入到已处理数据中
            hasNoticeList.Add(mes_id)
        Next
    End Sub
End Class
