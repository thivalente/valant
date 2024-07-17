using System.Collections.Generic;
using ValantDemoApi.Domain;

namespace ValantDemoApi.Infrastructure
{
    public interface IPersonalRecordRepository
    {
        PersonaRecord Add(int totalMistakes, int totalSeconds);
        List<PersonaRecord> GetAll();
        void RemoveAll();
    }

    internal class PersonalRecordRepository : IPersonalRecordRepository
    {
        private static List<PersonaRecord> _personalRecords = new();

        public PersonaRecord Add(int totalMistakes, int totalSeconds)
        {
            var newPersonalRecord = new PersonaRecord(totalMistakes, totalSeconds);

            if (_personalRecords is null)
                _personalRecords = new();

            _personalRecords.Add(newPersonalRecord);

            return newPersonalRecord;
        }

        public List<PersonaRecord> GetAll()
        {
            return _personalRecords;
        }

        public void RemoveAll()
        {
            _personalRecords = new();
        }
    }
}
