using System.Net.Http.Headers;
using paymentContext.Domain.Commands;
using paymentContext.Domain.Entities;
using paymentContext.Domain.Enums;
using paymentContext.Domain.Handlers;
using paymentContext.Domain.Queries;
using paymentContext.Domain.ValueObjects;
using paymentContext.tests.Mocks;

namespace paymentContext.tests
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                _students.Add(new Student(
                    new Name("Cairo", i.ToString()),
                    new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                    new Email(i.ToString() + "@zup.io")
                ));
            }
        }
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678910");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.IsNull(student);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("11111111111");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.IsNotNull(student);
        }
    }
}