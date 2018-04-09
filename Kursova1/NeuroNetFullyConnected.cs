using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Kursova1;

namespace Kursova1 {

    #region Neurons

    public interface INeuron {

        double f(double x);
        double f1(double x); //pohidna
    }

    [Serializable]
    public class NeuronTanh : INeuron {

        public double f(double x) {
            x *= 0.01;
            if (x < -325) return -1;
            if (x > 325) return 1;

            double rez = (Math.Exp(2 * x) - 1) / (Math.Exp(2 * x) + 1);
            return rez;
            
            
            //if (x < -5) return -1;
            //if (x > 5) return 1;

            //double rez = (Math.Exp(2 * x) - 1) / (Math.Exp(2 * x) + 1);
            //return rez;

        }

        public double f1(double x) {
            x *= 0.01;
            if (x < -325 || x > 325) return 0;
            double rez = 4 / (Math.Exp(2 * x) + 2 + Math.Exp(-2 * x));
            return double.IsNaN(rez) ? 0 : rez;
            //if (x < -5 || x > 5) return 0;
            //double rez = 4 / (Math.Exp(2 * x) + 2 + Math.Exp(-2 * x));
            //return double.IsNaN(rez) ? 0 : rez;
        }

        //public static double f(double x) { return (Math.Exp(2 * x) - 1) / (Math.Exp(2 * x) + 1); }
        //public static double f1(double x) { return 4 / (Math.Exp(2 * x) + 2 + Math.Exp(-2 * x)); }
    }

    [Serializable]
    public class NeuronSigma : INeuron {
        public double Alpfa = 1;

        public NeuronSigma(double alpfa) { Alpfa = alpfa; }
        public NeuronSigma() { }

        public double f(double x) { return 1 / (1 + Math.Exp(-Alpfa * x)); }

        public double f1(double x) {
            double ff = f(x);
            return Alpfa * ff * (1 - ff);
        }

        //public static double f(double x) { return 1 / (1 + Math.Exp(-1 * x)); }

        //public static double f1(double x) {
        //    double ff = f(x);
        //    return ff * (1 - ff);
        //}
    }

    #endregion

    #region Layers

    interface ILayer {
        Matrix Outcome(Matrix entries);
        int PrevLayerNeuronCount();
        int LayerNeuronCount();
        INeuron Neuron { get; set; }
        Matrix Weights { get; set; }
    }

    /// <summary>
    /// presents one layer of neuro network.
    /// weights - entry weights for this layer, aded first column which is bias-vector for each neurons of this layer,
    ///     in each layer aded one neuron for bias
    ///         each row is entry weights to one of neuron of this layer
    ///         each columns is outcome weights of one of neurons of previous layer
    /// </summary>
    [Serializable]
    public class LayerNormal : ILayer {
        /// <summary>
        /// weights first column is bias
        /// </summary>
        private Matrix _weights;

        private INeuron _neuron;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_weights">weights first column is bias</param>
        /// <param name="_neuron"></param>
        public LayerNormal(INeuron _neuron, Matrix _weights) {
            if (_weights == null) throw new ArgumentNullException(nameof(_weights));
            if (_neuron == null) throw new ArgumentNullException(nameof(_neuron));
            this._weights = _weights;
            this._neuron = _neuron;
        }

        /// <summary>
        /// generate layer with random weights 0..1, and bias = 0 .. 1
        /// </summary>
        /// <param name="_neuron"></param>
        /// <param name="countPrevLayerNeurons"> count of real number of neurons</param>
        /// <param name="countNewLayerNeurons">count of real number of neurons</param>
        public LayerNormal(INeuron _neuron, int countPrevLayerNeurons, int countNewLayerNeurons) {
            _weights = Matrix.Random(countNewLayerNeurons + 1, countPrevLayerNeurons + 1);
            //    for (int i = 0; i < _weights.Rows; i++) _weights[i, 0] = 0;
            this._neuron = _neuron;
        }

        public Matrix Weights {
            get { return _weights; }
            set {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _weights = value;
            }
        }

        public INeuron Neuron {
            get { return _neuron; }
            set { _neuron = value; }
        }

        /// <summary>
        /// return vector output of the layer, in which add element[0] always 1 (for bias)
        /// </summary>
        /// <param name="entries"> must have adding first elem '1' for bias</param>
        /// <returns></returns>
        public Matrix Outcome(Matrix entries) {
            if (entries == null) throw new ArgumentNullException(nameof(entries));
            if (entries.Rows != _weights.Columns) throw new Exception("wrong entry to the layer");
            Matrix rez = (_weights * entries).ApplyFunc(_neuron.f);
            for (int i = 0; i < entries.Columns; i++) rez[0, i] = 1; //for bias
            return rez;
        }

        /// <summary>
        /// bias column not included
        /// </summary>
        /// <returns></returns>
        public int PrevLayerNeuronCount() {
            return _weights.Columns - 1;
        }

        /// <summary>
        /// bias row not included
        /// </summary>
        /// <returns></returns>
        public int LayerNeuronCount() {
            return _weights.Rows - 1;
        }
    }

    [Serializable]
    public class LayerInput : ILayer {
        private int _count;
        private INeuron _neuron = null;
        private Matrix _weights = null;

        /// <summary>
        /// create new input layer
        /// </summary>
        /// <param name="_count"> real count of neurons</param>
        public LayerInput(int _count) {
            this._count = _count + 1;
        }

        public int Count {
            get { return _count; }
            set { _count = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries">entries must have aded first 1 for bias</param>
        /// <returns></returns>
        public Matrix Outcome(Matrix entries) {
            if (_count == entries.Rows) return entries;
            else {
                throw new Exception("Wrong entries to input level");
            }
        }

        public int PrevLayerNeuronCount() { return 0; }

        /// <summary>
        /// bias row not included
        /// </summary>
        /// <returns></returns>
        public int LayerNeuronCount() {
            return Count - 1;
        }

        public INeuron Neuron {
            get { throw new Exception("Returning Neuron of first layer"); }
            set { throw new Exception("Setting Neuron of first layer"); }
        }

        public Matrix Weights {
            get { throw new Exception("Returning Weights of first layer"); }
            set { throw new Exception("Setting Weights of first layer"); }
        }
    }

    #endregion

    #region  LossFunctions

    /// <summary>
    /// estimates the accuracy of neuronetwork
    /// </summary>
    interface ILossFunk {
        /// <summary>
        /// return loss value
        /// </summary>
        /// <param name="outcome"> real outcome of neuronet, each columns is answer to one sample input</param>
        /// <param name="answers">correct answers of neuronet, each columns is answer to one sample</param>
        /// <returns></returns>
        double GetLoss(Matrix outcome, Matrix answers);
    }

    [Serializable]
    public class LossFunkSquare : ILossFunk {
        /// <summary>
        /// return loss value
        /// </summary>
        /// <param name="outcome"> real outcome of neuronet, each columns is answer to one sample input</param>
        /// <param name="answers">correct answers of neuronet, each columns is answer to one sample</param>
        /// <returns></returns>
        public double GetLoss(Matrix outcome, Matrix answers) {
            Matrix X = outcome - answers;
            X = Matrix.MultiplySimple(X, X);
            double rez = 0;
            for (int i = 0; i < X.Rows; i++) {
                for (int j = 0; j < X.Columns; j++) { rez += X[i, j]; }
            }
            return 0.5 * rez;
        }
    }

    [Serializable]
    public class LossFunkSquareMedian : ILossFunk {
        private static LossFunkSquare _lossFunkSquare = new LossFunkSquare();

        /// <summary>
        /// return loss value
        /// </summary>
        /// <param name="outcome"> real outcome of neuronet, each columns is answer to one sample input</param>
        /// <param name="answers">correct answers of neuronet, each columns is answer to one sample</param>
        /// <returns></returns>
        public double GetLoss(Matrix outcome, Matrix answers) {
            return _lossFunkSquare.GetLoss(outcome, answers) / answers.Columns;
        }
    }

    #endregion

    #region VectorsComparator

    /// <summary>
    /// compare matrix of vectors, where each column is one vector, and return accuracy in %
    /// </summary>
    public interface IAccuracyCalculator {
        double Calculate(Matrix _left, Matrix _right);
    }

    /// <summary>
    /// compare median sum with bias
    /// </summary>
    class AccuracyCalculatorSimpleMedianBiasEqual : IAccuracyCalculator {
        private double _bias;
        public AccuracyCalculatorSimpleMedianBiasEqual(double bias) { _bias = bias; }

        public double Bias {
            get { return _bias; }
            set { _bias = value; }
        }

        public double Calculate(Matrix _left, Matrix _right) {
            if (_left == null) throw new ArgumentNullException(nameof(_left));
            if (_right == null) throw new ArgumentNullException(nameof(_right));
            if (_right.Columns != _left.Columns || _right.Rows != _left.Rows) throw new Exception("wrong matrix to compare");

            int successCount = 0;
            for (int i = 0; i < _left.Columns; i++) {
                int j;
                double sum = 0;
                for (j = 1; j < _left.Rows; j++) { sum += Math.Abs(_left[j, i] - _right[j, i]); }
                sum /= _left.Rows - 1;
                if (sum < _bias) successCount++;
            }
            return (double)successCount / _left.Columns;
        }
    }

    /// <summary>
    /// compare each elements of vector whith bias
    /// </summary>
    [Serializable]
    public class AccuracyCalculatorSimpleBiasABSEqual : IAccuracyCalculator {
        private double _bias;
        public AccuracyCalculatorSimpleBiasABSEqual(double bias) { _bias = bias; }

        public double Bias {
            get { return _bias; }
            set { _bias = value; }
        }

        public double Calculate(Matrix _left, Matrix _right) {
            if (_left == null) throw new ArgumentNullException(nameof(_left));
            if (_right == null) throw new ArgumentNullException(nameof(_right));
            if (_right.Columns != _left.Columns || _right.Rows != _left.Rows) throw new Exception("wrong matrix to compare");

            int successCount = 0;
            for (int i = 0; i < _left.Columns; i++) {
                int j;
                for (j = 1; j < _left.Rows; j++) {
                    if (Math.Abs(_left[j, i] - _right[j, i]) > _bias) break;
                }
                if (j == _left.Rows) successCount++;
            }
            return (double)successCount / _left.Columns;
        }
    }

    class AccuracyCalculatorDecart : IAccuracyCalculator {
        private double _bias;
        public AccuracyCalculatorDecart(double bias) { _bias = bias; }

        public double Bias {
            get { return _bias; }
            set { _bias = value; }
        }
        public double Calculate(Matrix _left, Matrix _right) {
            if (_left == null) throw new ArgumentNullException(nameof(_left));
            if (_right == null) throw new ArgumentNullException(nameof(_right));
            if (_right.Columns != _left.Columns || _right.Rows != _left.Rows) throw new Exception("wrong matrix to compare");

            int successCount = 0;
            for (int i = 0; i < _left.Columns; i++) {
                double rez = 0;
                double a;
                for (int j = 1; j < _left.Rows; j++) {
                    a = _left[j, i] - _right[j, i];
                    rez += a * a;
                }
                rez = Math.Sqrt(rez);
                if (rez < _bias) successCount++;
            }
            return (double)successCount / _left.Columns;
        }

    }

    #endregion

    #region Networks

    interface INeuroNet {
        double L2Regularization { get; set; }
        List<ILayer> Layers { get; set; }
        double LearnInert { get; set; }
        double LearnRate { get; set; }
        ILossFunk LossFunk { get; set; }

        void AddLayer(ILayer newLayer);
        void AddHiddenLayer(INeuron _neuron, int _neuronCount);
    //    Matrix ForvardPropagation(Matrix entry);
        Matrix Activate(Matrix entry);
        double getAccuracy(Matrix _samples, Matrix _answers, IAccuracyCalculator _calculator);
        double[] train(Matrix _samples, Matrix _answers, int _batchSize, int _maxIteration, double _accuracy, Matrix _testSamples, Matrix _testAnswers, IAccuracyCalculator _accuracyCalculator, string _savingPath);
        void save(string _filename);
    //    void load(string _filename);
    }

    [Serializable]
    class NeuroNetFullyConnected : INeuroNet {
        /// <summary>
        /// first must be input layer
        /// </summary>
        private List<ILayer> _layers = new List<ILayer>();


        private ILossFunk _lossFunk = new LossFunkSquare();
        private double _learnRate = 0.5;
        private double _learnInert = 0.0;
        private double _l2Regularization = 0.0;
        private List<Matrix> _prevDeltaWeights = null;  //for inert

        //for saving
        private double  _minAccurToSave = 0.25;

        public NeuroNetFullyConnected() { }

        public NeuroNetFullyConnected(List<ILayer> _layers) {
            if (_layers == null) throw new ArgumentNullException(nameof(_layers));
            if (_layers.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(_layers));
            this._layers = _layers;
        }

        public NeuroNetFullyConnected(ILayer layer) {
            if (layer == null) throw new ArgumentNullException(nameof(layer));
            if (!(layer is LayerInput)) throw new Exception("Trying add wrong input layer to new NeuroNetFullyConnected");
            _layers.Add(layer);
        }
        public NeuroNetFullyConnected(ILayer layer, double _learnRate, double _learnInert, double _L2Regularization) {
            if (layer == null) throw new ArgumentNullException(nameof(layer));
            if (!(layer is LayerInput)) throw new Exception("Trying add wrong input layer to new NeuroNetFullyConnected");
            this.LearnRate = _learnRate;
            this.LearnInert = _learnInert;
            this.L2Regularization = _L2Regularization;
            _layers.Add(layer);
        }

        public NeuroNetFullyConnected(List<ILayer> _layers, ILossFunk _lossFunk, double _learnRate, double _learnInert, double _L2Regularization) {
            if (_layers == null) throw new ArgumentNullException(nameof(_layers));
            if (_lossFunk == null) throw new ArgumentNullException(nameof(_lossFunk));
            if (_layers.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(_layers));
            this._layers = _layers;
            this._lossFunk = _lossFunk;
            this.LearnRate = _learnRate;
            this.LearnInert = _learnInert;
            this.L2Regularization = _L2Regularization;
        }
        public NeuroNetFullyConnected(List<ILayer> _layers, double _learnRate, double _learnInert, double _L2Regularization) {
            if (_layers == null) throw new ArgumentNullException(nameof(_layers));
            if (_layers.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(_layers));
            this._layers = _layers;
            this.LearnRate = _learnRate;
            this.LearnInert = _learnInert;
            this.L2Regularization = _L2Regularization;
        }
        public List<ILayer> Layers {
            get { return _layers; }
            set { _layers = value; }
        }

        public ILossFunk LossFunk {
            get { return _lossFunk; }
            set { _lossFunk = value; }
        }

        public double LearnRate {
            get { return _learnRate; }
            set {
                if (value <= 0 || value > 1) throw new ArgumentOutOfRangeException(nameof(value));
                _learnRate = value;
            }
        }

        public double LearnInert {
            get { return _learnInert; }
            set {
                if (value < 0 || value >= 1) throw new ArgumentOutOfRangeException(nameof(value));
                _learnInert = value;
            }
        }

        public double L2Regularization {
            get { return _l2Regularization; }
            set {
                if (value < 0 || value > 1) throw new ArgumentOutOfRangeException(nameof(value));
                _l2Regularization = value; }
        }

        public double MinAccurToSave {
            get { return _minAccurToSave; }
            set { _minAccurToSave = value; }
        }

        /// <summary>
        /// add new layer to neuronet, firs must be starting layer
        /// </summary>
        /// <param name="newLayer"></param>
        public void AddLayer(ILayer newLayer) {
            if ((_layers.Count == 0 && !(newLayer is LayerInput)) || (_layers.Count > 0 && newLayer is LayerInput)) throw new Exception("Trying add wrong layer");
            if (_layers.Count == 0) _layers.Add(newLayer);
            else {
                if (newLayer.PrevLayerNeuronCount() != _layers.Last().LayerNeuronCount()) throw new Exception("Incompatible layer to add");
                _layers.Add(newLayer);
            }
        }

        /// <summary>
        /// add new layer to neuronet, firs must be aded before
        /// </summary>
        /// <param name="newLayer"></param>
        /// <param name="_neuronCount"></param>
        public void AddHiddenLayer(INeuron _neuron, int _neuronCount) {
            AddLayer(new LayerNormal(_neuron, Layers.Last().LayerNeuronCount(), _neuronCount));
        }

        /// <summary>
        /// Do forvard propagation and return vectors(matrix) vector of output neurons(which have first elem of bias)
        /// </summary>
        /// <param name="entry">vertical vectors(matrix) of input data, must have first '1' for bias</param>
        /// <returns></returns>
        private Matrix ForvardPropagation(Matrix entry) {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            Matrix outcome = entry;
            for (int i = 0; i < _layers.Count; i++) {
                outcome = _layers[i].Outcome(outcome);
            }
            return outcome;
        }

        /// <summary>
        /// Do forvard propagation and return list of vertical vectors(matrix)(which have first elem of bias) of output neurons in envery layer
        /// </summary>
        /// <param name="entry">vertical vectors(matrix) of input data, entry must have first '1' for bias</param>
        /// <returns></returns>
        private List<Matrix> ForvardPropagationSaveSteps(Matrix entry) {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            List<Matrix> rez = new List<Matrix>();
            Matrix outcome = entry;
            for (int i = 0; i < _layers.Count; i++) {
                outcome = _layers[i].Outcome(outcome);
                rez.Add(outcome);
            }
            return rez;
        }

        /// <summary>
        ///Perform back propagation algorithm,
        /// with bias, and gradient sum on _samples, and regularization (_samples - 1 batch)
        /// </summary>
        /// <param name="_samples">matrix of _samples, each columns is one sample, first elems of each is 1 for bias</param>
        /// <param name="_answers">answers to this _samples, each columns is one answer, first elems of each is 1 for bias</param>
        /// <returns></returns>
        public void BackPropagation(Matrix _samples, Matrix _answers) {
            //2
            // steps matrix for each examples, each column for 1 example, each matrix in list for 1 layer
            List<Matrix> forwardpropag = ForvardPropagationSaveSteps(_samples);

            //3
            List<Matrix> layerErrors = new List<Matrix>(); //each matrix is one layer(starting from first hidden), each column - for one sample
            //  Matrix answers = _answers.ComposeAddRow(new Matrix(1, _answers.Columns, 1), 0); //added 1 to compare whith output which have bias

            //for last layer
            layerErrors.Add(Matrix.MultiplySimple(forwardpropag.Last() - _answers, forwardpropag.Last().ApplyFunc(Layers.Last().Neuron.f1)));

            //4                                                                              
            for (int i = forwardpropag.Count - 2; i > 0; i--) {
                layerErrors.Insert(0, Matrix.MultiplySimple(Layers[i + 1].Weights.Transpose() * layerErrors[0], forwardpropag[i].ApplyFunc(Layers[i].Neuron.f1)));
            }
            //5
            List<Matrix> deltaWeight = new List<Matrix>(); //delta w, for each layers, except starting
            for (int i = 1; i < Layers.Count; i++) { deltaWeight.Add(layerErrors[i - 1] * (forwardpropag[i - 1].Transpose())); }

            //6a - L2 regularization
            if (L2Regularization != 0.0) {
                for (int i = 1; i < Layers.Count; i++) { deltaWeight[i - 1] += Layers[i].Weights * L2Regularization; }
            }
            //6b - inertnist
            if (this.LearnInert != 0.0) {
                if (_prevDeltaWeights != null) {
                    for (int i = 1; i < Layers.Count; i++) {
                        deltaWeight[i - 1] *= 1.0 - LearnInert;
                        deltaWeight[i - 1] += _prevDeltaWeights[i - 1] * LearnInert;
                    }
                }
                _prevDeltaWeights = deltaWeight;
            }
            //6 adding deltaw       
            for (int i = 1; i < Layers.Count; i++) { Layers[i].Weights += deltaWeight[i - 1] * (-LearnRate/_samples.Columns); } // divide _samles.columns - batch size
        }

        /// <summary>
        /// train neoronet, returns accuracy on each epoh
        /// </summary>
        /// <param name="_samples">doesn't contain first 1 for bias</param>
        /// <param name="_answers">doesn't contain first 1 for bias</param>
        /// <param name="_batchSize"></param>
        /// <param name="_maxIteration"></param>
        /// <param name="_accuracy">% of recognized samples, 0..1</param>
        /// <param name="_testSamples">doesn't contain first 1 for bias</param>
        /// <param name="_testAnswers">doesn't contain first 1 for bias</param>
        /// <param name="_accuracyCalculator">for testing accuracy</param>
        /// <param name="_savingPath">path for saving good nets</param>
        /// <returns></returns>
        public double[] train(Matrix _samples, Matrix _answers, int _batchSize, int _maxIteration, double _accuracy, Matrix _testSamples, Matrix _testAnswers, IAccuracyCalculator _accuracyCalculator, string _savingPath) {
            if (_samples == null) throw new ArgumentNullException(nameof(_samples));
            if (_answers == null) throw new ArgumentNullException(nameof(_answers));
            if (_testSamples == null) throw new ArgumentNullException(nameof(_testSamples));
            if (_testAnswers == null) throw new ArgumentNullException(nameof(_testAnswers));
            if (_accuracyCalculator == null) throw new ArgumentNullException(nameof(_accuracyCalculator));
            if (_samples.Columns != _answers.Columns) throw new Exception("samples doesn't fit to answers");
            if (_batchSize <= 0 || _batchSize > _samples.Columns) throw new ArgumentOutOfRangeException(nameof(_batchSize));
            if (_maxIteration <= 0) throw new ArgumentOutOfRangeException(nameof(_maxIteration));
            if (_accuracy < 0 || _accuracy > 1) throw new ArgumentOutOfRangeException(nameof(_accuracy));

            //adding first "1" in samples and answers for algoritm
            Matrix samples1 = _samples.ComposeAddRow(new Matrix(1, _samples.Columns, 1), 0);
            Matrix answers1 = _answers.ComposeAddRow(new Matrix(1, _answers.Columns, 1), 0);

            //вибір даних з самплес в батч
            List<KeyValuePair<Matrix, Matrix>> batches = new List<KeyValuePair<Matrix, Matrix>>(); //batches (KVP<samples,answers>)

            if (samples1.Columns / _batchSize == 0 && samples1.Columns % _batchSize == 0) {
                batches.Add(new KeyValuePair<Matrix, Matrix>(samples1, answers1));
            } else {
                for (int i = 0; i < samples1.Columns / _batchSize; i++) {
                    batches.Add(new KeyValuePair<Matrix, Matrix>(samples1.Submatrix(0, samples1.Rows - 1, i * _batchSize, (i + 1) * _batchSize - 1),
                        answers1.Submatrix(0, answers1.Rows - 1, i * _batchSize, (i + 1) * _batchSize - 1)));
                }

                if (samples1.Columns % _batchSize != 0) {
                    batches.Add(
                        new KeyValuePair<Matrix, Matrix>(
                            samples1.Submatrix(0, samples1.Rows - 1, samples1.Columns / _batchSize * _batchSize, samples1.Columns - 1),
                            answers1.Submatrix(0, answers1.Rows - 1, answers1.Columns / _batchSize * _batchSize, samples1.Columns - 1)));
                }
            }
            //train
            List<double> rez = new List<double>();
            double curAccur;
              
            for (int i = 0; i < _maxIteration; i++) {
                for (int j = 0; j < batches.Count; j++) { BackPropagation(batches[j].Key, batches[j].Value); }
                curAccur = getAccuracy(_testSamples, _testAnswers, _accuracyCalculator);
                rez.Add(curAccur);
                if (curAccur > _accuracy)break;

                if (curAccur > _minAccurToSave) {
                    this.save(_savingPath + "\\" + curAccur + " " + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Ticks);
                    _minAccurToSave = curAccur;
                }
            }
            return rez.ToArray<double>();
        }

        public double getValueOfLossFunc(Matrix _samples, Matrix _answers) { return LossFunk.GetLoss(ForvardPropagation(_samples), _answers); }

        /// <summary>
        /// returns % of right recognized samples
        /// </summary>
        /// <param name="_samples">without bias "1"</param>
        /// <param name="_answers">without bias "1"</param>
        /// <param name="_calculator"></param>
        /// <returns></returns>
        public double getAccuracy(Matrix _samples, Matrix _answers, IAccuracyCalculator _calculator) {
            if (_samples == null) throw new ArgumentNullException(nameof(_samples));
            if (_answers == null) throw new ArgumentNullException(nameof(_answers));
            if (_calculator == null) throw new ArgumentNullException(nameof(_calculator));

            //adding first "1" in samples and answers for algoritm
            Matrix samples1 = _samples.ComposeAddRow(new Matrix(1, _samples.Columns, 1), 0);
            Matrix answers1 = _answers.ComposeAddRow(new Matrix(1, _answers.Columns, 1), 0);

            Matrix outcome = ForvardPropagation(samples1);

            return _calculator.Calculate(outcome, answers1);
        }

        public void save(string _filename) {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fStream = new FileStream(_filename, FileMode.Create, FileAccess.Write, FileShare.None)) {
                formatter.Serialize(fStream, this);
            }

        }

        public static NeuroNetFullyConnected load(string _filename) {
            NeuroNetFullyConnected rez;
            using (var fStream = File.OpenRead(_filename)) {
                 rez = (NeuroNetFullyConnected) new BinaryFormatter().Deserialize(fStream);
            }
            return rez;
        }

        /// <summary>
        /// perform forward propagation, return rezult (clear rez, whithout bias)
        /// </summary>
        /// <param name="entry">clear entry, without bias</param>
        /// <returns></returns>
        public Matrix Activate(Matrix entry) {
            Matrix rez = ForvardPropagation(entry.ComposeAddRow(new Matrix(1, entry.Columns, 1), 0));
            return rez.Submatrix(1, rez.Rows - 1, 0, rez.Columns - 1);

        }
    }

    #endregion

}
