Public Class Form1

    Private _vm As EmpleadoViewModel
    Private _seleccionado As Boolean

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _vm = New EmpleadoViewModel
        _seleccionado = False
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            dgvDatos.AllowUserToAddRows = False
            dgvDatos.AllowUserToDeleteRows = False
            dgvDatos.AllowUserToOrderColumns = False
            dgvDatos.ReadOnly = True
            dgvDatos.MultiSelect = False
            dgvDatos.ScrollBars = ScrollBars.Both
            dgvDatos.AllowUserToResizeColumns = True
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error ...!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadData()
        Try
            dgvDatos.DataSource = _vm.getUsers
            dgvDatos.Columns("Sueldo").Visible = False
            dgvDatos.Columns("FechaIngreso").Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub getDataFromGrid()
        Try
            If dgvDatos.SelectedRows.Count > 0 Then
                For Each row As DataGridViewRow In dgvDatos.SelectedRows
                    _vm.User = New Usuario

                    With _vm.User
                        .userId = CType(IIf(IsDBNull(row.Cells("userId").Value), 0, row.Cells("userId").Value), Integer)
                        .Login = CType(IIf(IsDBNull(row.Cells("Login").Value), "", row.Cells("Login").Value), String)
                        .Nombre = CType(IIf(IsDBNull(row.Cells("Nombre").Value), "", row.Cells("Nombre").Value), String)
                        .Paterno = CType(IIf(IsDBNull(row.Cells("Paterno").Value), "", row.Cells("Paterno").Value), String)
                        .Materno = CType(IIf(IsDBNull(row.Cells("Materno").Value), "", row.Cells("Materno").Value), String)
                    End With

                    _vm.Employee = New Empleado
                    With _vm.Employee
                        .Usuario = _vm.User
                        .Sueldo = CType(IIf(IsDBNull(row.Cells("Sueldo").Value), "", row.Cells("Sueldo").Value), Double)
                        .FechaIngreso = CType(IIf(IsDBNull(row.Cells("FechaIngreso").Value), "", row.Cells("FechaIngreso").Value), String)
                    End With
                Next
            End If

            _seleccionado = True
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub dgvDatos_Click(sender As Object, e As EventArgs) Handles dgvDatos.Click
        Try
            getDataFromGrid()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            _vm = New EmpleadoViewModel
            Dim edit As New NuevoEmpleado(False, _vm)
            edit.ShowDialog()
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error ...!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            If Not _seleccionado Then
                getDataFromGrid()
            End If
            Dim edit As New NuevoEmpleado(True, _vm)
            edit.ShowDialog()
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error ...!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Try
            Dim fd As New SaveFileDialog
            fd.Filter = "CSV|*.csv"
            fd.Title = "Guardar información"

            If fd.ShowDialog = DialogResult.OK Then
                Dim data As New Text.StringBuilder(1024)

                For Each col As DataColumn In dgvDatos.Rows
                    data.AppendLine(String.Format("{0},", col.ColumnName))
                Next

                For Each row As DataRow In dgvDatos.Rows
                    data.AppendLine(String.Format("{0},{1} {2} {3},{4},{5}", row.Item("Login").ToString(), row.Item("Nombre").ToString(), row.Item("Paterno").ToString(), row.Item("Materno").ToString(), row.Item("Sueldo").ToString(), row.Item("FechaIngreso").ToString()))
                Next

                My.Computer.FileSystem.WriteAllText(fd.FileName, data.ToString, False)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error ...!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
