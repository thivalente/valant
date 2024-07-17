using ValantDemoApi.Domain;
using ValantDemoApi.Models.Requests;

namespace ValantDemoApi.Mappers
{
    public static class PersonalRecordMapper
    {
        public static PersonaRecordResponseModel ToResponseModel(this PersonaRecord personaRecord)
        {
            if (personaRecord is null)
                return null;

            return new PersonaRecordResponseModel(personaRecord.Id, personaRecord.CreationDate, personaRecord.TotalMistakes, personaRecord.TotalSeconds);
        }
    }
}
