using System.Collections.Generic;
using System.Linq;
using ValantDemoApi.Domain;
using ValantDemoApi.Infrastructure;

namespace ValantDemoApi.Application
{
    public interface IPersonalRecordService
    {
        PersonaRecord Add(int totalMistakes, int totalSeconds);
        List<PersonaRecord> GetAll();
        List<PersonaRecord> GetTop5();
        void RemoveAll();
    }

    internal class PersonalRecordService : IPersonalRecordService
    {
        private readonly IPersonalRecordRepository _personalRecordRepository;

        public PersonalRecordService(IPersonalRecordRepository personalRecordRepository)
        {
            this._personalRecordRepository = personalRecordRepository;
        }

        public PersonaRecord Add(int totalMistakes, int totalSeconds)
        {
            return this._personalRecordRepository.Add(totalMistakes, totalSeconds);
        }

        public List<PersonaRecord> GetAll()
        {
            return this._personalRecordRepository.GetAll();
        }

        public List<PersonaRecord> GetTop5()
        {
            var records = this._personalRecordRepository.GetAll();

            return records.OrderBy(r => r.TotalSeconds).ThenBy(r => r.TotalMistakes).Take(5).ToList();
        }

        public void RemoveAll()
        {
            this._personalRecordRepository.RemoveAll();
        }
    }
}