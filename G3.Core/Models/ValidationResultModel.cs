using System.Collections.Generic;

namespace G3.Core.Models
{
    public class ValidationResultModel
    {
        public bool IsValid { get; private set; } = true; // Default to true

        public List<ValidationResultMessageModel> Results { get; } = new List<ValidationResultMessageModel>();

        public void AddMessage(string fieldName, string message)
        {
            Results.Add(new ValidationResultMessageModel
            {
                Message = message,
                FieldName = fieldName
            });

            // Assume error at this stage
            IsValid = false;
        }
    }
}