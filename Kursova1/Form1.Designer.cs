namespace Kursova1 {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonGenerateTestSamples = new System.Windows.Forms.Button();
            this.textBoxTestSamplesCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonGenerateLearningSamples = new System.Windows.Forms.Button();
            this.textBoxLearningSamplesCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonStartTeaching = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAccuracy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMaxIterations = new System.Windows.Forms.TextBox();
            this.buttonSaveNeuroNet = new System.Windows.Forms.Button();
            this.buttonLoadNeuronet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxLearnL2 = new System.Windows.Forms.TextBox();
            this.textBoxBatchSize = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxLearnInert = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxLearnRate = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxComparePrecision = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxControlsCount = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxAddLayerNeuronCount = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonTestAccur = new System.Windows.Forms.Button();
            this.buttonSaveTestSamples = new System.Windows.Forms.Button();
            this.buttonLoadTestSamples = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonChangeSavingFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxCalculator = new System.Windows.Forms.ListBox();
            this.buttonSetPrecision = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxMinSaveAccur = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonGenerateTestSamples);
            this.groupBox1.Controls.Add(this.textBoxTestSamplesCount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.buttonGenerateLearningSamples);
            this.groupBox1.Controls.Add(this.textBoxLearningSamplesCount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(619, 237);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 76);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // buttonGenerateTestSamples
            // 
            this.buttonGenerateTestSamples.Location = new System.Drawing.Point(209, 45);
            this.buttonGenerateTestSamples.Name = "buttonGenerateTestSamples";
            this.buttonGenerateTestSamples.Size = new System.Drawing.Size(75, 20);
            this.buttonGenerateTestSamples.TabIndex = 12;
            this.buttonGenerateTestSamples.Text = "Generate";
            this.buttonGenerateTestSamples.UseVisualStyleBackColor = true;
            this.buttonGenerateTestSamples.Click += new System.EventHandler(this.buttonGenerateTestSamples_Click);
            // 
            // textBoxTestSamplesCount
            // 
            this.textBoxTestSamplesCount.Location = new System.Drawing.Point(104, 46);
            this.textBoxTestSamplesCount.Name = "textBoxTestSamplesCount";
            this.textBoxTestSamplesCount.Size = new System.Drawing.Size(100, 20);
            this.textBoxTestSamplesCount.TabIndex = 11;
            this.textBoxTestSamplesCount.Text = "100";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 35);
            this.label4.TabIndex = 10;
            this.label4.Text = "Test samples count";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonGenerateLearningSamples
            // 
            this.buttonGenerateLearningSamples.Location = new System.Drawing.Point(209, 13);
            this.buttonGenerateLearningSamples.Name = "buttonGenerateLearningSamples";
            this.buttonGenerateLearningSamples.Size = new System.Drawing.Size(75, 20);
            this.buttonGenerateLearningSamples.TabIndex = 9;
            this.buttonGenerateLearningSamples.Text = "Generate";
            this.buttonGenerateLearningSamples.UseVisualStyleBackColor = true;
            this.buttonGenerateLearningSamples.Click += new System.EventHandler(this.buttonGenerateLearningSamples_Click);
            // 
            // textBoxLearningSamplesCount
            // 
            this.textBoxLearningSamplesCount.Location = new System.Drawing.Point(104, 13);
            this.textBoxLearningSamplesCount.Name = "textBoxLearningSamplesCount";
            this.textBoxLearningSamplesCount.Size = new System.Drawing.Size(100, 20);
            this.textBoxLearningSamplesCount.TabIndex = 8;
            this.textBoxLearningSamplesCount.Text = "1000";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(2, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 27);
            this.label3.TabIndex = 7;
            this.label3.Text = "Learning samples count";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(592, 379);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            // 
            // buttonStartTeaching
            // 
            this.buttonStartTeaching.Location = new System.Drawing.Point(209, 11);
            this.buttonStartTeaching.Name = "buttonStartTeaching";
            this.buttonStartTeaching.Size = new System.Drawing.Size(75, 75);
            this.buttonStartTeaching.TabIndex = 13;
            this.buttonStartTeaching.Text = "Start teaching";
            this.buttonStartTeaching.UseVisualStyleBackColor = true;
            this.buttonStartTeaching.Click += new System.EventHandler(this.buttonStartTeaching_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Accuracy, %";
            // 
            // textBoxAccuracy
            // 
            this.textBoxAccuracy.Location = new System.Drawing.Point(99, 11);
            this.textBoxAccuracy.Name = "textBoxAccuracy";
            this.textBoxAccuracy.Size = new System.Drawing.Size(100, 20);
            this.textBoxAccuracy.TabIndex = 15;
            this.textBoxAccuracy.Text = "0,95";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Max iterations";
            // 
            // textBoxMaxIterations
            // 
            this.textBoxMaxIterations.Location = new System.Drawing.Point(99, 37);
            this.textBoxMaxIterations.Name = "textBoxMaxIterations";
            this.textBoxMaxIterations.Size = new System.Drawing.Size(100, 20);
            this.textBoxMaxIterations.TabIndex = 17;
            this.textBoxMaxIterations.Text = "1000";
            // 
            // buttonSaveNeuroNet
            // 
            this.buttonSaveNeuroNet.Location = new System.Drawing.Point(9, 125);
            this.buttonSaveNeuroNet.Name = "buttonSaveNeuroNet";
            this.buttonSaveNeuroNet.Size = new System.Drawing.Size(120, 19);
            this.buttonSaveNeuroNet.TabIndex = 18;
            this.buttonSaveNeuroNet.Text = "Save Neuronet";
            this.buttonSaveNeuroNet.UseVisualStyleBackColor = true;
            this.buttonSaveNeuroNet.Click += new System.EventHandler(this.buttonSaveNeuroNet_Click);
            // 
            // buttonLoadNeuronet
            // 
            this.buttonLoadNeuronet.Location = new System.Drawing.Point(155, 125);
            this.buttonLoadNeuronet.Name = "buttonLoadNeuronet";
            this.buttonLoadNeuronet.Size = new System.Drawing.Size(120, 19);
            this.buttonLoadNeuronet.TabIndex = 19;
            this.buttonLoadNeuronet.Text = "Load Neuronet";
            this.buttonLoadNeuronet.UseVisualStyleBackColor = true;
            this.buttonLoadNeuronet.Click += new System.EventHandler(this.buttonLoadNeuronet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxLearnL2);
            this.groupBox2.Controls.Add(this.textBoxBatchSize);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxLearnInert);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonStartTeaching);
            this.groupBox2.Controls.Add(this.textBoxMaxIterations);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.textBoxAccuracy);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxLearnRate);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Location = new System.Drawing.Point(619, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(436, 97);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // textBoxLearnL2
            // 
            this.textBoxLearnL2.Location = new System.Drawing.Point(386, 59);
            this.textBoxLearnL2.Name = "textBoxLearnL2";
            this.textBoxLearnL2.Size = new System.Drawing.Size(44, 20);
            this.textBoxLearnL2.TabIndex = 25;
            this.textBoxLearnL2.Text = "0,0";
            // 
            // textBoxBatchSize
            // 
            this.textBoxBatchSize.Location = new System.Drawing.Point(99, 63);
            this.textBoxBatchSize.Name = "textBoxBatchSize";
            this.textBoxBatchSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxBatchSize.TabIndex = 19;
            this.textBoxBatchSize.Text = "100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Batch size";
            // 
            // textBoxLearnInert
            // 
            this.textBoxLearnInert.Location = new System.Drawing.Point(386, 33);
            this.textBoxLearnInert.Name = "textBoxLearnInert";
            this.textBoxLearnInert.Size = new System.Drawing.Size(44, 20);
            this.textBoxLearnInert.TabIndex = 23;
            this.textBoxLearnInert.Text = "0";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(297, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 37);
            this.label13.TabIndex = 24;
            this.label13.Text = "Learn L2 regularization";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(297, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "Learn inert";
            // 
            // textBoxLearnRate
            // 
            this.textBoxLearnRate.Location = new System.Drawing.Point(386, 7);
            this.textBoxLearnRate.Name = "textBoxLearnRate";
            this.textBoxLearnRate.Size = new System.Drawing.Size(44, 20);
            this.textBoxLearnRate.TabIndex = 21;
            this.textBoxLearnRate.Text = "1,0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(297, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Learn rate";
            // 
            // textBoxComparePrecision
            // 
            this.textBoxComparePrecision.Location = new System.Drawing.Point(107, 66);
            this.textBoxComparePrecision.Name = "textBoxComparePrecision";
            this.textBoxComparePrecision.Size = new System.Drawing.Size(50, 20);
            this.textBoxComparePrecision.TabIndex = 21;
            this.textBoxComparePrecision.Text = "0,1";
            this.textBoxComparePrecision.TextChanged += new System.EventHandler(this.textBoxComparePrecision_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Compare Precision";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxMinSaveAccur);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.buttonChangeSavingFolder);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.buttonLoadNeuronet);
            this.groupBox4.Controls.Add(this.textBoxControlsCount);
            this.groupBox4.Controls.Add(this.buttonSaveNeuroNet);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.textBoxAddLayerNeuronCount);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Location = new System.Drawing.Point(619, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(284, 225);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(266, 21);
            this.button1.TabIndex = 6;
            this.button1.Text = "End Creating";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonEndCreatingNeuroNet_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Controls count (outcome layer)";
            // 
            // textBoxControlsCount
            // 
            this.textBoxControlsCount.Location = new System.Drawing.Point(231, 16);
            this.textBoxControlsCount.Name = "textBoxControlsCount";
            this.textBoxControlsCount.Size = new System.Drawing.Size(44, 20);
            this.textBoxControlsCount.TabIndex = 2;
            this.textBoxControlsCount.Text = "10";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(269, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Create New Neuronet";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonCreateNewNeuronet_Click);
            // 
            // textBoxAddLayerNeuronCount
            // 
            this.textBoxAddLayerNeuronCount.Location = new System.Drawing.Point(95, 72);
            this.textBoxAddLayerNeuronCount.Name = "textBoxAddLayerNeuronCount";
            this.textBoxAddLayerNeuronCount.Size = new System.Drawing.Size(100, 20);
            this.textBoxAddLayerNeuronCount.TabIndex = 4;
            this.textBoxAddLayerNeuronCount.Text = "10";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(201, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "AddLayer";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonAddLayer_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 75);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Neuron count";
            // 
            // buttonTestAccur
            // 
            this.buttonTestAccur.Location = new System.Drawing.Point(914, 228);
            this.buttonTestAccur.Name = "buttonTestAccur";
            this.buttonTestAccur.Size = new System.Drawing.Size(111, 82);
            this.buttonTestAccur.TabIndex = 27;
            this.buttonTestAccur.Text = "Test Accuracy";
            this.buttonTestAccur.UseVisualStyleBackColor = true;
            this.buttonTestAccur.Click += new System.EventHandler(this.buttonTestAccur_Click);
            // 
            // buttonSaveTestSamples
            // 
            this.buttonSaveTestSamples.Location = new System.Drawing.Point(1031, 264);
            this.buttonSaveTestSamples.Name = "buttonSaveTestSamples";
            this.buttonSaveTestSamples.Size = new System.Drawing.Size(187, 30);
            this.buttonSaveTestSamples.TabIndex = 28;
            this.buttonSaveTestSamples.Text = "SaveTestSamples";
            this.buttonSaveTestSamples.UseVisualStyleBackColor = true;
            this.buttonSaveTestSamples.Click += new System.EventHandler(this.buttonSaveTestSamples_Click);
            // 
            // buttonLoadTestSamples
            // 
            this.buttonLoadTestSamples.Location = new System.Drawing.Point(1031, 228);
            this.buttonLoadTestSamples.Name = "buttonLoadTestSamples";
            this.buttonLoadTestSamples.Size = new System.Drawing.Size(187, 30);
            this.buttonLoadTestSamples.TabIndex = 29;
            this.buttonLoadTestSamples.Text = "LoadTestSamples";
            this.buttonLoadTestSamples.UseVisualStyleBackColor = true;
            this.buttonLoadTestSamples.Click += new System.EventHandler(this.buttonLoadTestSamples_Click);
            // 
            // buttonChangeSavingFolder
            // 
            this.buttonChangeSavingFolder.Location = new System.Drawing.Point(9, 166);
            this.buttonChangeSavingFolder.Name = "buttonChangeSavingFolder";
            this.buttonChangeSavingFolder.Size = new System.Drawing.Size(266, 22);
            this.buttonChangeSavingFolder.TabIndex = 31;
            this.buttonChangeSavingFolder.Text = "Change folder";
            this.buttonChangeSavingFolder.UseVisualStyleBackColor = true;
            this.buttonChangeSavingFolder.Click += new System.EventHandler(this.buttonChangeSavingFolder_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(-5, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 27);
            this.label1.TabIndex = 30;
            this.label1.Text = "Folder for saving: standart saves\\\\";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxCalculator
            // 
            this.listBoxCalculator.FormattingEnabled = true;
            this.listBoxCalculator.Items.AddRange(new object[] {
            "Decart Calculator",
            "Simple ABS bias Calculator",
            "Simple median bias Calculator"});
            this.listBoxCalculator.Location = new System.Drawing.Point(9, 19);
            this.listBoxCalculator.Name = "listBoxCalculator";
            this.listBoxCalculator.Size = new System.Drawing.Size(148, 43);
            this.listBoxCalculator.TabIndex = 26;
            this.listBoxCalculator.SelectedIndexChanged += new System.EventHandler(this.listBoxCalculator_SelectedIndexChanged);
            // 
            // buttonSetPrecision
            // 
            this.buttonSetPrecision.Location = new System.Drawing.Point(9, 89);
            this.buttonSetPrecision.Name = "buttonSetPrecision";
            this.buttonSetPrecision.Size = new System.Drawing.Size(148, 22);
            this.buttonSetPrecision.TabIndex = 32;
            this.buttonSetPrecision.Text = "Set";
            this.buttonSetPrecision.UseVisualStyleBackColor = true;
            this.buttonSetPrecision.Click += new System.EventHandler(this.buttonSetPrecision_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBoxCalculator);
            this.groupBox3.Controls.Add(this.buttonSetPrecision);
            this.groupBox3.Controls.Add(this.textBoxComparePrecision);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(1061, 296);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 117);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Kursova1.Properties.Resources.Screenshot_2;
            this.pictureBox1.Location = new System.Drawing.Point(914, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(315, 203);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxMinSaveAccur
            // 
            this.textBoxMinSaveAccur.Location = new System.Drawing.Point(130, 194);
            this.textBoxMinSaveAccur.Name = "textBoxMinSaveAccur";
            this.textBoxMinSaveAccur.Size = new System.Drawing.Size(62, 20);
            this.textBoxMinSaveAccur.TabIndex = 33;
            this.textBoxMinSaveAccur.Text = "0,25";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(201, 192);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 23);
            this.button4.TabIndex = 32;
            this.button4.Text = "Apply";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "MinAccurToAutoSave";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1233, 422);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonLoadTestSamples);
            this.Controls.Add(this.buttonSaveTestSamples);
            this.Controls.Add(this.buttonTestAccur);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Neuro";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonLoadNeuronet;
        private System.Windows.Forms.Button buttonSaveNeuroNet;
        private System.Windows.Forms.TextBox textBoxMaxIterations;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAccuracy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonStartTeaching;
        private System.Windows.Forms.Button buttonGenerateTestSamples;
        private System.Windows.Forms.TextBox textBoxTestSamplesCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonGenerateLearningSamples;
        private System.Windows.Forms.TextBox textBoxLearningSamplesCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxBatchSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxComparePrecision;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxAddLayerNeuronCount;
        private System.Windows.Forms.TextBox textBoxLearnRate;
        private System.Windows.Forms.TextBox textBoxLearnInert;
        private System.Windows.Forms.TextBox textBoxControlsCount;
        private System.Windows.Forms.TextBox textBoxLearnL2;
        private System.Windows.Forms.Button buttonTestAccur;
        private System.Windows.Forms.Button buttonSaveTestSamples;
        private System.Windows.Forms.Button buttonLoadTestSamples;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonChangeSavingFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxCalculator;
        private System.Windows.Forms.Button buttonSetPrecision;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxMinSaveAccur;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
    }
}

