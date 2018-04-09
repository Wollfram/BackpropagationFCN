using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Kursova1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            listBoxCalculator.SetSelected(1, true);
        }

        private NeuroNetFullyConnected _neuro;
        private KeyValuePair<Matrix, Matrix> _trainSet;
        private KeyValuePair<Matrix, Matrix> _testSet;
        private string _savingFolder = "saves";
        IAccuracyCalculator _accuracyCalculator = new AccuracyCalculatorSimpleBiasABSEqual(0.1);

        private void buttonCreateNewNeuronet_Click(object sender, EventArgs e) {
            try {
                double learnRate = Double.Parse(textBoxLearnRate.Text);
                double learnInert = Double.Parse(textBoxLearnInert.Text);
                double learnL2 = Double.Parse(textBoxLearnL2.Text);
                _neuro = new NeuroNetFullyConnected(new LayerInput(8));
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAddLayer_Click(object sender, EventArgs e) {
            try {
                _neuro.AddHiddenLayer(new NeuronTanh(), Int32.Parse(textBoxAddLayerNeuronCount.Text));
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        private void buttonEndCreatingNeuroNet_Click(object sender, EventArgs e) {
            try {
                _neuro.AddHiddenLayer(new NeuronTanh(), Int32.Parse(textBoxControlsCount.Text));
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        private void buttonSaveNeuroNet_Click(object sender, EventArgs e) {
            try {
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "") { _neuro.save(saveFileDialog1.FileName); }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        private void buttonLoadNeuronet_Click(object sender, EventArgs e) {
            try {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) { _neuro = NeuroNetFullyConnected.load(openFileDialog1.FileName); }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        private void buttonStartTeaching_Click(object sender, EventArgs e) {
            try {
                this.Cursor = Cursors.WaitCursor;

                double learnRate = Double.Parse(textBoxLearnRate.Text);
                double learnInert = Double.Parse(textBoxLearnInert.Text);
                double learnL2 = Double.Parse(textBoxLearnL2.Text);
                _neuro.LearnRate = learnRate;
                _neuro.LearnInert = learnInert;
                _neuro.L2Regularization = learnL2;

                double accuracy = Double.Parse(textBoxAccuracy.Text);
                int iterations = Int32.Parse(textBoxMaxIterations.Text);
                int batchSize = Int32.Parse(textBoxBatchSize.Text);
                double precision = Double.Parse(textBoxComparePrecision.Text);

                double[] rez = _neuro.train(_trainSet.Key, _trainSet.Value, batchSize, iterations, accuracy, _testSet.Key, _testSet.Value, _accuracyCalculator,
                    _savingFolder);
                //    double[] rez = _neuro.train(_trainSet.Key, _trainSet.Value, batchSize, iterations, accuracy, _trainSet.Key, _trainSet.Value, precision);

                chart1.Series.Clear();
                Series seriesAccur = new Series("Accuracy");
                seriesAccur.Color = Color.Red;
                seriesAccur.IsVisibleInLegend = true;
                seriesAccur.ChartType = SeriesChartType.Line;

                for (int i = 0; i < rez.Length; i++) { seriesAccur.Points.AddXY(i + 1, rez[i]); }

                chart1.Series.Add(seriesAccur);

            } catch (Exception
                exception) {
                MessageBox.Show(exception.Message);
            } finally {
                this.Cursor = Cursors.Default;
            }
        }

        private void buttonGenerateLearningSamples_Click(object sender, EventArgs e) {
            ISamplesGenerator generator = new SamplesGeneratorBeze(Int32.Parse(textBoxControlsCount.Text));
            this._trainSet = generator.Generate(Int32.Parse(textBoxLearningSamplesCount.Text));
        }

        private void buttonGenerateTestSamples_Click(object sender, EventArgs e) {
            ISamplesGenerator generator = new SamplesGeneratorBeze(Int32.Parse(textBoxControlsCount.Text));
            this._testSet = generator.Generate(Int32.Parse(textBoxTestSamplesCount.Text));
        }

        private void buttonTestAccur_Click(object sender, EventArgs e) {
            try {
                MessageBox.Show(_neuro.getAccuracy(_testSet.Key, _testSet.Value, _accuracyCalculator).ToString());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSaveTestSamples_Click(object sender, EventArgs e) {
            try {
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "") {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (var fStream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
                        formatter.Serialize(fStream, _testSet);
                    }
                }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }


        private void buttonLoadTestSamples_Click(object sender, EventArgs e) {
            try {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    using (var fStream = File.OpenRead(openFileDialog1.FileName)) {
                        _testSet = (KeyValuePair<Matrix, Matrix>) new BinaryFormatter().Deserialize(fStream);
                    }
                }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        private void buttonChangeSavingFolder_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) { _savingFolder = folderBrowserDialog1.SelectedPath; }
        }

        private void listBoxCalculator_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                double bias = double.Parse(textBoxComparePrecision.Text);
                switch (listBoxCalculator.SelectedIndex) {
                    case 0:
                        _accuracyCalculator = new AccuracyCalculatorDecart(bias);
                        break;
                    case 1:
                        _accuracyCalculator = new AccuracyCalculatorSimpleBiasABSEqual(bias);
                        break;
                    case 2:
                        _accuracyCalculator = new AccuracyCalculatorSimpleMedianBiasEqual(bias);
                        break;

                }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        private void textBoxComparePrecision_TextChanged(object sender, EventArgs e) { }

        private void buttonSetPrecision_Click(object sender, EventArgs e) { listBoxCalculator_SelectedIndexChanged(sender, e); }

        private void button4_Click(object sender, EventArgs e) {
            try {
                _neuro.MinAccurToSave = Double.Parse(textBoxMinSaveAccur.Text);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }

        }
    }
}
