using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace benchmarkLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<stringCompareBenchmark>();
            Console.ReadKey();
        }
    }

    //[MemoryDiagnoser]
    [ClrJob, CoreJob]
    public class stringCompareBenchmark
    {
        //[Params("This is a book", "marcus")]
        public string string1 = "i am iron man";
        //[Params("This is a book", "marcussss")]
        public string string2 = "I AM IRON MAN";

        [Benchmark]
        public void EqualityString()
        {
            if (string1 == string2) ;
        }
        [Benchmark]
        public void EqualString()
        {
            if (string1.Equals(string2)) ;
        }
        [Benchmark]
        public void CompareString()
        {
            if (string.Compare(string1, string2) == 0) ;
        }
        [Benchmark]
        public void CompareOrdinalString()
        {
            if (string.CompareOrdinal(string1, string2) == 0) ;
        }
        [Benchmark]
        public void CompareToString()
        {
            if (string1.CompareTo(string2) == 0) ;
        }
        [Benchmark]
        public void IndexOfString()
        {
            if (string1.IndexOf(string2) == 0) ;
        }
    }

    [MemoryDiagnoser]
    [ClrJob, CoreJob]
    public class stringTestBenchmark
    {
        StringBuilder _sb = new StringBuilder();
        [Benchmark]
        public void Normal()
        {
            string s = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                s += i.ToString();
            }
        }
        [Benchmark]
        public void StringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                sb.Append(i.ToString());
            }
            string s = sb.ToString();
        }

        [Benchmark]
        public void StringBuilderClear()
        {
            _sb.Clear();
            for (int i = 0; i < 10; i++)
            {
                _sb.Append(i.ToString());
            }
            string s = _sb.ToString();
        }

        [Benchmark]
        public void Format()
        {
            string s = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                s = string.Format("{0}{1}", s, i.ToString());
            }
        }
        [Benchmark]
        public void Interpolation()
        {
            string s = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                s = $"{s}{i.ToString()}";
            }
        }
        [Benchmark]
        public void Concat()
        {
            string s = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                s = string.Concat(s, i.ToString());
            }
        }
        [Benchmark]
        public void Join()
        {
            string s = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                s += string.Join(s, i.ToString());
            }
        }
    }

    [MemoryDiagnoser]
    [ClrJob,CoreJob]
    public class stringBenchmark
    {
        private static string firstWord = "This is a";
        private static string secondWord = "book";
        StringBuilder _sb = new StringBuilder();
        [Benchmark]
        public void Normal()
        {
            string s = string.Empty;
            s = firstWord;
            s += secondWord;
        }
        //[Benchmark]
        //public void StringBuilder()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(firstWord);
        //    sb.Append(secondWord);
        //    string s = sb.ToString();
        //}

        //[Benchmark]
        //public void StringBuilderClear()
        //{
        //    _sb.Clear();
        //    _sb.Append(firstWord);
        //    _sb.Append(secondWord);
        //    string s = _sb.ToString();
        //}

        //[Benchmark]
        //public void Format()
        //{
        //    string s = string.Empty;
        //    s = string.Format("{0} {1}", firstWord, secondWord);
        //}
        //[Benchmark]
        //public void Interpolation()
        //{
        //    string s = string.Empty;
        //    s = $"{firstWord}{secondWord}";
        //}
        //[Benchmark]
        //public void Concat()
        //{
        //    string s = string.Empty;
        //    s = string.Concat(firstWord, secondWord);
        //}
        [Benchmark]
        public void Join()
        {
            string s = firstWord;
            s += string.Join(s, secondWord);
        }
    }
}