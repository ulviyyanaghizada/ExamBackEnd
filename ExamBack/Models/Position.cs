using ExamBack.Models.Base;

namespace ExamBack.Models
{
    public class Position:BaseEntityName
    {
        public ICollection<Employee>? Employees { get; set;}
    }
}
