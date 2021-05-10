using System;
using System.Collections;
using System.Globalization;

/* На вход подается число N.
 * На каждой из следующих N строках записаны ФИО человека, 
 * разделенные одним пробелом. Отчество может отсутствовать.
 * Используя собственноручно написанный итератор, выведите имена людей,
 * отсортированные в лексико-графическом порядке в формате 
 *      <Фамилия_с_большой_буквы> <Заглавная_первая_буква_имени>.
 * Затем выведите имена людей в исходном порядке.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield.
 * 
 * Пример входных данных:
 * 3
 * Banana Bill Bananovich
 * Apple Alex Applovich
 * Carrot Clark Carrotovich
 * 
 * Пример выходных данных:
 * Apple A.
 * Banana B.
 * Carrot C.
 * 
 * Banana B.
 * Apple A.
 * Carrot C.
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
*/
namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
                long N = long.Parse(Console.ReadLine());
                Person[] people = new Person[N];
                for (long i = 0; i < N; i++)
                {
                    string[] name = Console.ReadLine().Split();
                    people[i] = new Person(name[0], name[1]);
                }
                People peopleList = new People(people);

                foreach (Person p in peopleList.GetPeople)
                    Console.WriteLine(p);

                foreach (Person p in peopleList)
                    Console.WriteLine(p);
            }
            catch (ArgumentException)
            {
                Console.Write("error");
            }
            Console.ReadLine();
        }
    }

    public class Person
    {
        public string firstName;
        public string lastName;

        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public override string ToString()
        {
            return $"{firstName[0].ToString().ToUpper()}{firstName.Substring(1, firstName.Length - 2)} {lastName[0].ToString().ToUpper()}.";
        }
    }


    public class People : IEnumerable
    {
        private Person[] _people;
        public Person[] GetPeople
        {
            get
            {
                Person[] sortPeople = new Person[_people.Length];
                Array.Copy(_people, sortPeople, _people.Length);
                Array.Sort(sortPeople, (a, b) => a.lastName.CompareTo(b.lastName));
                return sortPeople;
            }
        }
        public People(Person[] persons)
        {
            _people = persons;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }

    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;
        private int position = -1;
        public PeopleEnum(Person[] people)
        {
            _people = people;
        }

        public bool MoveNext()
        {
            if (position == _people.Length - 1)
            {
                Reset();
                return false;
            }
            position++;
            return true;
        }

        public void Reset()
        {
            position = -1;
        }


        public Person Current
        {
            get
            {
                return _people[position];
            }
        }

        object IEnumerator.Current => Current;
    }
}
