Public Class NuevoEmpleado

    Private _update As Boolean = False
    Private _vm As EmpleadoViewModel

    Public Sub New(ByVal update As Boolean, ByVal vm As EmpleadoViewModel)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _update = update
        _vm = vm
    End Sub

    Private Sub NuevoEmpleado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            With Me
                .FormBorderStyle = FormBorderStyle.FixedDialog
                .MaximizeBox = False
                .MinimizeBox = False
                .StartPosition = FormStartPosition.CenterScreen
            End With

            If _update Then
                txtLogin.ReadOnly = True
                txtNombre.ReadOnly = True
                txtPaterno.ReadOnly = True
                txtMaterno.ReadOnly = True
                dtpFecha.Enabled = False
                txtSueldo.Select()
            End If

            If Not _update Then
                _vm.Employee.FechaIngreso = dtpFecha.Value.ToString("yyyy-MM-dd")
            End If

            bindingData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error ...!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub bindingData()
        Try
            txtLogin.DataBindings.Add(New Binding("Text", _vm.Employee.Usuario, "Login"))
            txtNombre.DataBindings.Add(New Binding("Text", _vm.Employee.Usuario, "Nombre"))
            txtPaterno.DataBindings.Add(New Binding("Text", _vm.Employee.Usuario, "Paterno"))
            txtMaterno.DataBindings.Add(New Binding("Text", _vm.Employee.Usuario, "Materno"))
            txtSueldo.DataBindings.Add(New Binding("Text", _vm.Employee, "Sueldo"))
            dtpFecha.DataBindings.Add(New Binding("Value", _vm.Employee, "FechaIngreso"))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            If _update Then
                If _vm.UpdateSalary() Then
                    MessageBox.Show("Salario actualizado", "Atención ...!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("No hubo cambios", "Atención ...!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                If _vm.AddEmployee() Then
                    MessageBox.Show("Usuario agregado", "Atención ...!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("No hubo cambios", "Atención ...!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If

            Me.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error ...!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class