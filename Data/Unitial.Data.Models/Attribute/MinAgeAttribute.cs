using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unitial.Data.Models.Attribute
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int minAge;
        public MinAgeAttribute(int minAge)
            : this(minAge ,"")
        {

        }
        public MinAgeAttribute(int minAge, string errorMessage)
            : base(errorMessage)
        {
            this.minAge = minAge;

        }
        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(this.minAge) < DateTime.Now;
            }
            return false;
        }

    }
}
