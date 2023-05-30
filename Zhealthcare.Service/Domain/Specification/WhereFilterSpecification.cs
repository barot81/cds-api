using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Specification;
using Microsoft.Azure.CosmosRepository.Specification.Builder;
using System.ComponentModel;
using Zhealthcare.Service.Domain.Entities;
using Zhealthcare.Service.Models;

namespace Exxat.Meta.Infrastructure.CosmosDb
{
    public class PatientFilterSpecification : DefaultSpecification<Patient>
    {
        public PatientFilterSpecification(string facilityId, PageFilterModel filterModel)
        {
            Query.Where(x => x.Type == nameof(Patient) && x.PartitionKey == facilityId)
                .ApplyFilters(filterModel?.Filters)
                .ApplyPagination(filterModel?.PageSize, filterModel?.Start)
                .ApplyOrderBy(filterModel?.SortBy, filterModel?.Order);
        }
    }

    public static class QueryExtensions
    {
        public static ISpecificationBuilder<Patient, IQueryResult<Patient>> ApplyFilters(this ISpecificationBuilder<Patient, IQueryResult<Patient>> Query, PatientFilter? filters)
        {
            if (filters != null)
            {
                var statusFilter = filters?.Status;
                if (statusFilter != null && statusFilter.Any())
                    Query.Where(x => statusFilter.Contains(x.ReviewStatus));

                var queryStatusFilter = filters?.QueryStatus;
                if (queryStatusFilter != null && queryStatusFilter.Any())
                    Query.Where(x => queryStatusFilter.Contains(x.QueryStatus));

                DateTime? admStartDate = filters?.AdmissionStartDate;
                DateTime? admEndDate = filters?.AdmissionEndDate;
                if (admStartDate != null && admEndDate != null)
                    Query.Where(x => x.AdmitDate >= admStartDate && x.AdmitDate <= admEndDate);

                DateTime? disStartDate = filters?.DischargeStartDate;
                DateTime? disEndDate = filters?.DischargeEndDate;
                if (disStartDate != null && disEndDate != null)
                    Query.Where(x => x.DischargeDate >= disStartDate && x.DischargeDate <= disEndDate);
            }
            return Query;
        }

        public static ISpecificationBuilder<Patient, IQueryResult<Patient>> ApplyPagination(this ISpecificationBuilder<Patient, IQueryResult<Patient>> Query, int? PageSize = 50, int? Start = 0)
        {
            Query.PageNumber((Start!.Value / PageSize!.Value) + 1);
            Query.PageSize(PageSize!.Value);
            return Query;
        }

        public static ISpecificationBuilder<Patient, IQueryResult<Patient>> ApplyOrderBy(this ISpecificationBuilder<Patient, IQueryResult<Patient>> Query, string? OrderByProp = "FirstName", int? Order = 1)
        {
            if (OrderByProp != null)
            {
                PropertyDescriptor? prop = TypeDescriptor.GetProperties(typeof(Patient)).Find(OrderByProp, true);
                if (prop != null)
                {
                    if (Order == 1)
                        Query.Where(x => prop.GetValue(x) != null).OrderBy(x => prop.GetValue(x) ?? x.PatientName);
                    else
                        Query.Where(x => prop.GetValue(x) != null).OrderByDescending(x => prop.GetValue(x) ?? x.PatientName);
                }
            }
            return Query;
        }

    }
}