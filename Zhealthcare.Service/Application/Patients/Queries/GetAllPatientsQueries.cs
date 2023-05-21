﻿using MediatR;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Azure.Cosmos;
using Zhealthcare.Service.Domain.Entities;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public record GetAllPatientsQueriesRequest(string FacilityId, PageFilterModel FilterModel) : IRequest<PageResponseModel> 
    {
        private static string SortingString(int Order) => $" Order By c['@sortBy'] {(Order == 1 ? "ASC" : "DESC")}";

        private static readonly string PaginationString = " OFFSET @offset LIMIT @pageSize";
        public QueryDefinition GetQueryDefination(string selectClause, bool applySorting= false, bool applyPagination = false)
        {
            var filterQuery = GetQueryString(FilterModel.Filters);
            var sortingQuery = applySorting ? SortingString(FilterModel.Order) : "";
            var paginationQuery = applyPagination ? PaginationString : "";
            var parameterizedQuery = new QueryDefinition(
                "SELECT " + selectClause + " FROM c WHERE c.partitionKey = @partitionKey and c.entityName = @type"
                + filterQuery
                + sortingQuery
                + paginationQuery
                )
                .WithParameter("@partitionKey", FacilityId)
                .WithParameter("@type", nameof(Patient))
                .WithParameter("@statuses", string.Join("','", FilterModel?.Filters?.Status ?? Array.Empty<string>()))
                .WithParameter("@queryStatuses", string.Join("','", FilterModel?.Filters?.QueryStatus ?? Array.Empty<string>()))
                .WithParameter("@admissionStartDate", FilterModel?.Filters?.AdmissionStartDate)
                .WithParameter("@admissionEndDate", FilterModel?.Filters?.AdmissionEndDate)
                .WithParameter("@dischargeStartDate", FilterModel?.Filters?.DischargeStartDate)
                .WithParameter("@dischargeEndDate", FilterModel?.Filters?.DischargeEndDate)
                .WithParameter("@sortBy", FilterModel?.SortBy)
                .WithParameter("@offset", FilterModel?.Start == 0 ? 0 : (FilterModel?.Start - 1) )
                .WithParameter("@pageSize", FilterModel?.PageSize);
            return parameterizedQuery;
        }
        private static string GetQueryString(PatientFilter? filters)
        {
            var filterQuery = "";
            if (filters == null)
                return "";
            if (filters.Status != null)
                filterQuery = " AND ARRAY_CONTAINS([@statuses], c.status)";
            if (filters.QueryStatus != null)
                filterQuery = " AND ARRAY_CONTAINS([@queryStatuses], c.queryStatus)";
            if (filters.AdmissionStartDate != null && filters.AdmissionEndDate != null)
                filterQuery = " AND c.admissionDate >= @admissionStartDate &&  c.admissionDate <=  @admissionEndDate";
            if (filters.DischargeStartDate != null && filters.DischargeEndDate != null)
                filterQuery = " AND c.dischargeDate >= @dischargeStartDate &&  c.dischargeDate <=  @dischargeEndDate";
            return filterQuery;
        }

          

    }

}