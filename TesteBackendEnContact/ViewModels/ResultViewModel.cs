using System.Collections.Generic;

namespace TesteBackendEnContact.ViewModels
{
    public class ResultViewModel<T>
    {


        public T Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public ResultViewModel()
        {

        }

        public ResultViewModel(T data)
        {
            Data = data;
        }

        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }

        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }

    }
}
