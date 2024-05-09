using System.Linq.Expressions;
using System.Reflection.Metadata;
using paymentContext.Domain.Entities;

namespace paymentContext.Domain.Queries
{
    public static class StudentQueries
    {
        public static Expression<Func<Student, bool>> GetStudentInfo(string document)
        {
            return x => x.Document.Number == document;
        }
    }
}