using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestLabNET
{
    namespace Library
    {
        abstract class Edition // Класс "Издание"
        {
            private string _name;
            private string _author;
            private DateTime _published;

            public Edition()
            {
            }
            public Edition(string name, string author, DateTime published)
            {
                Name = name;
                Author = author;
                Published = published;
            }

            public string Name
            {
                get => _name;
                set => _name = value;
            }
            public string Author
            {
                get => _author;
                set => _author = value;
            }
            public DateTime Published
            {
                get => _published;
                set => _published = value;
            }

            virtual public string GenerateDescription() // Описание
            {
                return
                    "Автор: " + Author +
                    "\nГод издания: " + Published.ToShortDateString() +
                    "\nНазвание: " + Name +
                    "\n";
            }

            public override bool Equals(object other) // Переопределение класса
            {
                if ((other is null) || !this.GetType().Equals(other.GetType()))
                    return false;

                Edition otherEdition = (Edition)other;

                return
                    this.Name.Equals(otherEdition.Name) &&
                    this.Author.Equals(otherEdition.Author) &&
                    this.Published.Equals(otherEdition.Published);
            }
        }
        // Очень неохота комментировать дальше, ибо тут копипаст почти что полнейший.
        class Magazine : Edition // Издание типа "Журнал"
        {
            private List<string> _articles; // Массив статей

            public Magazine() : base()
            {
                Articles = new List<string>();
            }
            public Magazine(string name, string author, DateTime published, List<string> articles)
                : base(name, author, published)
            {
                Articles = articles;
            }

            public List<string> Articles
            {
                get => _articles;
                set => _articles = value;
            }

            public string Article() => Articles[Articles.Count - 1];
            public string Article(int index) => Articles[index];

            public void AddArticle(string item)
            {
                Articles.Add(item);
            }

            public override string GenerateDescription() // Вывод описания
            {
                string articlesDesc = string.Empty;

                foreach (string article in Articles)
                    articlesDesc += article + ";\n ";
                articlesDesc = articlesDesc.Trim(new char[] { ' ', ',' }); //Удаляет все начальные и конечные вхождения
                                                                           //набора символов, заданного в виде массива, из текущей строки.

                return base.GenerateDescription() +
                    "Статьи: \n " + articlesDesc; 
            }

            public override bool Equals(object other) // Переопределение метода
            {
                if ((other is null) || !this.GetType().Equals(other.GetType()))
                    return false;

                Magazine otherMagazine = (Magazine)other;

                return base.Equals(otherMagazine) && this.Articles.Equals(otherMagazine.Articles);
            }
        }

        class Book : Edition // Издание типа "Книга"
        {
            private string _resume;

            public Book() : base()
            {
            }
            public Book(string name, string author, DateTime published, string resume)
                : base(name, author, published)
            {
                Resume = resume;
            }

            public string Resume
            {
                get => _resume;
                set => _resume = value;
            }

            public override string GenerateDescription()
            {
                return base.GenerateDescription() +
                    "Резюме: " + Resume + "\n";
            }

            public override bool Equals(object other) // Переопределение
            {
                if ((other is null) || !this.GetType().Equals(other.GetType()))
                    return false;

                Book otherBook = (Book)other;

                return base.Equals(otherBook) && this.Resume.Equals(otherBook.Resume);
            }
        }

        class Section // Отдел
        {
            private string _name;
            private List<Edition> _editions;

            public Section()
            {
                Editions = new List<Edition>();
            }
            public Section(string name) : this()
            {
                Name = name;
            }

            public string Name
            {
                get => _name;
                set => _name = value;
            }

            public List<Edition> Editions
            {
                get => _editions;
                set => _editions = value;
            }

            public Edition Edition() => Editions[Editions.Count - 1];
            public Edition Edition(int index) => Editions[index];

            public void AddEdition(Edition item)
            {
                Editions.Add(item);
            }
        }

        class Library // Библиотека
        {
            private string _name;
            private List<Section> _sections;

            public Library()
            {
                Sections = new List<Section>();
            }
            public Library(string name) : this()
            {
                Name = name;
            }

            public string Name
            {
                get => _name;
                set => _name = value;
            }

            public List<Section> Sections
            {
                get => _sections;
                set => _sections = value;
            }

            public Section Section() => Sections[Sections.Count - 1];
            public Section Section(string name) => Sections.Find(section => section.Name == name);

            public void AddSection(Section item)
            {
                Sections.Add(item);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Library.Library library = new Library.Library();

            // Генерация жанров
            library.AddSection(new Library.Section("Тяжкое для осознания"));
            library.AddSection(new Library.Section("Жеванная бумага"));

            var editions = new List<Library.Edition>();

            editions.Add(new Library.Magazine( //Добавление в каталог Журнала
                "Гороскопы и всякое", // Название журнала
                "Шишки-телепаты",   // Издательство, оно же авторство
                new DateTime(2022, 4, 25), // Дата издания ГГГГ-ММ-ДД
                new List<string> { // Список статей
                    "Настоящее место жительства Гитлера - Антарктида?",
                    "Пожар на заводе искусственных кошек - трагедия или комедия?",
                    //"Ад для тихоходок",
                    //"Полный курс социал-дарвинизма",
                    //"Трёхмоторный дельтаплан летает быстрее мухи?",
                    //"Анна Чапман с РЕН-ТВ открывает глаза",
                    //"Гороскоп",
                    //"Зулейха закрывает глаза",
                    //"Сколько лет Софии Ротару на самом деле?",
                    //"Чулпан Хаматова - посредственность или серость?"
                    //Тестировал на вывод. 
                } // Просьба - не спрашивайте про названия статей. 
                ));

            editions.Add(new Library.Book( // Добавление Книги
                "Государство и революция", // Название
                "Владимир Ленин",          // Автор
                new DateTime(1918, 9, 14), // Дата издания
                "Политический труд Владимира Ленина, пересматривающий идеи Маркса.") // Резюме
                );

            editions.Add(new Library.Book(
                "Живи, вкалывай, сдохни",
                "Кори Пайн",
                new DateTime(2019, 5, 25),
                "Репортаж с тёмной стороны Кремниевой Долины.")
                );

            // Добавляем издания в отдел
            var neededSection = library.Section("Тяжкое для осознания"); // Обязано совпадать с блоком генерации жанров!

            neededSection.Editions = editions;

            // Вывод описания изданий данного отдела
            Console.WriteLine($"Издания из отдела \"{neededSection.Name}\": \n");
            foreach (var edition in neededSection.Editions)
            {
                Console.WriteLine(edition.GenerateDescription());
            }

        }
    }
}