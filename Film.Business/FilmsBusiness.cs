using System;
using System.IO;
using Film.Entity;

namespace Film.Business
{
    public class FilmsBusiness
    {
        private readonly FilmContext filmContext;

        public FilmsBusiness(FilmContext filmContext)
        {
            this.filmContext = filmContext;
        }

        public void GetProducers()
        {
            filmContext.Film.Add(new Entity.Film()
            {
                Producer = "",
                Studio = "",
                Title = "",
                Winner = false,
                Year = 1
            });

            filmContext.SaveChanges();
        }

        public async void ReadFilms(StreamReader streamReader)
        {
            while (!streamReader.EndOfStream)
            {
                try
                {
                    var line = streamReader.ReadLine().Split(';');
                    filmContext.Film.Add(new Entity.Film()
                    {
                        Producer = line[3],
                        Studio = line[2],
                        Title = line[1],
                        Winner = line[4] != null ? line[4].ToUpper().Equals("TRUE") : false,
                        Year = Convert.ToInt32(line[0])
                    });
                }
                catch (Exception)
                {
                    Console.WriteLine("erro");
                }
            }

            await filmContext.SaveChangesAsync();
        }
    }
}
