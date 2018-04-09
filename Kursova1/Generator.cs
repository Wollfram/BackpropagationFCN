using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova1 {
    interface ISamplesGenerator {
        KeyValuePair<Matrix, Matrix> Generate(int count);
    }

    class SamplesGeneratorBeze : ISamplesGenerator {
        private Random _rand = new Random();

        /// <summary>
        /// min/max values for X and Y cords
        /// </summary>
        private Beze.PointD _cordsBound = new Beze.PointD(0, 10);

        private int _segmentsCount;


        public int SegmentsCount {
            get { return _segmentsCount; }
            set {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                _segmentsCount = value;
            }
        }

        public Beze.PointD CordsBound {
            get { return _cordsBound; }

            set {
                if (value.Y < value.X) throw new Exception("Illegal cords bound");
                _cordsBound = value;
            }
        }

        public SamplesGeneratorBeze(int segmentsCount) { SegmentsCount = segmentsCount; }

        public SamplesGeneratorBeze(Beze.PointD cordsBound, int segmentsCount) {
            this.CordsBound = cordsBound;
            this.SegmentsCount = segmentsCount;
        }

        /// <summary>
        /// generate samples and answers, samples - cords of beze x1,y1,x2,y2,x3,y3,x4,y4
        /// answers = angles of parial vectors of beze line,
        /// answers and samples are normalized
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public KeyValuePair<Matrix, Matrix> Generate(int count) {
            Matrix rezSamples = new Matrix(8, count);
            Matrix rezAnswers = new Matrix(_segmentsCount, count);


            for (int i = 0; i < rezSamples.Columns; i++) {
                do {
                    // for (int j = 0; j < rezSamples.Rows; j++) { rezSamples[j, i] = _rand.NextDouble() * (_cordsBound.Y - CordsBound.X) + CordsBound.X; }
                    for (int j = 2; j < rezSamples.Rows - 2; j++) { rezSamples[j, i] = _rand.NextDouble() * (_cordsBound.Y - CordsBound.X) + CordsBound.X; }
                    rezSamples[0, i] = CordsBound.X;
                    rezSamples[1, i] = CordsBound.Y / 2;
                    rezSamples[6, i] = CordsBound.Y;
                    rezSamples[7, i] = CordsBound.Y / 2;
                } while (rezSamples[0, i] > rezSamples[6, i] || rezSamples[2, i] < rezSamples[0, i] || rezSamples[2, i] > rezSamples[6, i] ||
                         rezSamples[4, i] < rezSamples[0, i] || rezSamples[4, i] > rezSamples[6, i] // ||

                    //   Math.Abs(rezSamples[1,i]-rezSamples[7,i]) < (_cordsBound.Y - _cordsBound.X) * 0.1 ||
                    //    Math.Abs(rezSamples[0, i] - rezSamples[6, i]) < (_cordsBound.Y - _cordsBound.X) * 0.1
                );

            }


            double step = (double) 1 / _segmentsCount;

            for (int j = 0; j < rezAnswers.Columns; j++) {
                Beze curBez = new Beze(new Beze.PointD(rezSamples[0, j], rezSamples[1, j]), new Beze.PointD(rezSamples[2, j], rezSamples[3, j]),
                    new Beze.PointD(rezSamples[4, j], rezSamples[5, j]), new Beze.PointD(rezSamples[6, j], rezSamples[7, j]));
                Beze.PointD prevPoint = curBez.Points[0];
                for (int i = 0; i < rezAnswers.Rows; i++) {
                    Beze.PointD curPoint = curBez.GetCords((i + 1) * step);
                    rezAnswers[i, j] = getNormalizedAnglePlusMinusOne(curPoint - prevPoint);
                    prevPoint = curPoint;
                }
                //Beze normaBez = normalizeBezeZeroToOne(curBez);
                Beze normaBez = nornormalizeBezePlusMinusOne(curBez);

                rezSamples[0, j] = normaBez.Points[0].X;
                rezSamples[1, j] = normaBez.Points[0].Y;
                rezSamples[2, j] = normaBez.Points[1].X;
                rezSamples[3, j] = normaBez.Points[1].Y;
                rezSamples[4, j] = normaBez.Points[2].X;
                rezSamples[5, j] = normaBez.Points[2].Y;
                rezSamples[6, j] = normaBez.Points[3].X;
                rezSamples[7, j] = normaBez.Points[3].Y;
            }
            return new KeyValuePair<Matrix, Matrix>(rezSamples, rezAnswers);
        }

        /// <summary>
        /// returns  angle in Grad, +-0..180
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        private double getVecAngle(Beze.PointD vec1) {
            //double L = Math.Sqrt(vec1.X * vec1.X + vec1.Y * vec1.Y);
            //double cos = vec1.X / L;
            //double grad = Math.Acos(cos) * 180 / Math.PI;
            //if (vec1.Y < 0) grad = -grad;
            return vec1.Y > 0
                ? Math.Acos(vec1.X / Math.Sqrt(vec1.X * vec1.X + vec1.Y * vec1.Y)) * 180 / Math.PI
                : -Math.Acos(vec1.X / Math.Sqrt(vec1.X * vec1.X + vec1.Y * vec1.Y)) * 180 / Math.PI;
        }

        /// <summary>
        /// normalize angle -1..1
        /// </summary>
        /// <param name="angle">angle in grad [-180, 180]</param>
        /// <returns></returns>
        private double normalizeAnglePlusMinusOne(double angle) {
            return angle / 180;
        }

        /// <summary>
        /// gets vector angle in Grad, +-0..180 an normalize it to -1..1
        /// </summary>
        /// <param name="vec1"></param>
        /// <returns></returns>
        private double getNormalizedAnglePlusMinusOne(Beze.PointD vec1) {
            return vec1.Y > 0
                ? Math.Acos(vec1.X / Math.Sqrt(vec1.X * vec1.X + vec1.Y * vec1.Y)) / Math.PI
                : -Math.Acos(vec1.X / Math.Sqrt(vec1.X * vec1.X + vec1.Y * vec1.Y)) / Math.PI;
        }

        /// <summary>
        /// normalize points cords of beze to 0..1
        /// </summary>
        /// <param name="b1"></param>
        /// <returns></returns>
        private Beze normalizeBezeZeroToOne(Beze bez) {
            Beze rez = bez.copy();
            for (int i = 0; i < 4; i++) {
                if (rez.Points[i].X < 0) {
                    double up = -rez.Points[i].X;
                    for (int j = 0; j < 4; j++) { rez.Points[j].X += up; }
                }
                if (rez.Points[i].Y < 0) {
                    double up = -rez.Points[i].Y;
                    for (int j = 0; j < 4; j++) { rez.Points[j].Y += up; }
                }
            }

            double maxX = bez.Points[0].X;
            double maxY = bez.Points[0].Y;
            for (int i = 1; i < 4; i++) {
                maxX = maxX < bez.Points[i].X ? bez.Points[i].X : maxX;
                maxY = maxY < bez.Points[i].Y ? bez.Points[i].Y : maxY;
            }
            for (int i = 0; i < 4; i++) {
                rez.Points[i].X /= maxX;
                rez.Points[i].Y /= maxY;
            }
            return rez;
        }

        private Beze nornormalizeBezePlusMinusOne(Beze bez) {
            Beze rez = bez.copy();

            for (int i = 0; i < 4; i++) {
                if (rez.Points[i].X < 0) {
                    double up = -rez.Points[i].X;
                    for (int j = 0; j < 4; j++) { rez.Points[j].X += up; }
                }
                if (rez.Points[i].Y < 0) {
                    double up = -rez.Points[i].Y;
                    for (int j = 0; j < 4; j++) { rez.Points[j].Y += up; }
                }
            }
            double maxX = bez.Points[0].X;
            double maxY = bez.Points[0].Y;
            for (int i = 1; i < 4; i++) {
                maxX = maxX < bez.Points[i].X ? bez.Points[i].X : maxX;
                maxY = maxY < bez.Points[i].Y ? bez.Points[i].Y : maxY;
            }
            maxX /= 2;
            maxY /= 2;
            for (int i = 0; i < 4; i++) {
                rez.Points[i].X -= maxX;
                rez.Points[i].Y -= maxY;
            }

            for (int i = 0; i < 4; i++) {
                rez.Points[i].X /= maxX;
                rez.Points[i].Y /= maxY;
            }
            return rez;
        }

       
    }
}
