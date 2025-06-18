namespace SFA.DAS.AssessorService.ExternalApi.Examples.CsvClassMaps
{
    using CsvHelper.Configuration;
    using SFA.DAS.AssessorService.ExternalApi.Core.Messages.Request.Epa;
    using System.Collections.Generic;

    public sealed class UpdateEpaRequestMap : ClassMap<UpdateEpaRequest>
    {
        public UpdateEpaRequestMap()
        {
            Map(m => m.RequestId);
            Map(m => m.EpaReference);
            Map(m => m.Standard).Convert(row => row.Row.GetRecord<Core.Models.Certificates.Standard>());
            Map(m => m.Learner).Convert(row => row.Row.GetRecord<Core.Models.Certificates.Learner>());
            References<EpaDetailsMap>(m => m.EpaDetails);
        }

        public sealed class EpaDetailsMap : ClassMap<Core.Models.Epa.EpaDetails>
        {
            public EpaDetailsMap()
            {
                Map(m => m.LatestEpaDate).Ignore();
                Map(m => m.LatestEpaOutcome).Ignore();
                Map(m => m.Epas).Convert(row => new List<Core.Models.Epa.EpaRecord> { row.Row.GetRecord<Core.Models.Epa.EpaRecord>() });
            }
        }
    }
}
