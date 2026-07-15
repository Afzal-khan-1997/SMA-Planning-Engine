<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginForm

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me._titleLabel = New System.Windows.Forms.Label()
        Me._subtitleLabel = New System.Windows.Forms.Label()
        Me._usernameLabel = New System.Windows.Forms.Label()
        Me._usernameTextBox = New System.Windows.Forms.TextBox()
        Me._passwordLabel = New System.Windows.Forms.Label()
        Me._passwordTextBox = New System.Windows.Forms.TextBox()
        Me._statusLabel = New System.Windows.Forms.Label()
        Me._loginButton = New System.Windows.Forms.Button()
        Me._cancelButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        '_titleLabel
        '
        Me._titleLabel.AutoSize = False
        Me._titleLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!)
        Me._titleLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(66, Byte), Integer))
        Me._titleLabel.Location = New System.Drawing.Point(34, 26)
        Me._titleLabel.Name = "_titleLabel"
        Me._titleLabel.Size = New System.Drawing.Size(360, 34)
        Me._titleLabel.TabIndex = 0
        Me._titleLabel.Text = "SMA Planning Engine"
        Me._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        '_subtitleLabel
        '
        Me._subtitleLabel.AutoSize = False
        Me._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(120, Byte), Integer))
        Me._subtitleLabel.Location = New System.Drawing.Point(36, 66)
        Me._subtitleLabel.Name = "_subtitleLabel"
        Me._subtitleLabel.Size = New System.Drawing.Size(360, 24)
        Me._subtitleLabel.TabIndex = 1
        Me._subtitleLabel.Text = "Sign in with your Windows user name"
        '
        '_usernameLabel
        '
        Me._usernameLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me._usernameLabel.Location = New System.Drawing.Point(38, 112)
        Me._usernameLabel.Name = "_usernameLabel"
        Me._usernameLabel.Size = New System.Drawing.Size(120, 24)
        Me._usernameLabel.TabIndex = 2
        Me._usernameLabel.Text = "Username"
        '
        '_usernameTextBox
        '
        Me._usernameTextBox.Location = New System.Drawing.Point(160, 108)
        Me._usernameTextBox.Name = "_usernameTextBox"
        Me._usernameTextBox.Size = New System.Drawing.Size(230, 25)
        Me._usernameTextBox.TabIndex = 3
        '
        '_passwordLabel
        '
        Me._passwordLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me._passwordLabel.Location = New System.Drawing.Point(38, 154)
        Me._passwordLabel.Name = "_passwordLabel"
        Me._passwordLabel.Size = New System.Drawing.Size(120, 24)
        Me._passwordLabel.TabIndex = 4
        Me._passwordLabel.Text = "Password"
        '
        '_passwordTextBox
        '
        Me._passwordTextBox.Location = New System.Drawing.Point(160, 150)
        Me._passwordTextBox.Name = "_passwordTextBox"
        Me._passwordTextBox.Size = New System.Drawing.Size(230, 25)
        Me._passwordTextBox.TabIndex = 5
        Me._passwordTextBox.UseSystemPasswordChar = True
        '
        '_statusLabel
        '
        Me._statusLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me._statusLabel.Location = New System.Drawing.Point(38, 190)
        Me._statusLabel.Name = "_statusLabel"
        Me._statusLabel.Size = New System.Drawing.Size(352, 32)
        Me._statusLabel.TabIndex = 6
        '
        '_loginButton
        '
        Me._loginButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(112, Byte), Integer))
        Me._loginButton.FlatAppearance.BorderSize = 0
        Me._loginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me._loginButton.ForeColor = System.Drawing.Color.White
        Me._loginButton.Location = New System.Drawing.Point(190, 236)
        Me._loginButton.Name = "_loginButton"
        Me._loginButton.Size = New System.Drawing.Size(94, 34)
        Me._loginButton.TabIndex = 7
        Me._loginButton.Text = "Login"
        Me._loginButton.UseVisualStyleBackColor = False
        '
        '_cancelButton
        '
        Me._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me._cancelButton.Location = New System.Drawing.Point(296, 236)
        Me._cancelButton.Name = "_cancelButton"
        Me._cancelButton.Size = New System.Drawing.Size(94, 34)
        Me._cancelButton.TabIndex = 8
        Me._cancelButton.Text = "Cancel"
        Me._cancelButton.UseVisualStyleBackColor = True
        '
        'LoginForm
        '
        Me.AcceptButton = Me._loginButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.CancelButton = Me._cancelButton
        Me.ClientSize = New System.Drawing.Size(440, 300)
        Me.Controls.Add(Me._cancelButton)
        Me.Controls.Add(Me._loginButton)
        Me.Controls.Add(Me._statusLabel)
        Me.Controls.Add(Me._passwordTextBox)
        Me.Controls.Add(Me._passwordLabel)
        Me.Controls.Add(Me._usernameTextBox)
        Me.Controls.Add(Me._usernameLabel)
        Me.Controls.Add(Me._subtitleLabel)
        Me.Controls.Add(Me._titleLabel)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SMA Planning Engine Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents _titleLabel As System.Windows.Forms.Label
    Friend WithEvents _subtitleLabel As System.Windows.Forms.Label
    Friend WithEvents _usernameLabel As System.Windows.Forms.Label
    Friend WithEvents _usernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents _passwordLabel As System.Windows.Forms.Label
    Friend WithEvents _passwordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents _statusLabel As System.Windows.Forms.Label
    Friend WithEvents _loginButton As System.Windows.Forms.Button
    Friend WithEvents _cancelButton As System.Windows.Forms.Button
End Class
