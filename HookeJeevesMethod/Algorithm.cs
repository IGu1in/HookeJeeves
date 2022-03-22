using System;
using System.Collections.Generic;

namespace HookeJeevesMethod
{
    public class Algorithm
    {
        private List<Point> _points = new List<Point>();
        private List<Point> _basis = new List<Point>();
        private readonly decimal _accelerating = 2M;
        private readonly int _dimension = 2;
        private readonly decimal _stepReduction = 4;
        private readonly decimal _epsilon = 0.1M;
        private decimal _stepByX = 0.2M;
        private decimal _stepByY = 0.4M;
        private Point _currentPoint;
        private Point _comprasionPoint;
        private bool _isEnding = false;
        private int _countPasses = 0;

        public void Calculate()
        {
            _currentPoint = new Point(0.1M, 1);
            _points.Add(_currentPoint);
            _basis.Add(_currentPoint);
            var isX = true;
            var i = 1;

            while (!_isEnding)
            {
                _comprasionPoint = CoordinateOffset(_currentPoint, true, isX);

                if (_comprasionPoint.Value < _currentPoint.Value)
                {
                    //step 2a
                    isX = false;
                    _currentPoint = _comprasionPoint;
                    _points.Add(_currentPoint);

                    if (i < _dimension)
                    {
                        //step 3a
                        i++;
                    }
                    else
                    {
                        //step 3b
                        if (_points[2].Value < _basis[_countPasses].Value)
                        {
                            //step 4
                            _basis.Add(_currentPoint);
                            _countPasses++;
                            _currentPoint = new Point(_basis[_countPasses].X + _accelerating * (_basis[_countPasses].X - _basis[_countPasses-1].X),
                                _basis[_countPasses].Y + _accelerating * (_basis[_countPasses].Y - _basis[_countPasses - 1].Y));
                            _points.Clear();
                            _points.Add(_currentPoint);
                            isX = true;
                            i = 1;
                        }
                        else
                        {
                            //step 5
                            if (_stepByX <= _epsilon && _stepByY <= _epsilon)
                            {
                                _isEnding = true;
                                Console.WriteLine(_basis[_countPasses]);
                            }
                            else
                            {
                                if (_stepByX > _epsilon)
                                {
                                    _stepByX = _stepByX / _stepReduction;
                                }

                                if (_stepByY > _epsilon)
                                {
                                    _stepByY = _stepByY / _stepReduction;
                                }

                                _basis.Add(_basis[_countPasses]);
                                _points.Clear();
                                _points.Add(_basis[_countPasses]);
                                _currentPoint = _points[0];
                                i = 1;
                                _countPasses++;
                                isX = true;
                            }
                        }
                    }
                }
                else
                {
                    //step 2b
                    _comprasionPoint = CoordinateOffset(_currentPoint, false, isX);

                    if (_comprasionPoint.Value < _currentPoint.Value)
                    {
                        isX = false;
                        _currentPoint = _comprasionPoint;
                        _points.Add(_currentPoint);

                        if (i < _dimension)
                        {
                            //step 3a
                            i++;
                        }
                        else
                        {
                            //step 3b
                            if (_points[2].Value < _basis[_countPasses].Value)
                            {
                                //step 4
                                _basis.Add(_currentPoint);
                                _countPasses++;
                                _currentPoint = new Point(_basis[_countPasses].X + _accelerating * (_basis[_countPasses].X - _basis[_countPasses - 1].X),
                                    _basis[_countPasses].Y + _accelerating * (_basis[_countPasses].Y - _basis[_countPasses - 1].Y));
                                _points.Clear();
                                _points.Add(_currentPoint);
                                isX = true;
                                i = 1;
                            }
                            else
                            {
                                //step 5
                                if (_stepByX < _epsilon && _stepByY < _epsilon)
                                {
                                    _isEnding = true;
                                    Console.WriteLine(_basis[_countPasses]);
                                }
                                else
                                {
                                    if (_stepByX >= _epsilon)
                                    {
                                        _stepByX = _stepByX / _stepReduction;
                                    }

                                    if (_stepByY >= _epsilon)
                                    {
                                        _stepByY = _stepByY / _stepReduction;
                                    }

                                    _basis.Add(_basis[_countPasses]);
                                    _points.Clear();
                                    _points.Add(_basis[_countPasses]);
                                    i = 1;
                                    _countPasses++;
                                    isX = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        //step 2c
                        _points.Add(_currentPoint);

                        if (i < _dimension)
                        {
                            //step 3a
                            i++;
                        }
                        else
                        {
                            //step 3b
                            if (_points[2].Value < _basis[_countPasses].Value)
                            {
                                //step 4
                                _basis.Add(_currentPoint);
                                _countPasses++;
                                _currentPoint = new Point(_basis[_countPasses].X + _accelerating * (_basis[_countPasses].X - _basis[_countPasses - 1].X),
                                    _basis[_countPasses].Y + _accelerating * (_basis[_countPasses].Y - _basis[_countPasses - 1].Y));
                                _points.Clear();
                                _points.Add(_currentPoint);
                                isX = true;
                                i = 1;
                            }
                            else
                            {
                                //step 5
                                if (_stepByX < _epsilon && _stepByY < _epsilon)
                                {
                                    _isEnding = true;
                                    Console.WriteLine(_basis[_countPasses]);
                                }
                                else
                                {
                                    if (_stepByX >= _epsilon)
                                    {
                                        _stepByX = _stepByX / _stepReduction;
                                    }

                                    if (_stepByY >= _epsilon)
                                    {
                                        _stepByY = _stepByY / _stepReduction;
                                    }

                                    _basis.Add(_basis[_countPasses]);
                                    _points.Clear();
                                    _points.Add(_basis[_countPasses]);
                                    i = 1;
                                    _countPasses++;
                                    isX = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private Point CoordinateOffset(Point point, bool isIncrease, bool isX)
        {
            if (isX)
            {
                if (isIncrease)
                {
                    return new Point(point.X + _stepByX, point.Y);
                }
                else
                {
                    return new Point(point.X - _stepByX, point.Y);
                }
            }
            else
            {
                if (isIncrease)
                {
                    return new Point(point.X, point.Y + _stepByY);
                }
                else
                {
                    return new Point(point.X, point.Y - _stepByY);
                }
            }
        }
    }
}
