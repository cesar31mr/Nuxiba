Public Class ControllerUsuario
    Private _con As Conexion

    Public Sub New()
        Try
            _con = New Conexion(server:="DESKTOP-ZERO\SQLEXPRESS", catalog:="Nuxiba", user:="Nuxiba", pass:="Nuxiba")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function getUsers() As DataTable
        Try
            Dim query As String = "SELECT TOP 10 u.userId, u.Login, u.Nombre, u.Paterno, u.Materno, e.Sueldo, e.FechaIngreso FROM  Usuarios u INNER JOIN Empleados e ON u.userId = e.userId ORDER BY u.userId DESC"
            Return _con.execSelect(query)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddEmployee(ByVal employee As Empleado) As Boolean
        Try
            Dim query As New Text.StringBuilder(1024)

            query.AppendLine("INSERT INTO Usuarios ([Login], [Nombre], [Paterno], [Materno])")
            query.AppendLine("VALUES (@Login, @Nombre, @Paterno, @Materno)")
            query.Append("SELECT SCOPE_IDENTITY() ID")

            With _con.cmd
                .Parameters.Add("@Login", SqlDbType.VarChar, 100)
                .Parameters.Add("@Nombre", SqlDbType.VarChar, 100)
                .Parameters.Add("@Paterno", SqlDbType.VarChar, 100)
                .Parameters.Add("@Materno", SqlDbType.VarChar, 100)

                .Parameters("@Login").Value = employee.Usuario.Login
                .Parameters("@Nombre").Value = employee.Usuario.Nombre
                .Parameters("@Paterno").Value = employee.Usuario.Paterno
                .Parameters("@Materno").Value = employee.Usuario.Materno
            End With

            Dim dt As DataTable = _con.execSelect(query.ToString)
            Dim newId As Integer = 0

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    newId = Convert.ToInt32(row("ID"))
                Next
            End If

            If newId > 0 Then
                query.Clear()
                query.AppendLine("INSERT INTO Empleados (userId, Sueldo, FechaIngreso)")
                query.AppendLine("VALUES (@userId, @Sueldo, @FechaIngreso)")

                With _con.cmd
                    .Parameters.Add("@userId", SqlDbType.Int)
                    .Parameters.Add("@Sueldo", SqlDbType.Decimal)
                    .Parameters.Add("@FechaIngreso", SqlDbType.Date)

                    .Parameters("@userId").Value = newId
                    .Parameters("@Sueldo").Value = employee.Sueldo
                    .Parameters("@FechaIngreso").Value = employee.FechaIngreso
                End With

                Return _con.execCommand(query.ToString) > 0
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateSalary(ByVal employee As Empleado) As Boolean
        Try
            Dim query As New Text.StringBuilder(1024)

            query.Append("UPDATE e ")
            query.Append("SET Sueldo = @Sueldo ")
            query.Append("FROM Empleados e ")
            query.Append("WHERE e.userId = @userId ")

            With _con.cmd
                .Parameters.Add("@Sueldo", SqlDbType.Decimal)
                .Parameters.Add("@userId", SqlDbType.Int)
                .Parameters("@Sueldo").Value = employee.Sueldo
                .Parameters("@userId").Value = employee.Usuario.userId
            End With

            Return _con.execCommand(query.ToString) > 0

        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
