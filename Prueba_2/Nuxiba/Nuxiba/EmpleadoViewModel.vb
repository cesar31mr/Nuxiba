Public Class EmpleadoViewModel

    Private _user As Usuario
    Public Property User() As Usuario
        Get
            Return _user
        End Get
        Set(ByVal value As Usuario)
            _user = value
        End Set
    End Property

    Private _employee As Empleado
    Public Property Employee() As Empleado
        Get
            Return _employee
        End Get
        Set(ByVal value As Empleado)
            _employee = value
        End Set
    End Property

    Private _ctrl As ControllerUsuario

    Public Sub New()
        Try
            _ctrl = New ControllerUsuario
            _user = New Usuario
            _employee = New Empleado
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function getUsers() As DataTable
        Try
            Return _ctrl.getUsers
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddEmployee() As Boolean
        Try
            Return _ctrl.AddEmployee(employee:=Employee)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateSalary() As Boolean
        Try
            Return _ctrl.UpdateSalary(employee:=Employee)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
