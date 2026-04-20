using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Game
{
    public class BitFilter : Filter
    {
        private bool[] _correct;
        private bool[] _current;
        private string _correctFormat;
        private string _incorrectFormat;

        private BitFilter() { }

        public int GetLength() => _correct.Length;

        public bool IsAllCorrect()
        {
            for (int i = 0; i < _correct.Length; i++)
                if (_correct[i] != _current[i])
                    return false;

            return true;
        }

        public void Switch(int index)
        {
            _current[index] = !_current[index];
            if (IsAllCorrect())
                SendComplete();
        }
        
        public string GetString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < _correct.Length; i++)
            {
                string symb = _current[i] ? "1" : "0";
                string format = _current[i] == _correct[i] ? _correctFormat : _incorrectFormat;
                builder.Append(string.Format(format, symb));
            }

            return builder.ToString();
        }
        
        public static BitFilter New(int length, int minIncorrectCount, float incorrectChance,
            string correctFormat, string incorrectFormat)
        {
            Assert.IsFalse(minIncorrectCount > length);
            
            var filter = new BitFilter();
            filter._correct = new bool[length];
            filter._current = new bool[length];
            filter._correctFormat = correctFormat;
            filter._incorrectFormat = incorrectFormat;

            for (int i = 0; i < length; i++)
            {
                var value = Random.Range(0, 2) == 1;
                filter._correct[i] = value;
                
                bool isIncorrect = Random.Range(0, 1f) <= incorrectChance;
                filter._current[i] = isIncorrect ? !value : value;
                if (isIncorrect)
                    minIncorrectCount--;
            }

            if (minIncorrectCount > 0)
            {
                while (minIncorrectCount > 0)
                {
                    var index = Random.Range(0, length);
                    if (filter._correct[index] != filter._current[index])
                        continue;

                    filter._current[index] = !filter._current[index];
                    minIncorrectCount--;
                }
            }

            return filter;
        }

        public bool IsOne(int currentIndex) => _current[currentIndex];
    }
}