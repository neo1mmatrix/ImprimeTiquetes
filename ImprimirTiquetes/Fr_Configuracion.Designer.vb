<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fr_Configuracion
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Fr_Configuracion))
        Me.txtImpresoraMatrix = New System.Windows.Forms.TextBox()
        Me.txtImpresoraTermica = New System.Windows.Forms.MaskedTextBox()
        Me.lbImpresoraMatrix = New System.Windows.Forms.Label()
        Me.lbImpresoraTermica = New System.Windows.Forms.Label()
        Me.nudLineas = New System.Windows.Forms.NumericUpDown()
        Me.lbNumeroLineas = New System.Windows.Forms.Label()
        Me.rbImpresoraMatrix = New System.Windows.Forms.RadioButton()
        Me.rbImpresoraTermica = New System.Windows.Forms.RadioButton()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        CType(Me.nudLineas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtImpresoraMatrix
        '
        Me.txtImpresoraMatrix.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpresoraMatrix.Location = New System.Drawing.Point(15, 45)
        Me.txtImpresoraMatrix.Name = "txtImpresoraMatrix"
        Me.txtImpresoraMatrix.Size = New System.Drawing.Size(748, 27)
        Me.txtImpresoraMatrix.TabIndex = 0
        '
        'txtImpresoraTermica
        '
        Me.txtImpresoraTermica.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpresoraTermica.Location = New System.Drawing.Point(15, 104)
        Me.txtImpresoraTermica.Name = "txtImpresoraTermica"
        Me.txtImpresoraTermica.Size = New System.Drawing.Size(748, 27)
        Me.txtImpresoraTermica.TabIndex = 1
        '
        'lbImpresoraMatrix
        '
        Me.lbImpresoraMatrix.AutoSize = True
        Me.lbImpresoraMatrix.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbImpresoraMatrix.Location = New System.Drawing.Point(12, 24)
        Me.lbImpresoraMatrix.Name = "lbImpresoraMatrix"
        Me.lbImpresoraMatrix.Size = New System.Drawing.Size(147, 18)
        Me.lbImpresoraMatrix.TabIndex = 2
        Me.lbImpresoraMatrix.Text = "Impresora Matrix"
        '
        'lbImpresoraTermica
        '
        Me.lbImpresoraTermica.AutoSize = True
        Me.lbImpresoraTermica.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbImpresoraTermica.Location = New System.Drawing.Point(12, 83)
        Me.lbImpresoraTermica.Name = "lbImpresoraTermica"
        Me.lbImpresoraTermica.Size = New System.Drawing.Size(159, 18)
        Me.lbImpresoraTermica.TabIndex = 3
        Me.lbImpresoraTermica.Text = "Impresora Termica"
        '
        'nudLineas
        '
        Me.nudLineas.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudLineas.Location = New System.Drawing.Point(18, 167)
        Me.nudLineas.Minimum = New Decimal(New Integer() {40, 0, 0, 0})
        Me.nudLineas.Name = "nudLineas"
        Me.nudLineas.Size = New System.Drawing.Size(78, 27)
        Me.nudLineas.TabIndex = 4
        Me.nudLineas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudLineas.Value = New Decimal(New Integer() {40, 0, 0, 0})
        '
        'lbNumeroLineas
        '
        Me.lbNumeroLineas.AutoSize = True
        Me.lbNumeroLineas.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNumeroLineas.Location = New System.Drawing.Point(15, 146)
        Me.lbNumeroLineas.Name = "lbNumeroLineas"
        Me.lbNumeroLineas.Size = New System.Drawing.Size(156, 18)
        Me.lbNumeroLineas.TabIndex = 5
        Me.lbNumeroLineas.Text = "Número de Lineas"
        '
        'rbImpresoraMatrix
        '
        Me.rbImpresoraMatrix.AutoSize = True
        Me.rbImpresoraMatrix.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbImpresoraMatrix.Location = New System.Drawing.Point(231, 205)
        Me.rbImpresoraMatrix.Name = "rbImpresoraMatrix"
        Me.rbImpresoraMatrix.Size = New System.Drawing.Size(165, 22)
        Me.rbImpresoraMatrix.TabIndex = 6
        Me.rbImpresoraMatrix.TabStop = True
        Me.rbImpresoraMatrix.Text = "Impresora Matrix"
        Me.rbImpresoraMatrix.UseVisualStyleBackColor = True
        '
        'rbImpresoraTermica
        '
        Me.rbImpresoraTermica.AutoSize = True
        Me.rbImpresoraTermica.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbImpresoraTermica.Location = New System.Drawing.Point(415, 205)
        Me.rbImpresoraTermica.Name = "rbImpresoraTermica"
        Me.rbImpresoraTermica.Size = New System.Drawing.Size(177, 22)
        Me.rbImpresoraTermica.TabIndex = 7
        Me.rbImpresoraTermica.TabStop = True
        Me.rbImpresoraTermica.Text = "Impresora Termica"
        Me.rbImpresoraTermica.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Location = New System.Drawing.Point(417, 243)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(106, 34)
        Me.btnSalir.TabIndex = 8
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(290, 243)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(106, 34)
        Me.btnGuardar.TabIndex = 9
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'Configuracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 289)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.rbImpresoraTermica)
        Me.Controls.Add(Me.rbImpresoraMatrix)
        Me.Controls.Add(Me.lbNumeroLineas)
        Me.Controls.Add(Me.nudLineas)
        Me.Controls.Add(Me.lbImpresoraTermica)
        Me.Controls.Add(Me.lbImpresoraMatrix)
        Me.Controls.Add(Me.txtImpresoraTermica)
        Me.Controls.Add(Me.txtImpresoraMatrix)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Configuracion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuración"
        CType(Me.nudLineas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtImpresoraMatrix As TextBox
    Friend WithEvents txtImpresoraTermica As MaskedTextBox
    Friend WithEvents lbImpresoraMatrix As Label
    Friend WithEvents lbImpresoraTermica As Label
    Friend WithEvents nudLineas As NumericUpDown
    Friend WithEvents lbNumeroLineas As Label
    Friend WithEvents rbImpresoraMatrix As RadioButton
    Friend WithEvents rbImpresoraTermica As RadioButton
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnGuardar As Button
End Class
