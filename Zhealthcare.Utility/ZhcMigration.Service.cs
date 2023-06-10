using Exxat.Common.Components;
using MediatR;
using Microsoft.Extensions.Hosting;
using Zhealthcare.Service.Application.Lookups;
using Zhealthcare.Service.Application.Patients.Commands;
using Zhealthcare.Service.Application.Patients.Models;
using Zhealthcare.Service.Domain.Entities.Drg;
using Zhealthcare.Service.Domain.Entities.Lookup;
using Zhealthcare.Utility.Models;

namespace Zhealthcare.Utility
{
    internal class ZhcMigrationService : IHostedService, IDisposable
    {
        private readonly IMediator _mediator;
        private readonly CancellationTokenSource _stoppingCts =
                                                   new ();

        public ZhcMigrationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
           await MigratePatients(_stoppingCts.Token);
          // await MigrateLookups(_stoppingCts.Token);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Executed Sucessfully!!");
            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                Task.Delay(5, _stoppingCts.Token);
            }
            return Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _stoppingCts.Cancel();
        }
        private async Task MigrateLookups(CancellationToken cancellationToken)
        {
            List<string> failedIds = new();
            var msDrgItems = DataReaderService.LoadJsonDataFromFile<MsDrgLookupItem>("Data/lookups/MSDRGInfo.json");
            if (string.IsNullOrEmpty(await AddLookup("Lookups.MsDrg", msDrgItems, cancellationToken)))
                failedIds.Add("Lookups.MsDrg");

            var aprDrgLookupItem = DataReaderService.LoadJsonDataFromFile<AprDrgLookupItem>("Data/lookups/apr-drg.json");
            if (string.IsNullOrEmpty(await AddLookup("Lookups.AprDrg", aprDrgLookupItem, cancellationToken)))
                failedIds.Add("Lookups.AprDrg");

            var queryDiagnosisLookup = DataReaderService.LoadJsonDataFromFile<DiagnosisLookupItem>("Data/lookups/query diagnosis.json");
            if (string.IsNullOrEmpty(await AddLookup("Lookups.QueryDiagnosis", queryDiagnosisLookup, cancellationToken)))
                failedIds.Add("Lookups.QueryDiagnosis");

            var reimursementTypeLookup = DataReaderService.LoadJsonDataFromFile<ReimbursementTypeLookupItem>("Data/lookups/Reimbursement Type.json");
            if (string.IsNullOrEmpty(await AddLookup("Lookups.ReimursementType", reimursementTypeLookup, cancellationToken)))
                failedIds.Add("Lookups.ReimursementType");

            Console.WriteLine(failedIds);
        }

        private async Task<string> AddLookup(string id, IEnumerable<ILookupItem> lookupItems, CancellationToken cancellationToken)
        {
            try
            {
                var lookup = new Lookup<ILookupItem>(id, lookupItems);
                var result = await _mediator.Send(new AddLookupCommand<ILookupItem>(lookup), cancellationToken);
                if (!result)
                    return id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return id;
            }
            return "";
        }

        private async Task MigratePatients(CancellationToken cancellationToken)
        {
            var patients = DataReaderService.LoadJsonDataFromFile<PatientDto>("Data/CaseManagementCensus.json");
            var contract = DataReaderService.LoadJsonDataFromFile<Contract>("Data/contract.json");
            Random rnd = new();
            var Statuses = new List<string>() { "New", "Pending Query", "No Query", "Later Review", "Non DRG", "Reviewed" };
            var QueryStatuses = new List<string>() { "Pending", "Answered", "Completed", "Dropped", "No Response"};
            var FacilityIds = new List<string>() { "Z-healthcare", "Appolo", "Fortis", "Urgent Care D" };
            var ConcurrentPostDc = new List<string>() { "Retro", "Concurrent" };
            var CdsNames = new List<string>() { "Ashit", "Vishal", "Ankit" };
            var PatientClass = new List<string>() { "In", "Out" };
            foreach (var patient in patients)
            {
                patient.ReviewStatus = Statuses[rnd.Next(Statuses.Count)];
                patient.FacilityId = FacilityIds[rnd.Next(FacilityIds.Count)];
                patient.Concurrent_postDC = ConcurrentPostDc[rnd.Next(ConcurrentPostDc.Count)];
                patient.Mrn = $"M{GenerateRandomNumber()}";
                patient.Los = Convert.ToInt32(patient.Cur);
                patient.DrgNo = patient.Drg;
                patient.QueryStatus = QueryStatuses[rnd.Next(QueryStatuses.Count)];
                patient.ReimbursementType = contract.FirstOrDefault(x => x.Fc == patient.FinancialClass)?.ReimbursementType ?? "Non DRG";
                patient.Cds = CdsNames[rnd.Next(CdsNames.Count)];
                patient.PatientClass = PatientClass[rnd.Next(PatientClass.Count)];
            }
            List<Guid> FailedIds = new();
            foreach (var patient in patients)
            {
                try
                {
                    var result = await _mediator.Send(new CreatePatientCommand(patient.FacilityId, patient), cancellationToken);
                    if (result == null)
                        FailedIds.Add(patient.Id);
                }
                catch (Exception e)
                {
                    FailedIds.Add(patient.Id);
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine(FailedIds);
        }

        static int GenerateRandomNumber()
        {
            
                Random random = new();
                return random.Next(100000000, 1000000000);
        }

    }
}
