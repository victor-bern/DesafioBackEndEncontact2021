﻿using System.Collections.Generic;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;

namespace TesteBackendEnContact.ViewModels
{
#nullable enable
    public class CompanyWithContactListViewModel : Company
    {
        public List<Contact>? Contacts { get; set; } = new();
    }

}
