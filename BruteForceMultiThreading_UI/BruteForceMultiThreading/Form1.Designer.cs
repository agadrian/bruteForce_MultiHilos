namespace BruteForceMultiThreading
{
    partial class BruteForce
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_choose = new Button();
            label1 = new Label();
            number_threads = new NumericUpDown();
            btn_start = new Button();
            textBox_log = new TextBox();
            checkBox_auto = new CheckBox();
            btn_clear = new Button();
            ((System.ComponentModel.ISupportInitialize)number_threads).BeginInit();
            SuspendLayout();
            // 
            // btn_choose
            // 
            btn_choose.Location = new Point(37, 61);
            btn_choose.Margin = new Padding(3, 2, 3, 2);
            btn_choose.Name = "btn_choose";
            btn_choose.Size = new Size(117, 22);
            btn_choose.TabIndex = 0;
            btn_choose.Text = "Choose Wordlist";
            btn_choose.UseVisualStyleBackColor = true;
            btn_choose.Click += btn_choose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 149);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 2;
            label1.Text = "Nº Threads";
            label1.Click += label1_Click;
            // 
            // number_threads
            // 
            number_threads.Location = new Point(116, 163);
            number_threads.Margin = new Padding(3, 2, 3, 2);
            number_threads.Name = "number_threads";
            number_threads.Size = new Size(59, 23);
            number_threads.TabIndex = 3;
            // 
            // btn_start
            // 
            btn_start.Location = new Point(60, 243);
            btn_start.Margin = new Padding(3, 2, 3, 2);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(92, 33);
            btn_start.TabIndex = 4;
            btn_start.Text = "Start";
            btn_start.UseVisualStyleBackColor = true;
            btn_start.Click += btn_start_Click;
            // 
            // textBox_log
            // 
            textBox_log.Location = new Point(206, 33);
            textBox_log.Margin = new Padding(3, 2, 3, 2);
            textBox_log.Multiline = true;
            textBox_log.Name = "textBox_log";
            textBox_log.ScrollBars = ScrollBars.Vertical;
            textBox_log.Size = new Size(392, 389);
            textBox_log.TabIndex = 5;
            // 
            // checkBox_auto
            // 
            checkBox_auto.AutoSize = true;
            checkBox_auto.Location = new Point(116, 132);
            checkBox_auto.Margin = new Padding(3, 2, 3, 2);
            checkBox_auto.Name = "checkBox_auto";
            checkBox_auto.Size = new Size(52, 19);
            checkBox_auto.TabIndex = 6;
            checkBox_auto.Text = "Auto";
            checkBox_auto.UseVisualStyleBackColor = true;
            checkBox_auto.CheckedChanged += checkBox_auto_change;
            // 
            // btn_clear
            // 
            btn_clear.Location = new Point(375, 438);
            btn_clear.Margin = new Padding(3, 2, 3, 2);
            btn_clear.Name = "btn_clear";
            btn_clear.Size = new Size(82, 22);
            btn_clear.TabIndex = 7;
            btn_clear.Text = "Clear";
            btn_clear.UseVisualStyleBackColor = true;
            btn_clear.Click += btn_clear_Click;
            // 
            // BruteForce
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 533);
            Controls.Add(btn_clear);
            Controls.Add(checkBox_auto);
            Controls.Add(textBox_log);
            Controls.Add(btn_start);
            Controls.Add(number_threads);
            Controls.Add(label1);
            Controls.Add(btn_choose);
            Margin = new Padding(3, 2, 3, 2);
            Name = "BruteForce";
            Text = "BruteForce";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)number_threads).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_choose;
        private Label label1;
        private NumericUpDown number_threads;
        private Button btn_start;
        private TextBox textBox_log;
        private CheckBox checkBox_auto;
        private Button btn_clear;
    }
}
