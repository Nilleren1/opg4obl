using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using opg1obl;

namespace opg4obl.Manager
{
    public class CarManager
    {
        private static int nextID = 1;
        private static List<Car> data = new List<Car>()
        {
            new Car() {Id = nextID++, LicensePlate = "af92u95", Model = "Axios", Price = 24050},
            new Car() {Id = nextID++, LicensePlate = "az62u95", Model = "Susuki", Price = 29000},
            new Car() {Id = nextID++, LicensePlate = "kf82091", Model = "Golf", Price = 124000},
            new Car() {Id = nextID++, LicensePlate = "yj38805", Model = "Audi", Price = 124},
            new Car() {Id = nextID++, LicensePlate = "hg93019", Model = "Ferrari", Price = 24300}
           
        };

        /// <summary>
        /// A getAll method which you can filter in the url. 
        /// </summary>
        /// <param name="filterstring"></param>
        /// <returns>result</returns>
        public List<Car> GetAll(string filterstring)
        {
            List<Car> result = new List<Car>(data);
            if (!string.IsNullOrWhiteSpace(filterstring))
            {
                result = data.FindAll(c => c.Model.Contains(filterstring, StringComparison.OrdinalIgnoreCase));
            }
            return result;
        }


        public List<Car> GetByPrice(double carPrice)
        {
            List<Car> result;
            if (carPrice <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                result = data.FindAll(car => car.Price <= carPrice);
            }

            return result;
        }


        /// <summary>
        /// Finder en enkelt car, med det pågældende ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>En car med det korrekte ID</returns>
        public Car GetById(int ID)
        {
            //lærens løsning.
            return data.Find(car => car.Id == ID);



            //foreach (Item item in data)
            //{
            //    if (item.ID = ID)
            //    {
            //        return item;
            //    }

            //    return null;
            //}
        }

        public Car AddCar(Car car)
        {
            car.Id = nextID++;
            data.Add(car);

            return car;
        }

        public Car DeleteCar(int id)
        {
            Car car = GetById(id);

            if (car.Id == id)
            {
                data.Remove(car);
            }

            return car;

        }

        public Car UpdateCar(int id, Car carToBeUpdated)
        {
            var car = GetById(id);
            if (car == null)
            {
                return null;
            }
            else
            {
                car.Model = carToBeUpdated.Model;
                car.LicensePlate = carToBeUpdated.LicensePlate;
                car.Price = carToBeUpdated.Price;
            }
            return carToBeUpdated;
        }
    }
}
