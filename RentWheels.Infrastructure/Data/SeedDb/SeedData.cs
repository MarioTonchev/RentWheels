using RentWheels.Infrastructure.Models;

namespace RentWheels.Infrastructure.Data.SeedDb
{
	internal class SeedData
	{
        public Car FirstCar { get; set; }
        public Car SecondCar { get; set; }
        public Engine SmallEngine { get; set; }
        public Engine MediumEngine { get; set; }
        public Engine BigEngine { get; set; }
        public Engine SportEngine { get; set; }
        public Category Sedan { get; set; }
        public Category Hatchback { get; set; }
        public Category SUV { get; set; }
        public Category Estate { get; set; }
        public Category Coupe { get; set; }
        public Category Supercar { get; set; }

        public SeedData()
		{
			SeedEngines();
			SeedCars();
			SeedCategories();
		}

        private void SeedEngines()
        {
            SmallEngine = new Engine()
            {
                Id = 1,
                Horsepower = 100,
                Cubage = 1400,
                FuelType = "diesel"            
            };

			MediumEngine = new Engine()
			{
				Id = 2,
				Horsepower = 180,
				Cubage = 2000,
				FuelType = "diesel"
			};

			BigEngine = new Engine()
			{
				Id = 3,
				Horsepower = 240,
				Cubage = 2200,
				FuelType = "gasoline"
			};

			SportEngine = new Engine()
			{
				Id = 4,
				Horsepower = 500,
				Cubage = 4000,
				FuelType = "gasoline"
			};
		}

        private void SeedCars()
        {
            FirstCar = new Car()
            {
                Id = 1,
                Brand = "Audi",
                Model = "A4",
                Year = 2017,
                Available = "true",
                Color = "black metallic",
                EngineId = 2,
                PricePerDay = 100,
				OwnerId = "572f859b-0afa-4112-aa5b-23a6d9560fca",
				ImageUrl = "https://as1.ftcdn.net/v2/jpg/03/63/44/86/1000_F_363448659_uZxsIp3cObzOiDx6oDi20fb3QFoYVAJF.jpg",
                CategoryId = 1
			};

            SecondCar = new Car()
            {
				Id = 2,
				Brand = "BMW",
				Model = "M5",
				Year = 2018,
				Available = "true",
				Color = "royal blue",
				EngineId = 4,
				PricePerDay = 350,
				OwnerId = "572f859b-0afa-4112-aa5b-23a6d9560fca",
				ImageUrl = "https://as1.ftcdn.net/v2/jpg/04/35/92/40/1000_F_435924070_A2n5ZyQUF7nCRsYZj6SX1SAYOn5sggFh.jpg",
                CategoryId = 1
			};
        }

		private void SeedCategories()
		{
			Sedan = new Category()
			{
				Id = 1,
				Name = "Sedan",
				DoorCount = 4
			};

            Hatchback = new Category()
            {
                Id = 2,
                Name = "Hatchback",
                DoorCount = 4
            };

            SUV = new Category()
            {
                Id = 3,
                Name = "SUV",
                DoorCount = 4
            };

            Estate = new Category()
            {
                Id = 4,
                Name = "Estate",
                DoorCount = 4
            };

            Coupe = new Category()
            {
                Id = 5,
                Name = "Coupe",
                DoorCount = 2
            };

            Supercar = new Category()
            {
                Id = 6,
                Name = "Supercar",
                DoorCount = 2
            };
        }
    }
}
