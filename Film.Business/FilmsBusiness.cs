using System;
using System.IO;
using System.Linq;
using System.Text;
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

        public GetProducersResponseDto GetProducers()
        {
            var response = new GetProducersResponseDto();

            var entities = filmContext.Film.ToList();
            var groupByList = entities.Where(l => l.Winner).GroupBy(l => l.Producer);
            var minValue = 9999;
            var maxValue = 0;

            foreach (var item in groupByList)
            {
                var lastValue = 0;
                foreach (var producer in item.OrderBy(l => l.Year))
                {
                    if (lastValue == 0) 
                    {
                        lastValue = producer.Year;
                        continue;
                    }

                    if ((producer.Year - lastValue) < minValue)
                    {
                        response.Min.Clear();
                        minValue = producer.Year - lastValue;
                        response.Min.Add(new ProducerDto(producer.Producer, minValue, producer.Year - minValue, producer.Year));
                    } 
                    else if ((producer.Year - lastValue) == minValue)
                    {
                        response.Min.Add(new ProducerDto(producer.Producer, minValue, producer.Year - minValue, producer.Year));
                    }

                    if ((producer.Year - lastValue) > maxValue)
                    {
                        response.Max.Clear();
                        maxValue = producer.Year - lastValue;
                        response.Max.Add(new ProducerDto(producer.Producer, maxValue, producer.Year - maxValue, producer.Year));
                    }
                    else if ((producer.Year - lastValue) == maxValue)
                    {
                        response.Max.Add(new ProducerDto(producer.Producer, maxValue, producer.Year - maxValue, producer.Year));
                    }

                    lastValue = producer.Year;
                }
            }

            return response;
        }

        public async void ReadFilms(StreamReader streamReader)
        {
            var sb = new StringBuilder();
            var lineNumber = 0;

            while (!streamReader.EndOfStream)
            {
                try
                {
                    var line = streamReader.ReadLine().Split(';');
                    lineNumber++;
                    if (lineNumber == 1) continue;
                    int year = 0;

                    if (line.Length > 5) throw new FilmException($"Erro linha {lineNumber}! Layout fora do padrão definido (year, title, studios, producers, winner)");

                    if (line[4] != null && !string.IsNullOrEmpty(line[4]) && !(line[4].ToUpper().Equals("YES") || line[4].ToUpper().Equals("NO")))
                        throw new FilmException($"Erro linha {lineNumber}! coluna de vencedor com parâmetro diferente do esperado (yes, no)");

                    if (!Int32.TryParse(line[0], out year)) throw new FilmException($"Erro linha {lineNumber}! ano informado não é um número válido");

                    filmContext.Film.Add(new Entity.Film()
                    {
                        Producer = line[3],
                        Studio = line[2],
                        Title = line[1],
                        Winner = line[4] != null ? line[4].ToUpper().Equals("YES") : false,
                        Year = year
                    });
                }
                catch (FilmException e)
                {
                    sb.AppendLine(e.Message);
                }
                catch (Exception e)
                {
                    sb.AppendLine(e.ToString());
                }
            }

            if (sb.ToString().Length > 0) new FileHelper().SaveTextFile(sb.ToString(), $"log_{DateTime.Now.ToString("ddMMyyyy_HHmmss")}.txt");

            await filmContext.SaveChangesAsync();
        }
    }
}
