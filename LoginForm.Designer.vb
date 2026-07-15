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
        _titleLabel = New Label()
        _subtitleLabel = New Label()
        _usernameLabel = New Label()
        _usernameTextBox = New TextBox()
        _passwordLabel = New Label()
        _passwordTextBox = New TextBox()
        _statusLabel = New Label()
        _loginButton = New Button()
        _cancelButton = New Button()
        SuspendLayout()
        ' 
        ' _titleLabel
        ' 
        _titleLabel.Font = New Font("Segoe UI Semibold", 18F)
        _titleLabel.ForeColor = Color.FromArgb(CByte(35), CByte(46), CByte(66))
        _titleLabel.Location = New Point(34, 26)
        _titleLabel.Name = "_titleLabel"
        _titleLabel.Size = New Size(360, 34)
        _titleLabel.TabIndex = 0
        _titleLabel.Text = "SMA Planning Engine"
        _titleLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' _subtitleLabel
        ' 
        _subtitleLabel.ForeColor = Color.FromArgb(CByte(88), CByte(101), CByte(120))
        _subtitleLabel.Location = New Point(36, 66)
        _subtitleLabel.Name = "_subtitleLabel"
        _subtitleLabel.Size = New Size(360, 24)
        _subtitleLabel.TabIndex = 1
        _subtitleLabel.Text = "Sign in with your Windows user name"
        ' 
        ' _usernameLabel
        ' 
        _usernameLabel.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        _usernameLabel.Location = New Point(38, 112)
        _usernameLabel.Name = "_usernameLabel"
        _usernameLabel.Size = New Size(120, 24)
        _usernameLabel.TabIndex = 2
        _usernameLabel.Text = "Username"
        ' 
        ' _usernameTextBox
        ' 
        _usernameTextBox.Location = New Point(160, 108)
        _usernameTextBox.Name = "_usernameTextBox"
        _usernameTextBox.Size = New Size(230, 30)
        _usernameTextBox.TabIndex = 3
        ' 
        ' _passwordLabel
        ' 
        _passwordLabel.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        _passwordLabel.Location = New Point(38, 154)
        _passwordLabel.Name = "_passwordLabel"
        _passwordLabel.Size = New Size(120, 24)
        _passwordLabel.TabIndex = 4
        _passwordLabel.Text = "Password"
        ' 
        ' _passwordTextBox
        ' 
        _passwordTextBox.Location = New Point(160, 150)
        _passwordTextBox.Name = "_passwordTextBox"
        _passwordTextBox.Size = New Size(230, 30)
        _passwordTextBox.TabIndex = 5
        _passwordTextBox.UseSystemPasswordChar = True
        ' 
        ' _statusLabel
        ' 
        _statusLabel.ForeColor = Color.FromArgb(CByte(178), CByte(34), CByte(34))
        _statusLabel.Location = New Point(38, 190)
        _statusLabel.Name = "_statusLabel"
        _statusLabel.Size = New Size(352, 32)
        _statusLabel.TabIndex = 6
        ' 
        ' _loginButton
        ' 
        _loginButton.BackColor = Color.FromArgb(CByte(32), CByte(164), CByte(112))
        _loginButton.FlatAppearance.BorderSize = 0
        _loginButton.FlatStyle = FlatStyle.Flat
        _loginButton.ForeColor = Color.White
        _loginButton.Location = New Point(190, 236)
        _loginButton.Name = "_loginButton"
        _loginButton.Size = New Size(94, 34)
        _loginButton.TabIndex = 7
        _loginButton.Text = "Login"
        _loginButton.UseVisualStyleBackColor = False
        ' 
        ' _cancelButton
        ' 
        _cancelButton.DialogResult = DialogResult.Cancel
        _cancelButton.Location = New Point(296, 236)
        _cancelButton.Name = "_cancelButton"
        _cancelButton.Size = New Size(94, 34)
        _cancelButton.TabIndex = 8
        _cancelButton.Text = "Cancel"
        _cancelButton.UseVisualStyleBackColor = True
        ' 
        ' LoginForm
        ' 
        AcceptButton = _loginButton
        AutoScaleDimensions = New SizeF(9F, 23F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(246), CByte(248), CByte(252))
        CancelButton = _cancelButton
        ClientSize = New Size(440, 300)
        Controls.Add(_cancelButton)
        Controls.Add(_loginButton)
        Controls.Add(_statusLabel)
        Controls.Add(_passwordTextBox)
        Controls.Add(_passwordLabel)
        Controls.Add(_usernameTextBox)
        Controls.Add(_usernameLabel)
        Controls.Add(_subtitleLabel)
        Controls.Add(_titleLabel)
        Font = New Font("Segoe UI", 10F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "LoginForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "SMA Planning Engine Login"
        ResumeLayout(False)
        PerformLayout()
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
