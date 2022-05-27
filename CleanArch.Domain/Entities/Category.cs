using System;
using System.Collections.Generic;
using CleanArch.Domain.Validation;

namespace CleanArch.Domain.Entities
{
    public sealed class Category
    {
        public int Id {get; private set;}
        public string Name {get; private set;}
        
        public Category(string name)
        {
            ValidateDomain(name);
        }
        
        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, 
                "Invalid Id");
                Id = id;
            ValidateDomain(name);
        }

        public ICollection<Product> Products {get; set;}

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
               "Invalid name.Name is required");
            
            DomainExceptionValidation.When(name.Length < 3,
               "Invalid name. Too short");

            Name = name;
               
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }
    }
}