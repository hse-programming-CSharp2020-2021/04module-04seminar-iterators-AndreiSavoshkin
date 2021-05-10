using System;
using System.Collections;

/* На вход подается число N.
 * Нужно создать коллекцию из N элементов последовательного ряда натуральных чисел, возведенных в 10 степень, 
 * и вывести ее на экран ТРИЖДЫ. Инвертировать порядок элементов при каждом последующем выводе.
 * Элементы коллекции разделять пробелом. 
 * Очередной вывод коллекции разделять переходом на новую строку.
 * Не хранить всю коллекцию в памяти.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield и foreach. Не вызывать метод Reset() в классе Program.
 * 
 * Пример входных данных:
 * 2
 * 
 * Пример выходных данных:
 * 1 1024
 * 1024 1
 * 1 1024
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
 * В других ситуациях выбрасывайте 
*/
namespace Task05
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int value;
                if (!int.TryParse(Console.ReadLine(), out value))
                {
                    throw new ArgumentException();
                }
                MyDigits myDigits = new MyDigits(value);
                IEnumerator enumerator = myDigits.MyEnumerator(value);

                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
            catch (ArithmeticException)
            {
                Console.WriteLine("ooops");
            }
        }

        static void IterateThroughEnumeratorWithoutUsingForeach(IEnumerator enumerator)
        {
            string output = "";
            while (enumerator.MoveNext())
            {
                output += enumerator.Current + " ";
            }
            Console.Write(output.Remove(output.Length - 1));
        }
    }

    class MyDigits : IEnumerator // НЕ МЕНЯТЬ ЭТУ СТРОКУ
    {
        int position = -1;
        int value;
        bool isReserve = false;
        public MyDigits(int value)
        {
            this.value = value;
        }
        public object Current => Math.Pow(position + 1, 10);

        public bool MoveNext()
        {
            if (!isReserve)
            {
                if (position + 1 == value)
                {
                    Reset();
                    return false;
                }
                position++;
                return true;
            }
            if (position - 1 == -1)
            {
                Reset();
                return false;
            }
            position--;
            return true;
        }

        public void Reset()
        {
            position = isReserve ? -1 : value;
            isReserve = !isReserve;

        }

        internal IEnumerator MyEnumerator(int value)
        {
            this.value = value;
            return this;
        }
    }
}
