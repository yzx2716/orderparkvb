'Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Public Class WebRequestClass

    'Public url As String

    Public Sub New()
        'MyClass.url = url
    End Sub

    Public Function getRequest(ByVal url As String)
        Dim request As WebRequest = WebRequest.Create(url)
        request.Credentials = CredentialCache.DefaultCredentials

        Dim response As WebResponse = request.GetResponse()

        Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)

        Dim dataStream As Stream = response.GetResponseStream()

        Dim reader As New StreamReader(dataStream)

        Dim responseFromServer As String = reader.ReadToEnd()

        reader.Close()
        response.Close()

        Return responseFromServer
    End Function

    Public Function postRequest(ByVal url As String, ByVal postData As String)
        Dim request As WebRequest = WebRequest.Create(url)

        request.Method = "POST"

        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)

        request.ContentType = "application/x-www-form-urlencoded"

        request.ContentLength = byteArray.Length

        Dim dataStream As Stream = request.GetRequestStream()

        dataStream.Write(byteArray, 0, byteArray.Length)

        dataStream.Close()

        Dim response As WebResponse = request.GetResponse()

        dataStream = response.GetResponseStream()

        Dim reader As New StreamReader(dataStream)

        Dim responseFromServer As String = reader.ReadToEnd()

        reader.Close()
        dataStream.Close()
        response.Close()

        Return responseFromServer
    End Function
End Class

