using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ExampleForStudents.Contracts
{
    public class ResponseWrapperDto<T>
    {
        public ResponseWrapperDto()
        {
            Errors = new List<string>();
        }

        public ResponseWrapperDto(T data) : this()
        {
            Data = data;
        }

        public ResponseWrapperDto(string error) : this()
        {
            Errors.Add(error);
        }

        public ResponseWrapperDto(IEnumerable<string> errors) : this()
        {
            Errors.AddRange(errors);
        }

        public T Data { get; init; }
        public bool Success => !Errors.Any();
        public List<string> Errors { get; set; }

        public HttpStatusCode? StatusCode { get; init; }
    }
}