using System.Collections.Generic;

namespace Film.Entity
{
    public class GetProducersResponseDto
    {
        public List<ProducerDto> Min { get; set; }
        public List<ProducerDto> Max { get; set; }

        public GetProducersResponseDto()
        {
            this.Min = new List<ProducerDto>();
            this.Max = new List<ProducerDto>();
        }
    }

    public class ProducerDto
    {
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
        public ProducerDto(string producer, int interval, int previousWin, int followingWin)
        {
            this.Producer = producer;
            this.Interval = interval;
            this.PreviousWin = previousWin;
            this.FollowingWin = followingWin;
        }
    }
}