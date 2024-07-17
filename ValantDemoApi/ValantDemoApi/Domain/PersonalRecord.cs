using System;

namespace ValantDemoApi.Domain
{
    public class PersonaRecord
    {
        public PersonaRecord(int totalSeconds)
        {
            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
            this.TotalSeconds = totalSeconds;
        }

        public Guid Id { get; init; }
        public DateTime CreationDate { get; init; }
        public int TotalSeconds { get; init; }
    }
}
