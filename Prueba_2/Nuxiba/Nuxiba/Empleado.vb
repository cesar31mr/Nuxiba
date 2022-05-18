Public Class Empleado
    Private _usuario As Usuario
    Public Property Usuario() As Usuario
        Get
            Return _usuario
        End Get
        Set(ByVal value As Usuario)
            _usuario = value
        End Set
    End Property

    Private _Sueldo As Double
    Public Property Sueldo() As Double
        Get
            Return _Sueldo
        End Get
        Set(ByVal value As Double)
            _Sueldo = value
        End Set
    End Property

    Private _FechaIngreso As String
    Public Property FechaIngreso() As String
        Get
            Return _FechaIngreso
        End Get
        Set(ByVal value As String)
            _FechaIngreso = value
        End Set
    End Property

    Public Sub New()
        _usuario = New Usuario
    End Sub
End Class
