using Newtonsoft.Json;
using OfficeOpenXml;
using Zhealthcare.Service.Application.Patients.Models;
using Zhealthcare.Utility.Models;

namespace Zhealthcare.Utility.Services
{
    internal static class ExcelDataReaderService
    {
        public static List<PatientDto> LoadData()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Change this according to your licensing situation
            var filePath = "./Data/Case Management Census 8.8.23.xlsx";
            var mappingFilePath = "./Data/FacilityColumnMappings/AppoloFields.json";
            string json = File.ReadAllText(mappingFilePath);
            ColumnMap columnMaper = JsonConvert.DeserializeObject<ColumnMap>(json);

            var mapping = columnMaper.Mapping;
            using var package = new ExcelPackage(new FileInfo(filePath));
            var worksheet = package.Workbook.Worksheets[0]; // Assuming you want to read the first worksheet

            int startRow = 4; // Start reading from row 2

            int rowCount = worksheet.Dimension.Rows;

            var result = new List<PatientDto>();

            for (int row = startRow; row <= rowCount; row++)
            {
                var item = new PatientDto();

                foreach (var kvp in mapping)
                {
                    string excelHeader = kvp.Value;
                    string propertyName = kvp.Key;

                    var columnIndex = GetColumnIndexByName(worksheet, excelHeader);
                    if (columnIndex > 0)
                    {
                        var cellValue = worksheet.Cells[row, columnIndex].Value;
                        if (cellValue != null)
                        {
                            // Map cell value to model property using reflection
                            SetNestedPropertyValue(item, propertyName, cellValue);
                        }
                    }
                }
                item.FacilityId = columnMaper.FacilityId;

                result.Add(item);
            }
            return result;
            // Now you have the data mapped to the model class in the 'result' list
        }

        static void SetNestedPropertyValue(object obj, string propertyName, object value)
        {
            var propertyPath = propertyName.Split('.');
            var currentObj = obj;

            for (int i = 0; i < propertyPath.Length; i++)
            {
                var propertyInfo = currentObj.GetType().GetProperty(propertyPath[i]);
                if (propertyInfo == null)
                {
                    // Property not found, handle the situation accordingly
                    return;
                }

                if (i < propertyPath.Length - 1)
                {
                    // If it's not the last part of the property path, navigate deeper
                    currentObj = propertyInfo.GetValue(currentObj);

                    if (currentObj == null)
                    {
                        // Nested object not initialized, create a new instance
                        var nestedType = propertyInfo.PropertyType;
                        currentObj = Activator.CreateInstance(nestedType);
                        propertyInfo.SetValue(obj, currentObj);
                    }
                }
                else
                {
                    // Last part of the property path, set the value
                    if (propertyInfo.PropertyType == typeof(long) && long.TryParse(value.ToString(), out long parsedLong))
                    {
                        propertyInfo.SetValue(currentObj, parsedLong);
                    }
                    else if (propertyInfo.PropertyType == typeof(int) && int.TryParse(value.ToString(), out int parsedInt))
                    {
                        propertyInfo.SetValue(currentObj, parsedInt);
                    }
                    else
                    {
                        propertyInfo.SetValue(currentObj, value.ToString());
                    }
                }
            }
        }

        private static void MapPropertyValue(PatientDto item, string propertyName, object cellValue)
        {
            var property = typeof(PatientDto).GetProperty(propertyName);
            if (property != null)
            {
                if (property.PropertyType == typeof(Int64)) // Check if the property is of type Int64
                {
                    if (cellValue is string stringValue && Int64.TryParse(stringValue, out Int64 parsedValue))
                    {
                        property.SetValue(item, parsedValue);
                    }
                }
                else if (property.PropertyType == typeof(Int32)) // Check if the property is of type Int64
                {
                    if (cellValue is string stringValue && Int32.TryParse(stringValue, out Int32 parsedValue))
                    {
                        property.SetValue(item, parsedValue);
                    }
                }
                else
                {
                    property.SetValue(item, cellValue.ToString());
                }
            }
        }

        static int GetColumnIndexByName(ExcelWorksheet worksheet, string columnName)
        {
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                if (worksheet.Cells[3, col].Value != null && worksheet.Cells[3, col].Value.ToString() == columnName)
                {
                    return col;
                }
            }
            return -1; // Column not found
        }
    }
}
