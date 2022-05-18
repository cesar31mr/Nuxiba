Public Class Usuario
    Private _userId As Integer
    Public Property userId() As Integer
        Get
            Return _userId
        End Get
        Set(ByVal value As Integer)
            _userId = value
        End Set
    End Property

    Private _Login As String
    Public Property Login() As String
        Get
            Return _Login
        End Get
        Set(ByVal value As String)
            _Login = value
        End Set
    End Property

    Private _Nombre As String
    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property

    Private _Paterno As String
    Public Property Paterno() As String
        Get
            Return _Paterno
        End Get
        Set(ByVal value As String)
            _Paterno = value
        End Set
    End Property

    Private _Materno As String
    Public Property Materno() As String
        Get
            Return _Materno
        End Get
        Set(ByVal value As String)
            _Materno = value
        End Set
    End Property

    Public Sub New()

    End Sub
End Class
