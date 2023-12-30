namespace EasyServers
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            linkLabel1 = new LinkLabel();
            radioButton1 = new RadioButton();
            button1 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Yu Gothic UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(3, 10);
            label1.Name = "label1";
            label1.Size = new Size(266, 47);
            label1.TabIndex = 0;
            label1.Text = "Minecraft EULA";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            linkLabel1.Location = new Point(3, 116);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(271, 25);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://www.minecraft.net/eula";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.BackColor = Color.Transparent;
            radioButton1.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            radioButton1.Location = new Point(12, 236);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(200, 34);
            radioButton1.TabIndex = 5;
            radioButton1.TabStop = true;
            radioButton1.Text = "EULAに同意します。";
            radioButton1.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(793, 430);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 7;
            button1.TabStop = false;
            button1.Text = "作成";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(712, 430);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.TabStop = false;
            button2.Text = "戻る";
            button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(856, 153);
            panel1.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(3, 57);
            label2.Name = "label2";
            label2.Size = new Size(782, 50);
            label2.TabIndex = 6;
            label2.Text = "Minecraftのサーバーを使用するにはエンドユーザーライセンス(以下EULA)への同意が必須です。\r\n下のリンクからEULAをよく読み、同意する場合は下のボタンを押してサーバーの作成を開始させてください。";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBox1.Location = new Point(12, 201);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(271, 29);
            textBox1.TabIndex = 10;
            textBox1.Text = "無題のサーバー";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label3.Location = new Point(12, 177);
            label3.Name = "label3";
            label3.Size = new Size(72, 21);
            label3.TabIndex = 11;
            label3.Text = "サーバー名";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 465);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(panel1);
            Controls.Add(radioButton1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private LinkLabel linkLabel1;
        private RadioButton radioButton1;
        private Button button1;
        private Button button2;
        private Panel panel1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
    }
}