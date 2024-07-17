using System;

namespace ValantDemoApi.Domain
{
    public class PersonaRecord
    {
        public PersonaRecord(int totalMistakes, int totalSeconds)
        {
            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
            this.TotalMistakes = totalMistakes;
            this.TotalSeconds = totalSeconds;
        }

        public Guid Id { get; init; }
        public DateTime CreationDate { get; init; }
        public int TotalMistakes { get; init; }
        public int TotalSeconds { get; init; }
    }
}
