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
    }
    public class VinylContext
    {

    }

    public class VinylRepo
    {

    }
}