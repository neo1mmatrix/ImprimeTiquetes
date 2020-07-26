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
        Me.lbImpresoraMatrix = New System.Windows.Forms.Label()
        Me.nudLineas = New System.Windows.Forms.NumericUpDown()
        Me.lbNumeroLineas = New System.Windows.Forms.Label()
        Me.rbMedianaSize = New System.Windows.Forms.RadioButton()
        Me.rbNormalSize = New System.Windows.Forms.RadioButton()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.lbFontSize = New System.Windows.Forms.Label()
        Me.lbTiempo = New System.Windows.Forms.Label()
        Me.nudTiempo = New System.Windows.Forms.NumericUpDown()
        Me.cbPrinterList = New System.Windows.Forms.ComboBox()
        CType(Me.nudLineas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudTiempo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbImpresoraMatrix
        '
        Me.lbImpresoraMatrix.AutoSize = True
        Me.lbImpresoraMatrix.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbImpresoraMatrix.Location = New System.Drawing.Point(12, 24)
        Me.lbImpresoraMatrix.Name = "lbImpresoraMatrix"
        Me.lbImpresoraMatrix.Size = New System.Drawing.Size(91, 18)
        Me.lbImpresoraMatrix.TabIndex = 2
        Me.lbImpresoraMatrix.Text = "Impresora"
        '
        'nudLineas
        '
        Me.nudLineas.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudLineas.Location = New System.Drawing.Point(18, 112)
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
        Me.lbNumeroLineas.Location = New System.Drawing.Point(15, 91)
        Me.lbNumeroLineas.Name = "lbNumeroLineas"
        Me.lbNumeroLineas.Size = New System.Drawing.Size(156, 18)
        Me.lbNumeroLineas.TabIndex = 5
        Me.lbNumeroLineas.Text = "Número de Lineas"
        '
        'rbMedianaSize
        '
        Me.rbMedianaSize.AutoSize = True
        Me.rbMedianaSize.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMedianaSize.Location = New System.Drawing.Point(231, 140)
        Me.rbMedianaSize.Name = "rbMedianaSize"
        Me.rbMedianaSize.Size = New System.Drawing.Size(94, 22)
        Me.rbMedianaSize.TabIndex = 6
        Me.rbMedianaSize.TabStop = True
        Me.rbMedianaSize.Text = "Mediana"
        Me.rbMedianaSize.UseVisualStyleBackColor = True
        '
        'rbNormalSize
        '
        Me.rbNormalSize.AutoSize = True
        Me.rbNormalSize.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbNormalSize.Location = New System.Drawing.Point(231, 112)
        Me.rbNormalSize.Name = "rbNormalSize"
        Me.rbNormalSize.Size = New System.Drawing.Size(84, 22)
        Me.rbNormalSize.TabIndex = 7
        Me.rbNormalSize.TabStop = True
        Me.rbNormalSize.Text = "Normal"
        Me.rbNormalSize.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Location = New System.Drawing.Point(297, 184)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(106, 34)
        Me.btnSalir.TabIndex = 8
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(170, 184)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(106, 34)
        Me.btnGuardar.TabIndex = 9
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'lbFontSize
        '
        Me.lbFontSize.AutoSize = True
        Me.lbFontSize.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFontSize.Location = New System.Drawing.Point(228, 91)
        Me.lbFontSize.Name = "lbFontSize"
        Me.lbFontSize.Size = New System.Drawing.Size(119, 18)
        Me.lbFontSize.TabIndex = 12
        Me.lbFontSize.Text = "Tamaño Letra"
        '
        'lbTiempo
        '
        Me.lbTiempo.AutoSize = True
        Me.lbTiempo.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTiempo.Location = New System.Drawing.Point(427, 91)
        Me.lbTiempo.Name = "lbTiempo"
        Me.lbTiempo.Size = New System.Drawing.Size(129, 18)
        Me.lbTiempo.TabIndex = 14
        Me.lbTiempo.Text = "Tiempo Espera"
        '
        'nudTiempo
        '
        Me.nudTiempo.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudTiempo.Location = New System.Drawing.Point(430, 112)
        Me.nudTiempo.Minimum = New Decimal(New Integer() {40, 0, 0, 0})
        Me.nudTiempo.Name = "nudTiempo"
        Me.nudTiempo.Size = New System.Drawing.Size(78, 27)
        Me.nudTiempo.TabIndex = 13
        Me.nudTiempo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudTiempo.Value = New Decimal(New Integer() {40, 0, 0, 0})
        '
        'cbPrinterList
        '
        Me.cbPrinterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPrinterList.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPrinterList.FormattingEnabled = True
        Me.cbPrinterList.Location = New System.Drawing.Point(12, 45)
        Me.cbPrinterList.Name = "cbPrinterList"
        Me.cbPrinterList.Size = New System.Drawing.Size(559, 26)
        Me.cbPrinterList.TabIndex = 15
        '
        'Fr_Configuracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 234)
        Me.Controls.Add(Me.cbPrinterList)
        Me.Controls.Add(Me.lbTiempo)
        Me.Controls.Add(Me.nudTiempo)
        Me.Controls.Add(Me.lbFontSize)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.rbNormalSize)
        Me.Controls.Add(Me.rbMedianaSize)
        Me.Controls.Add(Me.lbNumeroLineas)
        Me.Controls.Add(Me.nudLineas)
        Me.Controls.Add(Me.lbImpresoraMatrix)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(599, 273)
        Me.Name = "Fr_Configuracion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuración"
        CType(Me.nudLineas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudTiempo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbImpresoraMatrix As Label
    Friend WithEvents nudLineas As NumericUpDown
    Friend WithEvents lbNumeroLineas As Label
    Friend WithEvents rbMedianaSize As RadioButton
    Friend WithEvents rbNormalSize As RadioButton
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents lbFontSize As Label
    Friend WithEvents lbTiempo As Label
    Friend WithEvents nudTiempo As NumericUpDown
    Friend WithEvents cbPrinterList As ComboBox
End Class
