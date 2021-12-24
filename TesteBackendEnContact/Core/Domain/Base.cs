using System.ComponentModel.DataAnnotations;

namespace TesteBackendEnContact.Core.Domain
{
    public class Base
    {
        [Key]
        public int Id { get; set; }

        public Base()
        {

        }

        public Base(int id)
        {
            Id = id;
        }
    }
}
