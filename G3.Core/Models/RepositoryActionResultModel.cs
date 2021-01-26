using G3.Core.Enums;
using G3.Core.Interfaces;

namespace G3.Core.Models
{
    public class RepositoryActionResultModel<TModel> 
        where TModel : class, IModel
    {
        public ResultTypeEnum ResultType { get; set; } 
        
        public TModel CurrentVersion { get; set; }
        
        public TModel PreviousVersion { get; set; }
        
        public ValidationResultModel ValidationResult { get; set; }
    }
}