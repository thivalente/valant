using System;

namespace ValantDemoApi.Models.Requests
{
    public record PersonaRecordResponseModel(Guid Id, DateTime CreationDate, int TotalMistakes, int TotalSeconds)
    {
        public string FormattedTime
        {
            get
            {
                if (TotalSeconds < 60)
                    return $"{TotalSeconds} sec";

                if (TotalSeconds < 3600)
                {
                    int minutes = TotalSeconds / 60;
                    int seconds = TotalSeconds % 60;

                    return $"{minutes} min {seconds} sec";
                }

                return "> 1 hour";
            }
        }
    }
}
