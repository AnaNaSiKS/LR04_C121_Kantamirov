using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR04_C121_Kantamirov.Модель
{   /// <summary>
    /// Класс часов 
    /// </summary>
    public class Clock
    {
        #region Набор свойств класса Clock
        public string Model { get; set; }//Модель часов

        public string Brand { get; set; }//Брэнд часов

        public string ProducerFactory { get; set; }// Завод-изготовитель

        public string TypeOfWatch { get; }// Тип часов

        public string PresenceOfDefects { get; set; }// Наличие дефектов

        public DateTime ReleaseDate { get; set; }//Даты выпуска

        public double Price { get; set; }//Цена часов

        public int CountOfClock { get; set; }//Количество часов
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="brand">Брэнд</param>
        /// <param name="producerFactory">Завод - изготовитель</param>
        /// <param name="typeOfWatch">Тип часов</param>
        /// <param name="presenceOfDefects">Наличие дефекта</param>
        /// <param name="releaceDate">Дата выпуска</param>
        /// <param name="price">Цена</param>
        /// <param name="countOfClock">Количество часов</param>

        public Clock(string model,
                     string brand, 
                     string producerFactory, 
                     string typeOfWatch, 
                     string presenceOfDefects, 
                     DateTime releaceDate, 
                     double price, 
                     int countOfClock)
        {
            #region Проверка условий
            if (string.IsNullOrWhiteSpace(model)) { 
                throw new ArgumentNullException("Модель часов не может быть пустым или null", nameof(model));
            }

            if (string.IsNullOrWhiteSpace(brand)) {
                throw new ArgumentNullException("Брэнд часов не может быть пустым или null",nameof(brand));
            }

            if (string.IsNullOrWhiteSpace(producerFactory)) {
                throw new ArgumentNullException("Завод - изготовитель не может быть пустым или null", nameof(producerFactory));
            }

            if (string.IsNullOrWhiteSpace(typeOfWatch)) {
                throw new ArgumentNullException("Тип часов не может быть пустым или null",nameof(typeOfWatch));
            }

            if (string.IsNullOrWhiteSpace(presenceOfDefects)) {
                throw new ArgumentException("Невозможное значение дефекта ", nameof(presenceOfDefects));
            }

            if (releaceDate < DateTime.Parse("01.01.1334") && releaceDate > DateTime.Now) {
                throw new ArgumentException("Невозможное значение даты", nameof(releaceDate));
            }

            if (price < 0) {
                throw new ArgumentException("Невозможное значение цены", nameof(price));
            }

            if (countOfClock < 0) {
                throw new ArgumentException("Невозможное значение количества", nameof(countOfClock));
            }
            #endregion

            Model = model;
            Brand = brand;
            ProducerFactory = producerFactory;
            TypeOfWatch = typeOfWatch;
            PresenceOfDefects = presenceOfDefects;
            ReleaseDate = releaceDate;
            Price = price;
            CountOfClock = countOfClock;
        }
        public override string ToString()
        {
            double allprice = 0.0;
            if (PresenceOfDefects == "Есть")
                allprice = (Price * CountOfClock) - (Price * CountOfClock * 0.1);
            else allprice = Price * CountOfClock;
            return Model + " " + Brand + " " + "Цена: " +allprice.ToString() + " Тип часов: " + TypeOfWatch + " Цена за 1 шт. " + Price + " Количество: " + CountOfClock + " Дефект: " + PresenceOfDefects;
        }
    }
}
