﻿using MediatR;
using Microsoft.Azure.Cosmos;
using Zhealthcare.Service.Domain.Entities;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public record GetAllPatientsQueriesRequest(string FacilityId, PageFilterModel FilterModel) : IRequest<PageResponseModel>
    {
        private static string SortingString(int Order) => $" Order By c[@sortBy] {(Order == 1 ? "ASC" : "DESC")}";

        private static readonly string PaginationString = " OFFSET @offset LIMIT @pageSize";
        private static string SearchString(string searchTxt)
        {
            if (string.IsNullOrEmpty(searchTxt))
                return "";
            return $" AND (LOWER(c.patientName) like @searchQuery" +
                $" OR LOWER(ToString(c.patientNo)) like @searchQuery" +
                $" OR LOWER(c.room) like @searchQuery" +
                $" OR LOWER(c.cds) like @searchQuery" +
                $" OR LOWER(c.healthPlan) like @searchQuery" +
                $" OR LOWER(c.reviewStatus) like @searchQuery" +
                $" OR LOWER(c.reimbursementType) like @searchQuery" +
                $" OR LOWER(c.generalComment.comments) like @searchQuery)";
        }
        public QueryDefinition GetQueryDefination(string selectClause, bool applySorting = false, bool applyPagination = false)
        {
            var filterQuery = GetQueryString(FilterModel.Filters);
            var sortingQuery = applySorting ? SortingString(FilterModel.Order) : "";
            var paginationQuery = applyPagination ? PaginationString : "";
            var parameterizedQuery = new QueryDefinition(
                "SELECT " + selectClause + " FROM c WHERE c.partitionKey = @partitionKey AND c.entityName = @type"
                + filterQuery
                + SearchString(FilterModel.SearchQuery)
                + sortingQuery
                + paginationQuery
                )
                .WithParameter("@partitionKey", FacilityId)
                .WithParameter("@type", nameof(Patient))
                .WithParameter("@statuses", FilterModel?.Filters?.ReviewStatus ?? Array.Empty<string>())
                .WithParameter("@queryStatuses", FilterModel?.Filters?.QueryStatus ?? Array.Empty<string>())
                .WithParameter("@admitStartDate", FilterModel?.Filters?.AdmitStartDate)
                .WithParameter("@admitEndDate", FilterModel?.Filters?.AdmitEndDate)
                .WithParameter("@searchQuery", $"%{FilterModel?.SearchQuery.ToLower()}%")
                .WithParameter("@sortBy", FilterModel?.SortBy)
                .WithParameter("@offset", FilterModel?.Start == 0 ? 0 : (FilterModel?.Start - 1))
                .WithParameter("@pageSize", FilterModel?.PageSize);
            return parameterizedQuery;
        }
        private static string GetQueryString(PatientFilter? filters)
        {
            var filterQuery = "";
            if (filters == null)
                return "";
            if (filters.ReviewStatus != null) {
                if (filters.ReviewStatus.First().StartsWith("Total")) {
                    filterQuery += " AND c.dischargeDate = null and LOWER(c.reviewStatus) != 'non drg'";
                }
                else
                    filterQuery += " AND ARRAY_CONTAINS(@statuses, c.reviewStatus)";
            }
            if (filters.QueryStatus != null)
                filterQuery += " AND ARRAY_CONTAINS(@queryStatuses, c.queryStatus)";
            if (filters.AdmitStartDate != null && filters.AdmitEndDate != null)
                filterQuery += " AND c.admitDate >= @admitStartDate AND c.admitDate <=  @admitEndDate";
            return filterQuery;
        }

    }

}
