using CsvHelper.Configuration;
using Zhealthcare.Service.Application.Patients.Models;

namespace Zhealthcare.Service.Helper
{
    public sealed class PatientMap : ClassMap<PatientDto>
    {
        public PatientMap()
        {
            var conlumnConfig = FileReader
                    .LoadJsonData<ColumnMapper>("Data", "ColumnMapperByFacility", "appolo.json");
            var mapper = conlumnConfig.Mapper;
            Map(m => m.PatientNo).Name(mapper["PatientNo"]);
            Map(m => m.PatientName).Name(mapper["PatientName"]);
            Map(m => m.Room).Name(mapper["Room"]);
            Map(m => m.Age).Name(mapper["Age"]);
            Map(m => m.Sex).Name(mapper["Sex"]);
            Map(m => m.Los).Name(mapper["Los"]);
            Map(m => m.FinancialClass).Name(mapper["FinancialClass"]);
            Map(m => m.AdmitDate).Name(mapper["AdmitDate"]);
            Map(m => m.ReimbursementType).Name(mapper["ReimbursementType"]);
            Map(m => m.GeneralComment.Comments).Name(mapper["GeneralComment.Comments"]);
            Map(m => m.UmReviewer).Name(mapper["UmReviewer"]);
            Map(m => m.Dcp).Name(mapper["Dcp"]);
            Map(m => m.PatientType).Name(mapper["PatientType"]);
            Map(m => m.SecondaryPhysician).Name(mapper["SecondaryPhysician"]);
            Map(m => m.DrgNo).Name(mapper["DrgNo"]);
            Map(m => m.Diagnosis).Name(mapper["Diagnosis"]);
            Map(m => m.ChiefComplaint).Name(mapper["ChiefComplaint"]);
            Map(m => m.AttendingPhysician).Name(mapper["AttendingPhysician"]);
            Map(m => m.AdmitOrigin).Name(mapper["AdmitOrigin"]);
            Map(m => m.OriginDesc).Name(mapper["OriginDesc"]);
            Map(m => m.Geo).Name(mapper["Geo"]);
            Map(m => m.Diff).Name(mapper["Diff"]);
            Map(m => m.RelWt).Name(mapper["RelWt"]);
            foreach (var propertyInfo in typeof(PatientDto).GetProperties())
            {
                if (!HasMappedProperty(propertyInfo.Name))
                {
                    Map(m=> propertyInfo).Ignore();
                }
            }
            Map(m=> m.FacilityId).Constant(conlumnConfig.FacilityId);
            
        }
        private bool HasMappedProperty(string propertyName)
        {
            // Check if a property is already mapped
            return MemberMaps.Any(map => map.Data.Member.Name == propertyName);
        }
    }
}
