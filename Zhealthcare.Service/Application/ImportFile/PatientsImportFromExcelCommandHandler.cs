﻿using Mapster;
using MediatR;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using OfficeOpenXml;
using Zhealthcare.Service.Application.Lookups;
using Zhealthcare.Service.Application.Patients.Commands;
using Zhealthcare.Service.Application.Patients.Models;
using Zhealthcare.Service.Application.Patients.Queries;
using Zhealthcare.Service.Domain.Entities.Drg;
using Zhealthcare.Service.Helper;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.ImportFile
{
    public class PatientsImportFromExcelCommandHandler : IRequestHandler<PatientsImportFromExcelCommand, ImportFileResponse>
    {
        private readonly IMediator _mediator;
        private readonly UserContext _userContext;

        public PatientsImportFromExcelCommandHandler(IMediator mediator, UserContext userContext)
        {
            _mediator = mediator;
            _userContext = userContext;
        }

        public async Task<ImportFileResponse> Handle(PatientsImportFromExcelCommand request, CancellationToken cancellationToken)
        {
            var patients = ReadExcelData(request.FacilityId, request.File);
            var contracts = FileReader.LoadJsonDataList<Contract>("Data", "contract.json");
            var patientNos = patients.Select(x => x.PatientNo).ToList();
            var existingPatients = await _mediator.Send(new GetAllNonDischagePatientsQuery(request.FacilityId), cancellationToken);
            HashSet<long> existingPatientNos = new(existingPatients.Select(x => x.PatientNo));
            var newPatients = patients.Where(x => !existingPatientNos.Contains(x.PatientNo));
            var msDrgLookup = await _mediator.Send(new GetMsDrgLookupRequest(), cancellationToken);
            var aprDrgLookup = await _mediator.Send(new GetMsDrgLookupRequest(), cancellationToken);
            foreach (var patient in newPatients)
            {
                var selectedContract = contracts.FirstOrDefault(x => x.Fc == patient.ReimbursementType);
                patient.ReimbursementType = selectedContract?.ReimbursementType ?? "Non DRG";
                patient.FinancialClass = selectedContract?.Fc ?? string.Empty;
                patient.HealthPlan = selectedContract?.Insurance ?? string.Empty;
                if (msDrgLookup.Items.Any(x => x.DrgNo == patient.DrgNo))
                    patient.ReimbursementType = "MS-DRG";
                else if (aprDrgLookup.Items.Any(x => x.DrgNo == patient.DrgNo))
                    patient.ReimbursementType = "APR-DRG";
                patient.ReviewStatus =
                   patient.ReimbursementType.ToLower().Contains("apr-drg")
                   || patient.ReimbursementType.ToLower().Contains("ms-drg")
                   ? "New"
                   : "Non DRG";

                await _mediator.Send(new CreatePatientCommand(request.FacilityId, patient), cancellationToken);
            }
            foreach (var patient in existingPatients)
            {
                if (!patientNos.Contains(patient.PatientNo))
                    patient.DischargeDate = DateTime.UtcNow.AddDays(-1);
                await _mediator.Send(new UpdatePatientCommand(patient.Id, request.FacilityId, patient.Adapt<PatientUpdateDto>(), _userContext.Name), cancellationToken);
            }
            return new ImportFileResponse(true, Enumerable.Empty<ErrorDetail>());
        }

        private static List<PatientDto> ReadExcelData(string facilityId, IFormFile file)
        {
            var conlumnConfig = FileReader.LoadJsonData<ColumnMapper>("Data", "ColumnMapperByFacility", $"{facilityId.ToLower()}.json");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Change this according to your licensing situation
            using var package = new ExcelPackage(file.OpenReadStream());
            var worksheet = package.Workbook.Worksheets[0];
            var patients = new List<PatientDto>();
            var headerRow = worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column];
            var columnIndexes = new Dictionary<string, int>();
            var noOfColumns = headerRow.End.Column;
            for (int col = 1; col <= noOfColumns; col++)
            {
                var columnName = headerRow[1, col].Text.Replace("\n", " ");
                var key = conlumnConfig.Mapper.FirstOrDefault(x => x.Value == columnName).Key;
                if (!string.IsNullOrWhiteSpace(key))
                    columnIndexes[key] = col;
            }

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var patient = new PatientDto { FacilityId = conlumnConfig.FacilityId.ToString() };

                foreach (var kvp in columnIndexes)
                {
                    var propertyName = kvp.Key;
                    var columnIndex = kvp.Value;
                    var cellValue = worksheet.Cells[row, columnIndex].Value;
                    SetNestedPropertyValue(patient, propertyName, cellValue);
                }
                patient.GeneralComment.AddedOn = string.IsNullOrWhiteSpace(patient.GeneralComment.Comments) ? null : DateTime.UtcNow;
                patients.Add(patient);
            }
            return patients;
        }
        static void SetNestedPropertyValue(Object patient, string propertyName, object propertyValue)
        {
            var propertyPath = propertyName.Split('.');
            var currentObject = patient;

            for (int i = 0; i < propertyPath.Length - 1; i++)
            {
                var propertyStep = propertyPath[i];
                var property = currentObject.GetType().GetProperty(propertyStep);
                if (property != null)
                {
                    var propertyInstance = property.GetValue(currentObject);
                    if (propertyInstance == null)
                    {
                        propertyInstance = Activator.CreateInstance(property.PropertyType);
                        property.SetValue(currentObject, propertyInstance);
                    }
                    currentObject = propertyInstance;
                }
                else
                {
                    Console.WriteLine($"Property '{propertyStep}' not found.");
                    return;
                }
            }

            var finalPropertyName = propertyPath.Last();
            var finalProperty = currentObject.GetType().GetProperty(finalPropertyName);
            if (finalProperty != null && propertyValue != null)
            {
                if(finalPropertyName.EndsWith("Date", StringComparison.InvariantCultureIgnoreCase) 
                    && double.TryParse(propertyValue.ToString(), out double numericDate))
                    propertyValue = DateTime.FromOADate(numericDate);  
                finalProperty.SetValue(currentObject, Convert.ChangeType(propertyValue, finalProperty.PropertyType));
            }
        }
    }
}
