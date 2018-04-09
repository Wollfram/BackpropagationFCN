using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova1 {
    [Serializable]
    class Beze {
        /// <summary>
        /// presents beze curve n3
        /// </summary>
        [Serializable]
        public class  PointD {
            public double X;
            public double Y;

            public PointD(double x, double y) {
                X = x;
                Y = y;
            }

            public PointD copy() {
                return  new PointD(this.X,this.Y);
            }
            public static PointD Add(PointD left, PointD right) {
                if (left == null) throw new ArgumentNullException(nameof(left));
                if (right == null) throw new ArgumentNullException(nameof(right));
                return new PointD(left.X + right.X, left.Y + left.X);
            }

            public static PointD operator +(PointD left, PointD right) { return Add(left, right); }

            public static PointD Subtract(PointD left, PointD right) {
                if (left == null) throw new ArgumentNullException(nameof(left));
                if (right == null) throw new ArgumentNullException(nameof(right));
                return new PointD(left.X - right.X, left.Y - left.X);
            }

            public static PointD operator -(PointD left, PointD right) { return Subtract(left, right); }
        }
        private PointD[] _points;


        public Beze(PointD[] _points) {
            if (_points == null) throw new ArgumentNullException("Beze constructor error");
            if (_points.Length != 4) throw new ArgumentException("Beze constructor error");
            this._points = _points;
        }
        public Beze(PointD p0, PointD p1, PointD p2, PointD p3) {
            if (p0 == null) throw new ArgumentNullException(nameof(p0));
            if (p1 == null) throw new ArgumentNullException(nameof(p1));
            if (p2 == null) throw new ArgumentNullException(nameof(p2));
            if (p3 == null) throw new ArgumentNullException(nameof(p3));
            this._points = new[] {p0, p1, p2, p3};
        }

        public Beze copy() {
            return new Beze(this._points[0].copy(),this._points[1].copy(),this._points[2].copy(),this._points[3].copy());
        }

        public PointD[] Points {
            get { return _points; }
            set { _points = value; }
        }

        public PointD GetCords(double t) {
            if (t < 0 || t > 1) throw new ArgumentOutOfRangeException(nameof(t));
            return
                new PointD(
                    (1 - t) * (1 - t) * (1 - t) * _points[0].X + 3 * t * (1 - t) * (1 - t) * _points[1].X +
                    3 * t * t * (1 - t) * _points[2].X + t * t * t * _points[3].X,
                    (1 - t) * (1 - t) * (1 - t) * _points[0].Y + 3 * t * (1 - t) * (1 - t) * _points[1].Y +
                    3 * t * t * (1 - t) * _points[2].Y + t * t * t * _points[3].Y);
        }
    }
}
