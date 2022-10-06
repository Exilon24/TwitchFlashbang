using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchFlashbang
{
    // (Not stolen code) ;)
    public class Tweener
    {
        private int _count;
        public float Min { get; }
        public float Max { get; }
        public float Step { get; }
        public float Value { get; private set; }
        public int Steps { get; }

        public Direction Direction { get; private set; }

        public Tweener(float min, float max, float step, int steps)
        {
            Min = min;
            Max = max;
            Step = step;
            Steps = steps;
            Value = min;
            Direction = Direction.Up;
        }

        public float Update()
        {
            var change = Direction switch
            {
                Direction.Up => Step,
                Direction.Down => -Step,
                _ => throw new ArgumentOutOfRangeException()
            };

            _count++;

            if (_count == Steps)
            {
                Direction = Direction switch
                {
                    Direction.Down => Direction.Up,
                    Direction.Up => Direction.Down,
                    _ => throw new ArgumentOutOfRangeException()
                };
                _count = 0;
            }

            var newValue = Math.Min(Max, Math.Max(Min, Value + change));
            Value = newValue;

            return Value;
        }
    }

    public enum Direction
    {
        Up,
        Down
    }
}
