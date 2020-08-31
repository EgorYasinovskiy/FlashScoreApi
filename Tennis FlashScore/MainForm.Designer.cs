namespace Tennis_FlashScore
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.mathesDataGrid = new System.Windows.Forms.DataGridView();
            this.LeagueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstPlayerCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecondPlayerCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.nudTimeBeforePost = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.минут = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mathesDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeBeforePost)).BeginInit();
            this.SuspendLayout();
            // 
            // mathesDataGrid
            // 
            this.mathesDataGrid.AllowUserToAddRows = false;
            this.mathesDataGrid.AllowUserToDeleteRows = false;
            this.mathesDataGrid.AllowUserToOrderColumns = true;
            this.mathesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mathesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LeagueColumn,
            this.FirstPlayerCol,
            this.SecondPlayerCol,
            this.StartTime});
            this.mathesDataGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.mathesDataGrid.Location = new System.Drawing.Point(0, 0);
            this.mathesDataGrid.Name = "mathesDataGrid";
            this.mathesDataGrid.ReadOnly = true;
            this.mathesDataGrid.Size = new System.Drawing.Size(452, 469);
            this.mathesDataGrid.TabIndex = 0;
            // 
            // LeagueColumn
            // 
            this.LeagueColumn.HeaderText = "Лига";
            this.LeagueColumn.Name = "LeagueColumn";
            this.LeagueColumn.ReadOnly = true;
            // 
            // FirstPlayerCol
            // 
            this.FirstPlayerCol.HeaderText = "Первый игрок";
            this.FirstPlayerCol.Name = "FirstPlayerCol";
            this.FirstPlayerCol.ReadOnly = true;
            // 
            // SecondPlayerCol
            // 
            this.SecondPlayerCol.HeaderText = "Второй игрок";
            this.SecondPlayerCol.Name = "SecondPlayerCol";
            this.SecondPlayerCol.ReadOnly = true;
            // 
            // StartTime
            // 
            this.StartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StartTime.HeaderText = "Время начала";
            this.StartTime.Name = "StartTime";
            this.StartTime.ReadOnly = true;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(0, 475);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(208, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Начать";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(232, 475);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(208, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Остановить";
            this.stopButton.UseVisualStyleBackColor = true;
            // 
            // nudTimeBeforePost
            // 
            this.nudTimeBeforePost.Location = new System.Drawing.Point(85, 528);
            this.nudTimeBeforePost.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nudTimeBeforePost.Name = "nudTimeBeforePost";
            this.nudTimeBeforePost.Size = new System.Drawing.Size(70, 20);
            this.nudTimeBeforePost.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 530);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Постить за ";
            // 
            // минут
            // 
            this.минут.AutoSize = true;
            this.минут.Location = new System.Drawing.Point(161, 535);
            this.минут.Name = "минут";
            this.минут.Size = new System.Drawing.Size(37, 13);
            this.минут.TabIndex = 3;
            this.минут.Text = "минут";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 574);
            this.Controls.Add(this.минут);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudTimeBeforePost);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.mathesDataGrid);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mathesDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeBeforePost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView mathesDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeagueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstPlayerCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SecondPlayerCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.NumericUpDown nudTimeBeforePost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label минут;
    }
}

