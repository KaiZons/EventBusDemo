using MessageControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //delegate void TestDele(string str);
    class Program
    {

        public static string Test(string str)
        {
            return Test2(GetAction());
            //return Test2((a, b) => { });
        }

        public static Action<string, int> GetAction()
        {
            return (a, b) => { };
        }
        public static string Test2(Action<string, int> action)
        {
            return "";
        }
        static void Main(string[] args)
        {
            //int[] array = { 1, 2, 3, 4, 5 };
            //IntArray intArray = new IntArray(array);
            ////测试，可以使用foreach
            //foreach (int i in intArray)
            //{
            //    //do some things
            //}
            Student stu = new Student("zhoukaikai", 25);
            WeakReference weakReference = new WeakReference(stu);
            Student strongRefer = weakReference.Target as Student;
            if (strongRefer != null)
            {
                //Student没有被回收
            }
        }

        /// <summary>
        /// 自定义IntArray类
        /// 注意：
        ///     1.没有实现IEnumerable接口
        ///     2.类中定义了GetEnumerator()方法，该方法与IEnumerable接口中的方法一致
        /// </summary>
        private class IntArray
        {
            private int[] m_array;
            public IntArray(int[] array)
            {
                m_array = array;
            }
            public IEnumerator GetEnumerator()
            {
                //注意：需要返回一个实现了IEnumerator接口的对象
                return new MyEnumerator(m_array);
            }
        }



        private class MyEnumerator<T>: IEnumerator where T : Student
        {
            private int index = -1;//记录当前迭代器的位置，初始为-1
            private int[] intArray;//当前集合
            public void Test(T rr)
            {
                rr.
            }
            public MyEnumerator(int[] array)
            {
                intArray = array;
            }

            //实现接口方法
            public object Current
            {
                get { return intArray[index]; }
            }

            //实现接口方法
            public bool MoveNext()
            {
                index++;
                if (index < intArray.Length)
                {
                    return true;
                }
                return false;
            }

            //实现接口方法
            public void Reset()
            {
                index = -1;
            }
        }
    }
}
