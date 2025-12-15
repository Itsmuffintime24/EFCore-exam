using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace  VinylStore
{
    public class Vinyl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string BandName { get; set; }
        public string Distributor { get; set; }
        public int TracksNum { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public int CostForStore { get; set; }
        public int Cost { get; set; }
        
        public int Stock { get; set; }
        public int Sold { get; set; }
        public int CurrentCost { get; set; }
    }

    public class Sale
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        public List<Vinyl> Vinyls { get; set; } = new();
    }

    public class VinylContext : DbContext
    {
        public DbSet<Vinyl> Vinyls {get;set;}
        public DbSet<Sale> Sales { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("UniversityDB");
        }
    }

    public class VinylManager
    {
        public int Budget { get; set; }

        public void AddVinyl(string name,string author,string bandName,string distributor,int tracksNum,string genre,int year,int costForStore,int cost,int stock)
        {
            using (var db = new VinylContext())
            {
                var vinyl = new Vinyl
                {Name = name,Author = author,BandName = bandName,Distributor = distributor,TracksNum = tracksNum,Genre = genre,Year = year,CostForStore = costForStore,Cost = cost,CurrentCost = cost,Stock = stock,Sold = 0
                };

                db.Vinyls.Add(vinyl);
                db.SaveChanges();
            }
        }

        public void RemoveVinyl(int id)
        {
            using (var db = new VinylContext())
            {
                db.Remove(db.Vinyls.FirstOrDefault(s => s.Id == id));
                db.SaveChanges();
            }
        }

        public void RedactVinyl<T>(int Id,int propertyId,T varToChange )
        {
            
            using (var db = new VinylContext())
            {
                var entity = db.Vinyls.FirstOrDefault(s => s.Id == Id);
                switch (propertyId)
                {
                    case 1:
                        if (varToChange is string Name)
                        {
                            entity.Name = Name;
                        }
                        break;
                    case 2:
                        if (varToChange is string Author)
                        {
                            entity.Author = Author;
                        }
                        break;
                    case 3:
                        if (varToChange is string BandName)
                        {
                            entity.BandName = BandName;
                        }
                        break;
                    case 4:
                        if (varToChange is string Distributor)
                        {
                            entity.Distributor = Distributor;
                        }
                        break;
                    case 5:
                        if (varToChange is int TracksNum)
                        {
                            entity.TracksNum = TracksNum;
                        }
                        break;
                    case 6:
                        if (varToChange is string Genre)
                        {
                            entity.Genre = Genre;
                        }
                        break;
                    case 7:
                        if (varToChange is int Year)
                        {
                            entity.Year = Year;
                        }
                        break;
                    case 8:
                        if (varToChange is int StoreCost)
                        {
                            entity.CostForStore = StoreCost;
                        }
                        break;
                    case 9:
                        if (varToChange is int Cost)
                        {
                            entity.Cost = Cost;
                        }
                        break;
                    case 10:
                        if(varToChange is int Stock)
                        {
                            entity.Stock = Stock;
                        }
                        break;
                    case 11:
                        if(varToChange is int Sold)
                        {
                            entity.Sold = Sold;
                        }
                        break;
                    case 12:
                        if (varToChange is int CurrentCost)
                        {
                            entity.CurrentCost = CurrentCost;
                        }
                        break;
                }
                db.SaveChanges();
            }
        }
        public void AddSale(List<int> Ids, int percent)
        {
            using (var db = new VinylContext())
            {
                List<Vinyl> entities = new();
                foreach(int i in Ids)
                {
                    var entity = db.Vinyls.FirstOrDefault(s => s.Id == i);
                    entity.CurrentCost -= (entity.Cost / 100) * percent;
                    entities.Add(entity);

                }
                db.Sales.Add(new Sale { Percent = percent, Vinyls = entities });
                db.SaveChanges();
            }
        }
        public void RemoveSale(int Id)
        {
            using (var db = new VinylContext())
            {
                var entity = db.Sales.FirstOrDefault(s => s.Id == Id);
                foreach(Vinyl i in entity.Vinyls)
                {
                    i.CurrentCost = i.Cost;
                }
                db.SaveChanges();
                db.Sales.Remove(entity);
                db.SaveChanges();
            }
        }


        public void SellVinyl(int Id, int num)
        {
            using (var db = new VinylContext())
            {
                var entity = db.Vinyls.FirstOrDefault(s => s.Id == Id);
                if (entity.Stock - num >= 0)
                {
                    Console.WriteLine($"Success! {num} copies of {entity.Name} were sold!");
                    entity.Stock -= num;
                    Budget += num*entity.Cost;
                    entity.Sold += num;
                }
                else
                {
                    Console.WriteLine($"Sorry, There is not enough copies to be sold");
                }
            }
        }
        public void SeedData()
        {
            using (var db = new VinylContext())
            {
                AddVinyl("American Idiot", "Green Day", "Green Day", "Reprise Records", 13, "Punk Rock", 2004, 15, 25, 50);
                AddVinyl("Toxicity", "System of a Down", "System of a Down", "American Recordings", 14, "Alternative Metal", 2001, 18, 30, 40);
                AddVinyl("Scars on Broadway", "Daron Malakian", "Scars on Broadway", "Interscope", 13, "Alternative Metal", 2008, 17, 28, 30);
                AddVinyl("Monopoly Money", "Moonwalker", "Moonwalker", "Epic Records", 12, "Hip-Hop", 2008, 10, 20, 25);
                AddVinyl("Hybrid Theory", "Linkin Park", "Linkin Park", "Warner Bros.", 12, "Nu Metal", 2000, 16, 26, 60);
                AddVinyl("Back in Black", "AC/DC", "AC/DC", "Atlantic Records", 10, "Hard Rock", 1980, 20, 35, 45);
                AddVinyl("The Black Parade", "My Chemical Romance", "My Chemical Romance", "Reprise Records", 13, "Alternative Rock", 2006, 15, 27, 50);
                AddVinyl("Californication", "Red Hot Chili Peppers", "Red Hot Chili Peppers", "Warner Bros.", 15, "Alternative Rock", 1999, 18, 32, 35);
                AddVinyl("Nevermind", "Nirvana", "Nirvana", "DGC Records", 12, "Grunge", 1991, 15, 30, 40);
                AddVinyl("Parachutes", "Coldplay", "Coldplay", "Parlophone", 10, "Alternative Rock", 2000, 14, 25, 50);
                AddVinyl("Lateralus", "Tool", "Tool", "Volcano Entertainment", 13, "Progressive Metal", 2001, 19, 33, 30);
                AddVinyl("A Night at the Opera", "Queen", "Queen", "EMI", 12, "Rock", 1975, 17, 29, 40);
                AddVinyl("Appetite for Destruction", "Guns N' Roses", "Guns N' Roses", "Geffen Records", 12, "Hard Rock", 1987, 20, 35, 45);
                AddVinyl("Ten", "Pearl Jam", "Pearl Jam", "Epic Records", 11, "Grunge", 1991, 16, 28, 50);
                AddVinyl("Ride the Lightning", "Metallica", "Metallica", "Elektra Records", 8, "Thrash Metal", 1984, 18, 31, 40);
                db.SaveChanges();

                var vinyls = db.Vinyls.ToList();

                var sale1VinylIds = vinyls.Where(v => v.Author == "Green Day" || v.Author == "AC/DC").Select(v => v.Id).ToList();
                var sale2VinylIds = vinyls.Where(v => v.Author == "Nirvana" || v.Author == "Pearl Jam").Select(v => v.Id).ToList();

                AddSale(sale1VinylIds, 10);
                AddSale(sale2VinylIds, 20);
            }
        }




        public void MainMenu()
        {
            
        }

    }

}