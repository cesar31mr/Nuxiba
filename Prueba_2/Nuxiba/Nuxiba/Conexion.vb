Imports System.Data.SqlClient
Imports System.Data

Public Class Conexion
    Private _con As SqlConnection = Nothing
    Private _cmd As SqlCommand = Nothing

    Public Property cmd() As SqlCommand
        Get
            Return _cmd
        End Get
        Set(ByVal value As SqlCommand)
            _cmd = value
        End Set
    End Property

    Private _conectionString As String
    Public Property ConectionString() As String
        Get
            Return _conectionString
        End Get
        Set(ByVal value As String)
            _conectionString = value
        End Set
    End Property

    Private _querys As ArrayList
    Public Property Querys() As ArrayList
        Get
            Return _querys
        End Get
        Set(ByVal value As ArrayList)
            _querys = value
        End Set
    End Property

    Public Sub New(ByVal server As String, ByVal catalog As String, ByVal user As String, ByVal pass As String)
        Try
            ConectionString = String.Format("Data Source={0};Initial Catalog={1}; Persist Security Info=True; User ID={2}; Password={3}; Connection Timeout=0;", server, catalog, user, pass)
            _con = New SqlConnection(ConectionString)
            _cmd = New SqlCommand
            Querys = New ArrayList
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Function isConnected() As Boolean
        Try
            If Not _con Is Nothing Then
                If _con.State = ConnectionState.Open Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub openConnection()
        Try
            If Not _con Is Nothing Then
                If Not _con.State = ConnectionState.Open Then
                    _con.Open()
                Else
                    _con.Open()
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function execCommand(ByVal query As String)
        Try
            Dim result As Integer = 0

            If _con.ConnectionString <= String.Empty Then
                _con.ConnectionString = _conectionString
            End If

            cmd.Connection = _con
            cmd.CommandText = query

            If Not isConnected() Then
                openConnection()
            End If

            cmd.CommandTimeout = 0
            result = cmd.ExecuteNonQuery()

            _con.Dispose()

            Return result
        Catch ex As Exception
            Throw
        Finally
            cmd = New SqlCommand
            _con.Close()
        End Try
    End Function

    Public Function execSelect(ByVal query As String) As DataTable
        Try
            If _con.ConnectionString <= String.Empty Then
                _con.ConnectionString = _conectionString
            End If

            cmd.Connection = _con
            cmd.CommandText = query


            If Not isConnected() Then
                openConnection()
            End If

            cmd.CommandTimeout = 0
            Dim ds As New DataTable
            Dim sda As New SqlDataAdapter(cmd)
            sda.Fill(ds)

            _con.Dispose()
            Return ds
        Catch ex As Exception
            Throw
        Finally
            cmd = New SqlCommand
            _con.Close()
        End Try
    End Function
End Class
